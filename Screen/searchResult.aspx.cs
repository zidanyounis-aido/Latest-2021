using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Screen
{
    public partial class searchResult : System.Web.UI.Page
    {
        dms.Masters.DocumentsMaster m = new Masters.DocumentsMaster();
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        DMS.DAL.operations op = new DMS.DAL.operations();
        Int32 fldrID = 0;
        Int32 userID;

        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                drpSortBy.Items.FindByValue("docID").Text = "تسلسل";
                drpSortBy.Items.FindByValue("docName").Text = "اسم الملف";
                drpSortBy.Items.FindByValue("docTypID").Text = "نوع الملف";
                drpSortBy.Items.FindByValue("addedDate").Text = "التاريخ المضاف";
                drpSortBy.Items.FindByValue("addedUserID").Text = "المستخدم المضاف";
                drpSortBy.Items.FindByValue("modifyDate").Text = "تعديل التاريخ";
                rdoOrderType.Items.FindByValue(" ").Text = "تصاعدي";
                rdoOrderType.Items.FindByValue("desc").Text = "تنازلي";
                grdDocuments.Columns[0].HeaderText = "تسلسل";
                grdDocuments.Columns[1].HeaderText = "اسم الملف";
                grdDocuments.Columns[2].HeaderText = "المجلد";
                grdDocuments.Columns[3].HeaderText = "النوع";
                grdDocuments.Columns[4].HeaderText = "الإضافة";
                grdDocuments.Columns[5].HeaderText = "المستخدم";
                grdDocuments.Columns[6].HeaderText = "تاريخ التعديل";

            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            if (c.convertToString(Session["userId"]) == "")
            {
                Response.Redirect("../Screen/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }

            m = (dms.Masters.DocumentsMaster)Page.Master;

            userID = c.convertToInt32(Session["userId"]);
            if (!IsPostBack)
            {
                string searchText = Request.QueryString["stxt"];
                txtSearch.Text = searchText;
                
                

                if(DMS.Security.isNotAllowedCharacters(searchText))
                {
                fillDocuments("docName like N'%" + searchText + "%' or Meta1 like N'%" + searchText + "%' or  Meta2 like N'%" + searchText + "%' or  Meta3 like N'%" + searchText + "%'");
                }
                else
                {
                    txtSearch.Text = "Illegal operation !!";
                }
            }
        }

        void Page_PreInit(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["isDiwan"]))
            {
                this.MasterPageFile = "~/Masters/DocumentsMaster.master";
            }
            else
            {
                this.MasterPageFile = "~/Masters/DiwanMaster.master";
            }
        }

        public string getFolderName(string fldrID)
        {
            if (fldrID != "")
            {
                string res = Convert.ToString(c.GetDataAsScalar("select fldrName from folders where fldrID=" + fldrID));
                return res;
            }
            else
                return "";
        }

        private void fillDocuments(string cond = "")
        {
            string fldrs = "";
            op = new DMS.DAL.operations();
            tables.dbo.userFolders userDocs = new tables.dbo.userFolders();
            userDocs = op.dboGetAllUserFolders("userID=" + Session["userID"].ToString());
            for (Int32 i = 0; i < userDocs.rowsCount; i++)
            {
                fldrs += userDocs.fieldFldrID.ToString() + ",";
                userDocs.moveNext();
            }
            fldrs = fldrs.Remove(fldrs.Length - 1);
            if (cond.Trim() != "")
                cond = "(" + cond + ") and ";

            cond += "(FldrID in (" + fldrs + "))";

            op = new DMS.DAL.operations();
            tables.dbo.documents docs = new tables.dbo.documents();
            docs = op.dboGetAllDocuments(cond.Trim()) ;
            docs.table.DefaultView.Sort = drpSortBy.SelectedValue + " " + rdoOrderType.SelectedValue;
            grdDocuments.DataSource = docs.table;
            grdDocuments.DataBind();
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

        protected void drpSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDocuments();
        }

        protected void rdoOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDocuments();
        }

        protected void grdDocuments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            c = new CommonFunction.clsCommon();
            string docID = grdDocuments.Rows[e.RowIndex].Cells[0].Text;
            op = new DMS.DAL.operations();
            op.dboDeleteDocumentVersions("docID=" + docID);

            op = new DMS.DAL.operations();
            op.dboDeleteDocumentsByPrimaryKey(c.convertToInt32(docID));

            fillDocuments();
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            fillDocuments("docName like '%" + txtSearch.Text + "%'");
        }
    }
}