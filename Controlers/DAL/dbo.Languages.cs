using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class Languages 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("Languages");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public Languages () { }
        public Languages (DataTable Table) 
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

        public string[] columnsArray = {"Int32 LanguageId","string LanguageName","string LanguageISOCode"};
private Int32 _fieldLanguageId;
private bool _fieldLanguageIdFlag = false;
public Int32 fieldLanguageId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldLanguageIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["LanguageId"]);
        else
            return _fieldLanguageId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldLanguageIdFlag = true;
        _fieldLanguageId = value;
        }
    }

private string _fieldLanguageName;
private bool _fieldLanguageNameFlag = false;
public string fieldLanguageName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldLanguageNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["LanguageName"]);
        else
            return _fieldLanguageName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldLanguageNameFlag = true;
        _fieldLanguageName = value;
        }
    }

private string _fieldLanguageISOCode;
private bool _fieldLanguageISOCodeFlag = false;
public string fieldLanguageISOCode
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldLanguageISOCodeFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["LanguageISOCode"]);
        else
            return _fieldLanguageISOCode;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldLanguageISOCodeFlag = true;
        _fieldLanguageISOCode = value;
        }
    }

public void reset()
{
_fieldLanguageIdFlag = false;
_fieldLanguageId = c.convertToInt32(_table.Rows[_currentIndex]["LanguageId"]);
_fieldLanguageNameFlag = false;
_fieldLanguageName = c.convertToString(_table.Rows[_currentIndex]["LanguageName"]);
_fieldLanguageISOCodeFlag = false;
_fieldLanguageISOCode = c.convertToString(_table.Rows[_currentIndex]["LanguageISOCode"]);

}
public void update()
{
op.dboUpdateLanguagesByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["LanguageId"]),_fieldLanguageName,_fieldLanguageISOCode);
}

       

    }
}
}
