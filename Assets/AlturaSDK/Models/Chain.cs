using System;

namespace AlturaSDK.Models
{
    [Serializable]
    public class Chain
    {
        public int id;
        
        public int latestHolderTimestamp;
        public int latestItemsTimestamp;
        public int latestEventsTimestamp;
        public int latestOfferTimestamp;

        public DateTime updatedAt;

        
    }
}
