using System;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json; 
using UnityEditor.PackageManager;

namespace AlturaNFT.Editor 
{
    using Internal;
    [System.Serializable]

    public class AlturaNFTSettings : EditorWindow 
    {
        static string myAPIString = AlturaConstants.DefaultAPIKey;

        protected static Type WindowType = typeof(AlturaNFTSettings);

        private static bool windowopen = false;

        private  bool ranLatestrel = false;
        static PkgJson releasedPkgJson = null;
        static private bool AlturaNFTStarted;

        GUIStyle _horizontalGroup;

        GUIStyle _sampleButtonStyle;
        static int _selectedSample;

                
        [MenuItem("AlturaNFT/Home")]
        public static void ShowWindow()
        {
            var win = GetWindow<AlturaNFTSettings>(AlturaConstants.HomeWindowName);
            SetSize(win);
        }

        [UnityEditor.InitializeOnLoadMethod]
        public static void InitializeOnLoadMethod()
        {
            Events.registeredPackages += RegisteredPackagesEventHandler;
        }

        static void RegisteredPackagesEventHandler(PackageRegistrationEventArgs packageRegistrationEventArgs)
        {
            ReadFromUserPrefs();

            InstallAlturaDependencies.OnListCheckCompleteForNewtonSoft(arg0 => DependencyAction(arg0));
            InstallAlturaDependencies.OnListCheckCompleteForGLTF(arg0 => DependencyAction(arg0));
            InstallAlturaDependencies.CheckPkgsListForNewtonsoft();
        }

        static void DependencyAction(bool exists)
        {
            if (exists)
            {

            }
            else
            {

            }
        }
        [InitializeOnLoad]
        public class StartUp
        {
            static StartUp()
            {
                EditorApplication.update += AfterLoad;
            }

            static void AfterLoad()
            {
                AlturaNFTStarted = SessionState.GetBool("AlturaNFTStarted", false);
                if (!AlturaNFTStarted)
                {
                    ReadFromUserPrefs();
                    GetlatestRelease();
                    SessionState.SetBool("AlturaNFTStarted", true);
                }
            }
        }


        static void  GetlatestRelease()
        {
            var ls = LatestRel.Initialize();
            if (ls != null)
            {
                ls.OnComplete(pkg => LatestReleaseCallback(pkg));
                ls.Run();
            }
        }


        static void LatestReleaseCallback(PkgJson pkg)
        {
            releasedPkgJson = pkg;

            if (SessionState.GetBool("FirstRelAfterFirstLoad", false) == false)
            {
                if (releasedPkgJson != null)
                {
                    string lv = releasedPkgJson.version;
                    string iv = PkgInfo.GetInstalledPackageVer();
                    if(lv!= iv)
                        Debug.Log("New AlturaNFT release is available : " + lv + " , your installed version is: " + iv + " , you may update via the Package Manager. View AlturaNFT/Home.");
                    SessionState.SetBool("FirstRelAfterFirstLoad", true);
                }
            }
        }

        void InitStyles()
        {
            _horizontalGroup = new GUIStyle();
            _horizontalGroup.padding = new RectOffset(0, 0, 4, 4);
            
            _sampleButtonStyle = new GUIStyle(GUI.skin.FindStyle("button"));
            _sampleButtonStyle.imagePosition = ImagePosition.ImageAbove;
            _sampleButtonStyle.padding = new RectOffset(10, 10, 15, 15);
            _sampleButtonStyle.fontStyle = FontStyle.Bold;
        }

      private bool apiIsPass = true;

