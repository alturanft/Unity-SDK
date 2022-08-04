namespace SDK.Assets.Models
{
    [Serializable]
    public class Chain
    {
    private int id;

    private int latestHolderTimestamp;
    public int latestItemsTimestamp;
    public int latestEventsTimestamp;
    public int latestOfferTimestamp;

    public DateTime updatedAt;

    public int Id { get => id; set => id = value; }
    public int LatestHolderTimestamp { get => latestHolderTimestamp; set => latestHolderTimestamp = value; }
    public int LatestItemsTimestamp { get => latestItemsTimestamp; set => latestItemsTimestamp = value; }   
    public int LatestEventsTimestamp { get => latestEventsTimestamp; set => latestEventsTimestamp = value; }
    public int LatestOfferTimestamp { get => latestOfferTimestamp; set => latestOfferTimestamp = value; }
    public DateTime UpdatedAt { get => updatedAt; set => updatedAt = value; }
    
}
}
