using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            int docID = 0;
            string res = Request.QueryString["docID"];
            res = c.decrypt(res);
            docID = c.convertToInt32(res);
            string verId = Request.QueryString["ver"];
            Session["pdfPath"] = Helper.GetUploadDiskPath() + docID + "-" + verId + ".pdf";
            Session["signture"] = c.GetDataAsScalar("select top 1 Signture from users where userID=" + Session["userID"].ToString()).ToString();
        }
    }
}