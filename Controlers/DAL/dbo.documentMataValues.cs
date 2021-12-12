using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class documentMataValues 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("documentMataValues");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public documentMataValues () { }
        public documentMataValues (DataTable Table) 
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

        public string[] columnsArray = {"Int32 metaID","Int64 docID","string value"};
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

private Int64 _fieldDocID;
private bool _fieldDocIDFlag = false;
public Int64 fieldDocID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocIDFlag == false)
           return c.convertToInt64(_table.Rows[_currentIndex]["docID"]);
        else
            return _fieldDocID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocIDFlag = true;
        _fieldDocID = value;
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

public void reset()
{
_fieldMetaIDFlag = false;
_fieldMetaID = c.convertToInt32(_table.Rows[_currentIndex]["metaID"]);
_fieldDocIDFlag = false;
_fieldDocID = c.convertToInt64(_table.Rows[_currentIndex]["docID"]);
_fieldValueFlag = false;
_fieldValue = c.convertToString(_table.Rows[_currentIndex]["value"]);

}
public void update()
{
op.dboUpdateDocumentMataValuesByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["metaID"]),c.convertToInt64(_table.Rows[_currentIndex]["docID"]),_fieldValue);
}

       

    }
}
}
