using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;


namespace AlturaNFT  
{ using Internal;
    

    [AddComponentMenu(AlturaConstants.BaseComponentMenu+AlturaConstants.FeatureName_GetUsersItems)]
    [ExecuteAlways]
    [HelpURL(AlturaConstants.Docs_GetUsersItems)]
    public class GetUsersItems : MonoBehaviour
    {

        #region Parameter Defines

            
            [SerializeField]
            private string address = "Input Account Address To Fetch NFT's from";
            private string jsonString;
            private string WEB_URL;
            private string _apiKey;
            private bool destroyAtEnd = false;
            private string _collectionAddress;
            private string _creatorAddress;
            private string _chainId;
            private string _name;
            private string _fileType;
            private string _holders;
            private string _isVerified;
            private string _page;
            private string _perPage;
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
        public static GetUsersItems Initialize(bool destroyAtEnd = true)
            {
                var _this = new GameObject("NFTs Of Account").AddComponent<GetUsersItems>();
                _this.destroyAtEnd = destroyAtEnd;
                _this.onEnable = false;
                _this.debugErrorLog = false;
                return _this;
            }
            
            /// <summary>
            /// Set Account Address to retrieve NFTs from as string
            /// </summary>
            /// <param name="account_address"> as string.</param>
            public GetUsersItems SetAddress(string account_address)
            {
                this.address = account_address;
                return this;
            }
            /// <summary>
            /// Set Filter. 
            /// </summary>        
            /// <param name="name"> name of collection</param>
            /// <param name="collectionAddress"> collection Address</param>
            /// <param name="chainId"> chainId</param>
            /// <param name="creatorAddress"> creator Address</param>
            /// <param name="fileType"> file Type</param>
            /// <param name="isVerified"> is Verified</param>
            /// <param name="page"> page number for pagination</param>
            /// <param name="perPage"> number of items per page</param>
            public GetUsersItems filter(string name = null, string collectionAddress = null, string chainId = null, string creatorAddress = null, string fileType = null, string isVerified = null, string page = null, string perPage = null)
            {
                if (name != null)
                    this._name = name;
                if (collectionAddress != null)
                    this._collectionAddress = collectionAddress;
                if (chainId != null)
                    this._chainId = chainId;
                if (creatorAddress != null)
                    this._creatorAddress = creatorAddress;
                if (fileType != null)
                    this._fileType = fileType;
                if (isVerified != null)
                    this._isVerified = isVerified;
                if (page != null)
                    this._page = page;
                if (perPage != null)
                    this._perPage = perPage;
                return this;
            }


            public GetUsersItems OnComplete(UnityAction<Items_model> action)
            {
                this.OnCompleteAction = action;
                return this;
            }
            
            /// <summary>
            /// Action on Error
            /// </summary>
            /// <param name="UnityAction action"> string.</param>
            /// <returns> Information on Error as string text.</returns>
            public GetUsersItems OnError(UnityAction<string> action)
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
                this.jsonString = "";
                if (this._name != null)
                    this.jsonString += "&name=" + this._name;
                if (this._collectionAddress != null)
                    this.jsonString += "&collectionAddress=" + this._collectionAddress;
                if (this._chainId != null)
                    this.jsonString += "&chainId=" + this._chainId;
                if (this._creatorAddress != null)
                    this.jsonString += "&creatorAddress=" + this._creatorAddress;
                if (this._fileType != null)
                    this.jsonString += "&fileType=" + this._fileType;
                if (this._isVerified != null)
                    this.jsonString += "&isVerified=" + this._isVerified;
                if (this._page != null)
                    this.jsonString += "&page=" + this._page;
                if (this._perPage != null)
                    this.jsonString += "&perPage=" + this._perPage;

                WEB_URL = AlturaConstants.APILink + "/v2/user/items/" + address + "?" + jsonString;
           
                
                if (debugErrorLog)
                {
                    var s = "Querying NFTs owned of Account: " + address + " on " ;
                    if (item.item.collectionAddress != "")
                        s += " / Filter from collection: " + item.item.collectionAddress;
                    Debug.Log(s);

                }
                
                return WEB_URL;
            }
            
            IEnumerator CallAPIProcess()
            {
                //Make request
                UnityWebRequest request = UnityWebRequest.Get(WEB_URL);
                request.SetRequestHeader("Content-Type", "application/json");
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
                            Debug.Log($" (⊙.◎) Null data. Response code: {request.responseCode}. Result {jsonResult}");
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
                            Debug.Log("Success , view NFTs model" );
                    }
                }
                request.Dispose();
                if(destroyAtEnd)
                    DestroyImmediate(this.gameObject);
            }
            
        #endregion
    }

}
