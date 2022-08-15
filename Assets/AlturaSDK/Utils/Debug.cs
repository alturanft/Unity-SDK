using System;

namespace AlturaSDK.Utils
{
    using Enum;
    using Settings;
    
    internal static class Debug
    {

        public static void LogError(object message)
        {
            UnityEngine.Debug.LogError("[Altura] " + message);
        }

        public static void LogAssertion(object message)
        {
            UnityEngine.Debug.LogAssertion("[Altura] " + message);
        }

        public static void LogException(Exception exception)
        {
            UnityEngine.Debug.LogException(exception);
        }
    }
}