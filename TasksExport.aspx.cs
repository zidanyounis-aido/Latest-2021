using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ClosedXML.Excel;

namespace dms
{
    public partial class TasksExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                //string strPDF = c.encrypt("PDF");
                //string strExcel = c.encrypt("PDF");
                //string strIDs = c.encrypt("1,2,3,4,5,6,7,8,9,10");
                //PDF//http://localhost:15427/TasksExport.aspx?IDs=dCui8abGjN9wQoTV9zXZXSWv82ivBm2HnAK9GbVqpsk=&fType=sI35pe1RXore4/CcxWQGFg==
                //EXCEL//http://localhost:15427/TasksExport.aspx?IDs=dCui8abGjN9wQoTV9zXZXSWv82ivBm2HnAK9GbVqpsk=&fType=t4Rlb/5x6bMrQdanetECMg==

                if (!string.IsNullOrEmpty(Request.QueryString["IDs"]) && !string.IsNullOrEmpty(Request.QueryString["fType"]))
                {
                    ViewState["IDs"] = c.decrypt(Request.QueryString["IDs"]);
                    ViewState["fType"] = c.decrypt(Request.QueryString["fType"]);
                    Export(null, null);
                }
            }
        }
        protected void Export(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DMS.DAL.DataProccess DP = new DMS.DAL.DataProccess();
            DP.parameters.Clear();
            DP.parameters.Add("@TaskIDs", ViewState["IDs"]);
            dt = DP.excuteQuery("getTasksList");
            if (ViewState["fType"].ToString().ToLower() == "excel")
                ExportToEXEL(dt);
            else
                ExportToPdf(dt);
        }

        public void ExportToPdf(DataTable myDataTable)
        {
            DataTable dt = myDataTable;
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            var ArialFontFile = Path.Combine(Server.MapPath("fonts"), "ARIALUNI.ttf");
            //Reference a Unicode font to be sure that the symbols are present. 
            BaseFont bfArialUniCode = BaseFont.CreateFont(ArialFontFile, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            //Create a font from the base font
            Font font12 = new Font(bfArialUniCode, 12);
            Font font10 = new Font(bfArialUniCode, 10);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                if (dt.Rows.Count > 0)
                {
                    PdfPTable PdfTable = new PdfPTable(1)
                    {
                        RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                    };
                    PdfTable.TotalWidth = 200f;
                    PdfTable.LockedWidth = true;
                    //Ensure that wrapping is on, otherwise Right to Left text will not display 
                    PdfTable.DefaultCell.NoWrap = false;
                    PdfPCell PdfPCell;
                    //PdfPCell PdfPCell = new PdfPCell(new Phrase(new Chunk("Employee Details", font18)));
                    //PdfPCell.Border = Rectangle.NO_BORDER;
                    //PdfTable.AddCell(PdfPCell);
                    //DrawLine(writer, 25f, pdfDoc.Top - 30f, pdfDoc.PageSize.Width - 25f, pdfDoc.Top - 30f, new BaseColor(System.Drawing.Color.Red));
                    pdfDoc.Add(PdfTable);

                    PdfTable = new PdfPTable(dt.Columns.Count)
                    {
                        RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                    };
                    PdfTable.DefaultCell.NoWrap = false;
                    PdfTable.SpacingBefore = 20f;
                    for (int columns = 0; columns <= dt.Columns.Count - 1; columns++)
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Columns[columns].ColumnName, font12)))
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                        };
                        //Ensure that wrapping is on, otherwise Right to Left text will not display 
                        PdfPCell.NoWrap = false;
                        PdfTable.AddCell(PdfPCell);
                    }

                    for (int rows = 0; rows <= dt.Rows.Count - 1; rows++)
                    {
                        for (int column = 0; column <= dt.Columns.Count - 1; column++)
                        {
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Rows[rows][column].ToString(), font10)))
                            {
                                RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                            };
                            //Ensure that wrapping is on, otherwise Right to Left text will not display 
                            PdfPCell.NoWrap = false;
                            PdfTable.AddCell(PdfPCell);
                        }
                    }
                    pdfDoc.Add(PdfTable);
                }
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename=TasksReport_" + DateTime.Now.Date.Day.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Hour.ToString() + DateTime.Now.Date.Minute.ToString() + DateTime.Now.Date.Second.ToString() + DateTime.Now.Date.Millisecond.ToString() + ".pdf");
                System.Web.HttpContext.Current.Response.Write(pdfDoc);
                Response.Flush();
                Response.End();
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

        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, BaseColor color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }

        public void ExportToEXEL(DataTable myDataTable)
        {
            DataTable dt = myDataTable;
            try
            {

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Tasks");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=TasksReport_" + DateTime.Now.Date.Day.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Hour.ToString() + DateTime.Now.Date.Minute.ToString() + DateTime.Now.Date.Second.ToString() + DateTime.Now.Date.Millisecond.ToString() + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
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
    }

}