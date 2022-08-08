
namespace SDK.Assets.Client
{
    public class Web3Client : ApiClient
    {
        public Web3Client(string basePath = "https://api.alturanft.com", string apiVersion = "/api/v2") : base(basePath, apiVersion)
        {
        }
    }
}