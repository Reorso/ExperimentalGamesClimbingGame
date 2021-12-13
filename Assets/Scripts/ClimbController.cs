using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;

public class ClimbController : MonoBehaviour
{
    //float lenght;
    private Transform centre;
    public TwoBoneIKConstraint[] limbs;
    public float[] lenghts, maxlenghts;
    public bool[] locked;
    public BlendConstraint centreC;
    private bool hipsMoving = false;
    Vector3[] pointsAttached;
    public GameObject centroid, centrebone;
    private int limbCounter = 0;
    public Camera cam;
    public GameObject[] debugs;

    // Start is called before the first frame update
    void Start()
    {
        lenghts = new float[4];
        maxlenghts = new float[4];
        centre = centreC.data.sourceObjectA;
        pointsAttached = new Vector3[4];
        locked = new bool[4];
        ResetLimbs();
        CalculateLenghts();
    }

    private void Update()
    {


        if (Input.GetMouseButton(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100, LayerMask.GetMask("Wall")))
            {
                if (centre.position.y < hit.point.y)
                {
                    MoveLimb(0, hit.point, false);
                }
                else
                {
                    MoveLimb(2, hit.point, false);
                }
            }
        }
        else if (Input.GetMouseButton(1))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100, LayerMask.GetMask("Wall")))
            {
                if (centre.position.y < hit.point.y)
                {
                    MoveLimb(1, hit.point, false);
                }
                else
                {
                    MoveLimb(3, hit.point, false);
                }
            }
        }
        else if (Input.GetMouseButton(2))
        {
            if (limbCounter > 0)
            {
                var ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, 100, LayerMask.GetMask("Centre")))
                {
                    centreC.weight = 1;
                    //move hips to mouse position constrained by limbs position
                    bool c = false;
                    
                    for (int i = 0; i < 4; i++)
                    {
                        if (locked[i])
                        {
                            Vector3 offset = hit.point - centre.position;
                            
                            if ((pointsAttached[i] - (limbs[i].data.root.position + offset)).magnitude > (lenghts[i]) )
                            {
                                print("limb num: " + i + "lenght : " + (pointsAttached[i] - limbs[i].data.root.position).magnitude);
                                c = true;
                            }
                            
                        }
                    }
                    if (!c)
                    {
                        centre.position = hit.point;
                    }
                    hipsMoving = true;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (var item in debugs)
            {
                item.GetComponent<MeshRenderer>().enabled = !item.GetComponent<MeshRenderer>().enabled;
            }
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene(0);
        }
        else if (Input.anyKeyDown)
        {
            ResetLimbs();
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100, LayerMask.GetMask("Route")))
            {
                
                if (centre.position.y < hit.point.y)
                {
                    if ((hit.collider.transform.position - (limbs[0].data.root.position)).magnitude < (lenghts[0]))
                    {
                        MoveLimb(0, hit.collider.transform.position, true);
                    }
                }
                else
                {
                    if ((hit.collider.transform.position - (limbs[2].data.root.position)).magnitude < (lenghts[2]))
                    {
                        MoveLimb(2, hit.collider.transform.position, true);
                    }
                }

                centroid.transform.position = limbCounter >= 3 ? FindCentroid(pointsAttached) : transform.position;

            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100, LayerMask.GetMask("Route")))
            {
                if (centre.position.y < hit.point.y)
                {
                    if ((hit.collider.transform.position - (limbs[1].data.root.position)).magnitude < (lenghts[1]))
                    {
                        MoveLimb(1, hit.collider.transform.position, true);
                    }
                }
                else
                {
                    if ((hit.collider.transform.position - (limbs[3].data.root.position)).magnitude < (lenghts[3]))
                    {
                        MoveLimb(3, hit.collider.transform.position, true);
                    }
                }
                centroid.transform.position = limbCounter >= 3 ? FindCentroid(pointsAttached) : transform.position;
            }
        }




        if (Input.GetMouseButtonUp(2) && hipsMoving)
        {
            //centreC.weight = 0;
            hipsMoving = false;
        }
    }



    private void MoveLimb(int n, Vector3 point, bool islocked)
    {
        Vector3 newpos = new Vector3();
        limbs[n].weight = 1;
        
        if (islocked)
        {
            limbCounter++;
            locked[n] = true;
            newpos = point;
        }
        else
        {
            locked[n] = false;
            newpos = limbs[n].data.root.position + ((point - limbs[n].data.root.position).normalized * Mathf.Clamp((point - limbs[n].data.root.position).magnitude, 0, lenghts[n]));
        }
        pointsAttached[n] = newpos;
        limbs[n].data.target.position = newpos;
        
    }
    private void ResetLimbs()
    {
        foreach (var limb in limbs)
        {
            limb.weight = 0;
            limb.data.target.position = limb.data.tip.position;
        }
        centreC.weight = 0;
        centreC.data.sourceObjectA.position = centreC.data.constrainedObject.position;
    }

    // Find the gravity centre
    private Vector3 FindCentroid(Vector3[] points)
    {
        var v3 = new Vector3();
        int i = 0;
        for (i = 0; i < points.Length; i++)
        {
            //if (locked[n])
            //{
                v3 += points[i];

            //}
            //else
            //{
                
            //}
        }
        v3 /= i;
        return v3;
    }
    private void CalculateLenghts()
    {
        for (int i = 0; i < limbs.Length; i++)
        {
            lenghts[i] = (limbs[i].data.tip.position - limbs[i].data.mid.position).magnitude +
                        (limbs[i].data.root.position - limbs[i].data.mid.position).magnitude;
            maxlenghts[i] = lenghts[i] + (centrebone.transform.position - limbs[i].data.root.position).magnitude;
            print(lenghts[i]);
        }
    }
}
