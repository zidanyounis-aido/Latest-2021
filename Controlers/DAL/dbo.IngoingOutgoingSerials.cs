using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class IngoingOutgoingSerials 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("IngoingOutgoingSerials");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public IngoingOutgoingSerials () { }
        public IngoingOutgoingSerials (DataTable Table) 
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

        public string[] columnsArray = {"Int32 Id","string SerialCode","string Serial","Int32 FolderId","Int32 Type"};
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

private string _fieldSerialCode;
private bool _fieldSerialCodeFlag = false;
public string fieldSerialCode
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSerialCodeFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["SerialCode"]);
        else
            return _fieldSerialCode;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSerialCodeFlag = true;
        _fieldSerialCode = value;
        }
    }

private string _fieldSerial;
private bool _fieldSerialFlag = false;
public string fieldSerial
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSerialFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["Serial"]);
        else
            return _fieldSerial;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSerialFlag = true;
        _fieldSerial = value;
        }
    }

private Int32 _fieldFolderId;
private bool _fieldFolderIdFlag = false;
public Int32 fieldFolderId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFolderIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["FolderId"]);
        else
            return _fieldFolderId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFolderIdFlag = true;
        _fieldFolderId = value;
        }
    }

private Int32 _fieldType;
private bool _fieldTypeFlag = false;
public Int32 fieldType
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldTypeFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["Type"]);
        else
            return _fieldType;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldTypeFlag = true;
        _fieldType = value;
        }
    }

public void reset()
{
_fieldIdFlag = false;
_fieldId = c.convertToInt32(_table.Rows[_currentIndex]["Id"]);
_fieldSerialCodeFlag = false;
_fieldSerialCode = c.convertToString(_table.Rows[_currentIndex]["SerialCode"]);
_fieldSerialFlag = false;
_fieldSerial = c.convertToString(_table.Rows[_currentIndex]["Serial"]);
_fieldFolderIdFlag = false;
_fieldFolderId = c.convertToInt32(_table.Rows[_currentIndex]["FolderId"]);
_fieldTypeFlag = false;
_fieldType = c.convertToInt32(_table.Rows[_currentIndex]["Type"]);

}
public void update()
{
op.dboUpdateIngoingOutgoingSerialsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["Id"]),_fieldSerialCode,_fieldSerial,_fieldFolderId,_fieldType);
}

       

    }
}
}
