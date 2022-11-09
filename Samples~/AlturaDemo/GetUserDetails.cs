namespace AlturaNFT.Samples.AlturaDemo{

    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using AlturaNFT;
    using AlturaWeb3;

    using UnityEngine.UI;

    // get user details
    public class GetUserDetails : MonoBehaviour
    {
        [SerializeField]
        Users_Details usersDetails;

        [SerializeField] 
        private Dropdown chainDropdown;

        [SerializeField] private Text accountAddressText;
        [SerializeField] private Text contractFilter;
        [SerializeField] private Text outputWindow;
        
        public void UsersDetails_Run()
        {
            usersDetails
                .Run();
        }

        #region Chain Dropdown Address
    
        Users_Details.Chains GetChainFromDropDownSelection()
        {
            if (chainDropdown.value == 0)
                return Users_Details.Chains.ethereum;
            else if(chainDropdown.value == 1)
                return Users_Details.Chains.bsctest;
            else 
                return Users_Details.Chains.binance;
        }

        void PopulateChainDropDownList()
        {
            chainDropdown.options.Clear();
            string[] enumChains = Enum.GetNames(typeof(Users_Details.Chains));
            List<string> chainNames = new List<string>(enumChains);
            chainDropdown.AddOptions(chainNames);
        }

        #endregion

        private async void Start()
        {
            //var verify = await AlturaWeb3.AuthenticateUser();
            PopulateChainDropDownList();
        }
    }

}