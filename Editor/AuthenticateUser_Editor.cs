using UnityEngine;

namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(AuthenticateUser))]
    public class AuthenticateUser_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            AuthenticateUser myScript = (AuthenticateUser)target;
            
            
            Texture banner = Resources.Load<Texture>("AlturaFrame");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Verify Auth Code", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.AuthenticateUser);
            DrawDefaultInspector();
        }
    }
}

