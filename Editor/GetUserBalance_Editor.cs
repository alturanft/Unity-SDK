using UnityEngine;

namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(GetUserBalance))]
    public class GetUserBalance_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            GetUserBalance myScript = (GetUserBalance)target;
            
            
            Texture banner = Resources.Load<Texture>("AlturaFrame");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("GET User Balance", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Docs_GetUserBalance);
            DrawDefaultInspector();
        }
    }
}

