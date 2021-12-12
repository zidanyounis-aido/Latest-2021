using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace tables
{
namespace dbo
{
    public class settings 
    {
		DMS.DAL.operations op = new DMS.DAL.operations();
		CommonFunction.clsCommon c = new CommonFunction.clsCommon();
		public enum recordStatuses
        {
            Read,Update,Insert,Delete
        }

        public recordStatuses recordStatus = recordStatuses.Read;

        private DataTable _table = new DataTable("settings");
        public DataTable table
        {
            get { return _table;}
            set { _table = value; }
        }

        public settings () { }
        public settings (DataTable Table) 
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

        public string[] columnsArray = {"Int32 ID","string allowedUsersCount","string systemActive","string systemActiveDate","Int16 passwordStrength","bool passwordAllowStartSpace","Int16 passwordLength","bool allowUsernamePasswordMatch","bool firstLoginChangePassword","Int32 passwordAgeDays","Int32 sessionTimeoutMinutes","Int16 lockTimeOut","string outgoingMailServer","string workflowEmail","string workflowEmailPassword","string systemEmail","string systemEmailPassword","string workflowEmailSubject","string workflowEmailBody","string systemEmailSignature","Int32 ClientId"};
private Int32 _fieldID;
private bool _fieldIDFlag = false;
public Int32 fieldID
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldIDFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["ID"]);
        else
            return _fieldID;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldIDFlag = true;
        _fieldID = value;
        }
    }

private string _fieldAllowedUsersCount;
private bool _fieldAllowedUsersCountFlag = false;
public string fieldAllowedUsersCount
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowedUsersCountFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["allowedUsersCount"]);
        else
            return _fieldAllowedUsersCount;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowedUsersCountFlag = true;
        _fieldAllowedUsersCount = value;
        }
    }

private string _fieldSystemActive;
private bool _fieldSystemActiveFlag = false;
public string fieldSystemActive
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSystemActiveFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["systemActive"]);
        else
            return _fieldSystemActive;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSystemActiveFlag = true;
        _fieldSystemActive = value;
        }
    }

private string _fieldSystemActiveDate;
private bool _fieldSystemActiveDateFlag = false;
public string fieldSystemActiveDate
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSystemActiveDateFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["systemActiveDate"]);
        else
            return _fieldSystemActiveDate;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSystemActiveDateFlag = true;
        _fieldSystemActiveDate = value;
        }
    }

private Int16 _fieldPasswordStrength;
private bool _fieldPasswordStrengthFlag = false;
public Int16 fieldPasswordStrength
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPasswordStrengthFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["passwordStrength"]);
        else
            return _fieldPasswordStrength;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPasswordStrengthFlag = true;
        _fieldPasswordStrength = value;
        }
    }

private bool _fieldPasswordAllowStartSpace;
private bool _fieldPasswordAllowStartSpaceFlag = false;
public bool fieldPasswordAllowStartSpace
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPasswordAllowStartSpaceFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["passwordAllowStartSpace"]);
        else
            return _fieldPasswordAllowStartSpace;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPasswordAllowStartSpaceFlag = true;
        _fieldPasswordAllowStartSpace = value;
        }
    }

private Int16 _fieldPasswordLength;
private bool _fieldPasswordLengthFlag = false;
public Int16 fieldPasswordLength
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPasswordLengthFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["passwordLength"]);
        else
            return _fieldPasswordLength;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPasswordLengthFlag = true;
        _fieldPasswordLength = value;
        }
    }

private bool _fieldAllowUsernamePasswordMatch;
private bool _fieldAllowUsernamePasswordMatchFlag = false;
public bool fieldAllowUsernamePasswordMatch
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldAllowUsernamePasswordMatchFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["allowUsernamePasswordMatch"]);
        else
            return _fieldAllowUsernamePasswordMatch;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldAllowUsernamePasswordMatchFlag = true;
        _fieldAllowUsernamePasswordMatch = value;
        }
    }

private bool _fieldFirstLoginChangePassword;
private bool _fieldFirstLoginChangePasswordFlag = false;
public bool fieldFirstLoginChangePassword
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldFirstLoginChangePasswordFlag == false)
           return c.convertToBoolean(_table.Rows[_currentIndex]["firstLoginChangePassword"]);
        else
            return _fieldFirstLoginChangePassword;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldFirstLoginChangePasswordFlag = true;
        _fieldFirstLoginChangePassword = value;
        }
    }

