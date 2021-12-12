using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class controlsTypes 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("controlsTypes");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public controlsTypes () { }
        public controlsTypes (DataTable Table) 
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

        public string[] columnsArray = {"Int32 crtlID","string ctrlDesc","string ctrlDescAr"};
private Int32 _fieldCrtlID;
private bool _fieldCrtlIDFlag = false;
public Int32 fieldCrtlID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCrtlIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["crtlID"]);
        else
            return _fieldCrtlID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCrtlIDFlag = true;
        _fieldCrtlID = value;
        }
    }

private string _fieldCtrlDesc;
private bool _fieldCtrlDescFlag = false;
public string fieldCtrlDesc
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCtrlDescFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["ctrlDesc"]);
        else
            return _fieldCtrlDesc;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCtrlDescFlag = true;
        _fieldCtrlDesc = value;
        }
    }

private string _fieldCtrlDescAr;
private bool _fieldCtrlDescArFlag = false;
public string fieldCtrlDescAr
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCtrlDescArFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["ctrlDescAr"]);
        else
            return _fieldCtrlDescAr;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCtrlDescArFlag = true;
        _fieldCtrlDescAr = value;
        }
    }

public void reset()
{
_fieldCrtlIDFlag = false;
_fieldCrtlID = c.convertToInt32(_table.Rows[_currentIndex]["crtlID"]);
_fieldCtrlDescFlag = false;
_fieldCtrlDesc = c.convertToString(_table.Rows[_currentIndex]["ctrlDesc"]);
_fieldCtrlDescArFlag = false;
_fieldCtrlDescAr = c.convertToString(_table.Rows[_currentIndex]["ctrlDescAr"]);

}
public void update()
{
op.dboUpdateControlsTypesByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["crtlID"]),_fieldCtrlDesc,_fieldCtrlDescAr);
}

       

    }
}
}
