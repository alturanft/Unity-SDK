using System;
using System.Collections.Generic;

namespace AlturaNFT
{
    [Serializable]
    public class Auth_model
    {
        public string authenticated;

    }

    [Serializable]
    public class TransferOneReq
    {
        public string chainId;
        public string address;
        public string to;
        public string tokenId;
        public string amount;
    }

    [Serializable]
    public class TransferReq
    {
        public string chainId;
        public string address;
        public string to;
        public string tokenIds;
        public string amounts;
    }

    [Serializable]
    public class Transfer_model
    {
        public string txHash;
    }

    [Serializable]
    public class Transfers_model
    {
        public List<Transfer_model> txHashes;
    }

    [Serializable]
    public class UpdatePropertyReq
    {
        public string address;
        public string tokenId;
        public string property_name;
        public string property_value;
    }

    [Serializable]
    public class UpdatePrimaryImageReq
    {
        public string collectionAddress;
        public string tokenId;
        public string imageIndex;
    }

    [Serializable]
    public class UpdateCollectionReq
    {
        public string image;
        public string image_url;
        public string description;
        public string website;
        public string genre;
    }

    [Serializable]
    public class Collection_model
    {
        public string response;
        public Collection collection;
        public List<Collection> collections;


    }
    [Serializable] 
    public class Collection
    {
        public string holders;
        public string volume_1d;
        public string volume_1w;
        public string volume_30d;
        public string volume_all;
        public int chainId;
        public string address;
        public string name;
        public string ownerAddress;
        public string image;
        public string imageHash;
        public string imageUrl;
        public string description;
        public string uri;
        public string slug;
        public string website;
        public string genre;
        public string mintDate;
    } 

    [Serializable]
    public class History_Schema
    {
    
        public string amount;
        public string blockNumber;
        public string chainId;
        public string eventz;
        public string from;
        public string itemCollection;
        public string itemRef;
        public string timestamp;
        public string to;
        public string tokenId;
        public string transactionHash;
    }

    [Serializable]
    public class Holders 
    {
        public string address;
        public string balance;
        public string name;
        public string profilePic;
        public string profilePicUrl;
    }
   
}
