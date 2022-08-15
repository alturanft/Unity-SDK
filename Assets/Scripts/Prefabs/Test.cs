using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

public class Test : MonoBehaviour
{
    async void Start() {
        string url = "https://api.alturanft.com/api/v2/";
                using (UnityWebRequest req = UnityWebRequest.Get(url + "user/verify_auth_code/0xfb7C2D5c65e00d05C48AfF5c02C6D4682156DF33/dA0ALg")) {
            req.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            req.SetRequestHeader("Content-Type", "application/json");
            await req.SendWebRequest();
            Debug.Log(req.downloadHandler.text);
        }
        using (UnityWebRequest req = UnityWebRequest.Get(url + "user?name=AlturaNFT")) {
            req.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            req.SetRequestHeader("Content-Type", "application/json");
            await req.SendWebRequest();
            Debug.Log(req.downloadHandler.text);
        }
             using (UnityWebRequest req = UnityWebRequest.Get(url + "user/items/0xcaf45074fc329692995d812aeb099070c7fdee2b?collectionAddress=0xdb0047cb1dfc44696f6e9868ef6bb40000280b05&slim=true&page=1")) {
            req.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            req.SetRequestHeader("Content-Type", "application/json");
            await req.SendWebRequest();
            Debug.Log(req.downloadHandler.text);
        }
             using (UnityWebRequest req = UnityWebRequest.Get(url + "collection")) {
            req.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            req.SetRequestHeader("Content-Type", "application/json");
            await req.SendWebRequest();
            Debug.Log(req.downloadHandler.text);
        }

        // Generate ApiKEy
        var key = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        generator.GetBytes(key);
        string apiKey = Convert.ToBase64String(key);
        Debug.Log(apiKey);
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
