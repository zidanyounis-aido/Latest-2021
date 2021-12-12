using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using tables.dbo;

namespace DMS
{
    public class ReadFormGenerator
    {
        int DocumentTypeId; long DocumentId; 
        int UserId; bool IsLangAr = false;
        DMS.DAL.operations op = new DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();

        public enum ControlType
        {

            TextBox = 1,
            DropDownList = 2,
            CheckBoxList = 3,
            RadioButtonList = 4,
            Label = 5,
            Table = 6,
            Map = 7,
            Image=8,
            Link = 9
        }

        public void FormMetaManager(int documentTypeId, long documentId, int userId, bool isLangAr = false)
        {
            DocumentTypeId = documentTypeId;
            UserId = userId;
            IsLangAr = isLangAr;
            DocumentId = documentId;
        }

        public void GetPanal(ref Panel panel, ref StringBuilder autorScript, int fldrID, int userID, bool isAdd = true)
        {
            try
            {
                var metasTB = op.dboGetAllMetas($" metaidfk=0 and DocTypID = {DocumentTypeId} and visible=1 order by orderSeq,columnSeq,metaID");
                if (metasTB.hasRows)
                {

                    //bool isAllowFolder = Boolean.Parse(c.GetDataAsScalar("select top 1 ISNULL(allow,0) from userFolders where userID=" + userID + " and fldrID=" + fldrID + "").ToString());
                    //Panel tr = new Panel();
                    //tr.CssClass = "rowCSS";
                    Int32 rows = 0;
                    bool isEdit = false;
                    var divRow = new Panel();
                    for (Int32 i = 0; i < metasTB.rowsCount; i++)
                    {
                        int lastRow = metasTB.rowsCount;
                        bool isNewRow = false;
                        var tr = new Panel();
                        tr.CssClass = IsLangAr ? "col-xs-4 main-field-holder" : "col-xs-4 main-field-holder";
                        metasTB.currentIndex = i;
                        if (metasTB.fieldMetaID == 6)
                        {
                            int zd = 0;
                        }
                        string hasCustom = c.GetDataAsScalar("select top 1 [allowEdit] from [dbo].[metaUsersPermissions] where [metaID] =  " + metasTB.fieldMetaID + "  and [userID] =" + UserId).ToString();
                        if (hasCustom != "")
                        {
                            string sql2 = "select [allowEdit] from [dbo].[metaUsersPermissions] where [metaID] =  " + metasTB.fieldMetaID + "  and [userID] =" + UserId;
                            isEdit = Boolean.Parse(c.GetDataAsScalar(sql2).ToString() != "" ? c.GetDataAsScalar(sql2).ToString() : "False");
                            if (DocumentId > 0)
                            {
                                string sql = "select [allowRead] from [dbo].[metaUsersPermissions] where [metaID] =  " + metasTB.fieldMetaID + "  and [userID] =" + UserId;
                                bool isRead = Boolean.Parse(c.GetDataAsScalar(sql).ToString() != "" ? c.GetDataAsScalar(sql).ToString() : "False");
                                if (!isRead)
                                {
                                    continue;
                                }
                            }
                        }
                        //else
                        //{
                        //    if (!isAllowFolder)
                        //    {
                        //        continue;
                        //    }
                        //}
                        if (i == 0 || (rows != metasTB.fieldOrderSeq))
                        {
                            // add prev row
                            panel.Controls.Add(divRow);
                            // define new row
                            divRow = new Panel();
                            divRow.CssClass = "row";
                            divRow.Style.Add("width", "100%");
                            isNewRow = true;
                        }
                        else
                        {
                            isNewRow = false;
                        }
                        //if (rows != metasTB.fieldOrderSeq)
                        //{
                        //    tr = new Panel();
                        //    tr.CssClass = "rowCSS";
                        //}
                        rows = metasTB.fieldOrderSeq;
                        if (metasTB.fieldCtrlID == (int)ControlType.Label)
                        {
                            tr.Style.Add("width", metasTB.fieldWidth.ToString() + "%");
                            Panel td = new Panel();
                            Label lbl = new Label();
                            lbl.Font.Bold = true;
                            lbl.Text = IsLangAr ? metasTB.fieldMetaDescAr : metasTB.fieldMetaDesc;
                            td.Controls.Add(lbl);
                            tr.Controls.Add(td);
                            tr.CssClass = "col-xs-4 main-field-holder";
                            //panel.Controls.Add(tr);
                            // add tr in new div
                            divRow.Controls.Add(tr);
                        }
                        //else if (metasTB.fieldCtrlID == (int)ControlType.Link || metasTB.fieldCtrlID == (int)ControlType.Image || metasTB.fieldCtrlID == (int)ControlType.Map)
                        //{
                        //    tr.Style.Add("width", metasTB.fieldWidth.ToString() + "%");
                        //    AddNotEditableControls(ref tr, metasTB, i);
                        //    // add tr in new div
                        //    divRow.Controls.Add(tr);
                        //}
                        else
                        {
                            tr.Style.Add("width", metasTB.fieldWidth.ToString() + "%");
                            AddLabelForControl(ref tr, metasTB);
                            string value = "";
                            if (DocumentId > 0)
                            {
                                tables.dbo.documentMataValues docMetas = new tables.dbo.documentMataValues();
                                docMetas = op.dboGetDocumentMataValuesByPrimaryKey(DocumentId, metasTB.fieldMetaID);
                                if (docMetas.hasRows)
                                    value = docMetas.fieldValue;
                            }
                            //AddControl(ref autorScript, ref tr, metasTB, i, isEdit, value, isAdd);

                            Panel td = new Panel();
                            td.CssClass = IsLangAr ? "cellTDAr" : "cellTDEn";

                            if (metasTB.fieldCtrlID == (int)ControlType.Image)
                            {
                                GenerateImage(ref td, metasTB, i);
                            }
                            else if (metasTB.fieldCtrlID == (int)ControlType.Table)
                            {
                                GenerateTable(ref autorScript, ref td, metasTB, i);
                            }
                            else if (metasTB.fieldCtrlID == (int)ControlType.Map)
                            {
                                GenerateMap(ref td, metasTB, i);
                            }
                            else
                            {
                                Label lbl = new Label();
                                lbl.Text = value;
                                td.Controls.Add(lbl);

                            }
                            tr.Controls.Add(td);
                            // add tr in new div
                            divRow.Controls.Add(tr);
                        }
                        // panel.Controls.Add(divRow);
                        if ((i + 1) == lastRow)
                        {
                            divRow.Controls.Add(tr);
                            panel.Controls.Add(divRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // throw;
            }
        }

        private void AddLabelForControl(ref Panel tr, metas obj)
        {
            Panel td = new Panel();
            Label lbl = new Label();
            td.CssClass = IsLangAr ? "cellTDAr" : "cellTDEn";
            lbl.Text = IsLangAr ? obj.fieldMetaDescAr : obj.fieldMetaDesc;
            td.Controls.Add(lbl);
            tr.Controls.Add(td);

        }

        private void GenerateImage(ref Panel td, metas obj, int i)
        {
            HtmlImage img = new HtmlImage();
            img.Src = "../MetaUploader/" + obj.fieldDefaultTexts;
            img.ID = $"meta_{i + 1}";
            img.Width = Convert.ToUInt16(obj.fieldWidth);
            td.Controls.Add(img);
        }
        private void GenerateMap(ref Panel td, metas obj, int i)
        {
            HtmlGenericControl createMapDiv = new HtmlGenericControl("div");
            createMapDiv.ID = $"meta_{i + 1}";
            //createMapDiv.InnerHtml = "";
            createMapDiv.Style.Add("width", Convert.ToUInt16(obj.fieldWidth).ToString());
            createMapDiv.Style.Add("height", "410px");
            //createMapDiv.Style.Add("display", "none");
            createMapDiv.Attributes.Add("class", "map");
            // add iframe
            HtmlGenericControl iframe = new HtmlGenericControl("iframe");
            iframe.Attributes.Add("src", "https://maps.google.com/maps?q=" + obj.fieldDefaultTexts + "&z=15&output=embed");
            iframe.Attributes.Add("height", "410px");
            iframe.Attributes.Add("width", "100%");
            iframe.Attributes.Add("frameBorder", "0");
            createMapDiv.Controls.Add(iframe);
            td.Controls.Add(createMapDiv);
        }
        private void GenerateTable(ref StringBuilder autorScript, ref Panel td, metas obj, int i, bool isAdd = true)
        {
            var tbl = new Table();
            tbl.ID = $"meta_{i + 1}";
            tbl.Style.Add("width", Convert.ToUInt16(obj.fieldWidth).ToString() + "%");
            tbl.Attributes.Add("class", "my-table");
            c.GetTblRowAndColumnNumber(obj.fieldDefaultTexts, out int rowNumber, out int columnNumber);
            var metasTBCtrls = op.dboGetAllMetas($"metaIdFK = {obj.fieldMetaID}  order by orderSeq,columnSeq,metaID");
            TableHeaderRow tableHeaderRow = new TableHeaderRow();
            for (int x = 0; x < metasTBCtrls.rowsCount; x++)
            {
                metasTBCtrls.currentIndex = x;
                TableHeaderCell tableHeaderCell = new TableHeaderCell();
                tableHeaderCell.Text = (HttpContext.Current.Session["lang"].ToString() == "0") ? metasTBCtrls.fieldMetaDesc : metasTBCtrls.fieldMetaDescAr;
                tableHeaderRow.Controls.Add(tableHeaderCell);
            }
            tbl.Controls.Add(tableHeaderRow);
            if (metasTBCtrls.hasRows)
            {
                for (int r = 0; r < rowNumber; r++)
                {
                    TableRow row = new TableRow();

                    for (var j = 0; j < metasTBCtrls.rowsCount; j++)
                    {

                        metasTBCtrls.currentIndex = j;

                        TableCell cel = new TableCell();

                        var celPanel = new Panel();

                        string value = "";

                        tables.dbo.documentMataValues docMetas = new tables.dbo.documentMataValues();
                        docMetas = op.dboGetDocumentMataValuesByPrimaryKey(DocumentId, metasTBCtrls.fieldMetaID);
                        if (docMetas.hasRows)
                            value = docMetas.fieldValue.Split(',')[r];

                        Label lbl = new Label();
                        lbl.Text = value;
                        //lbl.CssClass = "frm-item tbl-from-elment";
                        //lbl.Attributes.Add("data-meta", metasTBCtrls.fieldMetaID.ToString());

                        celPanel.Controls.Add(lbl);

                        cel.Controls.Add(celPanel);

                        row.Cells.Add(cel);
                    }
                    tbl.Rows.Add(row);
                }
            }
            td.Controls.Add(tbl);
        }

    }
}