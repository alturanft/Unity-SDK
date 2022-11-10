using UnityEngine;
/* TODO there is no Altura Gurd script
namespace AlturaNFT.Editor
{
    using UnityEditor;
    using Internal;

    [CustomEditor(typeof(Altura_Guard))]
    public class Altura_Guard_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            Altura_Guard myScript = (Altura_Guard)target;
            
            
            Texture banner = Resources.Load<Texture>("c_nftdata_details");
            GUILayout.BeginHorizontal();
            GUILayout.Box(banner);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Verify Auth Code", GUILayout.Height(45)))
            {
                AlturaUser.SetFromEditorWin();
                myScript.Run();
            }

            if(GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Altura_Guard);
            DrawDefaultInspector();
        }
    }
}
*/