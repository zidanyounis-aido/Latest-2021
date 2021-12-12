using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Araneas_ERP.screen
{
    public partial class subIcons : System.Web.UI.Page
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
                string parentID = c.convertToString(Request.QueryString["CODEN"]);
                if (parentID != "")
                {
                    string sql = "SELECT     dbo.programs.* FROM         dbo.userPrograms INNER JOIN "
                            + " dbo.programs ON dbo.userPrograms.programID = dbo.programs.programID"
                            + " WHERE     (dbo.userPrograms.userID = " + Session["userID"].ToString() + " and dbo.programs.parentProgramID=" + parentID + ") order by dbo.programs.orderNum";

                    DataTable dt = new DataTable();
                    dt = c.GetDataAsDataTable(sql);
                    dlMainIcons.DataSource = dt;
                    dlMainIcons.DataBind();
                }
            }
            catch { }
        }
    }
}