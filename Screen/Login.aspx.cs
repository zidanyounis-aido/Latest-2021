using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Screen
{
    public partial class Login : System.Web.UI.Page


    {
        string client = "";
        string clientNameAr = "";
        string clientNameEn = "";
        string color1 = "";
        string color2 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Localize();
            client = sysSettings.getSettingValue("client");
            clientNameAr = sysSettings.getSettingValue("client_name_ar");
            clientNameEn = sysSettings.getSettingValue("client_name_en");
            color1 = sysSettings.getSettingValue("client_color_1");
            color2 = sysSettings.getSettingValue("client_color_2");
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            imgCompany.Visible = true;
            imgCompany.ImageUrl = "../Assets/" + client + "/images/logo.png";
            //ltrClientName.Text = clientNameAr + "<br/>" + clientNameEn;
            //ltrClientName.Text = "Welcome";
            //imgProvider.ImageUrl = "../assets/provider/" + theme + "/logo.png";

            this.Title = clientNameEn;

            if (!String.IsNullOrEmpty(Request.QueryString["URL"]))
                if (!Request.QueryString["URL"].ToLower().Contains("default.aspx"))
                    text.Visible = false;

            
            if (!IsPostBack)
            {
                Session.Abandon();
                Session.Clear();
                //chkRemember.Attributes.Add("value", "1");
            }
            string brand = Request.QueryString["b"];
            if (!string.IsNullOrEmpty(brand))
            {
                //if (brand.ToLower() == "d")
                //  pnlLogo2.Visible = true;
                imgCompany.Visible = true;
                imgCompany.ImageUrl = "../Images/CompanyLogo" + brand + ".png";
            }

            if (Request.Cookies["DMUserID"] != null)
            {
                login(Convert.ToInt32(Request.Cookies["DMUserID"].Value));
            }
        }
        public string GetrecaptchaLanguage()
        {
            if (Session["lang"] != null && Session["lang"].ToString() == "0")
                return "en";
            else
                return "ar";
        }
        public void Localize()
        {
            if (Session["lang"] != null && Session["lang"].ToString() == "0")
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            else
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("ar");
        }
        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            if (Session["lang"] != null && Session["lang"].ToString() == "0")
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            else
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("ar");
            string errorMessage = string.Empty;
            bool isValidCaptcha = ValidateReCaptcha(ref errorMessage);

            if (isValidCaptcha)
            {
                DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
                System.Data.DataTable DT = new System.Data.DataTable();
                if (DMS.Security.isNotAllowedCharacters(txtPassword.Text) || DMS.Security.isNotAllowedCharacters(txtUserName.Text))
                {
                    CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                    string pass = c.encrypt(txtPassword.Text);
                    DT = sp.checkLog(txtUserName.Text, pass);
                    if (DT.Rows.Count > 0)
                    {
                        Session["logCounter"] = null;
                        login(Convert.ToInt32(DT.Rows[0]["userID"]));
                    }
                    else
                    {
                        Int32 count;
                        if (Session["logCounter"] == null)
                            Session["logCounter"] = 0;
                        count = Convert.ToInt32(Session["logCounter"]) + 1;
                        Session["logCounter"] = count;
                        DMS.DAL.operations op = new DMS.DAL.operations();
                        tables.dbo.settings settings = op.dboGetSettingsByPrimaryKey(1);
                        if (settings.fieldLockTimeOut <= count)
                        {
                            op = new DMS.DAL.operations();
                            tables.dbo.users user = op.dboGetAllUsers("username = '" + txtUserName.Text + "'");
                            if (user.hasRows)
                            {
                                DMS.BLL.specialCases SP = new DMS.BLL.specialCases();
                                SP.changeUserActiveStatus(user.fieldUserID, false);
                                lblResult.Text = HudhudResources.Resources.Screen_Login_Userhasbeendeactivated;
                            }
                        }
                        //Session.Timeout = settings.fieldSessionTimeoutMinutes;

                        lblResult.Text = HudhudResources.Resources.Screen_Login_UsernameOrPasswordnotmatch;
                    }
                }
            }
            else
            {
                lblResult.Text = errorMessage;
            }
        }

        private void login(Int32 uesrID)
        {
            DMS.DAL.operations op = new DMS.DAL.operations();
            tables.dbo.users userTbl = new tables.dbo.users();
            userTbl = op.dboGetUsersByPrimaryKey(uesrID);
            if (userTbl.hasRows)
            {
                if (userTbl.fieldActive)
                {
                    Session["userId"] = userTbl.fieldUserID;
                    DMS.Security.userID = userTbl.fieldUserID;
                    Session["userName"] = userTbl.fieldUserName;
                    Session["fullName"] = userTbl.fieldFullName;
                    Session["grpCode"] = userTbl.fieldGrpID;
                    Session["lang"] = ddlSelectLang.SelectedValue;//rdoLang.SelectedValue;
                    Session["clientId"] = userTbl.fieldClientId;
                    op = new DMS.DAL.operations();
                    tables.dbo.settings settings = op.dboGetSettingsByPrimaryKey(1);
                    Session.Timeout = settings.fieldSessionTimeoutMinutes;


                    //if (chkRemember.Checked)
                    //{
                    //    Response.Cookies.Add(new HttpCookie("DMUserID", userTbl.fieldUserID.ToString()));
                    //}

                    try
                    {
                        op = new DMS.DAL.operations();
                        Int32 eventID = op.dboAddSysEvents(userTbl.fieldUserID, 1, DateTime.Now, Request.Url.AbsoluteUri);
                        op = new DMS.DAL.operations();
                        op.dboAddLoginEvents(eventID, Request.UserHostAddress);

                    }
                    catch (Exception ex)
                    {
                    }
                    if (userTbl.fieldIsFirstLogin && settings.fieldFirstLoginChangePassword)
                    {
                        Response.Redirect("../admin/manageProfile.aspx?fl=1");
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(Request.QueryString["URL"]))
                            Response.Redirect("../screen/");
                        else
                            Response.Redirect(Request.QueryString["URL"]);
                    }
                }
                else
                {
                    lblResult.Text = HudhudResources.Resources.Screen_Login_Userhasbeendeactivated + "<br/>" + HudhudResources.Resources.Screen_Login_Pleasecontactyoursystemadministrator;
                }
            }
        }

        public bool ValidateReCaptcha(ref string errorMessage)
        {
            try
            {
                var gresponse = Request["g-recaptcha-response"];
                string secret = "6LedeQgaAAAAAMQgyzSnvNmUCNUEChrizXnKpIAZ";
                string ipAddress = GetIpAddress();

                var client = new WebClient();

                string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + secret + "&response=" + gresponse + "&remoteip=" + ipAddress;

                var response = client.DownloadString(url);

                var captchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(response);

                if (captchaResponse.Success)
                {
                    return true;
                }
                else
                {
                    var error = captchaResponse.ErrorCodes[0].ToLower();
                    switch (error)
                    {
                        case ("missing-input-secret"):
                            errorMessage = HudhudResources.Resources.Screen_Login_Thesecretkeyparameterismissing;
                            break;
                        case ("invalid-input-secret"):
                            errorMessage = HudhudResources.Resources.Screen_Login_Thegivensecretkeyparameterisinvalid;
                            break;
                        case ("missing-input-response"):
                            errorMessage = HudhudResources.Resources.Screen_Login_Theg_recaptcha_responseparameterismissing;
                            break;
                        case ("invalid-input-response"):
                            errorMessage = HudhudResources.Resources.Screen_Login_Thegivensecretkeyparameterisinvalid;
                            break;
                        default:
                            errorMessage = HudhudResources.Resources.Screen_Login_reCAPTCHAErrorPleasetryagain;
                            break;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                return true; // remove this code production
            }
        }

        string GetIpAddress()
        {
            var ipAddress = string.Empty;

            if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else if (!string.IsNullOrEmpty(Request.UserHostAddress))
            {
                ipAddress = Request.UserHostAddress;
            }

            return ipAddress;
        }

        protected void ddlSelectLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblResult.Text = "";
            Session["lang"] = ddlSelectLang.SelectedValue;
            Localize();
        }
    }
    public class ReCaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}