using System;

namespace AlturaSDK.Models
{
    [Serializable]
    public class Request 
    {
        public string from;
        public string type;
        public string status;

        // public MongoDB.Bson.BsonDocument data;
        public DateTime requestedAt;
        public DateTime updatedAt;
        public DateTime processedAt;
    }
}