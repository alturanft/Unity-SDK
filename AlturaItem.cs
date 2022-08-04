using System;

namespace AlturaSDK;
 
    public class AlturaItem
    {
        //blockchain id
        private string tokenId;

        private string itemCollection;
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

        public string TokenId { get => tokenId; set => tokenId = value; }
        public string ItemCollection { get => itemCollection; set => itemCollection = value; }
        public string ChainId { get => chainId; set => chainId = value; }
        public string ItemRef { get => itemRef; set => itemRef = value; }
        public int Royalty { get => royalty; set => royalty = value; }
        public string CreatorAddress { get => creatorAddress; set => creatorAddress = value; }
        public DateTime MintDate { get => mintDate; set => mintDate = value; }

        public string MintId { get => mintId; set => mintId = value; }
        public bool Stackable { get => stackable; set => stackable = value; }   
        public int Supply { get => supply; set => supply = value; }
        public int MaxSupply { get => maxSupply; set => maxSupply = value; }
        public int NonStackableSupply { get => nonStackableSupply; set => nonStackableSupply = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string ExternalLink { get => externalLink; set => externalLink = value; }
        public string UnlockableContent { get => unlockableContent; set => unlockableContent = value; }
        public string Image { get => image; set => image = value; }
        public string ImageHash { get => imageHash; set => imageHash = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
        public string FileType { get => fileType; set => fileType = value; }
        public bool IsVideo { get => isVideo; set => isVideo = value; }

        // otherImages is object
        // public object otherImages { get; set; }
    }
