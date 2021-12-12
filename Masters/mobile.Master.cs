using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Masters
{
    public partial class mobile : System.Web.UI.MasterPage
    {
        public Int32 userID;
        SecurityNB.TextEncryption sec = new SecurityNB.TextEncryption();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();


        protected override void OnInit(EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("../M/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DMS.DAL.operations op = new DMS.DAL.operations();
            tables.dbo.users u = new tables.dbo.users();
            
            if (c.convertToString(Session["userId"]) == "")
            {
                //pnlLogged.Visible = false;
                Response.Redirect("../M/Login.aspx");
            }

        }
    }
}