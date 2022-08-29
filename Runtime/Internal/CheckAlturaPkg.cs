#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Events;

namespace AlturaNFT.Editor
{
    public static class CheckAlturaPkg
    {

        static ListRequest listRequest;

        public static void CheckPkgList()
        {
            listRequest = Client.List();
            EditorApplication.update += CheckPackageProgress;
        }

        private static UnityAction<string> OnListCheckCompleteActionPackageExists;
        public static void OnListCheckComplete(UnityAction<string> callback)
        {
            OnListCheckCompleteActionPackageExists = callback;
        }

        static void CheckPackageProgress()
        {
            var pkgexists = false;
            if (listRequest.IsCompleted)
            {
                if (listRequest.Status == StatusCode.Success)
                foreach (var package in listRequest.Result)
                {
                    if (package.name.Contains("alturanft"))
                    {
                        pkgexists = true;
                    }
                }
                else if (listRequest.Status >= StatusCode.Failure)
                    OnListCheckCompleteActionPackageExists.Invoke(StatusCode.Failure.ToString());
                
                if(pkgexists)
                    OnListCheckCompleteActionPackageExists.Invoke(true.ToString());
                else
                {
                    OnListCheckCompleteActionPackageExists.Invoke(false.ToString());
                }

                EndListCheck();
            }
        }
        static void EndListCheck()
        {
            EditorApplication.update -= CheckPackageProgress;
        }
    }

}

#endif