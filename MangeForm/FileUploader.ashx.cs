using dms.Controlers.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace dms.MangeForm
{
    /// <summary>
    /// Summary description for FileUploader
    /// </summary>
    public class FileUploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            context.Response.ContentType = "text/plain";
            int metaId = context.Request["metaId"].ToString().ToInt();
            string filemetaId = $"metaId{context.Request["metaId"]}";
            try
            {
                string dirFullPath = HttpContext.Current.Server.MapPath("~/MetaUploader/");
                string[] files;
                int numFiles;
                files = System.IO.Directory.GetFiles(dirFullPath);
                numFiles = files.Length;
                numFiles = numFiles + 1;
                string str_image = "";

                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    string fileName = file.FileName;
                    string fileExtension = file.ContentType;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        fileExtension = Path.GetExtension(fileName);
                        str_image = filemetaId + fileExtension;
                     
                        string pathToSave = HttpContext.Current.Server.MapPath("~/MetaUploader/") + str_image;
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/MetaUploader/")))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/MetaUploader/"));
                        }
                        DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/MetaUploader/"));
                        FileInfo[] oldFiles = di.GetFiles($"{filemetaId}.*");
                        foreach (FileInfo fi in oldFiles)
                        {
                            fi.Delete();
                        }
                        file.SaveAs(pathToSave);
                        var response = new MetaManager().SaveImageUrl(metaId, str_image);
                    }
                }
                //  database record update logic here  ()

                context.Response.Write(str_image);
            }
            catch (Exception ex)
            {

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}