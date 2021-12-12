using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using dms.Utilities;

namespace DMS.BLL
{
    public class specialCases
    {
        DMS.DAL.DataProccess dp = new DMS.DAL.DataProccess();
        UserData _userData = new UserData();
        //changeUserActiveStatus
        public void changeUserActiveStatus(Int32 userID, bool active)
        {
            dp.parameters.Clear();
            dp.parameters.Add("@userID", userID);
            dp.parameters.Add("@active", active);
            dp.excuteNonQuery("changeUserActiveStatus");
        }
        public DataTable checkLog(string username, string password)
        {
            dp.parameters.Clear();
            dp.parameters.Add("@username", username);
            dp.parameters.Add("@password", password);
            return dp.excuteQuery("checkLog");
        }

        public Int32 dboUpdateSignture(Int32 userid, string signature)
        {
            dp.parameters.Clear();
            dp.parameters.Add("@userid", userid);
            dp.parameters.Add("@signature", signature);
            Int32 res;
            res = dp.excuteNonQuery("dbo.UpdateSignture");
            return res;
        }

        public Int32 dboAddDocuments(Int32 docTypID, string docName, string docExt, DateTime addedDate, Int32 addedUserID, Int16 lastVersion, DateTime modifyDate, Int32 modifyUserID, Int32 fldrID, string ocrContent, Int64 folderSeq, Int64 docTypeSeq, Int64 folderDocTypeSeq)
        {
            dp.parameters.Clear();
            dp.parameters.Add("@docTypID", docTypID);
            dp.parameters.Add("@docName", docName);
            dp.parameters.Add("@docExt", docExt);
            dp.parameters.Add("@addedDate", addedDate);
            dp.parameters.Add("@addedUserID", addedUserID);
            dp.parameters.Add("@lastVersion", lastVersion);
            dp.parameters.Add("@modifyDate", modifyDate);
            dp.parameters.Add("@modifyUserID", modifyUserID);
            dp.parameters.Add("@fldrID", fldrID);
            dp.parameters.Add("@ocrContent", ocrContent);
            dp.parameters.Add("@folderSeq", folderSeq);
            dp.parameters.Add("@docTypeSeq", docTypeSeq);
            dp.parameters.Add("@folderDocTypeSeq", folderDocTypeSeq);
            Int32 res = -1;
            res = Convert.ToInt32(dp.executeScalar("dbo._addDocuments"));
            return res;
        }

        public Int32 updateDocumentsWithOutMeta(Int64 docID, Int32 docTypID, string docName, string docExt, DateTime addedDate, Int32 addedUserID, Int16 lastVersion, DateTime modifyDate, Int32 modifyUserID, Int32 fldrID, string ocrContent, Int64 folderSeq, Int64 docTypeSeq, Int64 folderDocTypeSeq, Int32 wfPathID, Int16 wfCurrentSeq, Int32 wfCurrentRecipientID, Int16 wfCurrentRecipientType, DateTime wfStartDateTime, Decimal wfTimeFrame, Int16 wfStatus)
        {
            dp.parameters.Clear();
            dp.parameters.Add("@docID", docID);
            dp.parameters.Add("@docTypID", docTypID);
            dp.parameters.Add("@docName", docName);
            dp.parameters.Add("@docExt", docExt);
            dp.parameters.Add("@addedDate", addedDate);
            dp.parameters.Add("@addedUserID", addedUserID);
            dp.parameters.Add("@lastVersion", lastVersion);
            dp.parameters.Add("@modifyDate", modifyDate);
            dp.parameters.Add("@modifyUserID", modifyUserID);
            dp.parameters.Add("@fldrID", fldrID);
            dp.parameters.Add("@ocrContent", ocrContent);
            dp.parameters.Add("@folderSeq", folderSeq);
            dp.parameters.Add("@docTypeSeq", docTypeSeq);
            dp.parameters.Add("@folderDocTypeSeq", folderDocTypeSeq);
            dp.parameters.Add("@wfPathID", wfPathID);
            dp.parameters.Add("@wfCurrentSeq", wfCurrentSeq);
            dp.parameters.Add("@wfCurrentRecipientID", wfCurrentRecipientID);
            dp.parameters.Add("@wfCurrentRecipientType", wfCurrentRecipientType);
            dp.parameters.Add("@wfStartDateTime", wfStartDateTime);
            dp.parameters.Add("@wfTimeFrame", wfTimeFrame);
            dp.parameters.Add("@wfStatus", wfStatus);
            Int32 res;
            res = dp.excuteNonQuery("dbo.updateDocumentsWithOutMeta");
            return res;
        }


