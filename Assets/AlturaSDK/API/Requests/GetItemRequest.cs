namespace AlturaSDK.API.Requests
{
    using Utils;
    
    public class GetItemRequest : ApiRequest
    {
        public GetItemRequest()
        {
            Uri = Endpoints.Item;
        }
    }
}