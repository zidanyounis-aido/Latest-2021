using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace dms.Controlers.Common
{
    public class DocToPdfConvert
    {
        public void ConvertToPDF(HttpPostedFile uploadedFile, string newFileName, string UploadPath)
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            string docExt = uploadedFile.FileName.Substring(uploadedFile.FileName.LastIndexOf(".") + 1);
            if (c.getFileType(docExt) == CommonFunction.clsCommon.fileType.Word_Document || c.getFileType(docExt) == CommonFunction.clsCommon.fileType.Text)
            {
                SaveWordAsPDF(uploadedFile, newFileName, UploadPath);
            }
            else if (c.getFileType(docExt) == CommonFunction.clsCommon.fileType.Power_Point)
            {
                SavePowerPointAsPDF(uploadedFile, newFileName, UploadPath);
            }
            else if (c.getFileType(docExt) == CommonFunction.clsCommon.fileType.Image || c.getFileType(docExt) == CommonFunction.clsCommon.fileType.TIFF)
            {
                SaveImageAsPDF(uploadedFile, newFileName, UploadPath);
            }
            else if (new StreamReader(uploadedFile.InputStream).ReadToEnd().containsHtmlTags())
            {
                SaveHtmlAsPDF(uploadedFile, newFileName, UploadPath);
            }
            else if (c.getFileType(docExt) == CommonFunction.clsCommon.fileType.Excel)
            {
                SaveExcelAsPDF(uploadedFile, newFileName, UploadPath);
            }
        }
        public void SaveWordAsPDF(HttpPostedFile uploadedFile, string newFileName, string UploadPath)
        {
            uploadedFile.InputStream.Position = 0;
            var document1 = new Aspose.Words.Document(uploadedFile.InputStream);
            document1.Save(UploadPath + newFileName + ".PDF", Aspose.Words.SaveFormat.Pdf);
        }
        public void SaveExcelAsPDF(HttpPostedFile uploadedFile, string newFileName, string UploadPath)
        {
            uploadedFile.InputStream.Position = 0;
            var document1 = new Aspose.Cells.Workbook(uploadedFile.InputStream);
            document1.Save(UploadPath + newFileName + ".PDF", Aspose.Cells.SaveFormat.Pdf);
        }
        public void SavePowerPointAsPDF(HttpPostedFile uploadedFile, string newFileName, string UploadPath)
        {
            uploadedFile.InputStream.Position = 0;
            var document1 = new Aspose.Slides.Presentation(uploadedFile.InputStream);
            document1.Save(UploadPath + newFileName + ".PDF", Aspose.Slides.Export.SaveFormat.Pdf);
        }
        public void SaveImageAsPDF(HttpPostedFile uploadedFile, string newFileName, string UploadPath)
        {
            var pdfDocument = new Aspose.Pdf.Document();
            pdfDocument.Pages.Add();
            Aspose.Pdf.Image image = new Aspose.Pdf.Image();
            uploadedFile.InputStream.Position = 0;
            image.ImageStream = uploadedFile.InputStream;
            pdfDocument.Pages[1].Paragraphs.Add(image);
            pdfDocument.Save(UploadPath + newFileName + ".PDF");
        }
        public void SaveHtmlAsPDF(HttpPostedFile uploadedFile, string newFileName, string UploadPath)
        {
            Aspose.Pdf.HtmlLoadOptions objLoadOptions = new Aspose.Pdf.HtmlLoadOptions();
            objLoadOptions.PageInfo.Margin.Bottom = 10;
            objLoadOptions.PageInfo.Margin.Top = 10;
            uploadedFile.InputStream.Position = 0;
            Aspose.Pdf.Document document1 = new Aspose.Pdf.Document(new MemoryStream(Encoding.UTF8.GetBytes(new StreamReader(uploadedFile.InputStream).ReadToEnd())), objLoadOptions);
            document1.Save(UploadPath + newFileName + ".PDF");
        }
    }
    internal static class HtmlExts
    {
        public static bool containsHtmlTag(this string text, string tag)
        {
            var pattern = @"<\s*" + tag + @"\s*\/?>";
            return Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase);
        }

        public static bool containsHtmlTags(this string text, string tags)
        {
            var ba = tags.Split('|').Select(x => new { tag = x, hastag = text.containsHtmlTag(x) }).Where(x => x.hastag);

            return ba.Count() > 0;
        }

        public static bool containsHtmlTags(this string text)
        {
            return
                text.containsHtmlTags(
                    "a|abbr|acronym|address|area|b|base|bdo|big|blockquote|body|br|button|caption|cite|code|col|colgroup|dd|del|dfn|div|dl|DOCTYPE|dt|em|fieldset|form|h1|h2|h3|h4|h5|h6|head|html|hr|i|img|input|ins|kbd|label|legend|li|link|map|meta|noscript|object|ol|optgroup|option|p|param|pre|q|samp|script|select|small|span|strong|style|sub|sup|table|tbody|td|textarea|tfoot|th|thead|title|tr|tt|ul|var");
        }
    }
}