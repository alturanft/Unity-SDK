using UnityEngine;

namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(UpdatePrimaryImage))]
    public class UpdatePrimaryImage_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            UpdatePrimaryImage myScript = (UpdatePrimaryImage)target;
            
            
            Texture banner = Resources.Load<Texture>("AlturaFrame");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Update Primary Image", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Docs_UpdatePrimaryImage);
            DrawDefaultInspector();
        }
    }
}

