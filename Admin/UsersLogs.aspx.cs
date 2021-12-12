using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DAL;
namespace dms.Admin
{
    public partial class UsersLogs : System.Web.UI.Page
    {
        operations op = new operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                op = new operations();
                tables.dbo.users users = op.dboGetAllUsers();
                c.FillDropDownList(drpUsers, users.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "userID", "fullName");

            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            DateTime fromDate = DateTime.Today;DateTime toDate= DateTime.Today;

            if (txtFromDate.Text.Trim() == "")
            {
                txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
            else
            {
                fromDate = c.convertToDateTime(txtFromDate.Text);
            }

            if (txtToDate.Text.Trim() == "")
            {
                txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
            else
            {
                toDate = c.convertToDateTime(txtToDate.Text);
            }

            if (toDate < fromDate)
            {
                lblRes.Text = "يجب أن يكون تاريخ البداية أقل أو تساوي تاريخ النهاية";
            }
            else {
                lblRes.Text = "";
                string sqlDB = "select * from [dbo].[showAllDBEvents] where [userID]=" + drpUsers.SelectedValue + " and [eventDateTime] between '" + fromDate.ToString("MM/dd/yyyy") + " 00:00:00' and '" + toDate.ToString("MM/dd/yyyy") + " 23:59:59'";
                string sqlBrowse = "select * from [dbo].[showAllBrowsingEvents] where [userID]=" + drpUsers.SelectedValue + " and [eventDateTime] between '" + fromDate.ToString("MM/dd/yyyy") + " 00:00:00' and '" + toDate.ToString("MM/dd/yyyy") + " 23:59:59'";
                string sqlLogins = "select * from [dbo].[showAllLoginEvents] where [userID]=" + drpUsers.SelectedValue + " and [eventDateTime] between '" + fromDate.ToString("MM/dd/yyyy") + " 00:00:00' and '" + toDate.ToString("MM/dd/yyyy") + " 23:59:59'";

                grdShowAllDBEvents.DataSource = c.GetDataAsDataTable(sqlDB);
                grdShowAllDBEvents.DataBind();

                grdShowAllBrowsingEvents.DataSource = c.GetDataAsDataTable(sqlBrowse);
                grdShowAllBrowsingEvents.DataBind();

                grdShowAllLoginEvents.DataSource = c.GetDataAsDataTable(sqlLogins);
                grdShowAllLoginEvents.DataBind();
            }

                
        }
    }
}