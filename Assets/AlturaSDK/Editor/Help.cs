using UnityEditor;
using UnityEngine;

namespace AlturaSDK.Editor
{
    internal class Help : EditorWindow
    {
        [MenuItem("Altura/Help")]
        private static void ShowWindow()
        {
            Application.OpenURL("https://alturanft.com");
        }
    }
}