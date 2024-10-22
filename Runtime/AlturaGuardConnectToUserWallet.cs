using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace AlturaNFT
{
    using Internal;

    /// <summary>
    /// Altura Guard Connect to User Wallet API
    /// </summary>
    [AddComponentMenu(AlturaConstants.BaseComponentMenu + AlturaConstants.FeatureName_AlturaGuardConnectToUserWallet)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_PENDING)]
    public class AlturaGuardConnectToUserWallet : MonoBehaviour
    {

        #region Parameter Defines

        private string jsonString;
        private string _code;
        private string RequestUriInit = AlturaConstants.APILink + "/alturaguard/addRequest";
        private string WEB_URL;
        private WWWForm FORM;
        private string _apiKey;
        private bool destroyAtEnd = false;

        private UnityAction<string> OnErrorAction;
        private UnityAction<AlturaGuardConnection_model> OnCompleteAction;

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
        public AlturaGuardConnection_model alturaGuard;


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
        public static AlturaGuardConnectToUserWallet Initialize(bool destroyAtEnd = true)
        {
            var _this = new GameObject(AlturaConstants.FeatureName_AlturaGuardConnectToUserWallet).AddComponent<AlturaGuardConnectToUserWallet>();
            _this.destroyAtEnd = destroyAtEnd;
            _this.onEnable = false;
            _this.debugErrorLog = false;
            return _this;
        }

        /// <summary>
        /// Set Parameters to update Altura Guard connection properties with
        /// </summary>
        /// <param name="apiKey"> your api key</param>
        /// <param name="code"> user entered altura guard code</param>
        public AlturaGuardConnectToUserWallet SetProperties(string apiKey = null, string code = null)
        {
            if (apiKey != null)
                this._apiKey = apiKey;
            if (code != null)
                this._code = code;
            return this;
        }
        public AlturaGuardConnectToUserWallet OnComplete(UnityAction<AlturaGuardConnection_model> action)
        {
            this.OnCompleteAction = action;
            return this;
        }
        /// <summary>
        /// Action on Error (⊙.◎)
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> Information on Error as string text.</returns>
        public AlturaGuardConnectToUserWallet OnError(UnityAction<string> action)
        {
            this.OnErrorAction = action;
            return this;
        }

        #endregion


        #region Run - API
        /// <summary>
        /// Runs the Api call and fills the corresponding model in the component on success.
        /// </summary>
        public AlturaGuardConnection_model Run()
        {
            WEB_URL = BuildUrl();
            FORM = CreateForm();
            StopAllCoroutines();
            StartCoroutine(CallAPIProcess());
            return alturaGuard;
        }

        string BuildUrl()
        {
            this.jsonString = "";
            if (this._apiKey != null)
                this.jsonString += "&apiKey=" + this._apiKey;


            WEB_URL = RequestUriInit + "?" + jsonString;
            Debug.Log("Abol: " + WEB_URL);
            if (debugErrorLog)
                Debug.Log("connecting to Altura Guard for wallet: ");

            return WEB_URL;
        }

        WWWForm CreateForm()
        {
            WWWForm form = new WWWForm();
            if (this._code != null)
                form.AddField("code", this._code);
            return form;
        }

        IEnumerator CallAPIProcess()
        {
            //Make request
            UnityWebRequest request = UnityWebRequest.Post(WEB_URL, FORM);
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
                    alturaGuard = null;
                    //yield break;
                }
                else
                {
                    alturaGuard = JsonConvert.DeserializeObject<AlturaGuardConnection_model>(
                        jsonResult,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        });

                    if (OnCompleteAction != null)
                        OnCompleteAction.Invoke(alturaGuard);

                    if (afterSuccess != null)
                        afterSuccess.Invoke();

                    Debug.Log($"view token and address under AlturaGuardConnection_model");
                }
            }
            request.Dispose();
            if (destroyAtEnd)
                DestroyImmediate(this.gameObject);
        }

        #endregion
    }

}