             void OnGUI()
        {
            InitStyles();

            Texture banner = Resources.Load<Texture>("banner");
            GUIContent button_banner = new GUIContent(banner, "AlturaNFT.com");
            if (GUILayout.Button(button_banner, GUILayout.Width(530), GUILayout.Height(270)))
                Application.OpenURL(AlturaConstants.FeatureList);
            var hover = GUI.tooltip;
            
            Texture2D cursorNFT = Resources.Load<Texture2D>("logo 1");
            if (hover == "AlturaNFT.com")
                Cursor.SetCursor(cursorNFT, new Vector2(0,0),0);
                EditorGUIUtility.AddCursorRect(new Rect(0, 0, 500, 500), MouseCursor.FPS);
            

            GUILayout.Label("Welcome to AlturaNFT Unity SDK ", EditorStyles.whiteLargeLabel);
            GUILayout.Label("\n" +
                            " Use Altura NFT API in your Unity game today \n with our brand new ALTURA Unity SDK!!\n" +
                            "", EditorStyles.label);

            
            GUILayout.BeginHorizontal("box");
            var defaultColor = GUI.backgroundColor;

            if (myAPIString == AlturaConstants.DefaultAPIKey)
            {
                apiIsPass = false;
                GUI.color = UnityEngine.Color.cyan;
            }
            else
            {
                if(APIkeyOk())
                    GUI.color = UnityEngine.Color.green;
                else
                {
                    GUI.color = UnityEngine.Color.red;
                }
            }

            if (!apiIsPass)
            {
                if (GUILayout.Button("hide", GUILayout.Width(42)))
                    apiIsPass = true;
                
                myAPIString = EditorGUILayout.TextField("APIKEY", myAPIString); 
            }
            else
            {
                if (GUILayout.Button("show", GUILayout.Width(42)))
                    apiIsPass = false;
                
                myAPIString = EditorGUILayout.PasswordField("Altura APIKEY", myAPIString); 

            }
            

            GUI.color = defaultColor;
            GUILayout.EndHorizontal();


            if (GUILayout.Button("Save Altura API Key", GUILayout.Height(25)))
            {
                SaveChanges();
                apiIsPass = true;
            }



            GuiLine();
            

            
            GuiLine();
            
            if (GUILayout.Button("View Documentation", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.Docs_GettingStarted);
            
            if (GUILayout.Button("Community & Support", GUILayout.Height(25)))
                Application.OpenURL(AlturaConstants.DiscordInvite);
            
            GuiLine();
            
            GUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("installed version: " + PkgInfo.GetInstalledPackageVer());

            if (!ranLatestrel)
            {
               GetlatestRelease();
               ranLatestrel = true;
            }

            if (releasedPkgJson != null)
            {
                EditorGUILayout.LabelField("Latest release version: " + releasedPkgJson.version);
            }

            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Github", GUILayout.Height(22))) 
                Application.OpenURL(AlturaConstants.Github);
            
            if (GUILayout.Button("Go to Altura Dashboard", GUILayout.Height(22)))
                Application.OpenURL(AlturaConstants.Dashboard);
            
            GUILayout.EndHorizontal();
            
            GuiLine();
            
            EditorGUILayout.LabelField("");
            EditorGUILayout.LabelField("Altura SDK examples coming soon!");
            EditorGUILayout.BeginHorizontal();
          /*  
            Texture button_tex = (Texture)Resources.Load<Texture>("pg-gallery1");
            GUIContent button_tex_con = new GUIContent(button_tex);
            if (GUILayout.Button(button_tex_con,_sampleButtonStyle, GUILayout.Width(260), GUILayout.Height(100))) 
                Application.OpenURL(AlturaConstants.AdvPlaygroundGallery);

            Texture button_tex2 = (Texture)Resources.Load<Texture>("pg-gallery2");
            GUIContent button_tex_con2 = new GUIContent(button_tex2);
            if (GUILayout.Button(button_tex_con2,_sampleButtonStyle, GUILayout.Width(260), GUILayout.Height(100))) 
                Application.OpenURL(AlturaConstants.PlaygroundButterflyMint);
            */
            EditorGUILayout.EndHorizontal();


        }

        private bool firstload = true;


        void OnEnable()
        {
            if (!windowopen)
            {
                if (!firstload)
                {
                    firstload = false;
                    return;
                    
                }
                else
                {
                    ReadFromUserPrefs();
                }
                
            }
            windowopen = true;
            ranLatestrel = false;
            HighLightReadmeAsset();
        }

        void HighLightReadmeAsset()
        {
            Selection.activeObject=AssetDatabase.LoadMainAssetAtPath("Packages/com.alturanft.alturanft/Runtime/Readme.asset");
        }

        private void OnDisable()
        {
            windowopen = false;
        }

        public override void SaveChanges()
        {
            WriteToUserPrefs();
            UserStats(); 
        }
        
        static void ShowHomeWindow()
        {
            if(windowopen)
                return;
            
            AlturaNFTSettings win = GetWindow(WindowType, false, AlturaConstants.HomeWindowName, true) as AlturaNFTSettings;
            if (win == null)
            {
                return;  
            }

            windowopen = true;
            SetSize(win);
            win.Show();
        }
        
        static void SetSize(AlturaNFTSettings win) 
        {
            win.minSize = new Vector2(530, 750);
            win.maxSize = new Vector2(530, 750);
        } 

        static void GuiLine( int i_height = 1 )
        {
            Rect rect = EditorGUILayout.GetControlRect(false, i_height );
            rect.height = i_height;
            EditorGUI.DrawRect(rect, new UnityEngine.Color ( 0.5f,0.5f,0.5f, 1 ) );
        }
           
        #region ReadWrite UserPrefs
        
        private static AlturaUser.UserPrefs _userPrefs = new AlturaUser.UserPrefs();
        private static TextAsset targetFile;
        static void ReadFromUserPrefs()
        {
            targetFile = Resources.Load<TextAsset>("AlturaNFT UserPrefs");
            if (targetFile != null)
            {
                _userPrefs = JsonConvert.DeserializeObject<AlturaUser.UserPrefs>(targetFile.text);
                myAPIString = _userPrefs.API_KEY;
                AlturaUser.SetVersion(PkgInfo.GetInstalledPackageVer());
                AlturaUser.SaveNewApi(myAPIString);
                AlturaUser.Initialise();
                UserStats();
            }
            else
            { 
                AlturaUser._initialised = false;
                myAPIString = AlturaConstants.DefaultAPIKey;
                if(!windowopen)
                    ShowHomeWindow();
            }

        }
        void WriteToUserPrefs()
        {
            AlturaUser.SetVersion(PkgInfo.GetInstalledPackageVer());
            AlturaUser.SaveNewApi(myAPIString);
            base.SaveChanges();
        }

        #endregion

        #region Userstats
        static User_model userModel;
        static void UserStats()
        {
            userModel = null;
            AlturaUser.SetFromAuto();
            AlturaUserSettings
                .Initialize(true)
                .OnError(usermodel=> StatsError())
                .OnComplete(usermodel=> userModel = usermodel)
                .Run();
        }

        static void StatsError()
        {
            if (!windowopen && !APIkeyOk())
            {
                ShowHomeWindow();
            }
        }

        static bool APIkeyOk()
        {
       //     UnityWebRequest www = UnityWebRequest.Get(AlturaConstants.API_URL + "user/verify_auth_code" + "/" + address + "/" + code) ;
            if (userModel == null)
            {
                AlturaUser._initialised = false;
                return false;
            }

            if (userModel != null)
            {
                AlturaUser.Initialise();
                return true;
            }
            else
            {
                AlturaUser._initialised = false;
                return false;
            }
        }

        #endregion
         

    }

}