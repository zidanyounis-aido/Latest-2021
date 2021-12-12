using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class documents 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("documents");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public documents () { }
        public documents (DataTable Table) 
        {
            table = Table;
        }

        private Int32 _currentIndex;

        public Int32 currentIndex
        {
            get { return _currentIndex; }
            set { _currentIndex = value; }
        }

        public Int32 moveNext()
        {
            if (_currentIndex < _table.Rows.Count - 1)
            {
                _currentIndex += 1;
            }
            return _currentIndex;
        }

        public Int32 movePrevious()
        {
            if (_currentIndex > 0)
            {
                _currentIndex += 1;
            }
            return _currentIndex;
        }

        public Int32 moveLast()
        {
            _currentIndex = _table.Rows.Count - 1;
            return _currentIndex;
        }

        public Int32 moveFirst()
        {
            _currentIndex = 0;
            return _currentIndex;
        }

		 public Int32 rowsCount
        {
            get { return _table.Rows.Count; }
        }

        public bool hasRows
        {
            get {
                if (_table.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public string[] columnsArray = {"Int64 docID","Int32 docTypID","string docName","string docExt","DateTime addedDate","Int32 addedUserID","Int16 lastVersion","DateTime modifyDate","Int32 modifyUserID","Int32 fldrID","string ocrContent","Int64 folderSeq","Int64 docTypeSeq","Int64 folderDocTypeSeq","Int32 wfPathID","Int16 wfCurrentSeq","Int32 wfCurrentRecipientID","Int16 wfCurrentRecipientType","DateTime wfStartDateTime","Decimal wfTimeFrame","Int16 wfStatus","string meta1","string meta2","string meta3","string meta4","string meta5","string meta6","string meta7","string meta8","string meta9","string meta10","string meta11","string meta12","string meta13","string meta14","string meta15","string meta16","string meta17","string meta18","string meta19","string meta20","string meta21","string meta22","string meta23","string meta24","string meta25","string meta26","string meta27","string meta28","string meta29","string meta30","Int32 statusId","DateTime submitDate","Int32 DelayTime","Int32 typeId","string serial","DateTime outgoingDate"};
private Int64 _fieldDocID;
private bool _fieldDocIDFlag = false;
public Int64 fieldDocID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocIDFlag == false)
           return c.convertToInt64(_table.Rows[_currentIndex]["docID"]);
        else
            return _fieldDocID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocIDFlag = true;
        _fieldDocID = value;
        }
    }

private Int32 _fieldDocTypID;
private bool _fieldDocTypIDFlag = false;
public Int32 fieldDocTypID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocTypIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["docTypID"]);
        else
            return _fieldDocTypID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocTypIDFlag = true;
        _fieldDocTypID = value;
        }
    }

private string _fieldDocName;
private bool _fieldDocNameFlag = false;
public string fieldDocName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["docName"]);
        else
            return _fieldDocName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocNameFlag = true;
        _fieldDocName = value;
        }
    }

private string _fieldDocExt;
private bool _fieldDocExtFlag = false;
public string fieldDocExt
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocExtFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["docExt"]);
        else
            return _fieldDocExt;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocExtFlag = true;
        _fieldDocExt = value;
        }
    }

private DateTime _fieldAddedDate;
private bool _fieldAddedDateFlag = false;
public DateTime fieldAddedDate
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAddedDateFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["addedDate"]);
        else
            return _fieldAddedDate;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAddedDateFlag = true;
        _fieldAddedDate = value;
        }
    }

private Int32 _fieldAddedUserID;
private bool _fieldAddedUserIDFlag = false;
public Int32 fieldAddedUserID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAddedUserIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["addedUserID"]);
        else
            return _fieldAddedUserID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAddedUserIDFlag = true;
        _fieldAddedUserID = value;
        }
    }

private Int16 _fieldLastVersion;
private bool _fieldLastVersionFlag = false;
public Int16 fieldLastVersion
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldLastVersionFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["lastVersion"]);
        else
            return _fieldLastVersion;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldLastVersionFlag = true;
        _fieldLastVersion = value;
        }
    }

