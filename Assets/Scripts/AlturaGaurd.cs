using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;    
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AlturaWeb3.SDK {
    /// <summary>
    /// Altura SDK for Unity
    /// </summary>

    public class AlturaClient 
    {
        public class Response<T> { public T response; }

        public readonly static string BASE_URL = "https://api.alturanft.com/api/v2/";

        /// <summary>
        /// Calls the user/verify_auth_code endpoint. with address and code
        /// returns true or false, no query params
        /// </summary>
        public static async Task<string> VerifyAuthCode(string address, string code)
        {
            UnityWebRequest req = UnityWebRequest.Get(BASE_URL + "user/verify_auth_code" + "/" + address + "/" + code);
            await req.SendWebRequest();
            return req.downloadHandler.text;
        }

        /// <summary>
        /// Calls the "user/:address" endpoint. queryParams
        /// returns the  user object
        /// </summary>
        public static async Task<string> GetUser(string address)
        {
            UnityWebRequest request = UnityWebRequest.Get(BASE_URL + "user" + "/" + address);
  
            await request.SendWebRequest();
            return request.downloadHandler.text;
        }

        public static async Task<string> GetUsers(string queryParams)
        {
            UnityWebRequest request = UnityWebRequest.Get(BASE_URL + "user" + queryParams);
  
            await request.SendWebRequest();
            return request.downloadHandler.text;
            
        }


        /// <summary>
        /// Calls the "item" endpoint. queryParams determing how many perPage, page, sortBy, sortOrder, and slim
        /// returns the  many item objects
        /// </summary>
        public static async Task<string> GetItems(string queryParams)
        {
            UnityWebRequest request = UnityWebRequest.Get(BASE_URL + "item" + queryParams);
            await request.SendWebRequest();
            return request.downloadHandler.text;
        }
        

        /// <summary>
        /// Calls the "item/:address/:tokenId" endpoint. queryParams
        /// returns the  a single item object takes in address and tokenId
        /// </summary>
        public static async Task<string> GetItem(string address, string tokenId)
        {
            UnityWebRequest request = UnityWebRequest.Get(BASE_URL + "item" + "/" + address + "/" + tokenId);
            await request.SendWebRequest();
            return request.downloadHandler.text;
        }
   
        /// <summary>
        ///Calls the "item/holders/:address/:tokenId" endpoint. queryParams
        /// returns the  a single item object takes in address and tokenId
        /// </summary>
    
    
        public static async Task<string> GetItemHolders(string address, string tokenId)
        {
            UnityWebRequest request = UnityWebRequest.Get(BASE_URL + "item/holders" + "/" + address + "/" + tokenId);
            await request.SendWebRequest();
            return request.downloadHandler.text;
        }
    

        /// <summary>
        /// Calls the "item/events/:address/:tokenId" endpoint. queryParams
        /// returns the  a single item object takes in address and tokenId
        /// </summary>
        public static async Task<string> GetItemEvents(string address, string tokenId)
        {
            UnityWebRequest request = UnityWebRequest.Get(BASE_URL + "item/events" + "/" + address + "/" + tokenId);
            await request.SendWebRequest();
            return request.downloadHandler.text;

        }

        
        /// <summary>
        /// Calls the "item/activity" endpoint. queryParams
        /// returns the  a single item object takes in address and tokenId
        /// </summary>
    
        public static async Task<string> GetItemActivity(string queryParams)
        {
            UnityWebRequest request = UnityWebRequest.Get(BASE_URL + "item/activity" + queryParams);
            await request.SendWebRequest();
            return request.downloadHandler.text;
        }


        /// <summary>
        /// Calls the "collection/:address" endpoint. queryParams
        /// returns the  a single collection object takes in address
        /// </summary>
        public static async Task<string> GetCollection(string address)
        {
            UnityWebRequest request = UnityWebRequest.Get(BASE_URL + "collection" + "/" + address);
            await request.SendWebRequest();
            return request.downloadHandler.text;
        }

        /// <summary>
        /// Calls the "collection/" endpoint. queryParams
        /// returns the  all collections based on queryParams perPage, page, sortBy, sortOrder, and slim
        /// </summary>
        public static async Task<string> GetCollections(string queryParams)
        {
            UnityWebRequest request = UnityWebRequest.Get(BASE_URL + "collection" + queryParams);
            await request.SendWebRequest();
            return request.downloadHandler.text;
        }


        /// <summary>
        /// Calls the "/api/v2/item/transfer" endpoint. queryParams
        /// user must be authenticated to use this endpoint
        /// takes in collectionAddress, tokenId, toAddress, and returns txnHash string
        /// POST request
        /// </summary>
        
        

        public static async Task<string> TransferItem(string apikey, string address, string to, string amount, string tokenId)
        {
            WWWForm form = new WWWForm();
            form.AddField("address", address);
            form.AddField("to", to);
            form.AddField("tokenId", tokenId);
            form.AddField("amount", amount);
            UnityWebRequest request = UnityWebRequest.Post(BASE_URL + "item/transfer" + "?apiKey=" + apikey, form);

            await request.SendWebRequest();
            return request.downloadHandler.text;
        }
        
        
 
        /// <summary>
        /// Calls the "/api/v2/item/transfer" endpoint. queryParams
        /// user must be authenticated to use this endpoint
        /// takes in collectionAddress, tokenId, toAddress, and returns txnHash string
        /// send bulk transfer POST request
        /// </summary>


        public static async Task<string> TransferItems(string apikey, string address, string to, string amounts, string tokenIds)
        {
            WWWForm form = new WWWForm();
            form.AddField("address", address);
            form.AddField("to", to);
            form.AddField("tokenId", tokenIds);
            form.AddField("amount", amounts);
            UnityWebRequest request = UnityWebRequest.Post(BASE_URL + "item/transfer" + "?apiKey=" + apikey, form);

            await request.SendWebRequest();
            return request.downloadHandler.text;
        }
        


        /// <summary>
        /// Calls the '/api/v2/item/mint' constrainst must be authenticated to use this endpoint
        /// takes in address tokenId, amount and to, returns txnHash string
        /// POST request
        /// </summary>
        
        
        public static async Task<string> MintItem(string apikey, string address, string tokenId, string to, string amount)
        {
            WWWForm form = new WWWForm();
            form.AddField("address", address);
            form.AddField("tokenId", tokenId);
            form.AddField("amount", amount);
            form.AddField("to", to);
            UnityWebRequest request = UnityWebRequest.Post(BASE_URL + "item/mint" + "?apiKey=" + apikey, form);

            await request.SendWebRequest();
            return request.downloadHandler.text;        
            }

    }

}

 /// <summary>
    /// Class to enable asynchronous web requests via Unity 
    /// </summary>
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
