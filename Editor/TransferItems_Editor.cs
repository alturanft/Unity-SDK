using System.IO;
using UnityEngine;

namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(TransferItems))]
    public class TransferItem_Editors : Editor
    {
        private TransferItems myScript;
        public override void OnInspectorGUI()
        {
            myScript = (TransferItems)target;
            
            Texture banner = Resources.Load<Texture>("c_nftdata_details");
            GUILayout.Box(banner);
            
            if (GUILayout.Button("Transfer Items", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }
 
            
            
            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Docs_Transfers);


            DrawDefaultInspector();
        }
        
        

    }
}

