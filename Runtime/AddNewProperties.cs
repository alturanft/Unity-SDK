using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace AlturaNFT
{
    using Internal;

    /// <summary>
    /// Add new Properties API
    /// </summary>
    [AddComponentMenu(AlturaConstants.BaseComponentMenu + AlturaConstants.FeatureName_AddNewProperties)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_PENDING)]
    public class AddNewProperties : MonoBehaviour
    {

        #region Parameter Defines

        private string jsonString;
        private string _chainId;
        private string _address;
        private int _tokenId;
        private string _propertyName;
        private string _propertyValue;
        private string RequestUriInit = AlturaConstants.APILink + "/v2/item/add/property";
        private string WEB_URL;
        private WWWForm FORM;
        private string _apiKey;
        private bool destroyAtEnd = false;

        private UnityAction<string> OnErrorAction;
        private UnityAction OnCompleteAction;

        [Space(20)]
        //[Header("Called After Successful API call")]
        public UnityEvent afterSuccess;
        //[Header("Called After Error API call")]
        public UnityEvent afterError;

        [Header("Run Component when this Game Object is Set Active")]
        [SerializeField] private bool onEnable = false;
        public bool debugErrorLog = true;
        public bool debugLogRawApiResponse = true;


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
        public static AddNewProperties Initialize(bool destroyAtEnd = true)
        {
            var _this = new GameObject(AlturaConstants.FeatureName_AddNewProperties).AddComponent<AddNewProperties>();
            _this.destroyAtEnd = destroyAtEnd;
            _this.onEnable = false;
            _this.debugErrorLog = false;
            return _this;
        }

        /// <summary>
        /// Set Parameters to update item properties with
        /// </summary>
        /// <param name="apiKey"> your api key</param>
        /// <param name="chainId"> chain id of collection</param>
        /// <param name="address"> collection address</param>
        /// <param name="tokenId"> id of token you want to update</param>
        /// <param name="propertyName"> name of property you want to update</param>
        /// <param name="propertyValue"> value to update the property with</param>
        public AddNewProperties SetProperties(string apiKey = null, string chainId = null, string address = null, int tokenId = -1, string propertyName = null, string propertyValue = null)
        {
            if (apiKey != null)
                this._apiKey = apiKey;
            if (chainId != null)
                this._chainId = chainId;
            if (address != null)
                this._address = address;
            if (tokenId != -1)
                this._tokenId = tokenId;
            if (propertyName != null)
                this._propertyName = propertyName;
            if (propertyValue != null)
                this._propertyValue = propertyValue;
            return this;
        }
        public AddNewProperties OnComplete(UnityAction action)
        {
            this.OnCompleteAction = action;
            return this;
        }
        /// <summary>
        /// Action on Error (⊙.◎)
        /// </summary>
        /// <param name="UnityAction action"> string.</param>
        /// <returns> Information on Error as string text.</returns>
        public AddNewProperties OnError(UnityAction<string> action)
        {
            this.OnErrorAction = action;
            return this;
        }

        #endregion


        #region Run - API
        /// <summary>
        /// Runs the Api call and fills the corresponding model in the component on success.
        /// </summary>
        public void Run()
        {
            WEB_URL = BuildUrl();
            FORM = CreateForm();
            StopAllCoroutines();
            StartCoroutine(CallAPIProcess());            
        }

        string BuildUrl()
        {
            this.jsonString = "";
            if (this._apiKey != null)
                this.jsonString += "&apiKey=" + this._apiKey;


            WEB_URL = RequestUriInit + "?" + jsonString;
            Debug.Log("Abol: " + WEB_URL);
            if (debugErrorLog)
                Debug.Log("adding properties to item: ");

            return WEB_URL;
        }

        WWWForm CreateForm()
        {
            WWWForm form = new WWWForm();
            if (this._chainId != null)
                form.AddField("chainId", this._chainId);
            if(this._address != null)
                form.AddField("address", this._address);
            if (this._tokenId != -1)
                form.AddField("tokenId", this._tokenId);
            if (this._propertyName != null)
                form.AddField("propertyName", this._propertyName);
            if (this._propertyValue != null)
                form.AddField("propertyValue", this._propertyValue);
            return form;
        }

        IEnumerator CallAPIProcess()
        {
            //Make request
            UnityWebRequest request = UnityWebRequest.Post(WEB_URL, FORM);
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
                    //yield break;
                }
                else
                {
                    if (OnCompleteAction != null)
                        OnCompleteAction.Invoke();

                    if (afterSuccess != null)
                        afterSuccess.Invoke();

                    Debug.Log($"property added to item");
                }
            }
            request.Dispose();
            if (destroyAtEnd)
                DestroyImmediate(this.gameObject);
        }

        #endregion
    }

}
