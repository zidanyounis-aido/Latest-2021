using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Reports
{
    public partial class performance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            #region fillChart1
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            string[] fromAr = txtDateFrom.Text.Split('/');
            string fromDate = fromAr[1] + "/" + fromAr[0] + "/" + fromAr[2];

            string[] toAr = txtDateTo.Text.Split('/');
            string toDate = toAr[1] + "/" + toAr[0] + "/" + toAr[2];

            string sql = "select count(docID) as docsCount, fullName from"
                + " documents inner join users on documents.addedUserID = users.userID"
                + " where addedDate between '" + fromDate + "' and '" + toDate + "'"
                + " group by fullName";
            DataTable recent = c.GetDataAsDataTable(sql);

            StringBuilder script = new StringBuilder();
            script.AppendLine("<script type='text/javascript'>");
            script.AppendLine("$(function () {");

            script.AppendLine("var options = {");
            script.AppendLine("exportEnabled: true,");
            script.AppendLine("animationEnabled: true,");
            //script.AppendLine("title: {");
            //script.AppendLine("    text: \"Attendance Ratio\"");
            //script.AppendLine("},");
            //script.AppendLine("height:200,");
            //script.AppendLine("width:100%,");
            script.AppendLine("data: [");
            script.AppendLine("{");
            script.AppendLine("type: \"column\", //change it to line, area, bar, pie, etc");
            
            script.AppendLine("dataPoints: [");

            for (Int32 j = 0; j < recent.Rows.Count; j++)
            {
                
                script.AppendLine("	{ label: \"" + recent.Rows[j]["fullName"].ToString() + "\", y: " + recent.Rows[j]["docsCount"].ToString() + " },");
            }
            script.AppendLine("]");
            script.AppendLine("}");
            script.AppendLine("]");
            script.AppendLine("};");

            script.AppendLine("$(\"#chartContainer\").CanvasJSChart(options);");

            script.AppendLine("});");
            script.AppendLine("</script>");

            ltrScript.Text = script.ToString();
            #endregion
        }
    }
}