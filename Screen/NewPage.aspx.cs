using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Araneas_ERP.screen
{
    public partial class NewPage : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (c.convertToString(Session["userId"]) == "")
            {
                Response.Redirect("../Screen/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }

            try
            {
                
            }
            catch { }
        }
    }
}