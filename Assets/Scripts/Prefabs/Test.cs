using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Numerics;
using System.Runtime.CompilerServices;
public class Test : MonoBehaviour
{
    async void Start() {
        string url = "https://api.alturanft.com/api/v2/";
                using (UnityWebRequest req = UnityWebRequest.Get(url + "user/verify_auth_code/0xfb7C2D5c65e00d05C48AfF5c02C6D4682156DF33/7N4F067-HK0MFS3-PA7XGKA-WN2C1J6")) {
            req.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            req.SetRequestHeader("Content-Type", "application/json");
            await req.SendWebRequest();
            Debug.Log(req.downloadHandler.text);
        }
        using (UnityWebRequest req = UnityWebRequest.Get(url + "user")) {
            req.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
            req.SetRequestHeader("Content-Type", "application/json");
            await req.SendWebRequest();
            Debug.Log(req.downloadHandler.text);
        }
             using (UnityWebRequest req = UnityWebRequest.Get(url + "item")) {
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
