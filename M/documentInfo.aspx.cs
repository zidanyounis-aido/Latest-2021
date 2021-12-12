using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using iTextSharp.text.pdf;
using System.Drawing;
using System.IO;
using System.Net;

namespace dms.M
{
    public partial class documentInfo : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        Int32 docTypID; string docName; string docExt; string DocumentFileName; DateTime addedDate;
        Int32 addedUserID; Int16 lastVersion; DateTime modifyDate; Int32 modifyUserID;
        Int32 fldrID; string ocrContent; Int64 docID;
        tables.dbo.documents docTB = new tables.dbo.documents();
        Int64 folderSeq; Int64 docTypeSeq; Int64 folderDocTypeSeq;
        decimal wfTimeFrame; Int16 wfStatus;

        public void fillData(DataTable DT)
        {
            c.fillData(DT, 0, docTB.columnsArray, Page);

            showDocTypeMetas();

            fillVersions();
        }

        public void fillVariables()
        {
            docID = c.convertToInt64(txtDocID.Text);
            docTypID = c.convertToInt32(drpDocTypID.SelectedValue);
            docName = c.convertToString(txtDocName.Text);
            docExt = c.convertToString(hdnDocExt.Value);
            addedDate = c.convertToDateTime(hdnAddedDate.Value);
            addedUserID = c.convertToInt32(hdnAddedUserID.Value);
            lastVersion = c.convertToInt16(hdnLastVersion.Value);
            modifyDate = DateTime.Now;
            modifyUserID = c.convertToInt32(Session["userId"].ToString());
            fldrID = c.convertToInt32(drpFldrID.SelectedValue);
            ocrContent = hdnOcrContent.Value;
            folderSeq = c.convertToInt64(hdnFolderSeq.Value);
            docTypeSeq = c.convertToInt64(hdnDocTypeSeq.Value);
            folderDocTypeSeq = c.convertToInt64(hdnFolderDocTypeSeq.Value);
            wfTimeFrame = c.convertToDecimal(txtWfTimeFrame.Text);
            wfStatus = c.convertToInt16(hdnWfStatus.Value);

            if (drpTFType.SelectedIndex == 0)
            {
                wfTimeFrame = c.convertToDecimal(txtWfTimeFrame.Text);
            }

            if (drpTFType.SelectedIndex == 1)
            {
                wfTimeFrame = c.convertToDecimal(txtWfTimeFrame.Text) * 60;
            }

            if (drpTFType.SelectedIndex == 2)
            {
                wfTimeFrame = c.convertToDecimal(txtWfTimeFrame.Text) * 3600;
            }
        }


        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                lblFolderName.Text = "تعديل الملف";
                Label1.Text = "إضافة مرفق جديد";


                drpTFType.Items[0].Text = "دقائق";
                drpTFType.Items[1].Text = "ساعات";
                drpTFType.Items[2].Text = "أيام";

                drpRecipientType.Items.FindByValue("1").Text = "مستخدم";
                drpRecipientType.Items.FindByValue("2").Text = "مجموعة";
                drpRecipientType.Items.FindByValue("3").Text = "مسمى وظيفي";
                drpRecipientType.Items.FindByValue("4").Text = "وحدة";

