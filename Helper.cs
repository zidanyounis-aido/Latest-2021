using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace dms
{
    public static class Helper
    {
        private  static string appPath = AppDomain.CurrentDomain.BaseDirectory;
        private static void CreatePathIfNotExist()
        {
            if (!Directory.Exists($"{appPath}Uploads/{GetClientNumber()}"))
            {
                Directory.CreateDirectory($"{appPath}Uploads/{GetClientNumber()}");
            }
            if (!Directory.Exists($"{appPath}Images/{GetClientNumber()}"))
            {
                Directory.CreateDirectory($"{appPath}Images/{GetClientNumber()}");
            }
            if (!Directory.Exists($"{appPath}Temp/{GetClientNumber()}"))
            {
                Directory.CreateDirectory($"{appPath}Temp/{GetClientNumber()}");
            }
        }
        public static string GetUploadFolderUrl(this string fileName)
        {
            string path= $"{GetBaseUrl()}Uploads/{GetClientNumber()}/{fileName}";
            CheckPathDirectory(path);
            return path;
        }
        public static string GetImageFolderUrl(this string fileName)
        {
            string path= $"{GetBaseUrl()}Uploads/{GetClientNumber()}/{fileName}";
            CheckPathDirectory(path);
            return path;
        }

        public static string GetUploadDiskPath(this string fileName)
        {
            CreatePathIfNotExist();
            string path= $"{appPath}Uploads/{GetClientNumber()}/{fileName}";
            CheckPathDirectory(path);
            return path;
        }
        public static string GetUploadDiskPath()
        {
            CreatePathIfNotExist();
            string path= $"{appPath}Uploads/{GetClientNumber()}/";
            CheckPathDirectory(path);
            return path;
        }
        public static string GetImageDiskPath(this string fileName)
        {
            CreatePathIfNotExist();
            string path= $"{appPath}Images/{GetClientNumber()}/{fileName}";
            CheckPathDirectory(path);
            return path;
        }
        public static bool CheckPathDirectory(string path) {
            try
            {
                string directoryPath = Path.GetDirectoryName(path);
                bool exists = System.IO.Directory.Exists(directoryPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(directoryPath);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public static string GetImageDiskPath()
        {
            CreatePathIfNotExist();
            string path= $"{appPath}Images/{GetClientNumber()}/";
            CheckPathDirectory(path);
            return path;

        }

        public static string GetTempDiskPath(this string fileName)
        {
            CreatePathIfNotExist();
            string path= $"{appPath}Temp/{GetClientNumber()}/{fileName}";
            CheckPathDirectory(path);
            return path;
        }
        public static string GetTempDiskPath()
        {
            CreatePathIfNotExist();
            string path= $"{appPath}Temp/{GetClientNumber()}/";
            CheckPathDirectory(path);
            return path;
        }
        public static string GetBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }
        public static string GetClientNumber()
        {
            return HttpContext.Current.Session["clientId"].ToString();
        }
    }
}