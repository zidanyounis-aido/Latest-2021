using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Screen
{
    public partial class DefaultDiwan : System.Web.UI.Page
    {
        dms.Masters.DocumentsMaster m = new Masters.DocumentsMaster();
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        DMS.DAL.operations op = new DMS.DAL.operations();
        Int32 fldrID = 0;
        Int32 typeId = 0;
        Int32 userID;
        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                Label3.Text = "بحث";
                txtSearch.Attributes["placeholder"] = "بحث";
                drpSortBy.Items.FindByValue("docID").Text = "تسلسل";
                drpSortBy.Items.FindByValue("docName").Text = "اسم الملف";
                drpSortBy.Items.FindByValue("docTypID").Text = "نوع الملف";
                drpSortBy.Items.FindByValue("addedDate").Text = "التاريخ المضاف";
                drpSortBy.Items.FindByValue("addedUserID").Text = "المستخدم المضاف";
                drpSortBy.Items.FindByValue("modifyDate").Text = "تعديل التاريخ";
                rdoOrderType.Items.FindByValue("asc").Text = "تصاعدي";
                rdoOrderType.Items.FindByValue("desc").Text = "تنازلي";
                rdoOrderType.Attributes.Add("dir", "rtl");
                grdDocuments.Columns[0].HeaderText = "#";
                grdDocuments.Columns[1].HeaderText = "كود";
                grdDocuments.Columns[2].HeaderText = "اسم الملف";
                grdDocuments.Columns[3].HeaderText = "المجلد";
                grdDocuments.Columns[4].HeaderText = "النوع";
                grdDocuments.Columns[5].HeaderText = "الإضافة";
                grdDocuments.Columns[6].HeaderText = "المستخدم";
                grdDocuments.Columns[7].HeaderText = "تاريخ التصدير";
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            userID = c.convertToInt32(Session["userId"]);
            // if (!string.IsNullOrEmpty(Request.QueryString["typeId"]))
            // {
            typeId = 1;//int.Parse(Request.QueryString["typeId"]);
            //}
            fillDocuments();

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

        private void fillDocuments(string cond = "", Int32 PageIndex = 1)
        {
            string fldrs = "";
            if (string.IsNullOrEmpty(Request.QueryString["isDiwan"]))
            {
                op = new DMS.DAL.operations();
                tables.dbo.userFolders userDocs = new tables.dbo.userFolders();
                userDocs = op.dboGetAllUserFolders("userID=" + Session["userID"].ToString());
                for (Int32 i = 0; i < userDocs.rowsCount; i++)
                {
                    fldrs += userDocs.fieldFldrID.ToString() + ",";
                    userDocs.moveNext();
                }
                if (fldrs.Length > 0)
                    fldrs = fldrs.Remove(fldrs.Length - 1);
            }
            else
            {
                op = new DMS.DAL.operations();
                tables.dbo.folders diwanFolders = new tables.dbo.folders();
                diwanFolders = op.dboGetAllFolders("isDiwan=1");
                for (Int32 i = 0; i < diwanFolders.rowsCount; i++)
                {
                    fldrs += diwanFolders.fieldFldrID.ToString() + ",";
                    diwanFolders.moveNext();
                }
                if (fldrs.Length > 0)
                    fldrs = fldrs.Remove(fldrs.Length - 1);
            }
            if (cond.Trim() != "")
                cond = "(" + cond + ") and ";
            if (fldrs != "")
            {
                cond += "(FldrID in (" + fldrs + "))";
            }
            else
            {
                cond += "(FldrID in (0))";
            }
        
                if (typeId != 0)
                {
                    cond += " and TypeId=" + typeId;
                }
                else
                {
                    cond += " and TypeId in(1,2)";
               }
            
            if (!IsPostBack)
            {
                Int32 count = Convert.ToInt32(c.GetDataAsScalar("select count(*) from documents where " + cond));
                for (Int32 i = 1; i <= Convert.ToInt32(count / 50) + 1; i++)
                {
                    drpPager1.Items.Add(i.ToString());
                    //drpPager2.Items.Add(i.ToString());
                }
            }
            if (fldrs.Length > 0)
            {
                DataTable docs = new DataTable();
                docs = DMS.BLL.specialCases.getDocumentsByPage(cond.Trim(), PageIndex, 50, drpSortBy.SelectedValue + " " + rdoOrderType.SelectedValue, 0, "");
                grdDocuments.DataSource = docs;
                grdDocuments.DataBind();
                try
                {
                    grdDocuments.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grdDocuments.FooterRow.TableSection = TableRowSection.TableFooter;
                }
                catch (Exception)
                {
                }
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
            fillDocuments("", e.NewPageIndex);
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

        protected void drpPager1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drp = (DropDownList)sender;
            fillDocuments("", Convert.ToInt32(drp.SelectedValue));
        }
    }
}