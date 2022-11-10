
using System;
using System.Runtime.InteropServices;

namespace AlturaNFT
{
    using UnityEngine;
    using UnityEngine.Events;
    using Internal;

    [AddComponentMenu(AlturaConstants.BaseComponentMenu + AlturaConstants.FeatureName_ConnectUserWallet)]
    [HelpURL(AlturaConstants.Docs_ConnectUserWallet)]
    public class ConnectPlayerWallet : MonoBehaviour
    {
        public string MockconnectedWalletAddress = "0xfb7C2D5c65e00d05C48AfF5c02C6D4682156DF33";
        public string MockconnectedNetworkID = "1";
        [Space(30)]
        public string connectedWalletAddress;
        public string connectedNetworkID;

        public UnityEvent afterSuccess;
        private UnityAction<string,string> OnCompleteAction;

        [DllImport("__Internal")]
        private static extern void SendCallTo_GetAddress();

        
        /// <summary>
        /// Initialize creates a gameobject and assings this script as a component. This must be called if it doesn't already exists in the scene on gameObject named PlayerConnect_AlturaNFT.
        /// </summary>
        public static ConnectPlayerWallet Initialize()
        {
            var _this = new GameObject("PlayerConnect_AlturaNFT").AddComponent<ConnectPlayerWallet>();
            return _this;
        }
        
        /// <summary>
        /// Action when player successfully connects the wallet returning connected address and connected networkID
        /// </summary>
        /// <returns> Player Wallet Address String </returns>
        public ConnectPlayerWallet OnComplete(UnityAction<string,string> action)
        {
            this.OnCompleteAction = action;
            return this;
        }
        
        
        /// <summary>
        /// Use this function on a Button from inside Unity to Connect Account, If a mock wallet is entered It'll be connected only on editor level
        /// </summary>
        public void WebSend_GetAddress()
        {
#if UNITY_EDITOR
            connectedWalletAddress = MockconnectedWalletAddress;
            connectedNetworkID = MockconnectedNetworkID;
            
            User.ConnectedPlayerAddress = connectedWalletAddress;
            User.ConnectedPlayerNetworkID = connectedNetworkID;
            Debug.Log("Editor Mock Wallet Connected , Address: " + User.ConnectedPlayerAddress + " at Network ID:" + User.ConnectedPlayerNetworkID +" | Access it via User.ConnectedPlayerAddress");
            GetAddressSuccess();
#endif
#if !UNITY_EDITOR
            SendCallTo_GetAddress();
#endif
        }
        
        void Awake()
        {
            
#if UNITY_EDITOR
            User.ConnectedPlayerAddress = connectedWalletAddress;
            User.ConnectedPlayerNetworkID = connectedNetworkID;
#endif
          
            this.gameObject.name = "PlayerConnect_AlturaNFT";
        }

        /// <summary>
        /// Use this to hook up other wallet connect features to AlturaNFT wallet connect to access User.connectedplayeraddress
        /// </summary>
        /// <param name="walletaddress"></param>
        public void ConnectThisToAlturaNFTWalletConnect(string connectedWalletAddress)
        {
            WebHook_GetAddress(connectedWalletAddress);
        }

        //called from index - For WebGL
        public void WebHook_GetNetworkID(string networkID)
        {
            User.ConnectedPlayerNetworkID = networkID;
            connectedNetworkID = networkID;
            
        }
        
        //called from index - For WebGL
        public void WebHook_GetAddress(string recievedaddress)
        {
            connectedWalletAddress = recievedaddress;
            User.ConnectedPlayerAddress = connectedWalletAddress;
            GetAddressSuccess();
        }

        void GetAddressSuccess()
        {
            if(OnCompleteAction!=null)
                OnCompleteAction.Invoke(connectedWalletAddress,User.ConnectedPlayerNetworkID );
                        
            if(afterSuccess!=null)
                afterSuccess.Invoke();
        }
      
    }
}