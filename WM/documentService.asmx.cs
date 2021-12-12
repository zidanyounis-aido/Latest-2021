using dms.VM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace dms.WM
{
    /// <summary>
    /// Summary description for CheckDocs
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CheckDocs : System.Web.Services.WebService
    {

        [WebMethod]
        public string CheckDocuments()
        {
            try
            {
                DataTable docs = new DataTable();
                docs = DMS.BLL.specialCases.getUnArchivedDocumentsByPage("", "docID desc", 0, "docTypID in(1,23)", "0");
                List<DocumentsVM> documentsVMs = new List<DocumentsVM>();
                foreach (DataRow row in docs.Rows)
                {
                    DocumentsVM obj = new DocumentsVM();
                    obj.addedDate = row["addedDate"].ToString() != "" ? DateTime.Parse(row["addedDate"].ToString()) : DateTime.Now;
                    obj.docID = int.Parse(row["docID"].ToString());
                    obj.modifyDate = row["docTypID"].ToString() != "" ? DateTime.Parse(row["modifyDate"].ToString()) : (DateTime?)null;
                    obj.submitDate = row["submitDate"].ToString() != "" ? DateTime.Parse(row["submitDate"].ToString()) : DateTime.Parse(row["addedDate"].ToString());
                    obj.durationType = int.Parse(row["durationType"].ToString());
                    obj.duration = int.Parse(row["duration"].ToString());
                    if (obj.durationType != -1 && obj.duration != -1)
                    {
                        if (obj.duration > 0)
                        {
                            int durationMuliplie = obj.durationType == 1 ? 1 : 24;
                            var totalHours = (obj.submitDate.Value.AddHours((obj.duration * durationMuliplie)) - DateTime.Now).TotalHours;
                            if (totalHours > 0)
                            {
                                obj.LeftTime = Math.Round(totalHours, 1).ToString();
                            }
                            else
                            {
                                if (row["statusId"].ToString() != "2")
                                {
                                    obj.LeftTime = "0";
                                    totalHours = Math.Ceiling(totalHours);
                                    //obj.statusName = HttpContext.Current.Session["lang"].ToString() == "0" ? "Late" : "متأخر";
                                    CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                                    c.NonQuery("update dbo.documents set statusId=3,DelayTime=" + totalHours + " where dbo.documents.docID=" + obj.docID);
                                    SendailToUsers(obj.docID);
                                }
                            }
                        }
                    }
                    else
                    {
                        obj.LeftTime = "∞";
                    }
                }
                //Write JSON to response object
                return "true";
            }
            catch (Exception ex)
            {
                return "false";
            }
            // return "Hello World";
        }

        public void SendailToUsers(int docId)
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            DateTime EndDocumntDate = new DateTime();
            var op = new DMS.DAL.operations();
            op = new DMS.DAL.operations();

            tables.dbo.documents docTB = new tables.dbo.documents();
            docTB = op.dboGetDocumentsByPrimaryKey(docId);


            tables.dbo.folders folderTB = new tables.dbo.folders();
            folderTB = op.dboGetFoldersByPrimaryKey(docTB.fieldFldrID);
            //get seq no
            short wfPathId = short.Parse(c.GetDataAsScalar("select top 1  documentWFPath.ID as wfSeqNo  from documentWFPath where docID=" + docId + " order by ID DESC").ToString());

            tables.dbo.documentWFPath documentWFPath = new tables.dbo.documentWFPath();
            documentWFPath = op.dboGetDocumentWFPathByPrimaryKey(wfPathId);

            DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
            //////////////////////// send mail to folder owner //////////////////////
            try
            {
                sp.addDocumentWFPathDelayed(documentWFPath.fieldDocID, folderTB.fieldFolderOwner, DateTime.MaxValue.AddDays(-1),
                documentWFPath.fieldWfPathID, documentWFPath.fieldWfSeqNo, 0, 1, "", DateTime.Now, null, 0, 1, documentWFPath.fieldID);
            }
            catch (Exception ex)
            {
                //throw;
            }
            //string durationTypeQuery = ", ISNULL((SELECT durationType FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docTypID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docTypID  and dbo.documentWFPath.actionType=0)),-1) as durationType";
            //string durationQuery = ",ISNULL((SELECT duration FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docTypID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docTypID  and dbo.documentWFPath.actionType=0)),-1) as duration";
            int recipientID = int.Parse(c.GetDataAsScalar("(SELECT recipientID FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=" + docTB.fieldDocTypID + " ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=" + docTB.fieldDocTypID + "  and dbo.documentWFPath.actionType=0 ))").ToString());
            //////////////////////// send mail to current user//////////////////////
            try
            {
                sp.addDocumentWFPathDelayed(documentWFPath.fieldDocID, recipientID, DateTime.MaxValue.AddDays(-1),
               documentWFPath.fieldWfPathID, documentWFPath.fieldWfSeqNo, 0, 1, "", DateTime.Now, null, 0, 1, documentWFPath.fieldID);
            }
            catch (Exception ex)
            {
                // throw;
            }
        }
    }
}