private DateTime _fieldModifyDate;
private bool _fieldModifyDateFlag = false;
public DateTime fieldModifyDate
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldModifyDateFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["modifyDate"]);
        else
            return _fieldModifyDate;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldModifyDateFlag = true;
        _fieldModifyDate = value;
        }
    }

private Int32 _fieldModifyUserID;
private bool _fieldModifyUserIDFlag = false;
public Int32 fieldModifyUserID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldModifyUserIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["modifyUserID"]);
        else
            return _fieldModifyUserID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldModifyUserIDFlag = true;
        _fieldModifyUserID = value;
        }
    }

private Int32 _fieldFldrID;
private bool _fieldFldrIDFlag = false;
public Int32 fieldFldrID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFldrIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["fldrID"]);
        else
            return _fieldFldrID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFldrIDFlag = true;
        _fieldFldrID = value;
        }
    }

private string _fieldOcrContent;
private bool _fieldOcrContentFlag = false;
public string fieldOcrContent
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldOcrContentFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["ocrContent"]);
        else
            return _fieldOcrContent;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldOcrContentFlag = true;
        _fieldOcrContent = value;
        }
    }

private Int64 _fieldFolderSeq;
private bool _fieldFolderSeqFlag = false;
public Int64 fieldFolderSeq
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFolderSeqFlag == false)
           return c.convertToInt64(_table.Rows[_currentIndex]["folderSeq"]);
        else
            return _fieldFolderSeq;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFolderSeqFlag = true;
        _fieldFolderSeq = value;
        }
    }

private Int64 _fieldDocTypeSeq;
private bool _fieldDocTypeSeqFlag = false;
public Int64 fieldDocTypeSeq
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocTypeSeqFlag == false)
           return c.convertToInt64(_table.Rows[_currentIndex]["docTypeSeq"]);
        else
            return _fieldDocTypeSeq;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocTypeSeqFlag = true;
        _fieldDocTypeSeq = value;
        }
    }

private Int64 _fieldFolderDocTypeSeq;
private bool _fieldFolderDocTypeSeqFlag = false;
public Int64 fieldFolderDocTypeSeq
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFolderDocTypeSeqFlag == false)
           return c.convertToInt64(_table.Rows[_currentIndex]["folderDocTypeSeq"]);
        else
            return _fieldFolderDocTypeSeq;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFolderDocTypeSeqFlag = true;
        _fieldFolderDocTypeSeq = value;
        }
    }

private Int32 _fieldWfPathID;
private bool _fieldWfPathIDFlag = false;
public Int32 fieldWfPathID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWfPathIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["wfPathID"]);
        else
            return _fieldWfPathID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWfPathIDFlag = true;
        _fieldWfPathID = value;
        }
    }

private Int16 _fieldWfCurrentSeq;
private bool _fieldWfCurrentSeqFlag = false;
public Int16 fieldWfCurrentSeq
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWfCurrentSeqFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["wfCurrentSeq"]);
        else
            return _fieldWfCurrentSeq;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWfCurrentSeqFlag = true;
        _fieldWfCurrentSeq = value;
        }
    }

private Int32 _fieldWfCurrentRecipientID;
private bool _fieldWfCurrentRecipientIDFlag = false;
public Int32 fieldWfCurrentRecipientID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWfCurrentRecipientIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["wfCurrentRecipientID"]);
        else
            return _fieldWfCurrentRecipientID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWfCurrentRecipientIDFlag = true;
        _fieldWfCurrentRecipientID = value;
        }
    }

private Int16 _fieldWfCurrentRecipientType;
private bool _fieldWfCurrentRecipientTypeFlag = false;
public Int16 fieldWfCurrentRecipientType
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWfCurrentRecipientTypeFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["wfCurrentRecipientType"]);
        else
            return _fieldWfCurrentRecipientType;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWfCurrentRecipientTypeFlag = true;
        _fieldWfCurrentRecipientType = value;
        }
    }

