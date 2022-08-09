using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;

namespace AlturaSDK {

    public class AlturaGaurd {

        public class Response<T> { public T response; } //generic response

        public readonly static string apiBasePath = "https://api.alturanft.com/api/v2/"; //apiBasePath for apis

        public readonly static string apiKey = "WXJDYP7-08MMXNC-H8ZXH9H-QH9BRQT";

        /**
         * Takes a user's address and Altura Guard code and returns true if the code is valid and false otherwise
         * @param address The user's wallet address
         * @param code The user's inputted Altura Guard code
         */
        public static async Task<bool> AuthenticateUser(string address, string code) 
        {
            WWWForm form = new WWWForm();
            form.AddField("address", address);
            form.AddField("code", code);
            string url = apiBasePath + "user/verify_auth_code/" + address + "/" + code;
            UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
            await webRequest.SendWebRequest();
            
            Dictionary<string, string> jsonMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
            if (jsonMap != null) {
                string alturaKey = "WXJDYP7-08MMXNC-H8ZXH9H-QH9BRQT";
                jsonMap.TryGetValue("token", out alturaKey);
                PlayerPrefs.SetString(apiKey, alturaKey);
                return true;
            } else {
                return false;
            }

        }

        /**
         * Takes a user's connected wallet and Altura Guard code and returns true if the code is valid and false otherwise
         * @param code The user's inputed Altura Guard code
         */
        public static async Task<bool> AuthenticateWallet(string code)
        {
            WWWForm form = new WWWForm();
            form.AddField("code", code);
            string address = "";
            string url = apiBasePath + "user/verify_auth_code/" + address + "/" + code;
            UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
            await webRequest.SendWebRequest();
            Dictionary<string, string> jsonMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
            if (jsonMap != null) {
                string alturaKey = "WXJDYP7-08MMXNC-H8ZXH9H-QH9BRQT";
                jsonMap.TryGetValue("token", out alturaKey);
                PlayerPrefs.SetString(apiKey, alturaKey);
                return true;
            } else {
                return false;
            }
        }

