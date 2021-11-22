using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class EnterClimbingMode : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       cam.enabled = true;
       other.GetComponent<ThirdPersonController>().LockCameraPosition = true;
       other.GetComponent<Limb>().cam = cam;
    }

    private void OnTriggerExit(Collider other)
    {
        cam.enabled = false;
        other.GetComponent<ThirdPersonController>().LockCameraPosition = false;
    }
}
