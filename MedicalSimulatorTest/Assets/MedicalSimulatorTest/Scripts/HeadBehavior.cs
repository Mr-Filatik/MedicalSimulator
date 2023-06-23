using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeadBehavior : MonoBehaviour
{
    [SerializeField]
    [Header("Подбородок")]
    private GameObject _chin = null;
    private bool _chin_State = false;
    [SerializeField]
    private Vector3 _chin_StartPosition     = new Vector3(-0.01667475F, 0.01999504F, -0.0005313316F);
    [SerializeField]
    private Vector3 _chin_StartRotation     = new Vector3(-0.001F, 0.008F, -89.998F);
    [SerializeField]
    private Vector3 _chin_EndPosition       = new Vector3(-0.0057F, 0.02F, -0.0005F);
    [SerializeField]
    private Vector3 _chin_EndRotation       = new Vector3(-0.001F, 0.008F, -102.56F);

    [SerializeField]
    [Header("Грудная клетка")]
    private GameObject[] _chest = null;

    void Update()
    {
        if (_chin.transform.position.z > 2.55794F)
        {
            _chin.transform.position = SetZ(_chin.transform.position, 2.55794F);
        }
        if (_chin.transform.position.z < 2.52F)
        {
            _chin.transform.position = SetZ(_chin.transform.position, 2.52F);
        }
    }
    public void Breath()
    {
        //_chin.transform.DOLocalMove(_chin_EndPosition, 1F);
        //_chin.transform.DOLocalRotate(_chin_EndRotation, 1F);
       
        //Debug.Log("123EZ");
        Debug.Log(_chin.transform.position.z);
    }
    Vector3 SetZ(Vector3 vector, float z)
    {
        vector.z = z;
        return vector;
    }
}



    