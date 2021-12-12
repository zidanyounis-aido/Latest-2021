using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class sysSettings 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("sysSettings");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public sysSettings () { }
        public sysSettings (DataTable Table) 
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

        public string[] columnsArray = {"Int16 ID","string setting","string value","string description","Int32 ClientId"};
private Int16 _fieldID;
private bool _fieldIDFlag = false;
public Int16 fieldID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIDFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["ID"]);
        else
            return _fieldID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIDFlag = true;
        _fieldID = value;
        }
    }

private string _fieldSetting;
private bool _fieldSettingFlag = false;
public string fieldSetting
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSettingFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["setting"]);
        else
            return _fieldSetting;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSettingFlag = true;
        _fieldSetting = value;
        }
    }

private string _fieldValue;
private bool _fieldValueFlag = false;
public string fieldValue
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldValueFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["value"]);
        else
            return _fieldValue;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldValueFlag = true;
        _fieldValue = value;
        }
    }

private string _fieldDescription;
private bool _fieldDescriptionFlag = false;
public string fieldDescription
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDescriptionFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["description"]);
        else
            return _fieldDescription;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDescriptionFlag = true;
        _fieldDescription = value;
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
_fieldIDFlag = false;
_fieldID = c.convertToInt16(_table.Rows[_currentIndex]["ID"]);
_fieldSettingFlag = false;
_fieldSetting = c.convertToString(_table.Rows[_currentIndex]["setting"]);
_fieldValueFlag = false;
_fieldValue = c.convertToString(_table.Rows[_currentIndex]["value"]);
_fieldDescriptionFlag = false;
_fieldDescription = c.convertToString(_table.Rows[_currentIndex]["description"]);
_fieldClientIdFlag = false;
_fieldClientId = c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]);

}
public void update()
{
op.dboUpdateSysSettingsByPrimaryKey(c.convertToInt16(_table.Rows[_currentIndex]["ID"]),_fieldSetting,_fieldValue,_fieldDescription,_fieldClientId);
}

       

    }
}
}
