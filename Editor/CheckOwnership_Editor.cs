using UnityEngine;

namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(CheckOwnership))]
    public class CheckOwnership_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            CheckOwnership myScript = (CheckOwnership)target;
            
            
            Texture banner = Resources.Load<Texture>("AlturaFrame");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Verify item Ownership", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Docs_CheckOwnership);
            DrawDefaultInspector();
        }
    }
}

