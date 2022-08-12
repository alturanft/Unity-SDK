using System;

namespace AlturaSDK.Utils
{
    using Enum;
    using Settings;
    
    internal static class Debug
    {
        public static void Log(object message)
        {
            if (AlturaSettings.Instance.logLevel != LogLevel.Info) return;

            UnityEngine.Debug.Log("[Altura] " + message);
        }

        public static void LogWarning(object message)
        {
            if (AlturaSettings.Instance.logLevel > LogLevel.Warning) return;

            UnityEngine.Debug.LogWarning("[Altura] " + message);
        }

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