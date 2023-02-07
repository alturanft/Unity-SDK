using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


namespace AlturaNFT  
{ using Internal;
    
    /// <summary>
    /// Get an Item
    /// </summary>
    [AddComponentMenu(AlturaConstants.BaseComponentMenu+AlturaConstants.FeatureName_GetItem)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_NFTDetails)]
    public class GetItem : MonoBehaviour
    {

        #region Parameter Defines
  
            [SerializeField]
            private string _collection_address = "Input Collection Address of the Item";
            
            [SerializeField]
            [Tooltip("Token ID of the Item")]
            private int _token_id = 1;
            

            private string RequestUriInit = AlturaConstants.APILink + "/v2/item/";
            private string WEB_URL;
            private string _apiKey;
            private bool destroyAtEnd = false;


            private UnityAction<string> OnErrorAction;
            private UnityAction<Items_model> OnCompleteAction;
            
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
            public Items_model item;

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
        public static GetItem Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject(AlturaConstants.FeatureName_GetItem).AddComponent<GetItem>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = false;
                _this.debugErrorLog = false;
                return _this;
            }

        /// <summary>
        /// Set Parameters to retrieve Item Details
        /// </summary>
        /// <param name="collection_address"> as string - EVM</param>
        /// <param name="token_id"> as int - EVM.</param>
        public GetItem SetParameters(string collection_address = null, int token_id = -1)
            {
                if(collection_address!=null)
                    this._collection_address = collection_address;
                if (token_id != -1)
                    _token_id = token_id;


                return this;
            }

            public GetItem OnComplete(UnityAction<Items_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }
            
            /// <summary>
            /// Action on Error (⊙.◎)
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> Information on Error as string text.</returns>
            public GetItem OnError(UnityAction<string> action)
            {
                this.OnErrorAction = action;
                return this;
            }
            
        #endregion

        
        #region Run - API
            /// <summary>
            /// Runs the Api call and fills the corresponding model in the component on success.
            /// </summary>
            public Items_model Run()
            {
                WEB_URL = BuildUrl();
                StopAllCoroutines();
                StartCoroutine(CallAPIProcess());
                return item;
            }

            string BuildUrl()
            {

                    WEB_URL = RequestUriInit + _collection_address + "/" + _token_id.ToString();
                    if(debugErrorLog)
                        Debug.Log("Querying Single Item by address and tokenId: " + _collection_address + " on " );
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
                        item = null;
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
                        item = JsonConvert.DeserializeObject<Items_model>(
                            jsonResult,
                            new JsonSerializerSettings
                            {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                            });
                        
                        if(OnCompleteAction!=null)
                            OnCompleteAction.Invoke(item);
                        
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
