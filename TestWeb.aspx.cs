using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;
//using pdftron;
//using pdftron.Common;
//using pdftron.Filters;
//using pdftron.SDF;
//using pdftron.PDF;

namespace dms
{
    public partial class TestWeb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //string strUploadFolder = Server.MapPath("Uploads");
            //fileUpload.PostedFile.SaveAs(Path.Combine(strUploadFolder, fileUpload.PostedFile.FileName));
            //PDFNetLoader loader = PDFNetLoader.Instance();
            //PDFNet.Initialize();
            //PDFDoc pdfdoc = new PDFDoc();
            //pdftron.PDF.Convert.ToPdf(pdfdoc, Path.Combine(strUploadFolder, fileUpload.PostedFile.FileName));
            //pdfdoc.Save(Path.Combine(strUploadFolder, fileUpload.PostedFile.FileName) + ".pdf", SDFDoc.SaveOptions.e_compatibility);
        }
    }
}