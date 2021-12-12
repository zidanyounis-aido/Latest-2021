using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class workFlowPaths 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("workFlowPaths");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public workFlowPaths () { }
        public workFlowPaths (DataTable Table) 
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

        public string[] columnsArray = {"Int32 pathId","string pathDesc","Int32 fldrId","Int32 docTypId","string pathDescAr","Int32 ClientId"};
private Int32 _fieldPathId;
private bool _fieldPathIdFlag = false;
public Int32 fieldPathId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPathIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["pathId"]);
        else
            return _fieldPathId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPathIdFlag = true;
        _fieldPathId = value;
        }
    }

private string _fieldPathDesc;
private bool _fieldPathDescFlag = false;
public string fieldPathDesc
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPathDescFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["pathDesc"]);
        else
            return _fieldPathDesc;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPathDescFlag = true;
        _fieldPathDesc = value;
        }
    }

private Int32 _fieldFldrId;
private bool _fieldFldrIdFlag = false;
public Int32 fieldFldrId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFldrIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["fldrId"]);
        else
            return _fieldFldrId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFldrIdFlag = true;
        _fieldFldrId = value;
        }
    }

private Int32 _fieldDocTypId;
private bool _fieldDocTypIdFlag = false;
public Int32 fieldDocTypId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDocTypIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["docTypId"]);
        else
            return _fieldDocTypId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDocTypIdFlag = true;
        _fieldDocTypId = value;
        }
    }

private string _fieldPathDescAr;
private bool _fieldPathDescArFlag = false;
public string fieldPathDescAr
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPathDescArFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["pathDescAr"]);
        else
            return _fieldPathDescAr;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPathDescArFlag = true;
        _fieldPathDescAr = value;
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
_fieldPathIdFlag = false;
_fieldPathId = c.convertToInt32(_table.Rows[_currentIndex]["pathId"]);
_fieldPathDescFlag = false;
_fieldPathDesc = c.convertToString(_table.Rows[_currentIndex]["pathDesc"]);
_fieldFldrIdFlag = false;
_fieldFldrId = c.convertToInt32(_table.Rows[_currentIndex]["fldrId"]);
_fieldDocTypIdFlag = false;
_fieldDocTypId = c.convertToInt32(_table.Rows[_currentIndex]["docTypId"]);
_fieldPathDescArFlag = false;
_fieldPathDescAr = c.convertToString(_table.Rows[_currentIndex]["pathDescAr"]);
_fieldClientIdFlag = false;
_fieldClientId = c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]);

}
public void update()
{
op.dboUpdateWorkFlowPathsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["pathId"]),_fieldPathDesc,_fieldFldrId,_fieldDocTypId,_fieldPathDescAr,_fieldClientId);
}

       

    }
}
}
