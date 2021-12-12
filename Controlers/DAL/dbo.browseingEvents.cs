using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class browseingEvents 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("browseingEvents");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public browseingEvents () { }
        public browseingEvents (DataTable Table) 
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

        public string[] columnsArray = {"Int32 browseEventID","Int32 sysEventID","Int32 pageID"};
private Int32 _fieldBrowseEventID;
private bool _fieldBrowseEventIDFlag = false;
public Int32 fieldBrowseEventID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldBrowseEventIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["browseEventID"]);
        else
            return _fieldBrowseEventID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldBrowseEventIDFlag = true;
        _fieldBrowseEventID = value;
        }
    }

private Int32 _fieldSysEventID;
private bool _fieldSysEventIDFlag = false;
public Int32 fieldSysEventID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSysEventIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["sysEventID"]);
        else
            return _fieldSysEventID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSysEventIDFlag = true;
        _fieldSysEventID = value;
        }
    }

private Int32 _fieldPageID;
private bool _fieldPageIDFlag = false;
public Int32 fieldPageID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPageIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["pageID"]);
        else
            return _fieldPageID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPageIDFlag = true;
        _fieldPageID = value;
        }
    }

public void reset()
{
_fieldBrowseEventIDFlag = false;
_fieldBrowseEventID = c.convertToInt32(_table.Rows[_currentIndex]["browseEventID"]);
_fieldSysEventIDFlag = false;
_fieldSysEventID = c.convertToInt32(_table.Rows[_currentIndex]["sysEventID"]);
_fieldPageIDFlag = false;
_fieldPageID = c.convertToInt32(_table.Rows[_currentIndex]["pageID"]);

}
public void update()
{
op.dboUpdateBrowseingEventsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["browseEventID"]),_fieldSysEventID,_fieldPageID);
}

       

    }
}
}
