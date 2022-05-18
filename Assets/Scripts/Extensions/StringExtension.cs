using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringExtension : MonoBehaviour
{
    public static bool IsNullOrWhiteSpace(string value)
    {
        if (value != null)
        {
            for (int i = 0; i < value.Length; i += 1)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
