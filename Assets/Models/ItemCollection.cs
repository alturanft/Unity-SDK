using System;

namespace SDK.Assets.Models
{
    [Serializable]
    public class ItemCollection
    {
        //blockchain id
        public string address;        
        
        public int chainId;
        public int version;
    
        public string ownerAddress;
        public string slug;

        public int uri;
        public bool isPublic;
        public DateTime mintDate;
        public string name;

        public string description;
        public string website;
        public string genre;

        // image
        public string image;
        public string imageUrl;
        public string imageHash;

        // holders and Volume
        public string holders;
        public string volume1d;
        public string volume1W;
        public string volume30d;
        public string volumeAll;

        public bool isVerified;
        public bool featured;
        public bool featuredMain;

    }
}
