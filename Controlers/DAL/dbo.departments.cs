using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class departments 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("departments");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public departments () { }
        public departments (DataTable Table) 
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

        public string[] columnsArray = {"Int32 departmentID","string departmentName","Int32 headUserID","Int32 parentDepartmentID","string departmentNameAr","Int32 parentID","Int32 ClientId"};
private Int32 _fieldDepartmentID;
private bool _fieldDepartmentIDFlag = false;
public Int32 fieldDepartmentID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDepartmentIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["departmentID"]);
        else
            return _fieldDepartmentID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDepartmentIDFlag = true;
        _fieldDepartmentID = value;
        }
    }

private string _fieldDepartmentName;
private bool _fieldDepartmentNameFlag = false;
public string fieldDepartmentName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDepartmentNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["departmentName"]);
        else
            return _fieldDepartmentName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDepartmentNameFlag = true;
        _fieldDepartmentName = value;
        }
    }

private Int32 _fieldHeadUserID;
private bool _fieldHeadUserIDFlag = false;
public Int32 fieldHeadUserID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldHeadUserIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["headUserID"]);
        else
            return _fieldHeadUserID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldHeadUserIDFlag = true;
        _fieldHeadUserID = value;
        }
    }

private Int32 _fieldParentDepartmentID;
private bool _fieldParentDepartmentIDFlag = false;
public Int32 fieldParentDepartmentID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldParentDepartmentIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["parentDepartmentID"]);
        else
            return _fieldParentDepartmentID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldParentDepartmentIDFlag = true;
        _fieldParentDepartmentID = value;
        }
    }

private string _fieldDepartmentNameAr;
private bool _fieldDepartmentNameArFlag = false;
public string fieldDepartmentNameAr
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDepartmentNameArFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["departmentNameAr"]);
        else
            return _fieldDepartmentNameAr;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDepartmentNameArFlag = true;
        _fieldDepartmentNameAr = value;
        }
    }

private Int32 _fieldParentID;
private bool _fieldParentIDFlag = false;
public Int32 fieldParentID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldParentIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["parentID"]);
        else
            return _fieldParentID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldParentIDFlag = true;
        _fieldParentID = value;
        }
    }

private Int32 _fieldClientId;
private bool _fieldClientIdFlag = false;
public Int32 fieldClientId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldClientIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]);
        else
            return _fieldClientId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldClientIdFlag = true;
        _fieldClientId = value;
        }
    }

public void reset()
{
_fieldDepartmentIDFlag = false;
_fieldDepartmentID = c.convertToInt32(_table.Rows[_currentIndex]["departmentID"]);
_fieldDepartmentNameFlag = false;
_fieldDepartmentName = c.convertToString(_table.Rows[_currentIndex]["departmentName"]);
_fieldHeadUserIDFlag = false;
_fieldHeadUserID = c.convertToInt32(_table.Rows[_currentIndex]["headUserID"]);
_fieldParentDepartmentIDFlag = false;
_fieldParentDepartmentID = c.convertToInt32(_table.Rows[_currentIndex]["parentDepartmentID"]);
_fieldDepartmentNameArFlag = false;
_fieldDepartmentNameAr = c.convertToString(_table.Rows[_currentIndex]["departmentNameAr"]);
_fieldParentIDFlag = false;
_fieldParentID = c.convertToInt32(_table.Rows[_currentIndex]["parentID"]);
_fieldClientIdFlag = false;
_fieldClientId = c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]);

}
public void update()
{
op.dboUpdateDepartmentsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["departmentID"]),_fieldDepartmentName,_fieldHeadUserID,_fieldParentDepartmentID,_fieldDepartmentNameAr,_fieldParentID,_fieldClientId);
}

       

    }
}
}
