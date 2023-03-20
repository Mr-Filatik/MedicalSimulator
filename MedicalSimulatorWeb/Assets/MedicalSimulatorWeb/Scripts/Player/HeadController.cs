using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HeadController : MonoBehaviour
{
    private float _topBorder = 15f;
    private float _bottomBorder = 60f;

    void Start()
    {
        //transform.localEulerAngles = Vector3.zero;
    }

    public void HeadTilt(float value)
    {
        transform.Rotate(Vector3.right * value * Settings.MouseSensivity * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(HeadTiltLimiter(transform.localEulerAngles.x), 0f, 0f);
    }

    public float HeadTiltLimiter(float value)
    {
        if (value < 180f && value > _bottomBorder)
        {
            return _bottomBorder;
        }
        if (value > 180f && value < 360f - _topBorder)
        {
            return 360f - _topBorder;
        }
        return value;
    }
}
