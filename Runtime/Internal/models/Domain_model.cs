using System;
using System.Collections.Generic;

namespace AlturaNFT.Internal
{
    [Serializable]
    public class UserFromDomain_model
    {

        public bool success;
        public UserFromDomainBody body;

    }
    [Serializable]
    public class UserFromDomainBody
    {

        public string address;

    }    
    [Serializable]
    public class UsersDomainNames_model
    {
        public bool success;
        public Dictionary<string, string> body;
    }
}