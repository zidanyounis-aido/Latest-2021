using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class positions 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("positions");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public positions () { }
        public positions (DataTable Table) 
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

        public string[] columnsArray = {"Int32 positionID","string positionTitle","string positionTitleAr","Int32 ClientId"};
private Int32 _fieldPositionID;
private bool _fieldPositionIDFlag = false;
public Int32 fieldPositionID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPositionIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["positionID"]);
        else
            return _fieldPositionID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPositionIDFlag = true;
        _fieldPositionID = value;
        }
    }

private string _fieldPositionTitle;
private bool _fieldPositionTitleFlag = false;
public string fieldPositionTitle
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPositionTitleFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["positionTitle"]);
        else
            return _fieldPositionTitle;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPositionTitleFlag = true;
        _fieldPositionTitle = value;
        }
    }

private string _fieldPositionTitleAr;
private bool _fieldPositionTitleArFlag = false;
public string fieldPositionTitleAr
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPositionTitleArFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["positionTitleAr"]);
        else
            return _fieldPositionTitleAr;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPositionTitleArFlag = true;
        _fieldPositionTitleAr = value;
        }
    }

private Int32 _fieldClientId;
private bool _fieldClientIdFlag = false;
public Int32 fieldClientId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldClientIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]);
        else
            return _fieldClientId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldClientIdFlag = true;
        _fieldClientId = value;
        }
    }

public void reset()
{
_fieldPositionIDFlag = false;
_fieldPositionID = c.convertToInt32(_table.Rows[_currentIndex]["positionID"]);
_fieldPositionTitleFlag = false;
_fieldPositionTitle = c.convertToString(_table.Rows[_currentIndex]["positionTitle"]);
_fieldPositionTitleArFlag = false;
_fieldPositionTitleAr = c.convertToString(_table.Rows[_currentIndex]["positionTitleAr"]);
_fieldClientIdFlag = false;
_fieldClientId = c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]);

}
public void update()
{
op.dboUpdatePositionsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["positionID"]),_fieldPositionTitle,_fieldPositionTitleAr,_fieldClientId);
}

       

    }
}
}
