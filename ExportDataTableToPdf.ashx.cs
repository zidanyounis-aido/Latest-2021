using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace dms
{
    /// <summary>
    /// Summary description for ExportDataTableToPdf
    /// </summary>
    public class ExportDataTableToPdf : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (HttpContext.Current.Session["myDataTable"] != null)
            {
                ExportToPdf((DataTable)HttpContext.Current.Session["myDataTable"]);
                //System.Threading.Thread.Sleep(10000);
                //HttpContext.Current.Session["myDataTable"] = null;
                //HttpContext.Current.Response.Redirect(HttpContext.Current.Request.UrlReferrer.ToString());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public void ExportToPdf(DataTable myDataTable)
        {
            DataTable dt = myDataTable;
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            var ArialFontFile = Path.Combine(HttpContext.Current.Server.MapPath("fonts"), "ARIALUNI.ttf");
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
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=TasksReport_" + DateTime.Now.Date.Day.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Hour.ToString() + DateTime.Now.Date.Minute.ToString() + DateTime.Now.Date.Second.ToString() + DateTime.Now.Date.Millisecond.ToString() + ".pdf");
                System.Web.HttpContext.Current.Response.Write(pdfDoc);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
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
    }
}