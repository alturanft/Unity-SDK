using System;

namespace AlturaSDK.API.Responses
{
    /// <summary>
    /// Authorization code response which holds the code and state provided by the Identity Service
    /// </summary>
    [Serializable]
    public class CodeResponse
    {
        public string code;
        public string state;
    }
}