        public void closeDocWF(Int64 docID)
        {
            dp.parameters.Clear();
            dp.parameters.Add("@docID", docID);
            dp.excuteNonQuery("closeDocWF");
            // update document status to archived
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            c.NonQuery("update dbo.documents set statusId=2 where dbo.documents.docID=" + docID);
            // remove delay inbox
            c.NonQuery("update documentWFPathDelayed set documentWFPathDelayed.actionType=1 where  docID=" + docID + "");

        }

        public DataTable getDelayedDocuemnts()
        {
            dp.parameters.Clear();
            return dp.excuteQuery("getDelayedDocuemnts");

        }

        public DataTable getWorkflowUsers()
        {
            dp.parameters.Clear();
            dp.parameters.Add("@lang", Convert.ToInt16(HttpContext.Current.Session["lang"]));
            dp.parameters.Add("@clientId", _userData.ClientId);
            return dp.excuteQuery("getWorkflowUsers");

        }

        public static DataTable getDocumentsByPage(string cond, Int32 pageNumber, Int32 rowsCount, string orderBy = "docID desc", int userid = 0, string tabFilter = "", string lang = "0")
        {
            if (cond.Trim() != "" && !cond.Trim().ToLower().StartsWith("where"))
            {
                cond = "where " + cond;
            }
            if (userid == 0)
            {
                DataTable DT = new DataTable();
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                Int32 fromNo = ((pageNumber - 1) * rowsCount) + 1;
                Int32 ToNo = fromNo + rowsCount;
                string statusQuery = "";
                if (lang == "0")
                {
                    statusQuery = ",ISNULL(documentsStatus.statusNameEN,'in process') as statusName";
                }
                else
                {
                    statusQuery = ",ISNULL(documentsStatus.statusName,N'قيد الإجراء') as statusName";
                }
                string durationTypeQuery = ", ISNULL((SELECT durationType FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as durationType";
                string durationQuery = ",ISNULL((SELECT duration FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as duration";
                string SQL = "SELECT  *"
                    + " FROM(SELECT    ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS RowNum, documents.*,'black' as Color"
                    + statusQuery
                    + durationTypeQuery
                    + durationQuery
                    + " FROM  documents LEFT JOIN documentsStatus ON documents.statusId = documentsStatus.statusId"
                    + " " + cond
                    + " ) AS RowConstrainedResult"
                    + " WHERE RowNum >= " + fromNo.ToString()
                    + " AND RowNum < " + ToNo.ToString()
                    + tabFilter
                    + " ORDER BY RowNum";
                if (orderBy.Contains("docTypID"))
                {
                    SQL = "SELECT  *"
                    + " FROM(SELECT    ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS RowNum, documents.*,'black' as Color"
                     + statusQuery
                    + durationTypeQuery
                    + durationQuery
                    + " FROM  documents LEFT JOIN documentsStatus ON documents.statusId = documentsStatus.statusId"
                    + " " + cond
                    + " ) AS RowConstrainedResult"
                    + " WHERE RowNum >= " + fromNo.ToString()
                    + " AND RowNum < " + ToNo.ToString()
                    + tabFilter
                    + " ORDER BY " + orderBy;
                }
                DT = c.GetDataAsDataTable(SQL);
                return DT;
            }
            else
            {
                DataTable DT = new DataTable();
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                Int32 fromNo = ((pageNumber - 1) * rowsCount) + 1;
                Int32 ToNo = fromNo + rowsCount;
                string statusQuery = "";
                if (lang == "0")
                {
                    statusQuery = ",ISNULL(documentsStatus.statusNameEN,'in process') as statusName";
                }
                else
                {
                    statusQuery = ",ISNULL(documentsStatus.statusName,N'قيد الإجراء') as statusName";
                }
                string durationTypeQuery = ", ISNULL((SELECT durationType FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as durationType";
                string durationQuery = ",ISNULL((SELECT duration FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as duration";
                string SQL = "SELECT  *"
                    + " FROM(SELECT    ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS RowNum, documents.*,"
                    + "CASE WHEN ((select top(1) [EndDate] from [dbo].[documentWFPath]  where [docID] =dbo.documents.docID and [userID]=" + userid + " ) IS NOT NULL and (select top(1) [EndDate] from [dbo].[documentWFPath]  where [docID] =dbo.documents.docID and [userID]=" + userid + " ) >= GETDATE()) THEN 'black' ELSE CASE WHEN (select top(1) [EndDate] from [dbo].[documentWFPath]  where [docID] =dbo.documents.docID and [userID]=" + userid + " ) IS NOT NULL THEN CASE WHEN (select top(1) actionDateTime from [dbo].[documentWFPath]  where [docID] =dbo.documents.docID and [userID]=" + userid + " ) IS NOT NULL THEN 'black' ELSE 'red' END  ELSE 'black' END END as 'Color' "
                    + statusQuery
                    + durationTypeQuery
                    + durationQuery
                    + " FROM  documents LEFT JOIN documentsStatus ON documents.statusId = documentsStatus.statusId"
                    + " " + cond
                    + " ) AS RowConstrainedResult"
                    + " WHERE RowNum >= " + fromNo.ToString()
                    + " AND RowNum < " + ToNo.ToString()
                    + tabFilter
                    + " ORDER BY RowNum";
                if (orderBy.Contains("docTypID"))
                {
                    SQL = "SELECT  *"
                    + " FROM(SELECT    ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS RowNum, documents.*,'black' as Color"
                    + statusQuery
                    + durationTypeQuery
                    + durationQuery
                    + " FROM  documents LEFT JOIN documentsStatus ON documents.statusId = documentsStatus.statusId"
                    + " " + cond
                    + " ) AS RowConstrainedResult"
                    + " WHERE RowNum >= " + fromNo.ToString()
                    + " AND RowNum < " + ToNo.ToString()
                    + tabFilter
                    + " ORDER BY " + orderBy;
                }
                DT = c.GetDataAsDataTable(SQL);
                return DT;
            }

        }
        public static DataTable getDocumentsByPageWithSerials(string cond, Int32 pageNumber, Int32 rowsCount, string orderBy = "docID desc", int userid = 0, string tabFilter = "", string lang = "0")
        {
            if (cond.Trim() != "" && !cond.Trim().ToLower().StartsWith("where"))
            {
                cond = "where " + cond;
            }
            if (userid == 0)
            {
                DataTable DT = new DataTable();
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                Int32 fromNo = ((pageNumber - 1) * rowsCount) + 1;
                Int32 ToNo = fromNo + rowsCount;
                string statusQuery = "";
                if (lang == "0")
                {
                    statusQuery = ",ISNULL(documentsStatus.statusNameEN,'in process') as statusName";
                }
                else
                {
                    statusQuery = ",ISNULL(documentsStatus.statusName,N'قيد الإجراء') as statusName";
                }
                string durationTypeQuery = ", ISNULL((SELECT durationType FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as durationType";
                string durationQuery = ",ISNULL((SELECT duration FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as duration";
                string SQL = "SELECT  *"
                    + " FROM(SELECT    ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS RowNum, documents.*,'black' as Color"
                    + statusQuery
                    + durationTypeQuery
                    + durationQuery
                    + " FROM  documents LEFT JOIN documentsStatus ON documents.statusId = documentsStatus.statusId"
                    + " " + cond
                    + " ) AS RowConstrainedResult"
                    + " WHERE RowNum >= " + fromNo.ToString()
                    + " AND RowNum < " + ToNo.ToString()
                    + tabFilter
                    + " ORDER BY RowNum";
                if (orderBy.Contains("docTypID"))
                {
                    SQL = "SELECT  *"
                    + " FROM(SELECT    ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS RowNum, documents.*,'black' as Color"
                     + statusQuery
                    + durationTypeQuery
                    + durationQuery
                    + " FROM  documents LEFT JOIN documentsStatus ON documents.statusId = documentsStatus.statusId"
                    + " " + cond
                    + " ) AS RowConstrainedResult"
                    + " WHERE RowNum >= " + fromNo.ToString()
                    + " AND RowNum < " + ToNo.ToString()
                    + tabFilter
                    + " ORDER BY " + orderBy;
                }
                DT = c.GetDataAsDataTable(SQL);
                return DT;
            }
            else
            {
                DataTable DT = new DataTable();
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                Int32 fromNo = ((pageNumber - 1) * rowsCount) + 1;
                Int32 ToNo = fromNo + rowsCount;
                string statusQuery = "";
                if (lang == "0")
                {
                    statusQuery = ",ISNULL(documentsStatus.statusNameEN,'in process') as statusName";
                }
                else
                {
                    statusQuery = ",ISNULL(documentsStatus.statusName,N'قيد الإجراء') as statusName";
                }
                string durationTypeQuery = ", ISNULL((SELECT durationType FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as durationType";
                string durationQuery = ",ISNULL((SELECT duration FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as duration";
                string SQL = "SELECT  *"
                    + " FROM(SELECT    ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS RowNum, documents.*,"
                    + "CASE WHEN ((select top(1) [EndDate] from [dbo].[documentWFPath]  where [docID] =dbo.documents.docID and [userID]=" + userid + " ) IS NOT NULL and (select top(1) [EndDate] from [dbo].[documentWFPath]  where [docID] =dbo.documents.docID and [userID]=" + userid + " ) >= GETDATE()) THEN 'black' ELSE CASE WHEN (select top(1) [EndDate] from [dbo].[documentWFPath]  where [docID] =dbo.documents.docID and [userID]=" + userid + " ) IS NOT NULL THEN CASE WHEN (select top(1) actionDateTime from [dbo].[documentWFPath]  where [docID] =dbo.documents.docID and [userID]=" + userid + " ) IS NOT NULL THEN 'black' ELSE 'red' END  ELSE 'black' END END as 'Color' "
                    + statusQuery
                    + durationTypeQuery
                    + durationQuery
                    + " FROM  documents LEFT JOIN documentsStatus ON documents.statusId = documentsStatus.statusId"
                    + " " + cond
                    + " ) AS RowConstrainedResult"
                    + " WHERE RowNum >= " + fromNo.ToString()
                    + " AND RowNum < " + ToNo.ToString()
                    + tabFilter
                    + " ORDER BY RowNum";
                if (orderBy.Contains("docTypID"))
                {
                    SQL = "SELECT  *"
                    + " FROM(SELECT    ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS RowNum, documents.*,'black' as Color"
                    + statusQuery
                    + durationTypeQuery
                    + durationQuery
                    + " FROM  documents LEFT JOIN documentsStatus ON documents.statusId = documentsStatus.statusId"
                    + " " + cond
                    + " ) AS RowConstrainedResult"
                    + " WHERE RowNum >= " + fromNo.ToString()
                    + " AND RowNum < " + ToNo.ToString()
                    + tabFilter
                    + " ORDER BY " + orderBy;
                }
                DT = c.GetDataAsDataTable(SQL);
                return DT;
            }

        }
        public static DataTable getUnArchivedDocumentsByPage(string condstring, string orderBy = "docID desc", int userid = 0, string tabFilter = "", string lang = "0")
        {
            string cond = "";
            //if (cond.Trim() != "" && !cond.Trim().ToLower().StartsWith("where"))
            //{
            //    //cond = "where " + cond;
            //}
            cond = "where  documents.statusId  is null or documents.statusId =1";
            if (userid == 0)
            {
                DataTable DT = new DataTable();
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                // Int32 fromNo = ((pageNumber - 1) * rowsCount) + 1;
                // Int32 ToNo = fromNo + rowsCount;
                string statusQuery = "";
                if (lang == "0")
                {
                    statusQuery = ",ISNULL(documentsStatus.statusNameEN,'in process') as statusName";
                }
                else
                {
                    statusQuery = ",ISNULL(documentsStatus.statusName,N'قيد الإجراء') as statusName";
                }
                string durationTypeQuery = ", ISNULL((SELECT durationType FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as durationType";
                string durationQuery = ",ISNULL((SELECT duration FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as duration";
                string SQL = "SELECT  *"
                    + " FROM(SELECT    ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS RowNum, documents.*,'black' as Color"
                    + statusQuery
                    + durationTypeQuery
                    + durationQuery
                    + " FROM  documents LEFT JOIN documentsStatus ON documents.statusId = documentsStatus.statusId"
                    + " " + cond
                    + " ) AS RowConstrainedResult"
                    + " WHERE "
                    //+ " WHERE RowNum >= " + fromNo.ToString()
                    // + " AND RowNum < " + ToNo.ToString()
                    + tabFilter
                    + " ORDER BY RowNum";
                if (orderBy.Contains("docTypID"))
                {
                    SQL = "SELECT  *"
                    + " FROM(SELECT    ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS RowNum, documents.*,'black' as Color"
                     + statusQuery
                    + durationTypeQuery
                    + durationQuery
                    + " FROM  documents LEFT JOIN documentsStatus ON documents.statusId = documentsStatus.statusId"
                    + " " + cond
                    + " ) AS RowConstrainedResult"
                    // + " WHERE RowNum >= " + fromNo.ToString()
                    // + " AND RowNum < " + ToNo.ToString()
                    + tabFilter
                    + " ORDER BY " + orderBy;
                }
                DT = c.GetDataAsDataTable(SQL);
                return DT;
            }
            else
            {
                DataTable DT = new DataTable();
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                string statusQuery = "";
                if (lang == "0")
                {
                    statusQuery = ",ISNULL(documentsStatus.statusNameEN,'in process') as statusName";
                }
                else
                {
                    statusQuery = ",ISNULL(documentsStatus.statusName,N'قيد الإجراء') as statusName";
                }
                string durationTypeQuery = ", ISNULL((SELECT durationType FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as durationType";
                string durationQuery = ",ISNULL((SELECT duration FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as duration";
                string SQL = "SELECT  *"
                    + " FROM(SELECT    ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS RowNum, documents.*,"
                    + "CASE WHEN ((select top(1) [EndDate] from [dbo].[documentWFPath]  where [docID] =dbo.documents.docID and [userID]=" + userid + " ) IS NOT NULL and (select top(1) [EndDate] from [dbo].[documentWFPath]  where [docID] =dbo.documents.docID and [userID]=" + userid + " ) >= GETDATE()) THEN 'black' ELSE CASE WHEN (select top(1) [EndDate] from [dbo].[documentWFPath]  where [docID] =dbo.documents.docID and [userID]=" + userid + " ) IS NOT NULL THEN CASE WHEN (select top(1) actionDateTime from [dbo].[documentWFPath]  where [docID] =dbo.documents.docID and [userID]=" + userid + " ) IS NOT NULL THEN 'black' ELSE 'red' END  ELSE 'black' END END as 'Color' "
                    + statusQuery
                    + durationTypeQuery
                    + durationQuery
                    + " FROM  documents LEFT JOIN documentsStatus ON documents.statusId = documentsStatus.statusId"
                    + " " + cond
                    + " ) AS RowConstrainedResult"
                    //+ " WHERE RowNum >= " + fromNo.ToString()
                    // + " AND RowNum < " + ToNo.ToString()
                    + tabFilter
                    + " ORDER BY RowNum";
                if (orderBy.Contains("docTypID"))
                {
                    SQL = "SELECT  *"
                    + " FROM(SELECT    ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS RowNum, documents.*,'black' as Color"
                    + statusQuery
                    + durationTypeQuery
                    + durationQuery
                    + " FROM  documents LEFT JOIN documentsStatus ON documents.statusId = documentsStatus.statusId"
                    + " " + cond
                    + " ) AS RowConstrainedResult"
                    // + " WHERE RowNum >= " + fromNo.ToString()
                    //  + " AND RowNum < " + ToNo.ToString()
                    + tabFilter
                    + " ORDER BY " + orderBy;
                }
                DT = c.GetDataAsDataTable(SQL);
                return DT;
            }

        }

