using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Masters
{
    public partial class subPages : System.Web.UI.MasterPage
    {

        protected override void OnInit(EventArgs e)
        {
           // themeLink.Text = "<link runat=\"server\" href=\"../assets/" + dms.sysSettings.getSettingValue("client") + "/css/theme.css\" rel=\"stylesheet\" />";
            if (Session["userId"] == null)
            {
                Response.Redirect("../Screen/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Localize();
            linkLtr.Attributes["href"] = (Session["lang"].ToString() == "0") ? "/Assets/UIKIT/css/Style-LTR.css" : "" ;
            linkBootstrap.Attributes["href"] = (Session["lang"].ToString() == "0") ? "" : "/Assets/UIKIT/css/bootstrap-rtl.min.css";
            if (Session["userId"] == null)
            {
                Response.Redirect("../Screen/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }
            else
            {
                //if (Session["lang"].ToString() != "0")
                    //cssLink.Href = "../Styles/SiteAr.css";

                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
              Int32  userID = c.convertToInt32(Session["userId"]);
                 
                if(DMS.Security.checkAllowedPage(userID,Request.Url.AbsolutePath))
                {
                    DMS.DAL.operations op = new DMS.DAL.operations();
                    Int32 eventID = op.dboAddSysEvents(userID, 2, DateTime.Now, Request.Url.AbsoluteUri);
                    op = new DMS.DAL.operations();
                    op.dboAddBrowseingEvents(eventID,DMS.Security.getProgramID(Request.Url.AbsolutePath));
                }
                else
                {
                    Response.Redirect("../screen/notAllowed.html");
                }
            }
        }
        public void Localize()
        {
            if (Session["lang"] != null && Session["lang"].ToString() == "0")
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            else
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("ar");
        }
    }
}