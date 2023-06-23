using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseOximeterTrigger : MonoBehaviour
{
    public Vector3 vector;
    public Quaternion rotate;
    public GameObject pulse;
    private bool Fix = false;
   private void OnTriggerEnter(Collider collision)
    {
        if(collision.name == "pulseOximeter")
        {
            //Debug.Log("123");
            Fix = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.name == "pulseOximeter")
        {
            //Debug.Log("123");
            Fix = false;
        }
    }
    void Update()
    {
        if (Fix)
        {
            pulse.transform.position = vector;
            pulse.transform.rotation = rotate;
        }
        
    }
}