                rdoSendType.Items[0].Text = "نسخة إلى";
                rdoSendType.Items[1].Text = "نسخة مخفية";
            }

        }


        public void fillWorkflow()
        {
            string sql = "SELECT    dbo.getUserPosition( dbo.users.userID) as fullName, dbo.documentWFPath.receiveDate, dbo.documentWFPath.actionType, dbo.documentWFPath.actionDateTime,  " +
                        " dbo.documentWFPath.userNotes FROM         dbo.users INNER JOIN       dbo.documentWFPath ON dbo.users.userID = dbo.documentWFPath.userID " +
                        " WHERE     (dbo.documentWFPath.docID = " + docID.ToString() + ")";
            DataTable DT = new DataTable();
            DT = c.GetDataAsDataTable(sql);
            rptWorkflow.DataSource = DT;
            rptWorkflow.DataBind();
        }

        Int32 wfCount = 0;
        public string getCounter()
        {
            wfCount += 1;
            return wfCount.ToString();
        }

        public string getWFAction(Int32 actionID)
        {
            if (Session["lang"].ToString() == "1")
            {
                return c.ActionsAr[actionID];
            }
            else
            {
                return c.ActionsEn[actionID];
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();

            op = new DMS.DAL.operations();
            tables.dbo.users user = new tables.dbo.users();
            user = op.dboGetUsersByPrimaryKey(c.convertToInt32(Session["userID"]));
            if (!user.fieldAllowDiwan)
            {
                drpTFType.Enabled = false;
                txtWfTimeFrame.ReadOnly = true;
            }

            c = new CommonFunction.clsCommon();
            if (Request.QueryString["docID"] == "")
                Response.Redirect("../Scressn/", true);

            string dc = Request.QueryString["docID"];
            dc = c.decrypt(dc);
            docID = c.convertToInt64(dc);

            if (!IsPostBack)
            {
                fillDrpRecipientID();
                fillWorkflow();

                tables.dbo.docTypes docTypes = new tables.dbo.docTypes();
                op = new DMS.DAL.operations();
                docTypes = op.dboGetAllDocTypes();
                if (Session["lang"].ToString() == "0")
                    c.FillDropDownList(drpDocTypID, docTypes.table);
                else
                    c.FillDropDownList(drpDocTypID, docTypes.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "docTypID", "docTypDescAr");



                tables.dbo.folders folders = new tables.dbo.folders();
                op = new DMS.DAL.operations();
                folders = op.dboGetAllFolders();
                if (Session["lang"].ToString() == "0")
                    c.FillDropDownList(drpFldrID, folders.table);
                else
                    c.FillDropDownList(drpFldrID, folders.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "fldrID", "fldrNameAr");


                if (Request.QueryString["fldrID"] != null && Request.QueryString["fldrID"] != "")
                    drpFldrID.SelectedValue = c.decrypt(Request.QueryString["fldrID"]);

                op = new DMS.DAL.operations();
                docTB = op.dboGetDocumentsByPrimaryKey(docID);

                op = new DMS.DAL.operations();
                tables.dbo.documentsGroups docGroups = new tables.dbo.documentsGroups();
                docGroups = op.dboGetAllDocumentsGroups("docGroupID=" + docTB.fieldDocTypID);
                c.FillDropDownList(drpDocGroupID, docGroups.table);

                showDocTypeMetas();

                showData();

            }

            try
            {
                hdnUserCode.Value = c.convertToString(Session["userID"]);
                hdnURL.Value = Request.UrlReferrer.AbsoluteUri;
                hdnURL.Value = hdnURL.Value.Remove(hdnURL.Value.IndexOf(@"/", "https://".Length + 1));
                if (!hdnURL.Value.EndsWith("/"))
                    hdnURL.Value = hdnURL.Value + "/";
            }
            catch { }
        }

        private void showData()
        {

            op = new DMS.DAL.operations();
            docTB = op.dboGetDocumentsByPrimaryKey(docID);
            fillData(docTB.table);

            txtWfTimeFrame.Text = (docTB.fieldWfTimeFrame / 3600).ToString("0.00");

            tables.dbo.userFolders userFldr = new tables.dbo.userFolders();
            op = new DMS.DAL.operations();
            userFldr = op.dboGetUserFoldersByPrimaryKey(docTB.fieldFldrID, c.convertToInt32(Session["userID"]));
            if (userFldr.hasRows)
            {
                if (!userFldr.fieldAllow)
                    Response.Redirect("../M/DefaultAr.aspx");

                if (!userFldr.fieldAllowDelete)
                    LinkButton2.Visible = false;

                if (!userFldr.fieldAllowUpdate)
                {
                    LinkButton1.Visible = false;
                    LinkButton3.Visible = false;
                }

                txtDocID.Focus();
            }
            else
            {
                Response.Redirect("../M/notAllowed.html");
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
                TableRow TR = new TableRow();
                for (Int32 i = 0; i < metasTB.rowsCount; i++)
                {
                    metasTB.currentIndex = i;
                    TR = new TableRow();

                    TableCell TD = new TableCell();
                    if (Session["lang"].ToString() == "0")
                        TD.Text = metasTB.fieldMetaDesc;
                    else
                        TD.Text = metasTB.fieldMetaDescAr;
                    TD.Width = new Unit(20, UnitType.Percentage);
                    TR.Cells.Add(TD);

                    TD = new TableCell();
                    TD.Width = new Unit(30, UnitType.Percentage);
                    HiddenField hdn = new HiddenField();
                    hdn.ID = "hdn_" + (i + 1).ToString();
                    hdn.Value = metasTB.fieldMetaID.ToString();
                    TD.Controls.Add(hdn);

                    string value = "";
                    op = new DMS.DAL.operations();
                    tables.dbo.documentMataValues docMetas = new tables.dbo.documentMataValues();
                    docMetas = op.dboGetDocumentMataValuesByPrimaryKey(docID, metasTB.fieldMetaID);
                    if (docMetas.hasRows)
                        value = docMetas.fieldValue;

                    switch (metasTB.fieldCtrlID)
                    {
                        case 1:
                            TextBox txt = new TextBox();
                            txt.TabIndex = Convert.ToInt16(6 + i);
                            txt.ID = "meta_" + (i + 1).ToString();
                            txt.Text = metasTB.fieldDefaultTexts;
                            txt.Text = value;
                            if (metasTB.fieldMetaDataType == "DateTime")
                            {
                                txt.CssClass = "dateFeild";
                            }
                            TD.Controls.Add(txt);
                            break;
                        case 2:
                            DropDownList drp = new DropDownList();
                            drp.TabIndex = Convert.ToInt16(6 + i);
                            drp.ID = "meta_" + (i + 1).ToString();
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

                                    drp.Items.Add(new ListItem(texts[j].Trim(), _value));
                                }
                            }
                            else
                            {
                                drp.Items.Add(new ListItem(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim()));
                            }
                            if (value != "")
                                drp.SelectedValue = value;

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

                            if (value != "")
                                chk.SelectedValue = value;

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

                            if (value != "")
                                rdo.SelectedValue = value;

                            TD.Controls.Add(rdo);
                            break;
                    }
                    TR.Cells.Add(TD);
                    tblDocMetas.Rows.Add(TR);
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            fillVariables();
            DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
            sp.updateDocumentsWithOutMeta(docID, docTypID, docName,
                docExt, addedDate, addedUserID, lastVersion, modifyDate,
                modifyUserID, fldrID, ocrContent, folderSeq, docTypeSeq, folderDocTypeSeq
                , Int16.MinValue, Int16.MinValue, Int16.MinValue, Int16.MinValue, DateTime.MaxValue.AddDays(-1), wfTimeFrame, wfStatus
                );

            string sql = "select count(metaID) from metas where docTypID=" + drpDocTypID.SelectedValue;
            Int32 metaCount = c.convertToInt16(c.GetDataAsScalar(sql));

            bool isExsit = false;
            if (txtDocID.Text != "")
                isExsit = true;
            string updateSQL = "";
            bool flag = true;
            for (Int16 i = 0; i < metaCount; i++)
            {
                string metaID = Request.Form["ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolderBody$hdn_" + (i + 1).ToString()];
                string value = Request.Form["ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolderBody$meta_" + (i + 1).ToString()];

                if (!DMS.Security.isNotAllowedCharacters(value))
                {
                    flag = false;
                }
                op = new DMS.DAL.operations();
                tables.dbo.documentMataValues docMetas = new tables.dbo.documentMataValues();
                if (isExsit)
                {
                    docMetas = op.dboGetDocumentMataValuesByPrimaryKey(docID, c.convertToInt32(metaID));
                    if (docMetas.hasRows)
                    {
                        op = new DMS.DAL.operations();
                        op.dboUpdateDocumentMataValuesByPrimaryKey(c.convertToInt32(metaID), docID, value);
                    }
                }
                if (!isExsit || !docMetas.hasRows)
                {
                    op = new DMS.DAL.operations();
                    op.dboAddDocumentMataValues(c.convertToInt32(metaID), docID, value);
                }

                updateSQL += "Meta" + (i + 1).ToString() + " = N'" + value + "',";
            }
            if (updateSQL.Trim() != "")
            {
                if (flag)
                {
                    updateSQL = updateSQL.Remove(updateSQL.Length - 1);
                    updateSQL = "Update Documents set " + updateSQL + " where docID=" + txtDocID.Text;

                    c.NonQuery(updateSQL);
                }
            }

            showData();

            if (Session["lang"].ToString() == "0")
                lblRes.Text = "Document changes have been saved";
            else
                lblRes.Text = "تم حفظ التغييرات";
        }
        //lnkPDF_Click
        protected void lnkPDF_Click(object sender, EventArgs e)
        {
            #region Create PDF
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER);
            try
            {

                // step 2:
                // we create a writer that listens to the document
                // and directs a PDF-stream to a file
                string fname = dms.Helper.GetUploadDiskPath($"pdf/{txtDocName.Text.Replace(" ", "_")}.pdf");
                //PdfWriter.GetInstance(document, new FileStream(Server.MapPath( fname), FileMode.Create));
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                PdfWriter.GetInstance(document, memoryStream);
                // step 3: we open the document
                document.Open();

                //System.Drawing.Image img
                List<System.Drawing.Image> images = new List<System.Drawing.Image>();

                tables.dbo.documentVersions vers = new tables.dbo.documentVersions();
                op = new DMS.DAL.operations();
                vers = op.dboGetAllDocumentVersions("DocID=" + docID.ToString());

                for (Int32 i = 0; i < vers.rowsCount; i++)
                {
                    vers.currentIndex = i;
                    string filepah = dms.Helper.GetUploadDiskPath() + docID.ToString() + "-" + vers.fieldVersion.ToString() + "." + vers.fieldExt;
                    DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
                    if (c.getFileType(vers.fieldExt) == CommonFunction.clsCommon.fileType.Image)
                    {
                        images.Add(System.Drawing.Image.FromFile(Server.MapPath(filepah)));
                    }
                }

                foreach (var image in images)
                {
                    iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);

                    if (pic.Height > pic.Width)
                    {
                        //Maximum height is 800 pixels.
                        float percentage = 0.0f;
                        percentage = 700 / pic.Height;
                        pic.ScalePercent(percentage * 100);
                    }
                    else
                    {
                        //Maximum width is 600 pixels.
                        float percentage = 0.0f;
                        percentage = 540 / pic.Width;
                        pic.ScalePercent(percentage * 100);
                    }

                    pic.Border = iTextSharp.text.Rectangle.BOX;
                    pic.BorderColor = iTextSharp.text.BaseColor.BLACK;
                    pic.BorderWidth = 3f;
                    document.Add(pic);
                    document.NewPage();
                }
                document.Close();
                #region Download


                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + txtDocName.Text.Replace(" ", "_") + ".pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
                #endregion

            }
            catch (iTextSharp.text.DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }

            // step 5: we close the document
            document.Close();
            #endregion



        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (fluFile.HasFile)
            {
                Int16 res;
                res = c.convertToInt16(c.GetMax("version", "dbo.documentVersions", "docID=" + docID.ToString()));
                op = new DMS.DAL.operations();
                docExt = fluFile.FileName;
                docExt = docExt.Substring(docExt.LastIndexOf(".") + 1);
                DocumentFileName = fluFile.FileName.Substring(0, fluFile.FileName.LastIndexOf("."));
                op.dboAddDocumentVersions(docID, res, DateTime.Now, c.convertToInt32(Session["userID"]), docExt, c.convertToInt32(drpDocGroupID.SelectedValue), DocumentFileName, DocumentFileName);

                string desPath = dms.Helper.GetUploadDiskPath();
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(desPath);
                if (!dir.Exists)
                    dir.Create();

                fluFile.SaveAs(desPath + @"\" + docID + "-" + res.ToString() + "." + docExt);

                string sql = "update dbo.documents set lastVersion=" + res.ToString() + " where docID=" + docID.ToString();
                c.NonQuery(sql);

                fillVersions();
            }
            else
            {
                if (Session["lang"].ToString() == "0")
                    lblRes.Text = "Please choose a file";
                else
                    lblRes.Text = "الرجاء اختيار ملف";
            }

            showDocTypeMetas();
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            fillVariables();
            op = new DMS.DAL.operations();
            Int16 res;
            docExt = "tif";
            docExt = docExt.Substring(docExt.LastIndexOf(".") + 1);
            docName = "Scanned Document";
            docName = docName.Substring(docName.LastIndexOf(@"\") + 1);
            docName = docName.Split('.')[0];
            DMS.BLL.specialCases SP = new DMS.BLL.specialCases();

            res = c.convertToInt16(c.GetMax("version", "dbo.documentVersions", "docID=" + docID.ToString()));
            op = new DMS.DAL.operations();
            op.dboAddDocumentVersions(docID, res, DateTime.Now, c.convertToInt32(Session["userID"]), docExt, c.convertToInt32(drpDocGroupID.SelectedValue),docName, docName);


            string desPath = dms.Helper.GetUploadDiskPath();
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(desPath);

            if (dir.Exists == false)
                dir.Create();

            string fName = desPath + @"\" + docID + "-" + res.ToString() + ".tif";
            //fluFile.SaveAs(fName);
            if (System.IO.File.GetLastWriteTime(Server.MapPath("../") + Session["userID"].ToString() + ".tif") >= DateTime.Now.AddMinutes(-1))
            {
                System.IO.File.Copy(Server.MapPath("../") + Session["userID"].ToString() + ".tif", fName);
            }
            //string desPath = Server.MapPath(@"../Uploads/");
            //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(desPath);
            //if (!dir.Exists)
            //    dir.Create();

            //fluFile.SaveAs(desPath + @"\" + docID + "-" + res.ToString() + "." + docExt);

            string sql = "update dbo.documents set lastVersion=" + res.ToString() + " where docID=" + docID.ToString();
            c.NonQuery(sql);

            fillVersions();


        }

        private void fillVersions()
        {
            tables.dbo.documentVersions vers = new tables.dbo.documentVersions();
            op = new DMS.DAL.operations();
            vers = op.dboGetAllDocumentVersions("DocID=" + docID.ToString());

            if (Session["lang"].ToString() == "1")
            {
                lblDocCount.Text = vers.rowsCount.ToString() + " مرفق متوفر";
            }
            else
            {
                lblDocCount.Text = vers.rowsCount.ToString() + " attachment(s) included";
            }
            for (Int32 i = 0; i < vers.rowsCount; i++)
            {
                vers.currentIndex = i;
                TableRow TR = new TableRow();
                TableCell TD = new TableCell();
                TD.Text = "<img src='../Images/Icons/doc-Icon.png' />";
                TR.Cells.Add(TD);
                TD = new TableCell();
                TD.Text = drpDocGroupID.Items.FindByValue(vers.fieldDocGroupID.ToString()).Text;
                TR.Cells.Add(TD);
                TD = new TableCell();
                HyperLink lnk = new HyperLink();
                lnk.NavigateUrl = "../M/showDocument.aspx?docID=" + c.encrypt(docID.ToString()) + "&ver=" + vers.fieldVersion.ToString() + @"&";
                lnk.Target = "_parent";
                //lnk.NavigateUrl = "../M/showDocument.aspx?docID=" + c.encrypt(docID.ToString());
                c = new CommonFunction.clsCommon();
                //lnk.NavigateUrl += "&ver=" + vers.fieldVersion.ToString();
                lnk.Text = txtDocName.Text + "-" + vers.fieldVersion.ToString() + "." + vers.fieldExt;
                TD.Controls.Add(lnk);
                TR.Cells.Add(TD);
                tblVersions.Rows.Add(TR);
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            op = new DMS.DAL.operations();
            op.dboDeleteUserDocuments("docID=" + txtDocID.Text);

            op = new DMS.DAL.operations();
            op.dboDeleteDocumentMataValues("docID=" + txtDocID.Text);

            op = new DMS.DAL.operations();
            op.dboDeleteDocumentVersions("docID=" + txtDocID.Text);

            op = new DMS.DAL.operations();
            op.dboDeleteDocumentsByPrimaryKey(Convert.ToInt32(txtDocID.Text));

            Response.Redirect("../M/documentsList.aspx?fldrID=" + c.encrypt(drpFldrID.SelectedValue));
        }

        private void fillDrpRecipientID()
        {
            op = new DMS.DAL.operations();
            DataTable dt = new DataTable();
            string valueF = "";
            string textF = "";
            switch (drpRecipientType.SelectedValue)
            {
                case "1":
                    tables.dbo.users usersTB = new tables.dbo.users();
                    usersTB = op.dboGetAllUsers();
                    dt = usersTB.table;
                    valueF = "userID";
                    textF = "FullName";
                    break;
                case "2":
                    tables.dbo.groups grpTB = new tables.dbo.groups();
                    grpTB = op.dboGetAllGroups();
                    dt = grpTB.table;
                    valueF = "grpID";
                    textF = "grpDesc";
                    break;
                case "3":
                    tables.dbo.positions positionsTB = new tables.dbo.positions();
                    positionsTB = op.dboGetAllPositions();
                    dt = positionsTB.table;
                    valueF = "positionID";
                    if (Session["lang"].ToString() == "0")
                        textF = "positionTitle";
                    else
                        textF = "positionTitleAr";
                    break;
                case "4":
                    tables.dbo.departments departmentsTB = new tables.dbo.departments();
                    departmentsTB = op.dboGetAllDepartments();
                    dt = departmentsTB.table;
                    valueF = "departmentID";
                    if (Session["lang"].ToString() == "0")
                        textF = "departmentName";
                    else
                        textF = "departmentNameAr";
                    break;
            }

            c.FillDropDownList(drpRecipientID, dt, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", valueF, textF);
        }

        protected void drpRecipientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDrpRecipientID();
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            op = new DMS.DAL.operations();
            tables.dbo.settings settings = op.dboGetSettingsByPrimaryKey(1);

            op = new DMS.DAL.operations();
            tables.dbo.users users = new tables.dbo.users();

            switch (drpRecipientType.SelectedValue)
            {
                case "1":
                    users = op.dboGetUsersByPrimaryKey(c.convertToInt32(drpRecipientID.SelectedValue));
                    break;
                case "2":
                    users = op.dboGetAllUsers("grpID=" + drpRecipientID.SelectedValue);
                    break;
                case "3":
                    users = op.dboGetAllUsers("positionID=" + drpRecipientID.SelectedValue);
                    break;
                case "4":
                    users = op.dboGetAllUsers("departmentID=" + drpRecipientID.SelectedValue);
                    break;
            }

            try
            {
                if (!users.hasRows)
                {
                    if (Session["lang"].ToString() == "0")
                        errorMessage = "Please select a recipient";
                    else
                        errorMessage = "الرجاء اختيار مستلم";
                }
                System.Net.Mail.SmtpClient M = new System.Net.Mail.SmtpClient(settings.fieldOutgoingMailServer);
                System.Net.Mail.MailMessage MailMsg;
                if (drpRecipientType.SelectedValue == "1")
                {
                    if (users.fieldEmail == "")
                    {
                        if (Session["lang"].ToString() == "0")
                            errorMessage = "Selected user doesn't have an Email";
                        else
                            errorMessage = "المستخدم المختار ليس لديه بريد إلكتروني";
                    }
                    MailMsg = new System.Net.Mail.MailMessage(settings.fieldSystemEmail, users.fieldEmail);
                }
                else
                {
                    Int32 rec = 0;
                    MailMsg = new System.Net.Mail.MailMessage(settings.fieldSystemEmail, settings.fieldSystemEmail);

                    for (rec = 0; rec < users.rowsCount; rec++)
                    {
                        users.currentIndex = rec;
                        if (users.fieldEmail != "")
                        {
                            //DateTime? EndDocumntDate = null;
                            //if (txtenddateCount.Value != "")
                            //{
                            //    EndDocumntDate = DateTime.Now;
                            //    EndDocumntDate.Value.AddDays(int.Parse(txtenddateCount.Value));
                            //}
                            op = new DMS.DAL.operations();
                            op.dboAddUserDocuments(users.fieldUserID, docID, true, true, true, true);

                            if (rdoSendType.SelectedValue == "cc")
                                MailMsg.CC.Add(users.fieldEmail);
                            else
                                MailMsg.Bcc.Add(users.fieldEmail);
                        }
                    }
                }

                if (errorMessage == "")
                {

                    M.Credentials = new System.Net.NetworkCredential(settings.fieldSystemEmail, c.decrypt(settings.fieldSystemEmailPassword));
                    string siteURL = Request.Url.AbsoluteUri;
                    siteURL = siteURL.Remove(siteURL.LastIndexOf('/'));
                    siteURL = siteURL.Remove(siteURL.LastIndexOf('/'));
                    string docLink = siteURL + "/Screen/showDocument.aspx?docID=" + c.encrypt(docID.ToString()) + "&ver=1" + @"&'";
                    MailMsg.BodyEncoding = System.Text.UTF8Encoding.UTF8;
                    MailMsg.Subject = Session["FullName"].ToString() + " sends you a document";
                    MailMsg.Body = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'><html><body dir='rtl' style='font-family:Arial'>";
                    MailMsg.Body += "<div style='vertical-align:middle; background-color:none; padding:10px;font-size:12pt;'>";
                    MailMsg.Body += txtMailBody.Text;
                    MailMsg.Body += "<br/><br/>Document Link : <a href='" + docLink + "'>" + docLink + "</a>";
                    MailMsg.Body += "<br/><br/><hr/><br/>" + settings.fieldSystemEmailSignature;
                    MailMsg.Body += "</body></html>";
                    MailMsg.IsBodyHtml = true;
                    M.Send(MailMsg);
                    //Response.Write(URL)
                    //formtable.Visible = False
                    //complate.Visible = True
                    if (Session["lang"].ToString() == "0")
                        msglbl.Text = "Your message has been sent ...";
                    else
                        msglbl.Text = "تم ارسال رسالتك بنجاح ...";
                }
                else
                {
                    msglbl.Text = errorMessage;
                }

                //SendBtn.Enabled = True
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message)
                //formtable.Visible = False
                //complate.Visible = True
                msglbl.Text = ex.Message;
                if (Session["lang"].ToString() == "0")
                    msglbl.Text = "Error while sending ... <br/> Please try again after a while  ";
                else
                    msglbl.Text = " خطأ أثناء الإرسال ... <br/> يرجى المحاولة بعد قليل  ";
            }
        }
    }
}