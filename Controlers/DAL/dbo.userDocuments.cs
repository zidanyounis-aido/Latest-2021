using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class userDocuments 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("userDocuments");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public userDocuments () { }
        public userDocuments (DataTable Table) 
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

        public string[] columnsArray = {"Int32 userID","Int64 docID","bool allow","bool allowInsert","bool allowUpdate","bool allowDelete"};
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

private bool _fieldAllow;
private bool _fieldAllowFlag = false;
public bool fieldAllow
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["allow"]);
        else
            return _fieldAllow;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowFlag = true;
        _fieldAllow = value;
        }
    }

private bool _fieldAllowInsert;
private bool _fieldAllowInsertFlag = false;
public bool fieldAllowInsert
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowInsertFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["allowInsert"]);
        else
            return _fieldAllowInsert;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowInsertFlag = true;
        _fieldAllowInsert = value;
        }
    }

private bool _fieldAllowUpdate;
private bool _fieldAllowUpdateFlag = false;
public bool fieldAllowUpdate
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowUpdateFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["allowUpdate"]);
        else
            return _fieldAllowUpdate;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowUpdateFlag = true;
        _fieldAllowUpdate = value;
        }
    }

private bool _fieldAllowDelete;
private bool _fieldAllowDeleteFlag = false;
public bool fieldAllowDelete
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowDeleteFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["allowDelete"]);
        else
            return _fieldAllowDelete;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowDeleteFlag = true;
        _fieldAllowDelete = value;
        }
    }

public void reset()
{
_fieldUserIDFlag = false;
_fieldUserID = c.convertToInt32(_table.Rows[_currentIndex]["userID"]);
_fieldDocIDFlag = false;
_fieldDocID = c.convertToInt64(_table.Rows[_currentIndex]["docID"]);
_fieldAllowFlag = false;
_fieldAllow = c.convertToBool(_table.Rows[_currentIndex]["allow"]);
_fieldAllowInsertFlag = false;
_fieldAllowInsert = c.convertToBool(_table.Rows[_currentIndex]["allowInsert"]);
_fieldAllowUpdateFlag = false;
_fieldAllowUpdate = c.convertToBool(_table.Rows[_currentIndex]["allowUpdate"]);
_fieldAllowDeleteFlag = false;
_fieldAllowDelete = c.convertToBool(_table.Rows[_currentIndex]["allowDelete"]);

}
public void update()
{
op.dboUpdateUserDocumentsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["userID"]),c.convertToInt64(_table.Rows[_currentIndex]["docID"]),_fieldAllow,_fieldAllowInsert,_fieldAllowUpdate,_fieldAllowDelete);
}

       

    }
}
}
