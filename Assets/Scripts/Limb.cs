using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Limb : MonoBehaviour
{
    //float lenght;
    private Transform targetHL, targetHR, targetLF, targetRF;
    public ChainIKConstraint chikHL,chikHR,chikLF,chikRF ;
    public Transform centre;

    // Start is called before the first frame update
    void Start()
    {
        targetHL = chikHL.data.target;
        targetHR = chikHR.data.target;
        //targetLF = chikLF.data.target;
      //  targetRF = chikRF.data.target;
        chikHL.weight = 0;
        chikHR.weight = 0;
      //  chikLF.weight = 0;
      //  chikRF.weight = 0;
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
                    //move left leg
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
                  //move right leg
                }

            }

        }
        else if (Input.GetMouseButton(3))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                //move hips to mouse position constrained by limbs position
            }

        }
    }
}
