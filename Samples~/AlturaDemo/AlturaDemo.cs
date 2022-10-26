namespace AlturaNFT.Samples.AlturaDemo{
    
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using AlturaNFT;
    using UnityEngine.UI;

    public class AlturaDemo : MonoBehaviour
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
                .SetChain(GetChainFromDropDownSelection())
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

        private void Start()
        {
            PopulateChainDropDownList();
        }
    }
}