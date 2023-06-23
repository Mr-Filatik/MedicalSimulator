using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JawRootControll : MonoBehaviour
{
    public GameObject JaWRoot;
    // Start is called before the first frame update
    void Start()
    {
       Debug.Log(JaWRoot.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   
}
