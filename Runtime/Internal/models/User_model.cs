using System;
using System.Collections.Generic;

namespace AlturaNFT.Internal
{
        [Serializable]
        public class User_model
        {
            public string response;
            public Users user;
            public List<Users> users;

        }
        [Serializable]
        public class Users
        {
                public string address;
                public string name;
                public string bio;
                public string profilePic;
                public string socialLink;
                public string profilePicUrl;

        }
}