using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace AlturaNFT  
{ using Internal;
    
    /// <summary>
    /// Get Users Details API
    /// </summary>
    [AddComponentMenu(AlturaConstants.BaseComponentMenu+AlturaConstants.FeatureName_GetUsers)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_GetUsers)]
    public class GetUsers : MonoBehaviour
    {

        #region Parameter Defines
  
            [SerializeField]
            private string _perPage = "Input How much pages you want to get";
            [SerializeField]
            private string _page = "Input Which page you want to get";
            [SerializeField]
            private string _sortBy = "Input Sort By = name";
            [SerializeField]
            private string _sortDir = "Input Asc or Desc";

            private string jsonString;
            private string _address;
            private string _name;
            private string _bio;
            private string RequestUriInit = "https://api.alturanft.com/api/v2/user";
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

        /// <summary>
        /// Initialize creates a gameobject and assings this script as a component. This must be called if you are not refrencing the script any other way and it doesn't already exists in the scene.
        /// </summary>
        /// <param name="destroyAtEnd"> Optional bool parameter can set to false to avoid Spawned GameObject being destroyed after the Api process is complete. </param>
        public static GetUsers Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject(AlturaConstants.FeatureName_GetUsers).AddComponent<GetUsers>();
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
        public GetUsers SetParameters(string perPage = "20", string page = "1", string sortBy = "name", string sortDir = "asc")
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
            /// <param name="address"> address of the user</param>
        /// <param name="name"> user name</param>
        /// <param name="bio"> user bio</param>
            public GetUsers filter(string address = null, string name= null, string bio = null)
            {
            if (address != null)
                this._address = address;
            if (name != null)
                this._name = name;
            if (bio != null)
                this._bio = bio;
            return this;
        }
            public GetUsers OnComplete(UnityAction<User_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }
            /// <summary>
            /// Action on Error (⊙.◎)
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> Information on Error as string text.</returns>
            public GetUsers OnError(UnityAction<string> action)
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
                this.jsonString = "";
                if (this._perPage!=null)
                this.jsonString += "&perPage=" + this._perPage;
                if(this._page!=null)
                this.jsonString +=  "&page=" + this._page;
                if (this._sortBy!=null)
                this.jsonString += "&sortBy=" + this._sortBy;
                if (this._sortDir != null)
                this.jsonString += "&sortDir=" + this._sortDir;
                if (this._address != null)
                this.jsonString += "&address=" + this._address;
                if (this._name != null)
                this.jsonString += "&name=" + this._name;
                if (this._bio != null)
                this.jsonString += "&bio=" + this._bio;


                    WEB_URL = RequestUriInit + "?" + jsonString;
                        Debug.Log("Abol: " + WEB_URL);
                    if(debugErrorLog)
                        Debug.Log("Querying Details of User: " );

                return WEB_URL;
            }
            
            IEnumerator CallAPIProcess()
            {
                //Make request
                UnityWebRequest request = UnityWebRequest.Get(WEB_URL);
                request.SetRequestHeader("Content-Type", "application/json");                
            string url = "https://api.alturanft.com/api/sdk/unity/";
            WWWForm form = new WWWForm();
            UnityWebRequest www = UnityWebRequest.Post(url + "GetUsers" + "?apiKey=" + _apiKey, form);
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
                    Destroy (this.gameObject);
            }
            
        #endregion
    }

}
