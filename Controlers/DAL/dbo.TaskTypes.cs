using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class TaskTypes 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("TaskTypes");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public TaskTypes () { }
        public TaskTypes (DataTable Table) 
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

        public string[] columnsArray = {"Int32 Id","string ArText","string EnText","string Code"};
private Int32 _fieldId;
private bool _fieldIdFlag = false;
public Int32 fieldId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["Id"]);
        else
            return _fieldId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIdFlag = true;
        _fieldId = value;
        }
    }

private string _fieldArText;
private bool _fieldArTextFlag = false;
public string fieldArText
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldArTextFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["ArText"]);
        else
            return _fieldArText;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldArTextFlag = true;
        _fieldArText = value;
        }
    }

private string _fieldEnText;
private bool _fieldEnTextFlag = false;
public string fieldEnText
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldEnTextFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["EnText"]);
        else
            return _fieldEnText;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldEnTextFlag = true;
        _fieldEnText = value;
        }
    }

private string _fieldCode;
private bool _fieldCodeFlag = false;
public string fieldCode
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCodeFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["Code"]);
        else
            return _fieldCode;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCodeFlag = true;
        _fieldCode = value;
        }
    }

public void reset()
{
_fieldIdFlag = false;
_fieldId = c.convertToInt32(_table.Rows[_currentIndex]["Id"]);
_fieldArTextFlag = false;
_fieldArText = c.convertToString(_table.Rows[_currentIndex]["ArText"]);
_fieldEnTextFlag = false;
_fieldEnText = c.convertToString(_table.Rows[_currentIndex]["EnText"]);
_fieldCodeFlag = false;
_fieldCode = c.convertToString(_table.Rows[_currentIndex]["Code"]);

}
public void update()
{
op.dboUpdateTaskTypesByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["Id"]),_fieldArText,_fieldEnText,_fieldCode);
}

       

    }
}
}
