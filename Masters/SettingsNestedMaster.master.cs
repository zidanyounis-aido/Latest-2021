using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Masters
{
    public partial class SettingsNestedMaster : System.Web.UI.MasterPage
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
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
                                + " WHERE     (dbo.userPrograms.userID = " + Session["userID"].ToString() + " and dbo.programs.parentProgramID=" + 2 + ")";

                        DataTable dt = new DataTable();
                        dt = c.GetDataAsDataTable(sql);
                        rptSettingsMainIcons.DataSource = dt;
                        rptSettingsMainIcons.DataBind();
                    }
                }
                catch { }
            }
        }
        public string GetProgramName(int ProgramID)
        {
            try {
                return c.GetDataAsScalar("Select " + (Session["lang"].ToString() == "0" ? "programName" : "programNameAr") + " From programs Where programID=" + ProgramID).ToString();
            }
            catch {
                return "";
            }
        }
        public string GetProgramURL(int ProgramID)
        {
            try
            {
                return c.GetDataAsScalar("Select programURL From programs Where programID=" + ProgramID).ToString();
            }
            catch
            {
                return "";
            }
        }
    }
}