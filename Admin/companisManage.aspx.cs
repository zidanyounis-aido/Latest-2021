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
    public partial class companisManage : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        tables.dbo.companies companiesTB = new tables.dbo.companies();
        UserData _userData = new UserData();
        Int32 companyID; string companyName; string address; string tel1; string tel2; string zipcode; string mainEmail; string description;
        string companyNameAr;Int32 clientID;
        public void fillVariables()
        {
            companyID = c.convertToInt32(txtCompanyID.Text);
            companyName = c.convertToString(txtCompanyName.Text);
            companyNameAr = txtCompanyNameAr.Text;
            address = c.convertToString(txtAddress.Text);
            tel1 = c.convertToString(txtTel1.Text);
            tel2 = c.convertToString(txtTel2.Text);
            zipcode = c.convertToString(txtZipcode.Text);
            mainEmail = c.convertToString(txtMainEmail.Text);
            description = c.convertToString(txtDescription.Text);
            clientID = c.convertToInt32(Session["clientId"]);
        }
        protected string SafeSmartSubstring(string val)
        {
            try
            {
                if (val.IndexOf(" ") != -1)
                {
                    val = val.Trim();
                    string fullName = val;
                    string[] names = fullName.Split(' ');
                    string name = names.First();
                    string lastName = names.Last();
                    var fullVal = name + " " + lastName;
                    string str = String.Join(String.Empty, fullVal.Split(new[] { ' ' }).Select(word => word.First()));
                    str = System.Text.RegularExpressions.Regex.Replace(str, ".{1}", "$0 ");
                    return str;
                }
                else
                {
                    return val.Substring(0, 1);
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public void fillData(DataTable DT)
        {
            c.fillData(DT, 0, companiesTB.columnsArray, Page);

            fillCompanyFolders();
        }


        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                //grdCompanies.Columns[0].HeaderText = "الرقم";
                //grdCompanies.Columns[1].HeaderText = "اسم الشركة";
                //grdCompanies.DataBind();

                lblFormMode.Text = "اضافة شركة";
                lblCompanyID.Text = "الرقم";
                lblCompanyName.Text = "اسم الشركة (بالانجليزية)";
                lblCompanyNameAr.Text = "اسم الشركة (بالعربي)";
                lblAddress.Text = "العنوان";
                lblZipcode.Text = "الرمز البريدي";
                lblTel1.Text = "تلفون 1";
                lblTel2.Text = "تلفون 2";



                grdCompanyFolders.Columns[0].HeaderText = "الرقم";
                grdCompanyFolders.Columns[1].HeaderText = "اسم المجلد";
                lblMainEmail.Text = "البريد الإلكتروني";
                lblDescription.Text = "التفاصيل";
               
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        { converttoArabic();
            if (!IsPostBack)
            {
                fillcompanies();

                op = new DMS.DAL.operations();
                tables.dbo.folders fldrTB = new tables.dbo.folders();
                fldrTB = op.dboGetAllFolders();
                c.FillDropDownList(drpFolders, fldrTB.table);
            }
           
        }

        private void fillcompanies()
        {
            op = new DMS.DAL.operations();
            companiesTB = new tables.dbo.companies();
            companiesTB = op.dboGetAllCompanies();
            grdCompanies.DataSource = companiesTB.table;
            //BoundField bf = (BoundField)grdCompanies.Columns[1];

            //if (Session["lang"].ToString() == "0")
            //    bf.DataField = "companyName";
            //else
            //    bf.DataField = "companyNameAr";

            grdCompanies.DataBind();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                fillVariables();
                Int32 varRes=0;
                
                if (rdoSaveMethod.SelectedValue == "0")
                {
                    op = new DMS.DAL.operations();

                    varRes = op.dboAddCompanies(companyName, address, tel1, tel2, zipcode, mainEmail, description, companyNameAr, clientID);
                    txtCompanyID.Text = varRes.ToString();
                }
                else
                {
                    if (txtCompanyID.Text != "")
                        varRes = op.dboUpdateCompaniesByPrimaryKey(companyID, companyName, address, tel1, tel2, zipcode, mainEmail, description, companyNameAr, clientID);
                    else
                        varRes = -1;
                }

                if (varRes > -1)
                {
                    lblRes.Text = "Save successful";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "تم الحفظ بنجاح";
                    fillcompanies();

                    fillCompanyFolders();
                    drpFolders.SelectedIndex = 0;
                }
                else
                {
                    lblRes.Text = "Data Not Saved";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "لم يتم الحفظ";
                }
            }
            divDetails.Visible = false;
            divList.Visible = true;
        }

        protected void grdCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            //companiesTB = new tables.dbo.companies();
            //op = new DMS.DAL.operations();
            //companiesTB = op.dboGetCompaniesByPrimaryKey(c.convertToInt32(grdCompanies.SelectedRow.Cells[0].Text));

            //fillData(companiesTB.table);
            //rdoSaveMethod.SelectedValue = "1";

            //lblFormMode.Text = "Edit Company";
            //if (Session["lang"].ToString() == "1")
            //    lblFormMode.Text = "تعديل الشركة"; 

            //tblDetailsForm.Style["display"] = "table";
        }

        public void fillCompanyFolders()
        {
            if (txtCompanyID.Text != "")
            {
                string sql = "SELECT     dbo.folders.fldrName, dbo.companyFolders.fldrID,dbo.folders.fldrNameAr"
                      + " From dbo.folders INNER JOIN"
                      + " dbo.companyFolders ON dbo.folders.fldrID = dbo.companyFolders.fldrID"
                      + $" WHERE   dbo.folders.clientId = {_userData.ClientId}  and (dbo.companyFolders.companyID = " + txtCompanyID.Text + ")";
                DataTable DT = c.GetDataAsDataTable(sql);
                grdCompanyFolders.DataSource = DT;
                BoundField bf = (BoundField)grdCompanyFolders.Columns[1];

                if (Session["lang"].ToString() == "0")
                    bf.DataField = "fldrName";
                else
                    bf.DataField = "fldrNameAr";

                grdCompanyFolders.DataBind();
            }
        }

        protected void btnAddFolder_Click(object sender, EventArgs e)
        {
            if (drpFolders.SelectedIndex > 0)
            {
                fillVariables();
                op = new DMS.DAL.operations();
                op.dboAddCompanyFolders(c.convertToInt32(txtCompanyID.Text),
                    c.convertToInt32(drpFolders.SelectedValue));
                fillCompanyFolders();
            }
        }

        protected void grdCompanyFolders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            fillVariables();
            op = new DMS.DAL.operations();
            Int32 folderID = c.convertToInt32(grdCompanyFolders.Rows[e.RowIndex].Cells[0].Text);
            op.dboDeleteCompanyFoldersByPrimaryKey(companyID, folderID);
            fillCompanyFolders();
        }
        protected void dl_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)grdCompanies.Items[e.NewSelectedIndex];
            LinkButton btn = (LinkButton)item.FindControl("lnkSelect");
            int ID = c.convertToInt32(btn.CommandArgument);
            if (ID > 0)
            {
                companiesTB = new tables.dbo.companies();
                op = new DMS.DAL.operations();
                companiesTB = op.dboGetCompaniesByPrimaryKey(ID);

                fillData(companiesTB.table);
                rdoSaveMethod.SelectedValue = "1";

                lblFormMode.Text = "Edit Company";
                if (Session["lang"].ToString() == "1")
                    lblFormMode.Text = "تعديل الشركة";
                lblRes.Text = "";
            }
            divDetails.Visible = true;
            divList.Visible = false;
            btnUndo.Visible = true;
            btnDelete.Visible = true;
        }

        protected void btnUndo_ServerClick(object sender, EventArgs e)
        {
            divDetails.Visible = false;
            divList.Visible = true;
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            //fillVariables();
            op = new DMS.DAL.operations();
            Int32 compID = c.convertToInt32(txtCompanyID.Text);
            op.dboDeleteCompaniesByPrimaryKey(compID);
            fillCompanyFolders();
            fillcompanies();
            divDetails.Visible = false;
            divList.Visible = true;
        }

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            lblFormMode.Text = "Add Company";
            if (Session["lang"].ToString() == "1")
                lblFormMode.Text = "اضافة الشركة";
            divDetails.Visible = true;
            divList.Visible = false;
            btnUndo.Visible = true;
            btnDelete.Visible = false;
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:clear_form_elements('#ContentPlaceHolder1_ContentPlaceHolder1_divDetails'); ", true);
        }
    }
}