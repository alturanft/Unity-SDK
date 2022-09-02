using System.Collections;
using System.IO;
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
        /// <summary>
        /// Currently Supported chains for this endpoint.
        /// </summary>
        public enum Chains
        {
            ethereum,
            binance,
            bsctest,
            rinkeby,
        }

        #region Parameter Defines

            [SerializeField]
            private Chains chain = Chains.bsctest;
                      [SerializeField]
            [DrawIf("chain", Chains.bsctest , DrawIfAttribute.DisablingType.DontDrawInverse)]
            private string _apiKey;
  
            [SerializeField]
            [DrawIf("chain", Chains.bsctest , DrawIfAttribute.DisablingType.DontDrawInverse)]
            private string _collection_addr;
            [SerializeField]
            [DrawIf("chain", Chains.bsctest , DrawIfAttribute.DisablingType.DontDrawInverse)]
            private string _token_id = "Input Which page you want to get";
            [SerializeField]
            [DrawIf("chain", Chains.bsctest , DrawIfAttribute.DisablingType.DontDrawInverse)]
            private string _amount = "Input Sort By = name";
                        [SerializeField]
            [DrawIf("chain", Chains.bsctest , DrawIfAttribute.DisablingType.DontDrawInverse)]
            private string _to_addr = "Input Asc or Desc";


            private string RequestUriInit = "https://api.alturanft.com/api/v2/item/transfer";
            private string WEB_URL;
            private bool destroyAtEnd = false;


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
        public static TransferItem Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject(AlturaConstants.FeatureName_Transfer).AddComponent<TransferItem>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = false;
                _this.debugErrorLog = false;
                return _this;
            }

        /// <summary>
        /// Set Parameters to retrieve User From.  ≧◔◡◔≦ .
        /// </summary>
        /// <param name="perPage"> amount of pages to query</param>
        /// <param name="page"> page to query</param>
        /// <param name="sortBy"> sort by field</param>
        /// <param name="sortDir"> sort direction</param>
        public TransferItem SetParameters(string apiKey, string collection_addr, string token_id, string amount, string to_addr)
            {
                if(apiKey != null)
                {
                    _apiKey = apiKey;
                }
                if(collection_addr!=null)
                    this._collection_addr = collection_addr;
                if(token_id!=null)
                    this._token_id = token_id;
                if(amount!=null)
                    this._amount = amount;
                if(to_addr!=null)
                    this._to_addr = to_addr;
     

                return this;
            }


            /// <summary>
            /// Blockchain from which to query NFTs.
            /// </summary>
            /// <param name="chain"> Choose from available 'Chains' enum</param>
            public TransferItem SetChain(Chains chain)
            {
                this.chain = chain;
                return this;
            }

            /// <summary>
            /// Action on successful API Fetch. (*^∇^)ヾ(￣▽￣*)
            /// </summary>
            /// <param name="NFTs_OwnedByAnAccount_model.Root"> Use: .OnComplete(NFTs=> NFTsOfUser = NFTs) , where NFTsOfUser = NFTs_OwnedByAnAccount_model.Root;</param>
            /// <returns> NFTs_OwnedByAnAccount_model.Root </returns>
            public TransferItem OnComplete(UnityAction<Transfer_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }
            
            /// <summary>
            /// Action on Error (⊙.◎)
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> Information on Error as string text.</returns>
            public TransferItem OnError(UnityAction<string> action)
            {
                this.OnErrorAction = action;
                return this;
            }
            
        #endregion

        
        #region Run - API
            /// <summary>
            /// Runs the Api call and fills the corresponding model in the component on success.
            /// </summary>
            public Transfer_model Run()
            {
                WEB_URL = BuildUrl();
                StopAllCoroutines();
                StartCoroutine(CallAPIProcess());
                return txHash;
            }

            string BuildUrl()
            {
                if (chain == Chains.bsctest)
                {
                    WEB_URL = "https://api.alturanft.com/api/v2/item/transfer" + "?apiKey=" + _apiKey + "&collection_address=" + _collection_addr + "&token_id=" + _token_id + "&amount=" + _amount + "&to=" + _to_addr;
                    if(debugErrorLog)
                        Debug.Log("Transferring Item o: "  + " on " + chain);
                }
                else
                {
                    WEB_URL = "https://api.alturanft.com/api/v2/item/transfer" + "?apiKey=" + _apiKey + "&collection_address=" + _collection_addr + "&token_id=" + _token_id + "&amount=" + _amount + "&to=" + _to_addr;
                    if(debugErrorLog)
                        Debug.Log("Transferring Item: " +  " on " + chain);
                } 
                return WEB_URL;
            }
            
            IEnumerator CallAPIProcess()
            {


                WWWForm form = new WWWForm();
                UnityWebRequest request = UnityWebRequest.Post(RequestUriInit, "");
              request.SetRequestHeader("Authorization", "Bearer " + _apiKey);
                

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
                            Debug.Log($"(⊙.◎) Null data. Response code: {request.responseCode}. Result {jsonResult}");
                        if(afterError!=null)
                            afterError.Invoke();
                        txHash = null;
                        //yield break;
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
                        
                            Debug.Log($" ´ ▽ ` )ﾉ Success , view User under User model" );
                    }
                }
                request.Dispose();
                if(destroyAtEnd)
                    Destroy (this.gameObject);
            }
            
        #endregion
    }

}

