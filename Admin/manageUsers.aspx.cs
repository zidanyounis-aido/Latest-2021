using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using dms.Utilities;

namespace dms.Admin
{
    public partial class manageUsers : System.Web.UI.Page
    {
        //private void SaveUserImage(int UserID, string FullName)
        //{
        //    string filePath = Server.MapPath("/") + "Images/Users/" + UserID.ToString() + ".png";
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        tables.dbo.users usersTB = new tables.dbo.users();
        UserData _userData = new UserData();
        Int32 userID; string userName; string password; string fullName; Int32 grpID; bool active;
        Int32 companyID; Int32 branchID; Int32 departmentID; Int32 positionID; string email;
        bool allowCustomWF; bool allowCreateFolders; bool allowReplaceDocuments; bool allowDiwan;
        bool isFirstLogin; DateTime passwordCreationDate; DateTime passwordModifiedDate; string lastPassword; string Phone;
        string Signature;bool isMobileFirstLogin;bool isEmailVerfied;Int32 ClientId;
        bool allowOutgoing; bool allowIncoming;

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
        public void fillVariables()
        {
            userID = c.convertToInt32(txtUserID.Text);
            userName = c.convertToString(txtUserName.Text);
            password = c.encrypt(txtPassword.Text);
            fullName = c.convertToString(txtFullName.Text);
            grpID = c.convertToInt32(drpGrpID.SelectedValue);
            active = c.convertToBool(chkActive.Checked);
            companyID = c.convertToInt32(drpCompanyID.SelectedValue);
            branchID = c.convertToInt32(drpBranchID.SelectedValue);
            departmentID = c.convertToInt32(drpDepartmentID.SelectedValue);
            positionID = c.convertToInt32(drpPositionID.SelectedValue);
            email = c.convertToString(txtEmail.Text);
            allowCustomWF = chkAllowCustomWF.Checked;
            allowCreateFolders = chkAllowCreateFolders.Checked;
            allowReplaceDocuments = chkAllowReplaceDocuments.Checked;
            allowDiwan = chkAllowDiwan.Checked;

            isFirstLogin = c.convertToBool(hdnIsFirstLogin.Value);
            passwordCreationDate = c.convertToDateTime(hdnPasswordCreationDate.Value);
            passwordModifiedDate = c.convertToDateTime(hdnPasswordModifiedDate.Value);
            lastPassword = c.convertToString(hdnLastPassword.Value);
            Phone = c.convertToString(txtPhone.Text);

            Signature = hdnSignature.Value;
            isMobileFirstLogin = c.convertToBool( hdnIsMobileFirstLogin.Value) ; 
            isEmailVerfied = c.convertToBool(hdnIsEmailVerfied.Value); 
            ClientId = c.convertToInt32(hdnClientId.Value);

        }

        private void fillUserPrograms()
        {

            string sql = "SELECT     dbo.userPrograms.programID, dbo.programs.programName,dbo.programs.programNameAr " +
                        " FROM         dbo.userPrograms INNER JOIN dbo.programs ON dbo.userPrograms.programID = dbo.programs.programID" +
                        " WHERE     (dbo.userPrograms.userID = " + txtUserID.Text + ")";

            DataTable DT = new DataTable();
            DT = c.GetDataAsDataTable(sql);
            grdPrograms.DataSource = DT;
            BoundField BF = (BoundField)grdPrograms.Columns[1];
            if (Session["lang"].ToString() == "0")
                BF.DataField = "programName";
            else
                BF.DataField = "programNameAr";
            grdPrograms.DataBind();
        }

