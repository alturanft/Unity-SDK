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
            private string jsonString;

            private string RequestUriInit = AlturaConstants.APILink +"/v2/collection";
            private string WEB_URL;
            private string _apiKey;
            private bool destroyAtEnd = false;
            private string _isVerified;
            private string _holders;
            private string _chainId;
            private string _name;
            private string _address;
            private string _website;

            private UnityAction<string> OnErrorAction;
            private UnityAction<Collection_model> OnCompleteAction;
            
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
            public Collection_model collectionModel;


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
            if (perPage!=null)
                    this._perPage = perPage;
                if(page!=null)
                this._page = page;
            if (sortBy!=null)
                this._sortBy = sortBy;
            if (sortDir != null)
                this._sortDir = sortDir;

            return this;
            }
            /// <summary>
            /// Set Filter. 
            /// </summary>
            public GetCollections filter(string isVerified = null, string holders= null, string chainId = null, string name = null, string address = null, string website = null)
            {

            if (isVerified != null)
                this._isVerified = isVerified;
            if (holders != null)
                this._holders = holders;
            if (chainId != null)
                this._chainId = chainId;
            if (name != null)
                this._name = name;
            if (address != null)
                this._address = address;
            if (website != null)
                this._website = website;
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
                this.jsonString = "";
                if (this._perPage!=null)
                this.jsonString += "&perPage=" + this._perPage;
                if(this._page!=null)
                this.jsonString +=  "&page=" + this._page;
                if (this._sortBy!=null)
                this.jsonString += "&sortBy=" + this._sortBy;
                if (this._sortDir != null)
                this.jsonString += "&sortDir=" + this._sortDir;
                if (this._isVerified != null)
                this.jsonString += "&isVerified=" + this._isVerified;
                if (this._holders != null)
                this.jsonString += "&holders=" + this._holders;
                if (this._chainId != null)
                this.jsonString += "&chainId=" + this._chainId;
                if (this._name != null)
                this.jsonString += "&name=" + this._name;
                if (this._address != null)
                this.jsonString += "&address=" + this._address;
                if (this._website != null)
                this.jsonString += "&website=" + this._website;


                    WEB_URL = RequestUriInit + "?" + jsonString;
                    if(debugErrorLog)
                        Debug.Log("Querying Details of Collections: " );

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
                    DestroyImmediate(this.gameObject);
            }
            
        #endregion
    }

}
