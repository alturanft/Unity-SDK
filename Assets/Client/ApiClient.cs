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

        public ApiClient(String basePath = "http://localhost:3063", string apiVersion="/api/v2")
        {
            BasePath = basePath;
            RestClient = new HttpClient(); //new RestClient(BasePath);
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
            // Add Api & Version as HttpClient strips this off.
            path = $"{ApiVersion}{path}";
            
            // Add query parameters to path.
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

            UpdateParamsForAuth(headerParams, authSettings);

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

  
        public async Task<Object> CallApi(String path, HttpMethod method, Dictionary<String, String> queryParams, String postBody,
           // TODO Add support for file params.
            Dictionary<String, String> headerParams, Dictionary<String, String> formParams,
            Dictionary<String, object> fileParams, String[] authSettings)
        {
            return CallApi(path, method, postBody, headerParams, queryParams, authSettings);

        }


            public void AddDefaultHeader(string key, string value)
        {
            _defaultHeaderMap.Add(key, value);
        }

      
        public string EscapeString(string str)
        {
#if UNITY_2019_1_OR_NEWER
            return UnityEngine.Networking.UnityWebRequest.EscapeURL(str);
#else
            return HttpUtility.UrlEncode(str);
#endif
        }

  

        public string ParameterToString(object obj)
        {
            if (obj is DateTime)

                return ((DateTime)obj).ToString(Configuration.DateTimeFormat);
            else if (obj is List<string>)
                return String.Join(",", (obj as List<string>).ToArray());
            else if (obj is string || obj is int || obj is long || obj is decimal || obj is bool || obj is float || obj is double || obj is byte || obj is char)
            {
                return obj.ToString();
            }
            else
                return JsonConvert.SerializeObject(obj);
        }

        public string ParameterToHex(long val)
        {
            string resp = $"0x{val.ToString("X")}";

            return resp;
        }

        public async Task<object> Deserialize(HttpContent content, Type type, HttpResponseHeaders headers = null)
        {
            string jsonContent = await content.ReadAsStringAsync();

            if (type == typeof(Object)) // return an object
            {
                return jsonContent;
            }

            if (type == typeof(Stream))
            {
                var filePath = String.IsNullOrEmpty(Configuration.TempFolderPath)
                    ? Path.GetTempPath()
                    : Configuration.TempFolderPath;

                var fileName = filePath + Guid.NewGuid();
                if (headers != null)
                {
                    var regex = new Regex(@"Content-Disposition:.*filename=['""]?([^'""\s]+)['""]?$");
                    var match = regex.Match(headers.ToString());
                    if (match.Success)
                        fileName = filePath + match.Value.Replace("\"", "").Replace("'", "");
                }
                File.WriteAllText(fileName, jsonContent);
                return new FileStream(fileName, FileMode.Open);

            }

            if (type.Name.StartsWith("System.Nullable`1[[System.DateTime")) // return a datetime object
            {
                return DateTime.Parse(jsonContent, null, System.Globalization.DateTimeStyles.RoundtripKind);
            }

            if (type == typeof(String) || type.Name.StartsWith("System.Nullable")) // return primitive type
            {
                return ConvertType(content, type);
            }

            // at this point, it must be a model (json)
            try
            {
                return JsonConvert.DeserializeObject(jsonContent, type);
            }
            catch (IOException e)
            {
                throw new ApiException(500, e.Message);
            }
        }

        public string Serialize(object obj)
        {
            try
            {
                return obj != null ? JsonConvert.SerializeObject(obj) : null;
            }
            catch (Exception e)
            {
                throw new ApiException(500, e.Message);
            }
        }

        /// <summary>
        /// Get the API key with prefix.
        /// </summary>
        /// <param name="apiKeyIdentifier">API key identifier (authentication scheme).</param>
        /// <returns>API key with prefix.</returns>
        public string GetApiKeyWithPrefix(string apiKeyIdentifier)
        {
            var apiKeyValue = "";
            Configuration.ApiKey.TryGetValue(apiKeyIdentifier, out apiKeyValue);
            var apiKeyPrefix = "";
            if (Configuration.ApiKeyPrefix.TryGetValue(apiKeyIdentifier, out apiKeyPrefix))
                return apiKeyPrefix + " " + apiKeyValue;
            else
                return apiKeyValue;
        }

        /// <summary>
        /// Update parameters based on authentication.
        /// </summary>
        /// <param name="queryParams">Query parameters.</param>
        /// <param name="headerParams">Header parameters.</param>
        /// <param name="authSettings">Authentication settings.</param>
        public void UpdateParamsForAuth(Dictionary<String, String> queryParams, Dictionary<String, String> headerParams, string[] authSettings)
        {
            UpdateParamsForAuth(headerParams, authSettings);
        }

        public void UpdateParamsForAuth(Dictionary<String, String> headerParams, string[] authSettings)
        {

            if (authSettings == null || authSettings.Length == 0)
                return;

            foreach (string auth in authSettings)
            {
                // determine which one to use
                switch (auth)
                {
                    case "ApiKeyAuth":
                        headerParams["X-API-Key"] = GetApiKeyWithPrefix("X-API-Key");

                        break;
                    default:
                        //TODO show warning about security definition not found
                        break;
                }
            }
        }

        /// <summary>
        /// Encode string in base64 format.
        /// </summary>
        /// <param name="text">String to be encoded.</param>
        /// <returns>Encoded string.</returns>
        public static string Base64Encode(string text)
        {
            var textByte = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textByte);
        }

        /// <summary>
        /// Dynamically cast the object into target type.
        /// </summary>
        /// <param name="fromObject">Object to be casted</param>
        /// <param name="toObject">Target type</param>
        /// <returns>Casted object</returns>
        public static Object ConvertType(Object fromObject, Type toObject)
        {
            return Convert.ChangeType(fromObject, toObject);
        }

    }
}


    }
}