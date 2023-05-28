using System;
using System.Collections.Generic;

namespace AlturaNFT.Internal
{
        [Serializable]
        public class User_model
        {
            
            public Users user;
            public List<Users> users;

        }
        [Serializable]
        public class Users
        {
                public string address;
                public string name;
                public string bio;
                public string profilePic;
                public string socialLink;
                public string profilePicUrl;

        }

    [Serializable]
    public class Items_model
    {
        public Items item;
        public List<Items> items;
        public Pagination pagination;
    }
    [Serializable]
    public class Items
    {
        public string name;
        public string description;
        public Properties[] properties;
        public int tokenId;
        public string collectionAddress;
        public int chainId;
        public int royalty;
        public string creatorAddress;
        public string mintDate;
        public bool stackable;
        public string supply;
        public string maxSupply;
        public string image;
        public string imageHash;
        public string imageUrl;
        public string fileType;
        public bool isVideo;
        public string otherImageVisibility;
        public int holders;
        public int likes;
        public int views;
        public bool isListed;
        public string mostRecentListing;
        public string cheapestListingPrice;
        public string cheapestListingCurrenct;
        public string cheapestListingUSD;
        public bool nsfw;
        public bool isVerified;
        public bool hasUnlockableContent;
        public bool didLike;
        public int imageIndex;
        public int imageCount;
        public int totalListings;

    }
    [Serializable]
    public class Pagination
    {
        public int current;
        public int next;
        public bool hasPrevious;
        public bool hasNext;
        public int perPageCount;
        public int currentCount;
        public int totalCount;
        public int lastPage;
    }
}