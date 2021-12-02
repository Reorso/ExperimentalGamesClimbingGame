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
    private float[] lenghts;
    private bool[] locked;
    public BlendConstraint centreC;
    private bool hipsMoving = false;
    Vector3[] pointsAttached;
    public GameObject centroid;
    private int limbCounter = 0;
    public Camera cam;
    public GameObject[] debugs;

    // Start is called before the first frame update
    void Start()
    {
        lenghts = new float[4];
        centre = centreC.data.sourceObjectA;
        pointsAttached = new Vector3[4];
        locked = new bool[4];
        ResetLimbs();
        CalculateLenghts();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (var item in debugs)
            {
                item.GetComponent<MeshRenderer>().enabled = !item.GetComponent<MeshRenderer>().enabled;
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene(0);
        }


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
                    centre.position = hit.point;
                    hipsMoving = true;
                }
            }
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
                    MoveLimb(0, hit.collider.transform.position,true);
                }
                else
                {
                    MoveLimb(2, hit.collider.transform.position, true);
                }
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100, LayerMask.GetMask("Route")))
            {
                if (centre.position.y < hit.point.y)
                {
                    MoveLimb(1, hit.collider.transform.position, true);
                }
                else
                {
                    MoveLimb(3, hit.collider.transform.position, true);
                }
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
            newpos = limbs[n].data.root.position + ((point - limbs[n].data.root.position).normalized * Mathf.Clamp((point - limbs[n].data.root.position).magnitude, 0, lenghts[n]));
        }
        pointsAttached[n] = newpos;
        limbs[n].data.target.position = newpos;
        centroid.transform.position = limbCounter >= 3 ? FindCentroid(pointsAttached) : transform.position;
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
        var i = 0;
        foreach (var variable in points)
        {
            v3 += variable;
            i++;
        }
        v3 /= i;
        return v3;
    }
    private void CalculateLenghts()
    {
        for (int i = 0; i < limbs.Length; i++)
        {
            lenghts[i] += (limbs[i].data.tip.position - limbs[i].data.mid.position).magnitude +
                          (limbs[i].data.root.position - limbs[i].data.mid.position).magnitude;
        }

    }
}
