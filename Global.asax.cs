using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace dms
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(Object sender, EventArgs e)
        {
            //this.Application.Add("TestItem", new object[] { "one", "two", "three" });
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            
            
            
            
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            string fullOrigionalpath = Request.Url.AbsoluteUri;

            if (fullOrigionalpath.Contains("/Archive"))
            {
                Context.RewritePath("/screen/documentsList.aspx?fldrID=oa39HL8adWejTNKlR9Zy2A==");
            }

            //if (Request.Url.AbsolutePath.ToLower().Contains("uploads/"))
            //{

            //    string fileName = Request.Url.AbsolutePath;
            //    fileName = fileName.Substring(fileName.IndexOf("/", 4) + 1);

            //    Response.Redirect("Validation.ashx?file=" + fileName);
            //}
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            
        }


        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
            

        }

    }
}