        public void fillData(DataTable DT)
        {
            c.fillData(DT, 0, usersTB.columnsArray, Page);
            if (txtUserID.Text != "" && File.Exists(Server.MapPath("/Images/Users/" + txtUserID.Text + ".png")))
                imgUser.Src = "~/Images/Users/" + txtUserID.Text + ".png?"+DateTime.Now.ToString("ddMMyyyyhhssffff");
            else
                imgUser.Src = "/Assets/UIKIT/img/noAvatar.jpg";
            hdnPassword.Value = usersTB.fieldPassword;

            string signature = c.GetDataAsScalar("select Signature from users where userid=" + txtUserID.Text).ToString();
            string[] ar = signature.Split(',');
            imgSign.Visible = false;
            if (ar.Length > 1)
            {
                if (!String.IsNullOrEmpty(ar[1]))
                {
                    imgSign.Visible = true;
                    imgSign.ImageUrl = signature;
                    //imgSign.ImageUrl = ar[0] + "," + Convert.ToBase64String(ar[1]);
                }
            }

            fillUserFolders();
            string s;
            if (drpCompanyID.SelectedIndex > 0)
            {
                s = "SELECT     dbo.companies.companyName,dbo.companies.companyNameAr, dbo.folders.fldrID, dbo.folders.fldrName,dbo.folders.fldrNameAr, dbo.folders.fldrParentID,dbo.folders.iconID" +
                        " FROM         dbo.companies INNER JOIN" +
                        " dbo.companyFolders ON dbo.companies.companyID = dbo.companyFolders.companyID INNER JOIN" +
                        " dbo.folders ON dbo.companyFolders.fldrID = dbo.folders.fldrID" +
                        " WHERE     (dbo.companies.companyID = " + drpCompanyID.SelectedValue + ")";
            }
            else
            {
                s = "SELECT     dbo.companies.companyName,dbo.companies.companyNameAr, dbo.folders.fldrID, dbo.folders.fldrName,dbo.folders.fldrNameAr, dbo.folders.fldrParentID,dbo.folders.iconID" +
                                   " FROM         dbo.companies INNER JOIN" +
                                   " dbo.companyFolders ON dbo.companies.companyID = dbo.companyFolders.companyID INNER JOIN" +
                                   " dbo.folders ON dbo.companyFolders.fldrID = dbo.folders.fldrID";

            }
            folderTree1.selectStatment = s;

            fillUserPrograms();
        }

        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                //grdUsers.Columns[0].HeaderText = "الرقم";
                //grdUsers.Columns[1].HeaderText = "الاسم الكامل";
                lblFormMode.Text = "اضافة مستخدم جديد";
                Label1.Text = "رقم المستخدم";
                Label8.Text = "اسم المستخدم";
                Label2.Text = "كلمة السر";
                Label3.Text = "تأكيد كلمة السر";
                Label6.Text = "مجموعة";
                Label7.Text = "الاسم بالكامل";
                Label9.Text = "الشركة";
                Label11.Text = "الفرع";
                Label10.Text = "القسم";
                Label12.Text = "المسمى الوظيفي";
                Label13.Text = "البريد الالكتروني";
                LabelPhone.Text = "الجوال";
                chkActive.Text = "فعال";
                chkAllowCustomWF.Text = "السماح بالخروج عن مسار سير العمل ";
                //chkAllowCreateFolders.Text = "السماح بعمل مجلد جديد";
                //chkAllowReplaceDocuments.Text = "السماح باستبدال الملفات";
                //chkAllowDiwan.Text = "السماح بتغيير وقت إجراء العمل";
                rdoSaveMethod.Items.FindByValue("0").Text = "مستخدم جديد";
                rdoSaveMethod.Items.FindByValue("1").Text = "مستخدم حالي";
                grdUsersFolders.Columns[0].HeaderText = "الرقم";
                grdUsersFolders.Columns[1].HeaderText = "اسم المجلد";
                grdUsersFolders.Columns[2].HeaderText = "عرض";
                grdUsersFolders.Columns[3].HeaderText = "اضافة";
                grdUsersFolders.Columns[4].HeaderText = "تحديث";
                grdUsersFolders.Columns[5].HeaderText = "حذف";
                grdUsersFolders.Columns[6].HeaderText = "انشاء ملف صادر";
                grdUsersFolders.Columns[7].HeaderText = "إضافة ملف وارد";
                //grdUsersFolders.Columns[8].HeaderText = "تغير مكان المجلد";
                //grdUsersFolders.Columns[9].HeaderText = "وراثة المجلدات الفرعية";



                grdPrograms.Columns[0].HeaderText = "الرقم ";
                grdPrograms.Columns[1].HeaderText = "اسم البرنامج";

                //RequiredFieldValidator1.ErrorMessage = "الرجاء ادخال اسم المستخدم";
                //rfvPassword.ErrorMessage = "الرجاء ادخال كلمة السر";
                //CompareValidator1.ErrorMessage = "كلمة السر غير مطابقة";
                //RequiredFieldValidator3.ErrorMessage = "الرجاء ادخال الاسم الكامل";
                //RegularExpressionValidator1.ErrorMessage = "صيغة البريد الالكتروني خاطئة";
                //RequiredFieldValidator4.ErrorMessage = "الرجاء ادخال البريد الالكتروني";
                //RequiredFieldValidator5.ErrorMessage = "الرجاء ادخال الجوال";
                //grid grdGroupFolders
                //grdGroupFolders.Columns[0].HeaderText = "الرقم";
                //grdGroupFolders.Columns[1].HeaderText = "اسم المجلد";
                //grdGroupFolders.Columns[2].HeaderText = "عرض";
                //grdGroupFolders.Columns[3].HeaderText = "اضافة";
                //grdGroupFolders.Columns[4].HeaderText = "تحديث";
                //grdGroupFolders.Columns[5].HeaderText = "حذف";

