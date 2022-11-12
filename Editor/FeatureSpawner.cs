using AlturaNFT.Internal;
using UnityEditor;
using UnityEngine;

namespace AlturaNFT.Editor
{

    public class FeatureSpawner : EditorWindow
    {
        
        private const string GameObjMenu = "GameObject/AlturaNFT/";
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_GetItem)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_GetItem)]
        static void Spawn_GetItem()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_GetItem).AddComponent<GetItem>().gameObject;
        }
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_GetUser)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_GetUser)]
        static void Spawn_GetUser()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_GetUser).AddComponent<GetUser>().gameObject;
        }

        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_GetUsers)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_GetUsers)]
        static void Spawn_GetUsers()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_GetUsers).AddComponent<GetUsers>().gameObject;
        }
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_NFTs_OfAccount)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_NFTs_OfAccount)]
       static void Spawn_NFtsOfAccount()
        {
           Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_NFTs_OfAccount).AddComponent<NFTs_OwnedByAnAccount>().gameObject;
        }
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_AuthenticateUser)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_AuthenticateUser)]
        static void Spawn_NFtsOfContract()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_AuthenticateUser).AddComponent<AuthenticateUser>().gameObject;
        }

        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_ConnectUserWallet)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_ConnectUserWallet)]
        static void Spawn_ConnectWallet()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_ConnectUserWallet).AddComponent<ConnectPlayerWallet>().gameObject;
        }
        
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
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_Txn_NFT).AddComponent<GetItems>().gameObject;
        }
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_Txn_Collection)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_Txn_Collection)]
        static void Spawn_Txn_Collection()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_Txn_Collection).AddComponent<Txn_Collection>().gameObject;
        }

        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_Transfer)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_Transfer)]
        static void Spawn_Transfer()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_Transfer).AddComponent<TransferItem>().gameObject;
        }

        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_TransferItems)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_TransferItems)]
        static void Spawn_TransferItems()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_TransferItems).AddComponent<TransferItems>().gameObject;
        }

        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_MintAdditionalNFT)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_MintAdditionalNFT)]
        static void Spawn_MintAdditionalNFT()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_MintAdditionalNFT).AddComponent<MintAdditionalNFT>().gameObject;
        }
    }

}
