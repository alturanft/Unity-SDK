using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


namespace AlturaNFT  

{ 
    
    using Internal;
    /// <summary>
    /// Details of particular User
    /// </summary>
    [AddComponentMenu(AlturaConstants.BaseComponentMenu+AlturaConstants.FeatureName_GetUser)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_GetUser)]
    public class GetUser : MonoBehaviour
    {
        #region Parameter Defines

            
            [SerializeField]
            private string _address = "Input Address of the Altura User";
            
            private string RequestUriInit = "https://api.alturanft.com/api/v2/user/";
            private string WEB_URL;
            private string _apiKey;
            private bool destroyAtEnd = false;


            private UnityAction<string> OnErrorAction;
            private UnityAction<User_model> OnCompleteAction;
            
            [Space(20)]
            //[Header("Called After Successful API call")]
            public UnityEvent afterSuccess;
            //[Header("Called After Error API call")]
            public UnityEvent afterError;

            [Header("Run Component when this Game Object is Set Active")]
            [SerializeField] private bool onEnable = true;
            public bool debugErrorLog = true;
            public bool debugLogRawApiResponse = true;
            
            [Header("Gets filled with data and can be referenced:")]
            public User_model users;

        #endregion


        private void Awake()
        {
            AlturaUser.Initialise();
            _apiKey = AlturaUser.GetUserApiKey();
            
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

     
        public static GetUser Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject(AlturaConstants.FeatureName_GetUser).AddComponent<GetUser>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = false;
                _this.debugErrorLog = false;
                return _this;
            }

        /// <summary>
        /// Set Parameters to retrieve User From
        /// </summary>
        /// <param name="_address"> as string - EVM</param>
        public GetUser SetParameters(string _addresss = null)
            {
                if(_addresss!=null)
                    this._address = _addresss;
     

                return this;
            }
            

            public GetUser OnComplete(UnityAction<User_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }
            
            /// <summary>
            /// Action on Error (⊙.◎)
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> Information on Error as string text.</returns>
            public GetUser OnError(UnityAction<string> action)
            {
                this.OnErrorAction = action;
                return this;
            }
            
        #endregion

        
        #region Run - API
            /// <summary>
            /// Runs the Api call and fills the corresponding model in the component on success.
            /// </summary>
            public User_model Run()
            {
                WEB_URL = BuildUrl();
                StopAllCoroutines();
                StartCoroutine(CallAPIProcess());
                return users;
            }

            string BuildUrl()
            {

                    WEB_URL = RequestUriInit + _address;
                    if(debugErrorLog)
                        Debug.Log("Querying Details of User: " + _address + " on " );
                return WEB_URL;
            }
            
            IEnumerator CallAPIProcess()
            {
                //Make request
                UnityWebRequest request = UnityWebRequest.Get(WEB_URL);
                request.SetRequestHeader("Content-Type", "application/json");             
            string url = "https://api.alturanft.com/api/sdk/unity/";
            WWWForm form = new WWWForm();
            UnityWebRequest www = UnityWebRequest.Post(url + "GetUser" + "?apiKey=" + _apiKey, form);
                {
                    yield return request.SendWebRequest();
                    string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                    
                    if(debugLogRawApiResponse)
                        Debug.Log(jsonResult);

                    if (request.error != null)
                    {
                        if(OnErrorAction!=null)
                            OnErrorAction($"Null data. Response code: {request.responseCode}. Result {jsonResult}");
                        if(debugErrorLog)
                            Debug.Log($" Null data. Response code: {request.responseCode}. Result {jsonResult}");
                        if(afterError!=null)
                            afterError.Invoke();
                        users = null;
                        //yield break;
                    }
                    else
                    {
                        
                        users = JsonConvert.DeserializeObject<User_model>(
                            jsonResult,
                            new JsonSerializerSettings
                            {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                            });
                        
                        if(OnCompleteAction!=null)
                            OnCompleteAction.Invoke(users);
                        
                        if(afterSuccess!=null)
                            afterSuccess.Invoke();
                        
                            Debug.Log($"view User under User model" );
                    }
                }
                request.Dispose();
                if(destroyAtEnd)
                    DestroyImmediate(this.gameObject);
            }
            
        #endregion
    }

}
