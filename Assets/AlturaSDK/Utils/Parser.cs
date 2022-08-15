using System;
using AlturaSDK.API.Responses;
using AlturaSDK.Exceptions;
using UnityEngine;

namespace AlturaSDK.Utils
{
    internal static class Parser
    {
        public static bool ParseHttpError(string json, out Exception exception)
        {
            exception = null;
            ErrorResponse error = null;

            if (string.IsNullOrEmpty(json)) return false;

            if ((json.Contains("error") && json.Contains("error_description")) || json.Contains("message"))
            {
                error = JsonUtility.FromJson<ErrorResponse>(json);
            }

            if (error == null)
            {
                return false;
            }

            exception = ConvertErrorToException(error);

            return true;
        }

        private static Exception ConvertErrorToException(ErrorResponse error)
        {
            if (!string.IsNullOrEmpty(error.error))
            {
                var message = $"{error.error} - {error.error_description}";

                return error.error switch
                {
              //      "invalid_token" => new InvalidTokenException(message),
               //     "invalid_request" => new InvalidRequestException(message),
                    _ => new ApiHttpException(message)
                };
            }

            if (!string.IsNullOrEmpty(error.message))
            {
                return error.status switch
                {
                //    401 => new UnauthorizedException(error.message),
                    _ => new ApiHttpException(error.message)
                };
            }

            return new ApiHttpException("unknown_error");
        }

        public static CodeResponse ParseDeepLinkUrl(string url)
        {
            if (!url.Contains("altura"))
            {
                return null;
            }

            var response = new CodeResponse();
            var error = "";

            var value = url.Split("?"[0])[1];

            foreach (var pair in value.Split('&'))
            {
                var splitPair = pair.Split('=');

                switch (splitPair[0])
                {
                    case "code":
                        response.code = splitPair[1];
                        break;
                    case "state":
                        response.state = splitPair[1];
                        break;
                    case "error":
                        error += splitPair[1];
                        break;
                    case "error_description":
                        error += $" - {splitPair[1]}";
                        break;
                }
            }

            if (!string.IsNullOrEmpty(error))
            {
                return null;
                //AuthCodeException(error);
            }

            return response;
        }
    }
}