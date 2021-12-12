using Borland.Vcl;
using dms.DTOS;
using iTextSharp.text;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using tables.dbo;

namespace dms.MangeForm
{
    public class MetaManager
    {
        CommonFunction.clsCommon c => new CommonFunction.clsCommon();
        DMS.DAL.operations op => new DMS.DAL.operations();
        Int32 clientID;
        public List<MetaDTO> LoadMetas(int documentTypeId)
        {
            var response = new List<MetaDTO>();

            var metasTB = op.dboGetAllMetas($"DocTypID = {documentTypeId} and (metaIdFK = 0 or metaIdFK is null)  order by orderSeq,columnSeq,metaID");
            if (metasTB.hasRows)
            {
                for (var i = 0; i < metasTB.rowsCount; i++)
                {
                    metasTB.currentIndex = i;
                    var objMetaDto = new MetaDTO(metasTB);
                    if (metasTB.fieldCtrlID == (int)ControlType.Table)
                    {
                        var metasTBCtrls = op.dboGetAllMetas($"metaIdFK = {metasTB.fieldMetaID}  order by orderSeq,columnSeq,metaID");
                        if (metasTBCtrls.hasRows)
                        {
                            for (var j = 0; j < metasTBCtrls.rowsCount; j++)
                            {
                                metasTBCtrls.currentIndex = j;
                                objMetaDto.tableCtrls.Add(new MetaDTO(metasTBCtrls));
                            }
                        }
                    }
                    response.Add(objMetaDto);
                }

            }
            return response;

        }
        public Response<List<MetaCustomPermissionDTO>> GetMetaCustomPermissions(int metaId)
        {
            try
            {
                Hashtable parameters = new Hashtable();
                parameters.Add("@metaID", metaId);
                DataTable dt2 = c.GetDataAsDataTableFromSP("getMetaUsersAndGroupsPermissions", parameters);
                List<MetaCustomPermissionDTO> metaCustomPermission = new List<MetaCustomPermissionDTO>();
                foreach (DataRow row in dt2.Rows)
                {
                    MetaCustomPermissionDTO obj = new MetaCustomPermissionDTO();
                    obj.ID = int.Parse(row["ID"].ToString());
                    obj.Name = row["Name"].ToString();
                    obj.AllowRead = bool.Parse(row["AllowRead"].ToString());
                    obj.AllowEdit = bool.Parse(row["AllowEdit"].ToString());
                    obj.PerType = row["PerType"].ToString();
                    metaCustomPermission.Add(obj);
                }

                return Response<List<MetaCustomPermissionDTO>>.Valid(metaCustomPermission);
            }
            catch (Exception ex)
            {

                return Response<List<MetaCustomPermissionDTO>>.Failed(ex);
            }

        }
        public Response<List<MetaCustomPermissionDTO>> GetMetaInheritPermissions(int metaId)
        {
            try
            {
                Hashtable parameters = new Hashtable();
                parameters.Add("@metaID", metaId);
                DataTable dt2 = c.GetDataAsDataTableFromSP("getMetaUsersAndGroupsFolderPermissions", parameters);
                List<MetaCustomPermissionDTO> metaCustomPermission = new List<MetaCustomPermissionDTO>();
                foreach (DataRow row in dt2.Rows)
                {
                    MetaCustomPermissionDTO obj = new MetaCustomPermissionDTO();
                    obj.ID = int.Parse(row["ID"].ToString());
                    obj.Name = row["Name"].ToString();
                    obj.AllowRead = row["AllowRead"].ToString() != "" ? bool.Parse(row["AllowRead"].ToString()) : false;
                    obj.AllowEdit = row["AllowEdit"].ToString() != "" ? bool.Parse(row["AllowEdit"].ToString()) : false;
                    obj.PerType = row["PerType"].ToString();
                    metaCustomPermission.Add(obj);
                }

                return Response<List<MetaCustomPermissionDTO>>.Valid(metaCustomPermission);
            }
            catch (Exception ex)
            {

                return Response<List<MetaCustomPermissionDTO>>.Failed(ex);
            }

        }
        public Response<int> SaveMeta(int metaID, int docTypID, string metaDesc, string metaDataType, bool required, int orderSeq, int ctrlID, string defaultTexts, string defaultValues, bool visible, string metaDescAr, string defaultArTexts, int columnSeq, int metaIdFK, double width, string permissionType, bool isNewRow, bool isNewColumn)
        {
            DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
            try
            {
                if (isNewRow)
                {
                    c.NonQuery($"update metas set orderSeq = orderSeq + 1 where  orderSeq >= {orderSeq} and docTypID = {docTypID}");
                }
                else
                {
                    if (isNewColumn)
                    {
                        c.NonQuery($"update metas set columnSeq = columnSeq + 1 where  columnSeq >=  {columnSeq} and docTypID = {docTypID}");
                    }
                }

                if (metaID == 0)
                {
                    if (isNewRow)
                    {
                        c.NonQuery($"update metas set orderSeq = orderSeq + 1 where  orderSeq >= {orderSeq} and docTypID = {docTypID}");
                    }
                    
                    metaID = sp.dboAddMetas(docTypID, metaDesc, metaDataType, required, orderSeq, ctrlID, defaultTexts, defaultValues, 
                        visible, metaDescAr, defaultArTexts, columnSeq, metaIdFK, width, permissionType ?? "Inherit");
                    if (metaID > 0 && ctrlID == (int)ControlType.Table)
                    {
                        c.GetTblRowAndColumnNumber(defaultTexts, out int rowNumber, out int columnNumber);
                        if (columnNumber > 0)
                        {
                            for (int i = 0; i < columnNumber; i++)
                            {
                                sp.dboAddMetas(docTypID, "", "String", false, 0, (int)ControlType.TextBox, "", "", true, "", "", i, metaID, 100 / columnNumber, permissionType ?? "Inherit");
                            }
                        }
                    }
                }
                else
                {
                    if (metaID > 0 && ctrlID == (int)ControlType.Table)
                    {
                        var oldmeta = op.dboGetMetasByPrimaryKey(metaID);

                        c.GetTblRowAndColumnNumber(oldmeta.fieldDefaultTexts, out int oldRowNumber, out int oldColumnNumber);
                        c.GetTblRowAndColumnNumber(defaultTexts, out int rowNumber, out int columnNumber);

                        if (oldColumnNumber < columnNumber)
                        {
                            for (int i = 0; i < (columnNumber - oldColumnNumber); i++)
                            {
                                sp.dboAddMetas(docTypID, "", "String", false, 0, (int)ControlType.TextBox, "", "", true, "", "", i, metaID, 100 / columnNumber, permissionType ?? "Inherit");
                            }
                        }
                        else
                        {
                            defaultTexts = $"tblRowNumber_{rowNumber },tblColumnNumber_{oldColumnNumber}";
                            defaultArTexts = defaultTexts;
                        }
                    }
                    sp.dboUpdateMetasByPrimaryKey(metaID, docTypID, metaDesc, metaDataType, required, orderSeq, ctrlID, defaultTexts, defaultValues, visible, metaDescAr, defaultArTexts, columnSeq, metaIdFK, width, permissionType);

                }
                return Response<int>.Valid(metaID);
            }
            catch (Exception ex)
            {
                return Response<int>.Failed(ex);
            }



        }

