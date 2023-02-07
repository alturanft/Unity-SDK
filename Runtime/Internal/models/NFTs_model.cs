using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Object = UnityEngine.Object;

namespace AlturaNFT
{


    /*[Serializable]
    public class Items_model
    {
        public Items item;
        public List<Items> items;
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
        public int listers;
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

    }*/

    [Serializable]
    public class AllImages 
    {
        public string _id;
        public string imageHash;
        public string image;
        public string fileType;
        public string isVideo;
    }

    [Serializable]
    public class Properties
    {
        public string name;
        public string value;
       // public string static;

    }
  
}