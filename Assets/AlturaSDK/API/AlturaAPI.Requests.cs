using System;

namespace AlturaSDK.API
{
    using Requests;
    using Responses;

    public partial class AlturaAPI
    {
        public void GetUser()
        {
            Get(new GetUserRequest());
        }

        public void GetItem()
        {
            Get(new GetItemRequest());
        }

        public void GetCollection()
        {
            Get(new GetCollectionRequest());
        }
    }
}