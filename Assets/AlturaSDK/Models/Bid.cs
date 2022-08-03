using System;

namespace AlturaSDK.Models
{
    [Serializable]
    public class Bid
    {
        public string id;        
        
        public string pairId;
        public string chainId;
        
        public string bidIndex;
        public string bidTx;
        public string cancelTx;
        public DateTime placedAt;
        public DateTime cancelledAt;
        public string from;
        public string currency;
        public int price;
        public int priceUSD;
        public bool active;
        public DateTime updatedAt;

    }
}
