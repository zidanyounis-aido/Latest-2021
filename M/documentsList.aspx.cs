using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.M
{
    public partial class documentsList : System.Web.UI.Page
    {
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        DMS.DAL.operations op = new DMS.DAL.operations();
        Int32 fldrID = 0;
        Int32 userID;
        
        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                lblFolderName.Text = "اسم";
                
                drpSortBy.Items.FindByValue("docID").Text = "تسلسل";
                drpSortBy.Items.FindByValue("docName").Text = "اسم الملف";
                drpSortBy.Items.FindByValue("docTypID").Text = "النوع";
                drpSortBy.Items.FindByValue("addedDate").Text = "الإضافة";
                drpSortBy.Items.FindByValue("addedUserID").Text = "المستخدمين المضافين";
                drpSortBy.Items.FindByValue("modifyDate").Text = "تاريخ التعديل";
                rdoOrderType.Items.FindByValue(" ").Text = "تصاعدي";
                rdoOrderType.Items.FindByValue("desc").Text = "تنازلي";
                grdDocuments.Columns[0].HeaderText = "تسلسل";
                grdDocuments.Columns[1].HeaderText = "المستند";
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            if (c.convertToString(Session["userId"]) == "")
            {
                Response.Redirect("../M/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }
            
            userID = c.convertToInt32(Session["userId"]);

            if (Request.QueryString["fldrID"] == "")
                Response.Redirect("../M/", true);
            else
                fldrID = c.convertToInt16(c.decrypt(Request.QueryString["fldrID"]));

            Session["fldrID"] = fldrID;
            lnkAddDoc.NavigateUrl = "../M/newDocument.aspx?fldrID=" + Request.QueryString["fldrID"];
            

            if (!IsPostBack)
            {
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

                    fillDocuments();

                    tables.dbo.userFolders userFldr = new tables.dbo.userFolders();
                    op = new DMS.DAL.operations();
                    userFldr = op.dboGetUserFoldersByPrimaryKey(fldrID, c.convertToInt32(Session["userID"]));
                    if (!userFldr.fieldAllow)
                        Response.Redirect("../M/DefaultAr.aspx");

                    if (!userFldr.fieldAllowInsert)
                        lnkAddDoc.Visible = false;
                }
                else
                    Response.Redirect("../M/DefaultAr.aspx", true);

            }

        }

        private void fillDocuments(string cond = "", Int32 pageIndex = 1)
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
            docs = DMS.BLL.specialCases.getDocumentsByPage("fldrID = " + fldrID.ToString() + " " + cond.Trim(), pageIndex, 50, drpSortBy.SelectedValue + " " + rdoOrderType.SelectedValue);
            //docs.DefaultView.Sort = drpSortBy.SelectedValue + " " + rdoOrderType.SelectedValue;
            grdDocuments.DataSource = docs;

            grdDocuments.DataBind();

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
            Response.Redirect("../M/documentInfo.aspx?docID=" + res);
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
    }
}