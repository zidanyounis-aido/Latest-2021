using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class users 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("users");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public users () { }
        public users (DataTable Table) 
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

        public string[] columnsArray = {"Int32 userID","string userName","string password","string fullName","Int32 grpID","bool active","Int32 companyID","Int32 branchID","Int32 departmentID","Int32 positionID","string email","bool allowCustomWF","bool allowCreateFolders","bool allowReplaceDocuments","bool allowDiwan","bool isFirstLogin","DateTime passwordCreationDate","DateTime passwordModifiedDate","string lastPassword","string Signature","string Phone","bool isMobileFirstLogin","bool isEmailVerfied","Int32 ClientId"};
private Int32 _fieldUserID;
private bool _fieldUserIDFlag = false;
public Int32 fieldUserID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldUserIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["userID"]);
        else
            return _fieldUserID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldUserIDFlag = true;
        _fieldUserID = value;
        }
    }

private string _fieldUserName;
private bool _fieldUserNameFlag = false;
public string fieldUserName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldUserNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["userName"]);
        else
            return _fieldUserName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldUserNameFlag = true;
        _fieldUserName = value;
        }
    }

private string _fieldPassword;
private bool _fieldPasswordFlag = false;
public string fieldPassword
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPasswordFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["password"]);
        else
            return _fieldPassword;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPasswordFlag = true;
        _fieldPassword = value;
        }
    }

private string _fieldFullName;
private bool _fieldFullNameFlag = false;
public string fieldFullName
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFullNameFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["fullName"]);
        else
            return _fieldFullName;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFullNameFlag = true;
        _fieldFullName = value;
        }
    }

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

private Int32 _fieldBranchID;
private bool _fieldBranchIDFlag = false;
public Int32 fieldBranchID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldBranchIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["branchID"]);
        else
            return _fieldBranchID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldBranchIDFlag = true;
        _fieldBranchID = value;
        }
    }

private Int32 _fieldDepartmentID;
private bool _fieldDepartmentIDFlag = false;
public Int32 fieldDepartmentID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldDepartmentIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["departmentID"]);
        else
            return _fieldDepartmentID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldDepartmentIDFlag = true;
        _fieldDepartmentID = value;
        }
    }

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

private string _fieldEmail;
private bool _fieldEmailFlag = false;
public string fieldEmail
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldEmailFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["email"]);
        else
            return _fieldEmail;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldEmailFlag = true;
        _fieldEmail = value;
        }
    }

private bool _fieldAllowCustomWF;
private bool _fieldAllowCustomWFFlag = false;
public bool fieldAllowCustomWF
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowCustomWFFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["allowCustomWF"]);
        else
            return _fieldAllowCustomWF;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowCustomWFFlag = true;
        _fieldAllowCustomWF = value;
        }
    }

private bool _fieldAllowCreateFolders;
private bool _fieldAllowCreateFoldersFlag = false;
public bool fieldAllowCreateFolders
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowCreateFoldersFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["allowCreateFolders"]);
        else
            return _fieldAllowCreateFolders;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowCreateFoldersFlag = true;
        _fieldAllowCreateFolders = value;
        }
    }

private bool _fieldAllowReplaceDocuments;
private bool _fieldAllowReplaceDocumentsFlag = false;
public bool fieldAllowReplaceDocuments
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowReplaceDocumentsFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["allowReplaceDocuments"]);
        else
            return _fieldAllowReplaceDocuments;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowReplaceDocumentsFlag = true;
        _fieldAllowReplaceDocuments = value;
        }
    }

private bool _fieldAllowDiwan;
private bool _fieldAllowDiwanFlag = false;
public bool fieldAllowDiwan
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowDiwanFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["allowDiwan"]);
        else
            return _fieldAllowDiwan;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowDiwanFlag = true;
        _fieldAllowDiwan = value;
        }
    }

private bool _fieldIsFirstLogin;
private bool _fieldIsFirstLoginFlag = false;
public bool fieldIsFirstLogin
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIsFirstLoginFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["isFirstLogin"]);
        else
            return _fieldIsFirstLogin;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIsFirstLoginFlag = true;
        _fieldIsFirstLogin = value;
        }
    }

private DateTime _fieldPasswordCreationDate;
private bool _fieldPasswordCreationDateFlag = false;
public DateTime fieldPasswordCreationDate
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPasswordCreationDateFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["passwordCreationDate"]);
        else
            return _fieldPasswordCreationDate;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPasswordCreationDateFlag = true;
        _fieldPasswordCreationDate = value;
        }
    }

private DateTime _fieldPasswordModifiedDate;
private bool _fieldPasswordModifiedDateFlag = false;
public DateTime fieldPasswordModifiedDate
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPasswordModifiedDateFlag == false)
           return c.convertToDateTime(_table.Rows[_currentIndex]["passwordModifiedDate"]);
        else
            return _fieldPasswordModifiedDate;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPasswordModifiedDateFlag = true;
        _fieldPasswordModifiedDate = value;
        }
    }

private string _fieldLastPassword;
private bool _fieldLastPasswordFlag = false;
public string fieldLastPassword
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldLastPasswordFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["lastPassword"]);
        else
            return _fieldLastPassword;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldLastPasswordFlag = true;
        _fieldLastPassword = value;
        }
    }

