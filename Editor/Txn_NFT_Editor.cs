using UnityEngine;

namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Txn_NFT))]
    public class Txn_NFT_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Txn_NFT myScript = (Txn_NFT)target;
            
            
            Texture banner = Resources.Load<Texture>("c_nftdata_details");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Get NFT Transactions", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }
        

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Docs_Txns_NFT);
            DrawDefaultInspector();
        }
    }
}

