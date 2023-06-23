using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphyFixPos : MonoBehaviour
{
    public Vector3 vectorRuk;
    public Quaternion rotateRuk;
    public Vector3 vectorSphy;
    public Quaternion rotateSphy;
    public GameObject sphy;
    public GameObject ruk;
    private bool FixSphy    = false;
    private bool FixRuk     = false;
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.name);
        if (collision.name == "Sphy")
        {
            FixSphy = true;
        }
        if (collision.name == "Ruk")
        {

            FixRuk = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.name == "Sphy")
        {

            FixSphy = false;
        }
        if (collision.name == "Ruk")
        {

            FixRuk = false;
        }
    }
    void Update()
    {
        if (FixSphy)
        {
            sphy.transform.position = vectorSphy;
            sphy.transform.rotation = rotateSphy;
        }
        if (FixRuk)
        {
            ruk.transform.position = vectorRuk;
            ruk.transform.rotation = rotateRuk;
        }

    }
}
