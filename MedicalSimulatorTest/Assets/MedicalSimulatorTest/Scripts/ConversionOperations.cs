using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ConversionOperations : MonoBehaviour
{
    public static DateTime GetDateTimeFromTimestamp(string timestamp)
    {
        //2023-05-08T07:50:50.086974600Z
        string[] array = timestamp.Split('-', 'T', ':', '.', 'Z');
        int[] intArray = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            try
            {
                intArray[i] = int.Parse(array[i]);
            }
            catch
            {
                intArray[i] = 1;
            }
        }
        DateTime output = new DateTime(intArray[0], intArray[1], intArray[2], intArray[3], intArray[4], intArray[5]);
        return output;
    }

    public static string GetTimestampFromDateTime(DateTime dateTime)
    {
        return $"{dateTime.Year.ToString().PadLeft(4, '0')}-{dateTime.Month.ToString().PadLeft(2, '0')}-{dateTime.Day.ToString().PadLeft(2, '0')}T{dateTime.Hour.ToString().PadLeft(2, '0')}:{dateTime.Minute.ToString().PadLeft(2, '0')}:{dateTime.Second.ToString().PadLeft(2, '0')}.000000000Z";
    }
}
