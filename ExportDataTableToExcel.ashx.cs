using ClosedXML.Excel;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace dms
{
    /// <summary>
    /// Summary description for ExportDataTableToExcel
    /// </summary>
    public class ExportDataTableToExcel : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (HttpContext.Current.Session["myDataTable"] != null)
            {
                ExportToExcel((DataTable)HttpContext.Current.Session["myDataTable"]);
                //System.Threading.Thread.Sleep(10000);
                //HttpContext.Current.Session["myDataTable"] = null;
                //HttpContext.Current.Response.Redirect(HttpContext.Current.Request.UrlReferrer.ToString());
            }
        }
        public void ExportToExcel(DataTable myDataTable)
        {
            DataTable dt = myDataTable;
            try
            {

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Tasks");

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.Charset = "";
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Tasks_" + DateTime.Now.Date.Day.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Hour.ToString() + DateTime.Now.Date.Minute.ToString() + DateTime.Now.Date.Second.ToString() + DateTime.Now.Date.Millisecond.ToString() + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                    }
                }
            }
            catch (DocumentException de)
            {
            }
            // System.Web.HttpContext.Current.Response.Write(de.Message)
            catch (IOException ioEx)
            {
            }
            // System.Web.HttpContext.Current.Response.Write(ioEx.Message)
            catch (Exception ex)
            {
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}