using System;
using AlturaNFT;
using UnityEngine;
using UnityEngine.Networking;

public class Test : MonoBehaviour
{
   void Start()
  {
    NFT_Details
      .Initialize(destroyAtEnd:true)
      .SetChain(NFT_Details.Chains.bsctest)
      .SetParameters(

          //for ethereum- EVM chains
          contract_address:"0xb260b4b5e3357b3942ba71cfa0a7bdd32763f8ae",
          token_id:1

          //for solana:
          //mint_address: "EH8AaTF9vNiW2poTKoQZKf6yqExYSrnoTxAd62TEfaWn",

          )
      .OnError(error=>Debug.Log(error))
      .Run();

  }

  public Items_model NFTOfUser = new Items_model();


}