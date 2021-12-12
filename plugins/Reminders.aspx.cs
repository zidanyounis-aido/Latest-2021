using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace dms.plugins
{
    public partial class Reminders : System.Web.UI.Page
    {
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
            {
                
                string sql = "";
                DataTable dt = new DataTable();
                if (Session["lang"].ToString() == "0")
                {
                    sql = "select docID,'Reminder for document (' + docName + ') based on feild (' + metadesc + ')' from dbo.userReminders where userid=" + Session["userId"].ToString();
                }
                else
                {
                    sql = "select docID,N'تذكير على المستند (' + docName + N') بناءً على الحقل (' + metadescAr + N')' as ReminderText from dbo.userReminders where userid=" + Session["userId"].ToString();
                }
                dt = c.GetDataAsDataTable(sql);
                rptReminders.DataSource = dt;
                rptReminders.DataBind();
            }
        }
    }
}