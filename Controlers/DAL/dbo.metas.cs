using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class metas 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("metas");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public metas () { }
        public metas (DataTable Table) 
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

        public string[] columnsArray = {"Int32 metaID","Int32 docTypID","string metaDesc","string metaDataType","bool required","Int32 orderSeq","Int32 ctrlID","string defaultTexts","string defaultValues","bool visible","string metaDescAr","Int32 columnSeq","string permissionType","string defaultArTexts","Int32 metaIdFK","Decimal width"};
private Int32 _fieldMetaID;
private bool _fieldMetaIDFlag = false;
public Int32 fieldMetaID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMetaIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["metaID"]);
        else
            return _fieldMetaID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMetaIDFlag = true;
        _fieldMetaID = value;
        }
    }

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

private string _fieldMetaDesc;
private bool _fieldMetaDescFlag = false;
public string fieldMetaDesc
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMetaDescFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["metaDesc"]);
        else
            return _fieldMetaDesc;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMetaDescFlag = true;
        _fieldMetaDesc = value;
        }
    }

private string _fieldMetaDataType;
private bool _fieldMetaDataTypeFlag = false;
public string fieldMetaDataType
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMetaDataTypeFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["metaDataType"]);
        else
            return _fieldMetaDataType;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMetaDataTypeFlag = true;
        _fieldMetaDataType = value;
        }
    }

private bool _fieldRequired;
private bool _fieldRequiredFlag = false;
public bool fieldRequired
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldRequiredFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["required"]);
        else
            return _fieldRequired;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldRequiredFlag = true;
        _fieldRequired = value;
        }
    }

private Int32 _fieldOrderSeq;
private bool _fieldOrderSeqFlag = false;
public Int32 fieldOrderSeq
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldOrderSeqFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["orderSeq"]);
        else
            return _fieldOrderSeq;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldOrderSeqFlag = true;
        _fieldOrderSeq = value;
        }
    }

private Int32 _fieldCtrlID;
private bool _fieldCtrlIDFlag = false;
public Int32 fieldCtrlID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCtrlIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["ctrlID"]);
        else
            return _fieldCtrlID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCtrlIDFlag = true;
        _fieldCtrlID = value;
        }
    }

private string _fieldDefaultTexts;
private bool _fieldDefaultTextsFlag = false;
public string fieldDefaultTexts
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDefaultTextsFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["defaultTexts"]);
        else
            return _fieldDefaultTexts;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDefaultTextsFlag = true;
        _fieldDefaultTexts = value;
        }
    }

private string _fieldDefaultValues;
private bool _fieldDefaultValuesFlag = false;
public string fieldDefaultValues
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDefaultValuesFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["defaultValues"]);
        else
            return _fieldDefaultValues;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDefaultValuesFlag = true;
        _fieldDefaultValues = value;
        }
    }

private bool _fieldVisible;
private bool _fieldVisibleFlag = false;
public bool fieldVisible
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldVisibleFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["visible"]);
        else
            return _fieldVisible;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldVisibleFlag = true;
        _fieldVisible = value;
        }
    }

private string _fieldMetaDescAr;
private bool _fieldMetaDescArFlag = false;
public string fieldMetaDescAr
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMetaDescArFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["metaDescAr"]);
        else
            return _fieldMetaDescAr;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMetaDescArFlag = true;
        _fieldMetaDescAr = value;
        }
    }

private Int32 _fieldColumnSeq;
private bool _fieldColumnSeqFlag = false;
public Int32 fieldColumnSeq
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldColumnSeqFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["columnSeq"]);
        else
            return _fieldColumnSeq;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldColumnSeqFlag = true;
        _fieldColumnSeq = value;
        }
    }

private string _fieldPermissionType;
private bool _fieldPermissionTypeFlag = false;
public string fieldPermissionType
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPermissionTypeFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["permissionType"]);
        else
            return _fieldPermissionType;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPermissionTypeFlag = true;
        _fieldPermissionType = value;
        }
    }

