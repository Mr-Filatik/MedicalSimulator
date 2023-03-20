using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    //public static Settings instance;
    private static float _mouseSensivity = 100f;
    public static float MouseSensivity
    {
        get
        {
            return _mouseSensivity;
        }
        set
        {
            _mouseSensivity = value;
        }
    }

    private static bool _mouseVisibility = true;

    public static bool MouseVisibility 
    {
        get 
        { 
            return _mouseVisibility; 
        }
        set 
        {
            _mouseVisibility = value;
            Cursor.visible = _mouseVisibility;
            if (_mouseVisibility)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

}
