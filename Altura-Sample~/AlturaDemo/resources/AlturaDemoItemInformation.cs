namespace AlturaNFT.Samples.AlturaDemo
{
    using UnityEngine;
    using AlturaNFT;
    using UnityEngine.Events;
    using UnityEngine.UI;

    // get user details
    public class AlturaDemoItemInformation : MonoBehaviour
    {

        #region ItemInformation

        public Button _getdataButton;
        [SerializeField]
        public InputField _collectionaddress;

        [SerializeField]
        public InputField _tokenID;

        [SerializeField]
        public Text console;

        #endregion

        private void Start()
        {
            Button btn = _getdataButton.GetComponent<Button>();
            btn.onClick.AddListener(GetDataFunction);
        }

        public void GetDataFunction()
        {

            int number; int.TryParse(_tokenID.text, out int result); number = result;
            Debug.Log("Starting Dropdown Value : " + _collectionaddress.text.ToString());
            Debug.Log("Starting Dropdown Value : " + number);
            GetItem
              .Initialize(destroyAtEnd: true)
              .SetParameters(
                collection_address: _collectionaddress.text.ToString(),
                token_id: number
              ).OnError(error => Debug.Log(error))
              .OnComplete(result => setData(result)
              )
              .Run();

        }
        private void setData(Internal.Items_model _result)
        {
            console.text = "Please check the console log!";
        }

    }

}