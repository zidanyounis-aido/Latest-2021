using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class userPrograms 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("userPrograms");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public userPrograms () { }
        public userPrograms (DataTable Table) 
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

        public string[] columnsArray = {"Int32 userID","Int32 programID"};
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

private Int32 _fieldProgramID;
private bool _fieldProgramIDFlag = false;
public Int32 fieldProgramID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldProgramIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["programID"]);
        else
            return _fieldProgramID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldProgramIDFlag = true;
        _fieldProgramID = value;
        }
    }

public void reset()
{
_fieldUserIDFlag = false;
_fieldUserID = c.convertToInt32(_table.Rows[_currentIndex]["userID"]);
_fieldProgramIDFlag = false;
_fieldProgramID = c.convertToInt32(_table.Rows[_currentIndex]["programID"]);

}
public void update()
{
op.dboUpdateUserProgramsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["userID"]),c.convertToInt32(_table.Rows[_currentIndex]["programID"]));
}

       

    }
}
}
