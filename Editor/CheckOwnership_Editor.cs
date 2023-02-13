using UnityEngine;

namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(GetUserItemBalance))]
    public class GetUserItemBalance_Editor : Editor
    {
        public override void OnInspectorGUI()
        {

            GetUserItemBalance myScript = (GetUserItemBalance)target;
            
            
            Texture banner = Resources.Load<Texture>("AlturaFrame");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Get item Balance", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Docs_GetUserItemBalance);
            DrawDefaultInspector();
        }
    }
}

