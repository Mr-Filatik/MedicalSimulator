using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSettings : MonoBehaviour
{
    private static readonly string _localAddress = "http://localhost:5212";
    private static readonly string _remoteAddress = "http://filatik.somee.com"; // X#Wig4JzT9CBCPQ

    public static string Address
    {
        get
        {
            //if (Application.isEditor)
            //{
            //    return _localAddress;
            //}
            return _remoteAddress;
        }
    }

    public static int MaxRequestTime 
    { 
        get 
        { 
            return 10;
        } 
    }
}
