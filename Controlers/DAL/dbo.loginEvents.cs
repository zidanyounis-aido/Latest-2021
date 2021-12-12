using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class loginEvents 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("loginEvents");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public loginEvents () { }
        public loginEvents (DataTable Table) 
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

        public string[] columnsArray = {"Int32 loginID","Int32 sysEventID","string IPAddress"};
private Int32 _fieldLoginID;
private bool _fieldLoginIDFlag = false;
public Int32 fieldLoginID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldLoginIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["loginID"]);
        else
            return _fieldLoginID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldLoginIDFlag = true;
        _fieldLoginID = value;
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

private string _fieldIPAddress;
private bool _fieldIPAddressFlag = false;
public string fieldIPAddress
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIPAddressFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["IPAddress"]);
        else
            return _fieldIPAddress;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIPAddressFlag = true;
        _fieldIPAddress = value;
        }
    }

public void reset()
{
_fieldLoginIDFlag = false;
_fieldLoginID = c.convertToInt32(_table.Rows[_currentIndex]["loginID"]);
_fieldSysEventIDFlag = false;
_fieldSysEventID = c.convertToInt32(_table.Rows[_currentIndex]["sysEventID"]);
_fieldIPAddressFlag = false;
_fieldIPAddress = c.convertToString(_table.Rows[_currentIndex]["IPAddress"]);

}
public void update()
{
op.dboUpdateLoginEventsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["loginID"]),_fieldSysEventID,_fieldIPAddress);
}

       

    }
}
}
