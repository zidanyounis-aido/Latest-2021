using dms.VM;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Screen
{
    public partial class documentsList : System.Web.UI.Page
    {
        MasterPage m;

        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        DMS.DAL.operations op = new DMS.DAL.operations();
        Int32 fldrID = 0;
        Int32 userID;



        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                lblFolderName.Text = "اسم";
                Label3.Text = "بحث :";
                txtSearch.Attributes["placeholder"] = "بحث";
                drpSortBy.Items.FindByValue("docID").Text = "تسلسل";
                drpSortBy.Items.FindByValue("docName").Text = "اسم الملف";
                drpSortBy.Items.FindByValue("docTypID").Text = "النوع";
                drpSortBy.Items.FindByValue("addedDate").Text = "الإضافة";
                drpSortBy.Items.FindByValue("addedUserID").Text = "المستخدمين المضافين";
                drpSortBy.Items.FindByValue("modifyDate").Text = "تاريخ التعديل";
                rdoOrderType.Items.FindByValue(" ").Text = "تصاعدي";
                rdoOrderType.Items.FindByValue("desc").Text = "تنازلي";
                grdDocuments.Columns[0].HeaderText = "تسلسل";
                grdDocuments.Columns[1].HeaderText = "اسم الملف";
                grdDocuments.Columns[2].HeaderText = "النوع";
                grdDocuments.Columns[3].HeaderText = "تاريخ الإضافة";
                grdDocuments.Columns[4].HeaderText = "المستخدم";
                grdDocuments.Columns[5].HeaderText = "تاريخ  التعديل";
                grdDocuments.Columns[8].HeaderText = "الحالة";
                grdDocuments.Columns[9].HeaderText = "الوقت المتبقي";
                delDocumnetRowbtn1.Text = "نعم";
                lnkButtonDelete.Text = "نعم";
            }


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            if (c.convertToString(Session["userId"]) == "")
            {
                Response.Redirect("../Screen/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }
            if (string.IsNullOrEmpty(Request.QueryString["isDiwan"]))
                m = (dms.Masters.DocumentsMaster)Page.Master;
            else
                m = (dms.Masters.DiwanMaster)Page.Master;


            if (!string.IsNullOrEmpty(Request.QueryString["isDiwan"]))
            {
                lnkAdvanceSearch.NavigateUrl = "~/Screen/advancedSearch.aspx?isDiwan=true";
            }

            userID = c.convertToInt32(Session["userId"]);

            if (Request.QueryString["fldrID"] == "")
                Response.Redirect("../Screen/", true);
            else
                fldrID = c.convertToInt16(c.decrypt(Request.QueryString["fldrID"]));

            Session["fldrID"] = fldrID;
            lnkAddDoc.NavigateUrl = "../Screen/newDocument.aspx?fldrID=" + Request.QueryString["fldrID"];
            linkAddOutcome.NavigateUrl = "../Screen/newDocument.aspx?fldrID=" + Request.QueryString["fldrID"] + "&typeId=" + 2;
            lnkAdvanceSearch.NavigateUrl = "../Screen/advancedSearch.aspx?fldrID=" + Request.QueryString["fldrID"];

            if (!IsPostBack)
            {
                fillStstusDrop();
                tables.dbo.folders fldr = new tables.dbo.folders();
                op = new DMS.DAL.operations();
                fldr = op.dboGetFoldersByPrimaryKey(fldrID);
                if (Session["lang"].ToString() == "0")
                    lblFolderName.Text = fldr.fieldFldrName;
                else
                    lblFolderName.Text = fldr.fieldFldrNameAr;

                if (System.IO.File.Exists(Server.MapPath("../Images/dbIcons/") + fldr.fieldIconID.ToString() + "-32.png"))
                    fldrIcon.ImageUrl = "../Images/dbIcons/" + fldr.fieldIconID.ToString() + "-32.png";
                else
                    fldrIcon.ImageUrl = "../Images/Icons/folder-documents-icon.png";

                bool hasPer = false;
                op = new DMS.DAL.operations();
                tables.dbo.users userTbl = new tables.dbo.users();
                userTbl = op.dboGetUsersByPrimaryKey(userID);

                op = new DMS.DAL.operations();
                if (op.dboGetGroupFoldersByPrimaryKey(fldrID, userTbl.fieldGrpID).hasRows)
                    hasPer = true;
                else
                {
                    op = new DMS.DAL.operations();
                    if (op.dboGetUserFoldersByPrimaryKey(fldrID, userID).hasRows)
                        hasPer = true;
                }

                if (hasPer)
                {
                    tables.dbo.metas meta = new tables.dbo.metas();
                    fldr = new tables.dbo.folders();
                    op = new DMS.DAL.operations();
                    fldr = op.dboGetFoldersByPrimaryKey(fldrID);
                    op = new DMS.DAL.operations();
                    meta = op.dboGetAllMetas("DocTypID=" + fldr.fieldDefaultDocTypID.ToString());
                    if (meta.hasRows)
                    {
                        if (Session["lang"].ToString() == "0")
                        {
                            if (meta.rowsCount == 2)
                                grdDocuments.Columns[6].HeaderText = "To";
                            //grdDocuments.Columns[6].HeaderText = meta.fieldMetaDesc;

                            if (meta.rowsCount > 1)
                            {
                                meta.moveNext();
                                if (meta.rowsCount == 4)
                                    grdDocuments.Columns[7].HeaderText = "Date";
                                grdDocuments.Columns[8].HeaderText = "Status";
                                //grdDocuments.Columns[7].HeaderText = meta.fieldMetaDesc;
                            }
                            else
                            {
                                grdDocuments.Columns[7].Visible = false;
                            }
                        }
                        else
                        {
                            //grdDocuments.Columns[6].HeaderText = meta.fieldMetaDescAr;
                            grdDocuments.Columns[6].HeaderText = "التاريخ";
                            if (meta.rowsCount > 1)
                            {
                                meta.moveNext();
                                //grdDocuments.Columns[7].HeaderText = meta.fieldMetaDescAr;
                                grdDocuments.Columns[7].HeaderText = "الي";
                                //grdDocuments.Columns[8].HeaderText = "الحالة";
                            }
                            else
                            {
                                grdDocuments.Columns[7].Visible = false;
                            }
                        }
                    }
                    else
                    {
                        grdDocuments.Columns[7].Visible = false;
                        grdDocuments.Columns[6].Visible = false;
                    }
                    tables.dbo.userFolders userFldr = new tables.dbo.userFolders();
                    op = new DMS.DAL.operations();
                    userFldr = op.dboGetUserFoldersByPrimaryKey(fldrID, c.convertToInt32(Session["userID"]));
                    if (!userFldr.fieldAllow)
                        Response.Redirect("../Screen/DefaultAr.aspx");

                    fillDocuments("", 1, userFldr.fieldAllow);

                    if (!userFldr.fieldAllowInsert)
                        lnkAddDoc.Visible = false;
                }
                else
                    Response.Redirect("../Screen/DefaultAr.aspx", true);

            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
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

        private void fillDocuments(string cond = "", Int32 pageIndex = 1, bool allowView = false, bool ignoreDelayed = false)
        {
            if (cond.Trim() != "")
            {
                if (!cond.Trim().ToLower().StartsWith("and"))
                    cond = " and " + cond;
            }
            if (!IsPostBack)
            {
                Int32 count = Convert.ToInt32(c.GetDataAsScalar("select count(*) from documents where fldrID = " + fldrID.ToString() + " " + cond));

                for (Int32 i = 1; i <= Convert.ToInt32(count / 50) + 1; i++)
                {
                    drpPager1.Items.Add(i.ToString());
                    //drpPager2.Items.Add(i.ToString());
                }
            }
            DataTable docs = new DataTable();
            docs = DMS.BLL.specialCases.getDocumentsByPage("fldrID = " + fldrID.ToString() + " " + cond.Trim(), pageIndex, 50, drpSortBy.SelectedValue + " " + rdoOrderType.SelectedValue, allowView ? 0 : int.Parse(Session["userId"].ToString()), "", Session["lang"].ToString());
            //docs.DefaultView.Sort = drpSortBy.SelectedValue + " " + rdoOrderType.SelectedValue;
            // convert to ist of objects
            List<DocumentsVM> documentsVMs = new List<DocumentsVM>();
            foreach (DataRow row in docs.Rows)
            {
                DocumentsVM obj = new DocumentsVM();
                obj.addedDate = row["addedDate"].ToString() != "" ? DateTime.Parse(row["addedDate"].ToString()) : DateTime.Now;
                obj.addedUserID = int.Parse(row["addedUserID"].ToString());
                obj.Color = row["Color"] != null ? row["Color"].ToString() : "";
                obj.docID = int.Parse(row["docID"].ToString());
                obj.docName = row["docName"] != null ? row["docName"].ToString() : "";
                obj.docTypID = row["docTypID"] != null ? int.Parse(row["docTypID"].ToString()) : 0;
                obj.Meta2 = row["docTypID"] != null ? row["Meta2"].ToString() : "";
                obj.Meta4 = row["docTypID"] != null ? row["Meta4"].ToString() : "";
                obj.modifyDate = row["docTypID"].ToString() != "" ? DateTime.Parse(row["modifyDate"].ToString()) : (DateTime?)null;
                obj.typeId = row["typeId"].ToString() != "" ? row["typeId"].ToString() : "0";
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
                        string h = Session["lang"].ToString() == "0" ? "Hour" : "ساعة";
                        obj.LeftTime = Math.Round(totalHours, 1).ToString() + h;
                        if (obj.durationType == 2)
                        {
                            h = Session["lang"].ToString() == "0" ? "Day" : "يوم";
                            obj.LeftTime = Math.Round((totalHours / 24), 1).ToString() + h;
                        }
                        if (obj.durationType == 1 && totalHours < 1 && totalHours > 0)
                        {
                            h = Session["lang"].ToString() == "0" ? "Minute" : "دقيقة";
                            obj.LeftTime = Math.Round((totalHours * 60), 1).ToString() + h;
                        }
                    }
                    else
                    {
                        if (row["statusId"].ToString() != "2" && ignoreDelayed == false)
                        {
                            obj.LeftTime = "0";
                            obj.statusName = Session["lang"].ToString() == "0" ? "Late" : "متأخر";
                            //update object with sts 3 late
                            // CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                            // DataTable dt = c.GetDataAsDataTable("select * from users");
                            //c.NonQuery("update dbo.documents set statusId=3 where dbo.documents.docID=" + obj.docID);
                        }
                    }
                }
                else
                {
                    obj.LeftTime = "∞";
                }
                if (obj.statusName == "Archived" || obj.statusName == "مؤرشف")
                {
                    obj.LeftTime = "-";
                }
                documentsVMs.Add(obj);
                //TextBox1.Text = row["ImagePath"].ToString();
            }
            grdDocuments.DataSource = documentsVMs;

            grdDocuments.DataBind();
            try
            {
                grdDocuments.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdDocuments.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            catch (Exception)
            {


            }
            grdDocuments.GridLines = GridLines.None;
            op = new DMS.DAL.operations();
            tables.dbo.userFolders userFldr = new tables.dbo.userFolders();
            userFldr = op.dboGetUserFoldersByPrimaryKey(fldrID, userID);
            if (!userFldr.fieldAllowDelete)
            {
                grdDocuments.Columns[grdDocuments.Columns.Count - 1].Visible = false;
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
        protected void delDocumnetRowbtn_ServerClick(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            c = new CommonFunction.clsCommon();
            string docID = hdnDociId.Value;
            op = new DMS.DAL.operations();
            op.dboDeleteDocumentVersions("docID=" + docID);

            op = new DMS.DAL.operations();
            op.dboDeleteDocumentsByPrimaryKey(c.convertToInt32(docID));

            fillDocuments();
        }
        protected void lnkSearch_Click(object sender, EventArgs e)
        {

            if (DMS.Security.isNotAllowedCharacters(txtSearch.Text))
            {
                fillDocuments("docName like N'%" + txtSearch.Text + "%' or Meta1 like N'%" + txtSearch.Text + "%' or Meta2 like N'%" + txtSearch.Text + "%'");
            }
        }

        protected void grdDocuments_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdDocuments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {

                    HiddenField hdn = new HiddenField();
                    hdn = (HiddenField)e.Row.Cells[4].FindControl("hdnWfStartDateTime");
                    if (hdn.Value != "")
                    {
                        DateTime wfStartDateTime = Convert.ToDateTime(hdn.Value);

                        hdn = new HiddenField();
                        hdn = (HiddenField)e.Row.Cells[4].FindControl("hdnWfTimeFrame");
                        decimal wfTimeFrame = Convert.ToDecimal(hdn.Value);

                        hdn = new HiddenField();
                        hdn = (HiddenField)e.Row.Cells[4].FindControl("hdnWfStatus");
                        Int16 wfStatus = c.convertToInt16(hdn.Value);

                        if ((wfTimeFrame > 0) && (DateTime.Now.Subtract(wfStartDateTime).TotalMinutes > Convert.ToDouble(wfTimeFrame)) && (wfStatus == 1))
                        {
                            e.Row.BackColor = System.Drawing.Color.FromName("#f1cccc");
                        }
                    }
                }
                catch { }
            }
        }


        protected void drpPager1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drp = (DropDownList)sender;
            fillDocuments("", Convert.ToInt32(drp.SelectedValue));
        }

        //private void InitializeComponent()
        //{
        //    this.PreInit += new System.EventHandler(this.documentsList_PreInit);

        //}

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
            bool ignoreDelayed = false;
            if (ddlStatusFilter.SelectedValue == "1")
            {
                ignoreDelayed = true;
            }
            fillDocuments(cond, 1,false, ignoreDelayed);
        }
        protected void btnexportexcel_ServerClick(object sender, EventArgs e)
        {
            string attachment = "attachment; filename=Contacts.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            // Insert below
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdDocuments.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            //Response.Write('<script type=""text/javascript"">onButtonClick();</script>');
        }
    }
}