using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class documentWFPathDelayed 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("documentWFPathDelayed");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public documentWFPathDelayed () { }
        public documentWFPathDelayed (DataTable Table) 
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

        public string[] columnsArray = {"Int32 ID","Int64 docID","Int32 userID","DateTime actionDateTime","Int32 wfPathID","Int16 wfSeqNo","Int16 actionType","Int16 recipientType","string userNotes","DateTime receiveDate","DateTime EndDate","Int32 inboxType","Int32 documentWFPathId"};
private Int32 _fieldID;
private bool _fieldIDFlag = false;
public Int32 fieldID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["ID"]);
        else
            return _fieldID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIDFlag = true;
        _fieldID = value;
        }
    }

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

private Int32 _fieldUserID;
private bool _fieldUserIDFlag = false;
public Int32 fieldUserID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldUserIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["userID"]);
        else
            return _fieldUserID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldUserIDFlag = true;
        _fieldUserID = value;
        }
    }

private DateTime _fieldActionDateTime;
private bool _fieldActionDateTimeFlag = false;
public DateTime fieldActionDateTime
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldActionDateTimeFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["actionDateTime"]);
        else
            return _fieldActionDateTime;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldActionDateTimeFlag = true;
        _fieldActionDateTime = value;
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

private Int16 _fieldWfSeqNo;
private bool _fieldWfSeqNoFlag = false;
public Int16 fieldWfSeqNo
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWfSeqNoFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["wfSeqNo"]);
        else
            return _fieldWfSeqNo;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWfSeqNoFlag = true;
        _fieldWfSeqNo = value;
        }
    }

private Int16 _fieldActionType;
private bool _fieldActionTypeFlag = false;
public Int16 fieldActionType
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldActionTypeFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["actionType"]);
        else
            return _fieldActionType;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldActionTypeFlag = true;
        _fieldActionType = value;
        }
    }

private Int16 _fieldRecipientType;
private bool _fieldRecipientTypeFlag = false;
public Int16 fieldRecipientType
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldRecipientTypeFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["recipientType"]);
        else
            return _fieldRecipientType;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldRecipientTypeFlag = true;
        _fieldRecipientType = value;
        }
    }

private string _fieldUserNotes;
private bool _fieldUserNotesFlag = false;
public string fieldUserNotes
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldUserNotesFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["userNotes"]);
        else
            return _fieldUserNotes;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldUserNotesFlag = true;
        _fieldUserNotes = value;
        }
    }

private DateTime _fieldReceiveDate;
private bool _fieldReceiveDateFlag = false;
public DateTime fieldReceiveDate
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldReceiveDateFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["receiveDate"]);
        else
            return _fieldReceiveDate;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldReceiveDateFlag = true;
        _fieldReceiveDate = value;
        }
    }

private DateTime _fieldEndDate;
private bool _fieldEndDateFlag = false;
public DateTime fieldEndDate
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldEndDateFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["EndDate"]);
        else
            return _fieldEndDate;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldEndDateFlag = true;
        _fieldEndDate = value;
        }
    }

private Int32 _fieldInboxType;
private bool _fieldInboxTypeFlag = false;
public Int32 fieldInboxType
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldInboxTypeFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["inboxType"]);
        else
            return _fieldInboxType;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldInboxTypeFlag = true;
        _fieldInboxType = value;
        }
    }

private Int32 _fieldDocumentWFPathId;
private bool _fieldDocumentWFPathIdFlag = false;
public Int32 fieldDocumentWFPathId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocumentWFPathIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["documentWFPathId"]);
        else
            return _fieldDocumentWFPathId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocumentWFPathIdFlag = true;
        _fieldDocumentWFPathId = value;
        }
    }

public void reset()
{
_fieldIDFlag = false;
_fieldID = c.convertToInt32(_table.Rows[_currentIndex]["ID"]);
_fieldDocIDFlag = false;
_fieldDocID = c.convertToInt64(_table.Rows[_currentIndex]["docID"]);
_fieldUserIDFlag = false;
_fieldUserID = c.convertToInt32(_table.Rows[_currentIndex]["userID"]);
_fieldActionDateTimeFlag = false;
_fieldActionDateTime = c.convertToDateTime(_table.Rows[_currentIndex]["actionDateTime"]);
_fieldWfPathIDFlag = false;
_fieldWfPathID = c.convertToInt32(_table.Rows[_currentIndex]["wfPathID"]);
_fieldWfSeqNoFlag = false;
_fieldWfSeqNo = c.convertToInt16(_table.Rows[_currentIndex]["wfSeqNo"]);
_fieldActionTypeFlag = false;
_fieldActionType = c.convertToInt16(_table.Rows[_currentIndex]["actionType"]);
_fieldRecipientTypeFlag = false;
_fieldRecipientType = c.convertToInt16(_table.Rows[_currentIndex]["recipientType"]);
_fieldUserNotesFlag = false;
_fieldUserNotes = c.convertToString(_table.Rows[_currentIndex]["userNotes"]);
_fieldReceiveDateFlag = false;
_fieldReceiveDate = c.convertToDateTime(_table.Rows[_currentIndex]["receiveDate"]);
_fieldEndDateFlag = false;
_fieldEndDate = c.convertToDateTime(_table.Rows[_currentIndex]["EndDate"]);
_fieldInboxTypeFlag = false;
_fieldInboxType = c.convertToInt32(_table.Rows[_currentIndex]["inboxType"]);
_fieldDocumentWFPathIdFlag = false;
_fieldDocumentWFPathId = c.convertToInt32(_table.Rows[_currentIndex]["documentWFPathId"]);

}
public void update()
{
op.dboUpdateDocumentWFPathDelayedByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["ID"]),_fieldDocID,_fieldUserID,_fieldActionDateTime,_fieldWfPathID,_fieldWfSeqNo,_fieldActionType,_fieldRecipientType,_fieldUserNotes,_fieldReceiveDate,_fieldEndDate,_fieldInboxType,_fieldDocumentWFPathId);
}

       

    }
}
}
