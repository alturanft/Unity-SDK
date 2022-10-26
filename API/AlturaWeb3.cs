using System.Collections;
using Newtonsoft.Json;
using UnityEngine;

namespace AlturaNFT
{
    using Internal;
    public class AlturaWeb3
    {

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