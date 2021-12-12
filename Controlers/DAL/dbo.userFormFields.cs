using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class userFormFields 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("userFormFields");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public userFormFields () { }
        public userFormFields (DataTable Table) 
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

        public string[] columnsArray = {"Int32 userID","Int32 formID","Int32 fieldSeq","string value"};
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
_fieldUserIDFlag = false;
_fieldUserID = c.convertToInt32(_table.Rows[_currentIndex]["userID"]);
_fieldFormIDFlag = false;
_fieldFormID = c.convertToInt32(_table.Rows[_currentIndex]["formID"]);
_fieldFieldSeqFlag = false;
_fieldFieldSeq = c.convertToInt32(_table.Rows[_currentIndex]["fieldSeq"]);
_fieldValueFlag = false;
_fieldValue = c.convertToString(_table.Rows[_currentIndex]["value"]);

}
public void update()
{
op.dboUpdateUserFormFieldsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["userID"]),c.convertToInt32(_table.Rows[_currentIndex]["formID"]),c.convertToInt32(_table.Rows[_currentIndex]["fieldSeq"]),_fieldValue);
}

       

    }
}
}
