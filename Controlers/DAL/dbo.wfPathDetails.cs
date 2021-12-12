using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class wfPathDetails 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("wfPathDetails");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public wfPathDetails () { }
        public wfPathDetails (DataTable Table) 
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

        public string[] columnsArray = {"Int32 pathID","Int16 seqNo","Int32 recipientID","bool endOfPath","Int16 recipientType","Int32 companyID","Int32 branchID","Int16 approveType","Int32 id","Int32 duration","Int32 durationType"};
private Int32 _fieldPathID;
private bool _fieldPathIDFlag = false;
public Int32 fieldPathID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPathIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["pathID"]);
        else
            return _fieldPathID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPathIDFlag = true;
        _fieldPathID = value;
        }
    }

private Int16 _fieldSeqNo;
private bool _fieldSeqNoFlag = false;
public Int16 fieldSeqNo
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSeqNoFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["seqNo"]);
        else
            return _fieldSeqNo;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSeqNoFlag = true;
        _fieldSeqNo = value;
        }
    }

private Int32 _fieldRecipientID;
private bool _fieldRecipientIDFlag = false;
public Int32 fieldRecipientID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldRecipientIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["recipientID"]);
        else
            return _fieldRecipientID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldRecipientIDFlag = true;
        _fieldRecipientID = value;
        }
    }

private bool _fieldEndOfPath;
private bool _fieldEndOfPathFlag = false;
public bool fieldEndOfPath
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldEndOfPathFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["endOfPath"]);
        else
            return _fieldEndOfPath;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldEndOfPathFlag = true;
        _fieldEndOfPath = value;
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

private Int32 _fieldCompanyID;
private bool _fieldCompanyIDFlag = false;
public Int32 fieldCompanyID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCompanyIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["companyID"]);
        else
            return _fieldCompanyID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCompanyIDFlag = true;
        _fieldCompanyID = value;
        }
    }

private Int32 _fieldBranchID;
private bool _fieldBranchIDFlag = false;
public Int32 fieldBranchID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldBranchIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["branchID"]);
        else
            return _fieldBranchID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldBranchIDFlag = true;
        _fieldBranchID = value;
        }
    }

private Int16 _fieldApproveType;
private bool _fieldApproveTypeFlag = false;
public Int16 fieldApproveType
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldApproveTypeFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["approveType"]);
        else
            return _fieldApproveType;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldApproveTypeFlag = true;
        _fieldApproveType = value;
        }
    }

private Int32 _fieldId;
private bool _fieldIdFlag = false;
public Int32 fieldId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["id"]);
        else
            return _fieldId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIdFlag = true;
        _fieldId = value;
        }
    }

private Int32 _fieldDuration;
private bool _fieldDurationFlag = false;
public Int32 fieldDuration
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDurationFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["duration"]);
        else
            return _fieldDuration;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDurationFlag = true;
        _fieldDuration = value;
        }
    }

private Int32 _fieldDurationType;
private bool _fieldDurationTypeFlag = false;
public Int32 fieldDurationType
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDurationTypeFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["durationType"]);
        else
            return _fieldDurationType;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDurationTypeFlag = true;
        _fieldDurationType = value;
        }
    }

public void reset()
{
_fieldPathIDFlag = false;
_fieldPathID = c.convertToInt32(_table.Rows[_currentIndex]["pathID"]);
_fieldSeqNoFlag = false;
_fieldSeqNo = c.convertToInt16(_table.Rows[_currentIndex]["seqNo"]);
_fieldRecipientIDFlag = false;
_fieldRecipientID = c.convertToInt32(_table.Rows[_currentIndex]["recipientID"]);
_fieldEndOfPathFlag = false;
_fieldEndOfPath = c.convertToBool(_table.Rows[_currentIndex]["endOfPath"]);
_fieldRecipientTypeFlag = false;
_fieldRecipientType = c.convertToInt16(_table.Rows[_currentIndex]["recipientType"]);
_fieldCompanyIDFlag = false;
_fieldCompanyID = c.convertToInt32(_table.Rows[_currentIndex]["companyID"]);
_fieldBranchIDFlag = false;
_fieldBranchID = c.convertToInt32(_table.Rows[_currentIndex]["branchID"]);
_fieldApproveTypeFlag = false;
_fieldApproveType = c.convertToInt16(_table.Rows[_currentIndex]["approveType"]);
_fieldIdFlag = false;
_fieldId = c.convertToInt32(_table.Rows[_currentIndex]["id"]);
_fieldDurationFlag = false;
_fieldDuration = c.convertToInt32(_table.Rows[_currentIndex]["duration"]);
_fieldDurationTypeFlag = false;
_fieldDurationType = c.convertToInt32(_table.Rows[_currentIndex]["durationType"]);

}
public void update()
{
op.dboUpdateWfPathDetailsByPrimaryKey(_fieldPathID,_fieldSeqNo,_fieldRecipientID,_fieldEndOfPath,_fieldRecipientType,_fieldCompanyID,_fieldBranchID,_fieldApproveType,c.convertToInt32(_table.Rows[_currentIndex]["id"]),_fieldDuration,_fieldDurationType);
}

       

    }
}
}