        public string SaveMetaPositions(List<MetaPostionDTO> request)
        {
            try
            {
                request = fixWidth(request);
                StringBuilder updateStringBuilder = new StringBuilder();
                foreach (var item in request)
                {
                    updateStringBuilder.Append($"update metas set orderSeq = {item.orderSeq}, columnSeq = {item.columnSeq}, width = {item.width} where metaID = {item.metaID} ; ");
                }
                if (updateStringBuilder.Length > 0)
                {
                    c.NonQuery(updateStringBuilder.ToString());
                }

                return "";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public List<MetaPostionDTO> fixWidth(List<MetaPostionDTO> metaPostionDTOs)
        {
            try
            {
                int currentRow = 0;
                double rowWidth = 0;
                foreach (var item in metaPostionDTOs)
                {
                    if (currentRow != item.orderSeq)
                    {
                        currentRow = item.orderSeq;
                        rowWidth = 0;
                    }
                    if ((rowWidth + item.width) > 100)
                    {
                        item.width = item.width - ((rowWidth + item.width) - 100);
                    }
                    else
                    {
                        rowWidth = rowWidth + item.width;
                    }
                    //currentRow++;
                }
                return metaPostionDTOs;
            }
            catch (Exception ex)
            {

                return metaPostionDTOs;
            }
        }
        public Response<bool> SaveImageUrl(int metaId, string imageUrl)
        {
            c.NonQuery($"update metas set defaultTexts = '{imageUrl}' , defaultArTexts = '{imageUrl}' where  metaID = {metaId}");
            return Response<bool>.Valid(true);
        }

        public Response<int> SaveDocType(int docTypeId, string docTypeDesc, string docTypeDescAr, int defaultWFId, bool docTypeIsActive)
        {
            clientID = Convert.ToInt32(System.Web.HttpContext.Current.Session["clientId"]);
            if (docTypeId > 0)
            {
                docTypeId = op.dboUpdateDocTypesByPrimaryKey(docTypeId, docTypeDesc, docTypeIsActive, defaultWFId, docTypeDescAr,false,clientID);
            }

            if (docTypeId == 0)
            {

                docTypeId = op.dboAddDocTypes(docTypeDesc, docTypeIsActive, defaultWFId, docTypeDescAr,false, clientID);
                Response<int>.Valid(docTypeId);
            }

            return Response<int>.Valid(docTypeId);
        }

        public Response<DocTypeDTO> GetDocType(int docTypeId)
        {
            var data = op.dboGetDocTypesByPrimaryKey(docTypeId);

            return Response<DocTypeDTO>.Valid(new DocTypeDTO(data));
        }
        public Response<bool> DeleteMeta(int metaId)
        {
            var objmeta = op.dboGetMetasByPrimaryKey(metaId);
            var parentMetaId = objmeta.fieldMetaIdFK;

            c.NonQuery($"delete from metas  where  metaID = {metaId}");
            if (parentMetaId > 0)
            {
                var objParentMeta = op.dboGetMetasByPrimaryKey(metaId);
                c.GetTblRowAndColumnNumber(objmeta.fieldDefaultTexts, out int rowNumber, out int columnNumber);
                var metasTBCtrls = op.dboGetAllMetas($"metaIdFK = {objParentMeta.fieldMetaID}  order by orderSeq,columnSeq,metaID");
                var defaultTexts = $"tblRowNumber_{rowNumber },tblColumnNumber_{ metasTBCtrls.rowsCount}";

                c.NonQuery($"update metas set defaultTexts = {defaultTexts} , defaultArTexts = {defaultTexts} where  metaID = {metaId}");
            }
            return Response<bool>.Valid(true);
        }
        public Response<bool> DuplicationMeta(int metaId, int metaIdFk = 0)
        {
            var objMeta = op.dboGetMetasByPrimaryKey(metaId);
            if (objMeta != null)
            {

                var orderSeq = objMeta.fieldOrderSeq + ((objMeta.fieldCtrlID == (int)ControlType.Label) ? 1 : 0);
                var columnSeq = (objMeta.fieldCtrlID == (int)ControlType.Label) ? 0 : objMeta.fieldColumnSeq + 1;
                var newMetaId = op.dboAddMetas(objMeta.fieldDocTypID, objMeta.fieldMetaDesc, objMeta.fieldMetaDataType, objMeta.fieldRequired, orderSeq, objMeta.fieldCtrlID, objMeta.fieldDefaultTexts, objMeta.fieldDefaultValues, objMeta.fieldVisible, objMeta.fieldMetaDescAr, columnSeq, objMeta.fieldPermissionType, objMeta.fieldDefaultArTexts, metaIdFk, objMeta.fieldWidth);
                if (objMeta.fieldCtrlID == (int)ControlType.Table)
                {
                    var metasTBCtrls = op.dboGetAllMetas($"metaIdFK = {objMeta.fieldMetaIdFK}  order by orderSeq,columnSeq,metaID");
                    if (metasTBCtrls.hasRows)
                    {
                        for (var j = 0; j < metasTBCtrls.rowsCount; j++)
                        {
                            metasTBCtrls.currentIndex = j;
                            DuplicationMeta(metasTBCtrls.fieldMetaID, newMetaId);
                        }
                    }
                }
                if (objMeta.fieldPermissionType == PermissionType.Custom.ToString())
                {
                    DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
                    sp.dboDuplicateMetaCustomPermission(objMeta.fieldMetaID, newMetaId);
                }
            }


            return Response<bool>.Valid(true);
        }

        public Response<bool> HideMeta(int metaId)
        {
            c.NonQuery($"update metas set visible = 0   where  metaID = {metaId}");
            return Response<bool>.Valid(true);
        }
        public Response<bool> ShowMeta(int metaId)
        {
            c.NonQuery($"update metas set visible = 1  where  metaID = {metaId}");
            return Response<bool>.Valid(true);
        }
        public Response<MetaPermissionDTO> GetMetaUserPermission(int metaId, int userId)
        {
            try
            {
                var response = new MetaPermissionDTO();
                string sqlEdit = $"select [allowEdit] from [dbo].[metaUsersPermissions] where [metaID] =  { metaId }  and [userID] ={userId}";
                bool isEdit = Boolean.Parse(c.GetDataAsScalar(sqlEdit).ToString() != "" ? c.GetDataAsScalar(sqlEdit).ToString() : "False");
                response.AllowEdit = isEdit;

                string sqlRead = $"select [allowRead] from [dbo].[metaUsersPermissions] where [metaID] =  { metaId }  and [userID] ={userId}";
                bool isRead = Boolean.Parse(c.GetDataAsScalar(sqlRead).ToString() != "" ? c.GetDataAsScalar(sqlRead).ToString() : "False");
                response.AllowRead = isRead;

                return Response<MetaPermissionDTO>.Valid(response);
            }
            catch (Exception ex)
            {

                return Response<MetaPermissionDTO>.Failed(ex);
            }

        }
        public Response<MetaPermissionDTO> GetMetaGroupPermission(int metaId, int groupId)
        {
            try
            {
                var response = new MetaPermissionDTO();
                string sqlEdit = $"select [allowEdit] from [dbo].[metaGroupsPermissions] where [metaID] =  { metaId }  and [grpID] ={groupId}";
                bool isEdit = Boolean.Parse(c.GetDataAsScalar(sqlEdit).ToString() != "" ? c.GetDataAsScalar(sqlEdit).ToString() : "False");
                response.AllowEdit = isEdit;

                string sqlRead = $"select [allowRead] from [dbo].[metaGroupsPermissions] where [metaID] =  { metaId }  and [grpID] ={groupId}";
                bool isRead = Boolean.Parse(c.GetDataAsScalar(sqlRead).ToString() != "" ? c.GetDataAsScalar(sqlRead).ToString() : "False");
                response.AllowRead = isRead;

                return Response<MetaPermissionDTO>.Valid(response);
            }
            catch (Exception ex)
            {

                return Response<MetaPermissionDTO>.Failed(ex);
            }

        }

        public Response<bool> DeleteMetaUserPermission(int metaId, int userId)
        {
            c.NonQuery($"delete from metaUsersPermissions  where  metaID = {metaId} and userId = {userId}");

            return Response<bool>.Valid(true);
        }

        public Response<bool> DeleteMetaGroupPermission(int metaId, int groupId)
        {
            c.NonQuery($"delete from metaGroupsPermissions  where  metaID = {metaId} and grpID = {groupId}");

            return Response<bool>.Valid(true);
        }
        public Response<bool> SaveMetaUserPermission(int metaId, int userId, bool allowRead, bool allowEdit)
        {
            try
            {
                DeleteMetaUserPermission(metaId, userId);
                c.NonQuery($"update metas set permissionType = 'Custom' where metaID = {metaId}");
                var read = allowRead ? 1 : 0;
                var edit = allowEdit ? 1 : 0;
                c.NonQuery($"insert into [dbo].[metaUsersPermissions] values({ metaId },{ userId },{ read }, {edit })");

                return Response<bool>.Valid(true);
            }
            catch (Exception ex)
            {

                return Response<bool>.Failed(ex);
            }

        }

        public Response<bool> SaveMetaGroupPermission(int metaId, int groupId, bool allowRead, bool allowEdit)
        {
            try
            {
                DeleteMetaGroupPermission(metaId, groupId);
                c.NonQuery($"update metas set permissionType = 'Custom' where metaID = {metaId}");
                var read = allowRead ? 1 : 0;
                var edit = allowEdit ? 1 : 0;
                c.NonQuery($"insert into [dbo].[metaGroupsPermissions] values({ metaId },{ groupId },{ read }, {edit })");

                return Response<bool>.Valid(true);
            }
            catch (Exception ex)
            {

                return Response<bool>.Failed(ex);
            }

        }

        public Response<bool> SaveMetaInheritPermission(int metaId)
        {
            try
            {
                c.NonQuery($"update metas set permissionType = 'Inherit' where metaID = {metaId}");


                return Response<bool>.Valid(true);
            }
            catch (Exception ex)
            {

                return Response<bool>.Failed(ex);
            }

        }


    }
}