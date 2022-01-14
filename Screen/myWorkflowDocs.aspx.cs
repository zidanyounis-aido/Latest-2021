using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using dms.VM;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using IronBarCode;

namespace dms.Screen
{
    public partial class myWorkflowDocs : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        DMS.BLL.specialCases sp = new DMS.BLL.specialCases();

        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        public int indexId { get; set; }
        Int32 docTypID; string docName; string docExt; DateTime addedDate;
        Int32 addedUserID; Int16 lastVersion; DateTime modifyDate; Int32 modifyUserID;
        Int32 fldrID; string ocrContent; Int64 docID;
        tables.dbo.documents docTB = new tables.dbo.documents();
        Int64 folderSeq; Int64 docTypeSeq; Int64 folderDocTypeSeq; DateTime endDate;

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
        }

        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                grdMyDocs.Columns[0].HeaderText = "العنوان";

                drpAction.Items[0].Text = "لا إجراء";
                drpAction.Items[1].Text = "موافق";
                drpAction.Items[2].Text = "غير موافق";
                drpAction.Items[3].Text = "موافق بشروط";
                drpAction.Items[4].Text = "مداولة";
                drpAction.Items[5].Text = "تحويل";
                drpAction.Items[6].Text = "اطلعت";
                drpAction.Items[7].Text = "موافق وفق الضوابط";
                drpAction.Items[8].Text = "اجراء اللازم وفق الضوابط";


                btnCloseWindow.Text = "تم";
                lnkNextMulti.Text = "عدة مستخدمين";

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            //get selected index
            if (Request.QueryString["indexId"] != null)
            {
                try
                {
                    indexId = int.Parse(Request.QueryString["indexId"]);
                }
                catch (Exception)
                {

                    indexId = 0;
                }

            }
            else
            {
                indexId = 0;
            }
            if (!IsPostBack)
            {
                //Page.Header.DataBind();
                fillGrid();
                DMS.BLL.specialCases bll = new DMS.BLL.specialCases();
                DataTable wfUsers = new DataTable();
                wfUsers = bll.getWorkflowUsers();
                c.FillDropDownList(drpNext, wfUsers);
                //drpNext.Items.Remove(Session["userID"].ToString());
                ListItem itm = drpNext.Items.FindByValue(Session["userID"].ToString());
                if (itm != null)
                {
                    drpNext.Items.Remove(itm);
                }
                chkUsers.DataTextField = "userFullName";
                chkUsers.DataValueField = "userID";
                chkUsers.DataSource = wfUsers;
                chkUsers.DataBind();

                if (Session["lang"].ToString() == "1")
                {
                    foreach (ListItem li in drpNext.Items)
                    {
                        li.Text = li.Text.Replace("Department", "قسم");
                        li.Text = li.Text.Replace("Section", "شعبة");

                    }
                }

                tables.dbo.users userTbl = new tables.dbo.users();
                userTbl = new tables.dbo.users();
                op = new DMS.DAL.operations();
                userTbl = op.dboGetUsersByPrimaryKey(c.convertToInt32(Session["userID"]));
                if (!userTbl.fieldAllowCustomWF)
                {
                    drpNext.Visible = false;
                    lblNextMsg.Text = "Can't Choose custom workflow";
                    if (Session["lang"].ToString() == "1")
                        lblNextMsg.Text = "لا يمكنك إختيار مسار عمل مخصص";

                    lblNextMsg.Visible = true;
                }

                if (Session["userID"].ToString() == "8" || Session["userID"].ToString() == "12")
                {
                    drpAction.Items[drpAction.Items.Count - 1].Enabled = true;
                    drpAction.Items[drpAction.Items.Count - 1].Selected = true;
                }
                else
                {
                    drpAction.Items[drpAction.Items.Count - 1].Enabled = false;
                    drpAction.Items[1].Selected = true;
                }
            }

        }

        public void fillGrid(bool show = true)
        {
            string durationTypeQuery = ", ISNULL((SELECT durationType FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as durationType";
            string durationQuery = ",ISNULL((SELECT duration FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as duration";
            string sql = "SELECT     TOP (100) PERCENT dbo.documentWFPath.ID,dbo.docTypes.docTypDesc,dbo.docTypes.docTypDescAr, dbo.documents.docName, dbo.documentWFPath.receiveDate,dbo.documentWFPath.EndDate,dbo.documentWFPath.docID," +
                "CASE WHEN (dbo.documentWFPath.EndDate IS NOT NULL and dbo.documentWFPath.EndDate > GETDATE()) THEN 'black' ELSE CASE WHEN dbo.documentWFPath.EndDate IS NOT NULL THEN 'red' ELSE 'black' END END as 'Color'" +
                 durationTypeQuery +
                 durationQuery +
                ",documents.submitDate" +
                 ",documents.addedDate" +
                 ",documents.statusId" +
                " FROM dbo.documents INNER JOIN dbo.documentWFPath ON dbo.documents.docID = dbo.documentWFPath.docID INNER JOIN " +
                " dbo.docTypes ON dbo.documents.docTypID = dbo.docTypes.docTypID " +
                " WHERE     (dbo.documentWFPath.userID = " + Session["userID"].ToString() + ") AND (dbo.documentWFPath.actionType = 0) " +
                " ORDER BY dbo.documentWFPath.receiveDate DESC";
            DataTable DT = new DataTable();
            DT = c.GetDataAsDataTable(sql);
            //fill inbox data
            List<InboxVM> inboxVM = new List<InboxVM>();
            foreach (DataRow row in DT.Rows)
            {
                if (row["statusId"].ToString() != "2")
                {
                    InboxVM obj = new InboxVM();
                    obj.ID = int.Parse(row["ID"].ToString());
                    obj.Color = row["Color"].ToString();
                    obj.docTypDesc = row["docTypDesc"].ToString();
                    obj.docTypDescAr = row["docTypDescAr"].ToString();
                    obj.receiveDate = DateTime.Parse(row["receiveDate"].ToString());
                    obj.addedDate = row["addedDate"].ToString() != "" ? DateTime.Parse(row["addedDate"].ToString()) : DateTime.Now;
                    obj.Color = row["Color"] != null ? row["Color"].ToString() : "";
                    obj.docID = int.Parse(row["docID"].ToString());
                    obj.docName = row["docName"] != null ? row["docName"].ToString() : "";
                    obj.submitDate = row["submitDate"].ToString() != "" ? DateTime.Parse(row["submitDate"].ToString()) : DateTime.Parse(row["addedDate"].ToString());
                    obj.durationType = int.Parse(row["durationType"].ToString());
                    obj.duration = int.Parse(row["duration"].ToString());
                    obj.isDelay = 0;
                    if (obj.durationType != -1 && obj.duration != -1 && obj.duration > 0)
                    {

                        int durationMuliplie = obj.durationType == 1 ? 1 : 24;
                        var totalHours = (obj.submitDate.Value.AddHours(obj.duration * durationMuliplie) - DateTime.Now).TotalHours;
                        if (totalHours > 0)
                        {
                            string h = Session["lang"].ToString() == "0" ? "Hour" : "ساعة";
                            string remainTime = Session["lang"].ToString() == "0" ? "the remaining time" : "الوقت المتبقي";
                            obj.LeftTime = remainTime + ": " + Math.Round(totalHours, 1).ToString() + h;
                            if (obj.durationType == 2)
                            {
                                h = Session["lang"].ToString() == "0" ? "Day" : "يوم";
                                obj.LeftTime = Math.Round((totalHours / 24), 1).ToString() + h;
                            }
                            if (obj.durationType == 1 && totalHours < 1 && totalHours > 0)
                            {
                                h = Session["lang"].ToString() == "0" ? "Minute" : "دقيقة";
                                obj.LeftTime = remainTime + ": " + Math.Round((totalHours * 60), 1).ToString() + h;
                            }
                        }
                        else
                        {
                            if (row["statusId"].ToString() != "2")
                            {
                                string h = Session["lang"].ToString() == "0" ? "Hour" : "ساعة";
                                string remainTime = Session["lang"].ToString() == "0" ? "the remaining time" : "الوقت المتبقي";
                                obj.LeftTime = remainTime + ": " + "0 " + h;
                                obj.statusName = Session["lang"].ToString() == "0" ? "Late" : "متأخر";
                                if (obj.durationType == 2)
                                {
                                    h = Session["lang"].ToString() == "0" ? "Day" : "يوم";
                                    obj.LeftTime = remainTime + ": " + "0 " + h;
                                }
                                //CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                                //c.NonQuery("update dbo.documents set statusId=3 where dbo.documents.docID=" + obj.docID);
                            }
                        }
                    }
                    else
                    {
                        string remainTime = Session["lang"].ToString() == "0" ? "the remaining time" : "الوقت المتبقي";
                        obj.LeftTime = remainTime + ": " + "∞";
                    }
                    inboxVM.Add(obj);
                }
                //TextBox1.Text = row["ImagePath"].ToString();
            }
            string durationTypeQueryDelay = ", ISNULL((SELECT durationType FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPathDelayed.wfPathID from dbo.documentWFPath where dbo.documentWFPathDelayed.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPathDelayed.wfSeqNo from dbo.documentWFPath where dbo.documentWFPathDelayed.docID=documents.docID  and dbo.documentWFPathDelayed.actionType=0 )),-1) as durationType";
            string durationQueryDelay = ",ISNULL((SELECT duration FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPathDelayed.wfPathID from dbo.documentWFPath where dbo.documentWFPathDelayed.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPathDelayed.wfSeqNo from dbo.documentWFPath where dbo.documentWFPathDelayed.docID=documents.docID  and dbo.documentWFPathDelayed.actionType=0 )),-1) as duration";
            //get delay documents
            string sqlDelay = "SELECT     TOP (100) PERCENT dbo.documentWFPathDelayed.ID,dbo.docTypes.docTypDesc,dbo.docTypes.docTypDescAr, dbo.documents.docName, dbo.documentWFPathDelayed.receiveDate,dbo.documentWFPathDelayed.EndDate,dbo.documentWFPathDelayed.docID,CASE WHEN (dbo.documentWFPathDelayed.EndDate IS NOT NULL and dbo.documentWFPathDelayed.EndDate > GETDATE()) THEN 'black' ELSE CASE WHEN dbo.documentWFPathDelayed.EndDate IS NOT NULL THEN 'red' ELSE 'black' END END as 'Color'" +
    durationTypeQueryDelay +
    durationQueryDelay +
   ",documents.submitDate" +
    ",documents.addedDate" +
    ",documents.statusId" +
   " FROM dbo.documents INNER JOIN dbo.documentWFPathDelayed ON dbo.documents.docID = dbo.documentWFPathDelayed.docID INNER JOIN  dbo.docTypes ON dbo.documents.docTypID = dbo.docTypes.docTypID" +
   " WHERE     (dbo.documentWFPathDelayed.userID = " + Session["userID"].ToString() + ") AND (dbo.documentWFPathDelayed.actionType = 0) " +
   " ORDER BY dbo.documentWFPathDelayed.receiveDate DESC";
            DataTable DTDelay = new DataTable();
            DTDelay = c.GetDataAsDataTable(sqlDelay);
            foreach (DataRow row in DTDelay.Rows)
            {
                if (row["statusId"].ToString() != "2")
                {
                    InboxVM obj = new InboxVM();
                    obj.ID = int.Parse(row["ID"].ToString());
                    obj.Color = row["Color"].ToString();
                    obj.docTypDesc = "Late document";
                    obj.docTypDescAr = "مستند متأخر";
                    obj.receiveDate = DateTime.Parse(row["receiveDate"].ToString());
                    obj.addedDate = row["addedDate"].ToString() != "" ? DateTime.Parse(row["addedDate"].ToString()) : DateTime.Now;
                    obj.Color = row["Color"] != null ? row["Color"].ToString() : "";
                    obj.docID = int.Parse(row["docID"].ToString());
                    obj.docName = row["docName"] != null ? row["docName"].ToString() : "";
                    obj.submitDate = row["submitDate"].ToString() != "" ? DateTime.Parse(row["submitDate"].ToString()) : DateTime.Parse(row["addedDate"].ToString());
                    obj.durationType = int.Parse(row["durationType"].ToString());
                    obj.duration = int.Parse(row["duration"].ToString());
                    obj.isDelay = 1;
                    if (obj.durationType != -1 && obj.duration != -1 && obj.duration > 0)
                    {
                        int durationMuliplie = obj.durationType == 1 ? 1 : 24;
                        var totalHours = (obj.submitDate.Value.AddHours(obj.duration * durationMuliplie) - DateTime.Now).TotalHours;
                        if (totalHours > 0)
                        {
                            string h = Session["lang"].ToString() == "0" ? "Hour" : "ساعة";
                            string remainTime = Session["lang"].ToString() == "0" ? "the remaining time" : "الوقت المتبقي";
                            obj.LeftTime = remainTime + ": " + Math.Round(totalHours, 1).ToString() + h;
                            if (obj.durationType == 2)
                            {
                                h = Session["lang"].ToString() == "0" ? "Day" : "يوم";
                                obj.LeftTime = remainTime + ": " + Math.Round((totalHours / 24), 1).ToString() + h;
                            }
                            if (obj.durationType == 1 && totalHours < 1 && totalHours > 0)
                            {
                                h = Session["lang"].ToString() == "0" ? "Minute" : "دقيقة";
                                obj.LeftTime = remainTime + ": " + Math.Round((totalHours * 60), 1).ToString() + h;
                            }
                        }
                        else
                        {
                            if (row["statusId"].ToString() != "2")
                            {
                                string h = Session["lang"].ToString() == "0" ? "Hour" : "ساعة";
                                string remainTime = Session["lang"].ToString() == "0" ? "the remaining time" : "الوقت المتبقي";
                                obj.LeftTime = remainTime + ": " + "0 " + h;
                                obj.statusName = Session["lang"].ToString() == "0" ? "Late" : "متأخر";
                                if (obj.durationType == 2)
                                {
                                    h = Session["lang"].ToString() == "0" ? "Day" : "يوم";
                                    //obj.LeftTime = Math.Round((totalHours / 24), 1).ToString() + h;
                                    obj.LeftTime = remainTime + ": " + "0 " + h;
                                }
                                //CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                                //c.NonQuery("update dbo.documents set statusId=3 where dbo.documents.docID=" + obj.docID);
                            }
                        }
                    }
                    else
                    {
                        string remainTime = Session["lang"].ToString() == "0" ? "the remaining time" : "الوقت المتبقي";
                        obj.LeftTime = remainTime + ": " + "∞";
                    }
                    inboxVM.Add(obj);
                }

                //TextBox1.Text = row["ImagePath"].ToString();
            }
            inboxVM = inboxVM.OrderByDescending(x => x.receiveDate).ToList();
            //bind inbox data
            grdMyDocs.DataSource = inboxVM;
            grdMyDocs.DataBind();
            dlgrdMyDocs.DataSource = inboxVM;
            dlgrdMyDocs.DataBind();
            if (DT.Rows.Count == 0)
            {
                pnlEmpty.Visible = true;
                docDetails.Visible = false;
                divInbox.Visible = false;
            }
            else
            {
                if (indexId != 0)
                {

                    int indexValue = inboxVM.FindIndex(a => a.ID == indexId);
                    var docItem = inboxVM.FirstOrDefault(x => x.ID == indexId);
                    grdMyDocs.SelectedIndex = indexValue;
                    ListViewItem item = (ListViewItem)dlgrdMyDocs.Items[indexValue];
                    HiddenField hdnx = new HiddenField();
                    hdnx = (HiddenField)grdMyDocs.SelectedRow.Cells[2].FindControl("hdnIsDelay");
                    showDoc(int.Parse(hdnx.Value), docItem.docID.ToString(), indexId.ToString());
                    ClearAllClasses();
                    var ctrl = (HtmlContainerControl)item.FindControl("liItem");
                    ctrl.Attributes["class"] = "active";
                    //grdMyDocs.
                    //HiddenField hdn = new HiddenField();
                    //hdn = (HiddenField)grdMyDocs.SelectedRow.Cells[2].FindControl("hdnIsDelay");
                    //showDoc(indexId);
                }
                else
                {
                    if (grdMyDocs.Rows.Count > 0 && show)
                    {
                        grdMyDocs.SelectedIndex = 0;
                        HiddenField hdnx = new HiddenField();
                        hdnx = (HiddenField)grdMyDocs.SelectedRow.Cells[2].FindControl("hdnIsDelay");
                        showDoc(int.Parse(hdnx.Value));
                    }
                }

            }

        }

        protected void grdMyDocs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdMyDocs.SelectedIndex > -1)
                {
                    HiddenField hdn = new HiddenField();
                    hdn = (HiddenField)grdMyDocs.SelectedRow.Cells[2].FindControl("hdnIsDelay");
                    showDoc(int.Parse(hdn.Value));
                    fillGrid(false);
                }
            }
            catch (Exception ex)
            {
                lblDefaultNext.Text = ex.ToString();
            }
        }

        private void showDoc(int isDelay = 0, string documentId = "", string hdnWFID = "")
        {

            for (Int32 m = 0; m < chkUsers.Items.Count; m++)
            {
                chkUsers.Items[m].Selected = false;
            }
            string dc = documentId;
            if (Request.QueryString["docID"] != null && Request.QueryString["docID"] != "")
                dc = Request.QueryString["docID"];
            HiddenField hdn = new HiddenField();
            if (dc == "")
            {
                var ctrl = (HtmlContainerControl)dlgrdMyDocs.Items[0].FindControl("liItem");
                ctrl.Attributes["class"] = "active";

                hdn = (HiddenField)dlgrdMyDocs.Items[0].FindControl("hdnDocID");
                docID = c.convertToInt64(hdn.Value);
            }
            else
            {
                docID = c.convertToInt64(dc);
            }
            //TabContainer1.Visible = true;

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

            showData();
            fillWorkflow();

            op = new DMS.DAL.operations();
            if (hdnWFID == "")
            {
                hdn = new HiddenField();
                hdn = (HiddenField)(HiddenField)dlgrdMyDocs.Items[0].FindControl("hdnWFID");
                hdnWFID = hdn.Value;
            }
            //hdn=
            tables.dbo.documentWFPath docWF = new tables.dbo.documentWFPath();
            op = new DMS.DAL.operations();
            docWF = op.dboGetDocumentWFPathByPrimaryKey(c.convertToInt32(hdnWFID));
            if (docWF.fieldWfSeqNo == 1) // if first step hide return document button 
            {
                LinkButton2.Visible = false;
            }
            else
            {
                LinkButton2.Visible = true;
            }
            //set actio date
            //txtActionEndDate.Text = docWF.fieldEndDate.ToString("dd/MM/yyyy hh:mm tt");

            tables.dbo.wfPathDetails details = new tables.dbo.wfPathDetails();
            op = new DMS.DAL.operations();
            details = op.dboGetAllWfPathDetails("pathID = " + docWF.fieldWfPathID.ToString() + " and seqNo>" + docWF.fieldWfSeqNo.ToString());

            op = new DMS.DAL.operations();
            hdn = new HiddenField();
            hdn = (HiddenField)grdMyDocs.SelectedRow.Cells[2].FindControl("hdnWFID");

            docWF = new tables.dbo.documentWFPath();
            op = new DMS.DAL.operations();
            docWF = op.dboGetDocumentWFPathByPrimaryKey(c.convertToInt32(hdn.Value));

            details = new tables.dbo.wfPathDetails();
            op = new DMS.DAL.operations();
            details = op.dboGetAllWfPathDetails("pathID = " + docWF.fieldWfPathID.ToString() + " and seqNo>" + docWF.fieldWfSeqNo.ToString());
            //op = new DMS.DAL.operations();
            //sp.dboUpdateDocumentWFPathByPrimaryKey(docWF.fieldID, docWF.fieldDocID, docWF.fieldUserID, DateTime.Now, docWF.fieldWfPathID,
            //docWF.fieldWfSeqNo, 1, docWF.fieldRecipientType, txtNotes.Text, docWF.fieldReceiveDate);
            //DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
            //sp.closeDocWF(docID);
            //fillGrid();
            lblDefaultNext.Text = " - ";
            if (details.hasRows)
            {
                if (details.fieldPathID != 0)
                {
                    tables.dbo.users userTbl = new tables.dbo.users();
                    op = new DMS.DAL.operations();
                    userTbl = op.dboGetUsersByPrimaryKey(details.fieldRecipientID);
                    lblDefaultNext.Text = userTbl.fieldFullName;
                }
            }
            string statusId = c.GetDataAsScalar("select top 1 statusId from dbo.documents where docID=" + docID).ToString();
            if (statusId == "2")
            {
                LinkButton1.Visible = false;
                LinkButton2.Visible = false;
                LinkArchiveNotFinshed.Visible = false;
            }
            else
            {
                LinkButton1.Visible = true;
                //LinkButton2.Visible = false;
                LinkArchiveNotFinshed.Visible = true;

            }
            if (isDelay == 1)
            {
                LinkButton1.Visible = false;
                LinkButton2.Visible = false;
                LinkArchiveNotFinshed.Visible = false;
            }
            int typeId = int.Parse(c.GetDataAsScalar("select top 1 ISNULL(typeId,0) from dbo.documents where docID=" + docID).ToString());
            if (typeId == 0)
            {
                linkExport.Visible = true;
            }
        }

        private void showData()
        {
            op = new DMS.DAL.operations();
            docTB = op.dboGetDocumentsByPrimaryKey(docID);
            fillData(docTB.table);
        }

        protected void drpDocTypID_SelectedIndexChanged(object sender, EventArgs e)
        {
            showDocTypeMetas();
        }

        public void showDocTypeMetas() 
        {
            Int32 docTypeID = Convert.ToInt32(drpDocTypID.SelectedValue);
            DMS.ReadFormGenerator f = new DMS.ReadFormGenerator();
            f.FormMetaManager(docTypeID, docID, int.Parse(Session["userID"].ToString()), !(Session["lang"].ToString() == "0"));
            StringBuilder autoScript = new StringBuilder();
            //int docID = c.convertToInt32(c.decrypt(Request.QueryString["docID"]));
            int folderID = int.Parse(c.GetDataAsScalar("select top 1 fldrID from documents where docID=" + docID).ToString());
            f.GetPanal(ref tblDocMetas, ref autoScript, folderID, int.Parse(Session["userID"].ToString()), false);
        }

        private void fillVersions()
        {
            tables.dbo.documentVersions vers = new tables.dbo.documentVersions();
            op = new DMS.DAL.operations();
            DataTable dt = c.GetDataAsDataTable("SELECT documentVersions.docID, documentVersions.version, documentVersions.addedDate, documentVersions.addedUserID, documentVersions.ext, ISNULL(documentVersions.docGroupID,0)docGroupID, documents.docName, ISNULL(documentsGroups.docGroupID,0) AS GroupID, documentsGroups.docGroupTitle, ISNULL(documentsGroups.docTypeID,0) as docTypeID FROM     documentVersions LEFT JOIN documentsGroups ON documentVersions.docGroupID = documentsGroups.docGroupID INNER JOIN documents ON documentVersions.docID = documents.docID where documentVersions.docID= " + docID.ToString());
            //vers = op.dboGetAllDocumentVersions("DocID=" + docID.ToString());
            //imgDoc.Visible = false;
            //if (c.getFileType(vers.fieldExt) == CommonFunction.clsCommon.fileType.Image)
            //{
            //    string imgName = "../Validation.ashx?file=~/Uploads/" + vers.fieldDocID.ToString() + "-1." + vers.fieldExt;
            //    imgDoc.Visible = true;
            //    imgDoc.ImageUrl = imgName;
            //}
            //else
            //{
            //    imgDoc.Visible = false;
            //}
            tblVersions.DataSource = dt;
            tblVersions.DataBind();
            //for (Int32 i = 0; i < vers.rowsCount; i++)
            //{
            //    vers.currentIndex = i;
            //    TableRow TR = new TableRow();
            //    TableCell TD = new TableCell();
            //    TD.Text = "<img src='../Images/Icons/doc-Icon.png' />";
            //    TR.Cells.Add(TD);
            //    TD = new TableCell();
            //    HyperLink lnk = new HyperLink();
            //    //lnk.NavigateUrl = "../Screen/showDocument.aspx?docID=" + c.encrypt(docID.ToString());
            //    c = new CommonFunction.clsCommon();
            //    //lnk.NavigateUrl += "&ver=" + vers.fieldVersion.ToString();
            //    lnk.NavigateUrl = "../PdfLauncher.aspx?docID=" + c.encrypt(docID.ToString()) + "&ver=" + vers.fieldVersion.ToString() + "&userID=" + Session["userID"].ToString();
            //    //lnk.NavigateUrl = "javascript:parent.showDialog(195, '" + txtDocName.Text +
            //    //    "', " + "'../Screen/showDocument.aspx?docID=" + c.encrypt(docID.ToString()) + "&ver=" + vers.fieldVersion.ToString() + @"&'" +
            //    //    ", 1300, 700)";
            //    lnk.Target = "_blank";
            //    //lnk.Attributes.Add("data-id", c.encrypt(docID.ToString()));
            //    //lnk.Attributes.Add("onclick", "openFileDocument(this);");
            //    lnk.Text = txtDocName.Text + "-" + vers.fieldVersion.ToString() + "." + vers.fieldExt;
            //    TD.Controls.Add(lnk);
            //    TR.Cells.Add(TD);
            //    tblVersions.Rows.Add(TR);
            //}
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
            try
            {
                if (drpAction.SelectedIndex > 0)
                {
                    DateTime? EndDocumntDate = null;
                    if (txtenddateCount.Value != "")
                    {
                        EndDocumntDate = DateTime.Now;
                        EndDocumntDate = EndDocumntDate.Value.AddDays(int.Parse(txtenddateCount.Value));
                    }

                    op = new DMS.DAL.operations();
                    HiddenField hdn = new HiddenField();
                    hdn = (HiddenField)grdMyDocs.SelectedRow.Cells[2].FindControl("hdnWFID");

                    tables.dbo.documentWFPath docWF = new tables.dbo.documentWFPath();
                    op = new DMS.DAL.operations();
                    docWF = op.dboGetDocumentWFPathByPrimaryKey(c.convertToInt32(hdn.Value));

                    tables.dbo.wfPathDetails details = new tables.dbo.wfPathDetails();
                    op = new DMS.DAL.operations();
                    details = op.dboGetAllWfPathDetails("pathID = " + docWF.fieldWfPathID.ToString() + " and seqNo>" + docWF.fieldWfSeqNo.ToString());

                    op = new DMS.DAL.operations();
                    sp.dboUpdateDocumentWFPathByPrimaryKey(docWF.fieldID, docWF.fieldDocID, docWF.fieldUserID, DateTime.Now, 
                        docWF.fieldWfPathID,docWF.fieldWfSeqNo, c.convertToInt16(drpAction.SelectedValue), docWF.fieldRecipientType, 
                        txtNotes.Text, docWF.fieldReceiveDate, EndDocumntDate);

                    sp = new DMS.BLL.specialCases();
                    bool multiFlag = false;
                    for (Int32 m = 0; m < chkUsers.Items.Count; m++)
                    {
                        if (chkUsers.Items[m].Selected)
                        {
                            multiFlag = true;
                            break;
                        }
                    }

                    if (drpNext.SelectedIndex == 0 && docWF.fieldWfPathID == 0 && multiFlag == false)
                    {
                        //lblRes.Text = "This document in custom workflow mode ... Please select a recipient";
                        //if (Session["lang"].ToString() == "1")
                        //{
                        //    lblRes.Text = "هذا المستند يسير في مسار عمل مخصص ... يرجى اختيار المرسل إليه";
                        //}
                        sp.closeDocWF(docWF.fieldDocID);
                        //docDetails.Visible = false;
                        grdMyDocs.SelectedIndex = -1;
                        drpNext.SelectedIndex = 0;
                        drpAction.SelectedIndex = 1;
                        fillGrid();

                        lblRes.Text = "";
                        txtNotes.Text = "";
                        drpAction.SelectedIndex = 1;
                    }
                    else
                    {

                        if (drpNext.SelectedIndex == 0 && docWF.fieldWfPathID > 0 && multiFlag == false)
                        {
                            if (details.hasRows)
                            {
                                //Need to be changed
                                //sp = new DMS.BLL.specialCases();
                                //sp.dboAddDocumentWFPath(docWF.fieldDocID, details.fieldRecipientID, DateTime.MaxValue.AddDays(-1),
                                //    docWF.fieldWfPathID, details.fieldSeqNo, 0, 1, "", DateTime.Now, EndDocumntDate);

                                Int32 receipiantID = 0;

                                Int32[] receipiantIDs = new Int32[1];
                                Int32 count = 1;
                                switch (details.fieldRecipientType)
                                {
                                    case 1:
                                        receipiantID = details.fieldRecipientID;
                                        break;
                                    case 2:
                                        count = Convert.ToInt32(c.GetDataAsScalar("select count(userID) from users where grpID=" + details.fieldRecipientID));
                                        if (count <= 1)
                                        { receipiantID = Convert.ToInt32(c.GetDataAsScalar("select top userid 1 from users where grpID=" + details.fieldRecipientID)); }
                                        else
                                        {

                                            DataTable dt = c.GetDataAsDataTable("select userID from users where grpID=" + details.fieldRecipientID);
                                            receipiantIDs = new Int32[count];
                                            for (Int32 i = 0; i < dt.Rows.Count; i++)
                                            {
                                                receipiantIDs[i] = Convert.ToInt32(dt.Rows[i]["userID"]);
                                            }
                                        }
                                        break;
                                    case 3:
                                        count = Convert.ToInt32(c.GetDataAsScalar("select count(userID) from users where positionID=" + details.fieldRecipientID));
                                        if (count <= 1)
                                        { receipiantID = Convert.ToInt32(c.GetDataAsScalar("select top userid 1 from users where positionID=" + details.fieldRecipientID)); }
                                        else
                                        {
                                            DataTable dt = c.GetDataAsDataTable("select userID from users where positionID=" + details.fieldRecipientID);
                                            receipiantIDs = new Int32[count];
                                            for (Int32 i = 0; i < dt.Rows.Count; i++)
                                            {
                                                receipiantIDs[i] = Convert.ToInt32(dt.Rows[i]["userID"]);
                                            }
                                        }
                                        break;
                                    case 4:
                                        count = Convert.ToInt32(c.GetDataAsScalar("select count(userID) from users where departmentID=" + details.fieldRecipientID));
                                        if (count <= 1)
                                        { receipiantID = Convert.ToInt32(c.GetDataAsScalar("select top userid 1 from users where departmentID=" + details.fieldRecipientID)); }
                                        else
                                        {
                                            DataTable dt = c.GetDataAsDataTable("select userID from users where departmentID=" + details.fieldRecipientID);
                                            receipiantIDs = new Int32[count];
                                            for (Int32 i = 0; i < dt.Rows.Count; i++)
                                            {
                                                receipiantIDs[i] = Convert.ToInt32(dt.Rows[i]["userID"]);
                                            }
                                        }
                                        break;
                                    case 5:
                                        c = new CommonFunction.clsCommon();
                                        string addedUser = c.GetDataAsScalar("select addedUserID from documents where docID=" + docWF.fieldDocID.ToString()).ToString();
                                        string DepartmentID = c.GetDataAsScalar("select departmentID from users where userid=" + addedUser).ToString();
                                        if (DepartmentID != "")
                                        {
                                            string ManagerID = c.GetDataAsScalar("select headUserID from departments where departmentID=" + DepartmentID).ToString();
                                            if (ManagerID != "")
                                                receipiantID = Convert.ToInt32(ManagerID);
                                        }
                                        break;
                                }
                                if (count <= 1)
                                {
                                    sp = new DMS.BLL.specialCases();
                                    sp.dboAddDocumentWFPath(docWF.fieldDocID, receipiantID, DateTime.MaxValue.AddDays(-1),
                                        docWF.fieldWfPathID, details.fieldSeqNo, 0, 1, "", DateTime.Now,EndDocumntDate);

                                    try
                                    {
                                        string userEmail = c.GetDataAsScalar("select Email from users where userID=" + receipiantID.ToString()).ToString();
                                        if (!String.IsNullOrEmpty(userEmail))
                                            mailer.sendMail(userEmail, Request.Url.AbsoluteUri, docID, mailer.location.Inbox);

                                        op = new DMS.DAL.operations();
                                        op.dboAddUserDocuments(receipiantID, docID, true, true, true, false);
                                    }
                                    catch
                                    { }
                                }
                                else
                                {
                                    for (Int32 i = 0; i < count; i++)
                                    {
                                        sp = new DMS.BLL.specialCases();
                                        sp.dboAddDocumentWFPath(docWF.fieldDocID, receipiantIDs[i], DateTime.MaxValue.AddDays(-1),
                                            docWF.fieldWfPathID, details.fieldSeqNo, 0, 1, "", DateTime.Now,EndDocumntDate);

                                        op = new DMS.DAL.operations();
                                        op.dboAddUserDocuments(receipiantIDs[i], docID, true, true, true, false);

                                        try
                                        {
                                            string userEmail = c.GetDataAsScalar("select Email from users where userID=" + receipiantIDs[i].ToString()).ToString();
                                            if (!String.IsNullOrEmpty(userEmail))
                                                mailer.sendMail(userEmail, Request.Url.AbsoluteUri, docID, mailer.location.Inbox);

                                        }
                                        catch
                                        { }
                                    }
                                }
                            }
                            else
                            {
                                sp.closeDocWF(docWF.fieldDocID);
                            }
                        }
                        else
                        {
                            if (drpNext.SelectedIndex > 0)
                            {
                                op = new DMS.DAL.operations();
                                sp.dboAddDocumentWFPath(docWF.fieldDocID, c.convertToInt32(drpNext.SelectedValue), DateTime.MaxValue.AddDays(-1),
                                    0, c.convertToInt16(docWF.fieldWfSeqNo + 1), 0, 1, "", DateTime.Now, EndDocumntDate);
                                
                                sp.closeDocWF(docWF.fieldDocID);
                            }
                            if (multiFlag)
                            {
                                for (Int32 m = 0; m < chkUsers.Items.Count; m++)
                                {
                                    if (chkUsers.Items[m].Selected)
                                    {
                                        op = new DMS.DAL.operations();
                                        sp.dboAddDocumentWFPath(docWF.fieldDocID, c.convertToInt32(chkUsers.Items[m].Value), DateTime.MaxValue.AddDays(-1),
                                            0, c.convertToInt16(docWF.fieldWfSeqNo + 1), 0, 1, "", DateTime.Now);
                                        chkUsers.Items[m].Selected = false;
                                    }
                                }
                                sp.closeDocWF(docWF.fieldDocID);
                            }
                        }

                        //TabContainer1.Visible = false;
                        if (grdMyDocs.Rows.Count > grdMyDocs.SelectedIndex + 1)
                        {
                            grdMyDocs.SelectedIndex = grdMyDocs.SelectedIndex + 1;
                            HiddenField hdnx = new HiddenField();
                            hdnx = (HiddenField)grdMyDocs.SelectedRow.Cells[2].FindControl("hdnIsDelay");
                            showDoc(int.Parse(hdnx.Value));
                        }
                        else
                        {
                            grdMyDocs.SelectedIndex = -1;
                        }

                        drpNext.SelectedIndex = 0;
                        drpAction.SelectedIndex = 0;
                        fillGrid();

                        lblRes.Text = "";
                        txtNotes.Text = "";
                        drpAction.SelectedIndex = 0;
                    }
                    // update submit date 2020-07-10 19:20:43.540
                    c.NonQuery("update dbo.documents set statusId=1,submitDate ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' where docID=" + docWF.fieldDocID + "");
                    // remove delay inbox
                    c.NonQuery("update documentWFPathDelayed set documentWFPathDelayed.actionType=1 where  documentWFPathId=" + docWF.fieldID + "");
                }
                else
                {
                    lblRes.Text = "Please select an Action";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "يرجى إختيار  الإجراء";
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        protected void lnkUndo_Click(object sender, EventArgs e)
        {
            DateTime? EndDocumntDate = null;
            if (txtenddateCount.Value != "")
            {
                EndDocumntDate = DateTime.Now;
                EndDocumntDate = EndDocumntDate.Value.AddDays(int.Parse(txtenddateCount.Value));
            }

            op = new DMS.DAL.operations();
            HiddenField hdn = new HiddenField();
            hdn = (HiddenField)grdMyDocs.SelectedRow.Cells[2].FindControl("hdnWFID");

            tables.dbo.documentWFPath docWF = new tables.dbo.documentWFPath();
            op = new DMS.DAL.operations();
            docWF = op.dboGetDocumentWFPathByPrimaryKey(c.convertToInt32(hdn.Value));

            //tables.dbo.documentWFPath details = new tables.dbo.documentWFPath();
            //op = new DMS.DAL.operations();
            //details = op.dboGetAllDocumentWFPath("ID = " + docWF.fieldWfPathID.ToString() + " and seqNo <" + docWF.fieldWfSeqNo.ToString());

            op = new DMS.DAL.operations();
            sp.dboUpdateDocumentWFPathByPrimaryKey(docWF.fieldID, docWF.fieldDocID, docWF.fieldUserID, DateTime.Now, docWF.fieldWfPathID,
                docWF.fieldWfSeqNo, c.convertToInt16(drpAction.SelectedValue), docWF.fieldRecipientType, txtNotes.Text, docWF.fieldReceiveDate, EndDocumntDate);

            sp = new DMS.BLL.specialCases();
            bool multiFlag = false;
            for (Int32 m = 0; m < chkUsers.Items.Count; m++)
            {
                if (chkUsers.Items[m].Selected)
                {
                    multiFlag = true;
                    break;
                }
            }
            if (drpNext.SelectedIndex == 0 && docWF.fieldWfPathID == 0 && multiFlag == false)
            {
                //lblRes.Text = "This document in custom workflow mode ... Please select a recipient";
                //if (Session["lang"].ToString() == "1")
                //{
                //    lblRes.Text = "هذا المستند يسير في مسار عمل مخصص ... يرجى اختيار المرسل إليه";
                //}
                sp.closeDocWF(docWF.fieldDocID);
                //docDetails.Visible = false;
                grdMyDocs.SelectedIndex = -1;
                drpNext.SelectedIndex = 0;
                drpAction.SelectedIndex = 1;
                fillGrid();

                lblRes.Text = "";
                txtNotes.Text = "";
                drpAction.SelectedIndex = 1;
            }
            else
            {
                tables.dbo.documentWFPath prevDetails = new tables.dbo.documentWFPath();
                op = new DMS.DAL.operations();
                prevDetails = op.dboGetAllDocumentWFPath("ID < " + hdn.Value + "and wfSeqNo <" + docWF.fieldWfSeqNo + "and docID=" + docWF.fieldDocID, "ID Desc");
                if (drpNext.SelectedIndex == 0 && docWF.fieldWfPathID > 0 && multiFlag == false)
                {
                    if (prevDetails.hasRows)
                    {
                        sp = new DMS.BLL.specialCases();
                        sp.dboAddDocumentWFPath(prevDetails.fieldDocID, prevDetails.fieldUserID, DateTime.MaxValue.AddDays(-1),
                            docWF.fieldWfPathID, prevDetails.fieldWfSeqNo, 0, 1, "", DateTime.Now, EndDocumntDate);
                    }
                    else
                    {
                        sp.closeDocWF(docWF.fieldDocID);
                    }
                }
                else
                {
                    if (drpNext.SelectedIndex > 0)
                    {
                        sp = new DMS.BLL.specialCases();
                        sp.dboAddDocumentWFPath(docWF.fieldDocID, c.convertToInt32(drpNext.SelectedValue), DateTime.MaxValue.AddDays(-1),
                            0, c.convertToInt16(docWF.fieldWfSeqNo + 1), 0, 1, "", DateTime.Now, EndDocumntDate);
                        sp.closeDocWF(docWF.fieldDocID);
                    }
                    if (multiFlag)
                    {
                        for (Int32 m = 0; m < chkUsers.Items.Count; m++)
                        {
                            if (chkUsers.Items[m].Selected)
                            {
                                sp = new DMS.BLL.specialCases();
                                sp.dboAddDocumentWFPath(docWF.fieldDocID, c.convertToInt32(chkUsers.Items[m].Value), DateTime.MaxValue.AddDays(-1),
                                    0, c.convertToInt16(docWF.fieldWfSeqNo + 1), 0, 1, "", DateTime.Now);
                                chkUsers.Items[m].Selected = false;
                            }
                        }
                        sp.closeDocWF(docWF.fieldDocID);
                    }
                }

                //TabContainer1.Visible = false;


                if (grdMyDocs.Rows.Count > grdMyDocs.SelectedIndex + 1)
                {
                    grdMyDocs.SelectedIndex = grdMyDocs.SelectedIndex + 1;
                    HiddenField hdnx = new HiddenField();
                    hdnx = (HiddenField)grdMyDocs.SelectedRow.Cells[2].FindControl("hdnIsDelay");
                    showDoc(int.Parse(hdnx.Value));
                }
                else
                {
                    grdMyDocs.SelectedIndex = -1;
                }

                drpNext.SelectedIndex = 0;
                drpAction.SelectedIndex = 0;
                fillGrid();

                lblRes.Text = "";
                txtNotes.Text = "";
                drpAction.SelectedIndex = 0;
            }
            c.NonQuery("update dbo.documents set statusId=1,submitDate ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' where docID=" + docWF.fieldDocID + "");
            // remove delay inbox
            c.NonQuery("update documentWFPathDelayed set documentWFPathDelayed.actionType=1 where  documentWFPathId=" + docWF.fieldID + "");
        }

        public void fillWorkflow()
        {
            try
            {
                string sql = "SELECT    dbo.getUserPosition( dbo.users.userID) as fullName, dbo.documentWFPath.receiveDate, dbo.documentWFPath.actionType, dbo.documentWFPath.actionDateTime,  " +
                        " dbo.documentWFPath.userNotes FROM         dbo.users INNER JOIN  dbo.documentWFPath ON dbo.users.userID = dbo.documentWFPath.userID " +
                        " WHERE     (dbo.documentWFPath.docID = '" + docID.ToString() + "')";
                DataTable DT = new DataTable();
                DT = c.GetDataAsDataTable(sql);
                rptWorkflow.DataSource = DT;
                rptWorkflow.DataBind();

                rptHideWorkflow.DataSource = DT;
                rptHideWorkflow.DataBind();
            }
            catch { }

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

        protected void grdMyDocs_DataBound(object sender, EventArgs e)
        {

        }

        protected void LinkArchiveNotFinshed_Click(object sender, EventArgs e)
        {
            op = new DMS.DAL.operations();
            HiddenField hdn = new HiddenField();
            hdn = (HiddenField)grdMyDocs.SelectedRow.Cells[2].FindControl("hdnWFID");
            tables.dbo.documentWFPath docWF = new tables.dbo.documentWFPath();
            op = new DMS.DAL.operations();
            docWF = op.dboGetDocumentWFPathByPrimaryKey(c.convertToInt32(hdn.Value));
            DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
            sp.closeDocWF(docWF.fieldDocID);
            LinkArchiveNotFinshed.Visible = false;
            LinkButton1.Visible = false;
            LinkButton2.Visible = false;
            indexId = 0;
            txtNotes.Text = "";
            fillGrid();
        }
 

        protected void dlgrdMyDocs_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)dlgrdMyDocs.Items[e.NewSelectedIndex];
            HiddenField hdnWFID = (HiddenField)item.FindControl("hdnWFID");
            HiddenField hdnDocID = (HiddenField)item.FindControl("hdnDocID");
            HiddenField hdnIsDelay = (HiddenField)item.FindControl("hdnIsDelay");
            //int docID = c.convertToInt32(btn.CommandArgument);
            //int IsDely = c.convertToInt32(btn.CommandName);
            showDoc(int.Parse(hdnIsDelay.Value), hdnDocID.Value, hdnWFID.Value);
            ClearAllClasses();
            var ctrl = (HtmlContainerControl)item.FindControl("liItem");
            ctrl.Attributes["class"] = "active";
        }
        protected void ClearAllClasses()
        {
            foreach (var itx in dlgrdMyDocs.Items)
            {
                var ctrlx = (HtmlContainerControl)itx.FindControl("liItem");
                ctrlx.Attributes["class"] = "";
            }
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

        protected void linkExport_Click(object sender, EventArgs e)
        {
            // DMS.BLL.specialCases specialCases = new DMS.BLL.specialCases();
            DMS.BLL.specialCases.SaveInOutSerial(int.Parse(txtDocID.Text),1);
            linkExport.Visible = false;

            try
            {
                string serial = c.GetDataAsScalar("select top 1 serial from documents where docID=" + int.Parse(txtDocID.Text) + "").ToString();
                string typeId = c.GetDataAsScalar("select top 1 typeId from documents where docID=" + int.Parse(txtDocID.Text) + "").ToString();
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
            }
            catch (Exception ex)
            {

                //throw;
            }

            Response.Write("<script>alert('تم تصدير الملف');</script>");
        }

        protected void btnExport_ServerClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=RepeaterExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            rptHideWorkflow.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            //string attachment = "attachment; filename=Contacts.xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //// Insert below
            //Response.ContentEncoding = System.Text.Encoding.Unicode;
            //Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            //Response.ContentType = "application/ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //ExportDiv.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.End();

            //Response.AppendHeader("content-disposition", "attachment;filename=Path.xls");
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.ms-excel";
            //this.EnableViewState = false;
            //Response.Write(ExportDiv.InnerHtml);
            //Response.End();
        }
        //protected void dlgrdMyDocs_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ListViewItem item = (ListViewItem)dlgrdMyDocs.Items[e.NewSelectedIndex];
        //    HiddenField hdnWFID = (HiddenField)item.FindControl("hdnWFID");
        //    HiddenField hdnDocID = (HiddenField)item.FindControl("hdnDocID");
        //    HiddenField hdnIsDelay = (HiddenField)item.FindControl("hdnIsDelay");
        //    //int docID = c.convertToInt32(btn.CommandArgument);
        //    //int IsDely = c.convertToInt32(btn.CommandName);
        //    showDoc(int.Parse(hdnIsDelay.Value), hdnDocID.Value, hdnWFID.Value);
        //    fillGrid(false);
        //}
    }
}