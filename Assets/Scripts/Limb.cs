using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Limb : MonoBehaviour
{
    //float lenght;
    public GameObject target;
    public ChainIKConstraint chik;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.name);
                Debug.Log("hit");
                target.transform.position = hit.point;
                //chik.target.position = hit.point;
            }

        }
    }
}
