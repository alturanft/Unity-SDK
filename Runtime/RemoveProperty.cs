using System.Collections;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace AlturaNFT
{
    using Internal;
    using Utils;

    
    
    [AddComponentMenu(AlturaConstants.BaseComponentMenu+AlturaConstants.FeatureName_RemoveProperty)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_RemoveProperty)]
    public class RemoveProperty : MonoBehaviour
    {

        private string apiKey;

        #region Parameter Defines
            [SerializeField]
            private string _address;
            [SerializeField]
            private string _chainId;
        [SerializeField]
        private string _tokenId;
            [SerializeField]
            private string _propertyName = "The name (key) of the property you want to change";


            private string WEB_URL;
            private bool destroyAtEnd = false;


            private UnityAction<string> OnErrorAction;
            private UnityAction<Items_model> OnCompleteAction;
            
            [Space(20)]
            [Header("Called After Successful API call")]
            public UnityEvent afterSuccess;
            [Header("Called After Error API call")]
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
            apiKey = AlturaUser.GetUserApiKey();

            
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
        public static RemoveProperty Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject(AlturaConstants.FeatureName_Transfer).AddComponent<RemoveProperty>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = true;
                _this.debugErrorLog = true;
                return _this;
            }

        public RemoveProperty SetParameters(string address, string tokenId, string chainId, string propertyName)
            {
                if(address!=null) 
                    this._address = address;
                if(chainId!=null)
                    this._chainId = chainId;
                if(tokenId!=null)
                    this._tokenId = tokenId;
                if(propertyName!=null)
                    this._propertyName = propertyName;

                return this;
            }


            public RemoveProperty OnComplete(UnityAction<Items_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }

            public RemoveProperty OnError(UnityAction<string> action)
            {
                this.OnErrorAction = action;
                return this;
            }
            
        #endregion

        
        #region Run - API

            public Items_model Run()
            {
                StopAllCoroutines();
                RemovePropertyReq tx = new RemovePropertyReq();
                tx.chainId = _chainId;
                tx.address = _address;
                tx.tokenId = _tokenId;
                tx.propertyName = _propertyName;
                var  jsonString = JsonUtility.ToJson(tx);
                
              //  StartCoroutine(Post("https://api.alturanft.com/api/v2/item/update_property?apiKey=" + apiKey, jsonString));
                StartCoroutine(Post("http://localhost:5001/api/v2/item/delete/property?apiKey=" + apiKey, jsonString));
                return item;
            }

            IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");


        yield return request.SendWebRequest();
        {
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
                        item = null;
                        yield break;
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
                        
                            Debug.Log($" view Update Property" );

                                }

                                

            
        }
                 request.Dispose();
                if(destroyAtEnd)
                    DestroyImmediate(this.gameObject);
    }   
        #endregion
    }

}


