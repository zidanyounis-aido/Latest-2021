using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using dms.Utilities;

namespace dms.Admin
{
    public partial class branchsManage : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        tables.dbo.branchs BranchsTB = new tables.dbo.branchs();
        UserData _userData = new UserData();
        Int32 branchID; Int32 companyID; string branchName; string address; string tel1; string tel2; string zipcode; string mainEmail; string description; bool isMainBranch;
        string branchNameAr;
        public void fillVariables()
        {
            branchID = c.convertToInt32(txtBranchID.Text);
            companyID = c.convertToInt32(drpCompanyID.SelectedValue);
            branchName = c.convertToString(txtBranchName.Text);
            address = c.convertToString(txtAddress.Text);
            tel1 = c.convertToString(txtTel1.Text);
            tel2 = c.convertToString(txtTel2.Text);
            zipcode = c.convertToString(txtZipcode.Text);
            mainEmail = c.convertToString(txtMainEmail.Text);
            description = c.convertToString(txtDescription.Text);
            isMainBranch = c.convertToBool(chkIsMainBranch.Checked);
            branchNameAr = txtBranchNameAr.Text;
        }

        public void fillData(DataTable DT)
        {
            c.fillData(DT, 0, BranchsTB.columnsArray, Page);

            fillBranchFolders();
            string fldrNameFeild = "dbo.folders.fldrName";
            if(Session["lang"].ToString() == "1")
                fldrNameFeild = "dbo.folders.fldrNameAr";

            string s;
            if (drpCompanyID.SelectedIndex > 0)
            {
                s = "SELECT     dbo.companies.companyName,dbo.companies.companyNameAr, dbo.folders.fldrID, " + fldrNameFeild + ", dbo.folders.fldrParentID,dbo.folders.iconID" +
                        " FROM         dbo.companies INNER JOIN" +
                        " dbo.companyFolders ON dbo.companies.companyID = dbo.companyFolders.companyID INNER JOIN" +
                        $" dbo.folders ON dbo.companyFolders.fldrID = dbo.folders.fldrID and dbo.folders.clientId = {_userData.ClientId}" +
                        " WHERE     (dbo.companies.companyID = " + drpCompanyID.SelectedValue + ")";
            }
            else
            {
                s = "SELECT     dbo.companies.companyName,dbo.companies.companyNameAr, dbo.folders.fldrID, " + fldrNameFeild + ", dbo.folders.fldrParentID,dbo.folders.iconID" +
                                   " FROM         dbo.companies INNER JOIN" +
                                   " dbo.companyFolders ON dbo.companies.companyID = dbo.companyFolders.companyID INNER JOIN" +
                                   $" dbo.folders ON dbo.companyFolders.fldrID = dbo.folders.fldrID and dbo.folders.clientId = {_userData.ClientId}";

            }
            folderTree1.selectStatment = s;
        }


      public void  converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                grdBranchs.Columns[0].HeaderText = "الرقم المتسلسل";
                grdBranchs.Columns[1].HeaderText = "اسم الفرع";
                lblBranchID.Text = "رقم الفرع";
                lblCompanyID.Text = "الشركة";
                lblBranchName.Text = "اسم الفرع (بالانجليزية)";
                lblAddress.Text = "العنوان";
                lblTel1.Text = "تلفون 1";
                lblTel2.Text = "تلفون 2";
                lblZipcode.Text = "الرمز البريدي";
                lblMainEmail.Text = "البريد الالكتروني";
                lblDescription.Text = "الوصف";
                lblIsMainBranch.Text = "هل هو الفرع الرئيسي؟";
                lblFormMode.Text = "اضافة الفرع";
                lblBranchNameAr.Text = "اسم الفرع (بالعربية)";


