using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace AlturaNFT
{
    using Internal;

    /// <summary>
    /// Altura Guard Check Connetion API
    /// </summary>
    [AddComponentMenu(AlturaConstants.BaseComponentMenu + AlturaConstants.FeatureName_AlturaGuardCheckConnection)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_PENDING)]
    public class AlturaGuardCheckConnection : MonoBehaviour
    {

        #region Parameter Defines

        private string jsonString;
        private string _token;
        private string RequestUriInit = AlturaConstants.APILink + "/alturaguard/status";
        private string WEB_URL;
        private string FORM;
        private string _apiKey;
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
        public static AlturaGuardCheckConnection Initialize(bool destroyAtEnd = true)
        {
            var _this = new GameObject(AlturaConstants.FeatureName_AlturaGuardCheckConnection).AddComponent<AlturaGuardCheckConnection>();
            _this.destroyAtEnd = destroyAtEnd;
            _this.onEnable = false;
            _this.debugErrorLog = false;
            return _this;
        }

        /// <summary>
        /// Set Parameters to update item properties with
        /// </summary>
        /// <param name="token"> the user token recieved when authenticating</param>
        public AlturaGuardCheckConnection SetProperties(string apiKey = null, string token = null)
        {
            if (apiKey != null)
                this._apiKey = apiKey;
            if (token != null)
                this._token = token;
            return this;
        }
        public AlturaGuardCheckConnection OnComplete(UnityAction<AlturaGuard_model> action)
        {
            this.OnCompleteAction = action;
            return this;
        }
        /// <summary>
        /// Action on Error (⊙.◎)
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> Information on Error as string text.</returns>
        public AlturaGuardCheckConnection OnError(UnityAction<string> action)
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
            this.jsonString = "";
            if (this._apiKey != null)
                this.jsonString += "&apiKey=" + this._apiKey;


            WEB_URL = RequestUriInit + "?" + jsonString;
            Debug.Log("Abol: " + WEB_URL);
            if (debugErrorLog)
                Debug.Log("checking Altura Guard connection: ");

            return WEB_URL;
        }

        WWWForm CreateForm()
        {
            WWWForm form = new WWWForm();
            if (this._token != null)
                form.AddField("token", this._token);
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

                    Debug.Log($"altura guard status returned");
                }
            }
            request.Dispose();
            if (destroyAtEnd)
                DestroyImmediate(this.gameObject);
        }

        #endregion
    }

}
