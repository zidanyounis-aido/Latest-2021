using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

namespace dms.Screen
{
    public partial class advancedSearch : System.Web.UI.Page
    {
         DMS.DAL.operations op = new DMS.DAL.operations();
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                grdDocuments.Columns[0].HeaderText = "الرقم";
                grdDocuments.Columns[1].HeaderText = "عنوان المستند";
                grdDocuments.Columns[2].HeaderText = "المجلد";
                grdDocuments.Columns[3].HeaderText = "تاريخ الإضافة";
                grdDocuments.Columns[4].HeaderText = "أضيف بواسطة";
                grdDocuments.Columns[5].HeaderText = "آخر تعديل";
                


            }
        }
                protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            if (!IsPostBack)
            {
                fillStstusDrop();
                tables.dbo.docTypes docTypes = new tables.dbo.docTypes();
                op = new DMS.DAL.operations();
                docTypes = op.dboGetAllDocTypes();
                if (Session["lang"].ToString() == "0")
                    c.FillDropDownList(drpDocTypID, docTypes.table);
                else
                    c.FillDropDownList(drpDocTypID, docTypes.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "DocTypID", "DocTypDescAr");

                tables.dbo.folders folders = new tables.dbo.folders();
                op = new DMS.DAL.operations();
                //folders = op.dboGetAllFolders();
                System.Data.DataTable DT=new System.Data.DataTable();

                bool isDiwan = !(string.IsNullOrEmpty(Request.QueryString["isDiwan"]));
                DT = c.getUserFolders(Convert.ToInt32(Session["userID"]), isDiwan);
                
                if (Session["lang"].ToString() == "0")
                    c.FillDropDownList(drpFldrID, DT,CommonFunction.clsCommon.Typesech.byColomenName,CommonFunction.clsCommon.IsFilter.False,"","","fldrID","fldrName");
                else
                    c.FillDropDownList(drpFldrID, DT, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "fldrID", "fldrNameAr");

                

                if (!String.IsNullOrEmpty( Request.QueryString["fldrID"]))
                {
                    string fldrIDStr = Request.QueryString["fldrID"];
                    fldrIDStr = c.decrypt(fldrIDStr);
                    drpFldrID.SelectedValue = fldrIDStr;

                    tables.dbo.folders fldr = new tables.dbo.folders();
                    op = new DMS.DAL.operations();
                    fldr = op.dboGetFoldersByPrimaryKey(c.convertToInt32(fldrIDStr));
                    if (fldr.fieldDefaultDocTypID > 0)
                    { 
                        drpDocTypID.SelectedValue = fldr.fieldDefaultDocTypID.ToString();
                        showDocTypeMetas();
                    } 


                }
            }   
        }
        public void fillStstusDrop()
        {
            if (Session["lang"].ToString() != "0")
            {
                dropStatus.Items[0].Text = "الكل";
                dropStatus.Items[1].Text = "قيد الإجراء";
                dropStatus.Items[2].Text = "مؤرشف";
                dropStatus.Items[3].Text = "متأخر";
            }
            //ddlStatusFilter.Insert(0, new ListItem("", "Select a department..."))
        }
        void Page_PreInit(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["isDiwan"]))
            {
                this.MasterPageFile = "~/Masters/DocumentsMasterFullPage.master";
            }
            else
            {
                this.MasterPageFile = "~/Masters/DiwanMaster.master";
            }
        }

        protected void drpDocTypID_SelectedIndexChanged(object sender, EventArgs e)
        {
            showDocTypeMetas();
        }

        public void showDocTypeMetas()
        {
            //pnlDocDetails.Visible = true;
            op = new DMS.DAL.operations();
            tables.dbo.metas metasTB = new tables.dbo.metas();
            metasTB = op.dboGetAllMetas("DocTypID = " + drpDocTypID.SelectedValue + " and visible=1 order by orderSeq,metaID");
            if (metasTB.hasRows)
            {
                Panel TR = new Panel();
                TR.CssClass = "col-xs-4 custom-xs";
                Int32 rows = 0;
                for (Int32 i = 0; i < metasTB.rowsCount; i++)
                {
                    metasTB.currentIndex = i;
                    string sql = "select [allowRead] from [dbo].[metaUsersPermissions] where [metaID] =  " + metasTB.fieldMetaID + "  and [userID] =" + int.Parse(Session["userID"].ToString());
                    string sql2 = "select [allowEdit] from [dbo].[metaUsersPermissions] where [metaID] =  " + metasTB.fieldMetaID + "  and [userID] =" + int.Parse(Session["userID"].ToString());
                    bool isRead = Boolean.Parse(c.GetDataAsScalar(sql).ToString() != "" ? c.GetDataAsScalar(sql).ToString() : "False");
                    bool isEdit = Boolean.Parse(c.GetDataAsScalar(sql2).ToString() != "" ? c.GetDataAsScalar(sql2).ToString() : "False");
                    if (isRead || metasTB.fieldPermissionType == "Inherit")
                    {
                        //if (rows != metasTB.fieldOrderSeq)
                        //{
                            TR = new Panel();
                            TR.CssClass = "col-xs-4 custom-xs";
                        //}
                        rows = metasTB.fieldOrderSeq;

                        if (metasTB.fieldCtrlID == 5)
                        {
                            Panel TD = new Panel();
                            if (Session["lang"].ToString() == "0")
                                TD.CssClass = "cellTDEn";
                            else
                                TD.CssClass = "cellTDAr";

                            //TD.ColumnSpan = 4;
                            //lbl.Font.Bold = true;
                            Label lbl = new Label();
                            lbl.Font.Bold = true;

                            if (Session["lang"].ToString() == "0")
                                lbl.Text = metasTB.fieldMetaDesc;
                            else
                                lbl.Text = metasTB.fieldMetaDescAr;
                            TD.Controls.Add(lbl);
                            TR.Controls.Add(TD);
                            TR.CssClass = "rowTitle";
                            pnlDocMetas.Controls.Add(TR);
                        }
                        else
                        {


                            Panel TD = new Panel();
                            if (Session["lang"].ToString() == "0")
                                TD.CssClass = "cellTDEn";
                            else
                                TD.CssClass = "cellTDAr";

                            Label lbl = new Label();
                            if (Session["lang"].ToString() == "0")
                                lbl.Text = metasTB.fieldMetaDesc;
                            else
                                lbl.Text = metasTB.fieldMetaDescAr;
                            //TD.Width = new Unit(20, UnitType.Percentage);
                            TD.Controls.Add(lbl);
                            TR.Controls.Add(TD);

                            TD = new Panel();
                            if (Session["lang"].ToString() == "0")
                                TD.CssClass = "cellTDEn";
                            else
                                TD.CssClass = "cellTDAr";

                            if (Session["lang"].ToString() == "0")
                                lbl.Text = metasTB.fieldMetaDesc;
                            else
                                lbl.Text = metasTB.fieldMetaDescAr;
                            //TD.Width = new Unit(30, UnitType.Percentage);
                            HiddenField hdn = new HiddenField();
                            hdn.ID = "hdn_" + (i + 1).ToString();
                            hdn.Value = metasTB.fieldMetaID.ToString();
                            TD.Controls.Add(hdn);

                            //string value = "";
                            //op = new DMS.DAL.operations();
                            //tables.dbo.documentMataValues docMetas = new tables.dbo.documentMataValues();
                            //docMetas = op.dboGetDocumentMataValuesByPrimaryKey(docID, metasTB.fieldMetaID);
                            //if (docMetas.hasRows)
                            //    value = docMetas.fieldValue;

                            switch (metasTB.fieldCtrlID)
                            {
                                case 1:
                                    TextBox txt = new TextBox();
                                    txt.Attributes.Add("autocomplete", "off");
                                    txt.TabIndex = Convert.ToInt16(6 + i);
                                    txt.ID = "meta_" + (i + 1).ToString();
                                    txt.CssClass = "new-main-input";
                                    //txt.Text = metasTB.fieldDefaultTexts;
                                    //txt.Text = value;
                                    if (metasTB.fieldMetaDataType == "DateTime")
                                    {
                                        txt.CssClass = "dateFeild new-main-input";
                                      
                                    }
                                    if (metasTB.fieldMetaDataType == "Int32")
                                    {
                                        txt.Width = 75;
                                    }
                                    TD.Controls.Add(txt);
                                    break;
                                case 2:
                                    DropDownList drp = new DropDownList();
                                    drp.TabIndex = Convert.ToInt16(6 + i);
                                    drp.ID = "meta_" + (i + 1).ToString();
                                    drp.CssClass = "new-drop";
                                    if (!metasTB.fieldDefaultTexts.ToLower().StartsWith("db:"))
                                    {
                                        char[] chars = { ',', ';', '،' };
                                        if (metasTB.fieldDefaultTexts.IndexOfAny(chars) > -1)
                                        {
                                            string[] texts;
                                            texts = metasTB.fieldDefaultTexts.Split(chars);
                                            string[] values;
                                            values = metasTB.fieldDefaultValues.Split(chars);
                                            for (Int32 j = 0; j < texts.Length; j++)
                                            {
                                                string _value = "";
                                                if (j < values.Length)
                                                    _value = values[j].Trim();
                                                else
                                                    _value = texts[j].Trim();

                                                drp.Items.Add(new ListItem(texts[j].Trim(), _value));
                                            }
                                        }
                                        else
                                        {
                                            drp.Items.Add(new ListItem(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim()));
                                        }
                                    }
                                    else
                                    {
                                        string[] query = metasTB.fieldDefaultTexts.Split(':');
                                        string[] queryVal = metasTB.fieldDefaultValues.Split(':');
                                        string tableName = query[1];
                                        string textFeild = query[2];
                                        string valueFeild = queryVal[2];
                                        string cond = "";
                                        if (query.Length > 3)
                                            cond = " where " + query[3];
                                        string drpSQL = "select " + valueFeild + "," + textFeild + " from " + tableName + cond + " order by " + textFeild;
                                        DataTable drpDT = c.GetDataAsDataTable(drpSQL);
                                        c.FillDropDownList(drp, drpDT);
                                    }
                                    //if (value != "")
                                    //    drp.SelectedValue = value;

                                    TD.Controls.Add(drp);
                                    break;
                                case 3:
                                    CheckBoxList chk = new CheckBoxList();
                                    chk.TabIndex = Convert.ToInt16(6 + i);
                                    chk.ID = "meta_" + (i + 1).ToString();
                                    if (metasTB.fieldDefaultTexts.Contains(','))
                                    {
                                        string[] texts;
                                        texts = metasTB.fieldDefaultTexts.Split(',');
                                        string[] values;
                                        values = metasTB.fieldDefaultValues.Split(',');
                                        for (Int32 j = 0; j < texts.Length; j++)
                                        {
                                            string _value = "";
                                            if (j < values.Length)
                                                _value = values[j].Trim();
                                            else
                                                _value = texts[j].Trim();

                                            chk.Items.Add(new System.Web.UI.WebControls.ListItem(texts[j].Trim(), _value));
                                        }
                                    }
                                    else
                                    {
                                        chk.Items.Add(new ListItem(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim()));
                                    }

                                    //if (value != "")
                                    //    chk.SelectedValue = value;

                                    TD.Controls.Add(chk);
                                    break;
                                case 4:
                                    RadioButtonList rdo = new RadioButtonList();
                                    rdo.TabIndex = Convert.ToInt16(6 + i);
                                    rdo.ID = "meta_" + (i + 1).ToString();
                                    if (metasTB.fieldDefaultTexts.Contains(','))
                                    {
                                        string[] texts;
                                        texts = metasTB.fieldDefaultTexts.Split(',');
                                        string[] values;
                                        values = metasTB.fieldDefaultValues.Split(',');
                                        for (Int32 j = 0; j < texts.Length; j++)
                                        {
                                            string _value = "";
                                            if (j < values.Length)
                                                _value = values[j].Trim();
                                            else
                                                _value = texts[j].Trim();

                                            rdo.Items.Add(new ListItem(texts[j].Trim(), _value));
                                        }
                                    }
                                    else
                                    {
                                        rdo.Items.Add(new ListItem(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim()));
                                    }

                                    //if (value != "")
                                    //    rdo.SelectedValue = value;

                                    TD.Controls.Add(rdo);
                                    break;

                            }
                            TR.Controls.Add(TD);
                        }
                        pnlDocMetas.Controls.Add(TR);
                    }
                   
                }
            }
        }

        //public void showDocTypeMetas()
        //{
        //    pnlDocDetails.Visible = true;
        //    op = new DMS.DAL.operations();
        //    tables.dbo.metas metasTB = new tables.dbo.metas();
        //    metasTB = op.dboGetAllMetas("DocTypID = " + drpDocTypID.SelectedValue + " and visible=1 order by orderSeq,metaID");
        //    System.Text.StringBuilder autoScript = new System.Text.StringBuilder();
        //    autoScript.AppendLine(@"<script type=""text/javascript"">");
        //    autoScript.AppendLine(@"function araneasFillAutos(){");
        //    autoScript.AppendLine(@"try  {");
        //    if (metasTB.hasRows)
        //    {
        //        TableRow TR = new TableRow();
        //        Int32 rows = 0;
        //        for (Int32 i = 0; i < metasTB.rowsCount; i++)
        //        {
        //            metasTB.currentIndex = i;
        //            if (rows != metasTB.fieldOrderSeq)
        //            {
        //                TR = new TableRow();
        //            }
        //            rows = metasTB.fieldOrderSeq;

        //            if (metasTB.fieldCtrlID == 5)
        //            {
        //                TableCell TD = new TableCell();
        //                //TD.ColumnSpan = 4;
        //                TD.Font.Bold = true;
        //                if (Session["lang"].ToString() == "0")
        //                    TD.Text = metasTB.fieldMetaDesc;
        //                else
        //                    TD.Text = metasTB.fieldMetaDescAr;
        //                TR.Cells.Add(TD);

        //                tblDocMetas.Rows.Add(TR);

        //            }
        //            else
        //            {


        //                TableCell TD = new TableCell();
        //                if (Session["lang"].ToString() == "0")
        //                {
        //                    TD.Text = metasTB.fieldMetaDesc;
        //                }
        //                else
        //                {
        //                    TD.Text = metasTB.fieldMetaDescAr;
        //                }
        //                //TD.Width = 200;
        //                TR.Cells.Add(TD);

        //                TD = new TableCell();
        //                HiddenField hdn = new HiddenField();
        //                hdn.ID = "hdn_" + (i + 1).ToString();
        //                hdn.Value = metasTB.fieldMetaID.ToString();
        //                TD.Controls.Add(hdn);
        //                switch (metasTB.fieldCtrlID)
        //                {
        //                    case 1:
        //                        TextBox txt = new TextBox();
        //                        txt.ID = "meta_" + (i + 1).ToString();
        //                        if (metasTB.fieldDefaultTexts.ToLower().StartsWith("#expr:"))
        //                        {
        //                            string textValue = c.fixMetaExp(metasTB.fieldDefaultTexts, i);
        //                            textValue = textValue.Substring(textValue.IndexOf(":") + 1);
        //                            if (!textValue.EndsWith(";"))
        //                                textValue = textValue + ";";

        //                            autoScript.AppendLine(textValue);
        //                        }
        //                        else
        //                            txt.Text = metasTB.fieldDefaultTexts;
        //                        TD.Controls.Add(txt);
        //                        break;
        //                    case 2:
        //                        DropDownList drp = new DropDownList();
        //                        drp.TabIndex = Convert.ToInt16(6 + i);
        //                        drp.ID = "meta_" + (i + 1).ToString();
        //                        if (!metasTB.fieldDefaultTexts.ToLower().StartsWith("db:"))
        //                        {
        //                            char[] chars = { ',', ';', '،' };
        //                            if (metasTB.fieldDefaultTexts.IndexOfAny(chars) > -1)
        //                            {
        //                                string[] texts;
        //                                texts = metasTB.fieldDefaultTexts.Split(chars);
        //                                string[] values;
        //                                values = metasTB.fieldDefaultValues.Split(chars);
        //                                for (Int32 j = 0; j < texts.Length; j++)
        //                                {
        //                                    string _value = "";
        //                                    if (j < values.Length)
        //                                        _value = values[j].Trim();
        //                                    else
        //                                        _value = texts[j].Trim();

        //                                    drp.Items.Add(new ListItem(texts[j].Trim(), _value));
        //                                }
        //                            }
        //                            else
        //                            {
        //                                drp.Items.Add(new ListItem(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim()));
        //                            }
        //                        }
        //                        else
        //                        {
        //                            string tableName = metasTB.fieldDefaultTexts.Split(':')[1];
        //                            string textFeild = metasTB.fieldDefaultTexts.Split(':')[2];
        //                            string valueFeild = metasTB.fieldDefaultValues.Split(':')[2];
        //                            string drpSQL = "select " + valueFeild + "," + textFeild + " from " + tableName + " order by " + textFeild;
        //                            DataTable drpDT = c.GetDataAsDataTable(drpSQL);
        //                            c.FillDropDownList(drp, drpDT);
        //                        }
        //                        TD.Controls.Add(drp);
        //                        break;
        //                    case 3:
        //                        CheckBoxList chk = new CheckBoxList();
        //                        chk.ID = "meta_" + (i + 1).ToString();
        //                        if (metasTB.fieldDefaultTexts.Contains(','))
        //                        {
        //                            string[] texts;
        //                            texts = metasTB.fieldDefaultTexts.Split(',');
        //                            string[] values;
        //                            values = metasTB.fieldDefaultValues.Split(',');
        //                            for (Int32 j = 0; j < texts.Length; j++)
        //                            {
        //                                string value = "";
        //                                if (j < values.Length)
        //                                    value = values[j].Trim();
        //                                else
        //                                    value = texts[j].Trim();

        //                                chk.Items.Add(new ListItem(texts[j].Trim(), value));
        //                            }
        //                        }
        //                        else
        //                        {
        //                            chk.Items.Add(new ListItem(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim()));
        //                        }
        //                        TD.Controls.Add(chk);
        //                        break;
        //                    case 4:
        //                        RadioButtonList rdo = new RadioButtonList();
        //                        rdo.ID = "meta_" + (i + 1).ToString();
        //                        if (metasTB.fieldDefaultTexts.Contains(','))
        //                        {
        //                            string[] texts;
        //                            texts = metasTB.fieldDefaultTexts.Split(',');
        //                            string[] values;
        //                            values = metasTB.fieldDefaultValues.Split(',');
        //                            for (Int32 j = 0; j < texts.Length; j++)
        //                            {
        //                                string value = "";
        //                                if (j < values.Length)
        //                                    value = values[j].Trim();
        //                                else
        //                                    value = texts[j].Trim();

        //                                rdo.Items.Add(new ListItem(texts[j].Trim(), value));
        //                            }
        //                        }
        //                        else
        //                        {
        //                            rdo.Items.Add(new ListItem(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim()));
        //                        }
        //                        TD.Controls.Add(rdo);
        //                        break;

        //                }
        //                TR.Cells.Add(TD);
        //            }
        //            tblDocMetas.Rows.Add(TR);
        //        }
        //    }
        //    autoScript.AppendLine(@"return true;}catch(err)  {  alert(err);return false;  }");
        //    autoScript.AppendLine(@"}");
        //    autoScript.AppendLine(@"</script>");

        //    //ltrScripts.Text = autoScript.ToString();
        //}

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            fillDocuments();
            showDocTypeMetas();
            btnExport.Visible = true;
            //converttoArabic();
        }

        public void fillDocuments()
        {

            string sql = "";
            if (txtDocID.Text.Trim() != "")
                sql += "dbo.documents.docID = " + txtDocID.Text;
            bool flag = true;
            if (txtDocName.Text.Trim() != "")
                sql += " and DocName like N'%" + txtDocName.Text + "%'" ;

            if (drpFldrID.SelectedIndex > 0)
                sql += " and fldrID =" + drpFldrID.SelectedValue;

            if (dropStatus.SelectedIndex > 0)
            {
                sql += " and statusId =" + dropStatus.SelectedValue;
                if (dropStatus.SelectedValue == "1")
                {
                    sql += " or statusId is null";
                }
            }    

            if (drpDocTypID.SelectedIndex > 0)
            {
                sql += " and docTypID =" + drpDocTypID.SelectedValue;

                string sql2 = "select count(metaID) from metas where docTypID=" + drpDocTypID.SelectedValue;
                Int32 metaCount = c.convertToInt16(c.GetDataAsScalar(sql2));

                string values = "";
                try
                {
                    for (Int16 i = 0; i < metaCount; i++)
                    {
                        string metaID = Request.Form["ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolderBody$hdn_" + (i + 1).ToString()];
                        if (!String.IsNullOrEmpty(metaID))
                        {
                            string value = Request.Form["ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolderBody$meta_" + (i + 1).ToString()];

                            if (value != "" && value != Int16.MinValue.ToString())
                            {
                                if (!DMS.Security.isNotAllowedCharacters(value))
                                {
                                    flag = false;
                                }
                                values += "N'" + value + "',";
                            }
                        }
                    }

                    if (values.EndsWith(","))
                        values = values.Remove(values.Length - 1);

                    if (values != "")
                        sql += " and value in (" + values + ")";
                }
                catch { }
            }

            if (sql.StartsWith(" and"))
                sql = sql.Remove(0, 4);

            sql=" where " + sql;
            
            if (!DMS.Security.isNotAllowedCharacters(txtDocID.Text) || !DMS.Security.isNotAllowedCharacters(txtDocName.Text))
            {
                flag = false;
            }
            if (sql.Length > " where ".Length + 2)
            {
                if (flag)
                {
                    System.Data.DataTable DT = new System.Data.DataTable();
                    DT = c.GetDataAsDataTable("SELECT DISTINCT  dbo.documents.docID,dbo.documents.docName,dbo.documents.fldrID,dbo.documents.docTypID,dbo.documents.addedDate,dbo.documents.addedUserID,dbo.documents.modifyDate " +
                        " FROM dbo.documentMataValues right JOIN  dbo.documents ON dbo.documentMataValues.docID = dbo.documents.docID"
                        + sql);
                    grdDocuments.DataSource = DT;
                    grdDocuments.DataBind();
                    try
                    {
                        grdDocuments.HeaderRow.TableSection = TableRowSection.TableHeader;
                        grdDocuments.FooterRow.TableSection = TableRowSection.TableFooter;
                    }
                    catch (Exception)
                    {
                    }
                    grdDocuments.GridLines = GridLines.None;

                }
            }
        }

        public string getFolderName(string fldrID)
        {
            if (fldrID != "")
            {
                string res="";
                if(Session["lang"].ToString() == "0")
                    res = Convert.ToString(c.GetDataAsScalar("select fldrName from folders where fldrID=" + fldrID));
                else
                    res = Convert.ToString(c.GetDataAsScalar("select fldrNameAr from folders where fldrID=" + fldrID));
                return res;
            }
            else
                return "";
        }


        public string getDocTypeDesc(Int32 docTypID)
        {
            string res = "";
            op = new DMS.DAL.operations();
            try
            {
                if (Session["lang"].ToString() == "0")
                    res = op.dboGetDocTypesByPrimaryKey(docTypID).fieldDocTypDesc;
                else
                    res = op.dboGetDocTypesByPrimaryKey(docTypID).fieldDocTypDescAr;
            }
            catch { }
            return res;
        }

        protected void grdDocuments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDocuments.PageIndex = e.NewPageIndex;
            fillDocuments();
        }

        protected void grdDocuments_SelectedIndexChanged(object sender, EventArgs e)
        {
            c = new CommonFunction.clsCommon();
            string docID = grdDocuments.SelectedRow.Cells[0].Text;
            string res = c.encrypt(docID);
            Response.Redirect("../Screen/documentInfo.aspx?docID=" + res);
        }

        protected void drpFldrID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpFldrID.SelectedIndex > 0)
            {
                tables.dbo.folders fldr = new tables.dbo.folders();
                op = new DMS.DAL.operations();
                fldr = op.dboGetFoldersByPrimaryKey(c.convertToInt32(drpFldrID.SelectedValue));
                if (fldr.fieldDefaultDocTypID > 0)
                { 
                    drpDocTypID.SelectedValue = fldr.fieldDefaultDocTypID.ToString();
                    showDocTypeMetas();
                }
            }
            else
            {
            drpDocTypID.SelectedIndex = 0;

            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

            string fileName = "HudHud_" + DateTime.Now.ToString("d-MMM-yyyy");
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment;filename=" + fileName + ".xls");


            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";


            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            grdDocuments.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);

            //Response.Write(getClientName(cohortsTbl.fieldCohortID) + "\t \n");
            //Response.Write(cohortsTbl.fieldCohortNameEn + "\t \n");
            //Response.Write(getTrainerName(cohortsTbl.fieldCohortID) + "\t \n");
            Response.Write("\t \n");
            Response.Output.Write(sw.ToString());
            //loader.Style["display"] = "none";
            Response.Flush();

            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
        

    }
}