private Int32 _fieldPasswordAgeDays;
private bool _fieldPasswordAgeDaysFlag = false;
public Int32 fieldPasswordAgeDays
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldPasswordAgeDaysFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["passwordAgeDays"]);
        else
            return _fieldPasswordAgeDays;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldPasswordAgeDaysFlag = true;
        _fieldPasswordAgeDays = value;
        }
    }

private Int32 _fieldSessionTimeoutMinutes;
private bool _fieldSessionTimeoutMinutesFlag = false;
public Int32 fieldSessionTimeoutMinutes
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSessionTimeoutMinutesFlag == false)
           return c.convertToInt32(_table.Rows[_currentIndex]["sessionTimeoutMinutes"]);
        else
            return _fieldSessionTimeoutMinutes;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSessionTimeoutMinutesFlag = true;
        _fieldSessionTimeoutMinutes = value;
        }
    }

private Int16 _fieldLockTimeOut;
private bool _fieldLockTimeOutFlag = false;
public Int16 fieldLockTimeOut
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldLockTimeOutFlag == false)
           return c.convertToInt16(_table.Rows[_currentIndex]["lockTimeOut"]);
        else
            return _fieldLockTimeOut;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldLockTimeOutFlag = true;
        _fieldLockTimeOut = value;
        }
    }

private string _fieldOutgoingMailServer;
private bool _fieldOutgoingMailServerFlag = false;
public string fieldOutgoingMailServer
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldOutgoingMailServerFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["outgoingMailServer"]);
        else
            return _fieldOutgoingMailServer;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldOutgoingMailServerFlag = true;
        _fieldOutgoingMailServer = value;
        }
    }

private string _fieldWorkflowEmail;
private bool _fieldWorkflowEmailFlag = false;
public string fieldWorkflowEmail
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWorkflowEmailFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["workflowEmail"]);
        else
            return _fieldWorkflowEmail;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWorkflowEmailFlag = true;
        _fieldWorkflowEmail = value;
        }
    }

private string _fieldWorkflowEmailPassword;
private bool _fieldWorkflowEmailPasswordFlag = false;
public string fieldWorkflowEmailPassword
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWorkflowEmailPasswordFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["workflowEmailPassword"]);
        else
            return _fieldWorkflowEmailPassword;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWorkflowEmailPasswordFlag = true;
        _fieldWorkflowEmailPassword = value;
        }
    }

private string _fieldSystemEmail;
private bool _fieldSystemEmailFlag = false;
public string fieldSystemEmail
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSystemEmailFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["systemEmail"]);
        else
            return _fieldSystemEmail;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSystemEmailFlag = true;
        _fieldSystemEmail = value;
        }
    }

private string _fieldSystemEmailPassword;
private bool _fieldSystemEmailPasswordFlag = false;
public string fieldSystemEmailPassword
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSystemEmailPasswordFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["systemEmailPassword"]);
        else
            return _fieldSystemEmailPassword;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSystemEmailPasswordFlag = true;
        _fieldSystemEmailPassword = value;
        }
    }

private string _fieldWorkflowEmailSubject;
private bool _fieldWorkflowEmailSubjectFlag = false;
public string fieldWorkflowEmailSubject
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWorkflowEmailSubjectFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["workflowEmailSubject"]);
        else
            return _fieldWorkflowEmailSubject;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWorkflowEmailSubjectFlag = true;
        _fieldWorkflowEmailSubject = value;
        }
    }

private string _fieldWorkflowEmailBody;
private bool _fieldWorkflowEmailBodyFlag = false;
public string fieldWorkflowEmailBody
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldWorkflowEmailBodyFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["workflowEmailBody"]);
        else
            return _fieldWorkflowEmailBody;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldWorkflowEmailBodyFlag = true;
        _fieldWorkflowEmailBody = value;
        }
    }

