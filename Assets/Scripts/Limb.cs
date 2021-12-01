using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Limb : MonoBehaviour
{
    //public GameObject rig;
    public Transform target, shoulder;
    public TwoBoneIKConstraint cs;

    private void Start()
    {
        target = cs.data.target;
    }

    Vector3 GetLenght()
    {
        return transform.position - shoulder.position;
    }

    Vector3 GetPosition()
    {
        return transform.position;
    }

}