        public static string GetReciptNameByIdType(int recipientID, int recipientType, string lang)
        {
            // 0 is en
            try
            {
                string Name = "";
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                if (recipientType == 1)
                {
                    Name = c.GetDataAsScalar("select top 1 fullName  from users where users.userID=" + recipientID).ToString();
                }
                else if (recipientType == 2)
                {
                    Name = c.GetDataAsScalar("select top 1 grpDesc  from groups where groups.grpID=" + recipientID).ToString();
                }
                else if (recipientType == 3)
                {
                    if (lang == "0")
                    {
                        Name = c.GetDataAsScalar("select top 1 positionTitle  from positions where positions.positionID=" + recipientID).ToString();
                    }
                    else
                    {
                        Name = c.GetDataAsScalar("select top 1 positionTitleAr  from positions where positions.positionID=" + recipientID).ToString();
                    }
                }
                else if (recipientType == 4)
                {
                    if (lang == "0")
                    {
                        Name = c.GetDataAsScalar("select top 1 departmentName  from departments  where departments.departmentID=" + recipientID).ToString();
                    }
                    else
                    {
                        Name = c.GetDataAsScalar("select top 1 departmentNameAr  from departments where departments.departmentID=" + recipientID).ToString();
                    }
                }
                return Name;
            }
            catch (Exception ex)
            {

                return "";
            }
            //  case "1":
            //        tables.dbo.users usersTB = new tables.dbo.users();
            //usersTB = op.dboGetAllUsers();
            //dt = usersTB.table;
            //valueF = "userID";
            //textF = "FullName";
            //break;
            //    case "2":
            //        tables.dbo.groups grpTB = new tables.dbo.groups();
            //grpTB = op.dboGetAllGroups();
            //dt = grpTB.table;
            //valueF = "grpID";
            //textF = "grpDesc";
            //break;
            //    case "3":
            //        tables.dbo.positions positionsTB = new tables.dbo.positions();
            //positionsTB = op.dboGetAllPositions();
            //dt = positionsTB.table;
            //valueF = "positionID";
            //if (Session["lang"].ToString() == "0")
            //    textF = "positionTitle";
            //else
            //    textF = "positionTitleAr";
            //break;
            //    case "4":
            //        tables.dbo.departments departmentsTB = new tables.dbo.departments();
            //departmentsTB = op.dboGetAllDepartments();
            //dt = departmentsTB.table;
            //valueF = "departmentID";
            //if (Session["lang"].ToString() == "0")
            //    textF = "departmentName";
            //else
            //    textF = "departmentNameAr";
            //break;
        }

