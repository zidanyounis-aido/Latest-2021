using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class userFolders 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("userFolders");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public userFolders () { }
        public userFolders (DataTable Table) 
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

        public string[] columnsArray = {"Int32 userID","Int32 fldrID","bool allow","bool allowInsert","bool allowUpdate","bool allowDelete","bool allowOutgoing","bool allowIncoming","bool inheritSubFolders"};
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

private bool _fieldAllowOutgoing;
private bool _fieldAllowOutgoingFlag = false;
public bool fieldAllowOutgoing
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowOutgoingFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["allowOutgoing"]);
        else
            return _fieldAllowOutgoing;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowOutgoingFlag = true;
        _fieldAllowOutgoing = value;
        }
    }

private bool _fieldAllowIncoming;
private bool _fieldAllowIncomingFlag = false;
public bool fieldAllowIncoming
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowIncomingFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["allowIncoming"]);
        else
            return _fieldAllowIncoming;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowIncomingFlag = true;
        _fieldAllowIncoming = value;
        }
    }

private bool _fieldInheritSubFolders;
private bool _fieldInheritSubFoldersFlag = false;
public bool fieldInheritSubFolders
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldInheritSubFoldersFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["inheritSubFolders"]);
        else
            return _fieldInheritSubFolders;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldInheritSubFoldersFlag = true;
        _fieldInheritSubFolders = value;
        }
    }

public void reset()
{
_fieldUserIDFlag = false;
_fieldUserID = c.convertToInt32(_table.Rows[_currentIndex]["userID"]);
_fieldFldrIDFlag = false;
_fieldFldrID = c.convertToInt32(_table.Rows[_currentIndex]["fldrID"]);
_fieldAllowFlag = false;
_fieldAllow = c.convertToBool(_table.Rows[_currentIndex]["allow"]);
_fieldAllowInsertFlag = false;
_fieldAllowInsert = c.convertToBool(_table.Rows[_currentIndex]["allowInsert"]);
_fieldAllowUpdateFlag = false;
_fieldAllowUpdate = c.convertToBool(_table.Rows[_currentIndex]["allowUpdate"]);
_fieldAllowDeleteFlag = false;
_fieldAllowDelete = c.convertToBool(_table.Rows[_currentIndex]["allowDelete"]);
_fieldAllowOutgoingFlag = false;
_fieldAllowOutgoing = c.convertToBool(_table.Rows[_currentIndex]["allowOutgoing"]);
_fieldAllowIncomingFlag = false;
_fieldAllowIncoming = c.convertToBool(_table.Rows[_currentIndex]["allowIncoming"]);
_fieldInheritSubFoldersFlag = false;
_fieldInheritSubFolders = c.convertToBool(_table.Rows[_currentIndex]["inheritSubFolders"]);

}
public void update()
{
op.dboUpdateUserFoldersByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["userID"]),c.convertToInt32(_table.Rows[_currentIndex]["fldrID"]),_fieldAllow,_fieldAllowInsert,_fieldAllowUpdate,_fieldAllowDelete,_fieldAllowOutgoing,_fieldAllowIncoming,_fieldInheritSubFolders);
}

       

    }
}
}
