using System;

namespace AlturaSDK.Exceptions
{
    /// <summary>
    /// An exception which is thrown when the provided data to the API was invalid.
    /// A detailed error description will be provided in the message
    /// </summary>
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException()
        {
        }

        public InvalidRequestException(string message) : base(message)
        {
        }

        public InvalidRequestException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}