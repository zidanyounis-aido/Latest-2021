using dms.DTOS;
using dms.MangeForm;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using tables.dbo;

namespace dms.Admin
{
    public partial class manageDocTypes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                loadDocTypes();
                loadPermission();
                loadControlTypes();
            }
        }
        void loadDocTypes()
        {
            var op = new DMS.DAL.operations();
            var data = op.dboGetAllDocTypes();
            ListViewDocTypes.DataSource = data.table;
            ListViewDocTypes.DataBind();
        }
        void loadPermission()
        {
            loadGroups();
            loadUsers();
        }
        void loadGroups()
        {
            var op = new DMS.DAL.operations();
            var data = op.dboGetAllGroups();

            slctGroups.DataSource = data.table;

            slctGroups.DataTextField = "GrpDesc";

            slctGroups.DataValueField = "GrpID";
            slctGroups.DataBind();
            slctGroups.Items.Insert(0, new ListItem(System.Web.HttpContext.Current.Session["lang"].ToString() == "0" ? "--Select--" : "--اختر--", "0"));
        }

        void loadUsers()
        {
            var op = new DMS.DAL.operations();
            var data = op.dboGetAllUsers();

            slctUsers.DataSource = data.table;

            slctUsers.DataTextField = "FullName";

            slctUsers.DataValueField = "UserID";
            slctUsers.DataBind();
            slctUsers.Items.Insert(0, new ListItem(System.Web.HttpContext.Current.Session["lang"].ToString() == "0" ? "--Select--" : "--اختر--", "0"));
        }
        void loadControlTypes()
        {
            var op = new DMS.DAL.operations();
            var dataGetAllControlsType = op.dboGetAllControlsTypes();

            pctrlID.DataSource = dataGetAllControlsType.table;

            pctrlID.DataTextField = Session["lang"].ToString() == "0" ? "ctrlDesc" : "ctrlDescAr";

            pctrlID.DataValueField = "crtlID";
            pctrlID.DataBind();



            var dataGetAllWorkFlowPath = op.dboGetAllWorkFlowPaths();

            slctDefaultWF.DataSource = dataGetAllWorkFlowPath.table;

            slctDefaultWF.DataTextField = Session["lang"].ToString() == "0" ? "pathDesc" : "pathDescAr";

            slctDefaultWF.DataValueField = "pathId";
            slctDefaultWF.DataBind();


            if (System.Web.HttpContext.Current.Session["lang"].ToString() == "0")
            {
                pctrlID.Items.Insert(0, new ListItem("--Select--", Int16.MinValue.ToString()));
                slctDefaultWF.Items.Insert(0, new ListItem("--Select--", Int16.MinValue.ToString()));
            }
            else
            {
                pctrlID.Items.Insert(0, new ListItem("--اختر--", Int16.MinValue.ToString()));
                slctDefaultWF.Items.Insert(0, new ListItem("--اختر--", Int16.MinValue.ToString()));
            }


        }

        [System.Web.Services.WebMethod()]
        public static List<MetaDTO> GetDocTypeMetas(int documentTypeId)
        {
            var metaManager = new MetaManager();
            var response = metaManager.LoadMetas(documentTypeId);
            return response;
        }

        [System.Web.Services.WebMethod()]
        public static Response<int> SaveMeta(int metaID, int docTypID, string metaDesc, string metaDataType, bool required, int orderSeq, int ctrlID, string defaultTexts, string defaultValues, bool visible, string metaDescAr, string defaultArTexts, int columnSeq, int metaIdFK, double width, string permissionType, bool isNewRow, bool isNewColumn)
        {
            var metaManager = new MetaManager();
            return metaManager.SaveMeta(metaID, docTypID, metaDesc, metaDataType, required, orderSeq, ctrlID, defaultTexts, defaultValues, visible, metaDescAr, defaultArTexts, columnSeq, metaIdFK, width, permissionType, isNewRow, isNewColumn);
        }

        [System.Web.Services.WebMethod()]
        public static Response<int> SaveDocType(int docTypeId, string docTypeDesc, string docTypeDescAr, int defaultWFId, bool docTypeIsActive)
        {
            var metaManager = new MetaManager();
            return metaManager.SaveDocType(docTypeId, docTypeDesc, docTypeDescAr, defaultWFId, docTypeIsActive);
        }
        [System.Web.Services.WebMethod()]
        public static string Save(object request)
        {
            var metaManager = new MetaManager();
            var dataRequset = new JavaScriptSerializer().ConvertToType<List<MetaPostionDTO>>(request);
            return metaManager.SaveMetaPositions(dataRequset);
        }

        [System.Web.Services.WebMethod()]
        public static Response<DocTypeDTO> GetDocType(int documentTypeId)
        {
            var metaManager = new MetaManager();
            var response = metaManager.GetDocType(documentTypeId);
            return response;
        }
        [System.Web.Services.WebMethod()]
        public static Response<bool> DeleteMeta(int metaId)
        {
            var metaManager = new MetaManager();
            var response = metaManager.DeleteMeta(metaId);
            return response;
        }

        [System.Web.Services.WebMethod()]
        public static Response<bool> DuplicationMeta(int metaId)
        {
            var metaManager = new MetaManager();
            var response = metaManager.DuplicationMeta(metaId);
            return response;
        }
        [System.Web.Services.WebMethod()]
        public static Response<bool> ShowMeta(int metaId)
        {
            var metaManager = new MetaManager();
            var response = metaManager.ShowMeta(metaId);
            return response;
        }
        [System.Web.Services.WebMethod()]
        public static Response<bool> HideMeta(int metaId)
        {
            var metaManager = new MetaManager();
            var response = metaManager.HideMeta(metaId);
            return response;
        }

        [System.Web.Services.WebMethod()]
        public static Response<MetaPermissionDTO> GetMetaUserPermission(int metaId, int userId)
        {
            var metaManager = new MetaManager();
            var response = metaManager.GetMetaUserPermission(metaId, userId);
            return response;
        }
        [System.Web.Services.WebMethod()]
        public static Response<bool> DeleteMetaUserPermission(int metaId, int userId)
        {
            var metaManager = new MetaManager();
            var response = metaManager.DeleteMetaUserPermission(metaId, userId);
            return response;
        }
        [System.Web.Services.WebMethod()]
        public static Response<List<MetaCustomPermissionDTO>> GetMetaCustomPermissions(int metaId)
        {
            var metaManager = new MetaManager();
            var response = metaManager.GetMetaCustomPermissions(metaId);
            return response;
        }
        [System.Web.Services.WebMethod()]
        public static Response<List<MetaCustomPermissionDTO>> GetMetaInheritPermissions(int metaId)
        {
            var metaManager = new MetaManager();
            var response = metaManager.GetMetaInheritPermissions(metaId);
            return response;
        }
        [System.Web.Services.WebMethod()]
        public static Response<bool> SaveMetaUserPermission(int metaId, int userId, bool allowRead, bool allowEdit)
        {
            var metaManager = new MetaManager();
            var response = metaManager.SaveMetaUserPermission(metaId, userId, allowRead, allowEdit);
            return response;
        }

        [System.Web.Services.WebMethod()]
        public static Response<MetaPermissionDTO> GetMetaGroupPermission(int metaId, int groupId)
        {
            var metaManager = new MetaManager();
            var response = metaManager.GetMetaGroupPermission(metaId, groupId);
            return response;
        }


        [System.Web.Services.WebMethod()]
        public static Response<bool> DeleteMetaGroupPermission(int metaId, int groupId)
        {
            var metaManager = new MetaManager();
            var response = metaManager.DeleteMetaGroupPermission(metaId, groupId);
            return response;
        }
        [System.Web.Services.WebMethod()]
        public static Response<bool> SaveMetaGroupPermission(int metaId, int groupId, bool allowRead, bool allowEdit)
        {
            var metaManager = new MetaManager();
            var response = metaManager.SaveMetaGroupPermission(metaId, groupId, allowRead, allowEdit);
            return response;
        }

        [System.Web.Services.WebMethod()]
        public static Response<bool> SaveMetaInheritPermission(int metaId)
        {
            var metaManager = new MetaManager();
            var response = metaManager.SaveMetaInheritPermission(metaId);
            return response;
        }

        protected string SafeSmartSubstring(string val)
        {
            try
            {
                if (val.IndexOf(" ") != -1)
                {
                    val = val.Trim();
                    string fullName = val;
                    string[] names = fullName.Split(' ');
                    string name = names.First();
                    string lastName = names.Last();
                    var fullVal = name + " " + lastName;
                    string str = String.Join(String.Empty, fullVal.Split(new[] { ' ' }).Select(word => word.First()));
                    str = System.Text.RegularExpressions.Regex.Replace(str, ".{1}", "$0 ");
                    return str;
                }
                else
                {
                    return val.Substring(0, 1);
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        protected void ListViewDocTypes_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {

        }
    }
}