        public static string SaveInOutSerial(int docId, int typeId)
        {
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                //get folder id
                int fldrID = int.Parse(c.GetDataAsScalar("select top 1 fldrID from documents where docid=" + docId).ToString());
                int searcType = (typeId == 1)?1:0;
                //                SELECT Id, SerialCode, Serial, Type
                //FROM IngoingOutgoingSerials
                string SavedSerial = "";
                string SerialCode = c.GetDataAsScalar(" SELECT top 1 SerialCode from IngoingOutgoingSerials where FolderId=" + fldrID + "and Type=" + searcType).ToString();
                string Serial = c.GetDataAsScalar(" SELECT top 1 Serial from IngoingOutgoingSerials where FolderId=" + fldrID + "and Type=" + searcType).ToString();
                int countIndex = int.Parse(c.GetDataAsScalar("select count(docid) from documents where typeid=" + typeId).ToString()) + 1;
                if (SerialCode == "") //get general
                {
                    SerialCode = c.GetDataAsScalar("SELECT top 1 SerialCode  from IngoingOutgoingSerials where FolderId=0 and Type = " + searcType).ToString();
                    Serial = c.GetDataAsScalar("SELECT top 1 Serial from IngoingOutgoingSerials where FolderId=0 and Type=" + searcType).ToString();
                }
                var codeArr = SerialCode.Split(',');
                var serialArr = Serial.Split(',');
                for (int i = 0; i < codeArr.Length; i++)
                {

                    if (codeArr[i] == "text" || codeArr[i] == "code")
                    {
                        SavedSerial += serialArr[i];
                    }
                    else
                    {
                        if (codeArr[i] == "id")
                        {
                            SavedSerial += countIndex.ToString();
                        }
                        else
                        {
                            SavedSerial += DateTime.Now.ToString(codeArr[i].ToString()) ;
                        }
                    }
                }
                // update
                string query = "update documents set serial=N'" + SavedSerial + "',outgoingDate=GETDATE(),typeid="+typeId+" where docid=" + docId;
                c.NonQuery(query);
                return "1";
            }
            catch (Exception)
            {

                return "0";
            }
        }