private string _fieldSystemEmailSignature;
private bool _fieldSystemEmailSignatureFlag = false;
public string fieldSystemEmailSignature
    {
        get
        {
            if (recordStatus == recordStatuses.Read || _fieldSystemEmailSignatureFlag == false)
           return c.convertToString(_table.Rows[_currentIndex]["systemEmailSignature"]);
        else
            return _fieldSystemEmailSignature;
        }
        set {
        recordStatus = recordStatuses.Update;
        _fieldSystemEmailSignatureFlag = true;
        _fieldSystemEmailSignature = value;
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
_fieldIDFlag = false;
_fieldID = c.convertToInt32(_table.Rows[_currentIndex]["ID"]);
_fieldAllowedUsersCountFlag = false;
_fieldAllowedUsersCount = c.convertToString(_table.Rows[_currentIndex]["allowedUsersCount"]);
_fieldSystemActiveFlag = false;
_fieldSystemActive = c.convertToString(_table.Rows[_currentIndex]["systemActive"]);
_fieldSystemActiveDateFlag = false;
_fieldSystemActiveDate = c.convertToString(_table.Rows[_currentIndex]["systemActiveDate"]);
_fieldPasswordStrengthFlag = false;
_fieldPasswordStrength = c.convertToInt16(_table.Rows[_currentIndex]["passwordStrength"]);
_fieldPasswordAllowStartSpaceFlag = false;
_fieldPasswordAllowStartSpace = c.convertToBool(_table.Rows[_currentIndex]["passwordAllowStartSpace"]);
_fieldPasswordLengthFlag = false;
_fieldPasswordLength = c.convertToInt16(_table.Rows[_currentIndex]["passwordLength"]);
_fieldAllowUsernamePasswordMatchFlag = false;
_fieldAllowUsernamePasswordMatch = c.convertToBool(_table.Rows[_currentIndex]["allowUsernamePasswordMatch"]);
_fieldFirstLoginChangePasswordFlag = false;
_fieldFirstLoginChangePassword = c.convertToBool(_table.Rows[_currentIndex]["firstLoginChangePassword"]);
_fieldPasswordAgeDaysFlag = false;
_fieldPasswordAgeDays = c.convertToInt32(_table.Rows[_currentIndex]["passwordAgeDays"]);
_fieldSessionTimeoutMinutesFlag = false;
_fieldSessionTimeoutMinutes = c.convertToInt32(_table.Rows[_currentIndex]["sessionTimeoutMinutes"]);
_fieldLockTimeOutFlag = false;
_fieldLockTimeOut = c.convertToInt16(_table.Rows[_currentIndex]["lockTimeOut"]);
_fieldOutgoingMailServerFlag = false;
_fieldOutgoingMailServer = c.convertToString(_table.Rows[_currentIndex]["outgoingMailServer"]);
_fieldWorkflowEmailFlag = false;
_fieldWorkflowEmail = c.convertToString(_table.Rows[_currentIndex]["workflowEmail"]);
_fieldWorkflowEmailPasswordFlag = false;
_fieldWorkflowEmailPassword = c.convertToString(_table.Rows[_currentIndex]["workflowEmailPassword"]);
_fieldSystemEmailFlag = false;
_fieldSystemEmail = c.convertToString(_table.Rows[_currentIndex]["systemEmail"]);
_fieldSystemEmailPasswordFlag = false;
_fieldSystemEmailPassword = c.convertToString(_table.Rows[_currentIndex]["systemEmailPassword"]);
_fieldWorkflowEmailSubjectFlag = false;
_fieldWorkflowEmailSubject = c.convertToString(_table.Rows[_currentIndex]["workflowEmailSubject"]);
_fieldWorkflowEmailBodyFlag = false;
_fieldWorkflowEmailBody = c.convertToString(_table.Rows[_currentIndex]["workflowEmailBody"]);
_fieldSystemEmailSignatureFlag = false;
_fieldSystemEmailSignature = c.convertToString(_table.Rows[_currentIndex]["systemEmailSignature"]);
_fieldClientIdFlag = false;
_fieldClientId = c.convertToInt32(_table.Rows[_currentIndex]["ClientId"]);

}
public void update()
{
op.dboUpdateSettingsByPrimaryKey(c.convertToInt32(_table.Rows[_currentIndex]["ID"]),_fieldAllowedUsersCount,_fieldSystemActive,_fieldSystemActiveDate,_fieldPasswordStrength,_fieldPasswordAllowStartSpace,_fieldPasswordLength,_fieldAllowUsernamePasswordMatch,_fieldFirstLoginChangePassword,_fieldPasswordAgeDays,_fieldSessionTimeoutMinutes,_fieldLockTimeOut,_fieldOutgoingMailServer,_fieldWorkflowEmail,_fieldWorkflowEmailPassword,_fieldSystemEmail,_fieldSystemEmailPassword,_fieldWorkflowEmailSubject,_fieldWorkflowEmailBody,_fieldSystemEmailSignature,_fieldClientId);
}

       

    }
}
}
