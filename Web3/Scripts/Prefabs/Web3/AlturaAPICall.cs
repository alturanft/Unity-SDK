using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlturaAPICall : MonoBehaviour
{
    async void Start()
    {
        string chain = "ethereum";
        string network = "mainnet";
        string user = "0x0";
        string rpc = "https://api.alturanft.com/api/v2/";

        string collection = await Web3.Itemz();
        print(collection);
    }
}