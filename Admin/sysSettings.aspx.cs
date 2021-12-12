using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Admin
{
    public partial class sysSettings : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        Int32 ID; string allowedUsersCount; string systemActive; string systemActiveDate; Int16 passwordStrength; bool passwordAllowStartSpace; Int16 passwordLength; bool allowUsernamePasswordMatch; bool firstLoginChangePassword; Int32 passwordAgeDays; Int32 sessionTimeoutMinutes; Int16 lockTimeOut; string outgoingMailServer; string workflowEmail; string workflowEmailPassword; string systemEmail; string systemEmailPassword; string workflowEmailSubject; string workflowEmailBody; string systemEmailSignature;
        Int32 clientID;

        public void fillVariables()
        {
            ID = 1;
            allowedUsersCount = hdnAllowedUsersCount.Value;
            systemActive = hdnSystemActive.Value;
            systemActiveDate = hdnSystemActiveDate.Value;

            passwordStrength = c.convertToInt16(drpPasswordStrength.SelectedValue);
            passwordAllowStartSpace = chkPasswordAllowStartSpace.Checked;
            passwordLength = c.convertToInt16(txtPasswordLength.Value);
            allowUsernamePasswordMatch = chkAllowUsernamePasswordMatch.Checked;
            firstLoginChangePassword = chkFirstLoginChangePassword.Checked;
            passwordAgeDays = c.convertToInt32(txtPasswordAgeDays.Value);
            sessionTimeoutMinutes = c.convertToInt32(txtSessionTimeoutMinutes.Value);
            lockTimeOut = c.convertToInt16(txtLockTimeOut.Value);

            outgoingMailServer = c.convertToString(txtOutgoingMailServer.Text);
            workflowEmail = c.convertToString(txtWorkflowEmail.Text);
            if (txtWorkflowEmailPassword.Value != "")
                workflowEmailPassword = c.encrypt(txtWorkflowEmailPassword.Value);
            else
                workflowEmailPassword = hdnWorkflowEmailPassword.Value;

            systemEmail = c.convertToString(txtSystemEmail.Value);
            if (txtSystemEmailPassword.Value != "")
                systemEmailPassword = c.encrypt(txtSystemEmailPassword.Value);
            else
                systemEmailPassword = hdnSystemEmailPassword.Value;

            workflowEmailSubject = c.convertToString(txtWorkflowEmailSubject.Value);
            workflowEmailBody = c.convertToString(txtWorkflowEmailBody.Value);
            systemEmailSignature = c.convertToString(txtSystemEmailSignature.Value);
            clientID = c.convertToInt32(Session["clientId"]);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Localize();
            if (!IsPostBack)
            {
                tables.dbo.settings settingsDS = new tables.dbo.settings();
                op = new DMS.DAL.operations();
                settingsDS = op.dboGetSettingsByPrimaryKey(1);

                hdnID.Value = "1";
                hdnAllowedUsersCount.Value = settingsDS.fieldAllowedUsersCount;
                txtAllowedUsersCount.Value = c.decrypt(settingsDS.fieldAllowedUsersCount);

                hdnSystemActive.Value = settingsDS.fieldSystemActive;
                txtSystemActive.Value = c.decrypt(settingsDS.fieldSystemActive);

                hdnSystemActiveDate.Value = settingsDS.fieldSystemActiveDate;
                txtSystemActiveDate.Value = c.decrypt(settingsDS.fieldSystemActiveDate);

                drpPasswordStrength.Items.Clear();
                //drpPasswordStrength.Items.Add(new ListItem(HudhudResources.Resources.Admin_sysSettings_WeakAllowedanyinput, "1"));
                //drpPasswordStrength.Items.Add(new ListItem(HudhudResources.Resources.Admin_sysSettings_MediumMustcontainsAlphanumeric, "2"));
                //drpPasswordStrength.Items.Add(new ListItem(HudhudResources.Resources.Admin_sysSettings_StrongMustcontainsAlphanumericandSymbols, "3"));
                drpPasswordStrength.SelectedValue = settingsDS.fieldPasswordStrength.ToString();
                chkPasswordAllowStartSpace.Checked = settingsDS.fieldPasswordAllowStartSpace;
                txtPasswordLength.Value = settingsDS.fieldPasswordLength.ToString();
                chkAllowUsernamePasswordMatch.Checked = settingsDS.fieldAllowUsernamePasswordMatch ;
                chkFirstLoginChangePassword.Checked = settingsDS.fieldFirstLoginChangePassword;
                txtPasswordAgeDays.Value = settingsDS.fieldPasswordAgeDays.ToString();
                txtSessionTimeoutMinutes.Value = settingsDS.fieldSessionTimeoutMinutes.ToString();
                txtLockTimeOut.Value = settingsDS.fieldLockTimeOut.ToString();

                txtOutgoingMailServer.Text = settingsDS.fieldOutgoingMailServer;
                txtWorkflowEmail.Text = settingsDS.fieldWorkflowEmail;

                txtWorkflowEmailPassword.Value = c.decrypt(settingsDS.fieldWorkflowEmailPassword);

                hdnWorkflowEmailPassword.Value = settingsDS.fieldWorkflowEmailPassword;

                txtSystemEmail.Value = settingsDS.fieldSystemEmail;

                txtSystemEmailPassword.Value = c.decrypt(settingsDS.fieldSystemEmailPassword);

                hdnSystemEmailPassword.Value = settingsDS.fieldSystemEmailPassword;

                txtWorkflowEmailSubject.Value = settingsDS.fieldWorkflowEmailSubject;
                txtWorkflowEmailBody.Value = settingsDS.fieldWorkflowEmailBody;
                txtSystemEmailSignature.Value = settingsDS.fieldSystemEmailSignature;

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            op = new DMS.DAL.operations();
            fillVariables();
            op.dboUpdateSettingsByPrimaryKey(ID, allowedUsersCount, systemActive, systemActiveDate, passwordStrength, passwordAllowStartSpace, passwordLength, allowUsernamePasswordMatch, firstLoginChangePassword, passwordAgeDays, sessionTimeoutMinutes, lockTimeOut, outgoingMailServer, workflowEmail, workflowEmailPassword, systemEmail, systemEmailPassword, workflowEmailSubject, workflowEmailBody, systemEmailSignature, clientID);
            if (Session["lang"].ToString() == "1")
                lblRes.Text = "تم حفظ التعديلات";
            else
                lblRes.Text = "Changes saved";
        }
        private void Localize()
        {
            if (Session["lang"].ToString() == "0")
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            else
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("ar");
            lnkLicense.InnerHtml = HudhudResources.Resources.Admin_sysSettings_License;
            lnkPasswordPolicy.InnerHtml = HudhudResources.Resources.Admin_sysSettings_PasswordPolicy;
            lnkSettings.InnerHtml = HudhudResources.Resources.Admin_sysSettings_Settings;
            lnkEmailSettings.InnerHtml = HudhudResources.Resources.Admin_sysSettings_EmailSettings;
            lblEditLicense.InnerHtml = HudhudResources.Resources.Admin_sysSettings_EditLicense;
            lblAllowedUsersCount.InnerHtml = HudhudResources.Resources.Admin_sysSettings_AllowedUsersCount;
            lblSystemActiveDate.InnerHtml = HudhudResources.Resources.Admin_sysSettings_SystemActiveDate;
            lblSystemActive.InnerHtml = HudhudResources.Resources.Admin_sysSettings_SystemActive;
            lblEditPasswordpolicy.InnerHtml = HudhudResources.Resources.Admin_sysSettings_EditPasswordpolicy;
            lblPasswordStrength.InnerHtml = HudhudResources.Resources.Admin_sysSettings_PasswordStrength;
            lblPasswordminimumlength.InnerHtml = HudhudResources.Resources.Admin_sysSettings_Passwordminimumlength;
            lblPasswordAgeDays.InnerHtml = HudhudResources.Resources.Admin_sysSettings_PasswordAgeDays;
            lblFirstLoginChangePassword.InnerHtml = HudhudResources.Resources.Admin_sysSettings_FirstLoginChangePassword;
            lblAllowStartSpace.InnerHtml = HudhudResources.Resources.Admin_sysSettings_AllowStartSpace;
            lblAllowUsernamePasswordMatch.InnerHtml = HudhudResources.Resources.Admin_sysSettings_AllowUsernamePasswordMatch;
            lblEditSettings.InnerHtml = HudhudResources.Resources.Admin_sysSettings_EditSettings;
            lblSessionTimeoutMinutes.InnerHtml = HudhudResources.Resources.Admin_sysSettings_SessionTimeoutMinutes;
            lblLockTimeOut.InnerHtml = HudhudResources.Resources.Admin_sysSettings_LockTimeOut;
            lblEditEmailSettings.InnerHtml = HudhudResources.Resources.Admin_sysSettings_EditEmailSettings;
            lblOutgoingMailServer.InnerHtml = HudhudResources.Resources.Admin_sysSettings_OutgoingMailServer;
            lblWorkflowEmail.InnerHtml = HudhudResources.Resources.Admin_sysSettings_WorkflowEmail;
            lblSystemEmail.InnerHtml = HudhudResources.Resources.Admin_sysSettings_SystemEmail;
            lblWorkflowEmailBody.InnerHtml = HudhudResources.Resources.Admin_sysSettings_WorkflowEmailBody;
            lblWorkflowEmailSubject.InnerHtml = HudhudResources.Resources.Admin_sysSettings_WorkflowEmailSubject;
            lblWorkflowEmailPassword.InnerHtml = HudhudResources.Resources.Admin_sysSettings_WorkflowEmailPassword;
            lblSystemEmailPassword.InnerHtml = HudhudResources.Resources.Admin_sysSettings_SystemEmailPassword;
            lblSystemEmailSignature.InnerHtml = HudhudResources.Resources.Admin_sysSettings_SystemEmailSignature;
            lblSave1.InnerHtml = HudhudResources.Resources.Admin_sysSettings_Save;
            lblRetreat1.InnerHtml = HudhudResources.Resources.Admin_sysSettings_Retreat;
            lblSurvey1.InnerHtml = HudhudResources.Resources.Admin_sysSettings_Survey;
            lblSave2.InnerHtml = HudhudResources.Resources.Admin_sysSettings_Save;
            lblRetreat2.InnerHtml = HudhudResources.Resources.Admin_sysSettings_Retreat;
            lblSurvey2.InnerHtml = HudhudResources.Resources.Admin_sysSettings_Survey;
            lblSave3.InnerHtml = HudhudResources.Resources.Admin_sysSettings_Save;
            lblRetreat3.InnerHtml = HudhudResources.Resources.Admin_sysSettings_Retreat;
            lblSurvey3.InnerHtml = HudhudResources.Resources.Admin_sysSettings_Survey;
            lblSave4.InnerHtml = HudhudResources.Resources.Admin_sysSettings_Save;
            lblRetreat4.InnerHtml = HudhudResources.Resources.Admin_sysSettings_Retreat;
            lblSurvey4.InnerHtml = HudhudResources.Resources.Admin_sysSettings_Survey;
        }
    }
}