        public Int32 addDocumentWFPathDelayed(Int64 docID, Int32 userID, DateTime actionDateTime, Int32 wfPathID, Int16 wfSeqNo, Int16 actionType, Int16 recipientType, string userNotes, DateTime receiveDate, DateTime? endDate = null, int isreturned = 0, int inboxType = 0, int documentWFPathId = 0)
        {
            dp.parameters.Clear();
            dp.parameters.Add("@docID", docID);
            dp.parameters.Add("@userID", userID);
            dp.parameters.Add("@actionDateTime", actionDateTime);
            dp.parameters.Add("@wfPathID", wfPathID);
            dp.parameters.Add("@wfSeqNo", wfSeqNo);
            dp.parameters.Add("@actionType", actionType);
            dp.parameters.Add("@recipientType", recipientType);
            dp.parameters.Add("@userNotes", userNotes);
            dp.parameters.Add("@receiveDate", receiveDate);
            if (endDate != null)
                dp.parameters.Add("@endDate", endDate);
            else
                dp.parameters.Add("@endDate", DBNull.Value);
            dp.parameters.Add("@inboxType", inboxType);
            dp.parameters.Add("@documentWFPathId", documentWFPathId);
            Int32 res = -1;
            res = Convert.ToInt32(dp.executeScalar("dbo.addDocumentWFPathDelayed"));
            return res;
        }


