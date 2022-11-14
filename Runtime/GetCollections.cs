using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace AlturaNFT  
{ using Internal;
    
    /// <summary>
    /// Get Many Collections
    /// </summary>
    [AddComponentMenu(AlturaConstants.BaseComponentMenu+AlturaConstants.FeatureName_GetCollections)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_GetCollections)]
     public class GetCollections : MonoBehaviour
    {

        #region Parameter Defines
              
            [SerializeField]
            private string _perPage = "Input How much pqges you want to get";
            [SerializeField]
            private string _page = "Input Which page you want to get";
            [SerializeField]
            private string _sortBy = "Input Sort By = name";
            [SerializeField]
            private string _sortDir = "Input Asc or Desc";

            [Header("Optional: Filter and fetch items with specified property")]

            [SerializeField]
            [Tooltip("Filter from a documents by any properties")]
            string _isVerified;
            private string RequestUriInit = "https://api.alturanft.com/api/v2/collection";
            private string WEB_URL;
            private string _apiKey;
            private bool destroyAtEnd = false;


            private UnityAction<string> OnErrorAction;
            private UnityAction<Collection_model> OnCompleteAction;
            
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
            public Collection_model collectionModel;


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
        public static GetCollections Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject(AlturaConstants.FeatureName_GetCollections).AddComponent<GetCollections>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = false;
                _this.debugErrorLog = false;
                return _this;
            }

        /// <summary>
        /// Set Parameters to retrieve User From .
        /// </summary>
        /// <param name="perPage"> amount of pages to query</param>
        /// <param name="page"> page to query</param>
        /// <param name="sortBy"> sort by field</param>
        /// <param name="sortDir"> sort direction</param>
        public GetCollections SetParameters(string perPage = "20", string page = "1", string sortBy = "name", string sortDir = "asc")
            {

                if(perPage!=null)
                    this._perPage = perPage;
                if(page!=null)
                    this._page = page;
                if(sortBy!=null)
                    this._sortBy = sortBy;
                if(sortDir!=null)
                    this._sortDir = sortDir;
     

                return this;
            }

            /// <summary>
            /// Set Filter by to return NFTs only from the given contract address/collection. 
            /// </summary>
            ///<param name="name"> as string.</param>
            public GetCollections AlturaOptions(string isVerified)
            {
                this._isVerified = isVerified;
                return this;
            }


            public GetCollections OnComplete(UnityAction<Collection_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }
            
            /// <summary>
            /// Action on Error
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> Information on Error as string text.</returns>
            public GetCollections OnError(UnityAction<string> action)
            {
                this.OnErrorAction = action;
                return this;
            }
            
        #endregion

        
        #region Run - API
            /// <summary>
            /// Runs the Api call and fills the corresponding model in the component on success.
            /// </summary>
            public Collection_model Run()
            {
                WEB_URL = BuildUrl();
                StopAllCoroutines();
                StartCoroutine(CallAPIProcess());
                return collectionModel;
            }

            string BuildUrl()
            {

                    WEB_URL = RequestUriInit + "?perPage=" + _perPage + "&page=" + _page + "&sortBy=" + _sortBy + "&sortDir=" + _sortDir + "&isVerified=" + _isVerified;
                    if(debugErrorLog)
                        Debug.Log("Querying Details of User: " );

                return WEB_URL;
            }
            
            IEnumerator CallAPIProcess()
            {
                //Make request
                UnityWebRequest request = UnityWebRequest.Get(WEB_URL);
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("source", AlturaUser.GetSource());
                

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
                        collectionModel = null;
                        //yield break;
                    }
                    else
                    {
                        collectionModel = JsonConvert.DeserializeObject<Collection_model>(
                            jsonResult,
                            new JsonSerializerSettings
                            {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                            });
                        
                        if(OnCompleteAction!=null)
                            OnCompleteAction.Invoke(collectionModel);
                        
                        if(afterSuccess!=null)
                            afterSuccess.Invoke();
                        
                            Debug.Log($"Get Many Collection Models" );
                    }
                }
                request.Dispose();
                if(destroyAtEnd)
                    Destroy (this.gameObject);
            }
            
        #endregion
    }

}
