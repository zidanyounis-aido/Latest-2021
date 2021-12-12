using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Leadtools.Forms.Ocr;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data;

namespace dms.M
{
    public partial class newDocument : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();

        Int32 docTypID; string docName; string docExt; DateTime addedDate;
        Int32 addedUserID; Int16 lastVersion; DateTime modifyDate; Int32 modifyUserID;
        Int32 fldrID; string ocrContent; Int64 docID;
        Int64 folderSeq; Int64 docTypeSeq; Int64 folderDocTypeSeq;
        decimal wfTimeFrame; Int16 wfStatus;
        DateTime wfStartDateTime;

        tables.dbo.documents docTB = new tables.dbo.documents();

        public void fillData(DataTable DT)
        {
            c.fillData(DT, 0, docTB.columnsArray, Page);

            showDocTypeMetas();
        }

        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        public void fillVariables()
        {
            
            docID = c.convertToInt32(txtDocID.Text);
            docTypID = c.convertToInt32(drpDocTypID.SelectedValue);
            docName = c.convertToString(txtDocName.Text);
            //docExt = c.convertToString(txtDocExt.Text);
            addedDate = DateTime.Now;
            addedUserID = c.convertToInt32(Session["userId"].ToString());
            lastVersion = c.convertToInt16(1);
            modifyDate = DateTime.Now;
            modifyUserID = c.convertToInt32(Session["userId"].ToString());
            fldrID = c.convertToInt32(drpFldrID.SelectedValue);
            ocrContent = "";
            folderSeq = c.convertToInt64(hdnFolderSeq.Value);
            docTypeSeq = c.convertToInt64(hdnDocTypeSeq.Value);
            folderDocTypeSeq = c.convertToInt64(hdnFolderDocTypeSeq.Value);

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
                lblResultFinal.Text = "تم تحميل الملف بنجاح \n  سوف يصبح بامكانك تغيير مكان الملف في صفحة  الملفات";
                lblFolderName.Text = "ملف جديد";
                chkArchiveOnly.Text = "أرشفة فقط";
                lnkCheck.Text = "تأكد من المستند";

                drpTFType.Items[0].Text = "دقائق";
                drpTFType.Items[1].Text = "ساعات";
                drpTFType.Items[2].Text = "أيام";
            }

        }

        void fillNextUser()
        {
            Int32 folderID = c.convertToInt32(drpFldrID.SelectedValue);
            op = new DMS.DAL.operations();
            tables.dbo.folders fldr = op.dboGetFoldersByPrimaryKey(folderID);
            if (fldr.hasRows)
            {
                op = new DMS.DAL.operations();
                tables.dbo.docTypes dType = op.dboGetDocTypesByPrimaryKey(fldr.fieldDefaultDocTypID);
                if (dType.hasRows)
                {
                    if(dType.fieldDefaultWFID >0)
                    {
                        op = new DMS.DAL.operations();
                        tables.dbo.wfPathDetails WF = op.dboGetAllWfPathDetails("PathID = " + dType.fieldDefaultWFID.ToString(), "SeqNo");
                        op = new DMS.DAL.operations();
                        if(WF.fieldRecipientType == 1){
                            tables.dbo.users nextUser = op.dboGetUsersByPrimaryKey(WF.fieldRecipientID);
                            if (Session["lang"].ToString() == "1")
                                drpNextUser.Items[0].Text = "الإفتراضي : ";
                            else
                                drpNextUser.Items[0].Text = "Default : ";

                            drpNextUser.Items[0].Text += nextUser.fieldFullName;

                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            if (!IsPostBack)
            {
                op = new DMS.DAL.operations();
                tables.dbo.users user = new tables.dbo.users();
                user = op.dboGetUsersByPrimaryKey(c.convertToInt32(Session["userID"]));
                if(!user.fieldAllowDiwan)
                {
                    drpTFType.Enabled = false;
                    txtWfTimeFrame.ReadOnly = true;
                }

                //if (!user.fieldAllowCustomWF)
                //{
                //    customWF.Visible = false;
                //}

                tables.dbo.docTypes docTypes = new tables.dbo.docTypes();
                op = new DMS.DAL.operations();
                docTypes = op.dboGetAllDocTypes();
                c.FillDropDownList(drpDocTypID, docTypes.table);

                tables.dbo.folders folders = new tables.dbo.folders();
                op = new DMS.DAL.operations();
                folders = op.dboGetAllFolders();
                c.FillDropDownList(drpFldrID, folders.table);

                DMS.BLL.specialCases bll = new DMS.BLL.specialCases();
                DataTable wfUsers = new DataTable();
                wfUsers = bll.getWorkflowUsers();
                c.FillDropDownList(drpNextUser, wfUsers);

                if (Request.QueryString["fldrID"] != "")
                {
                    fldrID = c.convertToInt32(c.decrypt(Request.QueryString["fldrID"]));
                    drpFldrID.SelectedValue = fldrID.ToString();
                    tables.dbo.folders foldersTB = new tables.dbo.folders();
                    op = new DMS.DAL.operations();
                    foldersTB = op.dboGetFoldersByPrimaryKey(fldrID);
                    if (foldersTB.fieldDefaultDocTypID > 0)
                    { 
                        docTypID = foldersTB.fieldDefaultDocTypID;
                        drpDocTypID.SelectedValue = docTypID.ToString();
                        showDocTypeMetas();
                    }

                    fillNextUser();
                }


                //showDocTypeMetas();
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
        }

        protected void drpDocTypID_SelectedIndexChanged(object sender, EventArgs e)
        {
            showDocTypeMetas();
            fillNextUser();
        }

        public static string WordScrambler(Match match)
        {
            string replace = @"document.getElementById(""ContentPlaceHolder1_ContentPlaceHolderBody_" + match.Value + @""").value";
            return replace;
        }

        public void showDocTypeMetas()
        {
            //pnlDocDetails.Visible = true;
            op = new DMS.DAL.operations();
            tables.dbo.metas metasTB = new tables.dbo.metas();
            metasTB = op.dboGetAllMetas("DocTypID = " + drpDocTypID.SelectedValue + " and visible=1 order by orderSeq,metaID");
            System.Text.StringBuilder autoScript = new System.Text.StringBuilder();
            autoScript.AppendLine(@"<script type=""text/javascript"">");
            autoScript.AppendLine(@"function araneasFillAutos(){");
            autoScript.AppendLine(@"try  {");
            if (metasTB.hasRows)
            {
                for (Int32 i = 0; i < metasTB.rowsCount; i++)
                {
                    metasTB.currentIndex = i;
                    TableRow TR = new TableRow();
                    TableCell TD = new TableCell();
                    if (Session["lang"].ToString() == "0")
                        TD.Text = metasTB.fieldMetaDesc;
                    else
                        TD.Text = metasTB.fieldMetaDescAr;
                    //TD.Width = 200;
                    TR.Cells.Add(TD);

                    TD = new TableCell();
                    HiddenField hdn = new HiddenField();
                    hdn.ID = "hdn_" + (i + 1).ToString();
                    hdn.Value = metasTB.fieldMetaID.ToString();
                    hdn.ClientIDMode = System.Web.UI.ClientIDMode.Static;

                    TD.Controls.Add(hdn);
                    switch (metasTB.fieldCtrlID)
                    {
                        case 1:
                            TextBox txt = new TextBox();
                            txt.TabIndex = Convert.ToInt16(4 + i);
                            
                            txt.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                            txt.ID = "meta_" + (i + 1).ToString();
                            if (metasTB.fieldDefaultTexts.ToLower().StartsWith("#expr:"))
                            {
                                string textValue = c.fixMetaExp(metasTB.fieldDefaultTexts, i);
                                textValue = textValue.Substring(textValue.IndexOf(":") + 1);
                                if (!textValue.EndsWith(";"))
                                    textValue = textValue + ";";

                                autoScript.AppendLine(textValue);
                            }
                            else
                                txt.Text = metasTB.fieldDefaultTexts;

                            if (metasTB.fieldMetaDataType == "DateTime")
                            {
                                txt.CssClass = "dateFeild";
                            }
                            TD.Controls.Add(txt);
                            break;
                        case 2:
                            DropDownList drp = new DropDownList();
                            drp.TabIndex = Convert.ToInt16(4 + i);
                            drp.ID = "meta_" + (i + 1).ToString();
                            drp.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                            if (metasTB.fieldDefaultTexts.Contains(','))
                            {
                                string[] texts;
                                texts = metasTB.fieldDefaultTexts.Split(',');
                                string[] values;
                                values = metasTB.fieldDefaultValues.Split(',');
                                for (Int32 j = 0; j < texts.Length; j++)
                                {
                                    string value = "";
                                    if (j < values.Length)
                                        value = values[j].Trim();
                                    else
                                        value = texts[j].Trim();

                                    drp.Items.Add(new ListItem(texts[j].Trim(), value));
                                }
                            }
                            else
                            {
                                drp.Items.Add(new ListItem(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim()));
                            }
                            TD.Controls.Add(drp);
                            break;
                        case 3:
                            CheckBoxList chk = new CheckBoxList();
                            chk.TabIndex = Convert.ToInt16(4 + i);
                            chk.ID = "meta_" + (i + 1).ToString();
                            chk.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                            if (metasTB.fieldDefaultTexts.Contains(','))
                            {
                                string[] texts;
                                texts = metasTB.fieldDefaultTexts.Split(',');
                                string[] values;
                                values = metasTB.fieldDefaultValues.Split(',');
                                for (Int32 j = 0; j < texts.Length; j++)
                                {
                                    string value = "";
                                    if (j < values.Length)
                                        value = values[j].Trim();
                                    else
                                        value = texts[j].Trim();

                                    chk.Items.Add(new ListItem(texts[j].Trim(), value));
                                }
                            }
                            else
                            {
                                chk.Items.Add(new ListItem(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim()));
                            }
                            TD.Controls.Add(chk);
                            break;
                        case 4:
                            RadioButtonList rdo = new RadioButtonList();
                            rdo.TabIndex = Convert.ToInt16(4 + i);
                            rdo.ID = "meta_" + (i + 1).ToString();
                            rdo.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                            if (metasTB.fieldDefaultTexts.Contains(','))
                            {
                                string[] texts;
                                texts = metasTB.fieldDefaultTexts.Split(',');
                                string[] values;
                                values = metasTB.fieldDefaultValues.Split(',');
                                for (Int32 j = 0; j < texts.Length; j++)
                                {
                                    string value = "";
                                    if (j < values.Length)
                                        value = values[j].Trim();
                                    else
                                        value = texts[j].Trim();

                                    rdo.Items.Add(new ListItem(texts[j].Trim(), value));
                                }
                            }
                            else
                            {
                                rdo.Items.Add(new ListItem(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim()));
                            }
                            TD.Controls.Add(rdo);
                            break;

                    }
                    TR.Cells.Add(TD);
                    tblDocMetas.Rows.Add(TR);
                }
            }
            autoScript.AppendLine(@"return true;}catch(err)  {  alert(err);return false;  }");
            autoScript.AppendLine(@"}");
            autoScript.AppendLine(@"</script>");

            ltrScripts.Text = autoScript.ToString();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (fluFile.HasFile)
            {
                fillVariables();
                op = new DMS.DAL.operations();
                Int32 res;
                docExt = fluFile.FileName;
                docExt = docExt.Substring(docExt.LastIndexOf(".") + 1);
                if (docExt.ToLower() != "exe")
                {
                    docName = fluFile.FileName;
                    docName = docName.Substring(docName.LastIndexOf(@"\") + 1);
                    docName = docName.Split('.')[0];
                    DMS.BLL.specialCases SP = new DMS.BLL.specialCases();

                    res = SP.dboAddDocuments(docTypID, docName, docExt, addedDate, addedUserID, lastVersion, modifyDate, modifyUserID, fldrID, ocrContent, 0, 0, 0);
                    string desPath = Helper.GetUploadDiskPath();
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(desPath);
                    if (!dir.Exists)
                        dir.Create();

                    string fName = desPath + res + "-1." + docExt;
                    fluFile.SaveAs(fName);
                    dms.Controlers.Common.DocToPdfConvert docToPdfConvert = new Controlers.Common.DocToPdfConvert();
                    docToPdfConvert.ConvertToPDF(fluFile.PostedFile, res + "-1.", desPath);

                    if (c.getFileType(docExt) == CommonFunction.clsCommon.fileType.Image)
                    {
                        ocrContent = getOCR(fName);
                    }

                    Int32 docGroupID = Int16.MinValue;
                    op = new DMS.DAL.operations();
                    tables.dbo.documentsGroups docGroup = new tables.dbo.documentsGroups();
                    docGroup = op.dboGetAllDocumentsGroups("docTypeID=" + docTypID.ToString());
                    if (docGroup.rowsCount > 0)
                        docGroupID = docGroup.fieldDocGroupID;
                    

                        op = new DMS.DAL.operations();
                    op.dboAddDocumentVersions(res, 1, DateTime.Now, addedUserID, docExt, docGroupID,docName, docName);
                    txtDocID.Text = res.ToString();
                    txtDocName.Text = docName;
                    showData();

                    if (c.getFileType(docExt) == CommonFunction.clsCommon.fileType.Image)
                    {
                        string imgName = "../Validation.ashx?file=~/Uploads/" + res + "-1." + docExt;
                        imgFile.Visible = true;
                        imgFile.ImageUrl = imgName;
                    }
                    else
                    {
                        imgFile.Visible = false;
                    }


                }
                else
                {
                    if (Session["lang"].ToString() == "0")
                        lblStep1.Text = "Executable files not allowed";
                    else
                        lblStep1.Text = "الملفات التنفيذية غير مسموح بها";
                }
            }
            else
            {
                if (Session["lang"].ToString() == "0")
                    lblStep1.Text = "Please choose a file";
                else
                    lblStep1.Text = "الرجاء اختيار ملف";
            }
        }

        private void showData()
        {
            pnlDocDetails.Visible = true;
            pnlAttach.Visible = false;
            docID = c.convertToInt32(txtDocID.Text);
            op = new DMS.DAL.operations();
            docTB = op.dboGetDocumentsByPrimaryKey(docID);
            fillData(docTB.table);

            lnkCheck.NavigateUrl = "javascript:parent.showDialog(195, '" + txtDocName.Text +
                    "', " + "'../M/showDocument.aspx?docID=" + c.encrypt(docID.ToString()) + "&ver=1&'" +
                    ", 1300, 700)";
            lnkCheck.Target = "_parent";

            txtDocID.Focus();
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            fillVariables();
            bool workflow = false;
            tables.dbo.docTypes docT = new tables.dbo.docTypes();
            op = new DMS.DAL.operations();
            docT = op.dboGetDocTypesByPrimaryKey(docTypID);

            wfStatus = 0;
            wfStartDateTime = DateTime.MaxValue.AddDays(-1);
            if (docT.fieldDefaultWFID > 0 && chkArchiveOnly.Checked == false)
            { 
                workflow = true;
                wfStatus = 1;
                wfStartDateTime = DateTime.Now;
            }

            DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
            sp.updateDocumentsWithOutMeta(docID, docTypID, docName, docExt, addedDate, addedUserID, 1,
                modifyDate, modifyUserID, fldrID, ocrContent, folderSeq, docTypeSeq, folderDocTypeSeq
                , Int16.MinValue, Int16.MinValue, Int16.MinValue, Int16.MinValue, wfStartDateTime, wfTimeFrame,wfStatus);

            string sql = "select count(metaID) from metas where docTypID=" + drpDocTypID.SelectedValue;
            Int32 metaCount = c.convertToInt16(c.GetDataAsScalar(sql));

            bool isExsit = false;
            if (txtDocID.Text != "")
                isExsit = true;

            string updateSQL = "";
            bool flag = true;
            for (Int16 i = 0; i < metaCount; i++)
            {
                string metaID = Request.Form["ctl00$ContentPlaceHolder1$hdn_" + (i + 1).ToString()];
                string value = Request.Form["ctl00$ContentPlaceHolder1$meta_" + (i + 1).ToString()];
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

            string res = c.encrypt(docID.ToString());
            //Response.Redirect("../M/documentInfo.aspx?docID=" + res);
            ltrRedirect.Text = @"<meta http-equiv=""refresh"" content=""2;url=../M/documentsList.aspx?fldrID=" + 
                c.encrypt(fldrID.ToString()) + @""">";

if (drpNextUser.SelectedIndex == 0)
                {
            if (workflow)
            {
                tables.dbo.wfPathDetails wfPath = new tables.dbo.wfPathDetails();
                op = new DMS.DAL.operations();
                wfPath = op.dboGetAllWfPathDetails("PathId=" + docT.fieldDefaultWFID.ToString(),"seqNo");
                
                    if (wfPath.hasRows)
                    {
                        switch (wfPath.fieldRecipientType)
                        {
                            case 1:
                                op = new DMS.DAL.operations();
                                sp.dboAddDocumentWFPath(docID, c.convertToInt32(Session["userID"]), DateTime.Now,
                                    wfPath.fieldPathID, 0, 1, 1, "", DateTime.Now);

                                op = new DMS.DAL.operations();
                                sp.dboAddDocumentWFPath(docID, wfPath.fieldRecipientID, DateTime.MaxValue.AddDays(-1),
                                    wfPath.fieldPathID, wfPath.fieldSeqNo, 0, wfPath.fieldRecipientType, "", DateTime.Now);
                                try
                                {
                                    //DateTime? EndDocumntDate = null;
                                    //if (txtenddateCount.Value != "")
                                    //{
                                    //    EndDocumntDate = DateTime.Now;
                                    //    EndDocumntDate.Value.AddDays(int.Parse(txtenddateCount.Value));
                                    //}
                                    op = new DMS.DAL.operations();
                                    op.dboAddUserDocuments(wfPath.fieldRecipientID, docID, true, true, true, false);
                                }
                                catch
                                { }
                                break;
                        }

                    }
                }
                
            }
else
{
    op = new DMS.DAL.operations();
    sp.dboAddDocumentWFPath(docID, c.convertToInt32(drpNextUser.SelectedValue), DateTime.MaxValue.AddDays(-1),
        0, 1, 0, 1, "", DateTime.Now);
    sp = new DMS.BLL.specialCases();
    sp.closeDocWF(docID);
}
            pnlAddNew.Visible = false;
            pnlResult.Visible = true;
        }


        public string getOCR(string fileName)
        {
            //string des = Server.MapPath("output.txt");
            //using (IOcrEngine ocrEngain = OcrEngineManager.CreateEngine(OcrEngineType.Arabic, false))
            //{
            //    ocrEngain.Startup(null, null, null, null);
            //    ocrEngain.AutoRecognizeManager.Run(fileName, des, Leadtools.Forms.DocumentWriters.DocumentFormat.Text, null, null);
            //    Process.Start(des);

            //}

            return "";
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            DMS.BLL.specialCases sp = new DMS.BLL.specialCases();

            fillVariables();
            op = new DMS.DAL.operations();
            Int32 res;
            docExt = "tif";
            docExt = docExt.Substring(docExt.LastIndexOf(".") + 1);
            docName = "Scanned Document";
            docName = docName.Substring(docName.LastIndexOf(@"\") + 1);
            docName = docName.Split('.')[0];
            DMS.BLL.specialCases SP = new DMS.BLL.specialCases();

            res = SP.dboAddDocuments(docTypID, docName, docExt, addedDate, addedUserID, lastVersion, modifyDate, modifyUserID, fldrID, ocrContent, 0, 0, 0);
            string desPath = Helper.GetUploadDiskPath();
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(desPath);
            
            if (dir.Exists==false )
                dir.Create();

            string fName = desPath + res + "-1.tif";
            //fluFile.SaveAs(fName);
            if (System.IO.File.GetLastWriteTime(Server.MapPath("../") + Session["userID"].ToString() + ".tif") >= DateTime.Now.AddMinutes(-1))
            {
                System.IO.File.Copy(Server.MapPath("../") + Session["userID"].ToString() + ".tif", fName);


                if (c.getFileType(docExt) == CommonFunction.clsCommon.fileType.Image)
                {
                    ocrContent = getOCR(fName);
                }
                Int32 docGroupID = Int16.MinValue;
                op = new DMS.DAL.operations();
                tables.dbo.documentsGroups docGroup = new tables.dbo.documentsGroups();
                docGroup = op.dboGetAllDocumentsGroups("docTypeID=" + docTypID.ToString());
                if (docGroup.rowsCount > 0)
                    docGroupID = docGroup.fieldDocGroupID;

                op = new DMS.DAL.operations();
                op.dboAddDocumentVersions(res, 1, DateTime.Now, addedUserID, docExt, docGroupID, docName, docName);
                txtDocID.Text = res.ToString();
                txtDocName.Text = docName;
                showData();

                if (c.getFileType(docExt) == CommonFunction.clsCommon.fileType.Image)
                {
                    string imgName = "../Validation.ashx?file=~/Uploads/" + res + "-1." + docExt;
                    imgFile.Visible = true;
                    imgFile.ImageUrl = imgName;
                }
                else
                {
                    imgFile.Visible = false;
                }
            }
            else
            {
                if (Session["lang"].ToString() == "0")
                    lblStep1.Text = "Please scan a file";
                else
                    lblStep1.Text = "الرجاء مسح المستند";
            }
        }

        protected void drpFldrID_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillNextUser();
        }
    }
}