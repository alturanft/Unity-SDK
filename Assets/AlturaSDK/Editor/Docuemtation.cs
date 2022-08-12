using UnityEditor;
using UnityEngine;

namespace AlturaSDK.Editor
{
    internal class Documentation : EditorWindow
    {
        [MenuItem("Altura/Documentation")]
        private static void ShowWindow()
        {
            Application.OpenURL("https://alturanft.com");
        }
    }
}