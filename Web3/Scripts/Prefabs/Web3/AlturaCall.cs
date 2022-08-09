using System;
using System.Collections;
using UnityEngine;

public class AlturaCall : MonoBehaviour
{
    async void Start()
    {
        string chain = "ethereum";
        string network = "mainnet";
        string user = "0x0";
        string rpc = "https://api.alturanft.com/api/v2/";
        string contract = "0x0";
        string abi = "0x0";
        string method = "0x0";
        string args = "0x0";
        string response = await AlturaWeb3.AlturaCall(chain, network, user, rpc, contract, abi, method);
        print(response);
    
    }
}