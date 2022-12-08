using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace AlturaNFT  
{ using Internal;
    
    /// <summary>
    /// Get Many Items
    /// </summary>
    [AddComponentMenu(AlturaConstants.BaseComponentMenu+AlturaConstants.FeatureName_Txn_NFT)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_Txns_NFT)]
    public class GetItems : MonoBehaviour
    {
        /// <summary>
        /// Currently Supported chains for this endpoint.
        /// </summary>
        public enum Chains
        {
            ethereum,
            binance,
            bsctest
            
        }

        #region Parameter Defines


            [SerializeField]
            [DrawIf("chain", Chains.binance , DrawIfAttribute.DisablingType.DontDrawInverse)]
            private string _perPage = "Input How much pqges you want to get";
            [SerializeField]
            [DrawIf("chain", Chains.binance , DrawIfAttribute.DisablingType.DontDrawInverse)]
            private string _page = "Input Which page you want to get";
            [SerializeField]
            [DrawIf("chain", Chains.binance , DrawIfAttribute.DisablingType.DontDrawInverse)]
            private string _sortBy = "Input Sort By = name";
            [SerializeField]
            [DrawIf("chain", Chains.binance , DrawIfAttribute.DisablingType.DontDrawInverse)]
            private string _sortDir = "Input Asc or Desc";
            [SerializeField]
            private string jsonString = "Filters";
            [SerializeField]
            [DrawIf("chain", Chains.binance , DrawIfAttribute.DisablingType.DontDrawInverse)]
            private string _slim = "false";

            [Header("Optional: Filter and fetch items with specified collection address")]

            [SerializeField]
            [Tooltip("Filter from a documents by any properties")]
            [DrawIf("chain", Chains.binance , DrawIfAttribute.DisablingType.DontDrawInverse)]
            string collection_address;
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
        public static GetItems Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject(AlturaConstants.FeatureName_Txn_NFT).AddComponent<GetItems>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = false;
                _this.debugErrorLog = false;
                return _this;
            }

        /// <summary>
        /// Set Parameters to retrieve User From.
        /// </summary>
        /// <param name="perPage"> amount of pages to query</param>
        /// <param name="page"> page to query</param>
        /// <param name="sortBy"> sort by field</param>
        /// <param name="sortDir"> sort direction</param>
        /// <param name="slim"> bool</param>
        public GetItems SetParameters(string perPage = "20", string page = "1", string sortBy = "name", string sortDir = "asc", string slim = "true")
            {
            if (perPage!=null)
                    this.jsonString = "&perPage=" + perPage;
                    this._perPage = perPage;
                if(page!=null)
                this.jsonString =  "&page=" + page;
                this._page = page;
            if (sortBy!=null)
                this.jsonString = "&sortBy=" + sortBy;
                this._sortBy = sortBy;
            if (sortDir != null)
                this.jsonString = "&sortDir=" + sortDir;
                this._sortDir = sortDir;
                if(slim!=null)
                this.jsonString = "&slim=" + slim;
                    this._slim = slim;
     

                return this;
            }
            /// <summary>
            /// Set Filter. 
            /// </summary>        
            /// <param name="name"> name of collection</param>
            /// <param name="collectionAddress"> collection Address</param>
            /// <param name="chainId"> chainId</param>
            /// <param name="creatorAddress"> creator Address</param>
            /// <param name="holders"> amount of holders</param>
            /// <param name="isVerified"> is Verified</param>
            /// <param name="supply"> supply</param>
            public GetItems filter(string name = null,string collection_address = null, string chainId= null, string creatorAddress = null,
            string holders = null, string isVerified= null, string supply = null)
            {
            if (name != null)
                this.jsonString = "&name=" + name;
            if (collection_address != null)
                this.jsonString = "&collection_address=" + collection_address;
            if (chainId != null)
                this.jsonString = "&chainId=" + chainId;
            if (creatorAddress != null)
                this.jsonString = "&creatorAddress=" + creatorAddress;

            if (holders != null)
                this.jsonString = "&holders=" + holders;
            if (isVerified != null)
                this.jsonString = "&isVerified=" + isVerified;
            if (supply != null)
                this.jsonString = "&supply=" + supply;

            return this;
        }

        public GetItems OnComplete(UnityAction<Items_model> action)
        {
            this.OnCompleteAction = action;
            return this;
        }
        
        /// <summary>
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> Information on Error as string text.</returns>
        public GetItems OnError(UnityAction<string> action)
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

                    WEB_URL = "https://api.alturanft.com/api/v2/item?" + jsonString;

                    
                    if(debugErrorLog)
                        Debug.Log("Querying Many Items: "  + " on " );
                return WEB_URL;
            }
            
            IEnumerator CallAPIProcess()
            {
                //Make request
                UnityWebRequest request = UnityWebRequest.Get(WEB_URL);
                request.SetRequestHeader("Content-Type", "application/json");                
            string url = "https://api.alturanft.com/api/sdk/unity/";
            WWWForm form = new WWWForm();
            UnityWebRequest www = UnityWebRequest.Post(url + "GetItems" + "?apiKey=" + _apiKey, form);
                {
                    yield return request.SendWebRequest();
                    string jsonResult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                    
                    if(debugLogRawApiResponse)
                        Debug.Log(jsonResult);

                    if (request.error != null)
                    {
                        if(OnErrorAction!=null)
                            OnErrorAction($"Null data {request.responseCode}. Result {jsonResult}");
                        if(debugErrorLog)
                            Debug.Log($"(Null data: {request.responseCode}. Result {jsonResult}");
                        if(afterError!=null)
                            afterError.Invoke();

                        item = null;
                        //yield break;
                    }
                    else
                    {
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
                            Debug.Log($"Response: Success , view Txns model" );
                    }
                }
                request.Dispose();
                if(destroyAtEnd)
                    Destroy (this.gameObject);
            }
            
        #endregion
    }

}