        public tables.dbo.wfPathDetails dboGetWfPathDetailsByPrimaryKey(Int32 pathID, Int16 seqNo)
        {
            dp.parameters.Clear();
            dp.parameters.Add("@pathID", pathID);
            dp.parameters.Add("@seqNo", seqNo);
            tables.dbo.wfPathDetails varTable = new tables.dbo.wfPathDetails(dp.excuteQuery("dbo.getWfPathDetailsByPrimaryKey").Copy());
            return varTable;
        }

        public Int32 dboDeleteWfPathDetailsByPrimaryKey(Int32 pathID, Int16 seqNo)
        {
            dp.parameters.Clear();
            dp.parameters.Add("@pathID", pathID);
            dp.parameters.Add("@seqNo", seqNo);
            Int32 res;
            res = dp.excuteNonQuery("dbo.deleteWfPathDetailsByPrimaryKey");
            return res;
        }


        public Int32 dboAddMetas(Int32 docTypID, string metaDesc, string metaDataType, bool required, Int32 orderSeq, Int32 ctrlID, string defaultTexts, string defaultValues, bool visible, string metaDescAr, string defaultArTexts, int columnSeq, int metaIdFK, double width, string permissionType)
        {
            dp.parameters.Clear();
            dp.parameters.Add("@docTypID", docTypID);
            dp.parameters.Add("@metaDesc", metaDesc);
            dp.parameters.Add("@metaDataType", metaDataType);
            dp.parameters.Add("@required", required);
            dp.parameters.Add("@orderSeq", orderSeq);
            dp.parameters.Add("@ctrlID", ctrlID);
            dp.parameters.Add("@defaultTexts", defaultTexts);
            dp.parameters.Add("@defaultValues", defaultValues);
            dp.parameters.Add("@visible", visible);
            dp.parameters.Add("@metaDescAr", metaDescAr);
            dp.parameters.Add("@defaultArTexts", defaultArTexts);
            dp.parameters.Add("@columnSeq", columnSeq);
            dp.parameters.Add("@metaIdFK", metaIdFK);
            dp.parameters.Add("@width", width);
            dp.parameters.Add("@permissionType", permissionType);
            Int32 res = -1;
            res = Convert.ToInt32(dp.executeScalar("dbo.addMetas"));
            return res;
        }

