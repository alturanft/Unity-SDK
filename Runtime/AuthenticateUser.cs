using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace AlturaNFT  
{ using Internal;
    
    /// <summary>
    /// User authentication
    /// </summary>
    [AddComponentMenu(AlturaConstants.BaseComponentMenu+AlturaConstants.FeatureName_AuthenticateUser)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.AuthenticateUser)]
    public class AuthenticateUser : MonoBehaviour
    {
        /// <summary>
        /// Currently Supported chains for this endpoint.
        /// </summary>
        public enum Chains
        {
            ethereum,
            bsctest,
            rinkeby,
            binance
        }
        
        public enum Includes
        {
            Default,
            metadata, 
            all
        }
        
        #region Parameter Defines

            
            [SerializeField]
            private string wallet_address = "Input Wallet Address to verify";
            [SerializeField]
            private string altura_gaurd = "Input Altura Gaurd code to verify";


            
       //     private string RequestUriInit = "https://api.alturanft.com/api/v2/user/verify_auth_code/";
            private string WEB_URL;
            private string _apiKey;
            private bool destroyAtEnd = false;


            private UnityAction<string> OnErrorAction;
            private UnityAction<Auth_model> OnCompleteAction;
            
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
            public Auth_model gaurd;

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

        /// <summary>
        /// Initialize creates a gameobject and assings this script as a component. This must be called if you are not refrencing the script any other way and it doesn't already exists in the scene.
        /// </summary>
        /// <param name="destroyAtEnd"> Optional bool parameter can set to false to avoid Spawned GameObject being destroyed after the Api process is complete. </param>
        public static AuthenticateUser Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject("NFTs Of a Contract").AddComponent<AuthenticateUser>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = false;
                _this.debugErrorLog = false;
                return _this;
            }
            
            /// <summary>
            /// Set Contract Address to retrieve NFTs from as string
            /// </summary>
            /// <param name="wallet_address"> as string.</param>
            public AuthenticateUser SetParams(string wallet_address, string altura_gaurd)
            {
                this.wallet_address = wallet_address;
                this.altura_gaurd = altura_gaurd;
                return this;
            }

            

            public AuthenticateUser OnComplete(UnityAction<Auth_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }
            
            
            /// <summary>
            /// Action on Error
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> Information on Error as string text.</returns>
            public AuthenticateUser OnError(UnityAction<string> action)
            {
                this.OnErrorAction = action;
                return this;
            }
            
        #endregion

        
        #region Run - API
            /// <summary>
            /// Runs the Api call and fills the corresponding model in the component on success.
            /// </summary>
            public Auth_model Run()
            {
                WEB_URL = BuildUrl();
                StopAllCoroutines();
                StartCoroutine(CallAPIProcess());
                return gaurd;
            }

            string BuildUrl()
            {
                WEB_URL = "https://api.alturanft.com/api/v2/user/verify_auth_code/" + wallet_address + "/" + altura_gaurd;

                return WEB_URL;
            }
            
            IEnumerator CallAPIProcess()
            {
                //Make request
                UnityWebRequest request = UnityWebRequest.Get(WEB_URL);
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("source", AlturaUser.GetSource());
                request.SetRequestHeader("Authorization", _apiKey);
                

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
                            Debug.Log($"Null data. Response code: {request.responseCode}. Result {jsonResult}");
                        if(afterError!=null)
                            afterError.Invoke();
                        //yield break;
                    }
                    else
                    {
                        //Fill Data Model from recieved class
                        gaurd = JsonConvert.DeserializeObject<Auth_model>(
                            jsonResult,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                MissingMemberHandling = MissingMemberHandling.Ignore
                            });
                        
                        if(OnCompleteAction!=null)
                            OnCompleteAction.Invoke(gaurd);
                        
                        if(afterSuccess!=null)
                            afterSuccess.Invoke();
                        
                        if(debugErrorLog)
                            Debug.Log("Success , Verify Auth ran" );
                    }
                }
                request.Dispose();
                if(destroyAtEnd)
                    Destroy (this.gameObject);
            }
            
        #endregion
    }

}
