using System;

namespace AlturaSDK.Exceptions
{
    /// <summary>
    /// An exception which is thrown when calling API methods but the API is unresponsive or the client doesn't have an internet connection
    /// </summary>
    public class ApiNetworkException : Exception
    {
    }
}