        public Int32 dboUpdateDocumentWFPathByPrimaryKey(Int32 ID, Int64 docID, Int32 userID, DateTime actionDateTime, Int32 wfPathID, Int16 wfSeqNo, Int16 actionType, Int16 recipientType, string userNotes, DateTime receiveDate, DateTime? endDate = null)
        {
            dp.parameters.Clear();
            dp.parameters.Add("@ID", ID);
            dp.parameters.Add("@docID", docID);
            dp.parameters.Add("@userID", userID);
            dp.parameters.Add("@actionDateTime", actionDateTime);
            dp.parameters.Add("@wfPathID", wfPathID);
            dp.parameters.Add("@wfSeqNo", wfSeqNo);
            dp.parameters.Add("@actionType", actionType);
            dp.parameters.Add("@recipientType", recipientType);
            dp.parameters.Add("@userNotes", userNotes);
            dp.parameters.Add("@receiveDate", receiveDate);
            dp.parameters.Add("@isRemoved", 0);
            if (endDate != null)
                dp.parameters.Add("@endDate", endDate);
            else
                dp.parameters.Add("@endDate", DBNull.Value);
            Int32 res;
            res = dp.excuteNonQuery("dbo.updateDocumentWFPathByPrimaryKey");
            return res;
        }


        public Int32 dboUpdateMetasByPrimaryKey(Int32 metaID, Int32 docTypID, string metaDesc, string metaDataType, bool required, Int32 orderSeq, Int32 ctrlID, string defaultTexts, string defaultValues, bool visible, string metaDescAr, string defaultArTexts, int columnSeq, int metaIdFK, double width, string permissionType)
        {
            dp.parameters.Clear();
            dp.parameters.Add("@metaID", metaID);
            dp.parameters.Add("@docTypID", docTypID);
            dp.parameters.Add("@metaDesc", metaDesc);
            dp.parameters.Add("@metaDataType", metaDataType);
            dp.parameters.Add("@required", required);
            dp.parameters.Add("@orderSeq", orderSeq);
            dp.parameters.Add("@ctrlID", ctrlID);
            dp.parameters.Add("@defaultTexts", defaultTexts);
            dp.parameters.Add("@defaultValues", defaultValues);
            dp.parameters.Add("@visible", visible);
            dp.parameters.Add("@metaDescAr", metaDescAr);
            dp.parameters.Add("@defaultArTexts", defaultArTexts);
            dp.parameters.Add("@columnSeq", columnSeq);
            dp.parameters.Add("@metaIdFK", metaIdFK);
            dp.parameters.Add("@width", width);
            dp.parameters.Add("@permissionType", permissionType);
            Int32 res;
            res = dp.excuteNonQuery("dbo.updateMetasByPrimaryKey");
            return res;
        }

