using System;
using AlturaNFT;
using UnityEngine;
using UnityEngine.Networking;

public class Test : MonoBehaviour
{
   void Start()
  {
    GetItems
      .Initialize(destroyAtEnd:true)
      .SetChain(GetItems.Chains.bsctest)
      .SetParameters(perPage:"29", page: "4", sortBy: "name", sortDir: "desc", slim: "false")

      .OnError(error=>Debug.Log(error))
      .Run();

      Users_Details
        .Initialize(destroyAtEnd:true)
        .SetParameters(perPage:"29", page: "4", sortBy: "name", sortDir: "desc")
        .Run();

  }

  public Items_model NFTOfUser = new Items_model();


}