namespace AlturaNFT.Samples.AlturaDemo{
    
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using AlturaNFT;
    using UnityEngine.UI;
    using AlturaWeb3;

    public class AlturaDemo : MonoBehaviour
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




        private void Start()
        {
        }
    }

}