using System;

namespace AlturaSDK.API.Responses
{
    /// <summary>
    /// tfetch all Altura Items
    /// </summary>
    [Serializable]
    public class GetItemResponse
    {
        public string access_token;
        public string refresh_token;
        public string id_token;
        public int expires_in;
        public string scope;
        public string token_type;
    }
}