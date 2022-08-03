using System;

namespace AlturaSDK.Models
{
    [Serializable]
    public class Item
    {
        //blockchain id
        public string tokenId;        
        
        public string itemCollection;
        public string itemCollectionName;
        
        public string chainId;
        public string itemRef;

        // static blockchain data
        public int royalty;
        public string creatorAddress;
        public DateTime mintDate;
        public string mintId;

        //supply
        public bool stackable;
        public int supply;
        public int maxSupply;
        public int nonStackableSupply;

        //metadata
        public string name;
        public string description;
        public string externalLink;
        public string unlockableContent;

        //images
        public string image;
        public string imageHash;
        public string imageUrl;
        public string fileType;
        public bool isVideo;

        // otherImages is object
       // public object otherImages { get; set; }


    }
}
