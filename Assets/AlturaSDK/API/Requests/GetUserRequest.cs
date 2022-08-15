namespace AlturaSDK.API.Requests
{
    using Utils;

    public class GetUserRequest : ApiRequest
    {
        public GetUserRequest()
        {
            Uri = Endpoints.User;
        }
    }
}