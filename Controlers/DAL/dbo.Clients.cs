using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class Clients 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("Clients");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public Clients () { }
        public Clients (DataTable Table) 
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

        public string[] columnsArray = {"Int32 ClientId","string ClientName","string ClientEmail","Int32 CountryId","Int32 DefualtLanguageID","string ClientPhone","DateTime CreatedDate","bool IsActive"};
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

private string _fieldClientName;
private bool _fieldClientNameFlag = false;
public string fieldClientName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldClientNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["ClientName"]);
        else
            return _fieldClientName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldClientNameFlag = true;
        _fieldClientName = value;
        }
    }

private string _fieldClientEmail;
private bool _fieldClientEmailFlag = false;
public string fieldClientEmail
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldClientEmailFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["ClientEmail"]);
        else
            return _fieldClientEmail;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldClientEmailFlag = true;
        _fieldClientEmail = value;
        }
    }

private Int32 _fieldCountryId;
private bool _fieldCountryIdFlag = false;
public Int32 fieldCountryId
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCountryIdFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["CountryId"]);
        else
            return _fieldCountryId;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCountryIdFlag = true;
        _fieldCountryId = value;
        }
    }

private Int32 _fieldDefualtLanguageID;
private bool _fieldDefualtLanguageIDFlag = false;
public Int32 fieldDefualtLanguageID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDefualtLanguageIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["DefualtLanguageID"]);
        else
            return _fieldDefualtLanguageID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDefualtLanguageIDFlag = true;
        _fieldDefualtLanguageID = value;
        }
    }

private string _fieldClientPhone;
private bool _fieldClientPhoneFlag = false;
public string fieldClientPhone
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldClientPhoneFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["ClientPhone"]);
        else
            return _fieldClientPhone;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldClientPhoneFlag = true;
        _fieldClientPhone = value;
        }
    }

private DateTime _fieldCreatedDate;
private bool _fieldCreatedDateFlag = false;
public DateTime fieldCreatedDate
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCreatedDateFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["CreatedDate"]);
        else
            return _fieldCreatedDate;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCreatedDateFlag = true;
        _fieldCreatedDate = value;
        }
    }

private bool _fieldIsActive;
private bool _fieldIsActiveFlag = false;
public bool fieldIsActive
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIsActiveFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["IsActive"]);
        else
            return _fieldIsActive;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIsActiveFlag = true;
        _fieldIsActive = value;
        }
    }

public void reset()
{
_fieldClientIdFlag = false;
_fieldClientId = c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]);
_fieldClientNameFlag = false;
_fieldClientName = c.convertToString(_table.Rows[_currentIndex]["ClientName"]);
_fieldClientEmailFlag = false;
_fieldClientEmail = c.convertToString(_table.Rows[_currentIndex]["ClientEmail"]);
_fieldCountryIdFlag = false;
_fieldCountryId = c.convertToInt32(_table.Rows[_currentIndex]["CountryId"]);
_fieldDefualtLanguageIDFlag = false;
_fieldDefualtLanguageID = c.convertToInt32(_table.Rows[_currentIndex]["DefualtLanguageID"]);
_fieldClientPhoneFlag = false;
_fieldClientPhone = c.convertToString(_table.Rows[_currentIndex]["ClientPhone"]);
_fieldCreatedDateFlag = false;
_fieldCreatedDate = c.convertToDateTime(_table.Rows[_currentIndex]["CreatedDate"]);
_fieldIsActiveFlag = false;
_fieldIsActive = c.convertToBool(_table.Rows[_currentIndex]["IsActive"]);

}
public void update()
{
op.dboUpdateClientsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]),_fieldClientName,_fieldClientEmail,_fieldCountryId,_fieldDefualtLanguageID,_fieldClientPhone,_fieldCreatedDate,_fieldIsActive);
}

       

    }
}
}
