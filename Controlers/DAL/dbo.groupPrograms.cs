using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class groupPrograms 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("groupPrograms");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public groupPrograms () { }
        public groupPrograms (DataTable Table) 
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

        public string[] columnsArray = {"Int32 groupID","Int32 programID"};
private Int32 _fieldGroupID;
private bool _fieldGroupIDFlag = false;
public Int32 fieldGroupID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldGroupIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["groupID"]);
        else
            return _fieldGroupID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldGroupIDFlag = true;
        _fieldGroupID = value;
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
_fieldGroupIDFlag = false;
_fieldGroupID = c.convertToInt32(_table.Rows[_currentIndex]["groupID"]);
_fieldProgramIDFlag = false;
_fieldProgramID = c.convertToInt32(_table.Rows[_currentIndex]["programID"]);

}
public void update()
{
op.dboUpdateGroupProgramsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["groupID"]),c.convertToInt32(_table.Rows[_currentIndex]["programID"]));
}

       

    }
}
}
