using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    async void private void Start() {
        {
            string chain = "ethereum";
            string network = "mainnet";
            string txn = "0x0";

            string txnStatus = await Web3.Status(chain, network, txn);
            print(txnStatus);
        }
    }
}