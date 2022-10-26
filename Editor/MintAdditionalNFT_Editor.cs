using System.IO;
using UnityEngine;

namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(MintAdditionalNFT))]
    public class MintAdditionalNFT_Editor : Editor
    {
        private MintAdditionalNFT myScript;
        public override void OnInspectorGUI()
        {
            myScript = (MintAdditionalNFT)target;
            
            Texture banner = Resources.Load<Texture>("c_nftdata_details");
            GUILayout.Box(banner);
            
            if (GUILayout.Button("Mint Additional NFTs", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }
 
            
            
            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Docs_Mint);


            DrawDefaultInspector();
        }
        
        

    }
}

