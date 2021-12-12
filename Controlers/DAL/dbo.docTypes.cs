using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class docTypes 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("docTypes");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public docTypes () { }
        public docTypes (DataTable Table) 
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

        public string[] columnsArray = {"Int32 docTypID","string docTypDesc","bool active","Int32 defaultWFID","string docTypDescAr","bool isTemplate","Int32 ClientId"};
private Int32 _fieldDocTypID;
private bool _fieldDocTypIDFlag = false;
public Int32 fieldDocTypID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocTypIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["docTypID"]);
        else
            return _fieldDocTypID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocTypIDFlag = true;
        _fieldDocTypID = value;
        }
    }

private string _fieldDocTypDesc;
private bool _fieldDocTypDescFlag = false;
public string fieldDocTypDesc
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocTypDescFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["docTypDesc"]);
        else
            return _fieldDocTypDesc;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocTypDescFlag = true;
        _fieldDocTypDesc = value;
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

private Int32 _fieldDefaultWFID;
private bool _fieldDefaultWFIDFlag = false;
public Int32 fieldDefaultWFID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDefaultWFIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["defaultWFID"]);
        else
            return _fieldDefaultWFID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDefaultWFIDFlag = true;
        _fieldDefaultWFID = value;
        }
    }

private string _fieldDocTypDescAr;
private bool _fieldDocTypDescArFlag = false;
public string fieldDocTypDescAr
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocTypDescArFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["docTypDescAr"]);
        else
            return _fieldDocTypDescAr;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocTypDescArFlag = true;
        _fieldDocTypDescAr = value;
        }
    }

private bool _fieldIsTemplate;
private bool _fieldIsTemplateFlag = false;
public bool fieldIsTemplate
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIsTemplateFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["isTemplate"]);
        else
            return _fieldIsTemplate;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIsTemplateFlag = true;
        _fieldIsTemplate = value;
        }
    }

private Int32 _fieldClientId;
private bool _fieldClientIdFlag = false;
public Int32 fieldClientId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldClientIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]);
        else
            return _fieldClientId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldClientIdFlag = true;
        _fieldClientId = value;
        }
    }

public void reset()
{
_fieldDocTypIDFlag = false;
_fieldDocTypID = c.convertToInt32(_table.Rows[_currentIndex]["docTypID"]);
_fieldDocTypDescFlag = false;
_fieldDocTypDesc = c.convertToString(_table.Rows[_currentIndex]["docTypDesc"]);
_fieldActiveFlag = false;
_fieldActive = c.convertToBool(_table.Rows[_currentIndex]["active"]);
_fieldDefaultWFIDFlag = false;
_fieldDefaultWFID = c.convertToInt32(_table.Rows[_currentIndex]["defaultWFID"]);
_fieldDocTypDescArFlag = false;
_fieldDocTypDescAr = c.convertToString(_table.Rows[_currentIndex]["docTypDescAr"]);
_fieldIsTemplateFlag = false;
_fieldIsTemplate = c.convertToBool(_table.Rows[_currentIndex]["isTemplate"]);
_fieldClientIdFlag = false;
_fieldClientId = c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]);

}
public void update()
{
op.dboUpdateDocTypesByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["docTypID"]),_fieldDocTypDesc,_fieldActive,_fieldDefaultWFID,_fieldDocTypDescAr,_fieldIsTemplate,_fieldClientId);
}

       

    }
}
}
