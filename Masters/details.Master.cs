using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Araneas_ERP.Masters
{
    public partial class details : System.Web.UI.MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("../Screen/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["CODEN"] != "")
            {
                string CodeN = Request.QueryString["CODEN"];
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();

                //Araneas.DAL.operations op = new Araneas.DAL.operations();
                //tables.dbo.B_ENTITIES b_Ent = new tables.dbo.B_ENTITIES();
                //b_Ent = op.dboGetB_ENTITIESByPrimaryKey(c.convertToInt32(CodeN));
                //if (b_Ent.hasRows)
                //{
                //    c.fillData(b_Ent.table, 0, b_Ent.columnsArray, Page);
                //}
            }
            else
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                Int32 userID = c.convertToInt32(Session["userId"]);

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