private DateTime _fieldWfStartDateTime;
private bool _fieldWfStartDateTimeFlag = false;
public DateTime fieldWfStartDateTime
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWfStartDateTimeFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["wfStartDateTime"]);
        else
            return _fieldWfStartDateTime;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWfStartDateTimeFlag = true;
        _fieldWfStartDateTime = value;
        }
    }

private Decimal _fieldWfTimeFrame;
private bool _fieldWfTimeFrameFlag = false;
public Decimal fieldWfTimeFrame
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWfTimeFrameFlag == false)
           return c.convertToDecimal(_table.Rows[_currentIndex]["wfTimeFrame"]);
        else
            return _fieldWfTimeFrame;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWfTimeFrameFlag = true;
        _fieldWfTimeFrame = value;
        }
    }

private Int16 _fieldWfStatus;
private bool _fieldWfStatusFlag = false;
public Int16 fieldWfStatus
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWfStatusFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["wfStatus"]);
        else
            return _fieldWfStatus;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWfStatusFlag = true;
        _fieldWfStatus = value;
        }
    }

private string _fieldMeta1;
private bool _fieldMeta1Flag = false;
public string fieldMeta1
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta1Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta1"]);
        else
            return _fieldMeta1;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta1Flag = true;
        _fieldMeta1 = value;
        }
    }

private string _fieldMeta2;
private bool _fieldMeta2Flag = false;
public string fieldMeta2
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta2Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta2"]);
        else
            return _fieldMeta2;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta2Flag = true;
        _fieldMeta2 = value;
        }
    }

private string _fieldMeta3;
private bool _fieldMeta3Flag = false;
public string fieldMeta3
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta3Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta3"]);
        else
            return _fieldMeta3;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta3Flag = true;
        _fieldMeta3 = value;
        }
    }

private string _fieldMeta4;
private bool _fieldMeta4Flag = false;
public string fieldMeta4
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta4Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta4"]);
        else
            return _fieldMeta4;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta4Flag = true;
        _fieldMeta4 = value;
        }
    }

private string _fieldMeta5;
private bool _fieldMeta5Flag = false;
public string fieldMeta5
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta5Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta5"]);
        else
            return _fieldMeta5;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta5Flag = true;
        _fieldMeta5 = value;
        }
    }

private string _fieldMeta6;
private bool _fieldMeta6Flag = false;
public string fieldMeta6
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta6Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta6"]);
        else
            return _fieldMeta6;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta6Flag = true;
        _fieldMeta6 = value;
        }
    }

private string _fieldMeta7;
private bool _fieldMeta7Flag = false;
public string fieldMeta7
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta7Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta7"]);
        else
            return _fieldMeta7;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta7Flag = true;
        _fieldMeta7 = value;
        }
    }

private string _fieldMeta8;
private bool _fieldMeta8Flag = false;
public string fieldMeta8
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta8Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta8"]);
        else
            return _fieldMeta8;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta8Flag = true;
        _fieldMeta8 = value;
        }
    }

private string _fieldMeta9;
private bool _fieldMeta9Flag = false;
public string fieldMeta9
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta9Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta9"]);
        else
            return _fieldMeta9;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta9Flag = true;
        _fieldMeta9 = value;
        }
    }

private string _fieldMeta10;
private bool _fieldMeta10Flag = false;
public string fieldMeta10
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta10Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta10"]);
        else
            return _fieldMeta10;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta10Flag = true;
        _fieldMeta10 = value;
        }
    }

private string _fieldMeta11;
private bool _fieldMeta11Flag = false;
public string fieldMeta11
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta11Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta11"]);
        else
            return _fieldMeta11;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta11Flag = true;
        _fieldMeta11 = value;
        }
    }

private string _fieldMeta12;
private bool _fieldMeta12Flag = false;
public string fieldMeta12
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta12Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta12"]);
        else
            return _fieldMeta12;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta12Flag = true;
        _fieldMeta12 = value;
        }
    }

