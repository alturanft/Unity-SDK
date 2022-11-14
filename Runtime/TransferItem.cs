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

    
    
    [AddComponentMenu(AlturaConstants.BaseComponentMenu+AlturaConstants.FeatureName_Transfer)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_Transfer)]
    public class TransferItem : MonoBehaviour
    {      

        #region Parameter Defines

            [SerializeField]
            private string _apiKey;

            [SerializeField]
            private string chainId;
  
            [SerializeField]
            private string address;
            [SerializeField]
            private string _token_id = "Input Which page you want to get";
            [SerializeField]
            private string _amount = "Input Sort By = name";
            [SerializeField]
            private string _to_addr = "Input Asc or Desc";


            private string WEB_URL;
            private bool destroyAtEnd = true;


            private UnityAction<string> OnErrorAction;
            private UnityAction<Transfer_model> OnCompleteAction;
            
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
            public Transfer_model txHash;


        #endregion


        private void Awake()
        {
            AlturaUser.Initialise();
            //apiKey = AlturaUser.GetUserApiKey();
            
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
        public static TransferItem Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject(AlturaConstants.FeatureName_Transfer).AddComponent<TransferItem>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = true;
                _this.debugErrorLog = true;
                return _this;
            }

        public TransferItem SetParameters(
            string apiKey, 
            string collection_addr, string token_id, string amount, string to_addr)
            {
                if(apiKey != null) 
                    this._apiKey = apiKey;
                if(collection_addr!=null)
                    this.address = collection_addr;
                if(token_id!=null)
                    this._token_id = token_id;
                if(amount!=null)
                    this._amount = amount;
                if(to_addr!=null)
                    this._to_addr = to_addr;
     

                return this;
            }


            public TransferItem OnComplete(UnityAction<Transfer_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }

            public TransferItem OnError(UnityAction<string> action)
            {
                this.OnErrorAction = action;
                return this;
            }
            
        #endregion

        
        #region Run - API

            public Transfer_model Run()
            {
                StopAllCoroutines();
                TransferOneReq tx = new TransferOneReq();
                tx.chainId = chainId;
                tx.address = address;
                tx.tokenId = _token_id;
                tx.amount = _amount;
                tx.to = _to_addr;
                var  jsonString = JsonUtility.ToJson(tx);

                StartCoroutine(Post("https://api.alturanft.com/api/v2/item/transfer?apiKey=" + _apiKey, jsonString));
                return txHash;
            }

            IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
       // request.SetRequestHeader("ALTURA_API_KEY", apiKey);
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
                                    txHash = JsonConvert.DeserializeObject<Transfer_model>(
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
                        
                            Debug.Log($" view Tx Hash" );
                                }

                                

            
        }
                 request.Dispose();
                if(destroyAtEnd)
                    Destroy (this.gameObject);
    }   
        #endregion
    }

}