        public Int32 dboDuplicateMetaCustomPermission(int metaId, int newMetaId)
        {
            dp.parameters.Clear();

            dp.parameters.Add("@metaId", metaId);
            dp.parameters.Add("@newMetaId", newMetaId);
            Int32 res;
            res = dp.excuteNonQuery("dbo.DuplicateMetaCustomPermission");
            return res;
        }


        public Int32 dboAddDocumentWFPath(Int64 docID, Int32 userID, DateTime actionDateTime, Int32 wfPathID, Int16 wfSeqNo, Int16 actionType, Int16 recipientType, string userNotes, DateTime receiveDate, DateTime? endDate = null, int isreturned = 0)
        {
            dp.parameters.Clear();
            dp.parameters.Add("@docID", docID);
            dp.parameters.Add("@userID", userID);
            dp.parameters.Add("@actionDateTime", actionDateTime);
            dp.parameters.Add("@wfPathID", wfPathID);
            dp.parameters.Add("@wfSeqNo", wfSeqNo);
            dp.parameters.Add("@actionType", actionType);
            dp.parameters.Add("@recipientType", recipientType);
            dp.parameters.Add("@userNotes", userNotes);
            dp.parameters.Add("@receiveDate", receiveDate);
            if (endDate != null)
                dp.parameters.Add("@endDate", endDate);
            else
                dp.parameters.Add("@endDate", DBNull.Value);

            dp.parameters.Add("@isRemoved", false);

            Int32 res = -1;
            res = Convert.ToInt32(dp.executeScalar("dbo.addDocumentWFPath"));
            if (isreturned == 1)
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                c.NonQuery("update documents set docName= REPLACE(docName ,N'معاد - ','') where docID= " + docID);// clear if exist
                c.NonQuery("update documents set docName= N'معاد - '+ docName where docID= " + docID);
            }
            else
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                c.NonQuery("update documents set docName= REPLACE(docName ,N'معاد - ','') where docID= " + docID);
            }
            return res;
        }
    }
}