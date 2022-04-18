using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Utils
{
    internal static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

    public static string GenerateKey(int size)
    {
        byte[] data = new byte[4 * size];
        using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
        {
            crypto.GetBytes(data);
        }
        StringBuilder result = new StringBuilder(size);
        for (int i = 0; i < size; i++)
        {
            var rnd = BitConverter.ToUInt32(data, i * 4);
            var idx = rnd % chars.Length;

            result.Append(chars[idx]);
        }

        return result.ToString();
    }

    public static int[] ConvertToIntArray(byte[] inputElements)
    {
        int[] myFinalIntegerArray = new int[inputElements.Length / 4];
        for (int cnt = 0; cnt < inputElements.Length; cnt += 4)
        {
            myFinalIntegerArray[cnt / 4] = BitConverter.ToInt32(inputElements, cnt);
        }
        return myFinalIntegerArray;
    }


}