                grdBranchFolders.Columns[0].HeaderText = "الرقم";
                grdBranchFolders.Columns[1].HeaderText = "اسم المجلد";
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            if (!IsPostBack)
            {
                fillBranchs();

                fillDrp();
            }
            
        }

        private void fillDrp()
        {
            op = new DMS.DAL.operations();
            tables.dbo.companies companiesTB = new tables.dbo.companies();
            companiesTB = op.dboGetAllCompanies();
            if(Session["lang"].ToString() == "0")
                c.FillDropDownList(drpCompanyID, companiesTB.table);
            else
                c.FillDropDownList(drpCompanyID, companiesTB.table,CommonFunction.clsCommon.Typesech.byColomenName,CommonFunction.clsCommon.IsFilter.False,"","","companyID","companyNameAr");

        }

        private void fillBranchs()
        {
            op = new DMS.DAL.operations();
            BranchsTB = new tables.dbo.branchs();
            BranchsTB = op.dboGetAllBranchs();
            BoundField bf = (BoundField)grdBranchs.Columns[1];

            if (Session["lang"].ToString() == "0")
                bf.DataField = "branchName";
            else
                bf.DataField = "branchNameAr";

            grdBranchs.DataSource = BranchsTB.table;
            grdBranchs.DataBind();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                fillVariables();
                Int32 varRes;

                if (rdoSaveMethod.SelectedValue == "0")
                {
                    op = new DMS.DAL.operations();

                    varRes = op.dboAddBranchs(companyID, branchName, address, tel1, tel2, zipcode, mainEmail, description, isMainBranch, branchNameAr);
                    txtBranchID.Text = varRes.ToString();
                }
                else
                {
                    if (txtBranchID.Text != "")
                        varRes = op.dboUpdateBranchsByPrimaryKey(branchID, companyID, branchName, address, tel1, tel2, zipcode, mainEmail, description, isMainBranch, branchNameAr);
                    else
                        varRes = -1;
                }

                if (varRes > -1)
                {
                    lblRes.Text = "Save successful";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "تم الحفظ بنجاح";
                    fillBranchs();
                    fillBranchFolders();
                    hdnFldrID.Value = "";
                    
                }
                else
                {
                    lblRes.Text = "Data Not Saved";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "لم يتم الحفظ";
                }
            }
        }

        protected void grdBranchs_SelectedIndexChanged(object sender, EventArgs e)
        {
            BranchsTB = new tables.dbo.branchs();
            op = new DMS.DAL.operations();
            BranchsTB = op.dboGetBranchsByPrimaryKey(c.convertToInt32(grdBranchs.SelectedRow.Cells[0].Text));

            fillData(BranchsTB.table);
            rdoSaveMethod.SelectedValue = "1";

            lblFormMode.Text = "Edit Branch";
            if (Session["lang"].ToString() == "1")
                lblFormMode.Text = "تعديل الفرع"; 

            tblDetailsForm.Style["display"] = "table";
        }

        public void fillBranchFolders()
        {
            string fldrNameFeild = "dbo.folders.fldrName";
            if (Session["lang"].ToString() == "1")
                fldrNameFeild = "dbo.folders.fldrNameAr";

            if (txtBranchID.Text != "")
            {
                if (drpCompanyID.SelectedIndex > 0)
                {
                    DataTable dt = new DataTable();
                    dt = c.GetDataAsDataTable("SELECT     dbo.folders.fldrID, " + fldrNameFeild 
                    + " FROM         dbo.folders INNER JOIN"
                    + " dbo.companyFolders ON dbo.folders.fldrID = dbo.companyFolders.fldrID INNER JOIN"
                    + " dbo.companies ON dbo.companyFolders.companyID = dbo.companies.companyID"
                    + " WHERE     (dbo.companies.companyID = " + drpCompanyID.SelectedValue + ")");
                    c.FillDropDownList(drpFolders, dt);
                }

                string sql = "SELECT " + fldrNameFeild + ", dbo.branchFolders.fldrID"
                      + " From dbo.folders INNER JOIN"
                      + " dbo.branchFolders ON dbo.folders.fldrID = dbo.branchFolders.fldrID"
                      + " WHERE     (dbo.branchFolders.branchID = " + txtBranchID.Text + ")";
                DataTable DT = c.GetDataAsDataTable(sql);
                grdBranchFolders.DataSource = DT;

                BoundField bf = (BoundField)grdBranchFolders.Columns[1];

                if (Session["lang"].ToString() == "0")
                    bf.DataField = "fldrName";
                else
                    bf.DataField = "fldrNameAr";

                grdBranchFolders.DataBind();
            }
        }

        protected void btnAddFolder_Click(object sender, EventArgs e)
        {
            if (hdnFldrID.Value != "")
            {
                fillVariables();
                op = new DMS.DAL.operations();
                op.dboAddBranchFolders(c.convertToInt32(txtBranchID.Text),
                    c.convertToInt32(hdnFldrID.Value));
                fillBranchFolders();
            }
        }


        protected void grdBranchFolders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            fillVariables();
            op = new DMS.DAL.operations();
            Int32 folderID = c.convertToInt32(grdBranchFolders.Rows[e.RowIndex].Cells[0].Text);
            op.dboDeleteBranchFoldersByPrimaryKey(branchID, folderID);
            fillBranchFolders();
        }
    }
}