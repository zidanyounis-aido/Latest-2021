using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class eventsTypes 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("eventsTypes");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public eventsTypes () { }
        public eventsTypes (DataTable Table) 
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

        public string[] columnsArray = {"Int32 eventTypeID","string eventTypeDescA","string eventTypeDescE"};
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

private string _fieldEventTypeDescA;
private bool _fieldEventTypeDescAFlag = false;
public string fieldEventTypeDescA
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldEventTypeDescAFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["eventTypeDescA"]);
        else
            return _fieldEventTypeDescA;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldEventTypeDescAFlag = true;
        _fieldEventTypeDescA = value;
        }
    }

private string _fieldEventTypeDescE;
private bool _fieldEventTypeDescEFlag = false;
public string fieldEventTypeDescE
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldEventTypeDescEFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["eventTypeDescE"]);
        else
            return _fieldEventTypeDescE;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldEventTypeDescEFlag = true;
        _fieldEventTypeDescE = value;
        }
    }

public void reset()
{
_fieldEventTypeIDFlag = false;
_fieldEventTypeID = c.convertToInt32(_table.Rows[_currentIndex]["eventTypeID"]);
_fieldEventTypeDescAFlag = false;
_fieldEventTypeDescA = c.convertToString(_table.Rows[_currentIndex]["eventTypeDescA"]);
_fieldEventTypeDescEFlag = false;
_fieldEventTypeDescE = c.convertToString(_table.Rows[_currentIndex]["eventTypeDescE"]);

}
public void update()
{
op.dboUpdateEventsTypesByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["eventTypeID"]),_fieldEventTypeDescA,_fieldEventTypeDescE);
}

       

    }
}
}