  /**
   * Takes a user's wallet address and returns the instance of User class
   * @param address The user's wallet address; if null will fallback to the address of the connected wallet
   */
    public static async Task<Web3.Models.AlturaUser> GetUser(string address = null) 
    {
        if (address == null) {
            address = PlayerPrefs.GetString(apiKey);
        }
        string url = apiBasePath + "user/" + address;
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        await webRequest.SendWebRequest();
        Dictionary<string, string> jsonMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(
            System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
        if (jsonMap != null) {
            Web3.Models.AlturaUser user = new Web3.Models.AlturaUser();
            jsonMap.TryGetValue("address", out user.address);
            jsonMap.TryGetValue("name", out user.name);
            jsonMap.TryGetValue("bio", out user.bio);
            jsonMap.TryGetValue("profilePic", out user.profilePic);
            jsonMap.TryGetValue("socialLink", out user.socialLink);
            jsonMap.TryGetValue("profilePicUrl", out user.profilePicUrl);
            return user;
        } else {
            return null;
        }
    }

  /**
   * Takes any query and returns an array of users that match that query
   * @param perPage The number of users to show in one page (default: 24)
   * @param page The offset for returned users. Calculated as (page - 1) * perpage (default: 1)
   * @param sortBy The field to sort the users by (any field in the user schema may be used) (default: "name")
   * @param sortDir Choose to sort in ascending(asc) or descending(desc) order (default: 'desc')
   * @param searchQuery Object of search fields and values to get filterd users array
   * @returns An array of users
   */
    public static async  Task<List<Dictionary<string, string>>> GetUsers(object[] args , object[] searchQuery)
    {
        string url = apiBasePath + "user";
        var query = "perPage={24}&page={1}&sortBy={name}&sortDir={desc}";
        foreach (var arg in args) {
            query += "&" + arg;
        }
        foreach (var search in searchQuery) {
            query += "&" + search;
        }
        UnityWebRequest webRequest = UnityWebRequest.Get(url + "?" + query);
        await webRequest.SendWebRequest();
        Dictionary<string, string> jsonMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(
            System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
        if (jsonMap != null) {
            // TODO return object of users and count return {users: [], count: 0}
            return null;
        } else {
           throw new Exception("No users found");
        }
       
    }

  /**
   * Takes a item's collection address and tokenId and returns the instance of Item class
   * @param collectionAddress Collection Address of item
   * @param tokenId Token ID of item in collection
   * @param options Set slim to get simplified result
   */

   public static async Task<Web3.Models.AlturaItem> GetItem(string collectionAddress, int tokenId, bool slim = false)
    {
        string url = apiBasePath + "item/" + collectionAddress + "/" + tokenId;
        if (slim) {
            url += "?slim=false";
        }
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        await webRequest.SendWebRequest();
        Dictionary<string, string> jsonMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(
            System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
        
        if (jsonMap != null) {
            Web3.Models.AlturaItem value = new Web3.Models.AlturaItem();

            jsonMap.TryGetValue("name" , out value.name);
            jsonMap.TryGetValue("description", out value.description);
       //   jsonMap.TryGetValue("properties", out value.properties);
       //   jsonMap.TryGetValue("chainId", out value.chainId);
       //   jsonMap.TryGetValue("royalty", out value.royalty);
            jsonMap.TryGetValue("creatorAddress", out value.creatorAddress);
            jsonMap.TryGetValue("mintDate", out value.mintDate);
       //   jsonMap.TryGetValue("stackable", out value.stackable);
       //   jsonMap.TryGetValue("supply" , out value.supply);
       //   jsonMap.TryGetValue("maxSupply", out value.maxSupply);
            jsonMap.TryGetValue("image", out value.image);
            jsonMap.TryGetValue("imageHash", out value.imageHash);
            jsonMap.TryGetValue("imageUrl", out value.imageUrl);
            jsonMap.TryGetValue("fileType", out value.fileType);
       //   jsonMap.TryGetValue("isVideo", out value.isVideo);
            jsonMap.TryGetValue("otherImageVisibility", out value.otherImageVisibility);
       //   jsonMap.TryGetValue("holders" , out value.holders);
       //   jsonMap.TryGetValue("listers", out value.listers);
       //   jsonMap.TryGetValue("likes", out value.likes);
       //   jsonMap.TryGetValue("views", out value.views);
       //   jsonMap.TryGetValue("isListed", out value.isListed);
            jsonMap.TryGetValue("mostRecentListing", out value.mostRecentListing);
       //   jsonMap.TryGetValue("cheapestListingPrice", out value.cheapestListingPrice);
            jsonMap.TryGetValue("cheapestListingCurrency", out value.cheapestListingCurrency);
        //  jsonMap.TryGetValue("cheapestListingPriceUSD" , out value.cheapestListingUSD);
        //  jsonMap.TryGetValue("nsfw", out value.nsfw);
        //  jsonMap.TryGetValue("isVerified", out value.isVerified);
        //  jsonMap.TryGetValue("hasUnlockableContent", out value.hasUnlockableContent);
        //  jsonMap.TryGetValue("imageIndex", out value.imageIndex);
        //  jsonMap.TryGetValue("imageCount", out value.imageCount);
        //  jsonMap.TryGetValue("totalListings", out value.totalListings);

            return value;
        } else {
            return null;
        }

    }
  /**
   * Takes any query and returns an array of items that match that query
   * @param perPage The number of items to show in one page (default: 24)
   * @param page The offset for returned items. Calculated as (page - 1) * perpage (default: 1)
   * @param sortBy The field to sort the items by (any field in the item schema may be used) (default: "name")
   * @param sortDir Choose to sort in ascending(asc) or descending(desc) order (default: 'desc')
   * @param slim Returns a more condensed version of the items. Limits the item schema to: collectionAddress, tokenId, name, description, imageUrl and properties (default: false)
   * @param searchQuery Object of search fields and values to get filterd items array
   */

    public static async Task<List<Dictionary<string, string>>> GetItems(object[] args, object[] searchQuery)
    {
        string url = apiBasePath + "item";
        var query = "perPage={24}&page={1}&sortBy={name}&sortDir={desc}";
        foreach (var arg in args) {
            query += "&" + arg;
        }
        foreach (var search in searchQuery) {
            query += "&" + search;
        }
        UnityWebRequest webRequest = UnityWebRequest.Get(url + "?" + query);
        await webRequest.SendWebRequest();
        Dictionary<string, string> jsonMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(
            System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
        if (jsonMap != null) {
            // TODO return object of items and count return {items: [], count: 0}
      //      return jsonMap["item"];
              return null;
        } else {
            throw new Exception("No items found");
        }
    }

  /**
   * Takes any query and returns an array of collections that match that query
   * @param perPage The number of collections to show in one page (default: 24)
   * @param page The offset for returned collections. Calculated as (page - 1) * perpage (default: 1)
   * @param sortBy The field to sort the collections by (any field in the collection schema may be used) (default: "name")
   * @param sortDir Choose to sort in ascending(asc) or descending(desc) order (default: 'desc')
   * @param searchQuery Object of search fields and values to get filterd collections array
   */

    public static async Task<List<Dictionary<string, string>>> GetCollections(object[] args , object[] searchQuery)
    {
        string url = apiBasePath + "collection";
        var query = "perPage={24}&page={1}&sortBy={name}&sortDir={desc}";
        foreach (var arg in args) {
            query += "&" + arg;
        }
        foreach (var search in searchQuery) {
            query += "&" + search;
        }
        UnityWebRequest webRequest = UnityWebRequest.Get(url + "?" + query);
        await webRequest.SendWebRequest();
        Dictionary<string, string> jsonMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(
            System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
        if (jsonMap != null) {
            // TODO return object of collections and count return {collections: [], count: 0}
           // return jsonMap["collection", query];
           return null;
        } else {
           throw new Exception("No collections found");
        }
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
