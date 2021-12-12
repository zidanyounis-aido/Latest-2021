using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DMS.DAL
{
    public class operations
    {
        DataProccess dp = new DataProccess();

        public operations()
        {
            dp.catchingError += new DataProccess.catchingErrorHandle(catchingDataProccessError);
        }

        public void catchingDataProccessError(String Method, Exception e)
        {
            //get DataProccess Error and add it to logs
			catchingError(Method, e);
        }

		public delegate void catchingErrorHandle(String Method, Exception e);
        public event catchingErrorHandle catchingError;

        public Int32  dboDeleteBranchFoldersByPrimaryKey(Int32 branchID,Int32 fldrID)
{
dp.parameters.Clear();
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@fldrID",fldrID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteBranchFoldersByPrimaryKey");
return res;
}

public Int32  dboDeleteBranchFolders(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteBranchFolders");
return res;
}

public tables.dbo.branchFolders dboGetBranchFoldersByPrimaryKey(Int32 branchID,Int32 fldrID)
{
dp.parameters.Clear();
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@fldrID",fldrID);
tables.dbo.branchFolders varTable = new tables.dbo.branchFolders(dp.excuteQuery("dbo.getBranchFoldersByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.branchFolders dboGetAllBranchFolders(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.branchFolders varTable = new tables.dbo.branchFolders(dp.excuteQuery("dbo.getAllBranchFolders").Copy());
return varTable;
}

public Int32 dboAddBranchFolders(Int32 branchID,Int32 fldrID)
{
dp.parameters.Clear();
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@fldrID",fldrID);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addBranchFolders"));
return res;
}

public Int32  dboUpdateBranchFoldersByPrimaryKey(Int32 branchID,Int32 fldrID)
{
dp.parameters.Clear();
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@fldrID",fldrID);
Int32 res;
res = dp.excuteNonQuery("dbo.updateBranchFoldersByPrimaryKey");
return res;
}

public Int32  dboUpdateBranchFolders(Int32 branchID,Int32 fldrID,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateBranchFolders");
return res;
}

public Int32  dboDeleteBranchsByPrimaryKey(Int32 branchID)
{
dp.parameters.Clear();
dp.parameters.Add("@branchID",branchID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteBranchsByPrimaryKey");
return res;
}

public Int32  dboDeleteBranchs(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteBranchs");
return res;
}

public tables.dbo.branchs dboGetBranchsByPrimaryKey(Int32 branchID)
{
dp.parameters.Clear();
dp.parameters.Add("@branchID",branchID);
tables.dbo.branchs varTable = new tables.dbo.branchs(dp.excuteQuery("dbo.getBranchsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.branchs dboGetAllBranchs(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.branchs varTable = new tables.dbo.branchs(dp.excuteQuery("dbo.getAllBranchs").Copy());
return varTable;
}

public Int32 dboAddBranchs(Int32 companyID,string branchName,string address,string tel1,string tel2,string zipcode,string mainEmail,string description,bool isMainBranch,string branchNameAr)
{
dp.parameters.Clear();
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@branchName",branchName);
dp.parameters.Add("@address",address);
dp.parameters.Add("@tel1",tel1);
dp.parameters.Add("@tel2",tel2);
dp.parameters.Add("@zipcode",zipcode);
dp.parameters.Add("@mainEmail",mainEmail);
dp.parameters.Add("@description",description);
dp.parameters.Add("@isMainBranch",isMainBranch);
dp.parameters.Add("@branchNameAr",branchNameAr);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addBranchs"));
return res;
}

public Int32  dboUpdateBranchsByPrimaryKey(Int32 branchID,Int32 companyID,string branchName,string address,string tel1,string tel2,string zipcode,string mainEmail,string description,bool isMainBranch,string branchNameAr)
{
dp.parameters.Clear();
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@branchName",branchName);
dp.parameters.Add("@address",address);
dp.parameters.Add("@tel1",tel1);
dp.parameters.Add("@tel2",tel2);
dp.parameters.Add("@zipcode",zipcode);
dp.parameters.Add("@mainEmail",mainEmail);
dp.parameters.Add("@description",description);
dp.parameters.Add("@isMainBranch",isMainBranch);
dp.parameters.Add("@branchNameAr",branchNameAr);
Int32 res;
res = dp.excuteNonQuery("dbo.updateBranchsByPrimaryKey");
return res;
}

public Int32  dboUpdateBranchs(Int32 branchID,Int32 companyID,string branchName,string address,string tel1,string tel2,string zipcode,string mainEmail,string description,bool isMainBranch,string branchNameAr,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@branchName",branchName);
dp.parameters.Add("@address",address);
dp.parameters.Add("@tel1",tel1);
dp.parameters.Add("@tel2",tel2);
dp.parameters.Add("@zipcode",zipcode);
dp.parameters.Add("@mainEmail",mainEmail);
dp.parameters.Add("@description",description);
dp.parameters.Add("@isMainBranch",isMainBranch);
dp.parameters.Add("@branchNameAr",branchNameAr);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateBranchs");
return res;
}

public Int32  dboDeleteBrowseingEventsByPrimaryKey(Int32 browseEventID)
{
dp.parameters.Clear();
dp.parameters.Add("@browseEventID",browseEventID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteBrowseingEventsByPrimaryKey");
return res;
}

public Int32  dboDeleteBrowseingEvents(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteBrowseingEvents");
return res;
}

public tables.dbo.browseingEvents dboGetBrowseingEventsByPrimaryKey(Int32 browseEventID)
{
dp.parameters.Clear();
dp.parameters.Add("@browseEventID",browseEventID);
tables.dbo.browseingEvents varTable = new tables.dbo.browseingEvents(dp.excuteQuery("dbo.getBrowseingEventsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.browseingEvents dboGetAllBrowseingEvents(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.browseingEvents varTable = new tables.dbo.browseingEvents(dp.excuteQuery("dbo.getAllBrowseingEvents").Copy());
return varTable;
}

public Int32 dboAddBrowseingEvents(Int32 sysEventID,Int32 pageID)
{
dp.parameters.Clear();
dp.parameters.Add("@sysEventID",sysEventID);
dp.parameters.Add("@pageID",pageID);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addBrowseingEvents"));
return res;
}

public Int32  dboUpdateBrowseingEventsByPrimaryKey(Int32 browseEventID,Int32 sysEventID,Int32 pageID)
{
dp.parameters.Clear();
dp.parameters.Add("@browseEventID",browseEventID);
dp.parameters.Add("@sysEventID",sysEventID);
dp.parameters.Add("@pageID",pageID);
Int32 res;
res = dp.excuteNonQuery("dbo.updateBrowseingEventsByPrimaryKey");
return res;
}

public Int32  dboUpdateBrowseingEvents(Int32 browseEventID,Int32 sysEventID,Int32 pageID,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@browseEventID",browseEventID);
dp.parameters.Add("@sysEventID",sysEventID);
dp.parameters.Add("@pageID",pageID);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateBrowseingEvents");
return res;
}

public Int32  dboDeleteClientsByPrimaryKey(Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@ClientId",ClientId);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteClientsByPrimaryKey");
return res;
}

public Int32  dboDeleteClients(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteClients");
return res;
}

public tables.dbo.Clients dboGetClientsByPrimaryKey(Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@ClientId",ClientId);
tables.dbo.Clients varTable = new tables.dbo.Clients(dp.excuteQuery("dbo.getClientsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.Clients dboGetAllClients(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.Clients varTable = new tables.dbo.Clients(dp.excuteQuery("dbo.getAllClients").Copy());
return varTable;
}

public Int32 dboAddClients(string ClientName,string ClientEmail,Int32 CountryId,Int32 DefualtLanguageID,string ClientPhone,DateTime CreatedDate,bool IsActive)
{
dp.parameters.Clear();
dp.parameters.Add("@ClientName",ClientName);
dp.parameters.Add("@ClientEmail",ClientEmail);
dp.parameters.Add("@CountryId",CountryId);
dp.parameters.Add("@DefualtLanguageID",DefualtLanguageID);
dp.parameters.Add("@ClientPhone",ClientPhone);
dp.parameters.Add("@CreatedDate",CreatedDate);
dp.parameters.Add("@IsActive",IsActive);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addClients"));
return res;
}

public Int32  dboUpdateClientsByPrimaryKey(Int32 ClientId,string ClientName,string ClientEmail,Int32 CountryId,Int32 DefualtLanguageID,string ClientPhone,DateTime CreatedDate,bool IsActive)
{
dp.parameters.Clear();
dp.parameters.Add("@ClientId",ClientId);
dp.parameters.Add("@ClientName",ClientName);
dp.parameters.Add("@ClientEmail",ClientEmail);
dp.parameters.Add("@CountryId",CountryId);
dp.parameters.Add("@DefualtLanguageID",DefualtLanguageID);
dp.parameters.Add("@ClientPhone",ClientPhone);
dp.parameters.Add("@CreatedDate",CreatedDate);
dp.parameters.Add("@IsActive",IsActive);
Int32 res;
res = dp.excuteNonQuery("dbo.updateClientsByPrimaryKey");
return res;
}

public Int32  dboUpdateClients(Int32 ClientId,string ClientName,string ClientEmail,Int32 CountryId,Int32 DefualtLanguageID,string ClientPhone,DateTime CreatedDate,bool IsActive,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@ClientId",ClientId);
dp.parameters.Add("@ClientName",ClientName);
dp.parameters.Add("@ClientEmail",ClientEmail);
dp.parameters.Add("@CountryId",CountryId);
dp.parameters.Add("@DefualtLanguageID",DefualtLanguageID);
dp.parameters.Add("@ClientPhone",ClientPhone);
dp.parameters.Add("@CreatedDate",CreatedDate);
dp.parameters.Add("@IsActive",IsActive);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateClients");
return res;
}

public Int32  dboDeleteCompaniesByPrimaryKey(Int32 companyID)
{
dp.parameters.Clear();
dp.parameters.Add("@companyID",companyID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteCompaniesByPrimaryKey");
return res;
}

public Int32  dboDeleteCompanies(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteCompanies");
return res;
}

public tables.dbo.companies dboGetCompaniesByPrimaryKey(Int32 companyID)
{
dp.parameters.Clear();
dp.parameters.Add("@companyID",companyID);
tables.dbo.companies varTable = new tables.dbo.companies(dp.excuteQuery("dbo.getCompaniesByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.companies dboGetAllCompanies(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.companies varTable = new tables.dbo.companies(dp.excuteQuery("dbo.getAllCompanies").Copy());
return varTable;
}

public Int32 dboAddCompanies(string companyName,string address,string tel1,string tel2,string zipcode,string mainEmail,string description,string companyNameAr,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@companyName",companyName);
dp.parameters.Add("@address",address);
dp.parameters.Add("@tel1",tel1);
dp.parameters.Add("@tel2",tel2);
dp.parameters.Add("@zipcode",zipcode);
dp.parameters.Add("@mainEmail",mainEmail);
dp.parameters.Add("@description",description);
dp.parameters.Add("@companyNameAr",companyNameAr);
dp.parameters.Add("@ClientId",ClientId);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addCompanies"));
return res;
}

public Int32  dboUpdateCompaniesByPrimaryKey(Int32 companyID,string companyName,string address,string tel1,string tel2,string zipcode,string mainEmail,string description,string companyNameAr,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@companyName",companyName);
dp.parameters.Add("@address",address);
dp.parameters.Add("@tel1",tel1);
dp.parameters.Add("@tel2",tel2);
dp.parameters.Add("@zipcode",zipcode);
dp.parameters.Add("@mainEmail",mainEmail);
dp.parameters.Add("@description",description);
dp.parameters.Add("@companyNameAr",companyNameAr);
dp.parameters.Add("@ClientId",ClientId);
Int32 res;
res = dp.excuteNonQuery("dbo.updateCompaniesByPrimaryKey");
return res;
}

public Int32  dboUpdateCompanies(Int32 companyID,string companyName,string address,string tel1,string tel2,string zipcode,string mainEmail,string description,string companyNameAr,Int32 ClientId,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@companyName",companyName);
dp.parameters.Add("@address",address);
dp.parameters.Add("@tel1",tel1);
dp.parameters.Add("@tel2",tel2);
dp.parameters.Add("@zipcode",zipcode);
dp.parameters.Add("@mainEmail",mainEmail);
dp.parameters.Add("@description",description);
dp.parameters.Add("@companyNameAr",companyNameAr);
dp.parameters.Add("@ClientId",ClientId);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateCompanies");
return res;
}

public Int32  dboDeleteCompanyFoldersByPrimaryKey(Int32 companyID,Int32 fldrID)
{
dp.parameters.Clear();
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@fldrID",fldrID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteCompanyFoldersByPrimaryKey");
return res;
}

public Int32  dboDeleteCompanyFolders(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteCompanyFolders");
return res;
}

public tables.dbo.companyFolders dboGetCompanyFoldersByPrimaryKey(Int32 companyID,Int32 fldrID)
{
dp.parameters.Clear();
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@fldrID",fldrID);
tables.dbo.companyFolders varTable = new tables.dbo.companyFolders(dp.excuteQuery("dbo.getCompanyFoldersByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.companyFolders dboGetAllCompanyFolders(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.companyFolders varTable = new tables.dbo.companyFolders(dp.excuteQuery("dbo.getAllCompanyFolders").Copy());
return varTable;
}

public Int32 dboAddCompanyFolders(Int32 companyID,Int32 fldrID)
{
dp.parameters.Clear();
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@fldrID",fldrID);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addCompanyFolders"));
return res;
}

public Int32  dboUpdateCompanyFoldersByPrimaryKey(Int32 companyID,Int32 fldrID)
{
dp.parameters.Clear();
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@fldrID",fldrID);
Int32 res;
res = dp.excuteNonQuery("dbo.updateCompanyFoldersByPrimaryKey");
return res;
}

public Int32  dboUpdateCompanyFolders(Int32 companyID,Int32 fldrID,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateCompanyFolders");
return res;
}

public Int32  dboDeleteControlsTypesByPrimaryKey(Int32 crtlID)
{
dp.parameters.Clear();
dp.parameters.Add("@crtlID",crtlID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteControlsTypesByPrimaryKey");
return res;
}

public Int32  dboDeleteControlsTypes(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteControlsTypes");
return res;
}

public tables.dbo.controlsTypes dboGetControlsTypesByPrimaryKey(Int32 crtlID)
{
dp.parameters.Clear();
dp.parameters.Add("@crtlID",crtlID);
tables.dbo.controlsTypes varTable = new tables.dbo.controlsTypes(dp.excuteQuery("dbo.getControlsTypesByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.controlsTypes dboGetAllControlsTypes(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.controlsTypes varTable = new tables.dbo.controlsTypes(dp.excuteQuery("dbo.getAllControlsTypes").Copy());
return varTable;
}

public Int32 dboAddControlsTypes(Int32 crtlID,string ctrlDesc,string ctrlDescAr)
{
dp.parameters.Clear();
dp.parameters.Add("@crtlID",crtlID);
dp.parameters.Add("@ctrlDesc",ctrlDesc);
dp.parameters.Add("@ctrlDescAr",ctrlDescAr);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addControlsTypes"));
return res;
}

public Int32  dboUpdateControlsTypesByPrimaryKey(Int32 crtlID,string ctrlDesc,string ctrlDescAr)
{
dp.parameters.Clear();
dp.parameters.Add("@crtlID",crtlID);
dp.parameters.Add("@ctrlDesc",ctrlDesc);
dp.parameters.Add("@ctrlDescAr",ctrlDescAr);
Int32 res;
res = dp.excuteNonQuery("dbo.updateControlsTypesByPrimaryKey");
return res;
}

public Int32  dboUpdateControlsTypes(Int32 crtlID,string ctrlDesc,string ctrlDescAr,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@crtlID",crtlID);
dp.parameters.Add("@ctrlDesc",ctrlDesc);
dp.parameters.Add("@ctrlDescAr",ctrlDescAr);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateControlsTypes");
return res;
}

public Int32  dboDeleteDataBaseEventsByPrimaryKey(Int32 DBEventID)
{
dp.parameters.Clear();
dp.parameters.Add("@DBEventID",DBEventID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDataBaseEventsByPrimaryKey");
return res;
}

public Int32  dboDeleteDataBaseEvents(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDataBaseEvents");
return res;
}

public tables.dbo.dataBaseEvents dboGetDataBaseEventsByPrimaryKey(Int32 DBEventID)
{
dp.parameters.Clear();
dp.parameters.Add("@DBEventID",DBEventID);
tables.dbo.dataBaseEvents varTable = new tables.dbo.dataBaseEvents(dp.excuteQuery("dbo.getDataBaseEventsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.dataBaseEvents dboGetAllDataBaseEvents(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.dataBaseEvents varTable = new tables.dbo.dataBaseEvents(dp.excuteQuery("dbo.getAllDataBaseEvents").Copy());
return varTable;
}

public Int32 dboAddDataBaseEvents(Int32 sysEventID,Int32 DBActionTypeID,string tableName,string parameters)
{
dp.parameters.Clear();
dp.parameters.Add("@sysEventID",sysEventID);
dp.parameters.Add("@DBActionTypeID",DBActionTypeID);
dp.parameters.Add("@tableName",tableName);
dp.parameters.Add("@parameters",parameters);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addDataBaseEvents"));
return res;
}

public Int32  dboUpdateDataBaseEventsByPrimaryKey(Int32 DBEventID,Int32 sysEventID,Int32 DBActionTypeID,string tableName,string parameters)
{
dp.parameters.Clear();
dp.parameters.Add("@DBEventID",DBEventID);
dp.parameters.Add("@sysEventID",sysEventID);
dp.parameters.Add("@DBActionTypeID",DBActionTypeID);
dp.parameters.Add("@tableName",tableName);
dp.parameters.Add("@parameters",parameters);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDataBaseEventsByPrimaryKey");
return res;
}

public Int32  dboUpdateDataBaseEvents(Int32 DBEventID,Int32 sysEventID,Int32 DBActionTypeID,string tableName,string parameters,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@DBEventID",DBEventID);
dp.parameters.Add("@sysEventID",sysEventID);
dp.parameters.Add("@DBActionTypeID",DBActionTypeID);
dp.parameters.Add("@tableName",tableName);
dp.parameters.Add("@parameters",parameters);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDataBaseEvents");
return res;
}

public Int32  dboDeleteDBActionsTypesByPrimaryKey(Int32 DBActionTypeID)
{
dp.parameters.Clear();
dp.parameters.Add("@DBActionTypeID",DBActionTypeID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDBActionsTypesByPrimaryKey");
return res;
}

public Int32  dboDeleteDBActionsTypes(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDBActionsTypes");
return res;
}

public tables.dbo.DBActionsTypes dboGetDBActionsTypesByPrimaryKey(Int32 DBActionTypeID)
{
dp.parameters.Clear();
dp.parameters.Add("@DBActionTypeID",DBActionTypeID);
tables.dbo.DBActionsTypes varTable = new tables.dbo.DBActionsTypes(dp.excuteQuery("dbo.getDBActionsTypesByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.DBActionsTypes dboGetAllDBActionsTypes(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.DBActionsTypes varTable = new tables.dbo.DBActionsTypes(dp.excuteQuery("dbo.getAllDBActionsTypes").Copy());
return varTable;
}

public Int32 dboAddDBActionsTypes(Int32 DBActionTypeID,string DBActionTypeDescA,string FBActionTypeDescE)
{
dp.parameters.Clear();
dp.parameters.Add("@DBActionTypeID",DBActionTypeID);
dp.parameters.Add("@DBActionTypeDescA",DBActionTypeDescA);
dp.parameters.Add("@FBActionTypeDescE",FBActionTypeDescE);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addDBActionsTypes"));
return res;
}

public Int32  dboUpdateDBActionsTypesByPrimaryKey(Int32 DBActionTypeID,string DBActionTypeDescA,string FBActionTypeDescE)
{
dp.parameters.Clear();
dp.parameters.Add("@DBActionTypeID",DBActionTypeID);
dp.parameters.Add("@DBActionTypeDescA",DBActionTypeDescA);
dp.parameters.Add("@FBActionTypeDescE",FBActionTypeDescE);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDBActionsTypesByPrimaryKey");
return res;
}

public Int32  dboUpdateDBActionsTypes(Int32 DBActionTypeID,string DBActionTypeDescA,string FBActionTypeDescE,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@DBActionTypeID",DBActionTypeID);
dp.parameters.Add("@DBActionTypeDescA",DBActionTypeDescA);
dp.parameters.Add("@FBActionTypeDescE",FBActionTypeDescE);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDBActionsTypes");
return res;
}

public Int32  dboDeleteDepartmentsByPrimaryKey(Int32 departmentID)
{
dp.parameters.Clear();
dp.parameters.Add("@departmentID",departmentID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDepartmentsByPrimaryKey");
return res;
}

public Int32  dboDeleteDepartments(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDepartments");
return res;
}

public tables.dbo.departments dboGetDepartmentsByPrimaryKey(Int32 departmentID)
{
dp.parameters.Clear();
dp.parameters.Add("@departmentID",departmentID);
tables.dbo.departments varTable = new tables.dbo.departments(dp.excuteQuery("dbo.getDepartmentsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.departments dboGetAllDepartments(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.departments varTable = new tables.dbo.departments(dp.excuteQuery("dbo.getAllDepartments").Copy());
return varTable;
}

public Int32 dboAddDepartments(string departmentName,Int32 headUserID,Int32 parentDepartmentID,string departmentNameAr,Int32 parentID,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@departmentName",departmentName);
dp.parameters.Add("@headUserID",headUserID);
dp.parameters.Add("@parentDepartmentID",parentDepartmentID);
dp.parameters.Add("@departmentNameAr",departmentNameAr);
dp.parameters.Add("@parentID",parentID);
dp.parameters.Add("@ClientId",ClientId);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addDepartments"));
return res;
}

public Int32  dboUpdateDepartmentsByPrimaryKey(Int32 departmentID,string departmentName,Int32 headUserID,Int32 parentDepartmentID,string departmentNameAr,Int32 parentID,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@departmentID",departmentID);
dp.parameters.Add("@departmentName",departmentName);
dp.parameters.Add("@headUserID",headUserID);
dp.parameters.Add("@parentDepartmentID",parentDepartmentID);
dp.parameters.Add("@departmentNameAr",departmentNameAr);
dp.parameters.Add("@parentID",parentID);
dp.parameters.Add("@ClientId",ClientId);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDepartmentsByPrimaryKey");
return res;
}

public Int32  dboUpdateDepartments(Int32 departmentID,string departmentName,Int32 headUserID,Int32 parentDepartmentID,string departmentNameAr,Int32 parentID,Int32 ClientId,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@departmentID",departmentID);
dp.parameters.Add("@departmentName",departmentName);
dp.parameters.Add("@headUserID",headUserID);
dp.parameters.Add("@parentDepartmentID",parentDepartmentID);
dp.parameters.Add("@departmentNameAr",departmentNameAr);
dp.parameters.Add("@parentID",parentID);
dp.parameters.Add("@ClientId",ClientId);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDepartments");
return res;
}

public Int32  dboDeleteDocTypesByPrimaryKey(Int32 docTypID)
{
dp.parameters.Clear();
dp.parameters.Add("@docTypID",docTypID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocTypesByPrimaryKey");
return res;
}

public Int32  dboDeleteDocTypes(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocTypes");
return res;
}

public tables.dbo.docTypes dboGetDocTypesByPrimaryKey(Int32 docTypID)
{
dp.parameters.Clear();
dp.parameters.Add("@docTypID",docTypID);
tables.dbo.docTypes varTable = new tables.dbo.docTypes(dp.excuteQuery("dbo.getDocTypesByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.docTypes dboGetAllDocTypes(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.docTypes varTable = new tables.dbo.docTypes(dp.excuteQuery("dbo.getAllDocTypes").Copy());
return varTable;
}

public Int32 dboAddDocTypes(string docTypDesc,bool active,Int32 defaultWFID,string docTypDescAr,bool isTemplate,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@docTypDesc",docTypDesc);
dp.parameters.Add("@active",active);
dp.parameters.Add("@defaultWFID",defaultWFID);
dp.parameters.Add("@docTypDescAr",docTypDescAr);
dp.parameters.Add("@isTemplate",isTemplate);
dp.parameters.Add("@ClientId",ClientId);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addDocTypes"));
return res;
}

public Int32  dboUpdateDocTypesByPrimaryKey(Int32 docTypID,string docTypDesc,bool active,Int32 defaultWFID,string docTypDescAr,bool isTemplate,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@docTypID",docTypID);
dp.parameters.Add("@docTypDesc",docTypDesc);
dp.parameters.Add("@active",active);
dp.parameters.Add("@defaultWFID",defaultWFID);
dp.parameters.Add("@docTypDescAr",docTypDescAr);
dp.parameters.Add("@isTemplate",isTemplate);
dp.parameters.Add("@ClientId",ClientId);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocTypesByPrimaryKey");
return res;
}

public Int32  dboUpdateDocTypes(Int32 docTypID,string docTypDesc,bool active,Int32 defaultWFID,string docTypDescAr,bool isTemplate,Int32 ClientId,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@docTypID",docTypID);
dp.parameters.Add("@docTypDesc",docTypDesc);
dp.parameters.Add("@active",active);
dp.parameters.Add("@defaultWFID",defaultWFID);
dp.parameters.Add("@docTypDescAr",docTypDescAr);
dp.parameters.Add("@isTemplate",isTemplate);
dp.parameters.Add("@ClientId",ClientId);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocTypes");
return res;
}

public Int32  dboDeleteDocumentMataValuesByPrimaryKey(Int64 docID,Int32 metaID)
{
dp.parameters.Clear();
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@metaID",metaID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocumentMataValuesByPrimaryKey");
return res;
}

public Int32  dboDeleteDocumentMataValues(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocumentMataValues");
return res;
}

public tables.dbo.documentMataValues dboGetDocumentMataValuesByPrimaryKey(Int64 docID,Int32 metaID)
{
dp.parameters.Clear();
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@metaID",metaID);
tables.dbo.documentMataValues varTable = new tables.dbo.documentMataValues(dp.excuteQuery("dbo.getDocumentMataValuesByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.documentMataValues dboGetAllDocumentMataValues(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.documentMataValues varTable = new tables.dbo.documentMataValues(dp.excuteQuery("dbo.getAllDocumentMataValues").Copy());
return varTable;
}

public Int32 dboAddDocumentMataValues(Int32 metaID,Int64 docID,string value)
{
dp.parameters.Clear();
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@value",value);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addDocumentMataValues"));
return res;
}

public Int32  dboUpdateDocumentMataValuesByPrimaryKey(Int32 metaID,Int64 docID,string value)
{
dp.parameters.Clear();
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@value",value);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocumentMataValuesByPrimaryKey");
return res;
}

public Int32  dboUpdateDocumentMataValues(Int32 metaID,Int64 docID,string value,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@value",value);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocumentMataValues");
return res;
}

public Int32  dboDeleteDocumentsByPrimaryKey(Int64 docID)
{
dp.parameters.Clear();
dp.parameters.Add("@docID",docID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocumentsByPrimaryKey");
return res;
}

public Int32  dboDeleteDocuments(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocuments");
return res;
}

public tables.dbo.documents dboGetDocumentsByPrimaryKey(Int64 docID)
{
dp.parameters.Clear();
dp.parameters.Add("@docID",docID);
tables.dbo.documents varTable = new tables.dbo.documents(dp.excuteQuery("dbo.getDocumentsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.documents dboGetAllDocuments(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.documents varTable = new tables.dbo.documents(dp.excuteQuery("dbo.getAllDocuments").Copy());
return varTable;
}

public Int32 dboAddDocuments(Int32 docTypID,string docName,string docExt,DateTime addedDate,Int32 addedUserID,Int16 lastVersion,DateTime modifyDate,Int32 modifyUserID,Int32 fldrID,string ocrContent,Int64 folderSeq,Int64 docTypeSeq,Int64 folderDocTypeSeq,Int32 wfPathID,Int16 wfCurrentSeq,Int32 wfCurrentRecipientID,Int16 wfCurrentRecipientType,DateTime wfStartDateTime,Decimal wfTimeFrame,Int16 wfStatus,string meta1,string meta2,string meta3,string meta4,string meta5,string meta6,string meta7,string meta8,string meta9,string meta10,string meta11,string meta12,string meta13,string meta14,string meta15,string meta16,string meta17,string meta18,string meta19,string meta20,string meta21,string meta22,string meta23,string meta24,string meta25,string meta26,string meta27,string meta28,string meta29,string meta30,Int32 statusId,DateTime submitDate,Int32 DelayTime,Int32 typeId,string serial,DateTime outgoingDate)
{
dp.parameters.Clear();
dp.parameters.Add("@docTypID",docTypID);
dp.parameters.Add("@docName",docName);
dp.parameters.Add("@docExt",docExt);
dp.parameters.Add("@addedDate",addedDate);
dp.parameters.Add("@addedUserID",addedUserID);
dp.parameters.Add("@lastVersion",lastVersion);
dp.parameters.Add("@modifyDate",modifyDate);
dp.parameters.Add("@modifyUserID",modifyUserID);
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@ocrContent",ocrContent);
dp.parameters.Add("@folderSeq",folderSeq);
dp.parameters.Add("@docTypeSeq",docTypeSeq);
dp.parameters.Add("@folderDocTypeSeq",folderDocTypeSeq);
dp.parameters.Add("@wfPathID",wfPathID);
dp.parameters.Add("@wfCurrentSeq",wfCurrentSeq);
dp.parameters.Add("@wfCurrentRecipientID",wfCurrentRecipientID);
dp.parameters.Add("@wfCurrentRecipientType",wfCurrentRecipientType);
dp.parameters.Add("@wfStartDateTime",wfStartDateTime);
dp.parameters.Add("@wfTimeFrame",wfTimeFrame);
dp.parameters.Add("@wfStatus",wfStatus);
dp.parameters.Add("@meta1",meta1);
dp.parameters.Add("@meta2",meta2);
dp.parameters.Add("@meta3",meta3);
dp.parameters.Add("@meta4",meta4);
dp.parameters.Add("@meta5",meta5);
dp.parameters.Add("@meta6",meta6);
dp.parameters.Add("@meta7",meta7);
dp.parameters.Add("@meta8",meta8);
dp.parameters.Add("@meta9",meta9);
dp.parameters.Add("@meta10",meta10);
dp.parameters.Add("@meta11",meta11);
dp.parameters.Add("@meta12",meta12);
dp.parameters.Add("@meta13",meta13);
dp.parameters.Add("@meta14",meta14);
dp.parameters.Add("@meta15",meta15);
dp.parameters.Add("@meta16",meta16);
dp.parameters.Add("@meta17",meta17);
dp.parameters.Add("@meta18",meta18);
dp.parameters.Add("@meta19",meta19);
dp.parameters.Add("@meta20",meta20);
dp.parameters.Add("@meta21",meta21);
dp.parameters.Add("@meta22",meta22);
dp.parameters.Add("@meta23",meta23);
dp.parameters.Add("@meta24",meta24);
dp.parameters.Add("@meta25",meta25);
dp.parameters.Add("@meta26",meta26);
dp.parameters.Add("@meta27",meta27);
dp.parameters.Add("@meta28",meta28);
dp.parameters.Add("@meta29",meta29);
dp.parameters.Add("@meta30",meta30);
dp.parameters.Add("@statusId",statusId);
dp.parameters.Add("@submitDate",submitDate);
dp.parameters.Add("@DelayTime",DelayTime);
dp.parameters.Add("@typeId",typeId);
dp.parameters.Add("@serial",serial);
dp.parameters.Add("@outgoingDate",outgoingDate);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addDocuments"));
return res;
}

public Int32  dboUpdateDocumentsByPrimaryKey(Int64 docID,Int32 docTypID,string docName,string docExt,DateTime addedDate,Int32 addedUserID,Int16 lastVersion,DateTime modifyDate,Int32 modifyUserID,Int32 fldrID,string ocrContent,Int64 folderSeq,Int64 docTypeSeq,Int64 folderDocTypeSeq,Int32 wfPathID,Int16 wfCurrentSeq,Int32 wfCurrentRecipientID,Int16 wfCurrentRecipientType,DateTime wfStartDateTime,Decimal wfTimeFrame,Int16 wfStatus,string meta1,string meta2,string meta3,string meta4,string meta5,string meta6,string meta7,string meta8,string meta9,string meta10,string meta11,string meta12,string meta13,string meta14,string meta15,string meta16,string meta17,string meta18,string meta19,string meta20,string meta21,string meta22,string meta23,string meta24,string meta25,string meta26,string meta27,string meta28,string meta29,string meta30,Int32 statusId,DateTime submitDate,Int32 DelayTime,Int32 typeId,string serial,DateTime outgoingDate)
{
dp.parameters.Clear();
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@docTypID",docTypID);
dp.parameters.Add("@docName",docName);
dp.parameters.Add("@docExt",docExt);
dp.parameters.Add("@addedDate",addedDate);
dp.parameters.Add("@addedUserID",addedUserID);
dp.parameters.Add("@lastVersion",lastVersion);
dp.parameters.Add("@modifyDate",modifyDate);
dp.parameters.Add("@modifyUserID",modifyUserID);
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@ocrContent",ocrContent);
dp.parameters.Add("@folderSeq",folderSeq);
dp.parameters.Add("@docTypeSeq",docTypeSeq);
dp.parameters.Add("@folderDocTypeSeq",folderDocTypeSeq);
dp.parameters.Add("@wfPathID",wfPathID);
dp.parameters.Add("@wfCurrentSeq",wfCurrentSeq);
dp.parameters.Add("@wfCurrentRecipientID",wfCurrentRecipientID);
dp.parameters.Add("@wfCurrentRecipientType",wfCurrentRecipientType);
dp.parameters.Add("@wfStartDateTime",wfStartDateTime);
dp.parameters.Add("@wfTimeFrame",wfTimeFrame);
dp.parameters.Add("@wfStatus",wfStatus);
dp.parameters.Add("@meta1",meta1);
dp.parameters.Add("@meta2",meta2);
dp.parameters.Add("@meta3",meta3);
dp.parameters.Add("@meta4",meta4);
dp.parameters.Add("@meta5",meta5);
dp.parameters.Add("@meta6",meta6);
dp.parameters.Add("@meta7",meta7);
dp.parameters.Add("@meta8",meta8);
dp.parameters.Add("@meta9",meta9);
dp.parameters.Add("@meta10",meta10);
dp.parameters.Add("@meta11",meta11);
dp.parameters.Add("@meta12",meta12);
dp.parameters.Add("@meta13",meta13);
dp.parameters.Add("@meta14",meta14);
dp.parameters.Add("@meta15",meta15);
dp.parameters.Add("@meta16",meta16);
dp.parameters.Add("@meta17",meta17);
dp.parameters.Add("@meta18",meta18);
dp.parameters.Add("@meta19",meta19);
dp.parameters.Add("@meta20",meta20);
dp.parameters.Add("@meta21",meta21);
dp.parameters.Add("@meta22",meta22);
dp.parameters.Add("@meta23",meta23);
dp.parameters.Add("@meta24",meta24);
dp.parameters.Add("@meta25",meta25);
dp.parameters.Add("@meta26",meta26);
dp.parameters.Add("@meta27",meta27);
dp.parameters.Add("@meta28",meta28);
dp.parameters.Add("@meta29",meta29);
dp.parameters.Add("@meta30",meta30);
dp.parameters.Add("@statusId",statusId);
dp.parameters.Add("@submitDate",submitDate);
dp.parameters.Add("@DelayTime",DelayTime);
dp.parameters.Add("@typeId",typeId);
dp.parameters.Add("@serial",serial);
dp.parameters.Add("@outgoingDate",outgoingDate);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocumentsByPrimaryKey");
return res;
}

public Int32  dboUpdateDocuments(Int64 docID,Int32 docTypID,string docName,string docExt,DateTime addedDate,Int32 addedUserID,Int16 lastVersion,DateTime modifyDate,Int32 modifyUserID,Int32 fldrID,string ocrContent,Int64 folderSeq,Int64 docTypeSeq,Int64 folderDocTypeSeq,Int32 wfPathID,Int16 wfCurrentSeq,Int32 wfCurrentRecipientID,Int16 wfCurrentRecipientType,DateTime wfStartDateTime,Decimal wfTimeFrame,Int16 wfStatus,string meta1,string meta2,string meta3,string meta4,string meta5,string meta6,string meta7,string meta8,string meta9,string meta10,string meta11,string meta12,string meta13,string meta14,string meta15,string meta16,string meta17,string meta18,string meta19,string meta20,string meta21,string meta22,string meta23,string meta24,string meta25,string meta26,string meta27,string meta28,string meta29,string meta30,Int32 statusId,DateTime submitDate,Int32 DelayTime,Int32 typeId,string serial,DateTime outgoingDate,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@docTypID",docTypID);
dp.parameters.Add("@docName",docName);
dp.parameters.Add("@docExt",docExt);
dp.parameters.Add("@addedDate",addedDate);
dp.parameters.Add("@addedUserID",addedUserID);
dp.parameters.Add("@lastVersion",lastVersion);
dp.parameters.Add("@modifyDate",modifyDate);
dp.parameters.Add("@modifyUserID",modifyUserID);
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@ocrContent",ocrContent);
dp.parameters.Add("@folderSeq",folderSeq);
dp.parameters.Add("@docTypeSeq",docTypeSeq);
dp.parameters.Add("@folderDocTypeSeq",folderDocTypeSeq);
dp.parameters.Add("@wfPathID",wfPathID);
dp.parameters.Add("@wfCurrentSeq",wfCurrentSeq);
dp.parameters.Add("@wfCurrentRecipientID",wfCurrentRecipientID);
dp.parameters.Add("@wfCurrentRecipientType",wfCurrentRecipientType);
dp.parameters.Add("@wfStartDateTime",wfStartDateTime);
dp.parameters.Add("@wfTimeFrame",wfTimeFrame);
dp.parameters.Add("@wfStatus",wfStatus);
dp.parameters.Add("@meta1",meta1);
dp.parameters.Add("@meta2",meta2);
dp.parameters.Add("@meta3",meta3);
dp.parameters.Add("@meta4",meta4);
dp.parameters.Add("@meta5",meta5);
dp.parameters.Add("@meta6",meta6);
dp.parameters.Add("@meta7",meta7);
dp.parameters.Add("@meta8",meta8);
dp.parameters.Add("@meta9",meta9);
dp.parameters.Add("@meta10",meta10);
dp.parameters.Add("@meta11",meta11);
dp.parameters.Add("@meta12",meta12);
dp.parameters.Add("@meta13",meta13);
dp.parameters.Add("@meta14",meta14);
dp.parameters.Add("@meta15",meta15);
dp.parameters.Add("@meta16",meta16);
dp.parameters.Add("@meta17",meta17);
dp.parameters.Add("@meta18",meta18);
dp.parameters.Add("@meta19",meta19);
dp.parameters.Add("@meta20",meta20);
dp.parameters.Add("@meta21",meta21);
dp.parameters.Add("@meta22",meta22);
dp.parameters.Add("@meta23",meta23);
dp.parameters.Add("@meta24",meta24);
dp.parameters.Add("@meta25",meta25);
dp.parameters.Add("@meta26",meta26);
dp.parameters.Add("@meta27",meta27);
dp.parameters.Add("@meta28",meta28);
dp.parameters.Add("@meta29",meta29);
dp.parameters.Add("@meta30",meta30);
dp.parameters.Add("@statusId",statusId);
dp.parameters.Add("@submitDate",submitDate);
dp.parameters.Add("@DelayTime",DelayTime);
dp.parameters.Add("@typeId",typeId);
dp.parameters.Add("@serial",serial);
dp.parameters.Add("@outgoingDate",outgoingDate);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocuments");
return res;
}

public Int32  dboDeleteDocumentsGroupsByPrimaryKey(Int32 docGroupID)
{
dp.parameters.Clear();
dp.parameters.Add("@docGroupID",docGroupID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocumentsGroupsByPrimaryKey");
return res;
}

public Int32  dboDeleteDocumentsGroups(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocumentsGroups");
return res;
}

public tables.dbo.documentsGroups dboGetDocumentsGroupsByPrimaryKey(Int32 docGroupID)
{
dp.parameters.Clear();
dp.parameters.Add("@docGroupID",docGroupID);
tables.dbo.documentsGroups varTable = new tables.dbo.documentsGroups(dp.excuteQuery("dbo.getDocumentsGroupsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.documentsGroups dboGetAllDocumentsGroups(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.documentsGroups varTable = new tables.dbo.documentsGroups(dp.excuteQuery("dbo.getAllDocumentsGroups").Copy());
return varTable;
}

public Int32 dboAddDocumentsGroups(string docGroupTitle,Int32 docTypeID)
{
dp.parameters.Clear();
dp.parameters.Add("@docGroupTitle",docGroupTitle);
dp.parameters.Add("@docTypeID",docTypeID);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addDocumentsGroups"));
return res;
}

public Int32  dboUpdateDocumentsGroupsByPrimaryKey(Int32 docGroupID,string docGroupTitle,Int32 docTypeID)
{
dp.parameters.Clear();
dp.parameters.Add("@docGroupID",docGroupID);
dp.parameters.Add("@docGroupTitle",docGroupTitle);
dp.parameters.Add("@docTypeID",docTypeID);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocumentsGroupsByPrimaryKey");
return res;
}

public Int32  dboUpdateDocumentsGroups(Int32 docGroupID,string docGroupTitle,Int32 docTypeID,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@docGroupID",docGroupID);
dp.parameters.Add("@docGroupTitle",docGroupTitle);
dp.parameters.Add("@docTypeID",docTypeID);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocumentsGroups");
return res;
}

public Int32  dboDeleteDocumentsStatusByPrimaryKey(Int32 statusId)
{
dp.parameters.Clear();
dp.parameters.Add("@statusId",statusId);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocumentsStatusByPrimaryKey");
return res;
}

public Int32  dboDeleteDocumentsStatus(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocumentsStatus");
return res;
}

public tables.dbo.documentsStatus dboGetDocumentsStatusByPrimaryKey(Int32 statusId)
{
dp.parameters.Clear();
dp.parameters.Add("@statusId",statusId);
tables.dbo.documentsStatus varTable = new tables.dbo.documentsStatus(dp.excuteQuery("dbo.getDocumentsStatusByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.documentsStatus dboGetAllDocumentsStatus(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.documentsStatus varTable = new tables.dbo.documentsStatus(dp.excuteQuery("dbo.getAllDocumentsStatus").Copy());
return varTable;
}

public Int32 dboAddDocumentsStatus(Int32 statusId,string statusName,string statusNameEN,string color)
{
dp.parameters.Clear();
dp.parameters.Add("@statusId",statusId);
dp.parameters.Add("@statusName",statusName);
dp.parameters.Add("@statusNameEN",statusNameEN);
dp.parameters.Add("@color",color);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addDocumentsStatus"));
return res;
}

public Int32  dboUpdateDocumentsStatusByPrimaryKey(Int32 statusId,string statusName,string statusNameEN,string color)
{
dp.parameters.Clear();
dp.parameters.Add("@statusId",statusId);
dp.parameters.Add("@statusName",statusName);
dp.parameters.Add("@statusNameEN",statusNameEN);
dp.parameters.Add("@color",color);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocumentsStatusByPrimaryKey");
return res;
}

public Int32  dboUpdateDocumentsStatus(Int32 statusId,string statusName,string statusNameEN,string color,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@statusId",statusId);
dp.parameters.Add("@statusName",statusName);
dp.parameters.Add("@statusNameEN",statusNameEN);
dp.parameters.Add("@color",color);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocumentsStatus");
return res;
}

public Int32  dboDeleteDocumentVersionsByPrimaryKey(Int64 docID,Int16 version)
{
dp.parameters.Clear();
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@version",version);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocumentVersionsByPrimaryKey");
return res;
}

public Int32  dboDeleteDocumentVersions(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocumentVersions");
return res;
}

public tables.dbo.documentVersions dboGetDocumentVersionsByPrimaryKey(Int64 docID,Int16 version)
{
dp.parameters.Clear();
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@version",version);
tables.dbo.documentVersions varTable = new tables.dbo.documentVersions(dp.excuteQuery("dbo.getDocumentVersionsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.documentVersions dboGetAllDocumentVersions(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.documentVersions varTable = new tables.dbo.documentVersions(dp.excuteQuery("dbo.getAllDocumentVersions").Copy());
return varTable;
}

public Int32 dboAddDocumentVersions(Int64 docID,Int16 version,DateTime addedDate,Int32 addedUserID,string ext,Int32 docGroupID,string FileName,string DocumentFileName)
{
dp.parameters.Clear();
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@version",version);
dp.parameters.Add("@addedDate",addedDate);
dp.parameters.Add("@addedUserID",addedUserID);
dp.parameters.Add("@ext",ext);
dp.parameters.Add("@docGroupID",docGroupID);
dp.parameters.Add("@FileName",FileName);
dp.parameters.Add("@DocumentFileName",DocumentFileName);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addDocumentVersions"));
return res;
}

public Int32  dboUpdateDocumentVersionsByPrimaryKey(Int64 docID,Int16 version,DateTime addedDate,Int32 addedUserID,string ext,Int32 docGroupID,string FileName,string DocumentFileName)
{
dp.parameters.Clear();
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@version",version);
dp.parameters.Add("@addedDate",addedDate);
dp.parameters.Add("@addedUserID",addedUserID);
dp.parameters.Add("@ext",ext);
dp.parameters.Add("@docGroupID",docGroupID);
dp.parameters.Add("@FileName",FileName);
dp.parameters.Add("@DocumentFileName",DocumentFileName);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocumentVersionsByPrimaryKey");
return res;
}

public Int32  dboUpdateDocumentVersions(Int64 docID,Int16 version,DateTime addedDate,Int32 addedUserID,string ext,Int32 docGroupID,string FileName,string DocumentFileName,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@version",version);
dp.parameters.Add("@addedDate",addedDate);
dp.parameters.Add("@addedUserID",addedUserID);
dp.parameters.Add("@ext",ext);
dp.parameters.Add("@docGroupID",docGroupID);
dp.parameters.Add("@FileName",FileName);
dp.parameters.Add("@DocumentFileName",DocumentFileName);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocumentVersions");
return res;
}

public Int32  dboDeleteDocumentWFPathByPrimaryKey(Int32 ID)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocumentWFPathByPrimaryKey");
return res;
}

public Int32  dboDeleteDocumentWFPath(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocumentWFPath");
return res;
}

public tables.dbo.documentWFPath dboGetDocumentWFPathByPrimaryKey(Int32 ID)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
tables.dbo.documentWFPath varTable = new tables.dbo.documentWFPath(dp.excuteQuery("dbo.getDocumentWFPathByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.documentWFPath dboGetAllDocumentWFPath(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.documentWFPath varTable = new tables.dbo.documentWFPath(dp.excuteQuery("dbo.getAllDocumentWFPath").Copy());
return varTable;
}

public Int32 dboAddDocumentWFPath(Int64 docID,Int32 userID,DateTime actionDateTime,Int32 wfPathID,Int16 wfSeqNo,Int16 actionType,Int16 recipientType,string userNotes,DateTime receiveDate,DateTime EndDate,bool isRemoved)
{
dp.parameters.Clear();
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@actionDateTime",actionDateTime);
dp.parameters.Add("@wfPathID",wfPathID);
dp.parameters.Add("@wfSeqNo",wfSeqNo);
dp.parameters.Add("@actionType",actionType);
dp.parameters.Add("@recipientType",recipientType);
dp.parameters.Add("@userNotes",userNotes);
dp.parameters.Add("@receiveDate",receiveDate);
dp.parameters.Add("@EndDate",EndDate);
dp.parameters.Add("@isRemoved",isRemoved);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addDocumentWFPath"));
return res;
}

public Int32  dboUpdateDocumentWFPathByPrimaryKey(Int32 ID,Int64 docID,Int32 userID,DateTime actionDateTime,Int32 wfPathID,Int16 wfSeqNo,Int16 actionType,Int16 recipientType,string userNotes,DateTime receiveDate,DateTime EndDate,bool isRemoved)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@actionDateTime",actionDateTime);
dp.parameters.Add("@wfPathID",wfPathID);
dp.parameters.Add("@wfSeqNo",wfSeqNo);
dp.parameters.Add("@actionType",actionType);
dp.parameters.Add("@recipientType",recipientType);
dp.parameters.Add("@userNotes",userNotes);
dp.parameters.Add("@receiveDate",receiveDate);
dp.parameters.Add("@EndDate",EndDate);
dp.parameters.Add("@isRemoved",isRemoved);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocumentWFPathByPrimaryKey");
return res;
}

public Int32  dboUpdateDocumentWFPath(Int32 ID,Int64 docID,Int32 userID,DateTime actionDateTime,Int32 wfPathID,Int16 wfSeqNo,Int16 actionType,Int16 recipientType,string userNotes,DateTime receiveDate,DateTime EndDate,bool isRemoved,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@ID",ID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@actionDateTime",actionDateTime);
dp.parameters.Add("@wfPathID",wfPathID);
dp.parameters.Add("@wfSeqNo",wfSeqNo);
dp.parameters.Add("@actionType",actionType);
dp.parameters.Add("@recipientType",recipientType);
dp.parameters.Add("@userNotes",userNotes);
dp.parameters.Add("@receiveDate",receiveDate);
dp.parameters.Add("@EndDate",EndDate);
dp.parameters.Add("@isRemoved",isRemoved);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocumentWFPath");
return res;
}

public Int32  dboDeleteDocumentWFPathDelayedByPrimaryKey(Int32 ID)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocumentWFPathDelayedByPrimaryKey");
return res;
}

public Int32  dboDeleteDocumentWFPathDelayed(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteDocumentWFPathDelayed");
return res;
}

public tables.dbo.documentWFPathDelayed dboGetDocumentWFPathDelayedByPrimaryKey(Int32 ID)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
tables.dbo.documentWFPathDelayed varTable = new tables.dbo.documentWFPathDelayed(dp.excuteQuery("dbo.getDocumentWFPathDelayedByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.documentWFPathDelayed dboGetAllDocumentWFPathDelayed(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.documentWFPathDelayed varTable = new tables.dbo.documentWFPathDelayed(dp.excuteQuery("dbo.getAllDocumentWFPathDelayed").Copy());
return varTable;
}

public Int32 dboAddDocumentWFPathDelayed(Int64 docID,Int32 userID,DateTime actionDateTime,Int32 wfPathID,Int16 wfSeqNo,Int16 actionType,Int16 recipientType,string userNotes,DateTime receiveDate,DateTime EndDate,Int32 inboxType,Int32 documentWFPathId)
{
dp.parameters.Clear();
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@actionDateTime",actionDateTime);
dp.parameters.Add("@wfPathID",wfPathID);
dp.parameters.Add("@wfSeqNo",wfSeqNo);
dp.parameters.Add("@actionType",actionType);
dp.parameters.Add("@recipientType",recipientType);
dp.parameters.Add("@userNotes",userNotes);
dp.parameters.Add("@receiveDate",receiveDate);
dp.parameters.Add("@EndDate",EndDate);
dp.parameters.Add("@inboxType",inboxType);
dp.parameters.Add("@documentWFPathId",documentWFPathId);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addDocumentWFPathDelayed"));
return res;
}

public Int32  dboUpdateDocumentWFPathDelayedByPrimaryKey(Int32 ID,Int64 docID,Int32 userID,DateTime actionDateTime,Int32 wfPathID,Int16 wfSeqNo,Int16 actionType,Int16 recipientType,string userNotes,DateTime receiveDate,DateTime EndDate,Int32 inboxType,Int32 documentWFPathId)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@actionDateTime",actionDateTime);
dp.parameters.Add("@wfPathID",wfPathID);
dp.parameters.Add("@wfSeqNo",wfSeqNo);
dp.parameters.Add("@actionType",actionType);
dp.parameters.Add("@recipientType",recipientType);
dp.parameters.Add("@userNotes",userNotes);
dp.parameters.Add("@receiveDate",receiveDate);
dp.parameters.Add("@EndDate",EndDate);
dp.parameters.Add("@inboxType",inboxType);
dp.parameters.Add("@documentWFPathId",documentWFPathId);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocumentWFPathDelayedByPrimaryKey");
return res;
}

public Int32  dboUpdateDocumentWFPathDelayed(Int32 ID,Int64 docID,Int32 userID,DateTime actionDateTime,Int32 wfPathID,Int16 wfSeqNo,Int16 actionType,Int16 recipientType,string userNotes,DateTime receiveDate,DateTime EndDate,Int32 inboxType,Int32 documentWFPathId,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@ID",ID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@actionDateTime",actionDateTime);
dp.parameters.Add("@wfPathID",wfPathID);
dp.parameters.Add("@wfSeqNo",wfSeqNo);
dp.parameters.Add("@actionType",actionType);
dp.parameters.Add("@recipientType",recipientType);
dp.parameters.Add("@userNotes",userNotes);
dp.parameters.Add("@receiveDate",receiveDate);
dp.parameters.Add("@EndDate",EndDate);
dp.parameters.Add("@inboxType",inboxType);
dp.parameters.Add("@documentWFPathId",documentWFPathId);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateDocumentWFPathDelayed");
return res;
}

public Int32  dboDeleteEFormFieldsByPrimaryKey(Int32 fieldSeq,Int32 formID)
{
dp.parameters.Clear();
dp.parameters.Add("@fieldSeq",fieldSeq);
dp.parameters.Add("@formID",formID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteEFormFieldsByPrimaryKey");
return res;
}

public Int32  dboDeleteEFormFields(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteEFormFields");
return res;
}

public tables.dbo.eFormFields dboGetEFormFieldsByPrimaryKey(Int32 fieldSeq,Int32 formID)
{
dp.parameters.Clear();
dp.parameters.Add("@fieldSeq",fieldSeq);
dp.parameters.Add("@formID",formID);
tables.dbo.eFormFields varTable = new tables.dbo.eFormFields(dp.excuteQuery("dbo.getEFormFieldsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.eFormFields dboGetAllEFormFields(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.eFormFields varTable = new tables.dbo.eFormFields(dp.excuteQuery("dbo.getAllEFormFields").Copy());
return varTable;
}

public Int32 dboAddEFormFields(Int32 formID,Int32 fieldSeq,string metaDesc,string metaDataType,bool required,Int32 orderSeq,Int32 ctrlID,string defaultTexts,string defaultValues,bool visible)
{
dp.parameters.Clear();
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@fieldSeq",fieldSeq);
dp.parameters.Add("@metaDesc",metaDesc);
dp.parameters.Add("@metaDataType",metaDataType);
dp.parameters.Add("@required",required);
dp.parameters.Add("@orderSeq",orderSeq);
dp.parameters.Add("@ctrlID",ctrlID);
dp.parameters.Add("@defaultTexts",defaultTexts);
dp.parameters.Add("@defaultValues",defaultValues);
dp.parameters.Add("@visible",visible);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addEFormFields"));
return res;
}

public Int32  dboUpdateEFormFieldsByPrimaryKey(Int32 formID,Int32 fieldSeq,string metaDesc,string metaDataType,bool required,Int32 orderSeq,Int32 ctrlID,string defaultTexts,string defaultValues,bool visible)
{
dp.parameters.Clear();
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@fieldSeq",fieldSeq);
dp.parameters.Add("@metaDesc",metaDesc);
dp.parameters.Add("@metaDataType",metaDataType);
dp.parameters.Add("@required",required);
dp.parameters.Add("@orderSeq",orderSeq);
dp.parameters.Add("@ctrlID",ctrlID);
dp.parameters.Add("@defaultTexts",defaultTexts);
dp.parameters.Add("@defaultValues",defaultValues);
dp.parameters.Add("@visible",visible);
Int32 res;
res = dp.excuteNonQuery("dbo.updateEFormFieldsByPrimaryKey");
return res;
}

public Int32  dboUpdateEFormFields(Int32 formID,Int32 fieldSeq,string metaDesc,string metaDataType,bool required,Int32 orderSeq,Int32 ctrlID,string defaultTexts,string defaultValues,bool visible,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@fieldSeq",fieldSeq);
dp.parameters.Add("@metaDesc",metaDesc);
dp.parameters.Add("@metaDataType",metaDataType);
dp.parameters.Add("@required",required);
dp.parameters.Add("@orderSeq",orderSeq);
dp.parameters.Add("@ctrlID",ctrlID);
dp.parameters.Add("@defaultTexts",defaultTexts);
dp.parameters.Add("@defaultValues",defaultValues);
dp.parameters.Add("@visible",visible);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateEFormFields");
return res;
}

public Int32  dboDeleteEFormsByPrimaryKey(Int32 formID)
{
dp.parameters.Clear();
dp.parameters.Add("@formID",formID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteEFormsByPrimaryKey");
return res;
}

public Int32  dboDeleteEForms(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteEForms");
return res;
}

public tables.dbo.eForms dboGetEFormsByPrimaryKey(Int32 formID)
{
dp.parameters.Clear();
dp.parameters.Add("@formID",formID);
tables.dbo.eForms varTable = new tables.dbo.eForms(dp.excuteQuery("dbo.getEFormsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.eForms dboGetAllEForms(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.eForms varTable = new tables.dbo.eForms(dp.excuteQuery("dbo.getAllEForms").Copy());
return varTable;
}

public Int32 dboAddEForms(string formName,Int32 defaultPathID,bool active,Int32 catID,Int32 catPrgID,string formNameAr)
{
dp.parameters.Clear();
dp.parameters.Add("@formName",formName);
dp.parameters.Add("@defaultPathID",defaultPathID);
dp.parameters.Add("@active",active);
dp.parameters.Add("@catID",catID);
dp.parameters.Add("@catPrgID",catPrgID);
dp.parameters.Add("@formNameAr",formNameAr);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addEForms"));
return res;
}

public Int32  dboUpdateEFormsByPrimaryKey(Int32 formID,string formName,Int32 defaultPathID,bool active,Int32 catID,Int32 catPrgID,string formNameAr)
{
dp.parameters.Clear();
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@formName",formName);
dp.parameters.Add("@defaultPathID",defaultPathID);
dp.parameters.Add("@active",active);
dp.parameters.Add("@catID",catID);
dp.parameters.Add("@catPrgID",catPrgID);
dp.parameters.Add("@formNameAr",formNameAr);
Int32 res;
res = dp.excuteNonQuery("dbo.updateEFormsByPrimaryKey");
return res;
}

public Int32  dboUpdateEForms(Int32 formID,string formName,Int32 defaultPathID,bool active,Int32 catID,Int32 catPrgID,string formNameAr,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@formName",formName);
dp.parameters.Add("@defaultPathID",defaultPathID);
dp.parameters.Add("@active",active);
dp.parameters.Add("@catID",catID);
dp.parameters.Add("@catPrgID",catPrgID);
dp.parameters.Add("@formNameAr",formNameAr);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateEForms");
return res;
}

public Int32  dboDeleteEformsCategoriesByPrimaryKey(Int32 catID)
{
dp.parameters.Clear();
dp.parameters.Add("@catID",catID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteEformsCategoriesByPrimaryKey");
return res;
}

public Int32  dboDeleteEformsCategories(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteEformsCategories");
return res;
}

public tables.dbo.eformsCategories dboGetEformsCategoriesByPrimaryKey(Int32 catID)
{
dp.parameters.Clear();
dp.parameters.Add("@catID",catID);
tables.dbo.eformsCategories varTable = new tables.dbo.eformsCategories(dp.excuteQuery("dbo.getEformsCategoriesByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.eformsCategories dboGetAllEformsCategories(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.eformsCategories varTable = new tables.dbo.eformsCategories(dp.excuteQuery("dbo.getAllEformsCategories").Copy());
return varTable;
}

public Int32 dboAddEformsCategories(string catTitle,Int32 catPrgID)
{
dp.parameters.Clear();
dp.parameters.Add("@catTitle",catTitle);
dp.parameters.Add("@catPrgID",catPrgID);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addEformsCategories"));
return res;
}

public Int32  dboUpdateEformsCategoriesByPrimaryKey(Int32 catID,string catTitle,Int32 catPrgID)
{
dp.parameters.Clear();
dp.parameters.Add("@catID",catID);
dp.parameters.Add("@catTitle",catTitle);
dp.parameters.Add("@catPrgID",catPrgID);
Int32 res;
res = dp.excuteNonQuery("dbo.updateEformsCategoriesByPrimaryKey");
return res;
}

public Int32  dboUpdateEformsCategories(Int32 catID,string catTitle,Int32 catPrgID,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@catID",catID);
dp.parameters.Add("@catTitle",catTitle);
dp.parameters.Add("@catPrgID",catPrgID);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateEformsCategories");
return res;
}

public Int32  dboDeleteEformWFPathByPrimaryKey(Int32 ID)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteEformWFPathByPrimaryKey");
return res;
}

public Int32  dboDeleteEformWFPath(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteEformWFPath");
return res;
}

public tables.dbo.eformWFPath dboGetEformWFPathByPrimaryKey(Int32 ID)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
tables.dbo.eformWFPath varTable = new tables.dbo.eformWFPath(dp.excuteQuery("dbo.getEformWFPathByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.eformWFPath dboGetAllEformWFPath(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.eformWFPath varTable = new tables.dbo.eformWFPath(dp.excuteQuery("dbo.getAllEformWFPath").Copy());
return varTable;
}

public Int32 dboAddEformWFPath(Int32 docID,Int32 userID,Int32 actionUserID,DateTime actionDateTime,Int32 wfPathID,Int16 wfSeqNo,Int16 actionType,Int16 recipientType)
{
dp.parameters.Clear();
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@actionUserID",actionUserID);
dp.parameters.Add("@actionDateTime",actionDateTime);
dp.parameters.Add("@wfPathID",wfPathID);
dp.parameters.Add("@wfSeqNo",wfSeqNo);
dp.parameters.Add("@actionType",actionType);
dp.parameters.Add("@recipientType",recipientType);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addEformWFPath"));
return res;
}

public Int32  dboUpdateEformWFPathByPrimaryKey(Int32 ID,Int32 docID,Int32 userID,Int32 actionUserID,DateTime actionDateTime,Int32 wfPathID,Int16 wfSeqNo,Int16 actionType,Int16 recipientType)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@actionUserID",actionUserID);
dp.parameters.Add("@actionDateTime",actionDateTime);
dp.parameters.Add("@wfPathID",wfPathID);
dp.parameters.Add("@wfSeqNo",wfSeqNo);
dp.parameters.Add("@actionType",actionType);
dp.parameters.Add("@recipientType",recipientType);
Int32 res;
res = dp.excuteNonQuery("dbo.updateEformWFPathByPrimaryKey");
return res;
}

public Int32  dboUpdateEformWFPath(Int32 ID,Int32 docID,Int32 userID,Int32 actionUserID,DateTime actionDateTime,Int32 wfPathID,Int16 wfSeqNo,Int16 actionType,Int16 recipientType,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@ID",ID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@actionUserID",actionUserID);
dp.parameters.Add("@actionDateTime",actionDateTime);
dp.parameters.Add("@wfPathID",wfPathID);
dp.parameters.Add("@wfSeqNo",wfSeqNo);
dp.parameters.Add("@actionType",actionType);
dp.parameters.Add("@recipientType",recipientType);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateEformWFPath");
return res;
}

public Int32  dboDeleteEventByPrimaryKey(Int32 event_id)
{
dp.parameters.Clear();
dp.parameters.Add("@event_id",event_id);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteEventByPrimaryKey");
return res;
}

public Int32  dboDeleteEvent(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteEvent");
return res;
}


public Int32  dboDeleteEventsTypesByPrimaryKey(Int32 eventTypeID)
{
dp.parameters.Clear();
dp.parameters.Add("@eventTypeID",eventTypeID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteEventsTypesByPrimaryKey");
return res;
}

public Int32  dboDeleteEventsTypes(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteEventsTypes");
return res;
}

public tables.dbo.eventsTypes dboGetEventsTypesByPrimaryKey(Int32 eventTypeID)
{
dp.parameters.Clear();
dp.parameters.Add("@eventTypeID",eventTypeID);
tables.dbo.eventsTypes varTable = new tables.dbo.eventsTypes(dp.excuteQuery("dbo.getEventsTypesByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.eventsTypes dboGetAllEventsTypes(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.eventsTypes varTable = new tables.dbo.eventsTypes(dp.excuteQuery("dbo.getAllEventsTypes").Copy());
return varTable;
}

public Int32 dboAddEventsTypes(Int32 eventTypeID,string eventTypeDescA,string eventTypeDescE)
{
dp.parameters.Clear();
dp.parameters.Add("@eventTypeID",eventTypeID);
dp.parameters.Add("@eventTypeDescA",eventTypeDescA);
dp.parameters.Add("@eventTypeDescE",eventTypeDescE);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addEventsTypes"));
return res;
}

public Int32  dboUpdateEventsTypesByPrimaryKey(Int32 eventTypeID,string eventTypeDescA,string eventTypeDescE)
{
dp.parameters.Clear();
dp.parameters.Add("@eventTypeID",eventTypeID);
dp.parameters.Add("@eventTypeDescA",eventTypeDescA);
dp.parameters.Add("@eventTypeDescE",eventTypeDescE);
Int32 res;
res = dp.excuteNonQuery("dbo.updateEventsTypesByPrimaryKey");
return res;
}

public Int32  dboUpdateEventsTypes(Int32 eventTypeID,string eventTypeDescA,string eventTypeDescE,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@eventTypeID",eventTypeID);
dp.parameters.Add("@eventTypeDescA",eventTypeDescA);
dp.parameters.Add("@eventTypeDescE",eventTypeDescE);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateEventsTypes");
return res;
}

public Int32  dboDeleteFoldersByPrimaryKey(Int32 fldrID)
{
dp.parameters.Clear();
dp.parameters.Add("@fldrID",fldrID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteFoldersByPrimaryKey");
return res;
}

public Int32  dboDeleteFolders(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteFolders");
return res;
}

public tables.dbo.folders dboGetFoldersByPrimaryKey(Int32 fldrID)
{
dp.parameters.Clear();
dp.parameters.Add("@fldrID",fldrID);
tables.dbo.folders varTable = new tables.dbo.folders(dp.excuteQuery("dbo.getFoldersByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.folders dboGetAllFolders(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.folders varTable = new tables.dbo.folders(dp.excuteQuery("dbo.getAllFolders").Copy());
return varTable;
}

public Int32 dboAddFolders(string fldrName,Int32 fldrParentID,bool active,Int32 iconID,Int32 defaultDocTypID,Int32 folderOrder,bool isDiwan,string fldrNameAr,bool allowWF,Int32 folderOwner,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@fldrName",fldrName);
dp.parameters.Add("@fldrParentID",fldrParentID);
dp.parameters.Add("@active",active);
dp.parameters.Add("@iconID",iconID);
dp.parameters.Add("@defaultDocTypID",defaultDocTypID);
dp.parameters.Add("@folderOrder",folderOrder);
dp.parameters.Add("@isDiwan",isDiwan);
dp.parameters.Add("@fldrNameAr",fldrNameAr);
dp.parameters.Add("@allowWF",allowWF);
dp.parameters.Add("@folderOwner",folderOwner);
dp.parameters.Add("@ClientId",ClientId);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addFolders"));
return res;
}

public Int32  dboUpdateFoldersByPrimaryKey(Int32 fldrID,string fldrName,Int32 fldrParentID,bool active,Int32 iconID,Int32 defaultDocTypID,Int32 folderOrder,bool isDiwan,string fldrNameAr,bool allowWF,Int32 folderOwner,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@fldrName",fldrName);
dp.parameters.Add("@fldrParentID",fldrParentID);
dp.parameters.Add("@active",active);
dp.parameters.Add("@iconID",iconID);
dp.parameters.Add("@defaultDocTypID",defaultDocTypID);
dp.parameters.Add("@folderOrder",folderOrder);
dp.parameters.Add("@isDiwan",isDiwan);
dp.parameters.Add("@fldrNameAr",fldrNameAr);
dp.parameters.Add("@allowWF",allowWF);
dp.parameters.Add("@folderOwner",folderOwner);
dp.parameters.Add("@ClientId",ClientId);
Int32 res;
res = dp.excuteNonQuery("dbo.updateFoldersByPrimaryKey");
return res;
}

public Int32  dboUpdateFolders(Int32 fldrID,string fldrName,Int32 fldrParentID,bool active,Int32 iconID,Int32 defaultDocTypID,Int32 folderOrder,bool isDiwan,string fldrNameAr,bool allowWF,Int32 folderOwner,Int32 ClientId,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@fldrName",fldrName);
dp.parameters.Add("@fldrParentID",fldrParentID);
dp.parameters.Add("@active",active);
dp.parameters.Add("@iconID",iconID);
dp.parameters.Add("@defaultDocTypID",defaultDocTypID);
dp.parameters.Add("@folderOrder",folderOrder);
dp.parameters.Add("@isDiwan",isDiwan);
dp.parameters.Add("@fldrNameAr",fldrNameAr);
dp.parameters.Add("@allowWF",allowWF);
dp.parameters.Add("@folderOwner",folderOwner);
dp.parameters.Add("@ClientId",ClientId);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateFolders");
return res;
}

public Int32  dboDeleteGroupBlocksByPrimaryKey(Int32 blockNum,Int32 docTypID,Int32 grpID)
{
dp.parameters.Clear();
dp.parameters.Add("@blockNum",blockNum);
dp.parameters.Add("@docTypID",docTypID);
dp.parameters.Add("@grpID",grpID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteGroupBlocksByPrimaryKey");
return res;
}

public Int32  dboDeleteGroupBlocks(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteGroupBlocks");
return res;
}

public tables.dbo.groupBlocks dboGetGroupBlocksByPrimaryKey(Int32 blockNum,Int32 docTypID,Int32 grpID)
{
dp.parameters.Clear();
dp.parameters.Add("@blockNum",blockNum);
dp.parameters.Add("@docTypID",docTypID);
dp.parameters.Add("@grpID",grpID);
tables.dbo.groupBlocks varTable = new tables.dbo.groupBlocks(dp.excuteQuery("dbo.getGroupBlocksByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.groupBlocks dboGetAllGroupBlocks(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.groupBlocks varTable = new tables.dbo.groupBlocks(dp.excuteQuery("dbo.getAllGroupBlocks").Copy());
return varTable;
}

public Int32 dboAddGroupBlocks(Int32 grpID,Int32 docTypID,Int32 blockNum,string blockLeft,string blockTop,string blockWidth,string blockHeight)
{
dp.parameters.Clear();
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@docTypID",docTypID);
dp.parameters.Add("@blockNum",blockNum);
dp.parameters.Add("@blockLeft",blockLeft);
dp.parameters.Add("@blockTop",blockTop);
dp.parameters.Add("@blockWidth",blockWidth);
dp.parameters.Add("@blockHeight",blockHeight);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addGroupBlocks"));
return res;
}

public Int32  dboUpdateGroupBlocksByPrimaryKey(Int32 grpID,Int32 docTypID,Int32 blockNum,string blockLeft,string blockTop,string blockWidth,string blockHeight)
{
dp.parameters.Clear();
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@docTypID",docTypID);
dp.parameters.Add("@blockNum",blockNum);
dp.parameters.Add("@blockLeft",blockLeft);
dp.parameters.Add("@blockTop",blockTop);
dp.parameters.Add("@blockWidth",blockWidth);
dp.parameters.Add("@blockHeight",blockHeight);
Int32 res;
res = dp.excuteNonQuery("dbo.updateGroupBlocksByPrimaryKey");
return res;
}

public Int32  dboUpdateGroupBlocks(Int32 grpID,Int32 docTypID,Int32 blockNum,string blockLeft,string blockTop,string blockWidth,string blockHeight,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@docTypID",docTypID);
dp.parameters.Add("@blockNum",blockNum);
dp.parameters.Add("@blockLeft",blockLeft);
dp.parameters.Add("@blockTop",blockTop);
dp.parameters.Add("@blockWidth",blockWidth);
dp.parameters.Add("@blockHeight",blockHeight);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateGroupBlocks");
return res;
}

public Int32  dboDeleteGroupFoldersByPrimaryKey(Int32 fldrID,Int32 grpID)
{
dp.parameters.Clear();
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@grpID",grpID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteGroupFoldersByPrimaryKey");
return res;
}

public Int32  dboDeleteGroupFolders(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteGroupFolders");
return res;
}

public tables.dbo.groupFolders dboGetGroupFoldersByPrimaryKey(Int32 fldrID,Int32 grpID)
{
dp.parameters.Clear();
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@grpID",grpID);
tables.dbo.groupFolders varTable = new tables.dbo.groupFolders(dp.excuteQuery("dbo.getGroupFoldersByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.groupFolders dboGetAllGroupFolders(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.groupFolders varTable = new tables.dbo.groupFolders(dp.excuteQuery("dbo.getAllGroupFolders").Copy());
return varTable;
}

public Int32 dboAddGroupFolders(Int32 grpID,Int32 fldrID,bool allowInsert,bool allowUpdate,bool allowDelete,bool allowOutgoing,bool allowIncoming,bool inheritSubFolders)
{
dp.parameters.Clear();
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@allowInsert",allowInsert);
dp.parameters.Add("@allowUpdate",allowUpdate);
dp.parameters.Add("@allowDelete",allowDelete);
dp.parameters.Add("@allowOutgoing",allowOutgoing);
dp.parameters.Add("@allowIncoming",allowIncoming);
dp.parameters.Add("@inheritSubFolders",inheritSubFolders);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addGroupFolders"));
return res;
}

public Int32  dboUpdateGroupFoldersByPrimaryKey(Int32 grpID,Int32 fldrID,bool allowInsert,bool allowUpdate,bool allowDelete,bool allowOutgoing,bool allowIncoming,bool inheritSubFolders)
{
dp.parameters.Clear();
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@allowInsert",allowInsert);
dp.parameters.Add("@allowUpdate",allowUpdate);
dp.parameters.Add("@allowDelete",allowDelete);
dp.parameters.Add("@allowOutgoing",allowOutgoing);
dp.parameters.Add("@allowIncoming",allowIncoming);
dp.parameters.Add("@inheritSubFolders",inheritSubFolders);
Int32 res;
res = dp.excuteNonQuery("dbo.updateGroupFoldersByPrimaryKey");
return res;
}

public Int32  dboUpdateGroupFolders(Int32 grpID,Int32 fldrID,bool allowInsert,bool allowUpdate,bool allowDelete,bool allowOutgoing,bool allowIncoming,bool inheritSubFolders,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@allowInsert",allowInsert);
dp.parameters.Add("@allowUpdate",allowUpdate);
dp.parameters.Add("@allowDelete",allowDelete);
dp.parameters.Add("@allowOutgoing",allowOutgoing);
dp.parameters.Add("@allowIncoming",allowIncoming);
dp.parameters.Add("@inheritSubFolders",inheritSubFolders);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateGroupFolders");
return res;
}

public Int32  dboDeleteGroupProgramsByPrimaryKey(Int32 groupID,Int32 programID)
{
dp.parameters.Clear();
dp.parameters.Add("@groupID",groupID);
dp.parameters.Add("@programID",programID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteGroupProgramsByPrimaryKey");
return res;
}

public Int32  dboDeleteGroupPrograms(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteGroupPrograms");
return res;
}

public tables.dbo.groupPrograms dboGetGroupProgramsByPrimaryKey(Int32 groupID,Int32 programID)
{
dp.parameters.Clear();
dp.parameters.Add("@groupID",groupID);
dp.parameters.Add("@programID",programID);
tables.dbo.groupPrograms varTable = new tables.dbo.groupPrograms(dp.excuteQuery("dbo.getGroupProgramsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.groupPrograms dboGetAllGroupPrograms(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.groupPrograms varTable = new tables.dbo.groupPrograms(dp.excuteQuery("dbo.getAllGroupPrograms").Copy());
return varTable;
}

public Int32 dboAddGroupPrograms(Int32 groupID,Int32 programID)
{
dp.parameters.Clear();
dp.parameters.Add("@groupID",groupID);
dp.parameters.Add("@programID",programID);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addGroupPrograms"));
return res;
}

public Int32  dboUpdateGroupProgramsByPrimaryKey(Int32 groupID,Int32 programID)
{
dp.parameters.Clear();
dp.parameters.Add("@groupID",groupID);
dp.parameters.Add("@programID",programID);
Int32 res;
res = dp.excuteNonQuery("dbo.updateGroupProgramsByPrimaryKey");
return res;
}

public Int32  dboUpdateGroupPrograms(Int32 groupID,Int32 programID,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@groupID",groupID);
dp.parameters.Add("@programID",programID);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateGroupPrograms");
return res;
}

public Int32  dboDeleteGroupsByPrimaryKey(Int32 grpID)
{
dp.parameters.Clear();
dp.parameters.Add("@grpID",grpID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteGroupsByPrimaryKey");
return res;
}

public Int32  dboDeleteGroups(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteGroups");
return res;
}

public tables.dbo.groups dboGetGroupsByPrimaryKey(Int32 grpID)
{
dp.parameters.Clear();
dp.parameters.Add("@grpID",grpID);
tables.dbo.groups varTable = new tables.dbo.groups(dp.excuteQuery("dbo.getGroupsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.groups dboGetAllGroups(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.groups varTable = new tables.dbo.groups(dp.excuteQuery("dbo.getAllGroups").Copy());
return varTable;
}

public Int32 dboAddGroups(string grpDesc,Int32 companyID,Int32 branchID,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@grpDesc",grpDesc);
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@ClientId",ClientId);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addGroups"));
return res;
}

public Int32  dboUpdateGroupsByPrimaryKey(Int32 grpID,string grpDesc,Int32 companyID,Int32 branchID,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@grpDesc",grpDesc);
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@ClientId",ClientId);
Int32 res;
res = dp.excuteNonQuery("dbo.updateGroupsByPrimaryKey");
return res;
}

public Int32  dboUpdateGroups(Int32 grpID,string grpDesc,Int32 companyID,Int32 branchID,Int32 ClientId,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@grpDesc",grpDesc);
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@ClientId",ClientId);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateGroups");
return res;
}

public Int32  dboDeleteIconsByPrimaryKey(Int32 iconID)
{
dp.parameters.Clear();
dp.parameters.Add("@iconID",iconID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteIconsByPrimaryKey");
return res;
}

public Int32  dboDeleteIcons(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteIcons");
return res;
}

public tables.dbo.icons dboGetIconsByPrimaryKey(Int32 iconID)
{
dp.parameters.Clear();
dp.parameters.Add("@iconID",iconID);
tables.dbo.icons varTable = new tables.dbo.icons(dp.excuteQuery("dbo.getIconsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.icons dboGetAllIcons(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.icons varTable = new tables.dbo.icons(dp.excuteQuery("dbo.getAllIcons").Copy());
return varTable;
}

public Int32 dboAddIcons(string iconDescription)
{
dp.parameters.Clear();
dp.parameters.Add("@iconDescription",iconDescription);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addIcons"));
return res;
}

public Int32  dboUpdateIconsByPrimaryKey(Int32 iconID,string iconDescription)
{
dp.parameters.Clear();
dp.parameters.Add("@iconID",iconID);
dp.parameters.Add("@iconDescription",iconDescription);
Int32 res;
res = dp.excuteNonQuery("dbo.updateIconsByPrimaryKey");
return res;
}

public Int32  dboUpdateIcons(Int32 iconID,string iconDescription,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@iconID",iconID);
dp.parameters.Add("@iconDescription",iconDescription);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateIcons");
return res;
}

public Int32  dboDeleteIngoingOutgoingSerialsByPrimaryKey(Int32 Id)
{
dp.parameters.Clear();
dp.parameters.Add("@Id",Id);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteIngoingOutgoingSerialsByPrimaryKey");
return res;
}

public Int32  dboDeleteIngoingOutgoingSerials(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteIngoingOutgoingSerials");
return res;
}

public tables.dbo.IngoingOutgoingSerials dboGetIngoingOutgoingSerialsByPrimaryKey(Int32 Id)
{
dp.parameters.Clear();
dp.parameters.Add("@Id",Id);
tables.dbo.IngoingOutgoingSerials varTable = new tables.dbo.IngoingOutgoingSerials(dp.excuteQuery("dbo.getIngoingOutgoingSerialsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.IngoingOutgoingSerials dboGetAllIngoingOutgoingSerials(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.IngoingOutgoingSerials varTable = new tables.dbo.IngoingOutgoingSerials(dp.excuteQuery("dbo.getAllIngoingOutgoingSerials").Copy());
return varTable;
}

public Int32 dboAddIngoingOutgoingSerials(string SerialCode,string Serial,Int32 FolderId,Int32 Type)
{
dp.parameters.Clear();
dp.parameters.Add("@SerialCode",SerialCode);
dp.parameters.Add("@Serial",Serial);
dp.parameters.Add("@FolderId",FolderId);
dp.parameters.Add("@Type",Type);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addIngoingOutgoingSerials"));
return res;
}

public Int32  dboUpdateIngoingOutgoingSerialsByPrimaryKey(Int32 Id,string SerialCode,string Serial,Int32 FolderId,Int32 Type)
{
dp.parameters.Clear();
dp.parameters.Add("@Id",Id);
dp.parameters.Add("@SerialCode",SerialCode);
dp.parameters.Add("@Serial",Serial);
dp.parameters.Add("@FolderId",FolderId);
dp.parameters.Add("@Type",Type);
Int32 res;
res = dp.excuteNonQuery("dbo.updateIngoingOutgoingSerialsByPrimaryKey");
return res;
}

public Int32  dboUpdateIngoingOutgoingSerials(Int32 Id,string SerialCode,string Serial,Int32 FolderId,Int32 Type,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@Id",Id);
dp.parameters.Add("@SerialCode",SerialCode);
dp.parameters.Add("@Serial",Serial);
dp.parameters.Add("@FolderId",FolderId);
dp.parameters.Add("@Type",Type);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateIngoingOutgoingSerials");
return res;
}

public Int32  dboDeleteLanguagesByPrimaryKey(Int32 LanguageId)
{
dp.parameters.Clear();
dp.parameters.Add("@LanguageId",LanguageId);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteLanguagesByPrimaryKey");
return res;
}

public Int32  dboDeleteLanguages(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteLanguages");
return res;
}

public tables.dbo.Languages dboGetLanguagesByPrimaryKey(Int32 LanguageId)
{
dp.parameters.Clear();
dp.parameters.Add("@LanguageId",LanguageId);
tables.dbo.Languages varTable = new tables.dbo.Languages(dp.excuteQuery("dbo.getLanguagesByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.Languages dboGetAllLanguages(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.Languages varTable = new tables.dbo.Languages(dp.excuteQuery("dbo.getAllLanguages").Copy());
return varTable;
}

public Int32 dboAddLanguages(string LanguageName,string LanguageISOCode)
{
dp.parameters.Clear();
dp.parameters.Add("@LanguageName",LanguageName);
dp.parameters.Add("@LanguageISOCode",LanguageISOCode);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addLanguages"));
return res;
}

public Int32  dboUpdateLanguagesByPrimaryKey(Int32 LanguageId,string LanguageName,string LanguageISOCode)
{
dp.parameters.Clear();
dp.parameters.Add("@LanguageId",LanguageId);
dp.parameters.Add("@LanguageName",LanguageName);
dp.parameters.Add("@LanguageISOCode",LanguageISOCode);
Int32 res;
res = dp.excuteNonQuery("dbo.updateLanguagesByPrimaryKey");
return res;
}

public Int32  dboUpdateLanguages(Int32 LanguageId,string LanguageName,string LanguageISOCode,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@LanguageId",LanguageId);
dp.parameters.Add("@LanguageName",LanguageName);
dp.parameters.Add("@LanguageISOCode",LanguageISOCode);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateLanguages");
return res;
}

public Int32  dboDeleteLoginEventsByPrimaryKey(Int32 loginID)
{
dp.parameters.Clear();
dp.parameters.Add("@loginID",loginID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteLoginEventsByPrimaryKey");
return res;
}

public Int32  dboDeleteLoginEvents(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteLoginEvents");
return res;
}

public tables.dbo.loginEvents dboGetLoginEventsByPrimaryKey(Int32 loginID)
{
dp.parameters.Clear();
dp.parameters.Add("@loginID",loginID);
tables.dbo.loginEvents varTable = new tables.dbo.loginEvents(dp.excuteQuery("dbo.getLoginEventsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.loginEvents dboGetAllLoginEvents(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.loginEvents varTable = new tables.dbo.loginEvents(dp.excuteQuery("dbo.getAllLoginEvents").Copy());
return varTable;
}

public Int32 dboAddLoginEvents(Int32 sysEventID,string IPAddress)
{
dp.parameters.Clear();
dp.parameters.Add("@sysEventID",sysEventID);
dp.parameters.Add("@IPAddress",IPAddress);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addLoginEvents"));
return res;
}

public Int32  dboUpdateLoginEventsByPrimaryKey(Int32 loginID,Int32 sysEventID,string IPAddress)
{
dp.parameters.Clear();
dp.parameters.Add("@loginID",loginID);
dp.parameters.Add("@sysEventID",sysEventID);
dp.parameters.Add("@IPAddress",IPAddress);
Int32 res;
res = dp.excuteNonQuery("dbo.updateLoginEventsByPrimaryKey");
return res;
}

public Int32  dboUpdateLoginEvents(Int32 loginID,Int32 sysEventID,string IPAddress,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@loginID",loginID);
dp.parameters.Add("@sysEventID",sysEventID);
dp.parameters.Add("@IPAddress",IPAddress);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateLoginEvents");
return res;
}

public Int32  dboDeleteMetaGroupsPermissionsByPrimaryKey(Int32 grpID,Int32 metaID)
{
dp.parameters.Clear();
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@metaID",metaID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteMetaGroupsPermissionsByPrimaryKey");
return res;
}

public Int32  dboDeleteMetaGroupsPermissions(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteMetaGroupsPermissions");
return res;
}

public tables.dbo.metaGroupsPermissions dboGetMetaGroupsPermissionsByPrimaryKey(Int32 grpID,Int32 metaID)
{
dp.parameters.Clear();
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@metaID",metaID);
tables.dbo.metaGroupsPermissions varTable = new tables.dbo.metaGroupsPermissions(dp.excuteQuery("dbo.getMetaGroupsPermissionsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.metaGroupsPermissions dboGetAllMetaGroupsPermissions(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.metaGroupsPermissions varTable = new tables.dbo.metaGroupsPermissions(dp.excuteQuery("dbo.getAllMetaGroupsPermissions").Copy());
return varTable;
}

public Int32 dboAddMetaGroupsPermissions(Int32 metaID,Int32 grpID,bool allowRead,bool allowEdit)
{
dp.parameters.Clear();
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@allowRead",allowRead);
dp.parameters.Add("@allowEdit",allowEdit);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addMetaGroupsPermissions"));
return res;
}

public Int32  dboUpdateMetaGroupsPermissionsByPrimaryKey(Int32 metaID,Int32 grpID,bool allowRead,bool allowEdit)
{
dp.parameters.Clear();
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@allowRead",allowRead);
dp.parameters.Add("@allowEdit",allowEdit);
Int32 res;
res = dp.excuteNonQuery("dbo.updateMetaGroupsPermissionsByPrimaryKey");
return res;
}

public Int32  dboUpdateMetaGroupsPermissions(Int32 metaID,Int32 grpID,bool allowRead,bool allowEdit,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@allowRead",allowRead);
dp.parameters.Add("@allowEdit",allowEdit);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateMetaGroupsPermissions");
return res;
}

public Int32  dboDeleteMetasByPrimaryKey(Int32 metaID)
{
dp.parameters.Clear();
dp.parameters.Add("@metaID",metaID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteMetasByPrimaryKey");
return res;
}

public Int32  dboDeleteMetas(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteMetas");
return res;
}

public tables.dbo.metas dboGetMetasByPrimaryKey(Int32 metaID)
{
dp.parameters.Clear();
dp.parameters.Add("@metaID",metaID);
tables.dbo.metas varTable = new tables.dbo.metas(dp.excuteQuery("dbo.getMetasByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.metas dboGetAllMetas(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.metas varTable = new tables.dbo.metas(dp.excuteQuery("dbo.getAllMetas").Copy());
return varTable;
}

public Int32 dboAddMetas(Int32 docTypID,string metaDesc,string metaDataType,bool required,Int32 orderSeq,Int32 ctrlID,string defaultTexts,string defaultValues,bool visible,string metaDescAr,Int32 columnSeq,string permissionType,string defaultArTexts,Int32 metaIdFK,Decimal width)
{
dp.parameters.Clear();
dp.parameters.Add("@docTypID",docTypID);
dp.parameters.Add("@metaDesc",metaDesc);
dp.parameters.Add("@metaDataType",metaDataType);
dp.parameters.Add("@required",required);
dp.parameters.Add("@orderSeq",orderSeq);
dp.parameters.Add("@ctrlID",ctrlID);
dp.parameters.Add("@defaultTexts",defaultTexts);
dp.parameters.Add("@defaultValues",defaultValues);
dp.parameters.Add("@visible",visible);
dp.parameters.Add("@metaDescAr",metaDescAr);
dp.parameters.Add("@columnSeq",columnSeq);
dp.parameters.Add("@permissionType",permissionType);
dp.parameters.Add("@defaultArTexts",defaultArTexts);
dp.parameters.Add("@metaIdFK",metaIdFK);
dp.parameters.Add("@width",width);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addMetas"));
return res;
}

public Int32  dboUpdateMetasByPrimaryKey(Int32 metaID,Int32 docTypID,string metaDesc,string metaDataType,bool required,Int32 orderSeq,Int32 ctrlID,string defaultTexts,string defaultValues,bool visible,string metaDescAr,Int32 columnSeq,string permissionType,string defaultArTexts,Int32 metaIdFK,Decimal width)
{
dp.parameters.Clear();
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@docTypID",docTypID);
dp.parameters.Add("@metaDesc",metaDesc);
dp.parameters.Add("@metaDataType",metaDataType);
dp.parameters.Add("@required",required);
dp.parameters.Add("@orderSeq",orderSeq);
dp.parameters.Add("@ctrlID",ctrlID);
dp.parameters.Add("@defaultTexts",defaultTexts);
dp.parameters.Add("@defaultValues",defaultValues);
dp.parameters.Add("@visible",visible);
dp.parameters.Add("@metaDescAr",metaDescAr);
dp.parameters.Add("@columnSeq",columnSeq);
dp.parameters.Add("@permissionType",permissionType);
dp.parameters.Add("@defaultArTexts",defaultArTexts);
dp.parameters.Add("@metaIdFK",metaIdFK);
dp.parameters.Add("@width",width);
Int32 res;
res = dp.excuteNonQuery("dbo.updateMetasByPrimaryKey");
return res;
}

public Int32  dboUpdateMetas(Int32 metaID,Int32 docTypID,string metaDesc,string metaDataType,bool required,Int32 orderSeq,Int32 ctrlID,string defaultTexts,string defaultValues,bool visible,string metaDescAr,Int32 columnSeq,string permissionType,string defaultArTexts,Int32 metaIdFK,Decimal width,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@docTypID",docTypID);
dp.parameters.Add("@metaDesc",metaDesc);
dp.parameters.Add("@metaDataType",metaDataType);
dp.parameters.Add("@required",required);
dp.parameters.Add("@orderSeq",orderSeq);
dp.parameters.Add("@ctrlID",ctrlID);
dp.parameters.Add("@defaultTexts",defaultTexts);
dp.parameters.Add("@defaultValues",defaultValues);
dp.parameters.Add("@visible",visible);
dp.parameters.Add("@metaDescAr",metaDescAr);
dp.parameters.Add("@columnSeq",columnSeq);
dp.parameters.Add("@permissionType",permissionType);
dp.parameters.Add("@defaultArTexts",defaultArTexts);
dp.parameters.Add("@metaIdFK",metaIdFK);
dp.parameters.Add("@width",width);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateMetas");
return res;
}

public Int32  dboDeleteMetaUsersPermissionsByPrimaryKey(Int32 metaID,Int32 userID)
{
dp.parameters.Clear();
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@userID",userID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteMetaUsersPermissionsByPrimaryKey");
return res;
}

public Int32  dboDeleteMetaUsersPermissions(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteMetaUsersPermissions");
return res;
}

public tables.dbo.metaUsersPermissions dboGetMetaUsersPermissionsByPrimaryKey(Int32 metaID,Int32 userID)
{
dp.parameters.Clear();
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@userID",userID);
tables.dbo.metaUsersPermissions varTable = new tables.dbo.metaUsersPermissions(dp.excuteQuery("dbo.getMetaUsersPermissionsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.metaUsersPermissions dboGetAllMetaUsersPermissions(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.metaUsersPermissions varTable = new tables.dbo.metaUsersPermissions(dp.excuteQuery("dbo.getAllMetaUsersPermissions").Copy());
return varTable;
}

public Int32 dboAddMetaUsersPermissions(Int32 metaID,Int32 userID,bool allowRead,bool allowEdit)
{
dp.parameters.Clear();
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@allowRead",allowRead);
dp.parameters.Add("@allowEdit",allowEdit);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addMetaUsersPermissions"));
return res;
}

public Int32  dboUpdateMetaUsersPermissionsByPrimaryKey(Int32 metaID,Int32 userID,bool allowRead,bool allowEdit)
{
dp.parameters.Clear();
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@allowRead",allowRead);
dp.parameters.Add("@allowEdit",allowEdit);
Int32 res;
res = dp.excuteNonQuery("dbo.updateMetaUsersPermissionsByPrimaryKey");
return res;
}

public Int32  dboUpdateMetaUsersPermissions(Int32 metaID,Int32 userID,bool allowRead,bool allowEdit,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@allowRead",allowRead);
dp.parameters.Add("@allowEdit",allowEdit);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateMetaUsersPermissions");
return res;
}

public Int32  dboDeletePositionsByPrimaryKey(Int32 positionID)
{
dp.parameters.Clear();
dp.parameters.Add("@positionID",positionID);
Int32 res;
res = dp.excuteNonQuery("dbo.deletePositionsByPrimaryKey");
return res;
}

public Int32  dboDeletePositions(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deletePositions");
return res;
}

public tables.dbo.positions dboGetPositionsByPrimaryKey(Int32 positionID)
{
dp.parameters.Clear();
dp.parameters.Add("@positionID",positionID);
tables.dbo.positions varTable = new tables.dbo.positions(dp.excuteQuery("dbo.getPositionsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.positions dboGetAllPositions(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.positions varTable = new tables.dbo.positions(dp.excuteQuery("dbo.getAllPositions").Copy());
return varTable;
}

public Int32 dboAddPositions(string positionTitle,string positionTitleAr,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@positionTitle",positionTitle);
dp.parameters.Add("@positionTitleAr",positionTitleAr);
dp.parameters.Add("@ClientId",ClientId);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addPositions"));
return res;
}

public Int32  dboUpdatePositionsByPrimaryKey(Int32 positionID,string positionTitle,string positionTitleAr,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@positionID",positionID);
dp.parameters.Add("@positionTitle",positionTitle);
dp.parameters.Add("@positionTitleAr",positionTitleAr);
dp.parameters.Add("@ClientId",ClientId);
Int32 res;
res = dp.excuteNonQuery("dbo.updatePositionsByPrimaryKey");
return res;
}

public Int32  dboUpdatePositions(Int32 positionID,string positionTitle,string positionTitleAr,Int32 ClientId,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@positionID",positionID);
dp.parameters.Add("@positionTitle",positionTitle);
dp.parameters.Add("@positionTitleAr",positionTitleAr);
dp.parameters.Add("@ClientId",ClientId);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updatePositions");
return res;
}

public Int32  dboDeleteProgramsByPrimaryKey(Int32 programID)
{
dp.parameters.Clear();
dp.parameters.Add("@programID",programID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteProgramsByPrimaryKey");
return res;
}

public Int32  dboDeletePrograms(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deletePrograms");
return res;
}

public tables.dbo.programs dboGetProgramsByPrimaryKey(Int32 programID)
{
dp.parameters.Clear();
dp.parameters.Add("@programID",programID);
tables.dbo.programs varTable = new tables.dbo.programs(dp.excuteQuery("dbo.getProgramsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.programs dboGetAllPrograms(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.programs varTable = new tables.dbo.programs(dp.excuteQuery("dbo.getAllPrograms").Copy());
return varTable;
}

public Int32 dboAddPrograms(string programName,Int32 parentProgramID,string programURL,Int32 windowWidth,Int32 windowHeight,string programNameAr,string iconCss,Int32 orderNum,string svg,bool IsShowOnMobile)
{
dp.parameters.Clear();
dp.parameters.Add("@programName",programName);
dp.parameters.Add("@parentProgramID",parentProgramID);
dp.parameters.Add("@programURL",programURL);
dp.parameters.Add("@windowWidth",windowWidth);
dp.parameters.Add("@windowHeight",windowHeight);
dp.parameters.Add("@programNameAr",programNameAr);
dp.parameters.Add("@iconCss",iconCss);
dp.parameters.Add("@orderNum",orderNum);
dp.parameters.Add("@svg",svg);
dp.parameters.Add("@IsShowOnMobile",IsShowOnMobile);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addPrograms"));
return res;
}

public Int32  dboUpdateProgramsByPrimaryKey(Int32 programID,string programName,Int32 parentProgramID,string programURL,Int32 windowWidth,Int32 windowHeight,string programNameAr,string iconCss,Int32 orderNum,string svg,bool IsShowOnMobile)
{
dp.parameters.Clear();
dp.parameters.Add("@programID",programID);
dp.parameters.Add("@programName",programName);
dp.parameters.Add("@parentProgramID",parentProgramID);
dp.parameters.Add("@programURL",programURL);
dp.parameters.Add("@windowWidth",windowWidth);
dp.parameters.Add("@windowHeight",windowHeight);
dp.parameters.Add("@programNameAr",programNameAr);
dp.parameters.Add("@iconCss",iconCss);
dp.parameters.Add("@orderNum",orderNum);
dp.parameters.Add("@svg",svg);
dp.parameters.Add("@IsShowOnMobile",IsShowOnMobile);
Int32 res;
res = dp.excuteNonQuery("dbo.updateProgramsByPrimaryKey");
return res;
}

public Int32  dboUpdatePrograms(Int32 programID,string programName,Int32 parentProgramID,string programURL,Int32 windowWidth,Int32 windowHeight,string programNameAr,string iconCss,Int32 orderNum,string svg,bool IsShowOnMobile,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@programID",programID);
dp.parameters.Add("@programName",programName);
dp.parameters.Add("@parentProgramID",parentProgramID);
dp.parameters.Add("@programURL",programURL);
dp.parameters.Add("@windowWidth",windowWidth);
dp.parameters.Add("@windowHeight",windowHeight);
dp.parameters.Add("@programNameAr",programNameAr);
dp.parameters.Add("@iconCss",iconCss);
dp.parameters.Add("@orderNum",orderNum);
dp.parameters.Add("@svg",svg);
dp.parameters.Add("@IsShowOnMobile",IsShowOnMobile);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updatePrograms");
return res;
}

public Int32  dboDeleteSettingsByPrimaryKey(Int32 ID)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteSettingsByPrimaryKey");
return res;
}

public Int32  dboDeleteSettings(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteSettings");
return res;
}

public tables.dbo.settings dboGetSettingsByPrimaryKey(Int32 ID)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
tables.dbo.settings varTable = new tables.dbo.settings(dp.excuteQuery("dbo.getSettingsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.settings dboGetAllSettings(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.settings varTable = new tables.dbo.settings(dp.excuteQuery("dbo.getAllSettings").Copy());
return varTable;
}

public Int32 dboAddSettings(Int32 ID,string allowedUsersCount,string systemActive,string systemActiveDate,Int16 passwordStrength,bool passwordAllowStartSpace,Int16 passwordLength,bool allowUsernamePasswordMatch,bool firstLoginChangePassword,Int32 passwordAgeDays,Int32 sessionTimeoutMinutes,Int16 lockTimeOut,string outgoingMailServer,string workflowEmail,string workflowEmailPassword,string systemEmail,string systemEmailPassword,string workflowEmailSubject,string workflowEmailBody,string systemEmailSignature,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
dp.parameters.Add("@allowedUsersCount",allowedUsersCount);
dp.parameters.Add("@systemActive",systemActive);
dp.parameters.Add("@systemActiveDate",systemActiveDate);
dp.parameters.Add("@passwordStrength",passwordStrength);
dp.parameters.Add("@passwordAllowStartSpace",passwordAllowStartSpace);
dp.parameters.Add("@passwordLength",passwordLength);
dp.parameters.Add("@allowUsernamePasswordMatch",allowUsernamePasswordMatch);
dp.parameters.Add("@firstLoginChangePassword",firstLoginChangePassword);
dp.parameters.Add("@passwordAgeDays",passwordAgeDays);
dp.parameters.Add("@sessionTimeoutMinutes",sessionTimeoutMinutes);
dp.parameters.Add("@lockTimeOut",lockTimeOut);
dp.parameters.Add("@outgoingMailServer",outgoingMailServer);
dp.parameters.Add("@workflowEmail",workflowEmail);
dp.parameters.Add("@workflowEmailPassword",workflowEmailPassword);
dp.parameters.Add("@systemEmail",systemEmail);
dp.parameters.Add("@systemEmailPassword",systemEmailPassword);
dp.parameters.Add("@workflowEmailSubject",workflowEmailSubject);
dp.parameters.Add("@workflowEmailBody",workflowEmailBody);
dp.parameters.Add("@systemEmailSignature",systemEmailSignature);
dp.parameters.Add("@ClientId",ClientId);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addSettings"));
return res;
}

public Int32  dboUpdateSettingsByPrimaryKey(Int32 ID,string allowedUsersCount,string systemActive,string systemActiveDate,Int16 passwordStrength,bool passwordAllowStartSpace,Int16 passwordLength,bool allowUsernamePasswordMatch,bool firstLoginChangePassword,Int32 passwordAgeDays,Int32 sessionTimeoutMinutes,Int16 lockTimeOut,string outgoingMailServer,string workflowEmail,string workflowEmailPassword,string systemEmail,string systemEmailPassword,string workflowEmailSubject,string workflowEmailBody,string systemEmailSignature,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
dp.parameters.Add("@allowedUsersCount",allowedUsersCount);
dp.parameters.Add("@systemActive",systemActive);
dp.parameters.Add("@systemActiveDate",systemActiveDate);
dp.parameters.Add("@passwordStrength",passwordStrength);
dp.parameters.Add("@passwordAllowStartSpace",passwordAllowStartSpace);
dp.parameters.Add("@passwordLength",passwordLength);
dp.parameters.Add("@allowUsernamePasswordMatch",allowUsernamePasswordMatch);
dp.parameters.Add("@firstLoginChangePassword",firstLoginChangePassword);
dp.parameters.Add("@passwordAgeDays",passwordAgeDays);
dp.parameters.Add("@sessionTimeoutMinutes",sessionTimeoutMinutes);
dp.parameters.Add("@lockTimeOut",lockTimeOut);
dp.parameters.Add("@outgoingMailServer",outgoingMailServer);
dp.parameters.Add("@workflowEmail",workflowEmail);
dp.parameters.Add("@workflowEmailPassword",workflowEmailPassword);
dp.parameters.Add("@systemEmail",systemEmail);
dp.parameters.Add("@systemEmailPassword",systemEmailPassword);
dp.parameters.Add("@workflowEmailSubject",workflowEmailSubject);
dp.parameters.Add("@workflowEmailBody",workflowEmailBody);
dp.parameters.Add("@systemEmailSignature",systemEmailSignature);
dp.parameters.Add("@ClientId",ClientId);
Int32 res;
res = dp.excuteNonQuery("dbo.updateSettingsByPrimaryKey");
return res;
}

public Int32  dboUpdateSettings(Int32 ID,string allowedUsersCount,string systemActive,string systemActiveDate,Int16 passwordStrength,bool passwordAllowStartSpace,Int16 passwordLength,bool allowUsernamePasswordMatch,bool firstLoginChangePassword,Int32 passwordAgeDays,Int32 sessionTimeoutMinutes,Int16 lockTimeOut,string outgoingMailServer,string workflowEmail,string workflowEmailPassword,string systemEmail,string systemEmailPassword,string workflowEmailSubject,string workflowEmailBody,string systemEmailSignature,Int32 ClientId,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@ID",ID);
dp.parameters.Add("@allowedUsersCount",allowedUsersCount);
dp.parameters.Add("@systemActive",systemActive);
dp.parameters.Add("@systemActiveDate",systemActiveDate);
dp.parameters.Add("@passwordStrength",passwordStrength);
dp.parameters.Add("@passwordAllowStartSpace",passwordAllowStartSpace);
dp.parameters.Add("@passwordLength",passwordLength);
dp.parameters.Add("@allowUsernamePasswordMatch",allowUsernamePasswordMatch);
dp.parameters.Add("@firstLoginChangePassword",firstLoginChangePassword);
dp.parameters.Add("@passwordAgeDays",passwordAgeDays);
dp.parameters.Add("@sessionTimeoutMinutes",sessionTimeoutMinutes);
dp.parameters.Add("@lockTimeOut",lockTimeOut);
dp.parameters.Add("@outgoingMailServer",outgoingMailServer);
dp.parameters.Add("@workflowEmail",workflowEmail);
dp.parameters.Add("@workflowEmailPassword",workflowEmailPassword);
dp.parameters.Add("@systemEmail",systemEmail);
dp.parameters.Add("@systemEmailPassword",systemEmailPassword);
dp.parameters.Add("@workflowEmailSubject",workflowEmailSubject);
dp.parameters.Add("@workflowEmailBody",workflowEmailBody);
dp.parameters.Add("@systemEmailSignature",systemEmailSignature);
dp.parameters.Add("@ClientId",ClientId);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateSettings");
return res;
}

public Int32  dboDeleteSignatureTBByPrimaryKey(Int32 Id)
{
dp.parameters.Clear();
dp.parameters.Add("@Id",Id);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteSignatureTBByPrimaryKey");
return res;
}

public Int32  dboDeleteSignatureTB(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteSignatureTB");
return res;
}

public Int32  dboDeleteSysEventsByPrimaryKey(Int32 sysEventID)
{
dp.parameters.Clear();
dp.parameters.Add("@sysEventID",sysEventID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteSysEventsByPrimaryKey");
return res;
}

public Int32  dboDeleteSysEvents(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteSysEvents");
return res;
}

public tables.dbo.sysEvents dboGetSysEventsByPrimaryKey(Int32 sysEventID)
{
dp.parameters.Clear();
dp.parameters.Add("@sysEventID",sysEventID);
tables.dbo.sysEvents varTable = new tables.dbo.sysEvents(dp.excuteQuery("dbo.getSysEventsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.sysEvents dboGetAllSysEvents(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.sysEvents varTable = new tables.dbo.sysEvents(dp.excuteQuery("dbo.getAllSysEvents").Copy());
return varTable;
}

public Int32 dboAddSysEvents(Int32 userID,Int32 eventTypeID,DateTime eventDateTime,string URL)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@eventTypeID",eventTypeID);
dp.parameters.Add("@eventDateTime",eventDateTime);
dp.parameters.Add("@URL",URL);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addSysEvents"));
return res;
}

public Int32  dboUpdateSysEventsByPrimaryKey(Int32 sysEventID,Int32 userID,Int32 eventTypeID,DateTime eventDateTime,string URL)
{
dp.parameters.Clear();
dp.parameters.Add("@sysEventID",sysEventID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@eventTypeID",eventTypeID);
dp.parameters.Add("@eventDateTime",eventDateTime);
dp.parameters.Add("@URL",URL);
Int32 res;
res = dp.excuteNonQuery("dbo.updateSysEventsByPrimaryKey");
return res;
}

public Int32  dboUpdateSysEvents(Int32 sysEventID,Int32 userID,Int32 eventTypeID,DateTime eventDateTime,string URL,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@sysEventID",sysEventID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@eventTypeID",eventTypeID);
dp.parameters.Add("@eventDateTime",eventDateTime);
dp.parameters.Add("@URL",URL);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateSysEvents");
return res;
}

public Int32  dboDeleteSysSettingsByPrimaryKey(Int16 ID)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteSysSettingsByPrimaryKey");
return res;
}

public Int32  dboDeleteSysSettings(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteSysSettings");
return res;
}

public tables.dbo.sysSettings dboGetSysSettingsByPrimaryKey(Int16 ID)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
tables.dbo.sysSettings varTable = new tables.dbo.sysSettings(dp.excuteQuery("dbo.getSysSettingsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.sysSettings dboGetAllSysSettings(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.sysSettings varTable = new tables.dbo.sysSettings(dp.excuteQuery("dbo.getAllSysSettings").Copy());
return varTable;
}

public Int32 dboAddSysSettings(Int16 ID,string setting,string value,string description,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
dp.parameters.Add("@setting",setting);
dp.parameters.Add("@value",value);
dp.parameters.Add("@description",description);
dp.parameters.Add("@ClientId",ClientId);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addSysSettings"));
return res;
}

public Int32  dboUpdateSysSettingsByPrimaryKey(Int16 ID,string setting,string value,string description,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@ID",ID);
dp.parameters.Add("@setting",setting);
dp.parameters.Add("@value",value);
dp.parameters.Add("@description",description);
dp.parameters.Add("@ClientId",ClientId);
Int32 res;
res = dp.excuteNonQuery("dbo.updateSysSettingsByPrimaryKey");
return res;
}

public Int32  dboUpdateSysSettings(Int16 ID,string setting,string value,string description,Int32 ClientId,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@ID",ID);
dp.parameters.Add("@setting",setting);
dp.parameters.Add("@value",value);
dp.parameters.Add("@description",description);
dp.parameters.Add("@ClientId",ClientId);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateSysSettings");
return res;
}

public Int32  dboDeleteTaskTypesByPrimaryKey(Int32 Id)
{
dp.parameters.Clear();
dp.parameters.Add("@Id",Id);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteTaskTypesByPrimaryKey");
return res;
}

public Int32  dboDeleteTaskTypes(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteTaskTypes");
return res;
}

public tables.dbo.TaskTypes dboGetTaskTypesByPrimaryKey(Int32 Id)
{
dp.parameters.Clear();
dp.parameters.Add("@Id",Id);
tables.dbo.TaskTypes varTable = new tables.dbo.TaskTypes(dp.excuteQuery("dbo.getTaskTypesByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.TaskTypes dboGetAllTaskTypes(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.TaskTypes varTable = new tables.dbo.TaskTypes(dp.excuteQuery("dbo.getAllTaskTypes").Copy());
return varTable;
}

public Int32 dboAddTaskTypes(string ArText,string EnText,string Code)
{
dp.parameters.Clear();
dp.parameters.Add("@ArText",ArText);
dp.parameters.Add("@EnText",EnText);
dp.parameters.Add("@Code",Code);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addTaskTypes"));
return res;
}

public Int32  dboUpdateTaskTypesByPrimaryKey(Int32 Id,string ArText,string EnText,string Code)
{
dp.parameters.Clear();
dp.parameters.Add("@Id",Id);
dp.parameters.Add("@ArText",ArText);
dp.parameters.Add("@EnText",EnText);
dp.parameters.Add("@Code",Code);
Int32 res;
res = dp.excuteNonQuery("dbo.updateTaskTypesByPrimaryKey");
return res;
}

public Int32  dboUpdateTaskTypes(Int32 Id,string ArText,string EnText,string Code,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@Id",Id);
dp.parameters.Add("@ArText",ArText);
dp.parameters.Add("@EnText",EnText);
dp.parameters.Add("@Code",Code);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateTaskTypes");
return res;
}

public Int32  dboDeleteToDoListByPrimaryKey(Int32 Id)
{
dp.parameters.Clear();
dp.parameters.Add("@Id",Id);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteToDoListByPrimaryKey");
return res;
}

public Int32  dboDeleteToDoList(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteToDoList");
return res;
}


public Int32 dboAddToDoList(string TaskName,DateTime TaskDate,Int32 AssignTo,Int32 CreatedBy,Int32 TaskType,DateTime CreatedOn,bool IsComplete,bool IsDeleted,string Description,Int64 DocumentId,Int32 lastModifiedByUserId,DateTime lastModifiedDateTime,DateTime CompleteDate,string RepeatType,string RepeatWeekDays)
{
dp.parameters.Clear();
dp.parameters.Add("@TaskName",TaskName);
dp.parameters.Add("@TaskDate",TaskDate);
dp.parameters.Add("@AssignTo",AssignTo);
dp.parameters.Add("@CreatedBy",CreatedBy);
dp.parameters.Add("@TaskType",TaskType);
dp.parameters.Add("@CreatedOn",CreatedOn);
dp.parameters.Add("@IsComplete",IsComplete);
dp.parameters.Add("@IsDeleted",IsDeleted);
dp.parameters.Add("@Description",Description);
dp.parameters.Add("@DocumentId",DocumentId);
dp.parameters.Add("@lastModifiedByUserId",lastModifiedByUserId);
dp.parameters.Add("@lastModifiedDateTime",lastModifiedDateTime);
dp.parameters.Add("@CompleteDate",CompleteDate);
dp.parameters.Add("@RepeatType",RepeatType);
dp.parameters.Add("@RepeatWeekDays",RepeatWeekDays);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addToDoList"));
return res;
}

public Int32  dboDeleteToDoListCommentsByPrimaryKey(Int32 Id)
{
dp.parameters.Clear();
dp.parameters.Add("@Id",Id);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteToDoListCommentsByPrimaryKey");
return res;
}

public Int32  dboDeleteToDoListComments(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteToDoListComments");
return res;
}

public tables.dbo.ToDoListComments dboGetToDoListCommentsByPrimaryKey(Int32 Id)
{
dp.parameters.Clear();
dp.parameters.Add("@Id",Id);
tables.dbo.ToDoListComments varTable = new tables.dbo.ToDoListComments(dp.excuteQuery("dbo.getToDoListCommentsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.ToDoListComments dboGetAllToDoListComments(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.ToDoListComments varTable = new tables.dbo.ToDoListComments(dp.excuteQuery("dbo.getAllToDoListComments").Copy());
return varTable;
}

public Int32 dboAddToDoListComments(Int32 ToDoListId,string CommentText,Int32 CreatedBy,DateTime CreatedOn,bool IsDeleted)
{
dp.parameters.Clear();
dp.parameters.Add("@ToDoListId",ToDoListId);
dp.parameters.Add("@CommentText",CommentText);
dp.parameters.Add("@CreatedBy",CreatedBy);
dp.parameters.Add("@CreatedOn",CreatedOn);
dp.parameters.Add("@IsDeleted",IsDeleted);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addToDoListComments"));
return res;
}

public Int32  dboUpdateToDoListCommentsByPrimaryKey(Int32 Id,Int32 ToDoListId,string CommentText,Int32 CreatedBy,DateTime CreatedOn,bool IsDeleted)
{
dp.parameters.Clear();
dp.parameters.Add("@Id",Id);
dp.parameters.Add("@ToDoListId",ToDoListId);
dp.parameters.Add("@CommentText",CommentText);
dp.parameters.Add("@CreatedBy",CreatedBy);
dp.parameters.Add("@CreatedOn",CreatedOn);
dp.parameters.Add("@IsDeleted",IsDeleted);
Int32 res;
res = dp.excuteNonQuery("dbo.updateToDoListCommentsByPrimaryKey");
return res;
}

public Int32  dboUpdateToDoListComments(Int32 Id,Int32 ToDoListId,string CommentText,Int32 CreatedBy,DateTime CreatedOn,bool IsDeleted,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@Id",Id);
dp.parameters.Add("@ToDoListId",ToDoListId);
dp.parameters.Add("@CommentText",CommentText);
dp.parameters.Add("@CreatedBy",CreatedBy);
dp.parameters.Add("@CreatedOn",CreatedOn);
dp.parameters.Add("@IsDeleted",IsDeleted);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateToDoListComments");
return res;
}

public Int32  dboDeleteTypeByPrimaryKey(Int32 typeId)
{
dp.parameters.Clear();
dp.parameters.Add("@typeId",typeId);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteTypeByPrimaryKey");
return res;
}

public Int32  dboDeleteType(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteType");
return res;
}

public tables.dbo.type dboGetTypeByPrimaryKey(Int32 typeId)
{
dp.parameters.Clear();
dp.parameters.Add("@typeId",typeId);
tables.dbo.type varTable = new tables.dbo.type(dp.excuteQuery("dbo.getTypeByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.type dboGetAllType(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.type varTable = new tables.dbo.type(dp.excuteQuery("dbo.getAllType").Copy());
return varTable;
}

public Int32 dboAddType(Int32 typeId,string typeNameAr,string typeName)
{
dp.parameters.Clear();
dp.parameters.Add("@typeId",typeId);
dp.parameters.Add("@typeNameAr",typeNameAr);
dp.parameters.Add("@typeName",typeName);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addType"));
return res;
}

public Int32  dboUpdateTypeByPrimaryKey(Int32 typeId,string typeNameAr,string typeName)
{
dp.parameters.Clear();
dp.parameters.Add("@typeId",typeId);
dp.parameters.Add("@typeNameAr",typeNameAr);
dp.parameters.Add("@typeName",typeName);
Int32 res;
res = dp.excuteNonQuery("dbo.updateTypeByPrimaryKey");
return res;
}

public Int32  dboUpdateType(Int32 typeId,string typeNameAr,string typeName,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@typeId",typeId);
dp.parameters.Add("@typeNameAr",typeNameAr);
dp.parameters.Add("@typeName",typeName);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateType");
return res;
}

public Int32  dboDeleteUserDocumentsByPrimaryKey(Int64 docID,Int32 userID)
{
dp.parameters.Clear();
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@userID",userID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteUserDocumentsByPrimaryKey");
return res;
}

public Int32  dboDeleteUserDocuments(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteUserDocuments");
return res;
}

public tables.dbo.userDocuments dboGetUserDocumentsByPrimaryKey(Int64 docID,Int32 userID)
{
dp.parameters.Clear();
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@userID",userID);
tables.dbo.userDocuments varTable = new tables.dbo.userDocuments(dp.excuteQuery("dbo.getUserDocumentsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.userDocuments dboGetAllUserDocuments(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.userDocuments varTable = new tables.dbo.userDocuments(dp.excuteQuery("dbo.getAllUserDocuments").Copy());
return varTable;
}

public Int32 dboAddUserDocuments(Int32 userID,Int64 docID,bool allow,bool allowInsert,bool allowUpdate,bool allowDelete)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@allow",allow);
dp.parameters.Add("@allowInsert",allowInsert);
dp.parameters.Add("@allowUpdate",allowUpdate);
dp.parameters.Add("@allowDelete",allowDelete);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addUserDocuments"));
return res;
}

public Int32  dboUpdateUserDocumentsByPrimaryKey(Int32 userID,Int64 docID,bool allow,bool allowInsert,bool allowUpdate,bool allowDelete)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@allow",allow);
dp.parameters.Add("@allowInsert",allowInsert);
dp.parameters.Add("@allowUpdate",allowUpdate);
dp.parameters.Add("@allowDelete",allowDelete);
Int32 res;
res = dp.excuteNonQuery("dbo.updateUserDocumentsByPrimaryKey");
return res;
}

public Int32  dboUpdateUserDocuments(Int32 userID,Int64 docID,bool allow,bool allowInsert,bool allowUpdate,bool allowDelete,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@allow",allow);
dp.parameters.Add("@allowInsert",allowInsert);
dp.parameters.Add("@allowUpdate",allowUpdate);
dp.parameters.Add("@allowDelete",allowDelete);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateUserDocuments");
return res;
}

public Int32  dboDeleteUserFoldersByPrimaryKey(Int32 fldrID,Int32 userID)
{
dp.parameters.Clear();
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@userID",userID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteUserFoldersByPrimaryKey");
return res;
}

public Int32  dboDeleteUserFolders(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteUserFolders");
return res;
}

public tables.dbo.userFolders dboGetUserFoldersByPrimaryKey(Int32 fldrID,Int32 userID)
{
dp.parameters.Clear();
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@userID",userID);
tables.dbo.userFolders varTable = new tables.dbo.userFolders(dp.excuteQuery("dbo.getUserFoldersByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.userFolders dboGetAllUserFolders(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.userFolders varTable = new tables.dbo.userFolders(dp.excuteQuery("dbo.getAllUserFolders").Copy());
return varTable;
}

public Int32 dboAddUserFolders(Int32 userID,Int32 fldrID,bool allow,bool allowInsert,bool allowUpdate,bool allowDelete,bool allowOutgoing,bool allowIncoming,bool inheritSubFolders)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@allow",allow);
dp.parameters.Add("@allowInsert",allowInsert);
dp.parameters.Add("@allowUpdate",allowUpdate);
dp.parameters.Add("@allowDelete",allowDelete);
dp.parameters.Add("@allowOutgoing",allowOutgoing);
dp.parameters.Add("@allowIncoming",allowIncoming);
dp.parameters.Add("@inheritSubFolders",inheritSubFolders);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addUserFolders"));
return res;
}

public Int32  dboUpdateUserFoldersByPrimaryKey(Int32 userID,Int32 fldrID,bool allow,bool allowInsert,bool allowUpdate,bool allowDelete,bool allowOutgoing,bool allowIncoming,bool inheritSubFolders)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@allow",allow);
dp.parameters.Add("@allowInsert",allowInsert);
dp.parameters.Add("@allowUpdate",allowUpdate);
dp.parameters.Add("@allowDelete",allowDelete);
dp.parameters.Add("@allowOutgoing",allowOutgoing);
dp.parameters.Add("@allowIncoming",allowIncoming);
dp.parameters.Add("@inheritSubFolders",inheritSubFolders);
Int32 res;
res = dp.excuteNonQuery("dbo.updateUserFoldersByPrimaryKey");
return res;
}

public Int32  dboUpdateUserFolders(Int32 userID,Int32 fldrID,bool allow,bool allowInsert,bool allowUpdate,bool allowDelete,bool allowOutgoing,bool allowIncoming,bool inheritSubFolders,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@fldrID",fldrID);
dp.parameters.Add("@allow",allow);
dp.parameters.Add("@allowInsert",allowInsert);
dp.parameters.Add("@allowUpdate",allowUpdate);
dp.parameters.Add("@allowDelete",allowDelete);
dp.parameters.Add("@allowOutgoing",allowOutgoing);
dp.parameters.Add("@allowIncoming",allowIncoming);
dp.parameters.Add("@inheritSubFolders",inheritSubFolders);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateUserFolders");
return res;
}

public Int32  dboDeleteUserFormFieldsByPrimaryKey(Int32 fieldSeq,Int32 formID,Int32 userID)
{
dp.parameters.Clear();
dp.parameters.Add("@fieldSeq",fieldSeq);
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@userID",userID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteUserFormFieldsByPrimaryKey");
return res;
}

public Int32  dboDeleteUserFormFields(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteUserFormFields");
return res;
}

public tables.dbo.userFormFields dboGetUserFormFieldsByPrimaryKey(Int32 fieldSeq,Int32 formID,Int32 userID)
{
dp.parameters.Clear();
dp.parameters.Add("@fieldSeq",fieldSeq);
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@userID",userID);
tables.dbo.userFormFields varTable = new tables.dbo.userFormFields(dp.excuteQuery("dbo.getUserFormFieldsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.userFormFields dboGetAllUserFormFields(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.userFormFields varTable = new tables.dbo.userFormFields(dp.excuteQuery("dbo.getAllUserFormFields").Copy());
return varTable;
}

public Int32 dboAddUserFormFields(Int32 userID,Int32 formID,Int32 fieldSeq,string value)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@fieldSeq",fieldSeq);
dp.parameters.Add("@value",value);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addUserFormFields"));
return res;
}

public Int32  dboUpdateUserFormFieldsByPrimaryKey(Int32 userID,Int32 formID,Int32 fieldSeq,string value)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@fieldSeq",fieldSeq);
dp.parameters.Add("@value",value);
Int32 res;
res = dp.excuteNonQuery("dbo.updateUserFormFieldsByPrimaryKey");
return res;
}

public Int32  dboUpdateUserFormFields(Int32 userID,Int32 formID,Int32 fieldSeq,string value,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@fieldSeq",fieldSeq);
dp.parameters.Add("@value",value);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateUserFormFields");
return res;
}

public Int32  dboDeleteUserProgramsByPrimaryKey(Int32 programID,Int32 userID)
{
dp.parameters.Clear();
dp.parameters.Add("@programID",programID);
dp.parameters.Add("@userID",userID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteUserProgramsByPrimaryKey");
return res;
}

public Int32  dboDeleteUserPrograms(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteUserPrograms");
return res;
}

public tables.dbo.userPrograms dboGetUserProgramsByPrimaryKey(Int32 programID,Int32 userID)
{
dp.parameters.Clear();
dp.parameters.Add("@programID",programID);
dp.parameters.Add("@userID",userID);
tables.dbo.userPrograms varTable = new tables.dbo.userPrograms(dp.excuteQuery("dbo.getUserProgramsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.userPrograms dboGetAllUserPrograms(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.userPrograms varTable = new tables.dbo.userPrograms(dp.excuteQuery("dbo.getAllUserPrograms").Copy());
return varTable;
}

public Int32 dboAddUserPrograms(Int32 userID,Int32 programID)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@programID",programID);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addUserPrograms"));
return res;
}

public Int32  dboUpdateUserProgramsByPrimaryKey(Int32 userID,Int32 programID)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@programID",programID);
Int32 res;
res = dp.excuteNonQuery("dbo.updateUserProgramsByPrimaryKey");
return res;
}

public Int32  dboUpdateUserPrograms(Int32 userID,Int32 programID,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@programID",programID);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateUserPrograms");
return res;
}

public Int32  dboDeleteUsersByPrimaryKey(Int32 userID)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteUsersByPrimaryKey");
return res;
}

public Int32  dboDeleteUsers(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteUsers");
return res;
}

public tables.dbo.users dboGetUsersByPrimaryKey(Int32 userID)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
tables.dbo.users varTable = new tables.dbo.users(dp.excuteQuery("dbo.getUsersByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.users dboGetAllUsers(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.users varTable = new tables.dbo.users(dp.excuteQuery("dbo.getAllUsers").Copy());
return varTable;
}

public Int32 dboAddUsers(string userName,string password,string fullName,Int32 grpID,bool active,Int32 companyID,Int32 branchID,Int32 departmentID,Int32 positionID,string email,bool allowCustomWF,bool allowCreateFolders,bool allowReplaceDocuments,bool allowDiwan,bool isFirstLogin,DateTime passwordCreationDate,DateTime passwordModifiedDate,string lastPassword,string Signature,string Phone,bool isMobileFirstLogin,bool isEmailVerfied,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@userName",userName);
dp.parameters.Add("@password",password);
dp.parameters.Add("@fullName",fullName);
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@active",active);
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@departmentID",departmentID);
dp.parameters.Add("@positionID",positionID);
dp.parameters.Add("@email",email);
dp.parameters.Add("@allowCustomWF",allowCustomWF);
dp.parameters.Add("@allowCreateFolders",allowCreateFolders);
dp.parameters.Add("@allowReplaceDocuments",allowReplaceDocuments);
dp.parameters.Add("@allowDiwan",allowDiwan);
dp.parameters.Add("@isFirstLogin",isFirstLogin);
dp.parameters.Add("@passwordCreationDate",passwordCreationDate);
dp.parameters.Add("@passwordModifiedDate",passwordModifiedDate);
dp.parameters.Add("@lastPassword",lastPassword);
dp.parameters.Add("@Signature",Signature);
dp.parameters.Add("@Phone",Phone);
dp.parameters.Add("@isMobileFirstLogin",isMobileFirstLogin);
dp.parameters.Add("@isEmailVerfied",isEmailVerfied);
dp.parameters.Add("@ClientId",ClientId);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addUsers"));
return res;
}

public Int32  dboUpdateUsersByPrimaryKey(Int32 userID,string userName,string password,string fullName,Int32 grpID,bool active,Int32 companyID,Int32 branchID,Int32 departmentID,Int32 positionID,string email,bool allowCustomWF,bool allowCreateFolders,bool allowReplaceDocuments,bool allowDiwan,bool isFirstLogin,DateTime passwordCreationDate,DateTime passwordModifiedDate,string lastPassword,string Signature,string Phone,bool isMobileFirstLogin,bool isEmailVerfied,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@userName",userName);
dp.parameters.Add("@password",password);
dp.parameters.Add("@fullName",fullName);
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@active",active);
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@departmentID",departmentID);
dp.parameters.Add("@positionID",positionID);
dp.parameters.Add("@email",email);
dp.parameters.Add("@allowCustomWF",allowCustomWF);
dp.parameters.Add("@allowCreateFolders",allowCreateFolders);
dp.parameters.Add("@allowReplaceDocuments",allowReplaceDocuments);
dp.parameters.Add("@allowDiwan",allowDiwan);
dp.parameters.Add("@isFirstLogin",isFirstLogin);
dp.parameters.Add("@passwordCreationDate",passwordCreationDate);
dp.parameters.Add("@passwordModifiedDate",passwordModifiedDate);
dp.parameters.Add("@lastPassword",lastPassword);
dp.parameters.Add("@Signature",Signature);
dp.parameters.Add("@Phone",Phone);
dp.parameters.Add("@isMobileFirstLogin",isMobileFirstLogin);
dp.parameters.Add("@isEmailVerfied",isEmailVerfied);
dp.parameters.Add("@ClientId",ClientId);
Int32 res;
res = dp.excuteNonQuery("dbo.updateUsersByPrimaryKey");
return res;
}

public Int32  dboUpdateUsers(Int32 userID,string userName,string password,string fullName,Int32 grpID,bool active,Int32 companyID,Int32 branchID,Int32 departmentID,Int32 positionID,string email,bool allowCustomWF,bool allowCreateFolders,bool allowReplaceDocuments,bool allowDiwan,bool isFirstLogin,DateTime passwordCreationDate,DateTime passwordModifiedDate,string lastPassword,string Signature,string Phone,bool isMobileFirstLogin,bool isEmailVerfied,Int32 ClientId,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@userName",userName);
dp.parameters.Add("@password",password);
dp.parameters.Add("@fullName",fullName);
dp.parameters.Add("@grpID",grpID);
dp.parameters.Add("@active",active);
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@departmentID",departmentID);
dp.parameters.Add("@positionID",positionID);
dp.parameters.Add("@email",email);
dp.parameters.Add("@allowCustomWF",allowCustomWF);
dp.parameters.Add("@allowCreateFolders",allowCreateFolders);
dp.parameters.Add("@allowReplaceDocuments",allowReplaceDocuments);
dp.parameters.Add("@allowDiwan",allowDiwan);
dp.parameters.Add("@isFirstLogin",isFirstLogin);
dp.parameters.Add("@passwordCreationDate",passwordCreationDate);
dp.parameters.Add("@passwordModifiedDate",passwordModifiedDate);
dp.parameters.Add("@lastPassword",lastPassword);
dp.parameters.Add("@Signature",Signature);
dp.parameters.Add("@Phone",Phone);
dp.parameters.Add("@isMobileFirstLogin",isMobileFirstLogin);
dp.parameters.Add("@isEmailVerfied",isEmailVerfied);
dp.parameters.Add("@ClientId",ClientId);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateUsers");
return res;
}

public Int32  dboDeleteUsersFormsByPrimaryKey(Int32 formID,Int32 userID)
{
dp.parameters.Clear();
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@userID",userID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteUsersFormsByPrimaryKey");
return res;
}

public Int32  dboDeleteUsersForms(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteUsersForms");
return res;
}

public tables.dbo.usersForms dboGetUsersFormsByPrimaryKey(Int32 formID,Int32 userID)
{
dp.parameters.Clear();
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@userID",userID);
tables.dbo.usersForms varTable = new tables.dbo.usersForms(dp.excuteQuery("dbo.getUsersFormsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.usersForms dboGetAllUsersForms(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.usersForms varTable = new tables.dbo.usersForms(dp.excuteQuery("dbo.getAllUsersForms").Copy());
return varTable;
}

public Int32 dboAddUsersForms(Int32 userID,Int32 formID,Int32 pathID,DateTime submitDateTime,Int16 status)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@pathID",pathID);
dp.parameters.Add("@submitDateTime",submitDateTime);
dp.parameters.Add("@status",status);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addUsersForms"));
return res;
}

public Int32  dboUpdateUsersFormsByPrimaryKey(Int32 userID,Int32 formID,Int32 pathID,DateTime submitDateTime,Int16 status)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@pathID",pathID);
dp.parameters.Add("@submitDateTime",submitDateTime);
dp.parameters.Add("@status",status);
Int32 res;
res = dp.excuteNonQuery("dbo.updateUsersFormsByPrimaryKey");
return res;
}

public Int32  dboUpdateUsersForms(Int32 userID,Int32 formID,Int32 pathID,DateTime submitDateTime,Int16 status,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@formID",formID);
dp.parameters.Add("@pathID",pathID);
dp.parameters.Add("@submitDateTime",submitDateTime);
dp.parameters.Add("@status",status);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateUsersForms");
return res;
}

public Int32  dboDeleteUsersRemidersByPrimaryKey(Int64 reminderID)
{
dp.parameters.Clear();
dp.parameters.Add("@reminderID",reminderID);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteUsersRemidersByPrimaryKey");
return res;
}

public Int32  dboDeleteUsersRemiders(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteUsersRemiders");
return res;
}

public tables.dbo.usersRemiders dboGetUsersRemidersByPrimaryKey(Int64 reminderID)
{
dp.parameters.Clear();
dp.parameters.Add("@reminderID",reminderID);
tables.dbo.usersRemiders varTable = new tables.dbo.usersRemiders(dp.excuteQuery("dbo.getUsersRemidersByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.usersRemiders dboGetAllUsersRemiders(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.usersRemiders varTable = new tables.dbo.usersRemiders(dp.excuteQuery("dbo.getAllUsersRemiders").Copy());
return varTable;
}

public Int32 dboAddUsersRemiders(Int32 userID,Int32 metaID,Int64 docID,Int32 beforedays,bool isRemoved)
{
dp.parameters.Clear();
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@beforedays",beforedays);
dp.parameters.Add("@isRemoved",isRemoved);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addUsersRemiders"));
return res;
}

public Int32  dboUpdateUsersRemidersByPrimaryKey(Int64 reminderID,Int32 userID,Int32 metaID,Int64 docID,Int32 beforedays,bool isRemoved)
{
dp.parameters.Clear();
dp.parameters.Add("@reminderID",reminderID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@beforedays",beforedays);
dp.parameters.Add("@isRemoved",isRemoved);
Int32 res;
res = dp.excuteNonQuery("dbo.updateUsersRemidersByPrimaryKey");
return res;
}

public Int32  dboUpdateUsersRemiders(Int64 reminderID,Int32 userID,Int32 metaID,Int64 docID,Int32 beforedays,bool isRemoved,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@reminderID",reminderID);
dp.parameters.Add("@userID",userID);
dp.parameters.Add("@metaID",metaID);
dp.parameters.Add("@docID",docID);
dp.parameters.Add("@beforedays",beforedays);
dp.parameters.Add("@isRemoved",isRemoved);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateUsersRemiders");
return res;
}

public Int32  dboDeleteWfPathDetailsByPrimaryKey(Int32 id)
{
dp.parameters.Clear();
dp.parameters.Add("@id",id);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteWfPathDetailsByPrimaryKey");
return res;
}

public Int32  dboDeleteWfPathDetails(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteWfPathDetails");
return res;
}

public tables.dbo.wfPathDetails dboGetWfPathDetailsByPrimaryKey(Int32 id)
{
dp.parameters.Clear();
dp.parameters.Add("@id",id);
tables.dbo.wfPathDetails varTable = new tables.dbo.wfPathDetails(dp.excuteQuery("dbo.getWfPathDetailsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.wfPathDetails dboGetAllWfPathDetails(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.wfPathDetails varTable = new tables.dbo.wfPathDetails(dp.excuteQuery("dbo.getAllWfPathDetails").Copy());
return varTable;
}

public Int32 dboAddWfPathDetails(Int32 pathID,Int16 seqNo,Int32 recipientID,bool endOfPath,Int16 recipientType,Int32 companyID,Int32 branchID,Int16 approveType,Int32 duration,Int32 durationType)
{
dp.parameters.Clear();
dp.parameters.Add("@pathID",pathID);
dp.parameters.Add("@seqNo",seqNo);
dp.parameters.Add("@recipientID",recipientID);
dp.parameters.Add("@endOfPath",endOfPath);
dp.parameters.Add("@recipientType",recipientType);
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@approveType",approveType);
dp.parameters.Add("@duration",duration);
dp.parameters.Add("@durationType",durationType);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addWfPathDetails"));
return res;
}

public Int32  dboUpdateWfPathDetailsByPrimaryKey(Int32 pathID,Int16 seqNo,Int32 recipientID,bool endOfPath,Int16 recipientType,Int32 companyID,Int32 branchID,Int16 approveType,Int32 id,Int32 duration,Int32 durationType)
{
dp.parameters.Clear();
dp.parameters.Add("@pathID",pathID);
dp.parameters.Add("@seqNo",seqNo);
dp.parameters.Add("@recipientID",recipientID);
dp.parameters.Add("@endOfPath",endOfPath);
dp.parameters.Add("@recipientType",recipientType);
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@approveType",approveType);
dp.parameters.Add("@id",id);
dp.parameters.Add("@duration",duration);
dp.parameters.Add("@durationType",durationType);
Int32 res;
res = dp.excuteNonQuery("dbo.updateWfPathDetailsByPrimaryKey");
return res;
}

public Int32  dboUpdateWfPathDetails(Int32 pathID,Int16 seqNo,Int32 recipientID,bool endOfPath,Int16 recipientType,Int32 companyID,Int32 branchID,Int16 approveType,Int32 id,Int32 duration,Int32 durationType,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@pathID",pathID);
dp.parameters.Add("@seqNo",seqNo);
dp.parameters.Add("@recipientID",recipientID);
dp.parameters.Add("@endOfPath",endOfPath);
dp.parameters.Add("@recipientType",recipientType);
dp.parameters.Add("@companyID",companyID);
dp.parameters.Add("@branchID",branchID);
dp.parameters.Add("@approveType",approveType);
dp.parameters.Add("@id",id);
dp.parameters.Add("@duration",duration);
dp.parameters.Add("@durationType",durationType);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateWfPathDetails");
return res;
}

public Int32  dboDeleteWorkFlowPathsByPrimaryKey(Int32 pathId)
{
dp.parameters.Clear();
dp.parameters.Add("@pathId",pathId);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteWorkFlowPathsByPrimaryKey");
return res;
}

public Int32  dboDeleteWorkFlowPaths(string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@cond",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.deleteWorkFlowPaths");
return res;
}

public tables.dbo.workFlowPaths dboGetWorkFlowPathsByPrimaryKey(Int32 pathId)
{
dp.parameters.Clear();
dp.parameters.Add("@pathId",pathId);
tables.dbo.workFlowPaths varTable = new tables.dbo.workFlowPaths(dp.excuteQuery("dbo.getWorkFlowPathsByPrimaryKey").Copy());
return varTable;
}

public tables.dbo.workFlowPaths dboGetAllWorkFlowPaths(string condition="",string orderBy = "")
{
dp.parameters.Clear();
if(condition.Trim() != ""){
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;}
orderBy = orderBy.Trim();
if(!orderBy.StartsWith("order by") && orderBy != "")
 orderBy = " order by " + orderBy;
condition = condition + orderBy;
dp.parameters.Add("@cond",condition);
tables.dbo.workFlowPaths varTable = new tables.dbo.workFlowPaths(dp.excuteQuery("dbo.getAllWorkFlowPaths").Copy());
return varTable;
}

public Int32 dboAddWorkFlowPaths(string pathDesc,Int32 fldrId,Int32 docTypId,string pathDescAr,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@pathDesc",pathDesc);
dp.parameters.Add("@fldrId",fldrId);
dp.parameters.Add("@docTypId",docTypId);
dp.parameters.Add("@pathDescAr",pathDescAr);
dp.parameters.Add("@ClientId",ClientId);
Int32 res=-1;
res = Convert.ToInt32(dp.executeScalar("dbo.addWorkFlowPaths"));
return res;
}

public Int32  dboUpdateWorkFlowPathsByPrimaryKey(Int32 pathId,string pathDesc,Int32 fldrId,Int32 docTypId,string pathDescAr,Int32 ClientId)
{
dp.parameters.Clear();
dp.parameters.Add("@pathId",pathId);
dp.parameters.Add("@pathDesc",pathDesc);
dp.parameters.Add("@fldrId",fldrId);
dp.parameters.Add("@docTypId",docTypId);
dp.parameters.Add("@pathDescAr",pathDescAr);
dp.parameters.Add("@ClientId",ClientId);
Int32 res;
res = dp.excuteNonQuery("dbo.updateWorkFlowPathsByPrimaryKey");
return res;
}

public Int32  dboUpdateWorkFlowPaths(Int32 pathId,string pathDesc,Int32 fldrId,Int32 docTypId,string pathDescAr,Int32 ClientId,string condition)
{
dp.parameters.Clear();
condition = " " + condition.Trim();
if (condition.Trim().ToLower().StartsWith("where") == false)
condition = " where " + condition;
dp.parameters.Add("@pathId",pathId);
dp.parameters.Add("@pathDesc",pathDesc);
dp.parameters.Add("@fldrId",fldrId);
dp.parameters.Add("@docTypId",docTypId);
dp.parameters.Add("@pathDescAr",pathDescAr);
dp.parameters.Add("@ClientId",ClientId);
dp.parameters.Add("@condition",condition);
Int32 res;
res = dp.excuteNonQuery("dbo.updateWorkFlowPaths");
return res;
}


        

    }
}
