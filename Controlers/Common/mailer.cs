using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMS.DAL;
    public static class mailer
    {
        public enum location
        { 
            DocumentInfo, Inbox
        }
        public static bool sendMail(string Email,string URL,Int64 docID, location Location)
        {
            try
            {
                string _location = "";
                switch (Location)
                {
                    case location.DocumentInfo:
                        _location = "docID";
                        break;
                    case location.Inbox:
                        _location = "mailID";
                        break;
                }

                CommonFunction.clsCommon c = new CommonFunction.clsCommon();

                DMS.DAL.operations op = new DMS.DAL.operations();
                tables.dbo.settings settings = op.dboGetSettingsByPrimaryKey(1);

                System.Net.Mail.SmtpClient M = new System.Net.Mail.SmtpClient(settings.fieldOutgoingMailServer);
                M.EnableSsl = Convert.ToBoolean(dms.sysSettings.getSettingValue("MailSSL"));
                System.Net.Mail.MailMessage MailMsg;
                MailMsg = new System.Net.Mail.MailMessage(settings.fieldWorkflowEmail, Email);
                M.Credentials = new System.Net.NetworkCredential(settings.fieldWorkflowEmail, c.decrypt(settings.fieldWorkflowEmailPassword));
                M.Port = Convert.ToInt32(dms.sysSettings.getSettingValue("MailPort"));
                string siteURL = URL;
                siteURL = siteURL.Remove(siteURL.LastIndexOf('/'));
                siteURL = siteURL.Remove(siteURL.LastIndexOf('/'));
                string docLink = siteURL + "/Screen/?" + _location + "=" + c.encrypt(docID.ToString());
                MailMsg.BodyEncoding = System.Text.UTF8Encoding.UTF8;
                MailMsg.Subject = settings.fieldWorkflowEmailSubject;
                MailMsg.Body = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'><html><body style='font-family:Arial'>";
                MailMsg.Body += "<div style='vertical-align:middle; background-color:none; padding:10px;font-size:12pt;'>";
                MailMsg.Body += settings.fieldWorkflowEmailBody;
                MailMsg.Body += "<br/><br/>Document Link : <a href='" + docLink + "'>" + docLink + "</a>";
                MailMsg.Body += "<br/><br/><hr/><br/>" + settings.fieldSystemEmailSignature;
                MailMsg.Body += "</body></html>";
                MailMsg.IsBodyHtml = true;
                M.Send(MailMsg);

                return true;
            }
            catch {
                return false;
            }
        }
    }
