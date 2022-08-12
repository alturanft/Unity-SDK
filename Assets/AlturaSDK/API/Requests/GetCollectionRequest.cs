namespace AlturaSDK.API.Requests
{
    using Utils;
    
    public class GetCollectionRequest : ApiRequest
    {
        public GetCollectionRequest()
        {
            Uri = Endpoints.Collection;
        }
    }
}