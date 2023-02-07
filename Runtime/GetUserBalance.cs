using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


namespace AlturaNFT  
{ using Internal;
    
    /// <summary>
    /// Get Users Native Balances
    /// </summary>
    [AddComponentMenu(AlturaConstants.BaseComponentMenu+AlturaConstants.FeatureName_GetUserBalance)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_GetUserBalance)]
    public class GetUserBalance : MonoBehaviour
    {

        #region Parameter Defines
  
            [SerializeField]
            private string _userAddress = "input users address";
            
            [SerializeField]
            [Tooltip("Network ID!")]
            private int _chainID = 1;

            private string RequestUriInit = AlturaConstants.APILink + "/v2/native/balance";
            private string WEB_URL;
            private string _apiKey;
            private bool destroyAtEnd = false;


            private UnityAction<string> OnErrorAction;
            private UnityAction<Reponse_Balance_model> OnCompleteAction;
            
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
            public Reponse_Balance_model reponse;

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
        public static GetUserBalance Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject(AlturaConstants.FeatureName_GetUserBalance).AddComponent<GetUserBalance>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = false;
                _this.debugErrorLog = false;
                return _this;
            }

        /// <summary>
        /// Set Parameters to retrieve Item Details
        /// </summary>
        /// <param name="userAddress"> as string - EVM</param>
        /// <param name="chainId"> as int - EVM.</param>
        public GetUserBalance SetParameters(string userAddress = null, int chainId = -1)
            {
                if(userAddress!=null)
                    this._userAddress = userAddress;
                if (chainId != -1)
                this._chainID = chainId;
                return this;
            }

            public GetUserBalance OnComplete(UnityAction<Reponse_Balance_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }
            
            /// <summary>
            /// Action on Error (⊙.◎)
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> Information on Error as string text.</returns>
            public GetUserBalance OnError(UnityAction<string> action)
            {
                this.OnErrorAction = action;
                return this;
            }
            
        #endregion

        
        #region Run - API
            /// <summary>
            /// Runs the Api call and fills the corresponding model in the component on success.
            /// </summary>
            public Reponse_Balance_model Run()
            {
                WEB_URL = BuildUrl();
                StopAllCoroutines();
                StartCoroutine(CallAPIProcess());
                return reponse;
            }

            string BuildUrl()
            {
                    WEB_URL = RequestUriInit + "?userAddress=" + this._userAddress + "&chainId=" + this._chainID.ToString();

            if (debugErrorLog)
                        Debug.Log("Getting Balance of: " + this._userAddress + " on chainID: " + this._chainID.ToString());
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
                    
                    if(debugLogRawApiResponse)
                        Debug.Log(jsonResult);

                    if (request.error != null)
                    {
                    reponse = null;
                        if(OnErrorAction!=null)
                            OnErrorAction($"Null data. Response code: {request.responseCode}. Result {jsonResult}");
                        if(debugErrorLog)
                            Debug.Log($" Null data. Response code: {request.responseCode}. Result {jsonResult}");
                        if(afterError!=null)
                            afterError.Invoke();
                        //yield break;
                    }
                    else
                    {
                    //Fill Data Model from recieved class
                    reponse = JsonConvert.DeserializeObject<Reponse_Balance_model>(
                            jsonResult,
                            new JsonSerializerSettings
                            {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                            });
                        
                        if(OnCompleteAction!=null)
                            OnCompleteAction.Invoke(reponse);
                        
                        if(afterSuccess!=null)
                            afterSuccess.Invoke();
                        
                        if(debugErrorLog)
                            Debug.Log("Success , view Item under Item model" );
                    }
                }
                request.Dispose();
                if(destroyAtEnd)
                    DestroyImmediate(this.gameObject);
            }
            
        #endregion
    }

}
