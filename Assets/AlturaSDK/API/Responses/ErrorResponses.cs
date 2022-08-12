using System;

namespace AlturaSDK.API.Responses
{
    /// <summary>
    /// A generic error response which will be parsed to an Exception by the SDK
    /// </summary>
    [Serializable]
    public class ErrorResponse
    {
        public string error;
        public string error_description;
        public string error_detail;

        public int status;
        public string message;
    }
}