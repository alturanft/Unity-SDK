using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class AlturaTestTools : EditorWindow
    {
        [MenuItem("Altura/Test Tools")]
        private static void ShowWindow()
        {
            var window = GetWindow<AlturaTestTools>();
            window.titleContent = new GUIContent("Altura Test Tools");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Space(20);

            if (GUILayout.Button("Clear PlayerPrefs"))
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }
}