using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace DMS.screen
{
    public partial class _default_old : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        public Int32 userID;


        SecurityNB.TextEncryption sec = new SecurityNB.TextEncryption();

        DataTable treeDT = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (c.convertToString(Session["userId"]) == "")
            {
                Response.Redirect("../Screen/login.aspx");
            }
            else
            {
                userID = c.convertToInt32(Session["userId"]);
            }

            string sql = "SELECT     dbo.programs.* FROM         dbo.userPrograms INNER JOIN "
                        + " dbo.programs ON dbo.userPrograms.programID = dbo.programs.programID"
                        + " WHERE     (dbo.userPrograms.userID = " + userID.ToString() + " and dbo.programs.parentProgramID=0)";

            DataTable dt = new DataTable();
            dt = c.GetDataAsDataTable(sql);
            rptMainIcons.DataSource = dt;
            rptMainIcons.DataBind();


            //op.dboAddUsers(

            //op.dboAddB_SYSTEM(
        }
    }
}