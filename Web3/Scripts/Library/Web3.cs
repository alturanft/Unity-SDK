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
    private readonly static string host = "https://api.alturanft.com/api/v2/";
    public static async Task<string> GetHolders(int perPage, int page, bool includeListed)
{
    var url = host + "holders?perPage=" + perPage + "&page=" + page + "&includeListed=" + includeListed;
    WWWForm form = new WWWForm();
    form.AddField("perPage", perPage);
    form.AddField("page", page);
    string uri = host + "item";
    //UnityWebRequest www = UnityWebRequest.Get(Url);
    using (UnityWebRequest req = UnityWebRequest.Get(uri)) 
    {
        await req.SendWebRequest();
        Respons<string> rez = JsonUtility.FromJson<Response<string>>(System.Text.Encoding.UTF8.GetString(req.downloadHandler.data));
        return rez.data;
    }
}