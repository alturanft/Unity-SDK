using System.Collections;
using Newtonsoft.Json;
using UnityEngine;

// create AlturaWeb3().GetItem(), AlturaWeb3().MintAdditionalNFT(), AlturaWeb3().TransferItem()
namespace AlturaNFT
{
    using Internal;
    public class AlturaWeb3
    {
        public GetUsers Users_Details()
        {
            return new GetUsers();
        }
        public GetItems GetItems()
        {
            return new GetItems();
        }

        public GetItem NFT_Details()
        {
            return new GetItems();
        }
        public MintAdditionalNFT MintAdditionalNFT()
        {
            return new MintAdditionalNFT();
        }
        public TransferItem TransferItem()
        {
            return new TransferItem();
        }


    }
}