private string _fieldSignature;
private bool _fieldSignatureFlag = false;
public string fieldSignature
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSignatureFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["Signature"]);
        else
            return _fieldSignature;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSignatureFlag = true;
        _fieldSignature = value;
        }
    }

private string _fieldPhone;
private bool _fieldPhoneFlag = false;
public string fieldPhone
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPhoneFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["Phone"]);
        else
            return _fieldPhone;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPhoneFlag = true;
        _fieldPhone = value;
        }
    }

private bool _fieldIsMobileFirstLogin;
private bool _fieldIsMobileFirstLoginFlag = false;
public bool fieldIsMobileFirstLogin
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIsMobileFirstLoginFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["isMobileFirstLogin"]);
        else
            return _fieldIsMobileFirstLogin;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIsMobileFirstLoginFlag = true;
        _fieldIsMobileFirstLogin = value;
        }
    }

private bool _fieldIsEmailVerfied;
private bool _fieldIsEmailVerfiedFlag = false;
public bool fieldIsEmailVerfied
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIsEmailVerfiedFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["isEmailVerfied"]);
        else
            return _fieldIsEmailVerfied;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIsEmailVerfiedFlag = true;
        _fieldIsEmailVerfied = value;
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
_fieldUserIDFlag = false;
_fieldUserID = c.convertToInt32(_table.Rows[_currentIndex]["userID"]);
_fieldUserNameFlag = false;
_fieldUserName = c.convertToString(_table.Rows[_currentIndex]["userName"]);
_fieldPasswordFlag = false;
_fieldPassword = c.convertToString(_table.Rows[_currentIndex]["password"]);
_fieldFullNameFlag = false;
_fieldFullName = c.convertToString(_table.Rows[_currentIndex]["fullName"]);
_fieldGrpIDFlag = false;
_fieldGrpID = c.convertToInt32(_table.Rows[_currentIndex]["grpID"]);
_fieldActiveFlag = false;
_fieldActive = c.convertToBool(_table.Rows[_currentIndex]["active"]);
_fieldCompanyIDFlag = false;
_fieldCompanyID = c.convertToInt32(_table.Rows[_currentIndex]["companyID"]);
_fieldBranchIDFlag = false;
_fieldBranchID = c.convertToInt32(_table.Rows[_currentIndex]["branchID"]);
_fieldDepartmentIDFlag = false;
_fieldDepartmentID = c.convertToInt32(_table.Rows[_currentIndex]["departmentID"]);
_fieldPositionIDFlag = false;
_fieldPositionID = c.convertToInt32(_table.Rows[_currentIndex]["positionID"]);
_fieldEmailFlag = false;
_fieldEmail = c.convertToString(_table.Rows[_currentIndex]["email"]);
_fieldAllowCustomWFFlag = false;
_fieldAllowCustomWF = c.convertToBool(_table.Rows[_currentIndex]["allowCustomWF"]);
_fieldAllowCreateFoldersFlag = false;
_fieldAllowCreateFolders = c.convertToBool(_table.Rows[_currentIndex]["allowCreateFolders"]);
_fieldAllowReplaceDocumentsFlag = false;
_fieldAllowReplaceDocuments = c.convertToBool(_table.Rows[_currentIndex]["allowReplaceDocuments"]);
_fieldAllowDiwanFlag = false;
_fieldAllowDiwan = c.convertToBool(_table.Rows[_currentIndex]["allowDiwan"]);
_fieldIsFirstLoginFlag = false;
_fieldIsFirstLogin = c.convertToBool(_table.Rows[_currentIndex]["isFirstLogin"]);
_fieldPasswordCreationDateFlag = false;
_fieldPasswordCreationDate = c.convertToDateTime(_table.Rows[_currentIndex]["passwordCreationDate"]);
_fieldPasswordModifiedDateFlag = false;
_fieldPasswordModifiedDate = c.convertToDateTime(_table.Rows[_currentIndex]["passwordModifiedDate"]);
_fieldLastPasswordFlag = false;
_fieldLastPassword = c.convertToString(_table.Rows[_currentIndex]["lastPassword"]);
_fieldSignatureFlag = false;
_fieldSignature = c.convertToString(_table.Rows[_currentIndex]["Signature"]);
_fieldPhoneFlag = false;
_fieldPhone = c.convertToString(_table.Rows[_currentIndex]["Phone"]);
_fieldIsMobileFirstLoginFlag = false;
_fieldIsMobileFirstLogin = c.convertToBool(_table.Rows[_currentIndex]["isMobileFirstLogin"]);
_fieldIsEmailVerfiedFlag = false;
_fieldIsEmailVerfied = c.convertToBool(_table.Rows[_currentIndex]["isEmailVerfied"]);
_fieldClientIdFlag = false;
_fieldClientId = c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]);

}
public void update()
{
op.dboUpdateUsersByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["userID"]),_fieldUserName,_fieldPassword,_fieldFullName,_fieldGrpID,_fieldActive,_fieldCompanyID,_fieldBranchID,_fieldDepartmentID,_fieldPositionID,_fieldEmail,_fieldAllowCustomWF,_fieldAllowCreateFolders,_fieldAllowReplaceDocuments,_fieldAllowDiwan,_fieldIsFirstLogin,_fieldPasswordCreationDate,_fieldPasswordModifiedDate,_fieldLastPassword,_fieldSignature,_fieldPhone,_fieldIsMobileFirstLogin,_fieldIsEmailVerfied,_fieldClientId);
}

       

    }
}
}
