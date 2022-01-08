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
using System.IO;
using dms.MangeForm;
using Newtonsoft.Json;
using IronBarCode;

namespace dms.Screen
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
        int drpTFTypeValue = 0;
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
            drpTFTypeValue = drpTFType.SelectedIndex;
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
        public void callVariables()
        {

            txtDocID.Text = docID.ToString();
            drpDocTypID.SelectedValue = docTypID.ToString();
            txtDocName.Text = docName;
            //docExt = c.convertToString(txtDocExt.Text);
            //addedDate = DateTime.Now;
            //addedUserID = c.convertToInt32(Session["userId"].ToString());
            //lastVersion = c.convertToInt16(1);
            //modifyDate = DateTime.Now;
            //modifyUserID = c.convertToInt32(Session["userId"].ToString());
            drpFldrID.SelectedValue = fldrID.ToString();
            //ocrContent = "";
            hdnFolderSeq.Value = folderSeq.ToString();
            hdnDocTypeSeq.Value = docTypeSeq.ToString();
            hdnFolderDocTypeSeq.Value = folderDocTypeSeq.ToString();
            drpTFType.SelectedIndex = drpTFTypeValue;
            if (drpTFType.SelectedIndex == 0)
            {
                txtWfTimeFrame.Text = wfTimeFrame.ToString();
            }
            if (drpTFType.SelectedIndex == 1)
            {
                txtWfTimeFrame.Text = (wfTimeFrame / 60).ToString();
            }

            if (drpTFType.SelectedIndex == 2)
            {
                txtWfTimeFrame.Text = (wfTimeFrame / 3600).ToString();
            }
        }
        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                lblResultFinal.Text = "تم تحميل الملف بنجاح \n  سوف يصبح بامكانك تغيير مكان الملف في صفحة  الملفات";
                lblFolderName.Text = "إضافة ملف جديد";
                chkArchiveOnly.Text = "أرشفة فقط";
                lnkCheck.Text = "تأكد من المستند";

                drpTFType.Items[0].Text = "دقائق";
                drpTFType.Items[1].Text = "ساعات";
                drpTFType.Items[2].Text = "أيام";
                lblDocName.Text = "اسم المستند";

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
                    if (dType.fieldDefaultWFID > 0)
                    {
                        op = new DMS.DAL.operations();
                        tables.dbo.wfPathDetails WF = op.dboGetAllWfPathDetails("PathID = " + dType.fieldDefaultWFID.ToString(), "SeqNo");
                        op = new DMS.DAL.operations();
                        if (WF.fieldRecipientType != -1 && WF.fieldRecipientType == 1)
                        {
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

            if (!IsPostBack)
            {
                string desPath = Helper.GetUploadDiskPath();
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(desPath);
                hdnImageUploaded.Value = "0";
                checkTempFile();
                converttoArabic();
                op = new DMS.DAL.operations();
                tables.dbo.users user = new tables.dbo.users();
                user = op.dboGetUsersByPrimaryKey(c.convertToInt32(Session["userID"]));
                if (!user.fieldAllowDiwan)
                {
                    drpTFType.Enabled = false;
                    txtWfTimeFrame.ReadOnly = true;
                }
                if (!user.fieldAllowCustomWF)
                {
                    customWF.Visible = false;
                }
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
                    lblParentName.Text = Session["lang"].ToString() == "0" ? foldersTB.fieldFldrName : foldersTB.fieldFldrNameAr;
                    if (foldersTB.fieldDefaultDocTypID > 0)
                    {
                        docTypID = foldersTB.fieldDefaultDocTypID;
                        drpDocTypID.SelectedValue = docTypID.ToString();

                        op = new DMS.DAL.operations();
                        tables.dbo.documentsGroups docGroups = new tables.dbo.documentsGroups();
                        docGroups = op.dboGetAllDocumentsGroups("docTypeID=" + docTypID.ToString());
                        c.FillDropDownList(drpDocGroupID, docGroups.table);

                        //showDocTypeMetas();
                    }
                    //Int32 folderID = c.convertToInt32(drpFldrID.SelectedValue);
                    op = new DMS.DAL.operations();
                    tables.dbo.folders fldr = op.dboGetFoldersByPrimaryKey(fldrID);
                    if (fldr.hasRows)
                    {
                        if (fldr.fieldIsDiwan)
                        {
                            if (Session["lang"].ToString() == "1")
                                lblDocName.Text = "موضوع الكتاب";
                            else
                                lblDocName.Text = "Letter Subject";
                        }
                    }
                    fillNextUser();
                }
                else
                {
                    lblParentName.Text = Session["lang"].ToString() == "0" ? folders.fieldFldrName : folders.fieldFldrNameAr;
                }
                txttypeId.Text = "0";
                if (Request.QueryString["typeId"] != "")
                {
                    txttypeId.Text = Request.QueryString["typeId"];
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
                callSkipAttach();//skip attach file new version
            }


        }

        private void checkTempFile()
        {
            string[] formats = { "pdf", "jpg", "tiff", "tif", "png" };

            foreach (string format in formats)
            {
                string fName = Server.MapPath("../Temp/") + Session["userID"].ToString() + "." + format;
                if (!File.Exists(fName))
                {
                    File.Copy(Server.MapPath("../Samples/") + "sample." + format, fName);
                }
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
            System.Text.StringBuilder autoScript = new System.Text.StringBuilder();
            autoScript.AppendLine(@"<script type=""text/javascript"">");
            autoScript.AppendLine(@"function araneasFillAutos(){");
            autoScript.AppendLine(@"try  {");

            var objMetaManager = new FormMetaManager(int.Parse(drpDocTypID.SelectedValue), docID, int.Parse(Session["userID"].ToString()), !(Session["lang"].ToString() == "0"));
            int folderID = int.Parse(c.GetDataAsScalar("select top 1 fldrID from documents where docID=" + docID).ToString());
            objMetaManager.GetPanal(ref pnlDocMetas, ref autoScript, folderID, int.Parse(Session["userID"].ToString()));


            autoScript.AppendLine(@"return true;}catch(err)  {  alert(err);return false;  }");
            autoScript.AppendLine(@"}");
            autoScript.AppendLine(@"</script>");

            ltrScripts.Text = autoScript.ToString();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (fluFile.HasFile)
            {
                //callSaveDoc();
                //callSaveDoc();
                fillVariables();
                #region save old data
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
                docExt = Convert.ToString(c.GetDataAsScalar("select docExt from documents where docid=" + docID.ToString()));
                DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
                sp.updateDocumentsWithOutMeta(docID, docTypID, docName, docExt, addedDate, addedUserID, 1,
                    modifyDate, modifyUserID, fldrID, ocrContent, folderSeq, docTypeSeq, folderDocTypeSeq
                    , Int16.MinValue, Int16.MinValue, Int16.MinValue, Int16.MinValue, wfStartDateTime, wfTimeFrame, wfStatus);
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
                    int ctrlID = 0;
                    if (metaID != "" && metaID != null)
                        ctrlID = int.Parse(c.GetDataAsScalar("select top 1 metas.ctrlID from metas where metaID=" + metaID).ToString());
                    if (!String.IsNullOrEmpty(metaID))
                    {
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
                #endregion
                op = new DMS.DAL.operations();
                Int32 res;
                docExt = fluFile.FileName;
                docExt = docExt.Substring(docExt.LastIndexOf(".") + 1);
                if (docExt.ToLower() != "exe")
                {
                    foreach (HttpPostedFile uploadedFile in fluFile.PostedFiles)
                    {
                        // get file verstions
                        int fileVerstionID = int.Parse(c.GetDataAsScalar("select ISNULL(Count(dbo.documentVersions.version),0) from dbo.documentVersions where dbo.documentVersions.docID=" + txtDocID.Text).ToString()) + 1;
                        docName = uploadedFile.FileName;
                        docName = docName.Substring(docName.LastIndexOf(@"\") + 1);
                        docName = docName.Split('.')[0];
                        DMS.BLL.specialCases SP = new DMS.BLL.specialCases();

                        res = int.Parse(txtDocID.Text);//SP.dboAddDocuments(docTypID, docName, docExt, addedDate, addedUserID, lastVersion, modifyDate, modifyUserID, fldrID, ocrContent, 0, 0, 0);
                        string desPath = Helper.GetUploadDiskPath();
                        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(desPath);
                        if (!dir.Exists)
                            dir.Create();

                        string fName = desPath + res + "-" + fileVerstionID + "." + docExt;
                        uploadedFile.SaveAs(fName);

                        if (c.getFileType(docExt) == CommonFunction.clsCommon.fileType.Image)
                        {
                            ocrContent = getOCR(fName);
                        }
                        //Int32 docGroupID = Int16.MinValue;
                        //op = new DMS.DAL.operations();
                        //tables.dbo.documentsGroups docGroup = new tables.dbo.documentsGroups();
                        //docGroup = op.dboGetAllDocumentsGroups("docTypeID=" + docTypID.ToString());
                        //if (docGroup.rowsCount > 0)
                        //    docGroupID = docGroup.fieldDocGroupID;

                        op = new DMS.DAL.operations();
                        op.dboAddDocumentVersions(res, (short)fileVerstionID, DateTime.Now, addedUserID, docExt, c.convertToInt32(drpDocGroupID.SelectedValue), docName, docName);
                        txtDocID.Text = res.ToString();
                    }
                    //txtDocName.Text = docName;
                    showData();
                    //if (c.getFileType(docExt) == CommonFunction.clsCommon.fileType.Image)
                    //{
                    //    string imgName = "../Validation.ashx?file=~/Uploads/" + res + "-1." + docExt;
                    //    imgFile.Visible = true;
                    //    imgFile.ImageUrl = imgName;
                    //}
                    //else
                    //{
                    //    imgFile.Visible = false;
                    //}
                    imgFile.Visible = false;
                    BindAttachFiles();
                }
                else
                {
                    if (Session["lang"].ToString() == "0")
                        lblStep1.Text = "Executable files not allowed";
                    else
                        lblStep1.Text = "الملفات التنفيذية غير مسموح بها";
                }
                //callVariables();
            }
            else
            {
                if (Session["lang"].ToString() == "0")
                    lblStep1.Text = "Please choose a file or not choose empty file";
                else
                    lblStep1.Text = "الرجاء اختيار ملف او عدم اختيار ملف فارغ";
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
                    "', " + "'../Screen/showDocument.aspx?docID=" + c.encrypt(docID.ToString()) + "&ver=1&'" +
                    ", 1300, 700)";
            lnkCheck.Target = "_parent";

            txtDocID.Focus();
        }

        protected void SaveChanges_Click(object sender, EventArgs e)
        {
            fillVariables();
            callSaveDoc();
            if (txttypeId.Text != "0" && txttypeId.Text != "")
            {
                DMS.BLL.specialCases.SaveInOutSerial(int.Parse(txtDocID.Text), 2);
            }
            pnlAddNew.Visible = false;
            pnlResult.Visible = true;
            string serial = c.GetDataAsScalar("select top 1 serial from documents where docID=" + int.Parse(txtDocID.Text) + "").ToString();
            string typeId = c.GetDataAsScalar("select top 1 typeId from documents where docID=" + int.Parse(txtDocID.Text) + "").ToString();
            // Generate a Simple BarCode image and save as PNG
            //using IronBarCode;
            GeneratedBarcode MyBarCode = IronBarCode.BarcodeWriter.CreateBarcode("00000000" + txtDocID.Text, BarcodeWriterEncoding.Code128, 200, 50);
            string txtBarCode = "العنوان : " + txtDocName.Text + "";
            txtBarCode += "\r\n";
            txtBarCode += "التاريخ : " + DateTime.Now.ToString("dd-MM-yyyy") + "   " + "رقم المستند :" + txtDocID.Text;
            if (typeId != "" && typeId != null)
            {
                if (typeId == "1")
                {
                    txtBarCode += "\r\n";
                    txtBarCode += "رقم الصادر : " + serial;
                }
                else
                {
                    txtBarCode += "\r\n";
                    txtBarCode += "رقم الوارد : " + serial;
                }
            }
            string filename = "/images/barcode" + DateTime.Now.ToString("ddMMyyyyhhmmssfff") + ".png";
            MyBarCode.AddAnnotationTextAboveBarcode(txtBarCode);
            MyBarCode.SaveAsPng(Server.MapPath("~" + filename));
            c.NonQuery("update documents set Barcode='" + filename + "' where docID=" + int.Parse(txtDocID.Text));
            //MyBarCode.vo
            // This line opens the image in your default image viewer
            //System.Diagnostics.Process.Start(Server.MapPath("~/images/barcode.png"));
        }
        public void callSaveDoc()
        {
            //
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

            docExt = Convert.ToString(c.GetDataAsScalar("select docExt from documents where docid=" + docID.ToString()));

            DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
            sp.updateDocumentsWithOutMeta(docID, docTypID, docName, docExt, addedDate, addedUserID, 1,
                modifyDate, modifyUserID, fldrID, ocrContent, folderSeq, docTypeSeq, folderDocTypeSeq
                , Int16.MinValue, Int16.MinValue, Int16.MinValue, Int16.MinValue, wfStartDateTime, wfTimeFrame, wfStatus);
            //update submit date
            c.NonQuery("update dbo.documents set submitDate ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' where docID=" + docID + "");
            //try
            //{
            if (hdnImageUploaded.Value != "-1")
            {
                string desPath = Helper.GetUploadDiskPath();
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(desPath);
                string fName = desPath + docID.ToString() + "-1." + docExt;
                if (hdnScannerFlag.Value == "1")
                {
                    System.IO.File.Copy(Helper.GetTempDiskPath() + Session["userID"].ToString() + "." + docExt, fName);
                    hdnScannerFlag.Value = "0";
                }
                if (c.getFileType(docExt) == CommonFunction.clsCommon.fileType.Image)
                {
                    ocrContent = getOCR(fName);
                }
            }
            //}
            //catch { }


            string sql = "select count(metaID) from metas where docTypID=" + drpDocTypID.SelectedValue;
            Int32 metaCount = c.convertToInt16(c.GetDataAsScalar(sql));

            //get meta

            bool isExsit = false;
            if (txtDocID.Text != "")
                isExsit = true;

            string updateSQL = "";
            bool flag = true;
            string jsonString = hdnDynamicTabls.Value;
            //int i = 0;
            //foreach (DataRow row in dt.Rows)
            //{
            for (Int16 i = 0; i < metaCount; i++)
            {
                string metaID = Request.Form["ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolderBody$hdn_" + (i + 1).ToString()];
                int ctrlID = 0;
                int metaIDFK = 0;
                DataRow DR;
                if (metaID != "" && metaID != null)
                {
                    DR = c.GetDataAsDataTable("select top 1 * from metas where metaid=" + metaID).Rows[0];
                    ctrlID = int.Parse(DR["ctrlID"].ToString());
                    metaIDFK = int.Parse(DR["metaIDFK"].ToString());


                    if (!String.IsNullOrEmpty(metaID))
                    {
                        string value = Request.Form["ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolderBody$meta_" + (i + 1).ToString()];
                        if (ctrlID != 6 && !String.IsNullOrEmpty(value) && ctrlID != 0 && metaIDFK == 0)
                        {
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
                                //metaID = metaID != null ? metaID : myMetaID.ToString();
                                op.dboAddDocumentMataValues(c.convertToInt32(metaID), docID, value);
                            }
                            updateSQL += "Meta" + (i + 1).ToString() + " = N'" + value + "',";
                        }
                        else if (ctrlID == 6)
                        {
                            List<dynamicData> dynamicDatas = JsonConvert.DeserializeObject<List<dynamicData>>(jsonString);
                            DataTable dt = c.GetDataAsDataTable("select * from metas where metaIdFK=" + metaID);
                            int j = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                int myMetaID = int.Parse(row["metaID"].ToString());
                                int metaIdFK = int.Parse(row["metaIdFK"].ToString());
                                if (metaIdFK != 0) // this is exist
                                {
                                    var obj = dynamicDatas.FirstOrDefault(x => x.id == myMetaID);
                                    if (obj.id == myMetaID)
                                    {
                                        // JsonConvert.SerializeObject<dynamicData>(obj);
                                        value = obj.value;//JsonConvert.SerializeObject(obj);
                                    }
                                    if (!DMS.Security.isNotAllowedCharacters(value))
                                    {
                                        flag = false;
                                    }
                                    op = new DMS.DAL.operations();
                                    tables.dbo.documentMataValues docMetas = new tables.dbo.documentMataValues();
                                    if (isExsit)
                                    {
                                        docMetas = op.dboGetDocumentMataValuesByPrimaryKey(docID, c.convertToInt32(myMetaID));
                                        if (docMetas.hasRows)
                                        {
                                            op = new DMS.DAL.operations();
                                            op.dboUpdateDocumentMataValuesByPrimaryKey(c.convertToInt32(myMetaID), docID, value);
                                        }
                                    }
                                    if (!isExsit || !docMetas.hasRows)
                                    {
                                        op = new DMS.DAL.operations();
                                        //metaID = metaID != null ? metaID : myMetaID.ToString();
                                        op.dboAddDocumentMataValues(c.convertToInt32(myMetaID), docID, value);
                                    }
                                    //updateSQL += "Meta" + (i + 1 + j).ToString() + " = N'" + value + "',";
                                    
                                    j++;
                                }

                            }

                            updateSQL += "Meta" + i.ToString() + " = N'',";
                        }
                        else if (value == "")
                        { // this exp case
                          //get exp
                            string defaultTexts = c.GetDataAsScalar("select top 1 metas.defaultTexts from metas where metaID=" + metaID).ToString();
                            string defaultValues = c.GetDataAsScalar("select top 1 metas.defaultValues from metas where metaID=" + metaID).ToString();
                            if (defaultTexts != "" && defaultValues != "")
                            {
                                string textValue = fixMetaExpWithParms(defaultValues, i);
                                // JsonConvert.SerializeObject<dynamicData>(obj);
                                value = textValue;//JsonConvert.SerializeObject(obj);
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
                                    //metaID = metaID != null ? metaID : myMetaID.ToString();
                                    op.dboAddDocumentMataValues(c.convertToInt32(metaID), docID, value);
                                }
                                updateSQL += "Meta" + (i + 1).ToString() + " = N'" + value + "',";
                            }
                        }
                    }
                    //else
                    //{
                    //    tables.dbo.metas metas = op.dboGetMetasByPrimaryKey();
                    //}
                    //i++;
                }
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
            //Response.Redirect("../Screen/documentInfo.aspx?docID=" + res);
            if (docTypID == 2 || docTypID == 22)
            {
                ltrRedirect.Text = @"<meta http-equiv=""refresh"" content=""2;url=../Screen/documentsList.aspx?fldrID=" +
               c.encrypt(fldrID.ToString()) + "&isDiwan=true" + @""">";
            }
            else
            {
                ltrRedirect.Text = @"<meta http-equiv=""refresh"" content=""2;url=../Screen/documentsList.aspx?fldrID=" +
               c.encrypt(fldrID.ToString()) + @""">";
            }

            if (drpNextUser.SelectedIndex == 0)
            {
                if (workflow)
                {
                    tables.dbo.wfPathDetails wfPath = new tables.dbo.wfPathDetails();
                    op = new DMS.DAL.operations();
                    wfPath = op.dboGetAllWfPathDetails("PathId=" + docT.fieldDefaultWFID.ToString(), "seqNo");
                    bool isCalled = false;

                    DateTime? EndDocumntDate = null;
                    if (txtenddateCount.Value != "" && isCalled == false)
                    {
                        isCalled = true;
                        EndDocumntDate = DateTime.Now;
                        EndDocumntDate = EndDocumntDate.Value.AddDays(int.Parse(txtenddateCount.Value));
                    }
                    if (wfPath.hasRows)
                    {
                        switch (wfPath.fieldRecipientType)
                        {
                            case 1:
                                sp = new DMS.BLL.specialCases();
                                sp.dboAddDocumentWFPath(docID, c.convertToInt32(Session["userID"]), DateTime.Now,
                                    wfPath.fieldPathID, 0, 1, 1, "", DateTime.Now);

                                sp = new DMS.BLL.specialCases();
                                sp.dboAddDocumentWFPath(docID, wfPath.fieldRecipientID, DateTime.MaxValue.AddDays(-1),
                                    wfPath.fieldPathID, wfPath.fieldSeqNo, 0, wfPath.fieldRecipientType, "", DateTime.Now);
                                //update first user date
                                DataTable documnetPathDtails = c.GetDataAsDataTable("select * from [dbo].[wfPathDetails] where pathid=" + wfPath.fieldPathID + " and seqNo=1");
                                int recipientID = (from DataRow dr in documnetPathDtails.Rows
                                                   select (int)dr["recipientID"]).FirstOrDefault();
                                var updateWpPath = c.NonQuery("update [dbo].[documentWFPath] set [EndDate]='" + EndDocumntDate + "' where [userID]=" + recipientID + " and [docID]=" + docID + "");
                                try
                                {
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
                sp = new DMS.BLL.specialCases();
                sp.dboAddDocumentWFPath(docID, c.convertToInt32(drpNextUser.SelectedValue), DateTime.MaxValue.AddDays(-1),
                    0, 1, 0, 1, "", DateTime.Now);
                sp = new DMS.BLL.specialCases();
                sp.closeDocWF(docID);
            }
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
        public string fixMetaExpWithParms(string exp, Int32 index = 0)
        {
            Int32 i = index;
            string textValue = exp;
            textValue = textValue.Replace("#expr:this =", "");
            //textValue = textValue.Substring(textValue.IndexOf(":") + 1);
            //MatchEvaluator evaluator = new MatchEvaluator(WordScrambler);
            //textValue = Regex.Replace(textValue, @"meta_\d+", evaluator);
            //textValue = textValue.Replace("this", "document.getElementById('meta_" + (i + 1).ToString() + "').value");
            //textValue = textValue.Replace("this", "document.getElementById('meta_" + (i + 1).ToString() + "').value");
            var Arr = textValue.Split('+');
            textValue = "";
            foreach (var item in Arr)
            {
                if (item.IndexOf("'") != -1)
                    textValue += item.Replace("'", "");
                if (item.IndexOf("currentYearLong") != -1)
                    textValue += item.Replace("currentYearLong", DateTime.Now.ToString("yyyy"));
                if (item.IndexOf("currentYearShort") != -1)
                    textValue += item.Replace("currentYearShort", DateTime.Now.ToString("yy"));
                if (item.IndexOf("FolderSeq") != -1)
                    textValue += item.Replace("FolderSeq", folderSeq.ToString());
                if (item.IndexOf("DocTypeSeq") != -1)
                    textValue += item.Replace("DocTypeSeq", docTypeSeq.ToString());
                if (item.IndexOf("FolderDocTypeSeq") != -1)
                    textValue += item.Replace("FolderDocTypeSeq", folderDocTypeSeq.ToString());
            }
            return textValue;
        }
        [System.Web.Services.WebMethod]
        protected static string AjaxuploadScan_Click(string name)
        {
            return "Hello " + name + Environment.NewLine + "The Current Time is: "
        + DateTime.Now.ToString();
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupScript", "confirm('Is this selection correct?');", true);
            //if ( )
            //{
            //    //Run server-side to access database.
            //}
            fillVariables();
            op = new DMS.DAL.operations();
            Int32 res;
            docExt = drpFormat.SelectedValue;
            docExt = docExt.Substring(docExt.LastIndexOf(".") + 1);

            if (Session["lang"].ToString() == "1")
                docName = "اضبارة ";
            else
                docName = "Scanned Document";
            docName = docName.Substring(docName.LastIndexOf(@"\") + 1);
            docName = docName.Split('.')[0];
            DMS.BLL.specialCases SP = new DMS.BLL.specialCases();

            res = SP.dboAddDocuments(docTypID, docName, docExt, addedDate, addedUserID, lastVersion, modifyDate, modifyUserID, fldrID, ocrContent, 0, 0, 0);

            hdnScannerFlag.Value = "1";


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

            //if (c.getFileType(docExt) == CommonFunction.clsCommon.fileType.Image)
            //{
            //    string imgName = "../Validation.ashx?file=~/Uploads/" + res + "-1." + docExt;
            //    imgFile.Visible = true;
            //    imgFile.ImageUrl = imgName;
            //}
            //else
            //{
            imgFile.Visible = false;
            // }
            //}
            //else
            //{
            //    if (Session["lang"].ToString() == "0")
            //        lblStep1.Text = "Please scan a file";
            //    else
            //        lblStep1.Text = "الرجاء مسح المستند";
            //}
        }

        protected void drpFldrID_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillNextUser();
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            fillVariables();
            op = new DMS.DAL.operations();
            Int32 res;
            docExt = "";

            if (Session["lang"].ToString() == "1")
                docName = "نموذج إلكتروني جديد";
            else
                docName = "New eForm";

            DMS.BLL.specialCases SP = new DMS.BLL.specialCases();

            res = SP.dboAddDocuments(docTypID, docName, docExt, addedDate, addedUserID, lastVersion, modifyDate, modifyUserID, fldrID, ocrContent, 0, 0, 0);

            Int32 docGroupID = Int16.MinValue;
            op = new DMS.DAL.operations();
            tables.dbo.documentsGroups docGroup = new tables.dbo.documentsGroups();
            docGroup = op.dboGetAllDocumentsGroups("docTypeID=" + docTypID.ToString());
            if (docGroup.rowsCount > 0)
                docGroupID = docGroup.fieldDocGroupID;

            op = new DMS.DAL.operations();
            //op.dboAddDocumentVersions(res, 1, DateTime.Now, addedUserID, docExt, docGroupID);
            txtDocID.Text = res.ToString();
            txtDocName.Text = docName;
            showData();
            imgFile.Visible = false;
            hdnImageUploaded.Value = "-1";
        }
        public void callSkipAttach()
        {
            fillVariables();
            op = new DMS.DAL.operations();
            Int32 res;
            docExt = "";
            if (Session["lang"].ToString() == "1")
                docName = "نموذج إلكتروني جديد";
            else
                docName = "New eForm";
            DMS.BLL.specialCases SP = new DMS.BLL.specialCases();
            res = SP.dboAddDocuments(docTypID, docName, docExt, addedDate, addedUserID, lastVersion, modifyDate, modifyUserID, fldrID, ocrContent, 0, 0, 0);
            Int32 docGroupID = Int16.MinValue;
            op = new DMS.DAL.operations();
            tables.dbo.documentsGroups docGroup = new tables.dbo.documentsGroups();
            docGroup = op.dboGetAllDocumentsGroups("docTypeID=" + docTypID.ToString());
            if (docGroup.rowsCount > 0)
                docGroupID = docGroup.fieldDocGroupID;
            op = new DMS.DAL.operations();
            //op.dboAddDocumentVersions(res, 1, DateTime.Now, addedUserID, docExt, docGroupID);
            txtDocID.Text = res.ToString();
            txtDocName.Text = docName;
            showData();
            imgFile.Visible = false;
            hdnImageUploaded.Value = "-1";
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

        protected void btnDeleteDocumnet_ServerClick(object sender, EventArgs e)
        {
            c = new CommonFunction.clsCommon();
            string docID = txtDocID.Text;
            op = new DMS.DAL.operations();
            op.dboDeleteDocumentVersions("docID=" + docID);
            op = new DMS.DAL.operations();
            op.dboDeleteDocumentsByPrimaryKey(c.convertToInt32(docID));
            Response.Redirect("defaultAr.aspx");
        }

        protected void btnshowattachpanel_ServerClick(object sender, EventArgs e)
        {
            pnlAttach.Visible = true;
            pnlDocDetails.Visible = false;
            BindAttachFiles();
        }
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {

            var button = (LinkButton)sender;
            int verstionId = int.Parse(button.CommandArgument.ToString());
            c = new CommonFunction.clsCommon();
            string docID = txtDocID.Text;
            op = new DMS.DAL.operations();
            op.dboDeleteDocumentVersions("docID=" + docID + "&version=" + verstionId);
            BindAttachFiles();
            //  do what you want
        }
        public void BindAttachFiles()
        {
            c = new CommonFunction.clsCommon();
            DataTable dt = c.GetDataAsDataTable("SELECT distinct documentVersions.docID, documentVersions.version, documentVersions.ext, documents.docName FROM     documents Right JOIN documentVersions on documents.docID = documentVersions.docID where documentVersions.docID=" + txtDocID.Text);
            lstUploadFiles.DataSource = dt;
            lstUploadFiles.DataBind();
            lstUploadFiles2.DataSource = dt;
            lstUploadFiles2.DataBind();
            if (dt.Rows.Count <= 0)
            {
                fileEmpty2.Visible = true;
                fileEmpty.Visible = true;
            }
            else
            {
                fileEmpty2.Visible = false;
                fileEmpty.Visible = false;
            }
        }
    }
    public class dynamicData
    {
        public int id { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }
}