using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class usersRemiders 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("usersRemiders");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public usersRemiders () { }
        public usersRemiders (DataTable Table) 
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

        public string[] columnsArray = {"Int64 reminderID","Int32 userID","Int32 metaID","Int64 docID","Int32 beforedays","bool isRemoved"};
private Int64 _fieldReminderID;
private bool _fieldReminderIDFlag = false;
public Int64 fieldReminderID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldReminderIDFlag == false)
           return c.convertToInt64(_table.Rows[_currentIndex]["reminderID"]);
        else
            return _fieldReminderID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldReminderIDFlag = true;
        _fieldReminderID = value;
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

private Int32 _fieldMetaID;
private bool _fieldMetaIDFlag = false;
public Int32 fieldMetaID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMetaIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["metaID"]);
        else
            return _fieldMetaID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMetaIDFlag = true;
        _fieldMetaID = value;
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

private Int32 _fieldBeforedays;
private bool _fieldBeforedaysFlag = false;
public Int32 fieldBeforedays
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldBeforedaysFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["beforedays"]);
        else
            return _fieldBeforedays;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldBeforedaysFlag = true;
        _fieldBeforedays = value;
        }
    }

private bool _fieldIsRemoved;
private bool _fieldIsRemovedFlag = false;
public bool fieldIsRemoved
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIsRemovedFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["isRemoved"]);
        else
            return _fieldIsRemoved;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIsRemovedFlag = true;
        _fieldIsRemoved = value;
        }
    }

public void reset()
{
_fieldReminderIDFlag = false;
_fieldReminderID = c.convertToInt64(_table.Rows[_currentIndex]["reminderID"]);
_fieldUserIDFlag = false;
_fieldUserID = c.convertToInt32(_table.Rows[_currentIndex]["userID"]);
_fieldMetaIDFlag = false;
_fieldMetaID = c.convertToInt32(_table.Rows[_currentIndex]["metaID"]);
_fieldDocIDFlag = false;
_fieldDocID = c.convertToInt64(_table.Rows[_currentIndex]["docID"]);
_fieldBeforedaysFlag = false;
_fieldBeforedays = c.convertToInt32(_table.Rows[_currentIndex]["beforedays"]);
_fieldIsRemovedFlag = false;
_fieldIsRemoved = c.convertToBool(_table.Rows[_currentIndex]["isRemoved"]);

}
public void update()
{
op.dboUpdateUsersRemidersByPrimaryKey(c.convertToInt64(_table.Rows[_currentIndex]["reminderID"]),_fieldUserID,_fieldMetaID,_fieldDocID,_fieldBeforedays,_fieldIsRemoved);
}

       

    }
}
}
