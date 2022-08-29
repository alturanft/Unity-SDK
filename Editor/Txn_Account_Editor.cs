using UnityEngine;

namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Txn_Account))]
    public class Txn_Account_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Txn_Account myScript = (Txn_Account)target;
            
            
            Texture banner = Resources.Load<Texture>("c_nftdata_details");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Get Account NFT Transactions", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }
        

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Docs_Txns_Account);
            DrawDefaultInspector();
        }
    }
}

