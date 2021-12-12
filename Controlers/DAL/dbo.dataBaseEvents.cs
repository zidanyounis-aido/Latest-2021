using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class dataBaseEvents 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("dataBaseEvents");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public dataBaseEvents () { }
        public dataBaseEvents (DataTable Table) 
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

        public string[] columnsArray = {"Int32 DBEventID","Int32 sysEventID","Int32 DBActionTypeID","string tableName","string parameters"};
private Int32 _fieldDBEventID;
private bool _fieldDBEventIDFlag = false;
public Int32 fieldDBEventID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDBEventIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["DBEventID"]);
        else
            return _fieldDBEventID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDBEventIDFlag = true;
        _fieldDBEventID = value;
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

private Int32 _fieldDBActionTypeID;
private bool _fieldDBActionTypeIDFlag = false;
public Int32 fieldDBActionTypeID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDBActionTypeIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["DBActionTypeID"]);
        else
            return _fieldDBActionTypeID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDBActionTypeIDFlag = true;
        _fieldDBActionTypeID = value;
        }
    }

private string _fieldTableName;
private bool _fieldTableNameFlag = false;
public string fieldTableName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldTableNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["tableName"]);
        else
            return _fieldTableName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldTableNameFlag = true;
        _fieldTableName = value;
        }
    }

private string _fieldParameters;
private bool _fieldParametersFlag = false;
public string fieldParameters
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldParametersFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["parameters"]);
        else
            return _fieldParameters;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldParametersFlag = true;
        _fieldParameters = value;
        }
    }

public void reset()
{
_fieldDBEventIDFlag = false;
_fieldDBEventID = c.convertToInt32(_table.Rows[_currentIndex]["DBEventID"]);
_fieldSysEventIDFlag = false;
_fieldSysEventID = c.convertToInt32(_table.Rows[_currentIndex]["sysEventID"]);
_fieldDBActionTypeIDFlag = false;
_fieldDBActionTypeID = c.convertToInt32(_table.Rows[_currentIndex]["DBActionTypeID"]);
_fieldTableNameFlag = false;
_fieldTableName = c.convertToString(_table.Rows[_currentIndex]["tableName"]);
_fieldParametersFlag = false;
_fieldParameters = c.convertToString(_table.Rows[_currentIndex]["parameters"]);

}
public void update()
{
op.dboUpdateDataBaseEventsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["DBEventID"]),_fieldSysEventID,_fieldDBActionTypeID,_fieldTableName,_fieldParameters);
}

       

    }
}
}
