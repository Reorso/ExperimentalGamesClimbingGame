using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;

public class Limb : MonoBehaviour
{
    //float lenght;
    private Transform targetHL, targetHR, targetLF, targetRF, centre;
    public ChainIKConstraint chikHL, chikHR;
    public TwoBoneIKConstraint tbcLF,tbcRF;
    public BlendConstraint centreC;
    private bool hipsMoving = false;
    Vector3[] pointsAttached;
    public Transform centroid;
    private int limbCounter = 0;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        targetHL = chikHL.data.target;
        targetHR = chikHR.data.target;
        targetLF = tbcLF.data.target;
        targetRF = tbcRF.data.target;
        centre = centreC.data.sourceObjectA;
        chikHL.weight = 0;
        chikHR.weight = 0;
        tbcLF.weight = 0;
        tbcRF.weight = 0;
        centreC.weight = 0;
        pointsAttached = new Vector3[4];
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Route")))
            {
                if(centre.position.y < hit.point.y){
                  chikHL.weight = 1;
                  Debug.Log(hit.transform.name);
                  Debug.Log("hit");
                  targetHL.position = hit.point;
                  pointsAttached[3] = hit.point;
                  limbCounter++;
                  if (limbCounter >= 3)
                  {
                      centroid.position = FindCentroid(pointsAttached);
                  }
                  else
                  {
                      centroid.position = transform.position;
                  }
                  

                }
                else{
                    tbcLF.weight = 1;
                    Debug.Log(hit.transform.name);
                    Debug.Log("hit");
                    targetLF.position = hit.point;
                    pointsAttached[0] = hit.point;
                    limbCounter++;
                    if (limbCounter >= 3)
                    {
                        centroid.position = FindCentroid(pointsAttached);
                    }
                    else
                    {
                        centroid.position = transform.position;
                    }
                }
            }

        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100 , LayerMask.GetMask("Route")))
            {
                if(centre.position.y < hit.point.y){
                  chikHR.weight = 1;
                  Debug.Log(hit.transform.name);
                  Debug.Log("hit");
                  targetHR.position = hit.point;
                  pointsAttached[2] = hit.point;
                  limbCounter++;
                  if (limbCounter >= 3)
                  {
                      centroid.position = FindCentroid(pointsAttached);
                  }
                  else
                  {
                      centroid.position = transform.position;
                  }
                }
                else{
                    tbcRF.weight = 1;
                    Debug.Log(hit.transform.name);
                    Debug.Log("hit");
                    targetRF.position = hit.point;
                    pointsAttached[1] = hit.point;
                    limbCounter++;
                    if (limbCounter >= 3)
                    {
                        centroid.position = FindCentroid(pointsAttached);
                    }
                    else
                    {
                        centroid.position = transform.position;
                    }
                }

                

            }

        }
        else if (Input.GetMouseButton(2))
        {
            
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Centre")))
            {
                centreC.weight = 1;
                //move hips to mouse position constrained by limbs position
                centre.position = hit.point;
                hipsMoving = true;
            }

        }
        else if (Input.anyKeyDown)
        {
            chikHL.Reset();
            chikHL.weight = 0;
            chikHR.Reset();
            chikHR.weight = 0;
            centreC.Reset();
            centreC.weight = 0;
            tbcLF.Reset();
            tbcLF.weight = 0;
            tbcRF.Reset();
            tbcRF.weight = 0;
            limbCounter = 0;
            
        }

        if (Input.GetMouseButtonUp(2) && hipsMoving)
        {
            //centreC.weight = 0;
            hipsMoving = false;
        }
    }
    // Find the polygon's centroid. followed tutorial at http://csharphelper.com/blog/2014/07/find-the-centroid-of-a-polygon-in-c/ 
    Vector3 FindCentroid(Vector3[] points)
    {

        Vector3 V3 = new Vector3();
        int i = 0;
        foreach (var VARIABLE in points)
        {
            if (VARIABLE != null)
            {
                V3 += VARIABLE;
                i++;
            }
        }

        V3 /= i;
    
        
        return V3;
    }

}
