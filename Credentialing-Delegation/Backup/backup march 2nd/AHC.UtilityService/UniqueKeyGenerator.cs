﻿using System.Security.Cryptography;
using System.Text;

namespace AHC.UtilityService
{
    /// <summary>
    /// Author: Venkat
    /// Date: 6/11/2014
    /// Generates Unique Key using Random Number Generator Crypto Service Provider
    /// </summary>
    public static class UniqueKeyGenerator
    {
        public static string GetUniqueKey()
        {
            int maxSize = 8;
            
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }
    }
}