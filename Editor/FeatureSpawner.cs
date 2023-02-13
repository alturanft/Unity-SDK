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
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_GetUsersItems)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_GetUsersItems)]
       static void Spawn_GetUsersItems()
        {
           Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_GetUsersItems).AddComponent<GetUsersItems>().gameObject;
        }
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_AuthenticateUser)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_AuthenticateUser)]
        static void Spawn_NFtsOfContract()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_AuthenticateUser).AddComponent<AuthenticateUser>().gameObject;
        }

        /*[MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_ConnectUserWallet)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_ConnectUserWallet)]
        static void Spawn_ConnectWallet()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_ConnectUserWallet).AddComponent<ConnectPlayerWallet>().gameObject;
        }*/
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_GetCollections)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_GetCollections)]
        static void Spawn_GetCollections()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_GetCollections).AddComponent<GetCollections>().gameObject;
        }

        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_GetHolder)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_GetHolder)]
        static void Spawn_GetHolder()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_GetHolder).AddComponent<GetHolder>().gameObject;
        }

        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_GetHistory)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_GetHistory)]
        static void Spawn_GetHistory()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_GetHistory).AddComponent<GetHistory>().gameObject;
        }
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_Txn_NFT)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_Txn_NFT)]
        static void Spawn_Txn_NFT()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_Txn_NFT).AddComponent<GetItems>().gameObject;
        }
        
        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_GetCollection)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_GetCollection)]
        static void Spawn_GetCollection()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_GetCollection).AddComponent<GetCollection>().gameObject;
        }

        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_GetUserBalance)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_GetUserBalance)]
        static void Spawn_GetUserBalance()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_GetUserBalance).AddComponent<GetUserBalance>().gameObject;
        }

        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_GetUserERC20Balance)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_GetUserERC20Balance)]
        static void Spawn_GetUserERC20Balance()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_GetUserERC20Balance).AddComponent<GetUserERC20Balance>().gameObject;
        }

        [MenuItem(AlturaConstants.BaseFeatureSpawnerMenu + AlturaConstants.FeatureName_GetUserItemBalance)]
        [MenuItem(GameObjMenu + AlturaConstants.FeatureName_GetUserItemBalance)]
        static void Spawn_GetUserItemBalance()
        {
            Selection.activeGameObject= new GameObject(AlturaConstants.FeatureName_GetUserItemBalance).AddComponent<GetUserItemBalance>().gameObject;
        }

    }

}
