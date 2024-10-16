using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace AlturaNFT
{
    using Internal;

    /// <summary>
    /// Altura Guard Poll Transaction Response API
    /// </summary>
    [AddComponentMenu(AlturaConstants.BaseComponentMenu + AlturaConstants.FeatureName_AlturaGuardPollTransactionResponse)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_PENDING)]
    public class AlturaGuardPollTransactionResponse : MonoBehaviour
    {

        #region Parameter Defines

        private string _token;
        private string _requestId;
        private string RequestUriInit = AlturaConstants.APILink + "/alturaguard/getResponse";
        private string WEB_URL;
        private string FORM;
        private bool destroyAtEnd = false;

        private UnityAction<string> OnErrorAction;
        private UnityAction<AlturaGuard_model> OnCompleteAction;

        [Space(20)]
        //[Header("Called After Successful API call")]
        public UnityEvent afterSuccess;
        //[Header("Called After Error API call")]
        public UnityEvent afterError;

        [Header("Run Component when this Game Object is Set Active")]
        [SerializeField] private bool onEnable = false;
        public bool debugErrorLog = true;
        public bool debugLogRawApiResponse = true;

        [Header("Gets filled with data and can be referenced:")]
        public AlturaGuard_model alturaGuard;


        #endregion


        private void Awake()
        {
            AlturaUser.Initialise();

        }

        private void OnEnable()
        {
            if (onEnable & Application.isPlaying)
            {
                AlturaUser.SetFromOnEnable();
                Run();
            }
        }

        #region SetParams and Chain Functions

        /// <summary>
        /// Initialize creates a gameobject and assings this script as a component. This must be called if you are not refrencing the script any other way and it doesn't already exists in the scene.
        /// </summary>
        /// <param name="destroyAtEnd"> Optional bool parameter can set to false to avoid Spawned GameObject being destroyed after the Api process is complete. </param>
        public static AlturaGuardPollTransactionResponse Initialize(bool destroyAtEnd = true)
        {
            var _this = new GameObject(AlturaConstants.FeatureName_AlturaGuardPollTransactionResponse).AddComponent<AlturaGuardPollTransactionResponse>();
            _this.destroyAtEnd = destroyAtEnd;
            _this.onEnable = false;
            _this.debugErrorLog = false;
            return _this;
        }

        /// <summary>
        /// Set Parameters to update item properties with
        /// </summary>
        /// <param name="token"> the user token recieved when authenticating</param>
        /// <param name="requestId"> transaction request ID received</param>
        public AlturaGuardPollTransactionResponse SetProperties(string apiKey = null, string token = null, string requestId = null)
        {
            if (apiKey != null)
                this._apiKey = apiKey;
            if (token != null)
                this._token = token;
            if (requestId != null)
                this._requestId = requestId
            return this;
        }
        public AlturaGuardPollTransactionResponse OnComplete(UnityAction<AlturaGuard_model> action)
        {
            this.OnCompleteAction = action;
            return this;
        }
        /// <summary>
        /// Action on Error (⊙.◎)
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> Information on Error as string text.</returns>
        public AlturaGuardPollTransactionResponse OnError(UnityAction<string> action)
        {
            this.OnErrorAction = action;
            return this;
        }

        #endregion


        #region Run - API
        /// <summary>
        /// Runs the Api call and fills the corresponding model in the component on success.
        /// </summary>
        public AlturaGuard_model Run()
        {
            WEB_URL = BuildUrl();
            FORM = CreateForm();
            StopAllCoroutines();
            StartCoroutine(CallAPIProcess());
            return item;
        }

        string BuildUrl()
        {
            WEB_URL = RequestUriInit
            Debug.Log("Abol: " + WEB_URL);
            if (debugErrorLog)
                Debug.Log("polling Altura Guard transaction response: ");

            return WEB_URL;
        }

        WWWForm CreateForm()
        {
            WWWForm form = new WWWForm();
            if (this._token != null)
                form.AddField("token", this._token);
            if (this._requestId != null)
                form.AddField("requestId", this._requestId)
            return form;
        }

        IEnumerator CallAPIProcess()
        {
            //Make request
            UnityWebRequest www = UnityWebRequest.Post(WEB_URL, FORM);
            request.SetRequestHeader("Content-Type", "application/json");
            {
                yield return request.SendWebRequest();
                string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);

                if (debugLogRawApiResponse)
                    Debug.Log(jsonResult);

                if (request.error != null)
                {
                    if (OnErrorAction != null)
                        OnErrorAction($"Null data. Response code: {request.responseCode}. Result {jsonResult}");
                    if (debugErrorLog)
                        Debug.Log($" Null data. Response code: {request.responseCode}. Result {jsonResult}");
                    if (afterError != null)
                        afterError.Invoke();
                    item = null;
                    //yield break;
                }
                else
                {
                    item = JsonConvert.DeserializeObject<AlturaGuard_model>(
                        jsonResult,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        });

                    if (OnCompleteAction != null)
                        OnCompleteAction.Invoke(item);

                    if (afterSuccess != null)
                        afterSuccess.Invoke();

                    Debug.Log($"view txHash under AlturaGuard_model");
                }
            }
            request.Dispose();
            if (destroyAtEnd)
                DestroyImmediate(this.gameObject);
        }

        #endregion
    }

}