                //grdGroupFolders.Columns[6].HeaderText = "انشاء مجلدات فرعي";
                //grdGroupFolders.Columns[7].HeaderText = "اعادة تسمية المجلدات";
                //grdGroupFolders.Columns[8].HeaderText = "تغير مكان المجلد";
                //grdGroupFolders.Columns[9].HeaderText = "وراثة المجلدات الفرعية";
            }


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            if (txtUserID.Text != "")
            {
                lblFormMode.Text = "Edit User";
                if (Session["lang"].ToString() == "1")
                    lblFormMode.Text = "تعديل المستخدم";
            }
            if (!IsPostBack)
            {

                fillDrp();
                fillUsers();
            }

        }

        void fillDrp()
        {
            op = new DMS.DAL.operations();
            tables.dbo.groups grpTB = new tables.dbo.groups();
            grpTB = op.dboGetAllGroups();


            op = new DMS.DAL.operations();
            tables.dbo.companies companiesTB = new tables.dbo.companies();
            companiesTB = op.dboGetAllCompanies();



            op = new DMS.DAL.operations();
            tables.dbo.branchs branchsTB = new tables.dbo.branchs();
            branchsTB = op.dboGetAllBranchs();


            op = new DMS.DAL.operations();
            tables.dbo.departments departmentsTB = new tables.dbo.departments();
            departmentsTB = op.dboGetAllDepartments();


            op = new DMS.DAL.operations();
            tables.dbo.positions positionsTB = new tables.dbo.positions();
            positionsTB = op.dboGetAllPositions();

            c.FillDropDownList(drpGrpID, grpTB.table);

            if (Session["lang"].ToString() == "1")
            {
                c.FillDropDownList(drpBranchID, branchsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "branchID", "BranchNameAr");
                c.FillDropDownList(drpDepartmentID, departmentsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "DepartmentID", "DepartmentNameAr");
                c.FillDropDownList(drpPositionID, positionsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "PositionID", "PositionTitleAr");

                c.FillDropDownList(drpCompanyID, companiesTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "CompanyID", "CompanyNameAr");
            }
            else
            {
                c.FillDropDownList(drpBranchID, branchsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "branchID", "BranchName");
                c.FillDropDownList(drpDepartmentID, departmentsTB.table);
                c.FillDropDownList(drpPositionID, positionsTB.table);

                c.FillDropDownList(drpCompanyID, companiesTB.table);
            }

            op = new DMS.DAL.operations();
            tables.dbo.folders fldrTB = new tables.dbo.folders();
            fldrTB = op.dboGetAllFolders();
            if (Session["lang"].ToString() == "0")
                c.FillDropDownList(drpFolders, fldrTB.table);
            else
                c.FillDropDownList(drpFolders, fldrTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "FldrID", "FldrNameAr");

            tables.dbo.programs programsTB = new tables.dbo.programs();
            programsTB = op.dboGetAllPrograms();
            if (Session["lang"].ToString() == "0")
                c.FillDropDownList(drpProgramID, programsTB.table);
            else
                c.FillDropDownList(drpProgramID, programsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "programID", "programNameAr");
        }

        protected void lnkAddProgram_Click(object sender, EventArgs e)
        {
            if (drpProgramID.SelectedIndex > 0)
            {
                fillVariables();
                op = new DMS.DAL.operations();
                op.dboAddUserPrograms(userID, c.convertToInt32(drpProgramID.SelectedValue));

                fillUserPrograms();
            }
        }

        protected void grdPrograms_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 programID;
            userID = c.convertToInt32(txtUserID.Text);
            programID = c.convertToInt32(grdPrograms.Rows[e.RowIndex].Cells[0].Text);

            op = new DMS.DAL.operations();
            op.dboDeleteUserProgramsByPrimaryKey(programID, userID);

            fillUserPrograms();
        }

        public void fillUsers()
        {
            op = new DMS.DAL.operations();
            usersTB = new tables.dbo.users();
            usersTB = op.dboGetAllUsers();
            dlusers.DataSource = usersTB.table;
            dlusers.DataBind();
        }

        protected void grdUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblRes.Text = "";
            //usersTB = new tables.dbo.users();
            //op = new DMS.DAL.operations();
            //usersTB = op.dboGetUsersByPrimaryKey(c.convertToInt32(grdUsers.SelectedRow.Cells[0].Text));

            //fillData(usersTB.table);
            //rdoSaveMethod.SelectedValue = "1";
            //lblFormMode.Text = "Edit User";
            //if (Session["lang"].ToString() == "1")
            //    lblFormMode.Text = "تعديل المستخدم";
            //tblDetailsForm.Style["display"] = "block";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                lblRes.Text = "";
                DMS.DAL.operations op = new DMS.DAL.operations();
                tables.dbo.settings settings = op.dboGetSettingsByPrimaryKey(1);
                bool flag = true;
                //if (!settings.fieldAllowUsernamePasswordMatch)
                //{

                //    if (drpCompanyID.Items.Count > 1 && c.convertToInt32(drpCompanyID.SelectedValue) < 1)
                //    {
                //        flag = false;
                //        if (Session["lang"].ToString() == "0")
                //            lblRes.Text += "Please select user Company";
                //        else
                //            lblRes.Text += "يجب اختيار الشركة";
                //        lblRes.Text += "</br>";
                //    }
                //}
                //if (!settings.fieldAllowUsernamePasswordMatch)
                //{
                //    if (drpBranchID.Items.Count > 1 && c.convertToInt32(drpBranchID.SelectedValue) < 1)
                //    {
                //        flag = false;
                //        if (Session["lang"].ToString() == "0")
                //            lblRes.Text += "Please select user Branch";
                //        else
                //            lblRes.Text += "يجب اختيار الفرع";
                //        lblRes.Text += "</br>";
                //    }
                //}

                //if (!settings.fieldAllowUsernamePasswordMatch)
                //{
                //    if (drpDepartmentID.Items.Count > 1 && c.convertToInt32(drpDepartmentID.SelectedValue) < 1)
                //    {
                //        flag = false;
                //        if (Session["lang"].ToString() == "0")
                //            lblRes.Text += "Please select user Department";
                //        else
                //            lblRes.Text += "يجب اختيار القسم";
                //        lblRes.Text += "</br>";
                //    }
                //}
                fillVariables();
                Int32 UsersNameCount = Convert.ToInt32(c.GetDataAsScalar($"select count(userID) from users where clientId = {_userData.ClientId} And userID<>{userID} And userName='{userName}'"));
                if (UsersNameCount > 0)
                {
                    flag = false;
                    if (Session["lang"].ToString() == "0")
                        lblRes.Text += "The User Name is already used";
                    else
                        lblRes.Text += "اسم المستخدم مستخدم بالفعل";
                    lblRes.Text += "</br>";
                }

                Int32 UsersEmailCount = Convert.ToInt32(c.GetDataAsScalar($"select count(userID) from users where clientId = {_userData.ClientId} And userID<>{userID} And email='{email}'"));
                if (UsersEmailCount > 0 && !string.IsNullOrEmpty(email))
                {
                    flag = false;
                    if (Session["lang"].ToString() == "0")
                        lblRes.Text += "The Email is already used";
                    else
                        lblRes.Text += "البريد الإلكتروني مستخدم بالفعل";
                    lblRes.Text += "</br>";
                }
                Int32 UsersPhoneCount = Convert.ToInt32(c.GetDataAsScalar($"select count(userID) from users where clientId = {_userData.ClientId} And userID<>{userID} And Phone='{Phone}'"));
                if (UsersPhoneCount > 0 && !string.IsNullOrEmpty(Phone))
                {
                    flag = false;
                    if (Session["lang"].ToString() == "0")
                        lblRes.Text += "The Phone is already used";
                    else
                        lblRes.Text += "الهاتف مستخدم بالفعل";
                    lblRes.Text += "</br>";
                }

                if (!settings.fieldAllowUsernamePasswordMatch)
                {
                    if (txtPassword.Text.ToLower().Contains(Session["userName"].ToString().ToLower()))
                    {
                        flag = false;
                        if (Session["lang"].ToString() == "0")
                            lblRes.Text += "Password can't conatin the Username";
                        else
                            lblRes.Text += "كلمة السر يجب ألا تحتوي على اسم المرور";
                        lblRes.Text += "</br>";
                    }
                }

                if (!settings.fieldPasswordAllowStartSpace)
                {
                    if (txtPassword.Text.StartsWith(" "))
                    {
                        flag = false;
                        if (Session["lang"].ToString() == "0")
                            lblRes.Text += "Password can't start with Space";
                        else
                            lblRes.Text += "كلمة السر يجب ألا تبدأ بفراغ";
                        lblRes.Text += "</br>";
                    }
                }

                if (txtPassword.Text.Length < settings.fieldPasswordLength)
                {
                    flag = false;
                    if (Session["lang"].ToString() == "0")
                        lblRes.Text += "Password Max length is " + settings.fieldPasswordLength.ToString() + " letters";
                    else
                        lblRes.Text += "طول كلمة السر يجب ألا تقل عن " + settings.fieldPasswordLength.ToString() + " حرف";
                    lblRes.Text += "</br>";

                }

                if (settings.fieldPasswordStrength > 1)
                {
                    if (settings.fieldPasswordStrength == 2)
                    {
                        if (!txtPassword.Text.Any(c => char.IsDigit(c)) || !txtPassword.Text.Any(c => char.IsLetter(c)))
                        {
                            flag = false;
                            if (Session["lang"].ToString() == "0")
                                lblRes.Text += "Password must contain Letters and Numbers";
                            else
                                lblRes.Text += "كلمة السر يجب أن تحتوي على حروف و أرقام ";
                            lblRes.Text += "</br>";
                        }
                    }


                    if (settings.fieldPasswordStrength == 3)
                    {
                        if ((!txtPassword.Text.Any(c => char.IsDigit(c)) || !txtPassword.Text.Any(c => char.IsLetter(c))))
                        {
                            if (txtPassword.Text.All(c => char.IsLetter(c) || char.IsDigit(c)))
                            {
                                flag = false;
                                if (Session["lang"].ToString() == "0")
                                    lblRes.Text += "Password must contain Letters and Numbers and Special Characters";
                                else
                                    lblRes.Text += "كلمة السر يجب أن تحتوي على حروف و أرقام و رموز";
                                lblRes.Text += "</br>";
                            }
                        }
                    }
                }



                if (flag || txtPassword.Text == "")
                {

                    tables.dbo.users userTB = new tables.dbo.users();
                    op = new DMS.DAL.operations();
                    bool groupChanged = false;

                    Int32 varRes = 0;
                    //upload file

                    if (rdoSaveMethod.SelectedValue == "0")
                    {
                        //string ImageUrl = "";

                        op = new DMS.DAL.operations();

                        tables.dbo.settings set = new tables.dbo.settings();
                        set = op.dboGetSettingsByPrimaryKey(1);
                        Int32 allowedUsers = Convert.ToInt32(c.decrypt(set.fieldAllowedUsersCount));
                        op = new DMS.DAL.operations();
                        Int32 currentUsers = Convert.ToInt32(c.GetDataAsScalar($"select count(userID) from users where clientId = {_userData.ClientId}"));
                        if (currentUsers - 1 < allowedUsers)
                        {

                            varRes = op.dboAddUsers(userName, password, fullName, grpID, active, companyID, branchID, departmentID, 
                                positionID, email, allowCustomWF, allowCreateFolders, allowReplaceDocuments, allowDiwan, isFirstLogin, 
                                passwordCreationDate, passwordModifiedDate, lastPassword, Signature, Phone,isMobileFirstLogin,isEmailVerfied,ClientId);
                            txtUserID.Text = varRes.ToString();
                            userID = varRes;

                            //upload file
                            if (FileUpload1.PostedFile != null)
                            {
                                System.IO.Stream fs = FileUpload1.PostedFile.InputStream;
                                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                                string ImageUrl = "data:image/png;base64," + base64String;
                                DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
                                sp.dboUpdateSignture(userID, ImageUrl);
                            }
                            //save user default programes
                            c.NonQuery("insert into  [dbo].[userPrograms]  values(" + userID + ",6)");//البريد
                            c.NonQuery("insert into  [dbo].[userPrograms]  values(" + userID + ",32)");//لوحة القياده
                            c.NonQuery("insert into  [dbo].[userPrograms]  values(" + userID + ",30)");//قائمه المهام
                            c.NonQuery("insert into  [dbo].[userPrograms]  values(" + userID + ",13)");//ملفي الخاص

                        }
                        else
                        {
                            varRes = -1;
                            if (Session["lang"].ToString() == "0")
                                lblRes.Text += "The number of allowed users in the system has been exceeded";
                            else
                                lblRes.Text += "تم تجاوز عدد المستخدمين المسموح بة في النظام";
                        }
                    }
                    else
                    {
                        if (txtUserID.Text != "")
                        {
                            userTB = op.dboGetUsersByPrimaryKey(userID);
                            if (userTB.fieldGrpID != grpID)
                                groupChanged = true;
                            if (txtPassword.Text == "")
                                password = hdnPassword.Value;

                            varRes = op.dboUpdateUsersByPrimaryKey( userID, userName, password, fullName, grpID, active, companyID, 
                                branchID, departmentID, positionID, email, allowCustomWF, allowCreateFolders, allowReplaceDocuments, 
                                allowDiwan, isFirstLogin, passwordCreationDate, passwordModifiedDate, lastPassword,
                                Signature, Phone, isMobileFirstLogin, isEmailVerfied, ClientId);

                            if (FileUpload1.PostedFile != null)
                            {
                                System.IO.Stream fs = FileUpload1.PostedFile.InputStream;
                                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                                string ImageUrl = "data:image/png;base64," + base64String;

                                DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
                                sp.dboUpdateSignture(userID, ImageUrl);
                            }
                        }
                        else
                            varRes = -1;
                    }

                    if (varRes > -1)
                    {
                        if (groupChanged)
                        {

                            op = new DMS.DAL.operations();
                            op.dboDeleteUserFolders("userID=" + userID.ToString());

                            op = new DMS.DAL.operations();
                            op.dboDeleteUserPrograms("userID=" + userID.ToString());

                            if (grpID > 0)
                            {
                                tables.dbo.groupFolders grpFolders = new tables.dbo.groupFolders();
                                op = new DMS.DAL.operations();
                                grpFolders = op.dboGetAllGroupFolders("grpID=" + grpID.ToString());
                                for (Int16 i = 0; i < grpFolders.rowsCount; i++)
                                {
                                    grpFolders.currentIndex = i;
                                    op = new DMS.DAL.operations();
                                    op.dboAddUserFolders(userID, grpFolders.fieldFldrID, true, grpFolders.fieldAllowInsert, 
                                        grpFolders.fieldAllowUpdate, grpFolders.fieldAllowDelete, grpFolders.fieldAllowOutgoing, 
                                        grpFolders.fieldAllowIncoming,  grpFolders.fieldInheritSubFolders);
                                }

                                tables.dbo.groupPrograms grpPrograms = new tables.dbo.groupPrograms();
                                op = new DMS.DAL.operations();
                                grpPrograms = op.dboGetAllGroupPrograms("groupID=" + grpID.ToString());
                                for (Int16 i = 0; i < grpPrograms.rowsCount; i++)
                                {
                                    grpPrograms.currentIndex = i;
                                    op = new DMS.DAL.operations();
                                    op.dboAddUserPrograms(userID, grpPrograms.fieldProgramID);
                                }
                            }
                        }
                        SaveUserImage(userID, txtFullName.Text);
                        lblRes.Text = "Save successful";
                        if (Session["lang"].ToString() == "1")
                            lblRes.Text = "تم الحفظ بنجاح";
                        fillUsers();
                        op = new DMS.DAL.operations();
                        fillData(op.dboGetUsersByPrimaryKey(userID).table);

                        fillUserFolders();
                        fillUserPrograms();

                        hdnFldrID.Value = "";
                        drpProgramID.SelectedIndex = 0;

                        rdoSaveMethod.SelectedValue = "1";
                        lblFormMode.Text = "Edit User";
                        if (Session["lang"].ToString() == "1")
                            lblFormMode.Text = "تعديل المستخدم";
                        tblDetailsForm.Style["display"] = "block";
                        rfvPassword.Enabled = false;
                        rfvRePassword.Enabled = false;
                    }
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            op = new DMS.DAL.operations();
            usersTB = new tables.dbo.users();
            usersTB = op.dboGetAllUsers(" username like '%" + txtUserSearch.Text + "%' or fullname like '%" + txtUserSearch.Text + "%'");
            dlusers.DataSource = usersTB.table;
            dlusers.DataBind();
        }

        public void fillUserFolders()
        {
            if (txtUserID.Text != "")
            {
                if (drpCompanyID.SelectedIndex < 1 && drpBranchID.SelectedIndex < 1)
                {
                    tables.dbo.folders fldrTB = new tables.dbo.folders();
                    fldrTB = op.dboGetAllFolders();
                    c.FillDropDownList(drpFolders, fldrTB.table);
                }
                if (drpCompanyID.SelectedIndex > 0 && drpBranchID.SelectedIndex < 1)
                {
                    DataTable dt = new DataTable();
                    dt = c.GetDataAsDataTable("SELECT     dbo.folders.fldrID, dbo.folders.fldrName, dbo.folders.fldrNameAr"
                    + " FROM         dbo.folders INNER JOIN"
                    + " dbo.companyFolders ON dbo.folders.fldrID = dbo.companyFolders.fldrID INNER JOIN"
                    + " dbo.companies ON dbo.companyFolders.companyID = dbo.companies.companyID"
                    + " WHERE     (dbo.companies.companyID = " + drpCompanyID.SelectedValue + ")");
                    c.FillDropDownList(drpFolders, dt);
                }

                if (drpBranchID.SelectedIndex > 0)
                {
                    DataTable dt = new DataTable();
                    dt = c.GetDataAsDataTable("SELECT dbo.folders.fldrID, dbo.folders.fldrName, dbo.folders.fldrNameAr"
                    + " FROM dbo.folders INNER JOIN"
                    + " dbo.branchFolders ON dbo.folders.fldrID = dbo.branchFolders.fldrID INNER JOIN"
                    + " dbo.branchs ON dbo.branchFolders.branchID = dbo.branchs.branchID"
                    + " WHERE     (dbo.branchs.branchID = " + drpBranchID.SelectedValue + ")");
                    c.FillDropDownList(drpFolders, dt);
                }
                string groupId = c.GetDataAsScalar("select top 1 grpID from [dbo].[users] where userid=" + txtUserID.Text + "").ToString();

                string sql1 = "SELECT dbo.folders.fldrName,dbo.folders.fldrNameAr, dbo.groupFolders.fldrID, dbo.groupFolders.allowInsert as 'allow', dbo.groupFolders.allowInsert, dbo.groupFolders.allowUpdate, dbo.groupFolders.allowDelete, dbo.groupFolders.allowOutgoing,dbo.groupFolders.allowIncoming,dbo.groupFolders.inheritSubFolders FROM dbo.folders LEFT JOIN dbo.groupFolders ON dbo.folders.fldrID = dbo.groupFolders.fldrID where dbo.groupFolders.grpID=" + groupId + "";
                DataTable DT1 = c.GetDataAsDataTable(sql1);

                //if (DT1.Rows.Count > 0)
                //{
                //    trGroups.Visible = true;
                //    BoundField BF1 = (BoundField)grdGroupFolders.Columns[1];
                //    if (Session["lang"].ToString() == "0")
                //        BF1.DataField = "fldrName";
                //    else
                //        BF1.DataField = "fldrNameAr";
                //    grdGroupFolders.DataSource = DT1;
                //    grdGroupFolders.DataBind();
                //}
                //else
                //{
                //    trGroups.Visible = false;
                //}
                string sql = "SELECT     dbo.folders.fldrName,dbo.folders.fldrNameAr, dbo.userFolders.fldrID, dbo.userFolders.allow, dbo.userFolders.allowInsert, dbo.userFolders.allowUpdate, dbo.userFolders.allowDelete, "
                        + " dbo.userFolders.allowOutgoing,dbo.userFolders.allowIncoming,dbo.userFolders.inheritSubFolders"
                        + " FROM         dbo.folders INNER JOIN"
                        + " dbo.userFolders ON dbo.folders.fldrID = dbo.userFolders.fldrID"
                        + " WHERE     (dbo.userFolders.userID = " + txtUserID.Text + ")";
                DataTable DT = c.GetDataAsDataTable(sql);
                //DT.Merge((DataTable)Session["currentDataSet"]);
                grdUsersFolders.DataSource = DT;
                BoundField BF = (BoundField)grdUsersFolders.Columns[1];
                if (Session["lang"].ToString() == "0")
                    BF.DataField = "fldrName";
                else
                    BF.DataField = "fldrNameAr";
                grdUsersFolders.DataBind();


            }
        }

        protected void btnAddFolder_Click(object sender, EventArgs e)
        {
            if (hdnFldrID.Value != "")
            {
                fillVariables();
                op = new DMS.DAL.operations();
                op.dboAddUserFolders(c.convertToInt32(txtUserID.Text),
                    c.convertToInt32(hdnFldrID.Value), true, true, true, true,false,false,true);
                fillUserFolders();
            }
        }

        protected void grdUsersFolders_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdUsersFolders.EditIndex = e.NewEditIndex;
            fillUserFolders();
        }

        protected void grdUsersFolders_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdUsersFolders.EditIndex = -1;
            fillUserFolders();
        }

        protected void grdUsersFolders_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Int32 fldrID; bool allow; bool allowInsert; bool allowUpdate; bool allowDelete; bool inheritSubFolders;
            CheckBox chk = new CheckBox();
            userID = c.convertToInt32(txtUserID.Text);
            fldrID = c.convertToInt32(grdUsersFolders.Rows[e.RowIndex].Cells[0].Text);
            chk = new CheckBox();
            chk = (CheckBox)grdUsersFolders.Rows[e.RowIndex].Cells[2].Controls[0];
            allow = c.convertToBool(chk.Checked);
            chk = new CheckBox();
            chk = (CheckBox)grdUsersFolders.Rows[e.RowIndex].Cells[3].Controls[0];
            allowInsert = c.convertToBool(chk.Checked);
            chk = new CheckBox();
            chk = (CheckBox)grdUsersFolders.Rows[e.RowIndex].Cells[4].Controls[0];
            allowUpdate = c.convertToBool(chk.Checked);
            chk = new CheckBox();
            chk = (CheckBox)grdUsersFolders.Rows[e.RowIndex].Cells[5].Controls[0];
            allowDelete = c.convertToBool(chk.Checked);

            chk = new CheckBox();
            chk = (CheckBox)grdUsersFolders.Rows[e.RowIndex].Cells[6].Controls[0];
            allowOutgoing = c.convertToBool(chk.Checked);
            chk = new CheckBox();
            chk = (CheckBox)grdUsersFolders.Rows[e.RowIndex].Cells[7].Controls[0];
            allowIncoming = c.convertToBool(chk.Checked);
            
            chk = new CheckBox();
            chk = (CheckBox)grdUsersFolders.Rows[e.RowIndex].Cells[8].Controls[0];
            inheritSubFolders = c.convertToBool(chk.Checked);

            op = new DMS.DAL.operations();
            op.dboUpdateUserFoldersByPrimaryKey(userID, fldrID, allow, allowInsert, allowUpdate, allowDelete, allowOutgoing, allowIncoming, inheritSubFolders);

            grdUsersFolders.EditIndex = -1;
            fillUserFolders();
        }

        protected void grdUsersFolders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 fldrID;
            userID = c.convertToInt32(txtUserID.Text);
            fldrID = c.convertToInt32(grdUsersFolders.Rows[e.RowIndex].Cells[0].Text);

            op = new DMS.DAL.operations();
            op.dboDeleteUserFoldersByPrimaryKey(fldrID, userID);

            fillUserFolders();
        }

        protected void drpCompanyID_SelectedIndexChanged(object sender, EventArgs e)
        {

            op = new DMS.DAL.operations();
            tables.dbo.branchs branchsTB = new tables.dbo.branchs();
            branchsTB = op.dboGetAllBranchs("CompanyID = " + drpCompanyID.SelectedValue);


            if (Session["lang"].ToString() == "1")
            {
                c.FillDropDownList(drpBranchID, branchsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "branchID", "BranchNameAr");
            }
            else
            {
                c.FillDropDownList(drpBranchID, branchsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "branchID", "BranchName");
            }

        }
        private void SaveUserImage(int UserID, string FullName)
        {

            string filePath = Server.MapPath("/") + "Images/Users/" + UserID.ToString() + ".png";
            if (!String.IsNullOrEmpty(hCroppieImage.Value))
            {
                File.WriteAllBytes(filePath, Convert.FromBase64String(hCroppieImage.Value.Replace("data:image/png;base64,", "")));
            }
            else if (!File.Exists(filePath))
            {
                string firstName = FullName.Split(' ')[0];
                string lastName = FullName.Split(' ')[0];
                if (FullName.Split(' ').Length > 1)
                    lastName = FullName.Split(' ')[1];
                else if (FullName.Length > 1)
                    lastName = FullName.Substring(1, FullName.Length - 1);
                MemoryStream ms = GenerateCircle(firstName, lastName);
                File.WriteAllBytes(filePath, ms.GetBuffer());
            }
        }
        private List<string> _BackgroundColours = new List<string> { "339966", "3366CC", "CC33FF", "FF5050" };
        public MemoryStream GenerateCircle(string firstName, string lastName)
        {
            var avatarString = string.Format("{0} {1}", firstName[0], lastName[0]).ToUpper();

            var randomIndex = new Random().Next(0, _BackgroundColours.Count - 1);
            var bgColour = _BackgroundColours[randomIndex];

            var bmp = new Bitmap(192, 192);
            var sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            var font = new Font("Arial", 72, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(bmp);

            graphics.Clear(Color.Transparent);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            using (Brush b = new SolidBrush(ColorTranslator.FromHtml("#" + bgColour)))
            {
                graphics.FillEllipse(b, new Rectangle(0, 0, 192, 192));
            }
            graphics.DrawString(avatarString, font, new SolidBrush(Color.WhiteSmoke), 95, 100, sf);
            graphics.Flush();

            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            return ms;
        }

        protected void dl_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)dlusers.Items[e.NewSelectedIndex];
            LinkButton btn = (LinkButton)item.FindControl("lnkSelect");
            int ID = c.convertToInt32(btn.CommandArgument);
            if (ID > 0)
            {
                lblRes.Text = "";
                usersTB = new tables.dbo.users();
                op = new DMS.DAL.operations();
                usersTB = op.dboGetUsersByPrimaryKey(ID);
                lblusermode.Text = usersTB.fieldFullName;
                //get sign
                string usersign = c.GetDataAsScalar("select top 1 ISNULL(Signature,'0') from users where userID=" + usersTB.fieldUserID).ToString();
                if (usersign != "0")
                {
                    divfileSign.Visible = true;
                }
                fillData(usersTB.table);
                rdoSaveMethod.SelectedValue = "1";
                lblFormMode.Text = "Edit User";
                if (Session["lang"].ToString() == "1")
                    lblFormMode.Text = "تعديل المستخدم";
                tblDetailsForm.Style["display"] = "block";
                rfvPassword.Enabled = false;
                rfvRePassword.Enabled = false;
            }

            divDetails.Visible = true;
            divList.Visible = false;
            btnUndo.Visible = true;
        }
        protected void btnUndo_ServerClick(object sender, EventArgs e)
        {
            divDetails.Visible = false;
            divList.Visible = true;
            lblusermode.Text = "";
        }
        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            divDetails.Visible = false;
            divList.Visible = true;
            lblusermode.Text = "";
        }

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            string s = "SELECT     dbo.companies.companyName,dbo.companies.companyNameAr, dbo.folders.fldrID, dbo.folders.fldrName,dbo.folders.fldrNameAr, dbo.folders.fldrParentID,dbo.folders.iconID" +
                                     " FROM         dbo.companies INNER JOIN" +
                                     " dbo.companyFolders ON dbo.companies.companyID = dbo.companyFolders.companyID INNER JOIN" +
                                     " dbo.folders ON dbo.companyFolders.fldrID = dbo.folders.fldrID";
            folderTree1.selectStatment = s;
            rdoSaveMethod.SelectedValue = "0";
            if (Session["lang"].ToString() == "0")
            {
                lblFormMode.Text = "Add New User";
                lblusermode.Text = "Add New User";
            }
            else
            {
                lblFormMode.Text = "اضافة مستخدم جديد";
                lblusermode.Text = "اضافة مستخدم جديد";
            }

            divDetails.Visible = true;
            divList.Visible = false;
            btnUndo.Visible = true;
            txtUserID.Text = "";
            lblRes.Text = "";
            tblDetailsForm.Style["display"] = "none";
            rfvPassword.Enabled = true;
            rfvRePassword.Enabled = true;
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:clear_form_elements('#ContentPlaceHolder1_ContentPlaceHolder1_divDetails'); ", true);
        }

        protected void btnEditImage_ServerClick(object sender, EventArgs e)
        {

        }
    }
}