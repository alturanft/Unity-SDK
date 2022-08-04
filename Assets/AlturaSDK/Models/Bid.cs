using System;

namespace AlturaSDK.Models
{
    [Serializable]
    public class Bid
    {
        public string id;        
        
        public int chainId;
        public string newEvent;
        
        public int blockNumber;
        public int timestamp;
        public string to;
        public string from;
        public string transactionHash;
        public int tokenId;
        public string itemCollection;
        public string itemRef;

        public int amount;
        public string price;
        public string currency;
        public int worth;
        public int revenue;
        public bool notified;

    }
}
