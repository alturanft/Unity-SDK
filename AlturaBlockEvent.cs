
namespace AlturaSDK;

    [Serializable]
    public class AlturaBlockEvent
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

    public string Id { get => id; set => id = value; }
    public string PairId { get => pairId; set => pairId = value; }
    public string ChainId { get => chainId; set => chainId = value; }
    public string BidIndex { get => bidIndex; set => bidIndex = value; }
    public string BidTx { get => bidTx; set => bidTx = value; }
    public string CancelTx { get => cancelTx; set => cancelTx = value; }
    public DateTime PlacedAt { get => placedAt; set => placedAt = value; }
    public DateTime CancelledAt { get => cancelledAt; set => cancelledAt = value; }
    public string From { get => from; set => from = value; }
    public string Currency { get => currency; set => currency = value; }
    public int Price { get => price; set => price = value; }
    public int PriceUSD { get => priceUSD; set => priceUSD = value; }
    public bool Active { get => active; set => active = value; }
    public DateTime UpdatedAt { get => updatedAt; set => updatedAt = value; }
    
}

