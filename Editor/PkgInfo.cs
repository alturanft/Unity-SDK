using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AlturaNFT.Editor
{
    public static class PkgInfo
    {
        public static string GetInstalledPackageVer()
        {
            string path = "Packages/com.alturaft.alturanft/package.json";
            if (File.Exists(path))
            {
                var targetFile = File.ReadAllText(path);

                if (targetFile != null)
                {
                    PkgJson pkgJson = JsonConvert.DeserializeObject<PkgJson>(targetFile);
                    return pkgJson.version;
                }
            }
            return String.Empty;
        }
    }
}
