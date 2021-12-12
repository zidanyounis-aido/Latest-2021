using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Reports
{
    public partial class Calculate : System.Web.UI.Page
    {
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        DMS.DAL.operations op = new DMS.DAL.operations();
        Int32 fldrID = 0;
        Int32 userID;
        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                grdDocuments.Columns[0].HeaderText = "المنفذ";
                grdDocuments.Columns[1].HeaderText = "قيمة البضاعة بالدينار العراقي";
                grdDocuments.Columns[2].HeaderText = "المبلغ المقبوض بالدينار العراقي";
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            userID = c.convertToInt32(Session["userId"]);
            if (!IsPostBack) { 
            }
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            fillDocuments();
        }

        private void fillDocuments()
        {
            if (txtDateTo.Text == "")
                txtDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if (txtDateFrom.Text == "")
                txtDateFrom.Text = txtDateTo.Text;

            string[] fromAr = txtDateFrom.Text.Split('/');
            string fromDate = fromAr[1] + "/" + fromAr[0] + "/" + fromAr[2];

            string[] toAr = txtDateTo.Text.Split('/');
            string toDate = toAr[1] + "/" + toAr[0] + "/" + toAr[2];

            string sql = "select [fldrNameAr],sum(CONVERT(DECIMAL(16,3),isnull(Replace(Meta7,',',''),0))) as sum1,sum(CONVERT(DECIMAL(16,3),isnull(Replace(Meta11,',',''),0))) as sum2"
                + " from[dbo].[folders] inner join[dbo].[documents]"
                + " on[dbo].[folders].[fldrID] = [dbo].[documents].[fldrID]"
                + " where[addedDate] between '" + fromDate + "' and '" + toDate + "'"
                + " group by[fldrNameAr]";
            DataTable dt = new DataTable();
            dt = c.GetDataAsDataTable(sql);
            grdDocuments.DataSource = dt;
            grdDocuments.DataBind();
            double sum1 = Convert.ToDouble(dt.Compute("sum(sum1)", ""));
            double sum2 = Convert.ToDouble(dt.Compute("sum(sum2)", ""));
            if (Session["lang"].ToString() == "1")
                grdDocuments.FooterRow.Cells[0].Text = "المجموع";
            else
                grdDocuments.FooterRow.Cells[0].Text = "Total";

            grdDocuments.FooterRow.Cells[1].Text = sum1.ToString("#,0.00");
            grdDocuments.FooterRow.Cells[2].Text = sum2.ToString("#,0.00");
        }

        protected void grdDocuments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDocuments.PageIndex = e.NewPageIndex;
            fillDocuments();
        }

        protected void grdDocuments_SelectedIndexChanged(object sender, EventArgs e)
        {
            c = new CommonFunction.clsCommon();
            string docID = grdDocuments.SelectedRow.Cells[0].Text;
            string res = c.encrypt(docID);
            Response.Redirect("../Screen/documentInfo.aspx?docID=" + res);
        }

    }
}