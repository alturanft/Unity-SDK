using UnityEngine;

namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(UpdateCollection))]
    public class UpdateCollection_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            UpdateCollection myScript = (UpdateCollection)target;
            
            
            Texture banner = Resources.Load<Texture>("AlturaFrame");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("GET User Details", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Docs_UpdateCollection);
            DrawDefaultInspector();
        }
    }
}

