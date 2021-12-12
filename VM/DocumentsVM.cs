using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace dms.VM
{
    public class DocumentsVM
    {
        public int docID { get; set; }
        public string typeId { get; set; }
        public string Color { get; set; }
        public string docName { get; set; }
        public int? docTypID { get; set; }
        public int? fldrID { get; set; }
        public DateTime? addedDate { get; set; }
        public int addedUserID { get; set; }
        public int docTypeSeq { get; set; }
        public int WfStatus { get; set; }
        public DateTime? WfStartDateTime { get; set; }
        public decimal? WfTimeFrame { get; set; }
        public DateTime? modifyDate { get; set; }
        public DateTime? submitDate { get; set; }
        public string statusName { get; set; }
        public string Meta2 { get; set; }
        public string Meta4 { get; set; }
        public string LeftTime { get; set; }
        public int duration { get; set; }
        public int durationType { get; set; }
    }
    public class InboxVM
    {
        public int ID { get; set; }
        public string docTypDesc { get; set; }
        public string docTypDescAr { get; set; }
        public string docName { get; set; }
        public int? docID { get; set; }
        public DateTime? addedDate { get; set; }
        public string Color { get; set; }
        public DateTime? receiveDate { get; set; }
        public string receiveDateStr { get; set; }
        public string Culture { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? submitDate { get; set; }
        public string statusName { get; set; }
        public string LeftTime { get; set; }
        public int duration { get; set; }
        public int durationType { get; set; }
        public int isDelay { get; set; }
        public DataTable FilesList { get; set; }
        public int versionCount { get; set; }
    }
    public class GetAllDocumentLateVM
    {
        public long docID { get; set; }
        public int? DelayTime { get; set; }
        public int? docTypID { get; set; }
        public string docName { get; set; }
        public string docExt { get; set; }
        public DateTime? addedDate { get; set; }
        public int? addedUserID { get; set; }
        public short? lastVersion { get; set; }
        public DateTime? modifyDate { get; set; }
        public int? modifyUserID { get; set; }
        public int? fldrID { get; set; }
        public string ocrContent { get; set; }
        public long? folderSeq { get; set; }
        public long? docTypeSeq { get; set; }
        public long? folderDocTypeSeq { get; set; }
        public int? wfPathID { get; set; }
        public short? wfCurrentSeq { get; set; }
        public int? wfCurrentRecipientID { get; set; }
        public short? wfCurrentRecipientType { get; set; }
        public DateTime? wfStartDateTime { get; set; }
        public decimal? wfTimeFrame { get; set; }
        public short? wfStatus { get; set; }
        public string meta1 { get; set; }
        public string meta2 { get; set; }
        public string meta3 { get; set; }
        public string meta4 { get; set; }
        public string meta5 { get; set; }
        public string meta6 { get; set; }
        public string meta7 { get; set; }
        public string meta8 { get; set; }
        public string meta9 { get; set; }
        public string meta10 { get; set; }
        public string meta11 { get; set; }
        public string meta12 { get; set; }
        public string meta13 { get; set; }
        public string meta14 { get; set; }
        public string meta15 { get; set; }
        public string meta16 { get; set; }
        public string meta17 { get; set; }
        public string meta18 { get; set; }
        public string meta19 { get; set; }
        public string meta20 { get; set; }
        public string meta21 { get; set; }
        public string meta22 { get; set; }
        public string meta23 { get; set; }
        public string meta24 { get; set; }
        public string meta25 { get; set; }
        public string meta26 { get; set; }
        public string meta27 { get; set; }
        public string meta28 { get; set; }
        public string meta29 { get; set; }
        public string meta30 { get; set; }
        public int? statusId { get; set; }
        public string docTypDesc { get; set; }
        public string docTypDescAr { get; set; }
        public int recipientID { get; set; }
        public short recipientType { get; set; }
        public string userName { get; set; }
    }
}