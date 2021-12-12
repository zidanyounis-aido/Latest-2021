using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class DBActionsTypes 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("DBActionsTypes");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public DBActionsTypes () { }
        public DBActionsTypes (DataTable Table) 
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

        public string[] columnsArray = {"Int32 DBActionTypeID","string DBActionTypeDescA","string FBActionTypeDescE"};
private Int32 _fieldDBActionTypeID;
private bool _fieldDBActionTypeIDFlag = false;
public Int32 fieldDBActionTypeID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDBActionTypeIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["DBActionTypeID"]);
        else
            return _fieldDBActionTypeID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDBActionTypeIDFlag = true;
        _fieldDBActionTypeID = value;
        }
    }

private string _fieldDBActionTypeDescA;
private bool _fieldDBActionTypeDescAFlag = false;
public string fieldDBActionTypeDescA
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDBActionTypeDescAFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["DBActionTypeDescA"]);
        else
            return _fieldDBActionTypeDescA;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDBActionTypeDescAFlag = true;
        _fieldDBActionTypeDescA = value;
        }
    }

private string _fieldFBActionTypeDescE;
private bool _fieldFBActionTypeDescEFlag = false;
public string fieldFBActionTypeDescE
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFBActionTypeDescEFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["FBActionTypeDescE"]);
        else
            return _fieldFBActionTypeDescE;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFBActionTypeDescEFlag = true;
        _fieldFBActionTypeDescE = value;
        }
    }

public void reset()
{
_fieldDBActionTypeIDFlag = false;
_fieldDBActionTypeID = c.convertToInt32(_table.Rows[_currentIndex]["DBActionTypeID"]);
_fieldDBActionTypeDescAFlag = false;
_fieldDBActionTypeDescA = c.convertToString(_table.Rows[_currentIndex]["DBActionTypeDescA"]);
_fieldFBActionTypeDescEFlag = false;
_fieldFBActionTypeDescE = c.convertToString(_table.Rows[_currentIndex]["FBActionTypeDescE"]);

}
public void update()
{
op.dboUpdateDBActionsTypesByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["DBActionTypeID"]),_fieldDBActionTypeDescA,_fieldFBActionTypeDescE);
}

       

    }
}
}
