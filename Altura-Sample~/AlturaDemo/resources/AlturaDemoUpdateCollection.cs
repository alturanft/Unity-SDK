namespace AlturaNFT.Samples.AlturaDemo
{

    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using AlturaNFT;

    using UnityEngine.UI;

    // get user details
    public class AlturaDemoUpdateCollection : MonoBehaviour
    {

        #region AlturaDemoUpdateCollection
        [SerializeField]
        public Button _button;
        [SerializeField]
        public InputField _collectionAddress;
        [SerializeField]
        public InputField _image;
        [SerializeField]
        public InputField _gnre;
        [SerializeField]
        public InputField _description;
        [SerializeField]
        public InputField _websiteURL;
        [SerializeField]
        public Text outputWindow;



        #endregion


        private void Start()
        {
            Button btn = _button.GetComponent<Button>();
            btn.onClick.AddListener(updateCollection);
        }

        public void updateCollection()
        {
            UpdateCollection
              .Initialize(destroyAtEnd: true)
              .SetParameters(
                address: _collectionAddress.text.ToString(),
                image: _image.text.ToString(),
                image_url: "",
                description: _description.text.ToString(),
                website: _websiteURL.text.ToString(),
                genre: _gnre.text.ToString()
              )
              .OnError(error => Debug.Log(error))
              .OnComplete(result => outputWindow.text = $"Collection is Updated! please check: https://app.alturanft.com/collection/{result.collection.chainId}/{result.collection.address}" )
              .Run();
        }

    }

}