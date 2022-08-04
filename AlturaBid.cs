namespace AlturaSDK;

    [Serializable]
    public class AlturaBid
    {
    public string id;

    public int chainId;
    public string newEvent;
        
        public int blockNumber;
        public DateTime timestamp;
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

    public string Id { get => id; set => id = value; }
    public int ChainId { get => chainId; set => chainId = value; }
    public string NewEvent { get => newEvent; set => newEvent = value; }
    public int BlockNumber { get => blockNumber; set => blockNumber = value; }
    public DateTime Timestamp { get => timestamp; set => timestamp = value; }
    public string To { get => to; set => to = value; }
    public string From { get => from; set => from = value; }
    public string TransactionHash { get => transactionHash; set => transactionHash = value; }
    public int TokenId { get => tokenId; set => tokenId = value; }
    public string ItemCollection { get => itemCollection; set => itemCollection = value; }
    public string ItemRef { get => itemRef; set => itemRef = value; }
    public int Amount { get => amount; set => amount = value; }
    public string Price { get => price; set => price = value; }
    public string Currency { get => currency; set => currency = value; }
    public int Worth { get => worth; set => worth = value; }
    public int Revenue { get => revenue; set => revenue = value; }
    public bool Notified { get => notified; set => notified = value; }
    
    
}

