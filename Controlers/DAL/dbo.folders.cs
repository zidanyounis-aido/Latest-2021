using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class folders 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("folders");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public folders () { }
        public folders (DataTable Table) 
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

        public string[] columnsArray = {"Int32 fldrID","string fldrName","Int32 fldrParentID","bool active","Int32 iconID","Int32 defaultDocTypID","Int32 folderOrder","bool isDiwan","string fldrNameAr","bool allowWF","Int32 folderOwner","Int32 ClientId"};
private Int32 _fieldFldrID;
private bool _fieldFldrIDFlag = false;
public Int32 fieldFldrID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFldrIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["fldrID"]);
        else
            return _fieldFldrID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFldrIDFlag = true;
        _fieldFldrID = value;
        }
    }

private string _fieldFldrName;
private bool _fieldFldrNameFlag = false;
public string fieldFldrName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFldrNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["fldrName"]);
        else
            return _fieldFldrName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFldrNameFlag = true;
        _fieldFldrName = value;
        }
    }

private Int32 _fieldFldrParentID;
private bool _fieldFldrParentIDFlag = false;
public Int32 fieldFldrParentID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFldrParentIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["fldrParentID"]);
        else
            return _fieldFldrParentID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFldrParentIDFlag = true;
        _fieldFldrParentID = value;
        }
    }

private bool _fieldActive;
private bool _fieldActiveFlag = false;
public bool fieldActive
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldActiveFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["active"]);
        else
            return _fieldActive;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldActiveFlag = true;
        _fieldActive = value;
        }
    }

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

private Int32 _fieldDefaultDocTypID;
private bool _fieldDefaultDocTypIDFlag = false;
public Int32 fieldDefaultDocTypID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDefaultDocTypIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["defaultDocTypID"]);
        else
            return _fieldDefaultDocTypID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDefaultDocTypIDFlag = true;
        _fieldDefaultDocTypID = value;
        }
    }

private Int32 _fieldFolderOrder;
private bool _fieldFolderOrderFlag = false;
public Int32 fieldFolderOrder
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFolderOrderFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["folderOrder"]);
        else
            return _fieldFolderOrder;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFolderOrderFlag = true;
        _fieldFolderOrder = value;
        }
    }

private bool _fieldIsDiwan;
private bool _fieldIsDiwanFlag = false;
public bool fieldIsDiwan
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIsDiwanFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["isDiwan"]);
        else
            return _fieldIsDiwan;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIsDiwanFlag = true;
        _fieldIsDiwan = value;
        }
    }

private string _fieldFldrNameAr;
private bool _fieldFldrNameArFlag = false;
public string fieldFldrNameAr
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFldrNameArFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["fldrNameAr"]);
        else
            return _fieldFldrNameAr;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFldrNameArFlag = true;
        _fieldFldrNameAr = value;
        }
    }

private bool _fieldAllowWF;
private bool _fieldAllowWFFlag = false;
public bool fieldAllowWF
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowWFFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["allowWF"]);
        else
            return _fieldAllowWF;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowWFFlag = true;
        _fieldAllowWF = value;
        }
    }

private Int32 _fieldFolderOwner;
private bool _fieldFolderOwnerFlag = false;
public Int32 fieldFolderOwner
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFolderOwnerFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["folderOwner"]);
        else
            return _fieldFolderOwner;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFolderOwnerFlag = true;
        _fieldFolderOwner = value;
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
_fieldFldrIDFlag = false;
_fieldFldrID = c.convertToInt32(_table.Rows[_currentIndex]["fldrID"]);
_fieldFldrNameFlag = false;
_fieldFldrName = c.convertToString(_table.Rows[_currentIndex]["fldrName"]);
_fieldFldrParentIDFlag = false;
_fieldFldrParentID = c.convertToInt32(_table.Rows[_currentIndex]["fldrParentID"]);
_fieldActiveFlag = false;
_fieldActive = c.convertToBool(_table.Rows[_currentIndex]["active"]);
_fieldIconIDFlag = false;
_fieldIconID = c.convertToInt32(_table.Rows[_currentIndex]["iconID"]);
_fieldDefaultDocTypIDFlag = false;
_fieldDefaultDocTypID = c.convertToInt32(_table.Rows[_currentIndex]["defaultDocTypID"]);
_fieldFolderOrderFlag = false;
_fieldFolderOrder = c.convertToInt32(_table.Rows[_currentIndex]["folderOrder"]);
_fieldIsDiwanFlag = false;
_fieldIsDiwan = c.convertToBool(_table.Rows[_currentIndex]["isDiwan"]);
_fieldFldrNameArFlag = false;
_fieldFldrNameAr = c.convertToString(_table.Rows[_currentIndex]["fldrNameAr"]);
_fieldAllowWFFlag = false;
_fieldAllowWF = c.convertToBool(_table.Rows[_currentIndex]["allowWF"]);
_fieldFolderOwnerFlag = false;
_fieldFolderOwner = c.convertToInt32(_table.Rows[_currentIndex]["folderOwner"]);
_fieldClientIdFlag = false;
_fieldClientId = c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]);

}
public void update()
{
op.dboUpdateFoldersByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["fldrID"]),_fieldFldrName,_fieldFldrParentID,_fieldActive,_fieldIconID,_fieldDefaultDocTypID,_fieldFolderOrder,_fieldIsDiwan,_fieldFldrNameAr,_fieldAllowWF,_fieldFolderOwner,_fieldClientId);
}

       

    }
}
}
