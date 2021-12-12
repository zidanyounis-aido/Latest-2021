using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class eFormFields 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("eFormFields");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public eFormFields () { }
        public eFormFields (DataTable Table) 
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

        public string[] columnsArray = {"Int32 formID","Int32 fieldSeq","string metaDesc","string metaDataType","bool required","Int32 orderSeq","Int32 ctrlID","string defaultTexts","string defaultValues","bool visible"};
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

private Int32 _fieldFieldSeq;
private bool _fieldFieldSeqFlag = false;
public Int32 fieldFieldSeq
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFieldSeqFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["fieldSeq"]);
        else
            return _fieldFieldSeq;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFieldSeqFlag = true;
        _fieldFieldSeq = value;
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

public void reset()
{
_fieldFormIDFlag = false;
_fieldFormID = c.convertToInt32(_table.Rows[_currentIndex]["formID"]);
_fieldFieldSeqFlag = false;
_fieldFieldSeq = c.convertToInt32(_table.Rows[_currentIndex]["fieldSeq"]);
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

}
public void update()
{
op.dboUpdateEFormFieldsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["formID"]),c.convertToInt32(_table.Rows[_currentIndex]["fieldSeq"]),_fieldMetaDesc,_fieldMetaDataType,_fieldRequired,_fieldOrderSeq,_fieldCtrlID,_fieldDefaultTexts,_fieldDefaultValues,_fieldVisible);
}

       

    }
}
}
