using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

namespace dms.Masters
{
    public partial class Main : System.Web.UI.MasterPage
    {
        public Int32 userID;
        SecurityNB.TextEncryption sec = new SecurityNB.TextEncryption();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        string client = "";
        string clientNameAr = "";
        string clientNameEn = "";
        string color1 = "";
        string color2 = "";

        protected override void OnInit(EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("../Screen/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            linkLtr.Attributes["href"] = (Session["lang"].ToString() == "0") ? "/Assets/UIKIT/css/Style-LTR.css" : "";
            linkBootstrap.Attributes["href"] = (Session["lang"].ToString() == "0") ? "" : "/Assets/UIKIT/css/bootstrap-rtl.min.css";
            if (!Page.IsPostBack)
            {
                Localize();
                // HijriCalendar hijri = new HijriCalendar();
                //DateTime firstDayInMonth = new DateTime(1433, 10, 11, hijri);
                DropDownListLang.SelectedValue = Session["lang"].ToString();
                lblnaveDate.InnerHtml = ConvertDateCalendar(DateTime.Now, "Hijri", "en-US");//;DateTime.Now.ToString("dd MMMM yyyy");
                lblnavTime.InnerHtml = DateTime.Now.ToString("hh:mm tt");
            }
            client = sysSettings.getSettingValue("client");
            clientNameAr = sysSettings.getSettingValue("client_name_ar");
            clientNameEn = sysSettings.getSettingValue("client_name_en");
            color1 = sysSettings.getSettingValue("client_color_1");
            color2 = sysSettings.getSettingValue("client_color_2");
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            imgCompany.Visible = true;
            imgCompany.ImageUrl = "../Assets/" + client + "/images/headerlogo.png";

            DMS.DAL.operations op = new DMS.DAL.operations();
            tables.dbo.users u = new tables.dbo.users();

            //this.Page.Title = (Session["lang"].ToString() == "0") ? clientNameEn:clientNameAr;

            hdnBackground.Value = "grid_noise.png";
            if (c.convertToString(Session["userId"]) == "")
            {
                //pnlLogged.Visible = false;
                Response.Redirect("../Screen/Login.aspx");
            }
            else
            {
                // lnkSignOut.ToolTip= (Session["lang"].ToString() == "0") ? "Logout" : "تسجيل خروج";
                //pnlLogged.Visible = true;
                lblWelcome.Text = c.getUserName(c.convertToInt32(Session["userId"]));
                if (System.IO.File.Exists(Server.MapPath("../Images/bg_" + Session["userId"].ToString() + ".jpg")))
                    hdnBackground.Value = "bg_" + Session["userId"].ToString() + ".jpg";


                userID = c.convertToInt32(Session["userId"]);

                if (DMS.Security.checkAllowedPage(userID, Request.Url.AbsolutePath))
                {
                    op = new DMS.DAL.operations();
                    Int32 eventID = op.dboAddSysEvents(userID, 2, DateTime.Now, Request.Url.AbsoluteUri);
                    op = new DMS.DAL.operations();
                    op.dboAddBrowseingEvents(eventID, DMS.Security.getProgramID(Request.Url.AbsolutePath));
                }
                else
                {
                    Response.Redirect("../screen/notAllowed.html");
                }

                string sql = "SELECT     TOP (100) PERCENT dbo.documentWFPath.ID " +
            " FROM dbo.documents INNER JOIN dbo.documentWFPath ON dbo.documents.docID = dbo.documentWFPath.docID INNER JOIN " +
            " dbo.docTypes ON dbo.documents.docTypID = dbo.docTypes.docTypID " +
            " WHERE     (dbo.documentWFPath.userID = " + Session["userID"].ToString() + ") AND (dbo.documentWFPath.actionType = 0) " +
            " ORDER BY dbo.documentWFPath.receiveDate DESC";
                DataTable DT = new DataTable();
                DT = c.GetDataAsDataTable(sql);

                if (Session["lang"].ToString() == "1")
                {
                    //lnkMail.Text = "لديك (" + DT.Rows.Count.ToString() + ") مستند بحاجة لإجراءاتك";
                }
                else
                {
                    //lnkMail.Text = "You have (" + DT.Rows.Count.ToString() + ") Documents need your actions";
                }


            }
        }
        public void Localize()
        {
            if (Session["lang"].ToString() == "0")
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            else
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("ar");
            lblmypersonalfile.InnerHtml = HudhudResources.Resources.Admin_UpdateProfile_mypersonalfile;
        }
        public string ConvertDateCalendar(DateTime DateConv, string Calendar, string DateLangCulture)
        {
            DateTimeFormatInfo DTFormat;
            DateLangCulture = DateLangCulture.ToLower();
            /// We can't have the hijri date writen in English. We will get a runtime error

            if (Calendar == "Hijri" && DateLangCulture.StartsWith("en-"))
            {
                DateLangCulture = "ar-sa";
            }

            /// Set the date time format to the given culture
            DTFormat = new System.Globalization.CultureInfo(DateLangCulture, false).DateTimeFormat;

            /// Set the calendar property of the date time format to the given calendar
            switch (Calendar)
            {
                case "Hijri":
                    DTFormat.Calendar = new System.Globalization.HijriCalendar();
                    break;
                case "Gregorian":
                    DTFormat.Calendar = new System.Globalization.GregorianCalendar();
                    break;
                default:
                    return "";
            }

            /// We format the date structure to whatever we want
            DTFormat.ShortDatePattern = "dd MMMM yyyy";
            return (DateConv.Date.ToString("dd MMMM yyyy", DTFormat));
        }
        protected void lnkSignOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("../Screen/Login.aspx");
        }

        protected void ddlChangeLang_ServerChange(object sender, EventArgs e)
        {
            Session["lang"] = DropDownListLang.SelectedValue;
            Localize();
            Response.Redirect("../Screen/Redirect.aspx");
        }
    }
}