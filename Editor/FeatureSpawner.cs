using AlturaNFT.Internal;
using UnityEditor;
using UnityEngine;

namespace AlturaNFT.Editor
{

    public class FeatureSpawner : EditorWindow
    {
        
        //GameObject
        private const string GameObjMenu = "GameObject/AlturaNFT/";
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_NFT_Details)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_NFT_Details)]
        static void Spawn_NFTDetails()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_NFT_Details).AddComponent<NFT_Details>().gameObject;
        }
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_User_Details)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_User_Details)]
        static void Spawn_User()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_User_Details).AddComponent<User_Details>().gameObject;
        }
                [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_Users_Details)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_Users_Details)]
        static void Spawn_Users()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_Users_Details).AddComponent<Users_Details>().gameObject;
        }
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_NFTs_OfAccount)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_NFTs_OfAccount)]
       static void Spawn_NFtsOfAccount()
        {
           Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_NFTs_OfAccount).AddComponent<NFTs_OwnedByAnAccount>().gameObject;
        }
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_NFTs_OfContract)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_NFTs_OfContract)]
        static void Spawn_NFtsOfContract()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_NFTs_OfContract).AddComponent<NFTs_OfAContract>().gameObject;
        }
        /*
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_StorageFiles)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_StorageFiles)]
       static void Spawn_StorageFile()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_StorageFiles).AddComponent<Storage_UploadFile>().gameObject;
        }
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_StorageMetadata)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_StorageMetadata)]
        static void Spawn_StorageMetadata()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_StorageMetadata).AddComponent<Storage_UploadMetadata>().gameObject;
        }
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_AssetDownloader)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_AssetDownloader)]
        static void Spawn_AssetDownloader()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_AssetDownloader).AddComponent<AssetDownloader>().gameObject;
        }
        */
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_ConnectUserWallet)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_ConnectUserWallet)]
        static void Spawn_ConnectWallet()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_ConnectUserWallet).AddComponent<ConnectPlayerWallet>().gameObject;
        }
        
//        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_Mint_Custom)]
 //       [MenuItem(GameObjMenu + AlturaConstants.FeatureName_Mint_Custom)]
 //       static void Spawn_Mint_Custom()
 //       {
 //           Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_Mint_Custom).AddComponent<Mint_Custom>().gameObject;
 //       }
        
//        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_Mint_URL)]
//        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_Mint_URL)]
  //      static void Spawn_Mint_URL()
   //     {
   //         Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_Mint_URL).AddComponent<Mint_URL>().gameObject;
   //     }
        
        //[MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_Mint_File)]
       // [MenuItem(GameObjMenu + AlturaConstants.FeatureName_Mint_File)]
 //       static void Spawn_Mint_File()
   //     {
   //         Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_Mint_File).AddComponent<Mint_File>().gameObject;
   //     }
        
        /*
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_Deploy)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_Deploy)]
        static void Spawn_Deploy()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_Deploy).AddComponent<Deploy>().gameObject;
        }
        */
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_Txn_Account)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_Txn_Account)]
        static void Spawn_Txn_Account()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_Txn_Account).AddComponent<Txn_Account>().gameObject;
        }
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_Txn_NFT)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_Txn_NFT)]
        static void Spawn_Txn_NFT()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_Txn_NFT).AddComponent<Txn_NFT>().gameObject;
        }
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_Txn_Collection)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_Txn_Collection)]
        static void Spawn_Txn_Collection()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_Txn_Collection).AddComponent<Txn_Collection>().gameObject;
        }



    }

}
