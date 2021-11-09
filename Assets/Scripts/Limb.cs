using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Limb : MonoBehaviour
{
    //float lenght;
    private Transform targetHL, targetHR, targetLF, targetRF, centre;
    public ChainIKConstraint chikHL, chikHR;
    public TwoBoneIKConstraint tbcLF,tbcRF;
    public BlendConstraint centreC;
    private bool hipsMoving = false;

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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if(centre.position.y < hit.point.y){
                  chikHL.weight = 1;
                  Debug.Log(hit.transform.name);
                  Debug.Log("hit");
                  targetHL.position = hit.point;
                  
                }
                else{
                    tbcLF.weight = 1;
                    Debug.Log(hit.transform.name);
                    Debug.Log("hit");
                    targetLF.position = hit.point;
                }
            }

        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if(centre.position.y < hit.point.y){
                  chikHR.weight = 1;
                  Debug.Log(hit.transform.name);
                  Debug.Log("hit");
                  targetHR.position = hit.point;
                }
                else{
                    tbcRF.weight = 1;
                    Debug.Log(hit.transform.name);
                    Debug.Log("hit");
                    targetRF.position = hit.point;
                }

            }

        }
        else if (Input.GetMouseButton(2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Player")))
            {
                centreC.weight = 1;
                //move hips to mouse position constrained by limbs position
                centre.position = hit.point;
                hipsMoving = true;
            } 

        }
        else if (Input.anyKeyDown)
        {
            chikHL.weight = 0;
            chikHR.weight = 0;
            centreC.weight = 0;
            tbcLF.weight = 0;
            tbcRF.weight = 0;
        }

        if (Input.GetMouseButtonUp(2) && hipsMoving)
        {
            //centreC.weight = 0;
            hipsMoving = false;
        }
    }
}