private string _fieldMeta13;
private bool _fieldMeta13Flag = false;
public string fieldMeta13
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta13Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta13"]);
        else
            return _fieldMeta13;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta13Flag = true;
        _fieldMeta13 = value;
        }
    }

private string _fieldMeta14;
private bool _fieldMeta14Flag = false;
public string fieldMeta14
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta14Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta14"]);
        else
            return _fieldMeta14;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta14Flag = true;
        _fieldMeta14 = value;
        }
    }

private string _fieldMeta15;
private bool _fieldMeta15Flag = false;
public string fieldMeta15
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta15Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta15"]);
        else
            return _fieldMeta15;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta15Flag = true;
        _fieldMeta15 = value;
        }
    }

private string _fieldMeta16;
private bool _fieldMeta16Flag = false;
public string fieldMeta16
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta16Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta16"]);
        else
            return _fieldMeta16;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta16Flag = true;
        _fieldMeta16 = value;
        }
    }

private string _fieldMeta17;
private bool _fieldMeta17Flag = false;
public string fieldMeta17
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta17Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta17"]);
        else
            return _fieldMeta17;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta17Flag = true;
        _fieldMeta17 = value;
        }
    }

private string _fieldMeta18;
private bool _fieldMeta18Flag = false;
public string fieldMeta18
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta18Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta18"]);
        else
            return _fieldMeta18;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta18Flag = true;
        _fieldMeta18 = value;
        }
    }

private string _fieldMeta19;
private bool _fieldMeta19Flag = false;
public string fieldMeta19
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta19Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta19"]);
        else
            return _fieldMeta19;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta19Flag = true;
        _fieldMeta19 = value;
        }
    }

private string _fieldMeta20;
private bool _fieldMeta20Flag = false;
public string fieldMeta20
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta20Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta20"]);
        else
            return _fieldMeta20;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta20Flag = true;
        _fieldMeta20 = value;
        }
    }

private string _fieldMeta21;
private bool _fieldMeta21Flag = false;
public string fieldMeta21
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta21Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta21"]);
        else
            return _fieldMeta21;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta21Flag = true;
        _fieldMeta21 = value;
        }
    }

private string _fieldMeta22;
private bool _fieldMeta22Flag = false;
public string fieldMeta22
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta22Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta22"]);
        else
            return _fieldMeta22;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta22Flag = true;
        _fieldMeta22 = value;
        }
    }

private string _fieldMeta23;
private bool _fieldMeta23Flag = false;
public string fieldMeta23
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta23Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta23"]);
        else
            return _fieldMeta23;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta23Flag = true;
        _fieldMeta23 = value;
        }
    }

private string _fieldMeta24;
private bool _fieldMeta24Flag = false;
public string fieldMeta24
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta24Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta24"]);
        else
            return _fieldMeta24;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta24Flag = true;
        _fieldMeta24 = value;
        }
    }

private string _fieldMeta25;
private bool _fieldMeta25Flag = false;
public string fieldMeta25
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta25Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta25"]);
        else
            return _fieldMeta25;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta25Flag = true;
        _fieldMeta25 = value;
        }
    }

private string _fieldMeta26;
private bool _fieldMeta26Flag = false;
public string fieldMeta26
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta26Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta26"]);
        else
            return _fieldMeta26;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta26Flag = true;
        _fieldMeta26 = value;
        }
    }

private string _fieldMeta27;
private bool _fieldMeta27Flag = false;
public string fieldMeta27
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta27Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta27"]);
        else
            return _fieldMeta27;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta27Flag = true;
        _fieldMeta27 = value;
        }
    }

private string _fieldMeta28;
private bool _fieldMeta28Flag = false;
public string fieldMeta28
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta28Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta28"]);
        else
            return _fieldMeta28;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta28Flag = true;
        _fieldMeta28 = value;
        }
    }

