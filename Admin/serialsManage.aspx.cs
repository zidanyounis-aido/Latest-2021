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
    public partial class serialsManage : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        tables.dbo.companies companiesTB = new tables.dbo.companies();
        UserData _userData = new UserData();
        Int32 companyID; string companyName; string address; string tel1; string tel2; string zipcode; string mainEmail; string description;
        string companyNameAr;
        public void fillVariables()
        {
            companyID = c.convertToInt32(txtID.Text);
            companyName = c.convertToString(txtCompanyName.Text);
            companyNameAr = txtCompanyNameAr.Text;
            address = c.convertToString(txtAddress.Text);
            tel1 = c.convertToString(txtTel1.Text);
            tel2 = c.convertToString(txtTel2.Text);
            zipcode = c.convertToString(txtZipcode.Text);
            mainEmail = c.convertToString(txtMainEmail.Text);
            description = c.convertToString(txtDescription.Text);

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

        public void fillDrops(string serial = "",string text="", int folderId = 0)
        {
            // fill  foldes drop
            // fill first value
            DataTable dt = c.GetDataAsDataTable("select folders.fldrID,folders.fldrNameAr,folders.fldrName from folders");
            drpFoldersSerial.DataValueField = "fldrID";
            drpFoldersSerial.DataTextField = "fldrNameAr";
            drpFoldersSerial.DataSource = dt;
            drpFoldersSerial.DataBind();
            drpFoldersSerial.Items.Insert(0, new ListItem("-- اختر المجلد --", "0"));
            drpFoldersSerial.SelectedValue = folderId.ToString();

            List<ListItem> listItems = new List<ListItem>();
            listItems.Add(new ListItem() { Text = "اتسلسل", Value = "id" });
            listItems.Add(new ListItem() { Text = "السنه طويلة", Value = "yyyy" });
            listItems.Add(new ListItem() { Text = "السنه مختصر", Value = "yy" });
            listItems.Add(new ListItem() { Text = "نص", Value = "text" });
            //listItems.Add(new ListItem() { Text = "رمز", Value = "code" });

            drop1.DataValueField = "Value";
            drop1.DataTextField = "Text";
            drop1.DataSource = listItems;
            drop1.DataBind();

            drop2.DataValueField = "Value";
            drop2.DataTextField = "Text";
            drop2.DataSource = listItems;
            drop2.DataBind();

            drop3.DataValueField = "Value";
            drop3.DataTextField = "Text";
            drop3.DataSource = listItems;
            drop3.DataBind();

            drop4.DataValueField = "Value";
            drop4.DataTextField = "Text";
            drop4.DataSource = listItems;
            drop4.DataBind();
            txt1.Text = "";
            txt2.Text = "";
            txt3.Text = "";
            txt4.Text = "";
            if (serial != "")
            {
                var txtArr= text.Split(',');
                var arr = serial.Split(',');
                drop1.SelectedValue = arr[0];
                drop2.SelectedValue = arr[1];
                drop3.SelectedValue = arr[2];
                drop4.SelectedValue = arr[3];

                txt1.Text = drop1.SelectedItem.Text;
                if (drop1.SelectedValue != "id" && drop1.SelectedValue != "yyyy" && drop1.SelectedValue != "yy")
                {
                    txt1.Text = txtArr[0];
                    txt1.CssClass = "main-input";
                }
                txt2.Text = drop2.SelectedItem.Text;
                if (drop2.SelectedValue != "id" && drop2.SelectedValue != "yyyy" && drop2.SelectedValue != "yy")
                {
                    txt2.Text = txtArr[1];
                    txt2.CssClass = "main-input";
                }
                txt3.Text = drop3.SelectedItem.Text;
                if (drop3.SelectedValue != "id" && drop3.SelectedValue != "yyyy" && drop3.SelectedValue != "yy")
                {
                    txt3.Text = txtArr[2];
                    txt3.CssClass = "main-input";
                }
                txt4.Text = drop4.SelectedItem.Text;
                if (drop4.SelectedValue != "id" && drop4.SelectedValue != "yyyy" && drop4.SelectedValue != "yy")
                {
                    txt4.Text = txtArr[3];
                    txt4.CssClass = "main-input";
                }
            }
        }

        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                //grdCompanies.Columns[0].HeaderText = "الرقم";
                //grdCompanies.Columns[1].HeaderText = "اسم الشركة";
                //grdCompanies.DataBind();

                lblFormMode.Text = "اضافة شركة";
                lblID.Text = "الرقم";
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
        {
            lblRes.Text = "";
            //converttoArabic();
            if (!IsPostBack)
            {
                fillIngoingSerials();

                //op = new DMS.DAL.operations();
                //tables.dbo.folders fldrTB = new tables.dbo.folders();
                //fldrTB = op.dboGetAllFolders();
                //c.FillDropDownList(drpFolders, fldrTB.table);
            }

        }

        private void fillIngoingSerials()
        {
            DataTable dt = c.GetDataAsDataTable("SELECT IngoingOutgoingSerials.SerialCode, IngoingOutgoingSerials.Serial, IngoingOutgoingSerials.FolderId, Case When IngoingOutgoingSerials.FolderId <> 0 Then folders.fldrName ELSE 'General Setting' END AS 'Name' , Case When IngoingOutgoingSerials.FolderId <> 0 Then folders.fldrNameAr ELSE N'الاعدادات العامة' END AS 'NameAr', IngoingOutgoingSerials.Id FROM         IngoingOutgoingSerials LEFT JOIN folders ON IngoingOutgoingSerials.FolderId=folders.fldrID Where IngoingOutgoingSerials.Type=0 ");
            grdSerial.DataSource = dt;
            grdSerial.DataBind();
            //op = new DMS.DAL.operations();
            //companiesTB = new tables.dbo.companies();
            //companiesTB = op.dboGetAllCompanies();
            //grdCompanies.DataSource = companiesTB.table;
            //grdCompanies.DataBind();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            //fillVariables();
            Int32 varRes = 0;
            //if (rdoSaveMethod.SelectedValue == "0")
            //{
            //    op = new DMS.DAL.operations();
            //    varRes = op.dboAddCompanies(companyName, address, tel1, tel2, zipcode, mainEmail, description, companyNameAr);
            //}
            //else
            //{
            //    varRes = op.dboUpdateCompaniesByPrimaryKey(companyID, companyName, address, tel1, tel2, zipcode, mainEmail, description, companyNameAr);
            //}

            //if (varRes > -1)
            //{
            //    lblRes.Text = "Save successful";
            //    if (Session["lang"].ToString() == "1")
            //        lblRes.Text = "تم الحفظ بنجاح";
            //    fillCompanyFolders();
            //    drpFolders.SelectedIndex = 0;
            //}
            //else
            //{
            //    lblRes.Text = "Data Not Saved";
            //    if (Session["lang"].ToString() == "1")
            //        lblRes.Text = "لم يتم الحفظ";
            //}
            if (txtID.Text == "0") // this add
            {
                string code = drop1.SelectedValue + "," + drop2.SelectedValue + "," + drop3.SelectedValue + "," + drop4.SelectedValue;
                string serial = "";
                if (drop1.SelectedValue != "text" && drop1.SelectedValue != "code")
                {
                    serial = drop1.SelectedValue;
                }
                else
                {
                    serial = txt1.Text;
                }

                if (drop2.SelectedValue != "text" && drop2.SelectedValue != "code")
                {
                    serial += "," + drop2.SelectedValue;
                }
                else
                {
                    serial += "," + txt2.Text;
                }

                if (drop3.SelectedValue != "text" && drop3.SelectedValue != "code")
                {
                    serial += "," + drop3.SelectedValue;
                }
                else
                {
                    serial += "," + txt3.Text;
                }

                if (drop4.SelectedValue != "text" && drop4.SelectedValue != "code")
                {
                    serial += "," + drop4.SelectedValue;
                }
                else
                {
                    serial += "," + txt4.Text;
                }
                string query = "INSERT INTO IngoingOutgoingSerials (SerialCode,FolderID,Serial,[Type])  Values  ('" + code + "', " + drpFoldersSerial.SelectedValue + ",N'" + serial + "', 0)";
                c.NonQuery(query);

                divDetails.Visible = false;
                divList.Visible = true;
                //btnUndo.Visible = true;
                //btnDelete.Visible = false;
            }
            else // this edit
            {
                string code = drop1.SelectedValue + "," + drop2.SelectedValue + "," + drop3.SelectedValue + "," + drop4.SelectedValue;
                string serial = "";
                if (drop1.SelectedValue != "text" && drop1.SelectedValue != "code")
                {
                    serial = drop1.SelectedValue;
                }
                else
                {
                    serial = txt1.Text;
                }

                if (drop2.SelectedValue != "text" && drop2.SelectedValue != "code")
                {
                    serial += "," + drop2.SelectedValue;
                }
                else
                {
                    serial += "," + txt2.Text;
                }

                if (drop3.SelectedValue != "text" && drop3.SelectedValue != "code")
                {
                    serial += "," + drop3.SelectedValue;
                }
                else
                {
                    serial += "," + txt3.Text;
                }

                if (drop4.SelectedValue != "text" && drop4.SelectedValue != "code")
                {
                    serial += "," + drop4.SelectedValue;
                }
                else
                {
                    serial += "," + txt4.Text;
                }
                string query = "Update IngoingOutgoingSerials set   SerialCode='" + code + "', FolderId=" + drpFoldersSerial.SelectedValue + ", Serial=N'" + serial + "', Type=0 where         Id=" + txtID.Text;
                c.NonQuery(query);
            }
            divDetails.Visible = false;
            divList.Visible = true;
            fillIngoingSerials();
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

        }

        protected void btnAddFolder_Click(object sender, EventArgs e)
        {
            if (drpFolders.SelectedIndex > 0)
            {
                fillVariables();
                op = new DMS.DAL.operations();
                op.dboAddCompanyFolders(c.convertToInt32(txtID.Text),
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
            ListViewItem item = (ListViewItem)grdSerial.Items[e.NewSelectedIndex];
            LinkButton btn = (LinkButton)item.FindControl("lnkSelect");
            int ID = c.convertToInt32(btn.CommandArgument);
            if (ID > 0)
            {
                lblFormMode.Text = "Edit ";
                if (Session["lang"].ToString() == "1")
                    lblFormMode.Text = "تعديل ";
                txtID.Text = ID.ToString();
                string serial = c.GetDataAsScalar("select top 1 isNULL(SerialCode,'') from IngoingOutgoingSerials where id=" + ID).ToString();
                string text = c.GetDataAsScalar("select top 1 isNULL(Serial,'') from IngoingOutgoingSerials where id=" + ID).ToString();
                int folderID = int.Parse(c.GetDataAsScalar("select top 1 ISNULL(FolderId,0) from IngoingOutgoingSerials where id=" + ID).ToString());
                fillDrops(serial,text, folderID);
                if (folderID == 0)
                {
                    btnDelete.Visible = false;
                    drpFoldersSerial.Visible = false;
                    Label1.Visible = false;
                    CompareValidator1.Enabled = false;
                }
                else
                {
                    btnDelete.Visible = true;
                    drpFoldersSerial.Visible = true;
                    Label1.Visible = true;
                    CompareValidator1.Enabled = true;
                }
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
            if (drpFolders.SelectedValue != "0")
            {
                c.NonQuery("delete from IngoingOutgoingSerials where ID=" + txtID.Text);
                divDetails.Visible = false;
                divList.Visible = true;
                fillIngoingSerials();
            }
            else
            {
                lblRes.Text = "You are not allowed to delete the General Serial";
                if (Session["lang"].ToString() == "1")
                    lblRes.Text = "لا يسمح بحذف التسلسل العام";
            }
        }

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            lblFormMode.Text = "Add ";
            if (Session["lang"].ToString() == "1")
                lblFormMode.Text = "اضافة ";
            divDetails.Visible = true;
            divList.Visible = false;
            btnUndo.Visible = true;
            btnDelete.Visible = false;
            txtID.Text = "0";
            divDetails.Visible = true;
            divList.Visible = false;
            btnUndo.Visible = true;
            btnDelete.Visible = false;
            fillDrops();
        }
    }
}