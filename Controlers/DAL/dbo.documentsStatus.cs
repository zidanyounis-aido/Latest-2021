using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class documentsStatus 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("documentsStatus");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public documentsStatus () { }
        public documentsStatus (DataTable Table) 
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

        public string[] columnsArray = {"Int32 statusId","string statusName","string statusNameEN","string color"};
private Int32 _fieldStatusId;
private bool _fieldStatusIdFlag = false;
public Int32 fieldStatusId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldStatusIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["statusId"]);
        else
            return _fieldStatusId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldStatusIdFlag = true;
        _fieldStatusId = value;
        }
    }

private string _fieldStatusName;
private bool _fieldStatusNameFlag = false;
public string fieldStatusName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldStatusNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["statusName"]);
        else
            return _fieldStatusName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldStatusNameFlag = true;
        _fieldStatusName = value;
        }
    }

private string _fieldStatusNameEN;
private bool _fieldStatusNameENFlag = false;
public string fieldStatusNameEN
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldStatusNameENFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["statusNameEN"]);
        else
            return _fieldStatusNameEN;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldStatusNameENFlag = true;
        _fieldStatusNameEN = value;
        }
    }

private string _fieldColor;
private bool _fieldColorFlag = false;
public string fieldColor
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldColorFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["color"]);
        else
            return _fieldColor;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldColorFlag = true;
        _fieldColor = value;
        }
    }

public void reset()
{
_fieldStatusIdFlag = false;
_fieldStatusId = c.convertToInt32(_table.Rows[_currentIndex]["statusId"]);
_fieldStatusNameFlag = false;
_fieldStatusName = c.convertToString(_table.Rows[_currentIndex]["statusName"]);
_fieldStatusNameENFlag = false;
_fieldStatusNameEN = c.convertToString(_table.Rows[_currentIndex]["statusNameEN"]);
_fieldColorFlag = false;
_fieldColor = c.convertToString(_table.Rows[_currentIndex]["color"]);

}
public void update()
{
op.dboUpdateDocumentsStatusByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["statusId"]),_fieldStatusName,_fieldStatusNameEN,_fieldColor);
}

       

    }
}
}
