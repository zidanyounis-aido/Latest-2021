using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Masters
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        public Int32 userID;
        SecurityNB.TextEncryption sec = new SecurityNB.TextEncryption();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();

        protected override void OnInit(EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("../Screen/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            linkLtr.Attributes["href"] = (Session["lang"].ToString() == "0") ? "/Assets/UIKIT/css/Style-LTR.css" : "";
            linkBootstrap.Attributes["href"] = (Session["lang"].ToString() == "0") ? "" : "/Assets/UIKIT/css/bootstrap-rtl.min.css";
            if (!IsPostBack)
            {
                if (c.convertToString(Session["userId"]) == "")
                {
                    Response.Redirect("../Screen/login.aspx?URL=" + Request.Url.AbsoluteUri);
                }
                else
                {
                    userID = c.convertToInt32(Session["userId"]);

                    if (DMS.Security.checkAllowedPage(userID, Request.Url.AbsolutePath))
                    {
                        DMS.DAL.operations op = new DMS.DAL.operations();
                        Int32 eventID = op.dboAddSysEvents(userID, 2, DateTime.Now, Request.Url.AbsoluteUri);
                        op = new DMS.DAL.operations();
                        op.dboAddBrowseingEvents(eventID, DMS.Security.getProgramID(Request.Url.AbsolutePath));
                    }
                    else
                    {
                        Response.Redirect("../screen/notAllowed.html");
                    }
                }
            }
        }

      
    }
}