namespace AlturaNFT.Samples.PlayerConnect
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using AlturaNFT;

    public class PlayerConnect_Sample : MonoBehaviour
    {
      //  public AlturaNFT.Collection connectPlayerWallet;
        public Text addressText;
        public Text NetworkIDText;

        void Start()
        {
         //   AlturaNFT.ConnectPlayerWallet
           //     .OnComplete((address, networkID )=> UpdateUI(address,networkID));
        }

        void UpdateUI(string address, string networkID)
        {
        addressText.text = address;
            
            //This value can also be accessed from anywhere globally via Port.
        //    addressText.text = User.ConnectedPlayerAddress;
          //  NetworkIDText.text = User.ConnectedPlayerNetworkID;
        }
        
    }
}