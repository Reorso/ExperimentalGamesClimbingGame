using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class EnterClimbingMode : MonoBehaviour
{
    public Camera cam;
   
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<ClimbController3>() != null)
            {
                other.GetComponent<ClimbController3>().cam = cam;
            }
            else
            {
                other.GetComponent<ClimbController>().cam = cam;

            }
            other.GetComponent<ThirdPersonController>().LockCameraPosition = true;
            cam.enabled = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam.enabled = false;
            other.GetComponent<ThirdPersonController>().LockCameraPosition = false;
        }
    }
}
