using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace AlturaNFT.Internal
{
    using AlturaNFT.Editor;
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif

    public static class AlturaUser
    {

    public static bool _initialised = false;

    public static void Initialise()
    {
#if UNITY_EDITOR
            CheckAlturaPkg.OnListCheckComplete(isUPM => src.UPMImport = isUPM.ToString());
            CheckAlturaPkg.CheckPkgList();
#endif
        }
      

        #region Get Set Gos

   
        class Source
        {
            public string from = "AlturaNFT-Unity";
            public string isEditor = "";
            public string UnityVersion = "";
            public string ToolWin = "";
            public string UPMImport = "na";
            public string AppPlatform = "ni";
            public string ID = "na";
        }
        
        public static void SetFromEditorWin()
        {
            _toolWin = ToolWin.AlturaNFTEditor;
        }

                public static void SetFromAuto()
        {
            _toolWin = ToolWin.auto;
        }
        public static void SetFromOnEnable()
        {
            _toolWin = ToolWin.OnEnable;
        }


        private static ToolWin _toolWin;
        enum ToolWin
        {
            UserScript, // keepontop
            AlturaNFTEditor, // keepontop
            auto,
            OnEnable
        }

        static Source src = new Source();

        public static string GetSource()
        {
            if (!_initialised)
                return "";
            src.isEditor = Application.isEditor.ToString();
            src.UnityVersion = Application.unityVersion;
            src.ToolWin = _toolWin.ToString();
            src.AppPlatform = Application.platform.ToString();
            src.ID = Application.companyName.ToString() + " | " + Application.productName.ToString();

            string json = JsonConvert.SerializeObject(
                src, 
            new JsonSerializerSettings
            {
            });

            _toolWin = ToolWin.UserScript;
            
            return json;
        }

        #endregion

    }
}