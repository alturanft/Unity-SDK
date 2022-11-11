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
    public class Collection_model
    {
        public string response;
        public Collection collection;
        public List<Collection> collections;


    }
    [Serializable] 
    public class Collection
    {
        public int holders;
        public int volume_1d;
        public int volume_1w;
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
    public class Creator
    {
        public string account_address ;
        public string creator_share ;
        public string address;
        public object share;
        public bool verified;
    }



}
