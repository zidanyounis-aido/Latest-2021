using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Admin
{
    public partial class manageProfile : System.Web.UI.Page
    {
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void lnkChangePass_Click(object sender, EventArgs e)
        {
            Validate();
            if (IsValid)
            {
                lblRes.Text ="";
                DMS.DAL.operations op = new DMS.DAL.operations();
                tables.dbo.settings settings = op.dboGetSettingsByPrimaryKey(1);
                bool flag=true;
                
                if(!settings.fieldAllowUsernamePasswordMatch )
                {
                    if(txtNewPassword.Text.ToLower().Contains (Session["userName"].ToString().ToLower()))
                    {
                        flag = false;
                        if (Session["lang"].ToString() == "0")
                            lblRes.Text = "Password can't conatin the Username";
                        else
                            lblRes.Text = "كلمة السر يجب ألا تحتوي على اسم المرور";
                        lblRes.Text += "</br>";
                    }
                }

                if (!settings.fieldPasswordAllowStartSpace)
                {
                    if (txtNewPassword.Text.StartsWith(" "))
                    {
                        flag = false;
                        if (Session["lang"].ToString() == "0")
                            lblRes.Text += "Password can't start with Space";
                        else
                            lblRes.Text += "كلمة السر يجب ألا تبدأ بفراغ";
                        lblRes.Text += "</br>";
                    }
                }

                if (txtNewPassword.Text.Length < settings.fieldPasswordLength)
                {
                        flag = false;
                        if (Session["lang"].ToString() == "0")
                            lblRes.Text += "Password Min length is " + settings.fieldPasswordLength.ToString() + " letters";
                        else
                            lblRes.Text += "طول كلمة السر يجب ألا تقل عن " + settings.fieldPasswordLength.ToString() + " حرف";
                        lblRes.Text += "</br>";
                    
                }

                if (settings.fieldPasswordStrength > 1)
                {
                    if (settings.fieldPasswordStrength == 2)
                    {
                        if (!txtNewPassword.Text.Any(c => char.IsDigit(c)) || !txtNewPassword.Text.Any(c => char.IsLetter(c)))
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
                        if ((!txtNewPassword.Text.Any(c => char.IsDigit(c)) || !txtNewPassword.Text.Any(c => char.IsLetter(c))) )
                        {
                            if (txtNewPassword.Text.All(c => char.IsLetter(c) || char.IsDigit(c)))
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



                if (flag)
                {
                    DMS.DAL.DataProccess DP = new DMS.DAL.DataProccess();
                    DP.parameters.Clear();
                    DP.parameters.Add("@userID", Session["userID"]);
                    DP.parameters.Add("@password", c.encrypt( txtNewPassword.Text));
                    DP.excuteNonQuery("changePassword");

                    lblRes.Text = "You Password has been changed!";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "تم تغيير كلمة السر";

                    c.NonQuery("update users set isFirstLogin=0 where userID=" + Session["userID"].ToString());


                    if (Request.QueryString["fl"] == "1")
                    {
                        Response.Redirect("../Screen/");
                    }
                }
                
                
            }
        }

       
    }
}