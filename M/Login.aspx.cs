using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.M
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //chkRemember.Attributes.Add("value", "1");
            }
            string brand = Request.QueryString["b"];
            //if (!string.IsNullOrEmpty(brand))
            //{
            //    //if (brand.ToLower() == "d")
            //    //  pnlLogo2.Visible = true;
            //    imgCompany.Visible = true;
            //    imgCompany.ImageUrl = "../Images/CompanyLogo" + brand + ".png";
            //}

            if (Request.Cookies["DMUserID"] != null)
            {
                login(Convert.ToInt32(Request.Cookies["DMUserID"].Value));
            }
        }

        protected void lnkLogin_Click(object sender, EventArgs e)
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
                            lblResult.Text = "User has been deactivated";
                        }
                    }
                    //Session.Timeout = settings.fieldSessionTimeoutMinutes;

                    lblResult.Text = "Username Or Password not matchs";
                }
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
                    Session["lang"] = rdoLang.SelectedValue;

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
                    { }
                    if (userTbl.fieldIsFirstLogin && settings.fieldFirstLoginChangePassword)
                    {
                        Response.Redirect("../admin/manageProfile.aspx?fl=1");
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(Request.QueryString["URL"]))
                            Response.Redirect("../M/");
                        else
                            Response.Redirect(Request.QueryString["URL"]);
                    }
                }
                else
                {
                    lblResult.Text = "User has been deactivated<br/>Please contact your system administrator";
                }
            }
        }
    }
}