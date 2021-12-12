using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class companyFolders 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("companyFolders");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public companyFolders () { }
        public companyFolders (DataTable Table) 
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

        public string[] columnsArray = {"Int32 companyID","Int32 fldrID"};
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
_fieldCompanyIDFlag = false;
_fieldCompanyID = c.convertToInt32(_table.Rows[_currentIndex]["companyID"]);
_fieldFldrIDFlag = false;
_fieldFldrID = c.convertToInt32(_table.Rows[_currentIndex]["fldrID"]);

}
public void update()
{
op.dboUpdateCompanyFoldersByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["companyID"]),c.convertToInt32(_table.Rows[_currentIndex]["fldrID"]));
}

       

    }
}
}
