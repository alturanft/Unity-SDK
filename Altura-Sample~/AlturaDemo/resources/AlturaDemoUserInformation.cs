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

        public Button _getdataButton;
        [SerializeField]
        public InputField _address;

        [SerializeField]
        public Text NameInfo;

        [SerializeField]
        public Text BioInfo;
        [SerializeField]
        public GameObject UserInformationObject;

        #endregion

        private void Start()
        {
            Button btn = _getdataButton.GetComponent<Button>();
            btn.onClick.AddListener(GetDataFunction);
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
        private void setData(Internal.User_model _result)
        {
            NameInfo.text = _result.user.name;
            BioInfo.text = _result.user.bio;
            UserInformationObject.SetActive(true);
        }

    }

}