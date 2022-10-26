using System.IO;
using UnityEngine;

namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(TransferItem))]
    public class TransferItem_Editor : Editor
    {
        private TransferItem myScript;
        public override void OnInspectorGUI()
        {
            myScript = (TransferItem)target;
            
            Texture banner = Resources.Load<Texture>("c_nftdata_details");
            GUILayout.Box(banner);
            
            if (GUILayout.Button("Transfer Item", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }
 
            
            
            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Docs_Transfer);


            DrawDefaultInspector();
        }
        
        

    }
}

