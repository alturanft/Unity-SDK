using System;
using System.Collections;
using System.Collections.Generic;
using AlturaSDK.Enum;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;


namespace AlturaSDK.API
{
    using Exceptions;
    using Settings;
    using Utils;

    public partial class AlturaAPI : MonoBehaviour
    {
        private readonly List<UnityWebRequest> _requests = new List<UnityWebRequest>();
        private bool _wasRefreshed;




        /// <summary>
        /// Stops all currently active API requests
        /// </summary>
        public void StopAll()
        {
            _requests.ForEach(r => r.Dispose());
            _requests.Clear();
        }

        private void OnDestroy()
        {
            StopAll();
            StopAllCoroutines();
        }

        private IEnumerator Perform<T>(ApiRequest request, Action<T> onSuccess, Action<Exception> onError)
        {
            var unityRequest = ConvertToUnityRequest(request);

            _requests.Add(unityRequest);

            yield return StartCoroutine(Send(unityRequest));

            Process(unityRequest, onSuccess, exception => OnError(exception, request, onSuccess, onError));

            _requests.Remove(unityRequest);
        }

                private static IEnumerator Send(UnityWebRequest request)
        {
#if UNITY_2018_1_OR_NEWER
            yield return request.SendWebRequest();
#else
			yield return request.Send();

#endif
        }

        private static void Process<T>(UnityWebRequest request, Action<T> onSuccess = null,
            Action<Exception> onError = null)
        {
            try
            {
                Validate(request);

                var response = JsonUtility.FromJson<T>(request.downloadHandler.text);

                onSuccess?.Invoke(response);
            }
            catch (Exception e)
            {
                onError?.Invoke(e);
            }
        }


    private static void Validate(UnityWebRequest request)
        {
#if UNITY_2020_1_OR_NEWER
            var isNetworkError = request.result == UnityWebRequest.Result.ConnectionError;
            var isHttpError = request.result == UnityWebRequest.Result.ProtocolError;
#else
			var isNetworkError = webRequest.isNetworkError;
			var isHttpError = webRequest.isHttpError;
#endif
            if (isNetworkError)
            {
                throw new ApiNetworkException();
            }

            var url = request.url;
            var data = request.downloadHandler.text;


            switch (isHttpError)
            {
                case true when Parser.ParseHttpError(data, out var exception):
                    throw exception;
                case true:
                    throw new ApiHttpException("unknown_error");
            }
        }

        private void OnError<T>(Exception exception, ApiRequest request, Action<T> onSuccess = null,
            Action<Exception> onError = null)
        {
            if (_wasRefreshed || (!(exception is ApiHttpException) && !(exception is InvalidRequestException)))
            {
    

                Debug.LogError(exception);

                onError?.Invoke(exception);
                return;
            }

        }

        private static UnityWebRequest ConvertToUnityRequest(ApiRequest request)
        {
            UnityWebRequest unityWebRequest;

            if (request.FormData != null && (request.Method == UnityWebRequest.kHttpVerbPOST ||
                                             request.Method == UnityWebRequest.kHttpVerbPUT))
            {
                unityWebRequest = UnityWebRequest.Post(BuildUrl(request), request.FormData);
            }
            else
            {
                unityWebRequest = new UnityWebRequest(BuildUrl(request), request.Method);

                unityWebRequest = AssignBody(request, unityWebRequest);
            }

            unityWebRequest = AssignHeaders(request, unityWebRequest);
            unityWebRequest = AssignDefaults(unityWebRequest);

            return unityWebRequest;
        }

        private static string BuildUrl(ApiRequest request)
        {
            var url = request.Uri;

            if (!request.Params.Any()) return url;

            url += (url.Contains("?") ? "&" : "?") + string.Join("&",
                request.Params.Select(p => $"{p.Key}={UnityWebRequest.EscapeURL(p.Value)}").ToArray()
            );

            return url;
        }

        private static UnityWebRequest AssignHeaders(ApiRequest request, UnityWebRequest unityWebRequest)
        {
            foreach (var pair in GetDefaultHeaders(request))
            {
                unityWebRequest.SetRequestHeader(pair.Key, pair.Value);
            }

            if (!request.Headers.Any()) return unityWebRequest;

            foreach (var pair in request.Headers)
            {
                unityWebRequest.SetRequestHeader(pair.Key, pair.Value);
            }

            return unityWebRequest;
        }

        private static UnityWebRequest AssignBody(ApiRequest request, UnityWebRequest unityWebRequest)
        {
            if (request.Method != UnityWebRequest.kHttpVerbPOST && request.Method != UnityWebRequest.kHttpVerbPUT)
            {
                return unityWebRequest;
            }

            var rawBody = Array.Empty<byte>();
            var contentType = "";

            if (request.Body != null)
            {
                rawBody = Encoding.UTF8.GetBytes(JsonUtility.ToJson(request.Body).ToCharArray());
                contentType = "application/json";
            }
            else if (request.SimpleForm != null && request.SimpleForm.Count > 0)
            {
                rawBody = UnityWebRequest.SerializeSimpleForm(request.SimpleForm);
                contentType = "application/x-www-form-urlencoded";
            }

            unityWebRequest.uploadHandler = new UploadHandlerRaw(rawBody);
            unityWebRequest.uploadHandler.contentType = contentType;

            unityWebRequest.SetRequestHeader(Constants.ContentTypeHeader, contentType);

            return unityWebRequest;
        }

        private static UnityWebRequest AssignDefaults(UnityWebRequest unityWebRequest)
        {
            if (unityWebRequest.GetRequestHeader(Constants.ContentTypeHeader) == "")
            {
                unityWebRequest.SetRequestHeader(Constants.ContentTypeHeader, Constants.DefaultContentType);
            }

            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();

            return unityWebRequest;
        }

        private static Dictionary<string, string> GetDefaultHeaders(ApiRequest request)
        {
            var dictionary = new Dictionary<string, string>();


          //  if (request.UseAuthorization && Altura.Token.Exists())
          //  {
           //     dictionary["Authorization"] = $"Bearer {Altura.Token.AccessToken}";
           // }

            return dictionary;
        }
    }
}