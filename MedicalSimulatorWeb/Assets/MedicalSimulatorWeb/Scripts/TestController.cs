using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    private bool _isUpDirection = true;
    private float _speed = 2.0f;

    void Start()
    {
        transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
    }

    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (_isUpDirection ? 1 : -1) * _speed * Time.deltaTime, transform.localPosition.z);
        if (transform.localPosition.y > 2f) _isUpDirection = false;
        if (transform.localPosition.y < 0f) _isUpDirection = true;
    }
}
