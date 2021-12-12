using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class groupBlocks 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("groupBlocks");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public groupBlocks () { }
        public groupBlocks (DataTable Table) 
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

        public string[] columnsArray = {"Int32 grpID","Int32 docTypID","Int32 blockNum","string blockLeft","string blockTop","string blockWidth","string blockHeight"};
private Int32 _fieldGrpID;
private bool _fieldGrpIDFlag = false;
public Int32 fieldGrpID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldGrpIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["grpID"]);
        else
            return _fieldGrpID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldGrpIDFlag = true;
        _fieldGrpID = value;
        }
    }

private Int32 _fieldDocTypID;
private bool _fieldDocTypIDFlag = false;
public Int32 fieldDocTypID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocTypIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["docTypID"]);
        else
            return _fieldDocTypID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocTypIDFlag = true;
        _fieldDocTypID = value;
        }
    }

private Int32 _fieldBlockNum;
private bool _fieldBlockNumFlag = false;
public Int32 fieldBlockNum
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldBlockNumFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["blockNum"]);
        else
            return _fieldBlockNum;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldBlockNumFlag = true;
        _fieldBlockNum = value;
        }
    }

private string _fieldBlockLeft;
private bool _fieldBlockLeftFlag = false;
public string fieldBlockLeft
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldBlockLeftFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["blockLeft"]);
        else
            return _fieldBlockLeft;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldBlockLeftFlag = true;
        _fieldBlockLeft = value;
        }
    }

private string _fieldBlockTop;
private bool _fieldBlockTopFlag = false;
public string fieldBlockTop
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldBlockTopFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["blockTop"]);
        else
            return _fieldBlockTop;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldBlockTopFlag = true;
        _fieldBlockTop = value;
        }
    }

private string _fieldBlockWidth;
private bool _fieldBlockWidthFlag = false;
public string fieldBlockWidth
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldBlockWidthFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["blockWidth"]);
        else
            return _fieldBlockWidth;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldBlockWidthFlag = true;
        _fieldBlockWidth = value;
        }
    }

private string _fieldBlockHeight;
private bool _fieldBlockHeightFlag = false;
public string fieldBlockHeight
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldBlockHeightFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["blockHeight"]);
        else
            return _fieldBlockHeight;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldBlockHeightFlag = true;
        _fieldBlockHeight = value;
        }
    }

public void reset()
{
_fieldGrpIDFlag = false;
_fieldGrpID = c.convertToInt32(_table.Rows[_currentIndex]["grpID"]);
_fieldDocTypIDFlag = false;
_fieldDocTypID = c.convertToInt32(_table.Rows[_currentIndex]["docTypID"]);
_fieldBlockNumFlag = false;
_fieldBlockNum = c.convertToInt32(_table.Rows[_currentIndex]["blockNum"]);
_fieldBlockLeftFlag = false;
_fieldBlockLeft = c.convertToString(_table.Rows[_currentIndex]["blockLeft"]);
_fieldBlockTopFlag = false;
_fieldBlockTop = c.convertToString(_table.Rows[_currentIndex]["blockTop"]);
_fieldBlockWidthFlag = false;
_fieldBlockWidth = c.convertToString(_table.Rows[_currentIndex]["blockWidth"]);
_fieldBlockHeightFlag = false;
_fieldBlockHeight = c.convertToString(_table.Rows[_currentIndex]["blockHeight"]);

}
public void update()
{
op.dboUpdateGroupBlocksByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["grpID"]),c.convertToInt32(_table.Rows[_currentIndex]["docTypID"]),c.convertToInt32(_table.Rows[_currentIndex]["blockNum"]),_fieldBlockLeft,_fieldBlockTop,_fieldBlockWidth,_fieldBlockHeight);
}

       

    }
}
}
