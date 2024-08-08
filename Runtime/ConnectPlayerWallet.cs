using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

namespace AlturaNFT
{
    public class ConnectPlayerWallet : MonoBehaviour
    {
        public string MockconnectedWalletAddress = "0xfb7C2D5c65e00d05C48AfF5c02C6D4682156DF33";
        public string MockconnectedNetworkID = "1";
        [Space(30)]
        public string connectedWalletAddress;
        public string connectedNetworkID;

        public UnityEvent afterSuccess;
        private UnityAction<string, string> OnCompleteAction;

        /// <summary>
        /// Initialize creates a gameobject and assigns this script as a component. This must be called if it doesn't already exist in the scene on a gameObject named PlayerConnect_AlturaNFT.
        /// </summary>
        public static ConnectPlayerWallet Initialize()
        {
            var _this = new GameObject("PlayerConnect_AlturaNFT").AddComponent<ConnectPlayerWallet>();
            return _this;
        }

        /// <summary>
        /// Action when player successfully connects the wallet, returning connected address and connected networkID.
        /// </summary>
        /// <returns> Player Wallet Address String </returns>
        public ConnectPlayerWallet OnComplete(UnityAction<string, string> action)
        {
            this.OnCompleteAction = action;
            return this;
        }

        /// <summary>
        /// Use this function on a Button from inside Unity to Connect Account. If a mock wallet is entered, it'll be connected only on editor level.
        /// </summary>
        public void WebSend_GetAddress()
        {
#if UNITY_EDITOR
            connectedWalletAddress = MockconnectedWalletAddress;
            connectedNetworkID = MockconnectedNetworkID;

            User.ConnectedPlayerAddress = connectedWalletAddress;
            User.ConnectedPlayerNetworkID = connectedNetworkID;
            Debug.Log("Editor Mock Wallet Connected, Address: " + User.ConnectedPlayerAddress + " at Network ID:" + User.ConnectedPlayerNetworkID + " | Access it via User.ConnectedPlayerAddress");
            GetAddressSuccess();
#else
            connectedWalletAddress = SendCallTo_GetAddress();
            User.ConnectedPlayerAddress = connectedWalletAddress;
            Debug.Log("Wallet Connected, Address: " + User.ConnectedPlayerAddress);
            GetAddressSuccess();
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
        /// Use this to hook up other wallet connect features to AlturaNFT wallet connect to access User.connectedplayeraddress.
        /// </summary>
        /// <param name="walletaddress"></param>
        public void ConnectThisToAlturaNFTWalletConnect(string connectedWalletAddress)
        {
            WebHook_GetAddress(connectedWalletAddress);
        }

        // Called from index - For WebGL
        public void WebHook_GetNetworkID(string networkID)
        {
            User.ConnectedPlayerNetworkID = networkID;
            connectedNetworkID = networkID;
        }

        // Called from index - For WebGL
        public void WebHook_GetAddress(string receivedAddress)
        {
            connectedWalletAddress = receivedAddress;
            User.ConnectedPlayerAddress = connectedWalletAddress;
            GetAddressSuccess();
        }

        void GetAddressSuccess()
        {
            OnCompleteAction?.Invoke(connectedWalletAddress, User.ConnectedPlayerNetworkID);
            afterSuccess?.Invoke();
        }

#if UNITY_WEBGL
        [DllImport("__Internal")]
        public static extern string SendCallTo_GetAddress();
#else
        [DllImport("AlturaNFTConnectUser.jslib")]
        public static extern string SendCallTo_GetAddress();
#endif
    }
}
