using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class eForms 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("eForms");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public eForms () { }
        public eForms (DataTable Table) 
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

        public string[] columnsArray = {"Int32 formID","string formName","Int32 defaultPathID","bool active","Int32 catID","Int32 catPrgID","string formNameAr"};
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

private string _fieldFormName;
private bool _fieldFormNameFlag = false;
public string fieldFormName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFormNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["formName"]);
        else
            return _fieldFormName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFormNameFlag = true;
        _fieldFormName = value;
        }
    }

private Int32 _fieldDefaultPathID;
private bool _fieldDefaultPathIDFlag = false;
public Int32 fieldDefaultPathID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDefaultPathIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["defaultPathID"]);
        else
            return _fieldDefaultPathID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDefaultPathIDFlag = true;
        _fieldDefaultPathID = value;
        }
    }

private bool _fieldActive;
private bool _fieldActiveFlag = false;
public bool fieldActive
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldActiveFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["active"]);
        else
            return _fieldActive;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldActiveFlag = true;
        _fieldActive = value;
        }
    }

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

private string _fieldFormNameAr;
private bool _fieldFormNameArFlag = false;
public string fieldFormNameAr
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFormNameArFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["formNameAr"]);
        else
            return _fieldFormNameAr;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFormNameArFlag = true;
        _fieldFormNameAr = value;
        }
    }

public void reset()
{
_fieldFormIDFlag = false;
_fieldFormID = c.convertToInt32(_table.Rows[_currentIndex]["formID"]);
_fieldFormNameFlag = false;
_fieldFormName = c.convertToString(_table.Rows[_currentIndex]["formName"]);
_fieldDefaultPathIDFlag = false;
_fieldDefaultPathID = c.convertToInt32(_table.Rows[_currentIndex]["defaultPathID"]);
_fieldActiveFlag = false;
_fieldActive = c.convertToBool(_table.Rows[_currentIndex]["active"]);
_fieldCatIDFlag = false;
_fieldCatID = c.convertToInt32(_table.Rows[_currentIndex]["catID"]);
_fieldCatPrgIDFlag = false;
_fieldCatPrgID = c.convertToInt32(_table.Rows[_currentIndex]["catPrgID"]);
_fieldFormNameArFlag = false;
_fieldFormNameAr = c.convertToString(_table.Rows[_currentIndex]["formNameAr"]);

}
public void update()
{
op.dboUpdateEFormsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["formID"]),_fieldFormName,_fieldDefaultPathID,_fieldActive,_fieldCatID,_fieldCatPrgID,_fieldFormNameAr);
}

       

    }
}
}
