using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace AlturaNFT
{
    using Internal;

    /// <summary>
    /// Get User from Domain Names API
    /// </summary>
    [AddComponentMenu(AlturaConstants.BaseComponentMenu + AlturaConstants.FeatureName_GetUserFromDomainName)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_PENDING)]
    public class GetUserFromDomainName : MonoBehaviour
    {

        #region Parameter Defines

        private string jsonString;
        private string _domain;
        private string RequestUriInit = AlturaConstants.APILink + "/v2/spaceid/getAddressByDomain/:domain";
        private string WEB_URL;
        private string _apiKey;
        private bool destroyAtEnd = false;

        private UnityAction<string> OnErrorAction;
        private UnityAction<UserFromDomain_model> OnCompleteAction;

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
        public UserFromDomain_model userFromDomain;


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
        public static GetUserFromDomainName Initialize(bool destroyAtEnd = true)
        {
            var _this = new GameObject(AlturaConstants.FeatureName_GetUserFromDomainName).AddComponent<GetUserFromDomainName>();
            _this.destroyAtEnd = destroyAtEnd;
            _this.onEnable = false;
            _this.debugErrorLog = false;
            return _this;
        }

        /// <summary>
        /// Set Parameters to retrieve User From .
        /// </summary>
        /// <param name="domain"> domain of the user</param>
        public GetUserFromDomainName SetParameters(string domain = null)
        {
            if (domain != null)
                this._domain = domain;
            return this;
        }
        public GetUserFromDomainName OnComplete(UnityAction<UserFromDomain_model> action)
        {
            this.OnCompleteAction = action;
            return this;
        }
        /// <summary>
        /// Action on Error (⊙.◎)
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> Information on Error as string text.</returns>
        public GetUserFromDomainName OnError(UnityAction<string> action)
        {
            this.OnErrorAction = action;
            return this;
        }

        #endregion


        #region Run - API
        /// <summary>
        /// Runs the Api call and fills the corresponding model in the component on success.
        /// </summary>
        public UserFromDomain_model Run()
        {
            WEB_URL = BuildUrl();
            StopAllCoroutines();
            StartCoroutine(CallAPIProcess());
            return userFromDomain;
        }

        string BuildUrl()
        {
            this.jsonString = "";            
            if(this._domain != null)
                this.jsonString += "&domain=" + this._domain;


            WEB_URL = RequestUriInit + "?" + jsonString;
            Debug.Log("Abol: " + WEB_URL);
            if (debugErrorLog)
                Debug.Log("Querying user from domain name: ");

            return WEB_URL;
        }

        IEnumerator CallAPIProcess()
        {
            //Make request
            UnityWebRequest request = UnityWebRequest.Get(WEB_URL);
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
                    userFromDomain = null;
                    //yield break;
                }
                else
                {
                    userFromDomain = JsonConvert.DeserializeObject<UserFromDomain_model>(
                        jsonResult,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        });

                    if (OnCompleteAction != null)
                        OnCompleteAction.Invoke(userFromDomain);

                    if (afterSuccess != null)
                        afterSuccess.Invoke();

                    Debug.Log($"view user under User model");
                }
            }
            request.Dispose();
            if (destroyAtEnd)
                DestroyImmediate(this.gameObject);
        }

        #endregion
    }

}
