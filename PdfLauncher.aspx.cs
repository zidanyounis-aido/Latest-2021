using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms
{
    public partial class PdfLauncher : System.Web.UI.Page
    {
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userID"] = Request.QueryString["userID"];
            if (c.convertToString(Session["userId"]) == "")
            {
                Response.Redirect("Screen/Login.aspx");
            }
            int docID = 0;
            hdnCurrentName.Value = Session["userName"].ToString();
            string res = Request.QueryString["docID"];
            res = c.decrypt(res);
            docID = c.convertToInt32(res);
            string verId = Request.QueryString["ver"];
            //copy file
            // Get the current app path:
            string Tranfiles, ProcessedFiles;
            Tranfiles = Helper.GetUploadDiskPath() + docID.ToString() + "-" + verId.ToString() + ".pdf";
            if (File.Exists(Helper.GetTempDiskPath(docID.ToString() + "-" + verId.ToString() + ".pdf")))
            {
                File.Delete(Helper.GetTempDiskPath(docID.ToString() + "-" + verId.ToString() + ".pdf"));
            }
            //ProcessedFiles = Server.MapPath(@"~\godurian\sth100\ProcessedFiles");
            ProcessedFiles = Helper.GetTempDiskPath(docID.ToString() + "-" + verId.ToString() + ".pdf");
            File.Copy(Tranfiles, ProcessedFiles);

            string str = "Temp/" + Helper.GetClientNumber() + "/" + docID.ToString() + "-" + verId.ToString() + ".pdf";
            // string str = Helper.GetTempDiskPath() + docID.ToString() + "-" + verId.ToString() + ".pdf";
            hdnpath.Value = str;
            //Example 5
            string image = c.GetDataAsScalar("select top 1 Signature from users where userID=" + int.Parse(Request.QueryString["userID"].ToString())).ToString();
            string barcode = c.GetDataAsScalar("select top 1 Barcode from documents where docID=" + docID).ToString();
            //string x = c.GetDataAsScalar("select top 1 Signature from users where userID=" + Session["userID"].ToString()).ToString();
            hdnsignture.Value = image;
            hdnDocLable.Value = barcode;
            hdndocument.Value = docID.ToString() + "-" + verId.ToString();
            hdnuser.Value = Session["userID"].ToString();
        }
    }
}