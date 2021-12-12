using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class documentVersions 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("documentVersions");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public documentVersions () { }
        public documentVersions (DataTable Table) 
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

        public string[] columnsArray = {"Int64 docID","Int16 version","DateTime addedDate","Int32 addedUserID","string ext","Int32 docGroupID","string FileName","string DocumentFileName"};
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

private Int16 _fieldVersion;
private bool _fieldVersionFlag = false;
public Int16 fieldVersion
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldVersionFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["version"]);
        else
            return _fieldVersion;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldVersionFlag = true;
        _fieldVersion = value;
        }
    }

private DateTime _fieldAddedDate;
private bool _fieldAddedDateFlag = false;
public DateTime fieldAddedDate
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAddedDateFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["addedDate"]);
        else
            return _fieldAddedDate;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAddedDateFlag = true;
        _fieldAddedDate = value;
        }
    }

private Int32 _fieldAddedUserID;
private bool _fieldAddedUserIDFlag = false;
public Int32 fieldAddedUserID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAddedUserIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["addedUserID"]);
        else
            return _fieldAddedUserID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAddedUserIDFlag = true;
        _fieldAddedUserID = value;
        }
    }

private string _fieldExt;
private bool _fieldExtFlag = false;
public string fieldExt
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldExtFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["ext"]);
        else
            return _fieldExt;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldExtFlag = true;
        _fieldExt = value;
        }
    }

private Int32 _fieldDocGroupID;
private bool _fieldDocGroupIDFlag = false;
public Int32 fieldDocGroupID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocGroupIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["docGroupID"]);
        else
            return _fieldDocGroupID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocGroupIDFlag = true;
        _fieldDocGroupID = value;
        }
    }

private string _fieldFileName;
private bool _fieldFileNameFlag = false;
public string fieldFileName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFileNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["FileName"]);
        else
            return _fieldFileName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFileNameFlag = true;
        _fieldFileName = value;
        }
    }

private string _fieldDocumentFileName;
private bool _fieldDocumentFileNameFlag = false;
public string fieldDocumentFileName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocumentFileNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["DocumentFileName"]);
        else
            return _fieldDocumentFileName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocumentFileNameFlag = true;
        _fieldDocumentFileName = value;
        }
    }

public void reset()
{
_fieldDocIDFlag = false;
_fieldDocID = c.convertToInt64(_table.Rows[_currentIndex]["docID"]);
_fieldVersionFlag = false;
_fieldVersion = c.convertToInt16(_table.Rows[_currentIndex]["version"]);
_fieldAddedDateFlag = false;
_fieldAddedDate = c.convertToDateTime(_table.Rows[_currentIndex]["addedDate"]);
_fieldAddedUserIDFlag = false;
_fieldAddedUserID = c.convertToInt32(_table.Rows[_currentIndex]["addedUserID"]);
_fieldExtFlag = false;
_fieldExt = c.convertToString(_table.Rows[_currentIndex]["ext"]);
_fieldDocGroupIDFlag = false;
_fieldDocGroupID = c.convertToInt32(_table.Rows[_currentIndex]["docGroupID"]);
_fieldFileNameFlag = false;
_fieldFileName = c.convertToString(_table.Rows[_currentIndex]["FileName"]);
_fieldDocumentFileNameFlag = false;
_fieldDocumentFileName = c.convertToString(_table.Rows[_currentIndex]["DocumentFileName"]);

}
public void update()
{
op.dboUpdateDocumentVersionsByPrimaryKey(c.convertToInt64(_table.Rows[_currentIndex]["docID"]),c.convertToInt16(_table.Rows[_currentIndex]["version"]),_fieldAddedDate,_fieldAddedUserID,_fieldExt,_fieldDocGroupID,_fieldFileName,_fieldDocumentFileName);
}

       

    }
}
}