private string _fieldMeta29;
private bool _fieldMeta29Flag = false;
public string fieldMeta29
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta29Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta29"]);
        else
            return _fieldMeta29;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta29Flag = true;
        _fieldMeta29 = value;
        }
    }

private string _fieldMeta30;
private bool _fieldMeta30Flag = false;
public string fieldMeta30
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMeta30Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["meta30"]);
        else
            return _fieldMeta30;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMeta30Flag = true;
        _fieldMeta30 = value;
        }
    }

private Int32 _fieldStatusId;
private bool _fieldStatusIdFlag = false;
public Int32 fieldStatusId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldStatusIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["statusId"]);
        else
            return _fieldStatusId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldStatusIdFlag = true;
        _fieldStatusId = value;
        }
    }

private DateTime _fieldSubmitDate;
private bool _fieldSubmitDateFlag = false;
public DateTime fieldSubmitDate
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSubmitDateFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["submitDate"]);
        else
            return _fieldSubmitDate;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSubmitDateFlag = true;
        _fieldSubmitDate = value;
        }
    }

private Int32 _fieldDelayTime;
private bool _fieldDelayTimeFlag = false;
public Int32 fieldDelayTime
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDelayTimeFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["DelayTime"]);
        else
            return _fieldDelayTime;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDelayTimeFlag = true;
        _fieldDelayTime = value;
        }
    }

private Int32 _fieldTypeId;
private bool _fieldTypeIdFlag = false;
public Int32 fieldTypeId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldTypeIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["typeId"]);
        else
            return _fieldTypeId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldTypeIdFlag = true;
        _fieldTypeId = value;
        }
    }

private string _fieldSerial;
private bool _fieldSerialFlag = false;
public string fieldSerial
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSerialFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["serial"]);
        else
            return _fieldSerial;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSerialFlag = true;
        _fieldSerial = value;
        }
    }

private DateTime _fieldOutgoingDate;
private bool _fieldOutgoingDateFlag = false;
public DateTime fieldOutgoingDate
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldOutgoingDateFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["outgoingDate"]);
        else
            return _fieldOutgoingDate;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldOutgoingDateFlag = true;
        _fieldOutgoingDate = value;
        }
    }

