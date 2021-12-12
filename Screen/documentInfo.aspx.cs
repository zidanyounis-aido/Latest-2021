using dms.MangeForm;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using tables.dbo;

namespace dms.Screen
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
        private List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
        public void fillData(DataTable DT)
        {
            c.fillData(DT, 0, docTB.columnsArray, Page);

            showDocTypeMetas();

            fillVersions();
        }

        public void fillVariables()
        {
            docID = c.convertToInt64(txtDocID.Text);
            docTypID = c.convertToInt32(hdnDocTypID.Value);
            docName = c.convertToString(txtDocName.Text);
            docExt = c.convertToString(hdnDocExt.Value);
            addedDate = c.convertToDateTime(hdnAddedDate.Value);
            addedUserID = c.convertToInt32(hdnAddedUserID.Value);
            lastVersion = c.convertToInt16(hdnLastVersion.Value);
            modifyDate = DateTime.Now;
            modifyUserID = c.convertToInt32(Session["userId"].ToString());
            fldrID = c.convertToInt32(hdnDocTypID.Value);
            ocrContent = hdnOcrContent.Value;
            folderSeq = c.convertToInt64(hdnFolderSeq.Value);
            docTypeSeq = c.convertToInt64(hdnDocTypeSeq.Value);
            folderDocTypeSeq = c.convertToInt64(hdnFolderDocTypeSeq.Value);
            //wfTimeFrame = c.convertToDecimal(txtWfTimeFrame.Text);
            //wfStatus = c.convertToInt16(hdnWfStatus.Value);

            //if (drpTFType.SelectedIndex == 0)
            //{
            //    wfTimeFrame = c.convertToDecimal(txtWfTimeFrame.Text);
            //}

            //if (drpTFType.SelectedIndex == 1)
            //{
            //    wfTimeFrame = c.convertToDecimal(txtWfTimeFrame.Text) * 60;
            //}

            //if (drpTFType.SelectedIndex == 2)
            //{
            //    wfTimeFrame = c.convertToDecimal(txtWfTimeFrame.Text) * 3600;
            //}
        }


        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                lblDocName.Text = "اسم الملف";

                //lblFolderName.Text = "تعديل الملف";
                Label1.Text = "إضافة مرفق جديد";
                //TabPanel0.HeaderText = "معلومات المستند";
                //TabPanel1.HeaderText = "معلومات المستند";
                //TabPanel2.HeaderText = "مسار العمل";
                //TabPanel3.HeaderText = "تحويل";
                //TabPanel4.HeaderText = "احداث";
                //TabPanel5.HeaderText = "قائمة العمل";
                //TabPanel6.HeaderText = "التوقيع";

                //TabContainer1.Tabs[0].HeaderText = "معلومات المستند";
                //TabContainer1.Tabs[1].HeaderText = "المرفقات";
                //TabContainer1.Tabs[2].HeaderText = "مسار العمل";
                //TabContainer1.Tabs[3].HeaderText = "تحويل";
                //TabContainer1.Tabs[5].HeaderText = "التوقيع";
                //drpTFType.Items[0].Text = "دقائق";
                //drpTFType.Items[1].Text = "ساعات";
                //drpTFType.Items[2].Text = "أيام";

                //drpRecipientType.Items.FindByValue("1").Text = "مستخدم";
                //drpRecipientType.Items.FindByValue("2").Text = "مجموعة";
                //drpRecipientType.Items.FindByValue("3").Text = "مسمى وظيفي";
                //drpRecipientType.Items.FindByValue("4").Text = "وحدة";

                //rdoSendType.Items[0].Text = "نسخة إلى";
                //rdoSendType.Items[1].Text = "نسخة مخفية";
            }

        }


        public void fillWorkflow()
        {
            string sql = "SELECT    dbo.users.fullName, dbo.documentWFPath.receiveDate, dbo.documentWFPath.actionType, dbo.documentWFPath.actionDateTime,dbo.documentWFPath.EndDate,  " +
                        " dbo.documentWFPath.userNotes FROM         dbo.users INNER JOIN       dbo.documentWFPath ON dbo.users.userID = dbo.documentWFPath.userID " +
                        " WHERE     (dbo.documentWFPath.docID = " + docID.ToString() + ")";
            DataTable DT = new DataTable();
            DT = c.GetDataAsDataTable(sql);
            rptWorkflow.DataSource = DT;
            rptWorkflow.DataBind();

            if (DT.Rows.Count > 0)
                pnlNoPath.Visible = false;
            else
                pnlNoPath.Visible = true;
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
            //if (!user.fieldAllowDiwan)
            //{
            //    drpTFType.Enabled = false;
            //    txtWfTimeFrame.ReadOnly = true;
            //}

            c = new CommonFunction.clsCommon();
            if (Request.QueryString["docID"] == "")
                Response.Redirect("../Scressn/", true);

            string dc = Request.QueryString["docID"];
            if (dc.Contains(".aspx?CODEN="))
            {
                dc = dc.Replace(".aspx?CODEN=1", "");
            }
            dc = c.decrypt(dc);
            docID = c.convertToInt64(dc);
            FillAllVersionSignture();
            //src="<%# Request.QueryString["docID"] %>"
            var todoListUrl = string.Format("../Screen/ToDoList.aspx?docId={0}", docID);
            todoListFrame.Src = todoListUrl;
            eventsIframe.Src = string.Format("../Screen/EventsDocument.aspx?docId={0}", docID);
            if (!IsPostBack)
            {
                //fillDrpRecipientID();
                fillWorkflow();
                tables.dbo.docTypes docTypes = new tables.dbo.docTypes();
                op = new DMS.DAL.operations();
                docTypes = op.dboGetAllDocTypes();
                //if (Session["lang"].ToString() == "0")
                //    c.FillDropDownList(drpDocTypID, docTypes.table);
                //else
                //    c.FillDropDownList(drpDocTypID, docTypes.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "docTypID", "docTypDescAr");

                tables.dbo.folders folders = new tables.dbo.folders();
                op = new DMS.DAL.operations();
                folders = op.dboGetAllFolders();
                //if (Session["lang"].ToString() == "0")
                //    c.FillDropDownList(drpFldrID, folders.table);
                //else
                //    c.FillDropDownList(drpFldrID, folders.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "fldrID", "fldrNameAr");

                //if (Request.QueryString["fldrID"] != null && Request.QueryString["fldrID"] != "")
                //    hdnDocTypID.Value = c.decrypt(Request.QueryString["fldrID"]);

                op = new DMS.DAL.operations();
                docTB = op.dboGetDocumentsByPrimaryKey(docID);
                //lblPrent.InnerHtml = (Session["lang"].ToString() == "0") ? c.GetDataAsScalar("select top 1    fldrName FROM         folders where fldrID=" + docTB.fieldFldrID + "").ToString() : c.GetDataAsScalar("select top 1    fldrNameAr FROM         folders where fldrID=" + docTB.fieldFldrID + "").ToString();
                lnkParent.Text= (Session["lang"].ToString() == "0") ? c.GetDataAsScalar("select top 1    fldrName FROM         folders where fldrID=" + docTB.fieldFldrID + "").ToString() : c.GetDataAsScalar("select top 1    fldrNameAr FROM         folders where fldrID=" + docTB.fieldFldrID + "").ToString();
                lnkParent.NavigateUrl = "documentsList.aspx?fldrID=" + c.encrypt(docTB.fieldFldrID.ToString());

                lblFolderName.Text = docTB.fieldDocName;


                op = new DMS.DAL.operations();
                tables.dbo.documentsGroups docGroups = new tables.dbo.documentsGroups();
                docGroups = op.dboGetAllDocumentsGroups("docTypeID=" + docTB.fieldDocTypID);
                hdnDocTypID.Value = docTB.fieldDocTypID.ToString();
                c.FillDropDownList(drpDocGroupID, docGroups.table);
                //showDocTypeMetas();
                showData();
                // show export code if exist
                int TypeId = int.Parse(c.GetDataAsScalar("select top 1 ISNULL(typeId,0) from documents where docid=" + docID).ToString());
                if (TypeId != 0)
                {
                    divForExport.Visible = true;
                    string serialCode = c.GetDataAsScalar("select top 1 serial from documents where docid=" + docID).ToString();
                    if (TypeId == 1) //outgoing  
                    {
                        lblExport.Text = Session["lang"].ToString() == "0" ? "Upcoming number" : "رقم الصادر";
                    }
                    else //ingoing
                    {
                        lblExport.Text = Session["lang"].ToString() == "0" ? "Incoming number" : "رقم الوارد";
                    }
                    txtExportCode.Text = serialCode;
                }

                tables.dbo.users usersTB = new tables.dbo.users();
                usersTB = op.dboGetAllUsers();
                c.FillDropDownList(drpUsers, usersTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False,
                    "", "", "userID", "FullName");
                
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
            //TabContainer1.ActiveTabIndex = 0;
        }
        private void FillAllVersionSignture()
        {
            // CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            try
            {
                //List<UsersList> list = new List<UsersList>();
                string q = "SELECT dbo.SignatureTB.Id, dbo.SignatureTB.Signture, dbo.SignatureTB.Documnet, dbo.SignatureTB.UserId, dbo.users.fullName, dbo.documentVersions.DocumentFileName, dbo.documentVersions.docID,dbo.SignatureTB.Date"
                        + " FROM dbo.users INNER JOIN dbo.SignatureTB ON dbo.users.userID = dbo.SignatureTB.UserId INNER JOIN dbo.documentVersions ON CONVERT(varchar(10), dbo.documentVersions.docID) + '-' + CONVERT(varchar(10), dbo.documentVersions.version) = dbo.SignatureTB.Documnet"
                        + " where dbo.documentVersions.docID = " + docID.ToString();
                DataTable dtx = c.GetDataAsDataTable(q);
                
                lstAllSign.DataSource = dtx;
                lstAllSign.DataBind();
                if (dtx.Rows.Count==0)
                {
                    Panel1.Visible = false;
                    tblSigntureEmpty.Visible = true;
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        private void showData()
        {

            op = new DMS.DAL.operations();
            docTB = op.dboGetDocumentsByPrimaryKey(docID);
            fillData(docTB.table);
            op = new DMS.DAL.operations();
            tables.dbo.folders fldr = op.dboGetFoldersByPrimaryKey(docTB.fieldFldrID);
            //trWFDeadline.Visible = fldr.fieldAllowWF;
            //TabPanel2.Visible = fldr.fieldAllowWF;
            //TabPanel3.Visible = fldr.fieldAllowWF;

            if (fldr.fieldIsDiwan)
            {
                if (Session["lang"].ToString() == "1")
                    lblDocName.Text = "موضوع الكتاب";
                else
                    lblDocName.Text = "Letter subject";
            }
            //txtWfTimeFrame.Text = (docTB.fieldWfTimeFrame / 3600).ToString("0.00");
            tables.dbo.userFolders userFldr = new tables.dbo.userFolders();
            op = new DMS.DAL.operations();
            userFldr = op.dboGetUserFoldersByPrimaryKey(docTB.fieldFldrID, c.convertToInt32(Session["userID"]));
            if (userFldr.hasRows)
            {
                if (!userFldr.fieldAllow)
                    Response.Redirect("../Screen/DefaultAr.aspx");

                if (!userFldr.fieldAllowDelete)
                    LinkButton2.Visible = false;

                if (!userFldr.fieldAllowUpdate)
                {
                    lnkSaveDoc.Visible = false;
                    LinkButton3.Visible = false;
                }

                lnkExport.Visible = userFldr.fieldAllowOutgoing;
                if (docTB.fieldTypeId == 1)
                    lnkExport.Visible = false;

                lnkPrint.NavigateUrl = "javascript:parent.showDialog(195, '" + txtDocName.Text +
                    "', " + "'../Screen/DocumentInfoPrint.aspx?docID=" + c.encrypt(docID.ToString()) + "&'" +
                    ", 1300, 700)";

                txtDocID.Focus();
            }
            else
            {
                Response.Redirect("../screen/notAllowed.html");
            }

        }

        //protected void drpDocTypID_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    showDocTypeMetas();
        //}

        public void showDocTypeMetas()
        {
            var objMetaManager = new FormMetaManager(int.Parse(hdnDocTypID.Value), docID, int.Parse(Session["userID"].ToString()), !(Session["lang"].ToString() == "0"));
            StringBuilder autoScript = new StringBuilder();
            //int docID = c.convertToInt32(c.decrypt(Request.QueryString["docID"]));
            int folderID = int.Parse(c.GetDataAsScalar("select top 1 fldrID from documents where docID=" + docID).ToString());
            objMetaManager.GetPanal(ref pnlDocMetas, ref autoScript, folderID, int.Parse(Session["userID"].ToString()),false);

        }

        protected void lnkSaveDoc_Click(object sender, EventArgs e)
        {
            fillVariables();
            DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
            sp.updateDocumentsWithOutMeta(docID, docTypID, docName,
                docExt, addedDate, addedUserID, lastVersion, modifyDate,
                modifyUserID, fldrID, ocrContent, folderSeq, docTypeSeq, folderDocTypeSeq
                , Int16.MinValue, Int16.MinValue, Int16.MinValue, Int16.MinValue, DateTime.MaxValue.AddDays(-1), wfTimeFrame, wfStatus
                );

            string sql = "select count(metaID) from metas where docTypID=" + hdnDocTypID.Value;
            Int32 metaCount = c.convertToInt16(c.GetDataAsScalar(sql));

            bool isExsit = false;
            if (txtDocID.Text != "")
                isExsit = true;
            string updateSQL = "";
            bool flag = true;
            for (Int16 i = 0; i < metaCount; i++)
            {
                //ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolderBody$
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
                                op.dboAddDocumentMataValues(c.convertToInt32(metaID), docID, value);
                            }
                            updateSQL += "Meta" + (i + 1).ToString() + " = N'" + value + "',";
                        }
                        else if (ctrlID == 6) // save table
                        {
                            string jsonString = hdnDynamicTabls.Value;
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
                    }
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

            if (Session["lang"].ToString() == "0")
                lblRes.Text = "Document changes have been saved";
            else
                lblRes.Text = "تم حفظ التغييرات";
        }
        //lnkPDF_Click
        protected void lnkPDF_Click(object sender, EventArgs e)
        {
            #region Create PDF
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
            try
            {

                // step 2:
                // we create a writer that listens to the document
                // and directs a PDF-stream to a file
                string fname = $"{ Helper.GetUploadDiskPath()}pdf/" + txtDocName.Text.Replace(" ", "_") + ".pdf";
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
                    string filepah = Helper.GetUploadDiskPath() + docID.ToString() + "-" + vers.fieldVersion.ToString() + "." + vers.fieldExt;
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
                foreach (HttpPostedFile uploadedFile in fluFile.PostedFiles)
                {
                    Int16 res;
                    res = c.convertToInt16(c.GetMax("version", "dbo.documentVersions", "docID=" + docID.ToString()));
                    op = new DMS.DAL.operations();
                    docExt = uploadedFile.FileName;
                    docExt = docExt.Substring(docExt.LastIndexOf(".") + 1);
                    DocumentFileName = uploadedFile.FileName.Substring(0, uploadedFile.FileName.LastIndexOf("."));
                    op.dboAddDocumentVersions(docID, res, DateTime.Now, c.convertToInt32(Session["userID"]), docExt, c.convertToInt32(drpDocGroupID.SelectedValue), DocumentFileName, DocumentFileName);

                    //string desPath = Server.MapPath(@"../Uploads/");
                    string desPath = Helper.GetUploadDiskPath();
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(desPath);
                    if (!dir.Exists)
                        dir.Create();
                    uploadedFile.SaveAs(desPath + @"\" + docID + "-" + res.ToString() + "." + docExt);
                    dms.Controlers.Common.DocToPdfConvert docToPdfConvert = new Controlers.Common.DocToPdfConvert();
                    docToPdfConvert.ConvertToPDF(uploadedFile, docID + "-" + res.ToString(), desPath);
                    string sql = "update dbo.documents set lastVersion=" + res.ToString() + " where docID=" + docID.ToString();
                    c.NonQuery(sql);
                }

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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            fillVariables();
            op = new DMS.DAL.operations();
            Int16 res;
            docExt = drpFormat.SelectedValue;
            docExt = docExt.Substring(docExt.LastIndexOf(".") + 1);
            docName = "Scanned Document";
            docName = docName.Substring(docName.LastIndexOf(@"\") + 1);
            docName = docName.Split('.')[0];
            DMS.BLL.specialCases SP = new DMS.BLL.specialCases();

            res = c.convertToInt16(c.GetMax("version", "dbo.documentVersions", "docID=" + docID.ToString()));
            op = new DMS.DAL.operations();
            op.dboAddDocumentVersions(docID, res, DateTime.Now, c.convertToInt32(Session["userID"]), docExt, c.convertToInt32(drpDocGroupID.SelectedValue), docName, docName);


            string desPath = Helper.GetUploadDiskPath();
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(desPath);

            if (dir.Exists == false)
                dir.Create();

            string fName = desPath + @"\" + docID + "-" + res.ToString() + "." + docExt;
            //fluFile.SaveAs(fName);
            if (System.IO.File.GetLastWriteTime(Helper.GetTempDiskPath() + Session["userID"].ToString() + "." + docExt) >= DateTime.Now.AddMinutes(-1))
            {
                System.IO.File.Copy(Helper.GetTempDiskPath() + Session["userID"].ToString() + "." + docExt, fName);
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

            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            #region old code
            tables.dbo.documentVersions vers = new tables.dbo.documentVersions();
            op = new DMS.DAL.operations();
            vers = op.dboGetAllDocumentVersions("DocID=" + docID.ToString());
            //var documents= ConvertDataTable<documentVersions>(vers);

            if (Session["lang"].ToString() == "1")
            {
                lblDocCount.Text = vers.rowsCount.ToString() + " مرفق متوفر";
            }
            else
            {
                lblDocCount.Text = vers.rowsCount.ToString() + " attachment(s) included";
            }

            List<DocumentVerstionsVW> lstContent = new List<DocumentVerstionsVW>();
            for (Int32 i = 0; i < vers.rowsCount; i++)
            {
                DocumentVerstionsVW documentVerstionsVW = new DocumentVerstionsVW();
                vers.currentIndex = i;
                TableRow TR = new TableRow();
                TableCell TD = new TableCell();
                TD.Text = "<i class=\"fas fa-file-alt\"></i>";
                TR.Cells.Add(TD);
                TD = new TableCell();
                documentVerstionsVW.name = "<b>" + drpDocGroupID.Items.FindByValue(vers.fieldDocGroupID.ToString()).Text + " : </b>";
                TR.Cells.Add(TD);
                TD = new TableCell();
                HyperLink lnk = new HyperLink();
                //Example 5
                string docExt = c.GetDataAsScalar("select top 1 docExt  from  documents where docID=" + docID).ToString();
                if (docExt.ToLower() != "pdf")
                {
                    //lnk.NavigateUrl = "javascript:parent.showDialog(195, '" + txtDocName.Text + "', " + "'../Screen/showDocument.aspx?docID=" + c.encrypt(docID.ToString()) + "&ver=" + vers.fieldVersion.ToString() + @"&'" + ", 1300, 700)";
                    //lnk.Target = "_parent";
                    lnk.Attributes.Add("class", "lnkopenFile");
                    lnk.Attributes.Add("data-url", "javascript:parent.showDialog(195, '" + txtDocName.Text + "', " + "'../Screen/showDocument.aspx?docID=" + c.encrypt(docID.ToString()) + "&ver=" + vers.fieldVersion.ToString() + @"&'" + ", 1300, 700)");
                    lnk.Attributes.Add("data-id", c.encrypt(docID.ToString()));
                    lnk.Attributes.Add("data-verstion", vers.fieldVersion.ToString());
                    lnk.Attributes.Add("data-user", Session["userID"].ToString());
                    lnk.Attributes.Add("data-ext", docExt.ToLower());
                    documentVerstionsVW.url = "javascript:parent.showDialog(195, '" + txtDocName.Text + "', " + "'../Screen/showDocument.aspx?docID=" + c.encrypt(docID.ToString()) + "&ver=" + vers.fieldVersion.ToString() + @"&'" + ", 1300, 700)";
                    documentVerstionsVW.cssclass = "";
                    documentVerstionsVW.id = c.encrypt(docID.ToString());
                    documentVerstionsVW.verstion = vers.fieldVersion.ToString();
                    documentVerstionsVW.user = Session["userID"].ToString();
                    documentVerstionsVW.ext = docExt.ToLower();
                }
                else
                {
                    //lnk.NavigateUrl = "../PdfLauncher.aspx?docID=" + c.encrypt(docID.ToString()) + "&ver=" + vers.fieldVersion.ToString() + "&userID=" + Session["userID"].ToString();
                    //lnk.Target = "_blank";
                    lnk.Attributes.Add("class", "lnkopenFile");
                    lnk.Attributes.Add("data-id", c.encrypt(docID.ToString()));
                    lnk.Attributes.Add("data-verstion", vers.fieldVersion.ToString());
                    lnk.Attributes.Add("data-user", Session["userID"].ToString());
                    lnk.Attributes.Add("data-ext", "pdf");
                    //lnk.Attributes["data-id"] = c.encrypt(docID.ToString());
                    //lnk.Attributes["data-verstion"] = vers.fieldVersion.ToString();
                    //lnk.Attributes["data-user"] = Session["userID"].ToString();
                    documentVerstionsVW.url = "";
                    documentVerstionsVW.cssclass = "lnkopenFile";
                    documentVerstionsVW.id = c.encrypt(docID.ToString());
                    documentVerstionsVW.verstion = vers.fieldVersion.ToString();
                    documentVerstionsVW.user = Session["userID"].ToString();
                    documentVerstionsVW.ext = docExt.ToLower();
                }
                c = new CommonFunction.clsCommon();
                lnk.Text = txtDocName.Text + "-" + vers.fieldVersion.ToString() + "." + vers.fieldExt;
                TD.Controls.Add(lnk);
                TR.Cells.Add(TD);
                //click icon
                TD = new TableCell();
                HyperLink lnk2 = new HyperLink();
                //Example 5
                //string docExt2 = c.GetDataAsScalar("select top 1 docExt  from  documents where docID=" + docID).ToString();
                if (docExt.ToLower() == "pdf")
                {
                    lnk2.NavigateUrl = "";
                    //lnk2.Target = "";
                    //lnk2.Attributes.Add("class", "signOpen");
                    //lnk2.Attributes.Add("data-id", c.encrypt(docID.ToString()+"-"+ vers.fieldVersion.ToString()));
                }
                else
                {
                }
                c = new CommonFunction.clsCommon();
                lnk2.Text = "<i class=\"fas fa-file-signature\"></i>";
                lnk2.ToolTip = (Session["lang"].ToString() == "0") ? "Document has been signed" : "هذا المستند يحتوي على توقيعات";
                TD.Controls.Add(lnk2);
                TR.Cells.Add(TD);
                //tblVersionsnew.DataSource = lstContent;
                //tblVersionsnew.DataBind();

                chkAttachments.Items.Add(new ListItem(vers.fieldDocumentFileName + "." + vers.fieldExt, vers.fieldVersion.ToString()));
            }
            #endregion
            DataTable dt = c.GetDataAsDataTable("SELECT documentVersions.docID, documentVersions.version, documentVersions.addedDate, documentVersions.addedUserID, documentVersions.ext, ISNULL(documentVersions.docGroupID,0)docGroupID,documentVersions.DocumentFileName, documents.docName, ISNULL(documentsGroups.docGroupID,0) AS GroupID, documentsGroups.docGroupTitle, ISNULL(documentsGroups.docTypeID,0) as docTypeID FROM     documentVersions LEFT JOIN documentsGroups ON documentVersions.docGroupID = documentsGroups.docGroupID INNER JOIN documents ON documentVersions.docID = documents.docID where documentVersions.docID= " + docID.ToString());
            List<documentverstionTbl> lstdocumentverstionTbl = new List<documentverstionTbl>();

            //lstdocumentverstionTbl = ConvertDataTable<documentverstionTbl>(dt);
            foreach (DataRow row in dt.Rows)
            {
                documentverstionTbl documentverstionTbl = new documentverstionTbl();
                //documentverstionTbl.addedDate = row.Field<DateTime?>("addedDate");
                documentverstionTbl.addedUserID = Int64.Parse(row["addedUserID"].ToString());
                documentverstionTbl.docGroupID = Int64.Parse(row["docGroupID"].ToString());
                documentverstionTbl.docGroupTitle = row["docGroupTitle"].ToString();
                documentverstionTbl.docID = row["docID"].ToString() != "" ? Int64.Parse(row["docID"].ToString()) : 0;
                documentverstionTbl.docName = row["docName"].ToString();
                documentverstionTbl.docTypeID = Int64.Parse(row["docID"].ToString());
                documentverstionTbl.docIDInc = c.encrypt(documentverstionTbl.docID.ToString());
                string pdfFilePath = Helper.GetUploadDiskPath() + row["docID"].ToString() + "-" + row["version"].ToString() + ".pdf";
                documentverstionTbl.ext = row["ext"].ToString();
                documentverstionTbl.display = documentverstionTbl.ext == "pdf" || File.Exists(pdfFilePath) ? "" : "none";
                documentverstionTbl.GroupID = Int64.Parse(row["GroupID"].ToString());
                documentverstionTbl.version = Int64.Parse(row["version"].ToString());
                documentverstionTbl.DocumentFileName = row["DocumentFileName"].ToString();
                lstdocumentverstionTbl.Add(documentverstionTbl);
            }
            if (lstdocumentverstionTbl.Count > 0)
            {
                var groups = lstdocumentverstionTbl.Select(x => new { x.docGroupID, x.docGroupTitle }).Distinct().ToList();
                // fill groups
                //first fill zero group
                List<DocumentCategoryVW> lstDocumentCategoryVW = new List<DocumentCategoryVW>();
                DocumentCategoryVW documentCategoryVW = new DocumentCategoryVW() { id = 0, groupname = "No Group", groupnamear = "بدون مجموعة" };
                documentCategoryVW.FilesList = new List<documentverstionTbl>();
                documentCategoryVW.FilesList = lstdocumentverstionTbl.Where(x => x.docGroupID == 0).ToList();
                lstDocumentCategoryVW.Add(documentCategoryVW);
                foreach (var item in groups)
                {
                    if (item.docGroupID != 0)
                    {
                        DocumentCategoryVW documentCategory = new DocumentCategoryVW() { id = 0, groupname = item.docGroupTitle, groupnamear = item.docGroupTitle };
                        documentCategory.FilesList = new List<documentverstionTbl>();
                        documentCategory.FilesList = lstdocumentverstionTbl.Where(x => x.docGroupID == item.docGroupID).ToList();
                        lstDocumentCategoryVW.Add(documentCategory);
                    }
                }
                LstCategories.DataSource = lstDocumentCategoryVW;
                LstCategories.DataBind();
                lblgroups.Visible = false;
            }
            else
            {
                lblgroups.Visible = true;
                lblgroups.Text = Session["lang"].ToString() == "0" ? "There are no attachments" : "لا توجد مرفقات";
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

            Response.Redirect("../Screen/documentsList.aspx?fldrID=" + c.encrypt(hdnDocTypID.Value));
        }

        //private void fillDrpRecipientID()
        //{
        //    op = new DMS.DAL.operations();
        //    DataTable dt = new DataTable();
        //    string valueF = "";
        //    string textF = "";
        //    switch (drpRecipientType.SelectedValue)
        //    {
        //        case "1":
        //            tables.dbo.users usersTB = new tables.dbo.users();
        //            usersTB = op.dboGetAllUsers();
        //            dt = usersTB.table;
        //            valueF = "userID";
        //            textF = "FullName";
        //            break;
        //        case "2":
        //            tables.dbo.groups grpTB = new tables.dbo.groups();
        //            grpTB = op.dboGetAllGroups();
        //            dt = grpTB.table;
        //            valueF = "grpID";
        //            textF = "grpDesc";
        //            break;
        //        case "3":
        //            tables.dbo.positions positionsTB = new tables.dbo.positions();
        //            positionsTB = op.dboGetAllPositions();
        //            dt = positionsTB.table;
        //            valueF = "positionID";
        //            if (Session["lang"].ToString() == "0")
        //                textF = "positionTitle";
        //            else
        //                textF = "positionTitleAr";
        //            break;
        //        case "4":
        //            tables.dbo.departments departmentsTB = new tables.dbo.departments();
        //            departmentsTB = op.dboGetAllDepartments();
        //            dt = departmentsTB.table;
        //            valueF = "departmentID";
        //            if (Session["lang"].ToString() == "0")
        //                textF = "departmentName";
        //            else
        //                textF = "departmentNameAr";
        //            break;
        //    }

        //    c.FillDropDownList(drpRecipientID, dt, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", valueF, textF);
        //}

        //protected void drpRecipientType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    fillDrpRecipientID();
        //}

        //protected void btnSendEmail_Click(object sender, EventArgs e)
        //{
        //    DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
        //    string errorMessage = "";
        //    op = new DMS.DAL.operations();
        //    tables.dbo.settings settings = op.dboGetSettingsByPrimaryKey(1);

        //    op = new DMS.DAL.operations();
        //    tables.dbo.users users = new tables.dbo.users();

        //    switch (drpRecipientType.SelectedValue)
        //    {
        //        case "1":
        //            users = op.dboGetUsersByPrimaryKey(c.convertToInt32(drpRecipientID.SelectedValue));
        //            break;
        //        case "2":
        //            users = op.dboGetAllUsers("grpID=" + drpRecipientID.SelectedValue);
        //            break;
        //        case "3":
        //            users = op.dboGetAllUsers("positionID=" + drpRecipientID.SelectedValue);
        //            break;
        //        case "4":
        //            users = op.dboGetAllUsers("departmentID=" + drpRecipientID.SelectedValue);
        //            break;
        //    }

        //    try
        //    {
        //        if (!users.hasRows)
        //        {
        //            if (Session["lang"].ToString() == "0")
        //                errorMessage = "Please select a recipient";
        //            else
        //                errorMessage = "الرجاء اختيار مستلم";
        //        }
        //        System.Net.Mail.SmtpClient M = new System.Net.Mail.SmtpClient(settings.fieldOutgoingMailServer);
        //        System.Net.Mail.MailMessage MailMsg;
        //        //set end date

        //        if (drpRecipientType.SelectedValue == "1")
        //        {
        //            if (users.fieldEmail == "")
        //            {
        //                if (Session["lang"].ToString() == "0")
        //                    errorMessage = "Selected user doesn't have an Email";
        //                else
        //                    errorMessage = "المستخدم المختار ليس لديه بريد إلكتروني";
        //            }
        //            MailMsg = new System.Net.Mail.MailMessage(settings.fieldSystemEmail, users.fieldEmail);
        //        }
        //        else
        //        {
        //            Int32 rec = 0;
        //            MailMsg = new System.Net.Mail.MailMessage(settings.fieldSystemEmail, settings.fieldSystemEmail);

        //            for (rec = 0; rec < users.rowsCount; rec++)
        //            {
        //                users.currentIndex = rec;
        //                if (users.fieldEmail != "")
        //                {
        //                    DateTime? EndDocumntDate = null;
        //                    if (txtenddateCount.Value != "")
        //                    {
        //                        EndDocumntDate = DateTime.Now;
        //                        EndDocumntDate.Value.AddDays(int.Parse(txtenddateCount.Value));
        //                    }
        //                    op = new DMS.DAL.operations();
        //                    op.dboAddUserDocuments(users.fieldUserID, docID, true, true, true, true);
        //                    sp.dboAddDocumentWFPath(docID, users.fieldUserID, DateTime.MaxValue.AddDays(-1), 0, 1, 0, 1, "", DateTime.Now, EndDocumntDate);
        //                    if (rdoSendType.SelectedValue == "cc")
        //                        MailMsg.CC.Add(users.fieldEmail);
        //                    else
        //                        MailMsg.Bcc.Add(users.fieldEmail);
        //                }
        //            }
        //        }

        //        if (errorMessage == "")
        //        {

        //            M.Credentials = new System.Net.NetworkCredential(settings.fieldSystemEmail, c.decrypt(settings.fieldSystemEmailPassword));
        //            string siteURL = Request.Url.AbsoluteUri;
        //            siteURL = siteURL.Remove(siteURL.LastIndexOf('/'));
        //            siteURL = siteURL.Remove(siteURL.LastIndexOf('/'));
        //            string docLink = siteURL + "/Screen/showDocument.aspx?docID=" + c.encrypt(docID.ToString()) + "&ver=1" + @"&'";
        //            MailMsg.BodyEncoding = System.Text.UTF8Encoding.UTF8;
        //            MailMsg.Subject = Session["FullName"].ToString() + " sends you a document";
        //            MailMsg.Body = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'><html><body dir='rtl' style='font-family:Arial'>";
        //            MailMsg.Body += "<div style='vertical-align:middle; background-color:none; padding:10px;font-size:12pt;'>";
        //            MailMsg.Body += txtMailBody.Text;
        //            MailMsg.Body += "<br/><br/>Document Link : <a href='" + docLink + "'>" + docLink + "</a>";
        //            MailMsg.Body += "<br/><br/><hr/><br/>" + settings.fieldSystemEmailSignature;
        //            MailMsg.Body += "</body></html>";
        //            MailMsg.IsBodyHtml = true;
        //            M.Send(MailMsg);
        //            //Response.Write(URL)
        //            //formtable.Visible = False
        //            //complate.Visible = True
        //            if (Session["lang"].ToString() == "0")
        //                msglbl.Text = "Your message has been sent ...";
        //            else
        //                msglbl.Text = "تم ارسال رسالتك بنجاح ...";
        //        }
        //        else
        //        {
        //            msglbl.Text = errorMessage;
        //        }

        //        //SendBtn.Enabled = True
        //    }
        //    catch (Exception ex)
        //    {
        //        //Response.Write(ex.Message)
        //        //formtable.Visible = False
        //        //complate.Visible = True
        //        msglbl.Text = ex.Message;
        //        if (Session["lang"].ToString() == "0")
        //            msglbl.Text = "Error while sending ... <br/> Please try again after a while  ";
        //        else
        //            msglbl.Text = " خطأ أثناء الإرسال ... <br/> يرجى المحاولة بعد قليل  ";
        //    }
        //}

        protected void lnkDownload_Click(object sender, EventArgs e)
        {

            string filePath = Helper.GetUploadDiskPath() + txtDocID.Text + "-1." + hdnDocExt.Value;
            FileInfo file = new FileInfo(filePath);

            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "text/plain";
            Response.Flush();
            Response.TransmitFile(file.FullName);
            Response.End();
        }

        protected void lnkSaveRemider_Click(object sender, EventArgs e)
        {
            Int32 metaID = Convert.ToInt32(hdnReminderMetaID.Value);
            Int32 reminderPeriod = Convert.ToInt32(txtReminderPeriod.Text);
            Int32 userID = Convert.ToInt32(Session["userID"]);

            c.NonQuery("Insert into usersRemiders values(" + userID.ToString() + "," + metaID.ToString() + "," + docID.ToString() + "," + reminderPeriod.ToString() + ")");
            showData();
        }

        protected void lnkDownlod_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            string ca = btn.CommandArgument;
            string downloadFile = Helper.GetUploadDiskPath() + ca;
            HttpContext context = HttpContext.Current;
            context.Response.Buffer = true;
            context.Response.Clear();
            context.Response.AddHeader(
               "content-disposition",
               "attachement; filename=" + downloadFile);
            string contentType = "application/unknown";
            string ext = downloadFile.Split('.')[1].ToLower();
            switch (ext)
            {
                case "jpg":
                    contentType = "image/jpeg";
                    break;
                case "tif":
                    contentType = "image/tiff";
                    break;
                case "avi":
                    contentType = "video/avi";
                    break;
                case "mp4":
                    contentType = "video/mpeg";
                    break;
                case "pdf":
                    contentType = "application/pdf";
                    break;
                case "docx":
                    contentType = "Content-type: application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case "xlsx":
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

            }
            context.Response.ContentType = contentType;
            context.Response.WriteFile(
                downloadFile);
        }

        protected void btnback_ServerClick(object sender, EventArgs e)
        {

        }

        protected void lnkRefreshSign_Click(object sender, EventArgs e)
        {
            FillAllVersionSignture();
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            DMS.BLL.specialCases.SaveInOutSerial(int.Parse(txtDocID.Text), 1);
            lnkExport.Visible = false;
            Response.Write("<script>alert('تم تصدير الملف');</script>");

            showData();
        }

        protected void lnkSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                //string _location = "";
                //switch (Location)
                //{
                //    case location.DocumentInfo:
                //        _location = "docID";
                //        break;
                //    case location.Inbox:
                //        _location = "mailID";
                //        break;
                //}

                CommonFunction.clsCommon c = new CommonFunction.clsCommon();

                DMS.DAL.operations op = new DMS.DAL.operations();
                tables.dbo.settings settings = op.dboGetSettingsByPrimaryKey(1);

                System.Net.Mail.SmtpClient M = new System.Net.Mail.SmtpClient(settings.fieldOutgoingMailServer);
                M.EnableSsl = Convert.ToBoolean(dms.sysSettings.getSettingValue("MailSSL"));
                System.Net.Mail.MailMessage MailMsg;
                MailMsg = new System.Net.Mail.MailMessage(settings.fieldWorkflowEmail, txtToEmails.Text);
                M.Credentials = new System.Net.NetworkCredential(settings.fieldWorkflowEmail, c.decrypt(settings.fieldWorkflowEmailPassword));
                M.Port = Convert.ToInt32(dms.sysSettings.getSettingValue("MailPort"));
                MailMsg.BodyEncoding = System.Text.UTF8Encoding.UTF8;
                MailMsg.Subject = txtSubject.Text;
                MailMsg.Body = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'><html><body style='font-family:Arial'>";
                MailMsg.Body += "<div style='vertical-align:middle; background-color:none; padding:10px;font-size:12pt;'>";
                MailMsg.Body += txtBody.Text;
                MailMsg.Body += "</body></html>";
                MailMsg.IsBodyHtml = true;

                foreach (ListItem li in chkAttachments.Items)
                {
                    if (li.Selected)
                    {
                        string desPath = Helper.GetUploadDiskPath();
                        string attExt = li.Text.Substring(li.Text.LastIndexOf('.') + 1);
                        string attPath = desPath + @"\" + docID + "-" + li.Value + "." + attExt;
                        MailMsg.Attachments.Add(new System.Net.Mail.Attachment(attPath));
                    }
                }

                M.Send(MailMsg);

                //return true;
            }
            catch
            {
                //return false;
            }
        }

        protected void lnkForward_Click(object sender, EventArgs e)
        {
            DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
            Int16 sequance = 1;
            string sql = "select ISNULL(max(wfseqno),0) from DocumentWFPath where docID=" + docID.ToString();
            sequance = Convert.ToInt16(c.GetDataAsScalar(sql));
            sequance += 1;

            Int32 userID = c.convertToInt32(Session["userID"]);
            op = new DMS.DAL.operations();
            sp.dboAddDocumentWFPath(docID, userID, DateTime.Now,
                0, sequance, 1,1,txtNotes.Text, DateTime.Now, DateTime.Now,0);
            sequance += 1;

            sp.dboAddDocumentWFPath(docID, Convert.ToInt32(drpUsers.SelectedValue), DateTime.MaxValue.AddDays(-1),
                0, sequance, 0, 1, "", DateTime.Now, DateTime.Now, 0);

            c.NonQuery("update documents set statusId=1 where docid=" + docID.ToString());
            //sp.closeDocWF(docID);

            try
            {
                string userEmail = c.GetDataAsScalar("select Email from users where userID=" + drpUsers.SelectedValue.ToString()).ToString();
                if (!String.IsNullOrEmpty(userEmail))
                    mailer.sendMail(userEmail, Request.Url.AbsoluteUri, docID, mailer.location.Inbox);

                op = new DMS.DAL.operations();
                op.dboAddUserDocuments(Convert.ToInt32(drpUsers.SelectedValue), docID, true, true, true, false);
            }
            catch
            { }

            fillWorkflow();
            showData();
        }
    }

    internal class DocumentVerstionsVW
    {
        public string id { get; set; }
        public string name { get; set; }
        public int docid { get; set; }
        public string version { get; set; }
        public string url { get; set; }
        public string cssclass { get; set; }
        public string verstion { get; set; }
        public string user { get; set; }
        public string ext { get; set; }
    }

    public class UsersList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isRead { get; set; }
        public bool isEdit { get; set; }
    }
    internal class DocumentCategoryVW
    {
        public int id { get; set; }
        public string groupname { get; set; }
        public string groupnamear { get; set; }
        public List<documentverstionTbl> FilesList { get; set; }
        //public string version { get; set; }
        //public string url { get; set; }
        //public string cssclass { get; set; }
        //public string verstion { get; set; }
        //public string user { get; set; }
        //public string ext { get; set; }
    }
    public class documentverstionTbl
    {
        public System.Int64 docID { get; set; }
        public string docIDInc { get; set; }
        public System.Int64 version { get; set; }
        public DateTime? addedDate { get; set; }
        public System.Int64 addedUserID { get; set; }
        public string ext { get; set; }
        public System.Int64 docGroupID { get; set; }
        public string docName { get; set; }
        public System.Int64 GroupID { get; set; }
        public string docGroupTitle { get; set; }
        public string display { get; set; }
        public System.Int64 docTypeID { get; set; }
        public string DocumentFileName { get; set; }
        
    }
}