namespace AlturaNFT.Samples.AlturaDemo{

    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using AlturaNFT;

    using UnityEngine.UI;

    // get user details
    public class GetUserDetails : MonoBehaviour
    {

        #region TransferItem

        [SerializeField]
        private string address;

        [SerializeField]
        private string tokenId;

        [SerializeField]
        private string to;

        [SerializeField]
        private string amount;
        [SerializeField] private Text outputWindow;

        
        public void TransferItem_Run()
        {
            TransferItem
                .Initialize(destroyAtEnd: true)
                .SetParameters(
                  collection_addr: address,
                  token_id: tokenId,
                  amount: amount,
                  to_addr: to
                )
                .OnError(error => Debug.Log(error))
                .OnComplete(result => Debug.Log(result))
                .Run();

        }

        void TransferItem_Output(Transfer_model transferModel)
        {
            Debug.Log("TransferItem_Output: " + transferModel.txHash);
            outputWindow.text = "";

            outputWindow.text += "TransferItem_Output: " + transferModel.txHash + "\n";
        }


    #endregion
  

        private void Start()
        {
            TransferItem_Run();
        }
    }

}