using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dms
{
    /// <summary>
    /// Summary description for Validation
    /// </summary>
    public class Validation : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
   {

       if (HttpContext.Current.Session["userID"] != null)
       {
           string file = context.Request.QueryString["file"];
           
           if (!string.IsNullOrEmpty(file))
           {
               if (context.Request.RawUrl.ToString().ToLower().Contains("uploads"))
               {
                   string fileName = file;
                   fileName = fileName.Substring(fileName.IndexOf("/", 4) + 1);

                   fileName = fileName.Split('-')[0];

                   bool flag = false;
                   if (DMS.Security.getUserID() == 0)
                   {
                       context.Response.Redirect("~/Images/notAllowed.jpg");
                   }
                   else
                   {
                       Int32 userID = DMS.Security.getUserID();
                       DMS.DAL.operations op = new DMS.DAL.operations();
                       tables.dbo.userFolders userFldr = new tables.dbo.userFolders();
                       tables.dbo.documents doc = new tables.dbo.documents();
                       doc = op.dboGetDocumentsByPrimaryKey(Convert.ToInt32(fileName));
                       if (doc.hasRows)
                       {
                           op = new DMS.DAL.operations();
                           userFldr = op.dboGetUserFoldersByPrimaryKey(doc.fieldFldrID, userID);
                           if (userFldr.hasRows)
                           {
                               flag = true;
                           }
                           else
                           {
                               tables.dbo.userDocuments userDocs = new tables.dbo.userDocuments();
                               op = new DMS.DAL.operations();
                               userDocs = op.dboGetUserDocumentsByPrimaryKey(doc.fieldDocID, userID);
                               if (userDocs.hasRows)
                               {
                                   flag = true;
                               }

                               op = new DMS.DAL.operations();
                               tables.dbo.documentWFPath dwf = op.dboGetAllDocumentWFPath("docID=" + doc.fieldDocID.ToString() + " and userID=" + userID.ToString());
                               if (dwf.hasRows)
                                   flag = true;
                           }

                           if (!flag)
                           {

                               context.Response.Redirect("~/Images/notAllowed.jpg");
                           }
                           else
                           {
                               StartDownload(file);
                           }
                       }
                       else
                       {
                           context.Response.Redirect("~/Images/notAllowed.jpg");
                       }

                   }
               }
           }
           else
           {
               context.Response.ContentType = "text/plain";
               context.Response.Write("File cannot be found!");
           }
       }
       else
       {
           context.Response.ContentType = "text/plain";
           context.Response.Write("You not authenticated!");
       }
        }

        private void StartDownload(string downloadFile)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Buffer = true;
            context.Response.Clear();
            context.Response.AddHeader(
               "content-disposition",
               "attachement; filename=" + downloadFile);
            string contentType = "application/unknown";
            string ext = downloadFile.Split('.')[1].ToLower();
            switch (ext)
            { 
                case "jpg":
                    contentType = "image/jpeg";
                    break;
                case "tif":
                    contentType = "image/tiff";
                    break;
                case "avi":
                    contentType = "video/avi";
                    break;
                case "mp4":
                    contentType = "video/mpeg";
                    break;
                case "pdf":
                    contentType = "application/pdf";
                    break;
                case "docx":
                    contentType = "Content-type: application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case "xlsx":
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

            }
            context.Response.ContentType = contentType;
            context.Response.WriteFile(
                downloadFile);
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