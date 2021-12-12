using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dms.Calendar;
using System.Web.Script.Serialization;

namespace dms
{
    public partial class date : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["lang"] = "gregorian";
                spnlblcheckbox.InnerHtml = Session["lang"].ToString();
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Session["lang"].ToString() == "islamic")
            {
                Session["lang"] = "gregorian";
                spnlblcheckbox.InnerHtml = Session["lang"].ToString();
            }
            else
            {
                Session["lang"] = "islamic";
                spnlblcheckbox.InnerHtml = Session["lang"].ToString();
            }
        }

        public string ConvertHijriToGregorian()
        {
            return "";
        }
        public string ConvertGregorianToHijri()
        {
            return "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtdatajs.Value != "")
            {
                string result = "";
                var str = txtdatajs.Value.Split('/');
                if (Session["lang"].ToString() == "islamic")
                {
                    //get Gregorian
                    string date = str[2] + "-" + str[1] + "-" + str[0];
                    result = AladhanApi.CallhToG(date);
                    var serializer = new JavaScriptSerializer();
                    RootObjectDate RootObjectDate = serializer.Deserialize<RootObjectDate>(result);
                    lblotherdate.InnerHtml = RootObjectDate.data.gregorian.date;
                }
                else
                {
                    //get islamic
                    string date = str[0] + "-" + str[1] + "-" + str[2];
                    result = AladhanApi.CallgToH(date);
                    var serializer = new JavaScriptSerializer();
                    RootObjectDate RootObjectDate = serializer.Deserialize<RootObjectDate>(result);
                    lblotherdate.InnerHtml = RootObjectDate.data.hijri.date;
                }

            }
        }
    }
}