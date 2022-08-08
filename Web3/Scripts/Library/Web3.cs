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

        public static  async Task<string> Itemz()
        {
            UnityWebRequest www = UnityWebRequest.Get(host + "item/" );
            await www.SendWebRequest();
            
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
            await webRequest.SendWebRequest();
            Response<string> data = JsonUtility.FromJson<Response<string>>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
            return data.response;
        }
    }
   //public static async Task<string>

   public static async Task<string> GetHistory(int perPage, int page,)
    {
        var uri = host + "item/events/${address}/${tokenId}";
        WWWForm form = new WWWForm();
        form.AddField("perPage", perPage);
        form.AddField("page", page);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) 
        {
            await webRequest.SendWebRequest();
            Response<string> data = JsonUtility.FromJson<Response<string>>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
            return data.response;
        }

    }
}