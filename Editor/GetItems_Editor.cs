using UnityEngine;

namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(GetItems))]
    public class GetItems_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            GetItems myScript = (GetItems)target;
            
            
            Texture banner = Resources.Load<Texture>("AlturaFrame");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Get Bulk Items", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }
        

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Docs_GetItems);
            DrawDefaultInspector();
        }
    }
}

