using System;

namespace AlturaSDK.Exceptions
{
    /// <summary>
    /// A generic exception which is thrown when an unknown API error has happened
    /// </summary>
    public class ApiHttpException : Exception
    {
        public ApiHttpException()
        {
        }

        public ApiHttpException(string message) : base(message)
        {
        }

        public ApiHttpException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}