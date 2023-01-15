using UnityEngine;

namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(GetUserERC20Balance))]
    public class GetUserERC20Balance_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            GetUserERC20Balance myScript = (GetUserERC20Balance)target;
            
            
            Texture banner = Resources.Load<Texture>("AlturaFrame");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("GET User ERC20 Balance", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Docs_GetUserERC20Balance);
            DrawDefaultInspector();
        }
    }
}

