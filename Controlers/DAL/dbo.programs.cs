using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class programs 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("programs");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public programs () { }
        public programs (DataTable Table) 
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

        public string[] columnsArray = {"Int32 programID","string programName","Int32 parentProgramID","string programURL","Int32 windowWidth","Int32 windowHeight","string programNameAr","string iconCss","Int32 orderNum","string svg","bool IsShowOnMobile"};
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

private string _fieldProgramName;
private bool _fieldProgramNameFlag = false;
public string fieldProgramName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldProgramNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["programName"]);
        else
            return _fieldProgramName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldProgramNameFlag = true;
        _fieldProgramName = value;
        }
    }

private Int32 _fieldParentProgramID;
private bool _fieldParentProgramIDFlag = false;
public Int32 fieldParentProgramID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldParentProgramIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["parentProgramID"]);
        else
            return _fieldParentProgramID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldParentProgramIDFlag = true;
        _fieldParentProgramID = value;
        }
    }

private string _fieldProgramURL;
private bool _fieldProgramURLFlag = false;
public string fieldProgramURL
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldProgramURLFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["programURL"]);
        else
            return _fieldProgramURL;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldProgramURLFlag = true;
        _fieldProgramURL = value;
        }
    }

private Int32 _fieldWindowWidth;
private bool _fieldWindowWidthFlag = false;
public Int32 fieldWindowWidth
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWindowWidthFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["windowWidth"]);
        else
            return _fieldWindowWidth;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWindowWidthFlag = true;
        _fieldWindowWidth = value;
        }
    }

private Int32 _fieldWindowHeight;
private bool _fieldWindowHeightFlag = false;
public Int32 fieldWindowHeight
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWindowHeightFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["windowHeight"]);
        else
            return _fieldWindowHeight;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWindowHeightFlag = true;
        _fieldWindowHeight = value;
        }
    }

private string _fieldProgramNameAr;
private bool _fieldProgramNameArFlag = false;
public string fieldProgramNameAr
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldProgramNameArFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["programNameAr"]);
        else
            return _fieldProgramNameAr;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldProgramNameArFlag = true;
        _fieldProgramNameAr = value;
        }
    }

private string _fieldIconCss;
private bool _fieldIconCssFlag = false;
public string fieldIconCss
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIconCssFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["iconCss"]);
        else
            return _fieldIconCss;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIconCssFlag = true;
        _fieldIconCss = value;
        }
    }

private Int32 _fieldOrderNum;
private bool _fieldOrderNumFlag = false;
public Int32 fieldOrderNum
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldOrderNumFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["orderNum"]);
        else
            return _fieldOrderNum;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldOrderNumFlag = true;
        _fieldOrderNum = value;
        }
    }

private string _fieldSvg;
private bool _fieldSvgFlag = false;
public string fieldSvg
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSvgFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["svg"]);
        else
            return _fieldSvg;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSvgFlag = true;
        _fieldSvg = value;
        }
    }

private bool _fieldIsShowOnMobile;
private bool _fieldIsShowOnMobileFlag = false;
public bool fieldIsShowOnMobile
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIsShowOnMobileFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["IsShowOnMobile"]);
        else
            return _fieldIsShowOnMobile;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIsShowOnMobileFlag = true;
        _fieldIsShowOnMobile = value;
        }
    }

public void reset()
{
_fieldProgramIDFlag = false;
_fieldProgramID = c.convertToInt32(_table.Rows[_currentIndex]["programID"]);
_fieldProgramNameFlag = false;
_fieldProgramName = c.convertToString(_table.Rows[_currentIndex]["programName"]);
_fieldParentProgramIDFlag = false;
_fieldParentProgramID = c.convertToInt32(_table.Rows[_currentIndex]["parentProgramID"]);
_fieldProgramURLFlag = false;
_fieldProgramURL = c.convertToString(_table.Rows[_currentIndex]["programURL"]);
_fieldWindowWidthFlag = false;
_fieldWindowWidth = c.convertToInt32(_table.Rows[_currentIndex]["windowWidth"]);
_fieldWindowHeightFlag = false;
_fieldWindowHeight = c.convertToInt32(_table.Rows[_currentIndex]["windowHeight"]);
_fieldProgramNameArFlag = false;
_fieldProgramNameAr = c.convertToString(_table.Rows[_currentIndex]["programNameAr"]);
_fieldIconCssFlag = false;
_fieldIconCss = c.convertToString(_table.Rows[_currentIndex]["iconCss"]);
_fieldOrderNumFlag = false;
_fieldOrderNum = c.convertToInt32(_table.Rows[_currentIndex]["orderNum"]);
_fieldSvgFlag = false;
_fieldSvg = c.convertToString(_table.Rows[_currentIndex]["svg"]);
_fieldIsShowOnMobileFlag = false;
_fieldIsShowOnMobile = c.convertToBool(_table.Rows[_currentIndex]["IsShowOnMobile"]);

}
public void update()
{
op.dboUpdateProgramsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["programID"]),_fieldProgramName,_fieldParentProgramID,_fieldProgramURL,_fieldWindowWidth,_fieldWindowHeight,_fieldProgramNameAr,_fieldIconCss,_fieldOrderNum,_fieldSvg,_fieldIsShowOnMobile);
}

       

    }
}
}
