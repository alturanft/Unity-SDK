using System;
using UnityEngine;

namespace UnitySDK.Assets.AlturaSDK.Models
{ 
    [Serializable]
    public class AlturaUser
    {
        public string address;

        /// <summary>
        /// meatadata
        /// </summary>
        public string name;
        public string bio;
        public string socialLink;

        /// <summary>
        /// image
        /// </summary>
        public string imageHash;
        public string profilePic;
        public string profilePicUrl;

        /// <summary>
        /// login & security
        /// </summary>
        public int nonce;
        public DateTime lastLogin;

        /// <summary>
        /// Altura Guard code
        /// </summary>
        public string authCode;

        /// <summary>
        /// Sys flags
        /// </summary>
        public bool admin;
        public bool agreeToTerms;
        public bool blacklisted;


    }
}


