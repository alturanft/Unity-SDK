using System;
using Newtonsoft.Json;
using UnityEngine;
using AlturaSDK;

public class Testing : MonoBehaviour 
{
    async void Start()
    {
        string result = await AlturaWeb3.GetUsers();
        Debug.Log(result);
        string items = await AlturaWeb3.GetItems();
        Debug.Log(items);
        string collections = await AlturaWeb3.GetCollections();
        Debug.Log(collections);
        string activity = await AlturaWeb3.GetItemActivity();
        Debug.Log(activity);
        string history = await AlturaWeb3.GetItemHolders("0xb260b4b5e3357b3942ba71cfa0a7bdd32763f8ae", "1");
        var auth = await AlturaWeb3.AuthenticateUser("0xfb7C2D5c65e00d05C48AfF5c02C6D4682156DF33", "J9FWf5");
        Debug.Log(auth);
        var rez = await AlturaWeb3.TransferItem("SWA2M3G-W5V48RN-KAAWVWS-4FPFSTQ", "0xb260b4b5e3357b3942ba71cfa0a7bdd32763f8ae", "1", 12, "0xfb7C2D5c65e00d05C48AfF5c02C6D4682156DF33");
        Debug.Log(rez);
        
        //   var addy = await AlturaWeb3.CreateNewEthereumAddressAsync();
        //   Debug.Log(addy);
    }
}