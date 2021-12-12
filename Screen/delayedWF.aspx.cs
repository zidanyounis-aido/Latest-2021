using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Screen
{
    public partial class delayedWF : System.Web.UI.Page
    {
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        DMS.DAL.operations op = new DMS.DAL.operations();

        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                
                grdDocuments.Columns[0].HeaderText = "التسلسل";
                grdDocuments.Columns[1].HeaderText = "اسم الملف";

            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            if (c.convertToString(Session["userId"]) == "")
            {
                Response.Redirect("../Screen/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }

            if (!IsPostBack)
            {
                fillDocuments();
            }
        }


        private void fillDocuments()
        {
            System.Data.DataTable DT = new System.Data.DataTable();
            DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
            DT = sp.getDelayedDocuemnts();
            grdDocuments.DataSource = DT;
            grdDocuments.DataBind();

            if (DT.Rows.Count == 0)
            {
                pnlEmpty.Visible = true;
            }
        }

        public string getDocTypeDesc(Int32 docTypID)
        {
            string res = "";
            op = new DMS.DAL.operations();
            try
            {
                if (Session["lang"].ToString() == "0")
                    res = op.dboGetDocTypesByPrimaryKey(docTypID).fieldDocTypDesc;
                else
                    res = op.dboGetDocTypesByPrimaryKey(docTypID).fieldDocTypDescAr;
            }
            catch { }
            return res;
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


        public string getFolderName(string fldrID)
        {
            if (fldrID != "")
            {
                string res;
                if (Session["lang"].ToString() == "0")
                    res = Convert.ToString(c.GetDataAsScalar("select fldrName from folders where fldrID=" + fldrID));
                else
                    res = Convert.ToString(c.GetDataAsScalar("select fldrNameAr from folders where fldrID=" + fldrID));
                return res;
            }
            else
                return "";
        }
    }
}