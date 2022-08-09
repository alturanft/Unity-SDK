using System;

namespace Web3.Models
{
    public struct Properties 
    {
        public string name;
        public string value;
        public bool statix;
    }

    public class AlturaItem
    {
        public string name;
        public string description;
        public Properties properties;
        public int chainId;
        public int royalty;

        public string creatorAddress;
        public string mintDate;
        public bool stackable;
        public int supply;
        public int maxSupply;
        public string image;
        public string imageHash;
        public string imageUrl;
        public string fileType;
        public bool isVideo;
        public string otherImageVisibility;
        public int holders;
        public int listers;
        public int likes;
        public int views;
        public bool isListed;
        public string mostRecentListing;
        public int cheapestListingPrice;
        public string cheapestListingCurrency;
        public int cheapestListingUSD;
        public bool nsfw;
        public bool isVerified;
        public bool hasUnlockableContent;
        public int imageIndex;
        public int imageCount;
        public int totalListings;
      
    }
}