public void reset()
{
_fieldDocIDFlag = false;
_fieldDocID = c.convertToInt64(_table.Rows[_currentIndex]["docID"]);
_fieldDocTypIDFlag = false;
_fieldDocTypID = c.convertToInt32(_table.Rows[_currentIndex]["docTypID"]);
_fieldDocNameFlag = false;
_fieldDocName = c.convertToString(_table.Rows[_currentIndex]["docName"]);
_fieldDocExtFlag = false;
_fieldDocExt = c.convertToString(_table.Rows[_currentIndex]["docExt"]);
_fieldAddedDateFlag = false;
_fieldAddedDate = c.convertToDateTime(_table.Rows[_currentIndex]["addedDate"]);
_fieldAddedUserIDFlag = false;
_fieldAddedUserID = c.convertToInt32(_table.Rows[_currentIndex]["addedUserID"]);
_fieldLastVersionFlag = false;
_fieldLastVersion = c.convertToInt16(_table.Rows[_currentIndex]["lastVersion"]);
_fieldModifyDateFlag = false;
_fieldModifyDate = c.convertToDateTime(_table.Rows[_currentIndex]["modifyDate"]);
_fieldModifyUserIDFlag = false;
_fieldModifyUserID = c.convertToInt32(_table.Rows[_currentIndex]["modifyUserID"]);
_fieldFldrIDFlag = false;
_fieldFldrID = c.convertToInt32(_table.Rows[_currentIndex]["fldrID"]);
_fieldOcrContentFlag = false;
_fieldOcrContent = c.convertToString(_table.Rows[_currentIndex]["ocrContent"]);
_fieldFolderSeqFlag = false;
_fieldFolderSeq = c.convertToInt64(_table.Rows[_currentIndex]["folderSeq"]);
_fieldDocTypeSeqFlag = false;
_fieldDocTypeSeq = c.convertToInt64(_table.Rows[_currentIndex]["docTypeSeq"]);
_fieldFolderDocTypeSeqFlag = false;
_fieldFolderDocTypeSeq = c.convertToInt64(_table.Rows[_currentIndex]["folderDocTypeSeq"]);
_fieldWfPathIDFlag = false;
_fieldWfPathID = c.convertToInt32(_table.Rows[_currentIndex]["wfPathID"]);
_fieldWfCurrentSeqFlag = false;
_fieldWfCurrentSeq = c.convertToInt16(_table.Rows[_currentIndex]["wfCurrentSeq"]);
_fieldWfCurrentRecipientIDFlag = false;
_fieldWfCurrentRecipientID = c.convertToInt32(_table.Rows[_currentIndex]["wfCurrentRecipientID"]);
_fieldWfCurrentRecipientTypeFlag = false;
_fieldWfCurrentRecipientType = c.convertToInt16(_table.Rows[_currentIndex]["wfCurrentRecipientType"]);
_fieldWfStartDateTimeFlag = false;
_fieldWfStartDateTime = c.convertToDateTime(_table.Rows[_currentIndex]["wfStartDateTime"]);
_fieldWfTimeFrameFlag = false;
_fieldWfTimeFrame = c.convertToDecimal(_table.Rows[_currentIndex]["wfTimeFrame"]);
_fieldWfStatusFlag = false;
_fieldWfStatus = c.convertToInt16(_table.Rows[_currentIndex]["wfStatus"]);
_fieldMeta1Flag = false;
_fieldMeta1 = c.convertToString(_table.Rows[_currentIndex]["meta1"]);
_fieldMeta2Flag = false;
_fieldMeta2 = c.convertToString(_table.Rows[_currentIndex]["meta2"]);
_fieldMeta3Flag = false;
_fieldMeta3 = c.convertToString(_table.Rows[_currentIndex]["meta3"]);
_fieldMeta4Flag = false;
_fieldMeta4 = c.convertToString(_table.Rows[_currentIndex]["meta4"]);
_fieldMeta5Flag = false;
_fieldMeta5 = c.convertToString(_table.Rows[_currentIndex]["meta5"]);
_fieldMeta6Flag = false;
_fieldMeta6 = c.convertToString(_table.Rows[_currentIndex]["meta6"]);
_fieldMeta7Flag = false;
_fieldMeta7 = c.convertToString(_table.Rows[_currentIndex]["meta7"]);
_fieldMeta8Flag = false;
_fieldMeta8 = c.convertToString(_table.Rows[_currentIndex]["meta8"]);
_fieldMeta9Flag = false;
_fieldMeta9 = c.convertToString(_table.Rows[_currentIndex]["meta9"]);
_fieldMeta10Flag = false;
_fieldMeta10 = c.convertToString(_table.Rows[_currentIndex]["meta10"]);
_fieldMeta11Flag = false;
_fieldMeta11 = c.convertToString(_table.Rows[_currentIndex]["meta11"]);
_fieldMeta12Flag = false;
_fieldMeta12 = c.convertToString(_table.Rows[_currentIndex]["meta12"]);
_fieldMeta13Flag = false;
_fieldMeta13 = c.convertToString(_table.Rows[_currentIndex]["meta13"]);
_fieldMeta14Flag = false;
_fieldMeta14 = c.convertToString(_table.Rows[_currentIndex]["meta14"]);
_fieldMeta15Flag = false;
_fieldMeta15 = c.convertToString(_table.Rows[_currentIndex]["meta15"]);
_fieldMeta16Flag = false;
_fieldMeta16 = c.convertToString(_table.Rows[_currentIndex]["meta16"]);
_fieldMeta17Flag = false;
_fieldMeta17 = c.convertToString(_table.Rows[_currentIndex]["meta17"]);
_fieldMeta18Flag = false;
_fieldMeta18 = c.convertToString(_table.Rows[_currentIndex]["meta18"]);
_fieldMeta19Flag = false;
_fieldMeta19 = c.convertToString(_table.Rows[_currentIndex]["meta19"]);
_fieldMeta20Flag = false;
_fieldMeta20 = c.convertToString(_table.Rows[_currentIndex]["meta20"]);
_fieldMeta21Flag = false;
_fieldMeta21 = c.convertToString(_table.Rows[_currentIndex]["meta21"]);
_fieldMeta22Flag = false;
_fieldMeta22 = c.convertToString(_table.Rows[_currentIndex]["meta22"]);
_fieldMeta23Flag = false;
_fieldMeta23 = c.convertToString(_table.Rows[_currentIndex]["meta23"]);
_fieldMeta24Flag = false;
_fieldMeta24 = c.convertToString(_table.Rows[_currentIndex]["meta24"]);
_fieldMeta25Flag = false;
_fieldMeta25 = c.convertToString(_table.Rows[_currentIndex]["meta25"]);
_fieldMeta26Flag = false;
_fieldMeta26 = c.convertToString(_table.Rows[_currentIndex]["meta26"]);
_fieldMeta27Flag = false;
_fieldMeta27 = c.convertToString(_table.Rows[_currentIndex]["meta27"]);
_fieldMeta28Flag = false;
_fieldMeta28 = c.convertToString(_table.Rows[_currentIndex]["meta28"]);
_fieldMeta29Flag = false;
_fieldMeta29 = c.convertToString(_table.Rows[_currentIndex]["meta29"]);
_fieldMeta30Flag = false;
_fieldMeta30 = c.convertToString(_table.Rows[_currentIndex]["meta30"]);
_fieldStatusIdFlag = false;
_fieldStatusId = c.convertToInt32(_table.Rows[_currentIndex]["statusId"]);
_fieldSubmitDateFlag = false;
_fieldSubmitDate = c.convertToDateTime(_table.Rows[_currentIndex]["submitDate"]);
_fieldDelayTimeFlag = false;
_fieldDelayTime = c.convertToInt32(_table.Rows[_currentIndex]["DelayTime"]);
_fieldTypeIdFlag = false;
_fieldTypeId = c.convertToInt32(_table.Rows[_currentIndex]["typeId"]);
_fieldSerialFlag = false;
_fieldSerial = c.convertToString(_table.Rows[_currentIndex]["serial"]);
_fieldOutgoingDateFlag = false;
_fieldOutgoingDate = c.convertToDateTime(_table.Rows[_currentIndex]["outgoingDate"]);

}
public void update()
{
op.dboUpdateDocumentsByPrimaryKey(c.convertToInt64(_table.Rows[_currentIndex]["docID"]),_fieldDocTypID,_fieldDocName,_fieldDocExt,_fieldAddedDate,_fieldAddedUserID,_fieldLastVersion,_fieldModifyDate,_fieldModifyUserID,_fieldFldrID,_fieldOcrContent,_fieldFolderSeq,_fieldDocTypeSeq,_fieldFolderDocTypeSeq,_fieldWfPathID,_fieldWfCurrentSeq,_fieldWfCurrentRecipientID,_fieldWfCurrentRecipientType,_fieldWfStartDateTime,_fieldWfTimeFrame,_fieldWfStatus,_fieldMeta1,_fieldMeta2,_fieldMeta3,_fieldMeta4,_fieldMeta5,_fieldMeta6,_fieldMeta7,_fieldMeta8,_fieldMeta9,_fieldMeta10,_fieldMeta11,_fieldMeta12,_fieldMeta13,_fieldMeta14,_fieldMeta15,_fieldMeta16,_fieldMeta17,_fieldMeta18,_fieldMeta19,_fieldMeta20,_fieldMeta21,_fieldMeta22,_fieldMeta23,_fieldMeta24,_fieldMeta25,_fieldMeta26,_fieldMeta27,_fieldMeta28,_fieldMeta29,_fieldMeta30,_fieldStatusId,_fieldSubmitDate,_fieldDelayTime,_fieldTypeId,_fieldSerial,_fieldOutgoingDate);
}

       

    }
}
}
