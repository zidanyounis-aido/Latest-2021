using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class eformsCategories 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("eformsCategories");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public eformsCategories () { }
        public eformsCategories (DataTable Table) 
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

        public string[] columnsArray = {"Int32 catID","string catTitle","Int32 catPrgID"};
private Int32 _fieldCatID;
private bool _fieldCatIDFlag = false;
public Int32 fieldCatID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCatIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["catID"]);
        else
            return _fieldCatID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCatIDFlag = true;
        _fieldCatID = value;
        }
    }

private string _fieldCatTitle;
private bool _fieldCatTitleFlag = false;
public string fieldCatTitle
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCatTitleFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["catTitle"]);
        else
            return _fieldCatTitle;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCatTitleFlag = true;
        _fieldCatTitle = value;
        }
    }

private Int32 _fieldCatPrgID;
private bool _fieldCatPrgIDFlag = false;
public Int32 fieldCatPrgID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCatPrgIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["catPrgID"]);
        else
            return _fieldCatPrgID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCatPrgIDFlag = true;
        _fieldCatPrgID = value;
        }
    }

public void reset()
{
_fieldCatIDFlag = false;
_fieldCatID = c.convertToInt32(_table.Rows[_currentIndex]["catID"]);
_fieldCatTitleFlag = false;
_fieldCatTitle = c.convertToString(_table.Rows[_currentIndex]["catTitle"]);
_fieldCatPrgIDFlag = false;
_fieldCatPrgID = c.convertToInt32(_table.Rows[_currentIndex]["catPrgID"]);

}
public void update()
{
op.dboUpdateEformsCategoriesByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["catID"]),_fieldCatTitle,_fieldCatPrgID);
}

       

    }
}
}
