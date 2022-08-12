using System;

namespace AlturaSDK.API.Responses
{
    [Serializable]
    public class GetUserResponse
    {
        [Serializable]
        public struct UserItem
        {
            public string id;
            public string email;
            public string username;
            public string avatar;
            public string bio;
            public string twitter;
            public string discord;
            public string telegram;
            public string created_at;
        }

        public UserItem item;
    }
}