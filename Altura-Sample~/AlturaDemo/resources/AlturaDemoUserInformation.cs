namespace AlturaNFT.Samples.AlturaDemo
{
    using UnityEngine;
    using AlturaNFT;
    using UnityEngine.Events;
    using UnityEngine.UI;

    // get user details
    public class AlturaDemoUserInformation : MonoBehaviour
    {

        #region ItemInformation
        public Button _itemBalanceBtn;
        [SerializeField]
        public InputField _useraddressItem;
        [SerializeField]
        public InputField _chainidItem;
        [SerializeField]
        public InputField _tokenIdItem;
        [SerializeField]
        public InputField _collectionaAddressItem;
       

        public Button _getUserErc20Balance;
        [SerializeField]
        public InputField _addressErc20Balance;
        [SerializeField]
        public InputField _chainIDErc20Balance;
        [SerializeField]
        public InputField _tokenaddressErc20;


        public Button _getUserBalance;
        [SerializeField]
        public InputField _addressBalance;
        [SerializeField]
        public InputField _chainIDBalance;

        public Button _getItemHistory;
        public InputField collectionaddressHistory;
        [SerializeField]
        public InputField tokenIdHistory;

        public Button _getItemHolder;
        public InputField collectionaddressHolder;
        [SerializeField]
        public InputField tokenIdHolder;

        public Button _getManyCollectionData;
        public Button _getCollectionData;
        [SerializeField]
        public InputField _collectionaddressData;
        public Button _getMultipleItemData;
        public Button _getItemData;
        public Button _getMultipleUser;
        public Button _getUserItemdataButton;
        public Button _getdataButton;
        [SerializeField]
        public InputField _address;
        [SerializeField]
        public InputField _Itemaddress;
        [SerializeField]
        public InputField collectionaddress;
        [SerializeField]
        public InputField tokenId;
        [SerializeField]
        public Text informationBox;

        [SerializeField]
        public GameObject UserInformationObject;

        #endregion

        private void Start()
        {
            Button btn = _getdataButton.GetComponent<Button>();
            btn.onClick.AddListener(GetDataFunction);

            Button btn1 = _getMultipleUser.GetComponent<Button>();
            btn1.onClick.AddListener(getMultipleUser);


            Button btn2 = _getUserItemdataButton.GetComponent<Button>();
            btn2.onClick.AddListener(GetUsersItemFunction);

            Button btn3 = _getItemData.GetComponent<Button>();
            btn3.onClick.AddListener(GetItemData);

            
            Button btn4 = _getMultipleItemData.GetComponent<Button>();
            btn4.onClick.AddListener(GetMultipleItemData);

            Button btn5 = _getCollectionData.GetComponent<Button>();
            btn5.onClick.AddListener(GetCollectionData);

            
            Button btn6 = _getManyCollectionData.GetComponent<Button>();
            btn6.onClick.AddListener(GetMulCollection);

            Button btn7 = _getItemHolder.GetComponent<Button>();
            btn7.onClick.AddListener(GetItemHolderData);


            Button btn8 = _getItemHistory.GetComponent<Button>();
            btn8.onClick.AddListener(GetItemHistory);

            Button btn9 = _getUserBalance.GetComponent<Button>();
            btn9.onClick.AddListener(GetBalance);

            Button btn10 = _getUserErc20Balance.GetComponent<Button>();
            btn10.onClick.AddListener(GetErc20Balance);

            
            Button btn11 = _itemBalanceBtn.GetComponent<Button>();
            btn11.onClick.AddListener(GetItembalance);
        }

        public void GetItembalance()
        {

            int intValue;
            int.TryParse(_tokenIdItem.text, out intValue);
            int intValue2;
            int.TryParse(_chainidItem.text, out intValue2);
            GetUserItemBalance
              .Initialize(destroyAtEnd: true)
              .SetParameters(
                _useraddressItem.text.ToString(),
                intValue,
                intValue2,
                _collectionaAddressItem.text.ToString()
              ).OnError(error => Debug.Log(error))
              .OnComplete(result => setItemBalance(result)
              )
              .Run();

        }
        private void setItemBalance(Reponse_owner_model _result)
        {
            string json = JsonUtility.ToJson(_result);
            informationBox.text = json.ToString();
            UserInformationObject.SetActive(true);
        }
        public void GetErc20Balance()
        {

            int intValue;
            int.TryParse(_chainIDErc20Balance.text, out intValue);
            GetUserERC20Balance
              .Initialize(destroyAtEnd: true)
              .SetParameters(
                _addressErc20Balance.text.ToString(),
                intValue,
                _tokenaddressErc20.text
              ).OnError(error => Debug.Log(error))
              .OnComplete(result => setBalanceText(result)
              )
              .Run();

        }

        public void GetBalance()
        {

            int intValue;
            int.TryParse(_chainIDBalance.text, out intValue);
            GetUserBalance
              .Initialize(destroyAtEnd: true)
              .SetParameters(
                _addressBalance.text.ToString(),
                intValue
              ).OnError(error => Debug.Log(error))
              .OnComplete(result => setBalanceText(result)
              )
              .Run();

        }
        private void setBalanceText(Reponse_Balance_model _result)
        {
            string json = JsonUtility.ToJson(_result);
            informationBox.text = json.ToString();
            UserInformationObject.SetActive(true);
        }
        public void GetItemHistory()
        {

            int intValue;
            int.TryParse(tokenIdHistory.text, out intValue);
            GetHistory
              .Initialize(destroyAtEnd: true)
              .SetParameters(
                collectionaddressHistory.text.ToString(),
                intValue
              ).OnError(error => Debug.Log(error))
              .OnComplete(result => setHistorys(result)
              )
              .Run();

        }

        public void GetItemHolderData()
        {

            int intValue;
            int.TryParse(tokenIdHolder.text, out intValue);
            GetHolder
              .Initialize(destroyAtEnd: true)
              .SetParameters(
                collectionaddressHolder.text.ToString(),
                intValue
              ).OnError(error => Debug.Log(error))
              .OnComplete(result => setHolders(result)
              )
              .Run();

        }

        public void GetMulCollection()
        {

            int intValue;
            int.TryParse(tokenId.text, out intValue);
            GetCollections
              .Initialize(destroyAtEnd: true)
              .SetParameters(
              ).OnError(error => Debug.Log(error))
              .OnComplete(result => setCollectionData(result)
              )
              .Run();

        }

        public void GetCollectionData()
        {

            int intValue;
            int.TryParse(tokenId.text, out intValue);
            GetCollection
              .Initialize(destroyAtEnd: true)
              .SetParameters(
                _collectionaddressData.text
              ).OnError(error => Debug.Log(error))
              .OnComplete(result => setCollectionData(result)
              )
              .Run();

        }
        public void GetMultipleItemData()
        {

            int intValue;
            int.TryParse(tokenId.text, out intValue);
            GetItems
              .Initialize(destroyAtEnd: true)
              .SetParameters(
              ).OnError(error => Debug.Log(error))
              .OnComplete(result => setItemData(result)
              )
              .Run();

        }

        public void GetItemData()
        {

            int intValue;
            int.TryParse(tokenId.text, out intValue);
            GetItem
              .Initialize(destroyAtEnd: true)
              .SetParameters(
                collectionaddress.text.ToString(),
                intValue
              ).OnError(error => Debug.Log(error))
              .OnComplete(result => setItemData(result)
              )
              .Run();

        }
        public void getMultipleUser()
        {

            GetUsers
              .Initialize(destroyAtEnd: true)
              .SetParameters().OnError(error => Debug.Log(error))
              .OnComplete(result => setData(result)
              )
              .Run();

        }

        public void GetUsersItemFunction()
        {

            GetUsersItems
              .Initialize(destroyAtEnd: true)
              .SetAddress(
                _Itemaddress.text.ToString()
              ).OnError(error => Debug.Log(error))
              .OnComplete(result => setItemData(result)
              )
              .Run();

        }
        public void GetDataFunction()
        {

            GetUser
              .Initialize(destroyAtEnd: true)
              .SetParameters(
                _address.text.ToString()
              ).OnError(error => Debug.Log(error))
              .OnComplete(result => setData(result)
              )
              .Run();

        }

        private void setHistorys(History_model _result)
        {
            string json = JsonUtility.ToJson(_result);
            informationBox.text = json.ToString();
            UserInformationObject.SetActive(true);
        }
        private void setHolders(Holders_model _result)
        {
            string json = JsonUtility.ToJson(_result);
            informationBox.text = json.ToString();
            UserInformationObject.SetActive(true);
        }

        private void setCollectionData(Collection_model _result)
        {
            string json = JsonUtility.ToJson(_result);
            informationBox.text = json.ToString();
            UserInformationObject.SetActive(true);
        }


        private void setItemData(Internal.Items_model _result)
        {
            string json = JsonUtility.ToJson(_result);
            informationBox.text = json.ToString();
            UserInformationObject.SetActive(true);
        }

        private void setData(Internal.User_model _result)
        {
            string json = JsonUtility.ToJson(_result);
            informationBox.text = json.ToString();
            UserInformationObject.SetActive(true);
        }

    }

}