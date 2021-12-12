using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class documentsGroups 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("documentsGroups");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public documentsGroups () { }
        public documentsGroups (DataTable Table) 
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

        public string[] columnsArray = {"Int32 docGroupID","string docGroupTitle","Int32 docTypeID"};
private Int32 _fieldDocGroupID;
private bool _fieldDocGroupIDFlag = false;
public Int32 fieldDocGroupID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocGroupIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["docGroupID"]);
        else
            return _fieldDocGroupID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocGroupIDFlag = true;
        _fieldDocGroupID = value;
        }
    }

private string _fieldDocGroupTitle;
private bool _fieldDocGroupTitleFlag = false;
public string fieldDocGroupTitle
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocGroupTitleFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["docGroupTitle"]);
        else
            return _fieldDocGroupTitle;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocGroupTitleFlag = true;
        _fieldDocGroupTitle = value;
        }
    }

private Int32 _fieldDocTypeID;
private bool _fieldDocTypeIDFlag = false;
public Int32 fieldDocTypeID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocTypeIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["docTypeID"]);
        else
            return _fieldDocTypeID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocTypeIDFlag = true;
        _fieldDocTypeID = value;
        }
    }

public void reset()
{
_fieldDocGroupIDFlag = false;
_fieldDocGroupID = c.convertToInt32(_table.Rows[_currentIndex]["docGroupID"]);
_fieldDocGroupTitleFlag = false;
_fieldDocGroupTitle = c.convertToString(_table.Rows[_currentIndex]["docGroupTitle"]);
_fieldDocTypeIDFlag = false;
_fieldDocTypeID = c.convertToInt32(_table.Rows[_currentIndex]["docTypeID"]);

}
public void update()
{
op.dboUpdateDocumentsGroupsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["docGroupID"]),_fieldDocGroupTitle,_fieldDocTypeID);
}

       

    }
}
}
