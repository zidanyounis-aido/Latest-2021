using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class ToDoListComments 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("ToDoListComments");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public ToDoListComments () { }
        public ToDoListComments (DataTable Table) 
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

        public string[] columnsArray = {"Int32 Id","Int32 ToDoListId","string CommentText","Int32 CreatedBy","DateTime CreatedOn","bool IsDeleted"};
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

private Int32 _fieldToDoListId;
private bool _fieldToDoListIdFlag = false;
public Int32 fieldToDoListId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldToDoListIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["ToDoListId"]);
        else
            return _fieldToDoListId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldToDoListIdFlag = true;
        _fieldToDoListId = value;
        }
    }

private string _fieldCommentText;
private bool _fieldCommentTextFlag = false;
public string fieldCommentText
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCommentTextFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["CommentText"]);
        else
            return _fieldCommentText;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCommentTextFlag = true;
        _fieldCommentText = value;
        }
    }

private Int32 _fieldCreatedBy;
private bool _fieldCreatedByFlag = false;
public Int32 fieldCreatedBy
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCreatedByFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["CreatedBy"]);
        else
            return _fieldCreatedBy;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCreatedByFlag = true;
        _fieldCreatedBy = value;
        }
    }

private DateTime _fieldCreatedOn;
private bool _fieldCreatedOnFlag = false;
public DateTime fieldCreatedOn
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCreatedOnFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["CreatedOn"]);
        else
            return _fieldCreatedOn;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCreatedOnFlag = true;
        _fieldCreatedOn = value;
        }
    }

private bool _fieldIsDeleted;
private bool _fieldIsDeletedFlag = false;
public bool fieldIsDeleted
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIsDeletedFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["IsDeleted"]);
        else
            return _fieldIsDeleted;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIsDeletedFlag = true;
        _fieldIsDeleted = value;
        }
    }

public void reset()
{
_fieldIdFlag = false;
_fieldId = c.convertToInt32(_table.Rows[_currentIndex]["Id"]);
_fieldToDoListIdFlag = false;
_fieldToDoListId = c.convertToInt32(_table.Rows[_currentIndex]["ToDoListId"]);
_fieldCommentTextFlag = false;
_fieldCommentText = c.convertToString(_table.Rows[_currentIndex]["CommentText"]);
_fieldCreatedByFlag = false;
_fieldCreatedBy = c.convertToInt32(_table.Rows[_currentIndex]["CreatedBy"]);
_fieldCreatedOnFlag = false;
_fieldCreatedOn = c.convertToDateTime(_table.Rows[_currentIndex]["CreatedOn"]);
_fieldIsDeletedFlag = false;
_fieldIsDeleted = c.convertToBool(_table.Rows[_currentIndex]["IsDeleted"]);

}
public void update()
{
op.dboUpdateToDoListCommentsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["Id"]),_fieldToDoListId,_fieldCommentText,_fieldCreatedBy,_fieldCreatedOn,_fieldIsDeleted);
}

       

    }
}
}
