using UnityEngine;

namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Txn_Collection))]
    public class Txn_Collection_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Txn_Collection myScript = (Txn_Collection)target;
            
            
            Texture banner = Resources.Load<Texture>("c_nftdata_details");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("GET Contract/Collection Transactions", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Altura_Guard);
            DrawDefaultInspector();
        }
    }
}

