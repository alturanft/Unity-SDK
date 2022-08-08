using System.Runtime.CompilerServices;
using System;
using System.Collections;
using System.Threading.Tasks;
using Nethereum.Signer;
using UnityEngine;
using Nethereum.Util;

public class AlturaWallet
{
    public static async Task<string> Sign(string _msg) 
    {
        string msg = Uri.EscapeDataString(_msg);
        Application.OpenURL(string.Format("https://api.alturanft.com/api/v2/sign?msg={0}", msg));
        GUIUtility.systemCopyBuffer = "";
        string copy = "";
        while (copy == "")
        {
            copy = GUIUtility.systemCopyBuffer;
            await Task.Delay(100);
        }

        if(copy.StartsWith("0x")) 
        {
            return copy;
        }
        else;
        {
            throw new Exception("Invalid signature");
        }
    }
}