using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
namespace dms.enc
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            if (TextBox1.Text != "")
            {
                TextBox2.Text = c.decrypt(TextBox1.Text);
            }
            else
            {
                if (TextBox2.Text != "")
                {
                    TextBox1.Text = c.encrypt(TextBox2.Text);
                }
            }
        }
    }
}