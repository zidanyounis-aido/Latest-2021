using Borland.Delphi;
using iTextSharp.text.pdf.codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using tables.dbo;

namespace dms.MangeForm
{
    public class FormMetaManager
    {
        DMS.DAL.operations op => new DMS.DAL.operations();
        CommonFunction.clsCommon c => new CommonFunction.clsCommon();


        #region Properties
        private int DocumentTypeId { get; set; }
        private long DocumentId { get; set; }
        private int UserId { get; set; }
        private bool IsLangAr { get; set; }
        #endregion

        public FormMetaManager(int documentTypeId, long documentId, int userId, bool isLangAr = false)
        {
            DocumentTypeId = documentTypeId;
            UserId = userId;
            IsLangAr = isLangAr;
            DocumentId = documentId;
        }

        public void GetPanal(ref Panel panel, ref StringBuilder autorScript, int fldrID, int userID,bool isAdd=true)
        {
            try
            {
                var metasTB = op.dboGetAllMetas($" metaidfk=0 and DocTypID = {DocumentTypeId} and visible=1 order by orderSeq,columnSeq,metaID");
                if (metasTB.hasRows)
                {

                    bool isAllowFolder = Boolean.Parse(c.GetDataAsScalar("select top 1 ISNULL(allow,0) from userFolders where userID=" + userID + " and fldrID=" + fldrID + "").ToString());
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
                        else
                        {
                            if (!isAllowFolder)
                            {
                                continue;
                            }
                        }
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
                            AddControl(ref autorScript, ref tr, metasTB, i, isEdit, value, isAdd);
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

        private void AddControl(ref StringBuilder autorScript, ref Panel tr, metas obj, int i, bool isEdit, string value,bool isAdd=true)
        {
            Panel td = new Panel();
            td.CssClass = IsLangAr ? "cellTDAr" : "cellTDEn";
            HiddenField hdn = new HiddenField();
            hdn.ID = $"hdn_{i + 1}";
            hdn.Value = obj.fieldMetaID.ToString();
            td.Controls.Add(hdn);



            if (obj.fieldCtrlID == (int)ControlType.TextBox)
            {
                GenerateTextBox(ref autorScript, ref td, obj, i, isEdit, value, isAdd);
            }
            else if (obj.fieldCtrlID == (int)ControlType.DropDownList)
            {
                GenerateDropDownList(ref autorScript, ref td, obj, i, isEdit, value);
            }
            else if (obj.fieldCtrlID == (int)ControlType.CheckBoxList)
            {
                GenerateCheckBox(ref autorScript, ref td, obj, i, isEdit, value);
            }
            else if (obj.fieldCtrlID == (int)ControlType.RadioButtonList)
            {
                GenerateRadioButton(ref autorScript, ref td, obj, i, isEdit, value);
            }
            else if (obj.fieldCtrlID == (int)ControlType.Image)
            {
                GenerateImage(ref td, obj, i);
            }
            else if (obj.fieldCtrlID == (int)ControlType.Table)
            {
                GenerateTable(ref autorScript, ref td, obj, i);
            }
            else if (obj.fieldCtrlID == (int)ControlType.Map)
            {
                GenerateMap(ref td, obj, i);
            }
            tr.Controls.Add(td);
        }
        private void GenerateTextBox(ref StringBuilder autorScript, ref Panel td, metas obj, int i, bool isEdit, string value,bool isAdd,string idName="",int fieldMetaID=0)
        {
            TextBox txt = new TextBox();
            txt.Attributes.Add("autocomplete", "off");
            txt.CssClass = "frm-item";
            txt.TabIndex = Convert.ToInt16(4 + i);

            txt.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            txt.ID = idName == "" ? $"meta_{i + 1}" : "";

            if (obj.fieldDefaultValues.ToLower().StartsWith("#expr:") && obj.fieldDefaultTexts != "")
            {
                if (!isAdd)
                {
                    //string textValue = c.fixMetaExp(obj.fieldDefaultValues, i);
                    //textValue = textValue.Substring(textValue.IndexOf(":") + 1);
                    //if (!textValue.EndsWith(";"))
                    //    textValue = textValue + ";";
                    //txt.Text = textValue;
                    //autorScript.AppendLine(textValue);
                }
                txt.Attributes.Add("readonly", "readonly");
            }
            else
            {
                txt.Text =  "";
            }

            if (obj.fieldMetaDataType.ToLower() == DataType.DateTime.ToString().ToLower())
            {
                txt.CssClass = "frm-item dateFeild";
            }
            else if (obj.fieldMetaDataType.ToLower() == DataType.Int32.ToString().ToLower())
            {
                txt.Width = 75;
            }
            if (value != "")
                txt.Text = value;
            if (isEdit == false)
                txt.Enabled = false;
            td.Controls.Add(txt);

            if (obj.fieldMetaDataType.ToLower() == DataType.Decimal.ToString())
            {
                RegularExpressionValidator val = new RegularExpressionValidator();
                val.ControlToValidate = txt.ID;
                val.ValidationExpression = @"((\d+)((\.\d{1,2})?))$";

                val.ErrorMessage = IsLangAr ? "الرجاء إدخال قيمة رقمية" : "Please enter decimal value";

                val.Enabled = true;
                td.Controls.Add(val);
            }
            if (obj.fieldRequired)
            {
                RequiredFieldValidator req = new RequiredFieldValidator();
                req.ControlToValidate = txt.ID;
                req.ErrorMessage = "*";
                req.Enabled = true;
                td.Controls.Add(req);
            }
            if (idName == "tblmeta_") //this genrate inside table
            {
                txt.CssClass = "frm-item tbl-from-elment";
                txt.Attributes.Add("data-meta", fieldMetaID.ToString());
            }
        }
        private void GenerateDropDownList(ref StringBuilder autorScript, ref Panel td, metas obj, int i, bool isEdit, string value, string idName = "", int fieldMetaID = 0)
        {
            DropDownList drp = new DropDownList();
            drp.TabIndex = Convert.ToInt16(6 + i);
            drp.CssClass = "frm-drop";
            drp.ID = $"meta_{i + 1}";
            drp.ID = idName == "" ? $"meta_{i + 1}" : "";
            if (!obj.fieldDefaultTexts.ToLower().StartsWith("db:"))
            {
                char[] chars = { ',', ';', '،' };
                if (obj.fieldDefaultTexts.IndexOfAny(chars) > -1)
                {
                    string[] texts;
                    texts = obj.fieldDefaultTexts.Split(chars);
                    string[] values;
                    values = obj.fieldDefaultValues.Split(chars);
                    for (Int32 j = 0; j < texts.Length; j++)
                    {
                        drp.Items.Add(new ListItem(texts[j].Trim(), j < values.Length ? values[j].Trim() : texts[j].Trim()));
                    }
                }
                else
                {
                    drp.Items.Add(new ListItem(obj.fieldDefaultTexts.Trim(), obj.fieldDefaultValues.Trim()));
                }
            }
            else
            {
                string[] query = obj.fieldDefaultTexts.Split(':');
                string[] queryVal = obj.fieldDefaultValues.Split(':');
                string tableName = query[1];
                string textFeild = query[2];
                string valueFeild = queryVal[2];
                string cond = "";
                if (query.Length > 3)
                    cond = " where " + query[3];
                string drpSQL = $"select {valueFeild} , { textFeild } from { tableName } { cond } order by { textFeild}";
                var drpDT = c.GetDataAsDataTable(drpSQL);
                c.FillDropDownList(drp, drpDT);
            }
            if (value != "")
                drp.SelectedValue = value;
            if (isEdit == false)
                drp.Enabled = false;
            if (idName == "tblmeta_") //this genrate inside table
            {
                drp.CssClass = "frm-item tbl-from-elment";
                drp.Attributes.Add("data-meta", fieldMetaID.ToString());
            }
            td.Controls.Add(drp);
        }
        private void GenerateCheckBox(ref StringBuilder autorScript, ref Panel td, metas obj, int i, bool isEdit, string value, string idName = "", int fieldMetaID = 0)
        {

            CheckBoxList chk = new CheckBoxList();
            chk.TabIndex = Convert.ToInt16(6 + i);
           // chk.ID = $"meta_{i + 1}";
            chk.ID = idName == "" ? $"meta_{i + 1}" : "";
            if (obj.fieldDefaultTexts.Contains(','))
            {
                string[] texts;
                texts = obj.fieldDefaultTexts.Split(',');
                string[] values;
                values = obj.fieldDefaultValues.Split(',');
                for (var j = 0; j < texts.Length; j++)
                {
                    chk.Items.Add(new ListItem(texts[j].Trim(), j < values.Length ? values[j].Trim() : texts[j].Trim()));
                }
            }
            else
            {
                chk.Items.Add(new ListItem(obj.fieldDefaultTexts.Trim(), obj.fieldDefaultValues.Trim()));
            }

            if (value != "")
                chk.SelectedValue = value;
            if (isEdit == false)
                chk.Enabled = false;
            if (idName == "tblmeta_") //this genrate inside table
            {
                chk.CssClass = "frm-item tbl-from-elment";
                chk.Attributes.Add("data-meta", fieldMetaID.ToString());
            }
            td.Controls.Add(chk);

        }
        private void GenerateRadioButton(ref StringBuilder autorScript, ref Panel td, metas obj, int i, bool isEdit, string value, string idName = "", int fieldMetaID = 0)
        {
            RadioButtonList rdo = new RadioButtonList();
            rdo.TabIndex = Convert.ToInt16(6 + i);
            //rdo.ID = $"meta_{i + 1}";
            rdo.ID = idName == "" ? $"meta_{i + 1}" : "";
            if (obj.fieldDefaultTexts.Contains(','))
            {
                string[] texts;
                texts = obj.fieldDefaultTexts.Split(',');
                string[] values;
                values = obj.fieldDefaultValues.Split(',');
                for (var j = 0; j < texts.Length; j++)
                {
                    rdo.Items.Add(new ListItem(texts[j].Trim(), j < values.Length ? values[j].Trim() : texts[j].Trim()));
                }
            }
            else
            {
                rdo.Items.Add(new ListItem(obj.fieldDefaultTexts.Trim(), obj.fieldDefaultValues.Trim()));
            }

            if (value != "")
                rdo.SelectedValue = value;
            if (isEdit == false)
                rdo.Enabled = false;
            if (idName == "tblmeta_") //this genrate inside table
            {
                rdo.CssClass = "frm-item tbl-from-elment";
                rdo.Attributes.Add("data-meta", fieldMetaID.ToString());
            }
            td.Controls.Add(rdo);
        }

        private void AddNotEditableControls(ref Panel tr, metas obj, int i)
        {
            Panel td = new Panel();
            td.CssClass = IsLangAr ? "cellTDAr" : "cellTDEn";
            HiddenField hdn = new HiddenField();
            hdn.ID = $"hdn_{i + 1}";
            hdn.Value = obj.fieldMetaID.ToString();
            td.Controls.Add(hdn);

            if (obj.fieldCtrlID == (int)ControlType.Image)
            {
                GenerateImage(ref td, obj, i);
            }
            else if (obj.fieldCtrlID == (int)ControlType.Link)
            {
                GenerateLink(ref td, obj, i);
            }
            else if (obj.fieldCtrlID == (int)ControlType.Map)
            {
                GenerateMap(ref td, obj, i);
            }
            tr.Controls.Add(td);
        }
        private void GenerateLink(ref Panel td, metas obj, int i)
        {
            HtmlAnchor anchor = new HtmlAnchor();
            anchor.ID = $"meta_{i + 1}";
            anchor.HRef = obj.fieldDefaultTexts;
            anchor.InnerHtml = IsLangAr ? obj.fieldMetaDescAr : obj.fieldMetaDesc;
            td.Controls.Add(anchor);
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
        private void GenerateTable(ref StringBuilder autorScript, ref Panel td, metas obj, int i,bool isAdd=true)
        {
            var tbl = new Table();
            tbl.ID = $"meta_{i + 1}";
            tbl.Style.Add("width", Convert.ToUInt16(obj.fieldWidth).ToString());
            tbl.Attributes.Add("class", "my-table");
            c.GetTblRowAndColumnNumber(obj.fieldDefaultTexts, out int rowNumber, out int columnNumber);
            var metasTBCtrls = op.dboGetAllMetas($"metaIdFK = {obj.fieldMetaID}  order by orderSeq,columnSeq,metaID");
            TableHeaderRow tableHeaderRow = new TableHeaderRow();
            for (int x = 0; x < metasTBCtrls.rowsCount; x++)
            {
                metasTBCtrls.currentIndex = x;
                TableHeaderCell tableHeaderCell = new TableHeaderCell();
                tableHeaderCell.Text = (HttpContext.Current.Session["lang"].ToString() == "0") ?  metasTBCtrls.fieldMetaDesc : metasTBCtrls.fieldMetaDescAr;
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

                        if (metasTBCtrls.fieldCtrlID == (int)ControlType.TextBox)
                        {
                            int idtbl = new Random().Next(0, 9999);
                            GenerateTextBox(ref autorScript, ref celPanel, obj, idtbl, true, value, isAdd, "tblmeta_", metasTBCtrls.fieldMetaID);
                        }
                        else if (metasTBCtrls.fieldCtrlID == (int)ControlType.DropDownList)
                        {
                            int idtbl = new Random().Next(0, 9999);
                            GenerateDropDownList(ref autorScript, ref celPanel, obj, idtbl, true, value, "tblmeta_", metasTBCtrls.fieldMetaID);
                        }
                        else if (metasTBCtrls.fieldCtrlID == (int)ControlType.CheckBoxList)
                        {
                            int idtbl = new Random().Next(0, 9999);
                            GenerateCheckBox(ref autorScript, ref celPanel, obj, idtbl, true, value, "tblmeta_", metasTBCtrls.fieldMetaID);
                        }
                        else if (metasTBCtrls.fieldCtrlID == (int)ControlType.RadioButtonList)
                        {
                            int idtbl = new Random().Next(0, 9999);
                            GenerateRadioButton(ref autorScript, ref celPanel, obj, idtbl, true, value, "tblmeta_", metasTBCtrls.fieldMetaID);
                        }
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