using System;
using UnityEngine;
using UnityEngine.Networking;

namespace AlturaSDK.API
{
   using Responses;

    public partial class AlturaAPI
    {
        private Coroutine Get(ApiRequest request)
        {
            return Get<GenericResponse>(request);
        }

        private Coroutine Get<T>(ApiRequest request)
        {
            request.Method = UnityWebRequest.kHttpVerbGET;

            return Request<T>(request);
        }

        private Coroutine Head(ApiRequest request, Action onSuccess = null, Action<Exception> onError = null)
        {
            return Head<GenericResponse>(request, (_) => onSuccess?.Invoke(), onError);
        }

        private Coroutine Head<T>(ApiRequest request, Action<T> onSuccess = null, Action<Exception> onError = null)
        {
            request.Method = UnityWebRequest.kHttpVerbHEAD;

            return Request<T>(request, onSuccess, onError);
        }

        private Coroutine Post(ApiRequest request, Action onSuccess = null, Action<Exception> onError = null)
        {
            return Post<GenericResponse>(request, (_) => onSuccess?.Invoke(), onError);
        }

        private Coroutine Post<T>(ApiRequest request, Action<T> onSuccess = null, Action<Exception> onError = null)
        {
            request.Method = UnityWebRequest.kHttpVerbPOST;

            return Request<T>(request, onSuccess, onError);
        }

        private Coroutine Put(ApiRequest request, Action onSuccess = null, Action<Exception> onError = null)
        {
            return Put<GenericResponse>(request, (_) => onSuccess?.Invoke(), onError);
        }

        private Coroutine Put<T>(ApiRequest request, Action<T> onSuccess = null, Action<Exception> onError = null)
        {
            request.Method = UnityWebRequest.kHttpVerbPUT;

            return Request<T>(request, onSuccess, onError);
        }

        private Coroutine Delete(ApiRequest request, Action onSuccess = null, Action<Exception> onError = null)
        {
            return Delete<GenericResponse>(request, (_) => onSuccess?.Invoke(), onError);
        }

        private Coroutine Delete<T>(ApiRequest request, Action<T> onSuccess = null, Action<Exception> onError = null)
        {
            request.Method = UnityWebRequest.kHttpVerbDELETE;

            return Request<T>(request, onSuccess, onError);
        }

        private Coroutine Request(ApiRequest request, Action onSuccess = null, Action<Exception> onError = null)
        {
            return Request<GenericResponse>(request, _ => { onSuccess?.Invoke(); }, onError);
        }

        private Coroutine Request<T>(ApiRequest request, Action<T> onSuccess = null, Action<Exception> onError = null)
        {
            return StartCoroutine(Perform(request, onSuccess, onError));
        }
    }
}