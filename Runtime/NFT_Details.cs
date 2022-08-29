using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace AlturaNFT  
{ using Internal;
    
    /// <summary>
    /// Details of particular NFT
    /// </summary>
    [AddComponentMenu(AlturaConstants.BaseComponentMenu+AlturaConstants.FeatureName_NFT_Details)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_NFTDetails)]
    public class NFT_Details : MonoBehaviour
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
            private Chains chain = Chains.ethereum;
            
            [SerializeField]
          //  [DrawIf("chain", Chains.binance , DrawIfAttribute.DisablingType.DontDrawInverse)]
            private string _contract_address = "Input Contract Address of the NFT collection";
            
            [SerializeField]
          //  [DrawIf("chain", Chains.binance , DrawIfAttribute.DisablingType.DontDrawInverse)]
            [Tooltip("Token ID of the NFT")]
            private int _token_id = 1;
            

            private string RequestUriInit = "https://api.alturanft.com/api/v2/item/";
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
            [SerializeField] private bool onEnable = false;
            public bool debugErrorLog = true;
            public bool debugLogRawApiResponse = false;
            
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
        public static NFT_Details Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject(AlturaConstants.FeatureName_NFT_Details).AddComponent<NFT_Details>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = false;
                _this.debugErrorLog = false;
                return _this;
            }

        /// <summary>
        /// Set Parameters to retrieve NFT From.  ≧◔◡◔≦ .
        /// </summary>
        /// <param name="contract_address"> as string - EVM</param>
        /// <param name="token_id"> as int - EVM.</param>
        public NFT_Details SetParameters(string contract_address = null, int token_id = -1)
            {
                if(contract_address!=null)
                    this._contract_address = contract_address;
                if (token_id != -1)
                    _token_id = token_id;


                return this;
            }
            
            /// <summary>
            /// Blockchain from which to query NFTs.
            /// </summary>
            /// <param name="chain"> Choose from available 'Chains' enum</param>
            public NFT_Details SetChain(Chains chain)
            {
                this.chain = chain;
                return this;
            }

            /// <summary>
            /// Action on successful API Fetch. (*^∇^)ヾ(￣▽￣*)
            /// </summary>
            /// <param name="NFTs_OwnedByAnAccount_model.Root"> Use: .OnComplete(NFTs=> NFTsOfUser = NFTs) , where NFTsOfUser = NFTs_OwnedByAnAccount_model.Root;</param>
            /// <returns> NFTs_OwnedByAnAccount_model.Root </returns>
            public NFT_Details OnComplete(UnityAction<Items_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }
            
            /// <summary>
            /// Action on Error (⊙.◎)
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> Information on Error as string text.</returns>
            public NFT_Details OnError(UnityAction<string> action)
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
                if (chain == Chains.bsctest)
                {
                    WEB_URL = RequestUriInit + _contract_address + "/" + _token_id.ToString();
                    if(debugErrorLog)
                        Debug.Log("Querying Details of NFT: " + _contract_address + " on " + chain);
                }
                else
                {
                    WEB_URL = RequestUriInit + _contract_address + "/" + _token_id.ToString();
                    if(debugErrorLog)
                        Debug.Log("Querying Details of NFT: " + _contract_address + "  token ID  " + _token_id + " on " + chain);
                } 
                return WEB_URL;
            }
            
            IEnumerator CallAPIProcess()
            {
                //Make request
                UnityWebRequest request = UnityWebRequest.Get(WEB_URL);
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("source", AlturaUser.GetSource());
                request.SetRequestHeader("Authorization", _apiKey);
                

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
                            Debug.Log($"(⊙.◎) Null data. Response code: {request.responseCode}. Result {jsonResult}");
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
                            Debug.Log($" ´ ▽ ` )ﾉ Success , view NFT under NFTs model" );
                    }
                }
                request.Dispose();
                if(destroyAtEnd)
                    Destroy (this.gameObject);
            }
            
        #endregion
    }

}
