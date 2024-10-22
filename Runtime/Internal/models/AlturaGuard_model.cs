using System;
using System.Collections.Generic;

namespace AlturaNFT.Internal
{
    [Serializable]
    public class AlturaGuardTransactionResponse_model
    {

        public string txHash;

    }
    [Serializable]
    public class AlturaGuardConnection_model
    {

        public string address;
        public string token;
    }
}