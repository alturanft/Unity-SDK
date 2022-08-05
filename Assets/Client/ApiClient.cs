using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SDK.Assets.Client
{
    public class ApiClient 
    {
        private readonly Dictionary<String, String> _defaultHeaderMap = new Dictionary<String, String>();

        public ApiClient(String basePath = "https://api.alturanft.com", string apiVersion="/api/v2")
        {
            BasePath = basePath;
            RestClient = new HttpClient(); 
            RestClient.BaseAddress = new Uri(BasePath);
            ApiVersion = apiVersion;
        }


        public string ApiVersion { get; set; }
        public string BasePath { get; set; }

        public HttpClient RestClient { get; set; }

  public Dictionary<String, String> DefaultHeader
        {
            get { return _defaultHeaderMap; }
        }

        public async Task<HttpResponseMessage> CallApi(String path, HttpMethod method, String postBody,
            Dictionary<String, String> headerParams, Dictionary<String, String> queryParams, String[] authSettings)
        {
            path = $"{ApiVersion}{path}";
            
            if (queryParams.Count > 0)
            {
                using (var content = new FormUrlEncodedContent(queryParams))
                {
                    string query = content.ReadAsStringAsync().Result;

                    path = $"{path}?{query}";
                }
            }

            //var request = new RestRequest(path, method);
            var request = new HttpRequestMessage(method, path);

         

            // add default header, if any
            foreach (var defaultHeader in _defaultHeaderMap)
                request.Headers.Add(defaultHeader.Key, defaultHeader.Value);

            // add header parameter, if any
            foreach (var param in headerParams)
                request.Headers.Add(param.Key, param.Value);

            if (postBody != null)
            { 
                request.Content = new StringContent(postBody, Encoding.UTF8, "application/json");
            }

            Task<HttpResponseMessage> restTask = Task.Run(() =>
            {
                return RestClient.SendAsync(request);
            });

            return await restTask;
        }

    }
}

