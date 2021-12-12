using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class groups 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("groups");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public groups () { }
        public groups (DataTable Table) 
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

        public string[] columnsArray = {"Int32 grpID","string grpDesc","Int32 companyID","Int32 branchID","Int32 ClientId"};
private Int32 _fieldGrpID;
private bool _fieldGrpIDFlag = false;
public Int32 fieldGrpID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldGrpIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["grpID"]);
        else
            return _fieldGrpID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldGrpIDFlag = true;
        _fieldGrpID = value;
        }
    }

private string _fieldGrpDesc;
private bool _fieldGrpDescFlag = false;
public string fieldGrpDesc
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldGrpDescFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["grpDesc"]);
        else
            return _fieldGrpDesc;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldGrpDescFlag = true;
        _fieldGrpDesc = value;
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
_fieldGrpIDFlag = false;
_fieldGrpID = c.convertToInt32(_table.Rows[_currentIndex]["grpID"]);
_fieldGrpDescFlag = false;
_fieldGrpDesc = c.convertToString(_table.Rows[_currentIndex]["grpDesc"]);
_fieldCompanyIDFlag = false;
_fieldCompanyID = c.convertToInt32(_table.Rows[_currentIndex]["companyID"]);
_fieldBranchIDFlag = false;
_fieldBranchID = c.convertToInt32(_table.Rows[_currentIndex]["branchID"]);
_fieldClientIdFlag = false;
_fieldClientId = c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]);

}
public void update()
{
op.dboUpdateGroupsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["grpID"]),_fieldGrpDesc,_fieldCompanyID,_fieldBranchID,_fieldClientId);
}

       

    }
}
}
