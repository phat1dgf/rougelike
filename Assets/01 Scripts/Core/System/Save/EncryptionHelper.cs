using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class EncryptionHelper
{
    private static readonly string key = "1234567890123456"; 
    private static readonly string iv = "6543210987654321";

    public static string Encrypt(string plainText)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = Encoding.UTF8.GetBytes(iv);

        using MemoryStream ms = new MemoryStream();
        using CryptoStream cs = new CryptoStream(
            ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
        using StreamWriter sw = new StreamWriter(cs);

        sw.Write(plainText);
        sw.Close();

        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Decrypt(string cipherText)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = Encoding.UTF8.GetBytes(iv);

        byte[] buffer = Convert.FromBase64String(cipherText);

        using MemoryStream ms = new MemoryStream(buffer);
        using CryptoStream cs = new CryptoStream(
            ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader sr = new StreamReader(cs);

        return sr.ReadToEnd();
    }
}

