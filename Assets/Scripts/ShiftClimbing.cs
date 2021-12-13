using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ShiftClimbing : MonoBehaviour
{
    public KeyCode c;
    public float speed = 10;
    bool climbing = false;
    Collider o;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (climbing)
        {
            if (Input.GetKey(c))
            {

                o.GetComponent<CharacterController>().Move(Vector3.up * speed * Time.deltaTime);
                o.GetComponent<ThirdPersonController>().Gravity = 0;
            }
            else if (Input.GetKeyUp(c))
            {
                o.GetComponent<ThirdPersonController>().Gravity = -15;
            }
        }
        else
        {
            if(o != null)
            {
                o.GetComponent<ThirdPersonController>().Gravity = -15;
            }
            
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            climbing = true;
            o = other;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            climbing = false;
        }

    }
}
