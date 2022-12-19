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

    
    
    [AddComponentMenu(AlturaConstants.BaseComponentMenu+AlturaConstants.FeatureName_UpdateCollection)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_UpdateCollection)]
    public class UpdateCollection : MonoBehaviour
    {

        private string apiKey;
        #region Parameter Defines

            [SerializeField]
            private string _address;


            [SerializeField]
            private string _image;
            [SerializeField]
            private string _image_url = "Input image URL";
            [SerializeField]
            private string _description;
            [SerializeField]
            private string _website;
            [SerializeField]
            private string _genre;


            private string WEB_URL;
            private bool destroyAtEnd = true;


            private UnityAction<string> OnErrorAction;
            private UnityAction<Collection_model> OnCompleteAction;
            
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
            public Collection_model txHash;


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
        public static UpdateCollection Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject(AlturaConstants.FeatureName_Transfer).AddComponent<UpdateCollection>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = true;
                _this.debugErrorLog = true;
                return _this;
            }

        public UpdateCollection SetParameters(string address, string image, string image_url, string description, string website, string genre)
        {
                if(address != null) 
                    this._address = address;
                if(image!=null)
                    this._image = image;
                if(image_url!=null)
                    this._image_url = image_url;
                if(description!=null)
                    this._description = description;
                if(website!=null)
                    this._website = website;
                if(genre!=null)
                    this._genre = genre;
                return this;
            }


            public UpdateCollection OnComplete(UnityAction<Collection_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }

            public UpdateCollection OnError(UnityAction<string> action)
            {
                this.OnErrorAction = action;
                return this;
            }
            
        #endregion

        
        #region Run - API

            public Collection_model Run()
            {
                StopAllCoroutines();
                UpdateCollectionReq tx = new UpdateCollectionReq();
                tx.address = _address;
                tx.image = _image;
                tx.image_url = _image_url;
                tx.description = _description;
                tx.website = _website;
                tx.genre = _genre;
                var  jsonString = JsonUtility.ToJson(tx);

                StartCoroutine(Post("https://api.alturanft.com/api/v2/collection/update?apiKey=" + apiKey, jsonString));

                return txHash;
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
                        txHash = null;
                        yield break;
                                }
                                else
                                {
                                    txHash = JsonConvert.DeserializeObject<Collection_model>(
                            jsonResult,
                            new JsonSerializerSettings
                            {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                            });
                        
                        if(OnCompleteAction!=null)
                            OnCompleteAction.Invoke(txHash);
                        
                        if(afterSuccess!=null)
                            afterSuccess.Invoke();
                        
                            Debug.Log($" view Updated Collections" );

                                }
        
                                

            
        }
                 request.Dispose();
                if(destroyAtEnd)
                    DestroyImmediate(this.gameObject);
    }   
        #endregion
    }
}

