using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
//using Nethereum;
//using Nethereum.Signer;
//using Nethereum.Web3;

namespace AlturaSDK {

    public class AlturaWeb3 {


        public class Response<T> { public T response; } //generic response

        public readonly static string host = "https://api.alturanft.com/api/v2/"; //host for apis
        
    //     public static Task<string> CreateNewEthereumAddressAsync()
    //     {
    //        var key = Nethereum.Signer.EthECKey.GenerateKey();
    //        var pKey = key.GetPrivateKeyAsBytes().ToString();
    //        var acc = new Nethereum.Web3.Accounts.Account(pKey);
    //        return await web3.Personal.NewAccount.SendRequestAsync("");
    //        return Task.FromResult(acc.Address);
            
           
    //    }
    //////////////
    //// USER ////
    //////////////

    /// <summary>
    /// Altura Web3 API for getting a Users.
    /// </summary>
        public static async Task<string> GetUser(string address) {
            string url = host + "user" + "/" + address;
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            await webRequest.SendWebRequest();
            return System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

        }
    /// <summary>
    /// Get a Users Items.
    /// </summary>
        public static async Task<string> GetUserItem(string address) {
            string url = host + "user/items" + "/" + address;
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            await webRequest.SendWebRequest();
            return System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

        }

    /// <summary>
    /// Verify a Users Auth Code
    /// </summary>
        public static async Task<string> AuthenticateUser(string address, string code) 
        {
            string url = host + "user/verify_auth_code" + "/" + address + "/"  + code;
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            await webRequest.SendWebRequest();
            return System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

        }

    /// <summary>
    /// Altura Web3 API for getting all Users.
    /// </summary>
        public static async Task<string> GetUsers() {
            string url = host + "user/";
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            await webRequest.SendWebRequest();
            return System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

        }


    //////////////
    //// Items ////
    //////////////


    /// <summary>
    /// Altura Web3 API for Items.
    /// </summary>
        public static async Task<string> GetItems() {
            string url = host + "item/";
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            await webRequest.SendWebRequest();
            return System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

        }
        public static async Task<string> GetItem(string address, string tokenId) {
            string url = host + "item" + "/" + address + "/"  + tokenId;
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            await webRequest.SendWebRequest();
            return System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

        }

        public static async Task<string> GetItemHolders(string address, string tokenId) {
            string url = host + "item/holders" + "/" + address + "/"  + tokenId;
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            await webRequest.SendWebRequest();
            return System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

        }

        public static async Task<string> GetItemHistory(string address, string tokenId) {
            string url = host + "item/events" + "/" + address + "/"  + tokenId;
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            await webRequest.SendWebRequest();
            return System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

        }

        public static async Task<string> GetItemActivity() {
            string url = host + "item/activity";
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            await webRequest.SendWebRequest();
            return System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

        }

    //////////////////////
    //// Collection /////
    ////////////////////
        public static async Task<string> GetCollection(string addrerss) {
            string url = host + "collection/" + addrerss;
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            await webRequest.SendWebRequest();
            return System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

        }


        public static async Task<string> GetCollections() {
            string url = host + "collection/";
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            await webRequest.SendWebRequest();
            return System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

        }


    //////////////////////
    //// POST /////
    ////////////////////
        public static async Task<string> TransferItem(string apiKey, string address, string tokenId, int amount, string to) {
            WWWForm form = new WWWForm();
            form.AddField("address", address);
            form.AddField("tokenId", tokenId);
            form.AddField("amount", amount);
            form.AddField("to", to);

            string url = host + "item/transfer" + "?" + "apiKey=" + apiKey + "&" + "address=" + address + "&" + "tokenId=" + tokenId + "&" + "amount=" + amount + "&" + "to=" + to;
            UnityWebRequest webRequest = UnityWebRequest.Post(url, form);

            await webRequest.SendWebRequest();
            return System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

        }

        public static async Task<string> TransferItems(string apiKey, string collectionAddress, string tokenIds, int amounts, string to) {
            WWWForm form = new WWWForm();
            form.AddField("collectionAddress", collectionAddress);
            form.AddField("tokenIds", tokenIds);
            form.AddField("amounts", amounts);
            form.AddField("to", to);

            string url = host + "item/transfer" + "?" + "apiKey=" + apiKey + "&" + "collectionAddress=" + form.ToString();
            UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
            await webRequest.SendWebRequest();
            return System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

        }

        public static async Task<string> MintAdditionalNFT(string apiKey, string address, string tokenId, int amount, string to) {
            WWWForm form = new WWWForm();
            form.AddField("address", address);
            form.AddField("tokenId", tokenId);
            form.AddField("amount", amount);
            form.AddField("to", to);

            string url = host + "item/mint" + "?" + "apiKey=" + apiKey + "&" + form.ToString();
            UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("X-API-KEY", apiKey);
            await webRequest.SendWebRequest();
            return System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

        }

}

 public class UnityWebRequestAwaiter : INotifyCompletion {
        private UnityWebRequestAsyncOperation asyncOp;
        private Action continuation;

        public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOp) {
            this.asyncOp = asyncOp;
            asyncOp.completed += OnRequestCompleted;
        }

        public bool IsCompleted { get { return asyncOp.isDone; } }

        public void GetResult() { }

        public void OnCompleted(Action continuation) {
            this.continuation = continuation;
        }

        private void OnRequestCompleted(AsyncOperation obj) {
            continuation();
        }
    }

    public static class ExtensionMethods {
        public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp) {
            return new UnityWebRequestAwaiter(asyncOp);
        }
    }
}
