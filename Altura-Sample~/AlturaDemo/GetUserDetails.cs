namespace AlturaNFT.Samples.AlturaDemo{

    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using AlturaNFT;

    using UnityEngine.UI;

    // get user details
    public class GetUserDetails : MonoBehaviour
    {
        [SerializeField]
        GetUsers usersDetails;

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
    
        GetUsers.Chains GetChainFromDropDownSelection()
        {
            if (chainDropdown.value == 0)
                return GetUsers.Chains.ethereum;
            else if(chainDropdown.value == 1)
                return GetUsers.Chains.bsctest;
            else 
                return GetUsers.Chains.binance;
        }

        void PopulateChainDropDownList()
        {
            chainDropdown.options.Clear();
            string[] enumChains = Enum.GetNames(typeof(GetUsers.Chains));
            List<string> chainNames = new List<string>(enumChains);
            chainDropdown.AddOptions(chainNames);
        }

        #endregion

        private void Start()
        {
            PopulateChainDropDownList();
        }
    }

}