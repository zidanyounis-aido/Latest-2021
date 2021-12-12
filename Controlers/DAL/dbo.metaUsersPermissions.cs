using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class metaUsersPermissions 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("metaUsersPermissions");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public metaUsersPermissions () { }
        public metaUsersPermissions (DataTable Table) 
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

        public string[] columnsArray = {"Int32 metaID","Int32 userID","bool allowRead","bool allowEdit"};
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

private bool _fieldAllowRead;
private bool _fieldAllowReadFlag = false;
public bool fieldAllowRead
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowReadFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["allowRead"]);
        else
            return _fieldAllowRead;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowReadFlag = true;
        _fieldAllowRead = value;
        }
    }

private bool _fieldAllowEdit;
private bool _fieldAllowEditFlag = false;
public bool fieldAllowEdit
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowEditFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["allowEdit"]);
        else
            return _fieldAllowEdit;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowEditFlag = true;
        _fieldAllowEdit = value;
        }
    }

public void reset()
{
_fieldMetaIDFlag = false;
_fieldMetaID = c.convertToInt32(_table.Rows[_currentIndex]["metaID"]);
_fieldUserIDFlag = false;
_fieldUserID = c.convertToInt32(_table.Rows[_currentIndex]["userID"]);
_fieldAllowReadFlag = false;
_fieldAllowRead = c.convertToBool(_table.Rows[_currentIndex]["allowRead"]);
_fieldAllowEditFlag = false;
_fieldAllowEdit = c.convertToBool(_table.Rows[_currentIndex]["allowEdit"]);

}
public void update()
{
op.dboUpdateMetaUsersPermissionsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["metaID"]),c.convertToInt32(_table.Rows[_currentIndex]["userID"]),_fieldAllowRead,_fieldAllowEdit);
}

       

    }
}
}
