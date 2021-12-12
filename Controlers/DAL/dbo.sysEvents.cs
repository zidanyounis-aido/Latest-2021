using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class sysEvents 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("sysEvents");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public sysEvents () { }
        public sysEvents (DataTable Table) 
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

        public string[] columnsArray = {"Int32 sysEventID","Int32 userID","Int32 eventTypeID","DateTime eventDateTime","string URL"};
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

private Int32 _fieldEventTypeID;
private bool _fieldEventTypeIDFlag = false;
public Int32 fieldEventTypeID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldEventTypeIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["eventTypeID"]);
        else
            return _fieldEventTypeID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldEventTypeIDFlag = true;
        _fieldEventTypeID = value;
        }
    }

private DateTime _fieldEventDateTime;
private bool _fieldEventDateTimeFlag = false;
public DateTime fieldEventDateTime
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldEventDateTimeFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["eventDateTime"]);
        else
            return _fieldEventDateTime;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldEventDateTimeFlag = true;
        _fieldEventDateTime = value;
        }
    }

private string _fieldURL;
private bool _fieldURLFlag = false;
public string fieldURL
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldURLFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["URL"]);
        else
            return _fieldURL;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldURLFlag = true;
        _fieldURL = value;
        }
    }

public void reset()
{
_fieldSysEventIDFlag = false;
_fieldSysEventID = c.convertToInt32(_table.Rows[_currentIndex]["sysEventID"]);
_fieldUserIDFlag = false;
_fieldUserID = c.convertToInt32(_table.Rows[_currentIndex]["userID"]);
_fieldEventTypeIDFlag = false;
_fieldEventTypeID = c.convertToInt32(_table.Rows[_currentIndex]["eventTypeID"]);
_fieldEventDateTimeFlag = false;
_fieldEventDateTime = c.convertToDateTime(_table.Rows[_currentIndex]["eventDateTime"]);
_fieldURLFlag = false;
_fieldURL = c.convertToString(_table.Rows[_currentIndex]["URL"]);

}
public void update()
{
op.dboUpdateSysEventsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["sysEventID"]),_fieldUserID,_fieldEventTypeID,_fieldEventDateTime,_fieldURL);
}

       

    }
}
}
