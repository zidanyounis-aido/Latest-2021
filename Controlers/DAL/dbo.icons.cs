using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class icons 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("icons");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public icons () { }
        public icons (DataTable Table) 
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

        public string[] columnsArray = {"Int32 iconID","string iconDescription"};
private Int32 _fieldIconID;
private bool _fieldIconIDFlag = false;
public Int32 fieldIconID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIconIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["iconID"]);
        else
            return _fieldIconID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIconIDFlag = true;
        _fieldIconID = value;
        }
    }

private string _fieldIconDescription;
private bool _fieldIconDescriptionFlag = false;
public string fieldIconDescription
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIconDescriptionFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["iconDescription"]);
        else
            return _fieldIconDescription;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIconDescriptionFlag = true;
        _fieldIconDescription = value;
        }
    }

public void reset()
{
_fieldIconIDFlag = false;
_fieldIconID = c.convertToInt32(_table.Rows[_currentIndex]["iconID"]);
_fieldIconDescriptionFlag = false;
_fieldIconDescription = c.convertToString(_table.Rows[_currentIndex]["iconDescription"]);

}
public void update()
{
op.dboUpdateIconsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["iconID"]),_fieldIconDescription);
}

       

    }
}
}
