using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class type 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("type");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public type () { }
        public type (DataTable Table) 
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

        public string[] columnsArray = {"Int32 typeId","string typeNameAr","string typeName"};
private Int32 _fieldTypeId;
private bool _fieldTypeIdFlag = false;
public Int32 fieldTypeId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldTypeIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["typeId"]);
        else
            return _fieldTypeId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldTypeIdFlag = true;
        _fieldTypeId = value;
        }
    }

private string _fieldTypeNameAr;
private bool _fieldTypeNameArFlag = false;
public string fieldTypeNameAr
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldTypeNameArFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["typeNameAr"]);
        else
            return _fieldTypeNameAr;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldTypeNameArFlag = true;
        _fieldTypeNameAr = value;
        }
    }

private string _fieldTypeName;
private bool _fieldTypeNameFlag = false;
public string fieldTypeName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldTypeNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["typeName"]);
        else
            return _fieldTypeName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldTypeNameFlag = true;
        _fieldTypeName = value;
        }
    }

public void reset()
{
_fieldTypeIdFlag = false;
_fieldTypeId = c.convertToInt32(_table.Rows[_currentIndex]["typeId"]);
_fieldTypeNameArFlag = false;
_fieldTypeNameAr = c.convertToString(_table.Rows[_currentIndex]["typeNameAr"]);
_fieldTypeNameFlag = false;
_fieldTypeName = c.convertToString(_table.Rows[_currentIndex]["typeName"]);

}
public void update()
{
op.dboUpdateTypeByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["typeId"]),_fieldTypeNameAr,_fieldTypeName);
}

       

    }
}
}
