namespace AlturaNFT.Samples.AlturaDemo{

    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using AlturaNFT;

    using UnityEngine.UI;

    // get user details
    public class AlturaDemoTransfer : MonoBehaviour
    {

        #region TransferItem

        public Button _transferButton;
        [SerializeField]
        public InputField _address;

        [SerializeField]
        public InputField _tokenID;

        [SerializeField]
        public InputField _toAddress;

        [SerializeField]
        public InputField _amount;
        [SerializeField]
        public Text outputWindow; 

       

    #endregion
  

        private void Start()
        {
            Button btn = _transferButton.GetComponent<Button>();
            btn.onClick.AddListener(TransferItem_Run);
        }

        public void TransferItem_Run()
        {
            TransferItem
                .Initialize(destroyAtEnd: true)
                .SetParameters(
                  collection_addr: _address.text.ToString(),
                  token_id: _tokenID.text.ToString(),
                  amount: _amount.text.ToString(),
                  to_addr: _toAddress.text.ToString()
                )
                .OnError(error => outputWindow.text = error)
                .OnComplete(result => outputWindow.text = result.txHash)
                .Run();

        }

    }

}