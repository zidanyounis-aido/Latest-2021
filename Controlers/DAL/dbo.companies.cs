using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class companies 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("companies");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public companies () { }
        public companies (DataTable Table) 
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

        public string[] columnsArray = {"Int32 companyID","string companyName","string address","string tel1","string tel2","string zipcode","string mainEmail","string description","string companyNameAr","Int32 ClientId"};
private Int32 _fieldCompanyID;
private bool _fieldCompanyIDFlag = false;
public Int32 fieldCompanyID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCompanyIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["companyID"]);
        else
            return _fieldCompanyID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCompanyIDFlag = true;
        _fieldCompanyID = value;
        }
    }

private string _fieldCompanyName;
private bool _fieldCompanyNameFlag = false;
public string fieldCompanyName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCompanyNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["companyName"]);
        else
            return _fieldCompanyName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCompanyNameFlag = true;
        _fieldCompanyName = value;
        }
    }

private string _fieldAddress;
private bool _fieldAddressFlag = false;
public string fieldAddress
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAddressFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["address"]);
        else
            return _fieldAddress;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAddressFlag = true;
        _fieldAddress = value;
        }
    }

private string _fieldTel1;
private bool _fieldTel1Flag = false;
public string fieldTel1
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldTel1Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["tel1"]);
        else
            return _fieldTel1;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldTel1Flag = true;
        _fieldTel1 = value;
        }
    }

private string _fieldTel2;
private bool _fieldTel2Flag = false;
public string fieldTel2
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldTel2Flag == false)
           return c.convertToString(_table.Rows[_currentIndex]["tel2"]);
        else
            return _fieldTel2;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldTel2Flag = true;
        _fieldTel2 = value;
        }
    }

private string _fieldZipcode;
private bool _fieldZipcodeFlag = false;
public string fieldZipcode
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldZipcodeFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["zipcode"]);
        else
            return _fieldZipcode;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldZipcodeFlag = true;
        _fieldZipcode = value;
        }
    }

private string _fieldMainEmail;
private bool _fieldMainEmailFlag = false;
public string fieldMainEmail
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldMainEmailFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["mainEmail"]);
        else
            return _fieldMainEmail;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldMainEmailFlag = true;
        _fieldMainEmail = value;
        }
    }

private string _fieldDescription;
private bool _fieldDescriptionFlag = false;
public string fieldDescription
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDescriptionFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["description"]);
        else
            return _fieldDescription;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDescriptionFlag = true;
        _fieldDescription = value;
        }
    }

private string _fieldCompanyNameAr;
private bool _fieldCompanyNameArFlag = false;
public string fieldCompanyNameAr
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldCompanyNameArFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["companyNameAr"]);
        else
            return _fieldCompanyNameAr;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldCompanyNameArFlag = true;
        _fieldCompanyNameAr = value;
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
_fieldCompanyIDFlag = false;
_fieldCompanyID = c.convertToInt32(_table.Rows[_currentIndex]["companyID"]);
_fieldCompanyNameFlag = false;
_fieldCompanyName = c.convertToString(_table.Rows[_currentIndex]["companyName"]);
_fieldAddressFlag = false;
_fieldAddress = c.convertToString(_table.Rows[_currentIndex]["address"]);
_fieldTel1Flag = false;
_fieldTel1 = c.convertToString(_table.Rows[_currentIndex]["tel1"]);
_fieldTel2Flag = false;
_fieldTel2 = c.convertToString(_table.Rows[_currentIndex]["tel2"]);
_fieldZipcodeFlag = false;
_fieldZipcode = c.convertToString(_table.Rows[_currentIndex]["zipcode"]);
_fieldMainEmailFlag = false;
_fieldMainEmail = c.convertToString(_table.Rows[_currentIndex]["mainEmail"]);
_fieldDescriptionFlag = false;
_fieldDescription = c.convertToString(_table.Rows[_currentIndex]["description"]);
_fieldCompanyNameArFlag = false;
_fieldCompanyNameAr = c.convertToString(_table.Rows[_currentIndex]["companyNameAr"]);
_fieldClientIdFlag = false;
_fieldClientId = c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]);

}
public void update()
{
op.dboUpdateCompaniesByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["companyID"]),_fieldCompanyName,_fieldAddress,_fieldTel1,_fieldTel2,_fieldZipcode,_fieldMainEmail,_fieldDescription,_fieldCompanyNameAr,_fieldClientId);
}

       

    }
}
}