private string _fieldDefaultArTexts;
private bool _fieldDefaultArTextsFlag = false;
public string fieldDefaultArTexts
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDefaultArTextsFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["defaultArTexts"]);
        else
            return _fieldDefaultArTexts;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDefaultArTextsFlag = true;
        _fieldDefaultArTexts = value;
        }
    }

private Int32 _fieldMetaIdFK;
private bool _fieldMetaIdFKFlag = false;
public Int32 fieldMetaIdFK
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMetaIdFKFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["metaIdFK"]);
        else
            return _fieldMetaIdFK;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMetaIdFKFlag = true;
        _fieldMetaIdFK = value;
        }
    }

private Decimal _fieldWidth;
private bool _fieldWidthFlag = false;
public Decimal fieldWidth
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWidthFlag == false)
           return c.convertToDecimal(_table.Rows[_currentIndex]["width"]);
        else
            return _fieldWidth;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWidthFlag = true;
        _fieldWidth = value;
        }
    }

public void reset()
{
_fieldMetaIDFlag = false;
_fieldMetaID = c.convertToInt32(_table.Rows[_currentIndex]["metaID"]);
_fieldDocTypIDFlag = false;
_fieldDocTypID = c.convertToInt32(_table.Rows[_currentIndex]["docTypID"]);
_fieldMetaDescFlag = false;
_fieldMetaDesc = c.convertToString(_table.Rows[_currentIndex]["metaDesc"]);
_fieldMetaDataTypeFlag = false;
_fieldMetaDataType = c.convertToString(_table.Rows[_currentIndex]["metaDataType"]);
_fieldRequiredFlag = false;
_fieldRequired = c.convertToBool(_table.Rows[_currentIndex]["required"]);
_fieldOrderSeqFlag = false;
_fieldOrderSeq = c.convertToInt32(_table.Rows[_currentIndex]["orderSeq"]);
_fieldCtrlIDFlag = false;
_fieldCtrlID = c.convertToInt32(_table.Rows[_currentIndex]["ctrlID"]);
_fieldDefaultTextsFlag = false;
_fieldDefaultTexts = c.convertToString(_table.Rows[_currentIndex]["defaultTexts"]);
_fieldDefaultValuesFlag = false;
_fieldDefaultValues = c.convertToString(_table.Rows[_currentIndex]["defaultValues"]);
_fieldVisibleFlag = false;
_fieldVisible = c.convertToBool(_table.Rows[_currentIndex]["visible"]);
_fieldMetaDescArFlag = false;
_fieldMetaDescAr = c.convertToString(_table.Rows[_currentIndex]["metaDescAr"]);
_fieldColumnSeqFlag = false;
_fieldColumnSeq = c.convertToInt32(_table.Rows[_currentIndex]["columnSeq"]);
_fieldPermissionTypeFlag = false;
_fieldPermissionType = c.convertToString(_table.Rows[_currentIndex]["permissionType"]);
_fieldDefaultArTextsFlag = false;
_fieldDefaultArTexts = c.convertToString(_table.Rows[_currentIndex]["defaultArTexts"]);
_fieldMetaIdFKFlag = false;
_fieldMetaIdFK = c.convertToInt32(_table.Rows[_currentIndex]["metaIdFK"]);
_fieldWidthFlag = false;
_fieldWidth = c.convertToDecimal(_table.Rows[_currentIndex]["width"]);

}
public void update()
{
op.dboUpdateMetasByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["metaID"]),_fieldDocTypID,_fieldMetaDesc,_fieldMetaDataType,_fieldRequired,_fieldOrderSeq,_fieldCtrlID,_fieldDefaultTexts,_fieldDefaultValues,_fieldVisible,_fieldMetaDescAr,_fieldColumnSeq,_fieldPermissionType,_fieldDefaultArTexts,_fieldMetaIdFK,_fieldWidth);
}

       

    }
}
}
