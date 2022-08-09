using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
//using Models;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class Web3 
{
    public class Response<T> { public T response; }
    private readonly static string host = "https://api.alturanft.com/api/v2/";


    public static async Task<string> AlturaCall(
        string _chain, string _network, string _contract, 
        string _abi, string _method, string _args, string _rpc = ""
    ) {
        WWWForm form = new WWWForm();
        form.AddField("chain", _chain);
        form.AddField("network", _network);
        form.AddField("contract", _contract);
        form.AddField("abi", _abi);
        form.AddField("method", _method);
        form.AddField("args", _args);
        form.AddField("rpc", _rpc);
        
        string uri = host + "user";
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
            req.SendWebRequest();
            Response<string> data =JsonUtility.FromJson<Response<string>>(System.Text.Encoding.UTF8.GetString(req.downloadHandler.data));
            return data.response;
        }
    }

   public static async Task<string> Itemz()
        {
            UnityWebRequest www = UnityWebRequest.Get(host + "item/" );
            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
                return www.downloadHandler.text;
            }
            return www.downloadHandler.text;
        }

    public static async Task<string> Status(string _chain, string _network, string _txn) 
    {
        WWWForm form = new WWWForm();
        form.AddField("chain", _chain);
        form.AddField("network", _network);
        form.AddField("txn", _txn);
        
        string uri = host + "status";
        using (UnityWebRequest req = UnityWebRequest.Post(uri, form))
        {
            req.SendWebRequest();
            Response<string> data =JsonUtility.FromJson<Response<string>>(System.Text.Encoding.UTF8.GetString(req.downloadHandler.data));
            return data.response;
        }
    }
    
    public static async Task<string> GetHolders(int perPage, int page, bool includeListed)
    {
        var url = host + "holders?perPage=" + perPage + "&page=" + page + "&includeListed=" + includeListed;
        WWWForm form = new WWWForm();
        form.AddField("perPage", perPage);
        form.AddField("page", page);
        string uri = host + "item";
        UnityWebRequest www = UnityWebRequest.Get(uri);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) 
        {
            //await
            webRequest.SendWebRequest();
            Response<string> data = JsonUtility.FromJson<Response<string>>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
            return data.response;
        }
    }
   //public static async Task<string>

   public static async Task<string> GetHistory(int perPage, int page)
    {
        var uri = host + "item/events/${address}/${tokenId}";
        WWWForm form = new WWWForm();
        form.AddField("perPage", perPage);
        form.AddField("page", page);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) 
        {
            //await 
            webRequest.SendWebRequest();
            Response<string> data = JsonUtility.FromJson<Response<string>>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
            return data.response;
        }

    }
}