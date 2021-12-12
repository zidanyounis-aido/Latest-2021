using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;

namespace dms.Admin
{
    public partial class UpdateProfile : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        tables.dbo.users usersTB = new tables.dbo.users();
        Int32 userID; string userName; string password; string fullName; Int32 grpID; bool active;
        Int32 companyID; Int32 branchID; Int32 departmentID; Int32 positionID; string email;
        bool allowCustomWF; bool allowCreateFolders; bool allowReplaceDocuments; bool allowDiwan;
        bool isFirstLogin; DateTime passwordCreationDate; DateTime passwordModifiedDate; string lastPassword; string Phone;
        Int32 clientID;bool isEmailVerfied;bool isMobileFirstLogin;string Signature;

        protected void Page_Load(object sender, EventArgs e)
        {
            Localize();
            if (!IsPostBack)
            {
                usersTB = new tables.dbo.users();
                op = new DMS.DAL.operations();
                usersTB = op.dboGetUsersByPrimaryKey(c.convertToInt32(Session["userID"].ToString()));

                fillData(usersTB.table);
            }
        }
        public void fillData(DataTable DT)
        {
            txtPassword.Text = "";
            txtRePassword.Text = "";
            c.fillData(DT, 0, usersTB.columnsArray, Page);
            if (File.Exists(Server.MapPath("/Images/Users/" + Session["userID"] + ".png")))
                imgUser.Src = "~/Images/Users/" + Session["userID"] + ".png";
            else
                imgUser.Src = "";
            lblUserName.Text = usersTB.fieldFullName;
            hdnPassword.Value = usersTB.fieldPassword;
            hdnIsEmailVerfied.Value = usersTB.fieldIsEmailVerfied.ToString();
            hdnIsMobileFirstLogin.Value = usersTB.fieldIsMobileFirstLogin.ToString();
            hdnSignature.Value = usersTB.fieldSignature;
        }
        public void Localize()
        {
            if (Session["lang"].ToString() == "0")
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            else
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("ar");
            lblmypersonalfile.InnerHtml = HudhudResources.Resources.Admin_UpdateProfile_mypersonalfile;
            lblpassword.Text = HudhudResources.Resources.Admin_UpdateProfile_password;
            lblpasswordconfirmation.Text = HudhudResources.Resources.Admin_UpdateProfile_passwordconfirmation;
            lblE_mail.Text = HudhudResources.Resources.Admin_UpdateProfile_E_mail;
            lblcellphone.Text = HudhudResources.Resources.Admin_UpdateProfile_cellphone;
            cvPassworddonotmatch.ErrorMessage = "";// HudhudResources.Resources.Admin_UpdateProfile_Passworddonotmatch;
            revE_mailformatiswrong.ErrorMessage = "";//HudhudResources.Resources.Admin_UpdateProfile_E_mailformatiswrong;
            rfvPleaseenteryoure_mail.ErrorMessage = "";//HudhudResources.Resources.Admin_UpdateProfile_Pleaseenteryoure_mail;
            SpanSave.InnerHtml = HudhudResources.Resources.Admin_UpdateProfile_Save;
            lblRetreat.InnerHtml = HudhudResources.Resources.Admin_UpdateProfile_Retreat;
            lblSurvey.InnerHtml = HudhudResources.Resources.Admin_UpdateProfile_Survey;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                lblRes.Text = "";
                DMS.DAL.operations op = new DMS.DAL.operations();
                tables.dbo.settings settings = op.dboGetSettingsByPrimaryKey(1);
                bool flag = true;
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
                    userTB = op.dboGetUsersByPrimaryKey(int.Parse(Session["userID"].ToString()));
                    fillVariables(userTB);
                    Int32 varRes = 0;
                    if (txtPassword.Text == "")
                        password = hdnPassword.Value;
                    varRes = op.dboUpdateUsersByPrimaryKey(userID, userName, password, fullName, grpID, active, companyID, branchID, 
                        departmentID, positionID, email, allowCustomWF, allowCreateFolders, allowReplaceDocuments, allowDiwan, 
                        isFirstLogin, passwordCreationDate, passwordModifiedDate, lastPassword, Signature,
                        Phone,isMobileFirstLogin,isEmailVerfied, clientID);
                    SaveUserImage(userID, fullName);
                    lblRes.Text = "Save successful";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "تم الحفظ بنجاح";
                    usersTB = op.dboGetUsersByPrimaryKey(c.convertToInt32(Session["userID"].ToString()));
                    fillData(usersTB.table);
                    RegisterStartupScript("closeModal", "<script>closeModal()</script>");
                }
            }
        }
        public void fillVariables(tables.dbo.users userTB)
        {
            userID = c.convertToInt32(userTB.fieldUserID);
            userName = c.convertToString(userTB.fieldUserName);
            password = c.encrypt(txtPassword.Text);
            fullName = c.convertToString(userTB.fieldFullName);
            grpID = c.convertToInt32(userTB.fieldGrpID);
            active = c.convertToBool(userTB.fieldActive);
            companyID = c.convertToInt32(userTB.fieldCompanyID);
            branchID = c.convertToInt32(userTB.fieldBranchID);
            departmentID = c.convertToInt32(userTB.fieldDepartmentID);
            positionID = c.convertToInt32(userTB.fieldPositionID);
            email = c.convertToString(txtEmail.Text);
            allowCustomWF = userTB.fieldAllowCustomWF;
            allowCreateFolders = userTB.fieldAllowCreateFolders;
            allowReplaceDocuments = userTB.fieldAllowReplaceDocuments;
            allowDiwan = userTB.fieldAllowDiwan;

            isFirstLogin = c.convertToBool(userTB.fieldIsFirstLogin);
            passwordCreationDate = c.convertToDateTime(userTB.fieldPasswordCreationDate);
            passwordModifiedDate = c.convertToDateTime(userTB.fieldPasswordModifiedDate);
            lastPassword = c.convertToString(userTB.fieldLastPassword);
            Phone = c.convertToString(txtPhone.Text);
            clientID = c.convertToInt32(Session["clientId"]);
            isEmailVerfied = c.convertToBool(hdnIsEmailVerfied.Value);
            isMobileFirstLogin = c.convertToBool(hdnIsMobileFirstLogin.Value);
            Signature = hdnSignature.Value;

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
                MemoryStream ms = GenerateCircle(FullName.Split(' ')[0], FullName.Split(' ').Length > 0 ? FullName.Split(' ')[1] : "");
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
    }
}