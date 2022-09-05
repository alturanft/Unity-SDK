using AlturaNFT.Internal;
using UnityEditor;
using UnityEngine;

namespace AlturaNFT.Editor
{

    public class FeatureSpawner : EditorWindow
    {
        
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
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_Altura_Guard)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_Altura_Guard)]
        static void Spawn_NFtsOfContract()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_Altura_Guard).AddComponent<Altura_Guard>().gameObject;
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

    }

}
