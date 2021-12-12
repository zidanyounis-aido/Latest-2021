using dms.VM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Screen
{
    public partial class DefaultArbak : System.Web.UI.Page
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
                Label3.Text = "بحث";
                drpSortBy.Items.FindByValue("docID").Text = "تسلسل";
                drpSortBy.Items.FindByValue("docName").Text = "اسم الملف";
                drpSortBy.Items.FindByValue("docTypID").Text = "نوع الملف";
                drpSortBy.Items.FindByValue("addedDate").Text = "التاريخ المضاف";
                drpSortBy.Items.FindByValue("addedUserID").Text = "المستخدم المضاف";
                drpSortBy.Items.FindByValue("modifyDate").Text = "تاريخ التعديل";
                rdoOrderType.Items.FindByValue("asc").Text = "تصاعدي";
                rdoOrderType.Items.FindByValue("desc").Text = "تنازلي";
                rdoOrderType.Attributes.Add("dir", "rtl");
                grdDocuments.Columns[0].HeaderText = "تسلسل";
                grdDocuments.Columns[1].HeaderText = "اسم الملف";
                grdDocuments.Columns[2].HeaderText = "المجلد";
                grdDocuments.Columns[3].HeaderText = "النوع";
                grdDocuments.Columns[4].HeaderText = "الإضافة";
                grdDocuments.Columns[5].HeaderText = "المستخدم";
                grdDocuments.Columns[6].HeaderText = "تاريخ التعديل";
                grdDocuments.Columns[7].HeaderText = "الحالة";
                grdDocuments.Columns[8].HeaderText = "الوقت المتبقي";

            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            userID = c.convertToInt32(Session["userId"]);
            fillDocuments("");
            fillStstusDrop();
        }
        public void fillStstusDrop()
        {
            if (Session["lang"].ToString() != "0")
            {
                ddlStatusFilter.Items[0].Text = "الكل";
                ddlStatusFilter.Items[1].Text = "قيد الإجراء";
                ddlStatusFilter.Items[2].Text = "مؤرشف";
                ddlStatusFilter.Items[3].Text = "متأخر";
            }
            //ddlStatusFilter.Insert(0, new ListItem("", "Select a department..."))
        }
        public string getFolderName(string fldrID)
        {
            string fldrFeild = (Session["lang"].ToString() == "1") ? "fldrNameAr" : "fldrName";
            if (fldrID != "")
            {
                string res = Convert.ToString(c.GetDataAsScalar("select " + fldrFeild + " from folders where fldrID=" + fldrID));
                return res;
            }
            else
                return "";
        }

        private void fillDocuments(string cond = "", Int32 PageIndex = 1)
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
            if (fldrs.Length > 0)
                fldrs = fldrs.Remove(fldrs.Length - 1);
            if (cond.Trim() != "")
                cond = "(" + cond + ") and ";

            cond += "(FldrID in (" + fldrs + "))";
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
                docs = DMS.BLL.specialCases.getDocumentsByPage(cond.Trim(), PageIndex, 50, drpSortBy.SelectedValue + " " + rdoOrderType.SelectedValue, int.Parse(Session["userId"].ToString()), "and docTypID in(1,23)", Session["lang"].ToString());
                List<DocumentsVM> documentsVMs = new List<DocumentsVM>();
                foreach (DataRow row in docs.Rows)
                {
                    DocumentsVM obj = new DocumentsVM();
                    obj.addedDate = row["addedDate"].ToString() != "" ? DateTime.Parse(row["addedDate"].ToString()) : DateTime.Now;
                    obj.addedUserID = int.Parse(row["addedUserID"].ToString());
                    obj.Color = row["Color"] != null ? row["Color"].ToString() : "";
                    obj.docID = int.Parse(row["docID"].ToString());
                    obj.fldrID = int.Parse(row["fldrID"].ToString());
                    obj.docName = row["docName"] != null ? row["docName"].ToString() : "";
                    obj.docTypID = row["docTypID"] != null ? int.Parse(row["docTypID"].ToString()) : 0;
                    obj.Meta2 = row["docTypID"] != null ? row["Meta2"].ToString() : "";
                    obj.Meta4 = row["docTypID"] != null ? row["Meta4"].ToString() : "";
                    obj.modifyDate = row["docTypID"].ToString() != "" ? DateTime.Parse(row["modifyDate"].ToString()) : (DateTime?)null;
                    if (Session["lang"].ToString() == "0")
                    {
                        obj.statusName = row["statusName"] != null ? row["statusName"].ToString() : "in process";
                    }
                    else
                    {
                        obj.statusName = row["statusName"] != null ? row["statusName"].ToString() : "قيد الإجراء";
                    }
                    obj.submitDate = row["submitDate"].ToString() != "" ? DateTime.Parse(row["submitDate"].ToString()) : DateTime.Parse(row["addedDate"].ToString());
                    obj.WfStartDateTime = row["WfStartDateTime"].ToString() != "" ? DateTime.Parse(row["WfStartDateTime"].ToString()) : (DateTime?)null;
                    obj.WfStatus = row["WfStatus"] != null ? int.Parse(row["WfStatus"].ToString()) : 0;
                    obj.WfTimeFrame = row["WfTimeFrame"] != null ? decimal.Parse(row["WfTimeFrame"].ToString()) : 0;
                    obj.durationType = int.Parse(row["durationType"].ToString());
                    obj.duration = int.Parse(row["duration"].ToString());
                    if (obj.durationType != -1 && obj.duration != -1 && obj.duration > 0)
                    {
                        int durationMuliplie = obj.durationType == 1 ? 1 : 24;
                        var totalHours = (obj.submitDate.Value.AddHours(obj.duration * durationMuliplie) - DateTime.Now).TotalHours;
                        if (totalHours > 0)
                        {
                            string h = Session["lang"].ToString() == "0" ? "Hour" : "ساعة" ;
                            obj.LeftTime = Math.Round(totalHours, 1).ToString() + h;
                            if (obj.durationType == 2)
                            {
                                h = Session["lang"].ToString() == "0" ? "Day" : "يوم";
                                obj.LeftTime = Math.Round((totalHours / 24), 1).ToString() + h;
                            }
                            if (obj.durationType == 1 && totalHours < 1 && totalHours > 0)
                            {
                                h = Session["lang"].ToString() == "0" ? "Minute" : "دقيقة";
                                obj.LeftTime = Math.Round((totalHours * 60 ), 1).ToString() + h;
                            }
                        }
                        else
                        {
                            if (row["statusId"].ToString() != "2")
                            {
                                obj.LeftTime = "0";
                                obj.statusName = Session["lang"].ToString() == "0" ? "Late" : "متأخر";
                                //update object with sts 3 late
                                //CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                                // DataTable dt = c.GetDataAsDataTable("select * from users");
                                // c.NonQuery("update dbo.documents set statusId=3 where dbo.documents.docID=" + obj.docID);
                            }

                        }
                    }
                    else
                    {
                        obj.LeftTime = "∞";
                    }
                    documentsVMs.Add(obj);
                    //TextBox1.Text = row["ImagePath"].ToString();
                }
                grdDocuments.DataSource = documentsVMs;
                // grdDocuments.DataSource = docs;
                grdDocuments.DataBind();
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
        protected void ddlStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cond = "documents.statusId=" + ddlStatusFilter.SelectedValue;
            if (ddlStatusFilter.SelectedValue == "0")
            {
                cond = "";
            }
            else if (ddlStatusFilter.SelectedValue == "1")
            {
                cond = "(documents.statusId=" + ddlStatusFilter.SelectedValue + " or documents.statusId is null)";
            }
            fillDocuments(cond, 1);
        }
    }
}