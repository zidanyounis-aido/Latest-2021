using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class branchFolders 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("branchFolders");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public branchFolders () { }
        public branchFolders (DataTable Table) 
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

        public string[] columnsArray = {"Int32 branchID","Int32 fldrID"};
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

public void reset()
{
_fieldBranchIDFlag = false;
_fieldBranchID = c.convertToInt32(_table.Rows[_currentIndex]["branchID"]);
_fieldFldrIDFlag = false;
_fieldFldrID = c.convertToInt32(_table.Rows[_currentIndex]["fldrID"]);

}
public void update()
{
op.dboUpdateBranchFoldersByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["branchID"]),c.convertToInt32(_table.Rows[_currentIndex]["fldrID"]));
}

       

    }
}
}
