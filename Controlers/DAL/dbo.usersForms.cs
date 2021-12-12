using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class usersForms 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("usersForms");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public usersForms () { }
        public usersForms (DataTable Table) 
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

        public string[] columnsArray = {"Int32 userID","Int32 formID","Int32 pathID","DateTime submitDateTime","Int16 status"};
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

private Int32 _fieldFormID;
private bool _fieldFormIDFlag = false;
public Int32 fieldFormID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFormIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["formID"]);
        else
            return _fieldFormID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFormIDFlag = true;
        _fieldFormID = value;
        }
    }

private Int32 _fieldPathID;
private bool _fieldPathIDFlag = false;
public Int32 fieldPathID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPathIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["pathID"]);
        else
            return _fieldPathID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPathIDFlag = true;
        _fieldPathID = value;
        }
    }

private DateTime _fieldSubmitDateTime;
private bool _fieldSubmitDateTimeFlag = false;
public DateTime fieldSubmitDateTime
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSubmitDateTimeFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["submitDateTime"]);
        else
            return _fieldSubmitDateTime;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSubmitDateTimeFlag = true;
        _fieldSubmitDateTime = value;
        }
    }

private Int16 _fieldStatus;
private bool _fieldStatusFlag = false;
public Int16 fieldStatus
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldStatusFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["status"]);
        else
            return _fieldStatus;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldStatusFlag = true;
        _fieldStatus = value;
        }
    }

public void reset()
{
_fieldUserIDFlag = false;
_fieldUserID = c.convertToInt32(_table.Rows[_currentIndex]["userID"]);
_fieldFormIDFlag = false;
_fieldFormID = c.convertToInt32(_table.Rows[_currentIndex]["formID"]);
_fieldPathIDFlag = false;
_fieldPathID = c.convertToInt32(_table.Rows[_currentIndex]["pathID"]);
_fieldSubmitDateTimeFlag = false;
_fieldSubmitDateTime = c.convertToDateTime(_table.Rows[_currentIndex]["submitDateTime"]);
_fieldStatusFlag = false;
_fieldStatus = c.convertToInt16(_table.Rows[_currentIndex]["status"]);

}
public void update()
{
op.dboUpdateUsersFormsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["userID"]),c.convertToInt32(_table.Rows[_currentIndex]["formID"]),_fieldPathID,_fieldSubmitDateTime,_fieldStatus);
}

       

    }
}
}
