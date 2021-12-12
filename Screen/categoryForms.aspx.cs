using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace dms.Screen
{
    public partial class categoryForms : System.Web.UI.Page
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
                    if (DMS.Security.isNotAllowedCharacters(parentID))
                    {
                        string sql = "SELECT     dbo.programs.* FROM         dbo.userPrograms INNER JOIN "
                                + " dbo.programs ON dbo.userPrograms.programID = dbo.programs.programID"
                                + " WHERE     (dbo.userPrograms.userID = " + Session["userID"].ToString() + " and dbo.programs.parentProgramID=" + parentID + ")";

                        tables.dbo.eForms forms = new tables.dbo.eForms();
                        op = new DMS.DAL.operations();
                        forms = op.dboGetAllEForms("catPrgID=" + parentID);

                        //DataTable dt = new DataTable();
                        //dt = c.GetDataAsDataTable(sql);
                        rptMainIcons.DataSource = forms.table;
                        rptMainIcons.DataBind();
                    }
                }
            }
            catch { }
        }
    }
}