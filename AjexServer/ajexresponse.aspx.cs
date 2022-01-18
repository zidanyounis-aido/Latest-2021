using dms.VM;
using DMS.Resources;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.AjexServer
{
    public partial class ajexresponse : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // get all Clinck in organization
        //[WebMethod]
        //public static string BinallClincs()
        //{
        //    ScopeMasterEntities db = new ScopeMasterEntities();
        //    List<ObjetX> list = new List<ObjetX>();
        //    var data = db.Clinics.ToList();
        //    int v = data.Count();
        //    int i = 0;
        //    foreach (var item in data)
        //    {
        //        ObjetX objst = new ObjetX();
        //        objst.ID = Convert.ToInt32(item.ClinicID);
        //        objst.Name = Convert.ToString(item.ClinicName);
        //        list.Insert(i, objst);
        //        i++;
        //    }
        //    JavaScriptSerializer jscript = new JavaScriptSerializer();
        //    return jscript.Serialize(list);
        //}
        //// get all rooms in spec clinic
        [WebMethod]
        public static string BindAllusers(int ID)
        {
            List<UsersList> list = new List<UsersList>();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                string query = "select userID as 'Id',userName as 'Name', CASE WHEN (select[allowRead] from[dbo].[metaUsersPermissions] where[metaID] =  " + ID + "  and [userID] = dbo.users.userID)= 1 THEN '1' ELSE '0' END as 'isRead', CASE WHEN (select[allowEdit] from[dbo].[metaUsersPermissions] where[metaID] =" + ID + " and[userID] = dbo.users.userID)= 1 THEN '1' ELSE '0' END as 'isEdit' from dbo.users where active = 1";
                DataTable dt = c.GetDataAsDataTable(query);
                foreach (var item in dt.AsEnumerable())
                {
                    UsersList obj = new UsersList();
                    obj.Id = item.Field<int>("Id");
                    obj.Name = item.Field<string>("Name");
                    obj.isRead = item.Field<string>("isRead") == "1" ? true : false;
                    obj.isEdit = item.Field<string>("isEdit") == "1" ? true : false; ;
                    list.Add(obj);
                }
                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(list);
            }
            catch (Exception ex)
            {

                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(list);
            }
        }
        [WebMethod]
        public static string BindEvenets()
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1);
            DateTime start = startDate;
            DateTime end = endDate;
            var userId = Convert.ToInt32(HttpContext.Current.Session["userID"].ToString());
            List<int> idList = new List<int>();
            List<ImproperCalendarEvent> tasksList = new List<ImproperCalendarEvent>();
            //Generate JSON serializable events
            foreach (CalendarEvent cevent in EventDAO.getEvents(start, end, userId))
            {

                tasksList.Add(new ImproperCalendarEvent
                {
                    id = cevent.id,
                    title = cevent.title,
                    start = String.Format("{0:s}", cevent.start),
                    end = String.Format("{0:s}", cevent.end),
                    description = cevent.description,
                    allDay = cevent.allDay,
                    backgroundColor = cevent.backgroundColor,
                    borderColor = cevent.backgroundColor,
                    CreatedBy = cevent.CreatedBy,
                    color = cevent.backgroundColor
                });
                idList.Add(cevent.id);
            }
            HttpContext.Current.Session["idList"] = idList;
            //Serialize events to string
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(tasksList);
            return sJSON;
        }
        //// get all Proc in spec clinic
        [WebMethod]
        public static string SavePermisstion(string jsonData, int metaId)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                var httpCookie = HttpContext.Current.Request.Cookies["privileges"];
                List<UsersList> collection = serializer.Deserialize<List<UsersList>>(jsonData);
                foreach (var item in collection)
                {
                    //Example 1
                    CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                    c.NonQuery("delete [dbo].[metaUsersPermissions] where [metaID]=" + metaId + " and [userID]=" + item.Id + " ");
                    int allowEdit = item.isEdit == true ? 1 : 0;
                    int allowRead = item.isRead == true ? 1 : 0;
                    c.NonQuery("insert into [dbo].[metaUsersPermissions] values(" + metaId + "," + item.Id + "," + allowRead + "," + allowEdit + ")");
                }
                return serializer.Serialize("true");
            }
            catch (Exception ex)
            {

                return serializer.Serialize("false");
            }
        }

        [WebMethod]
        public static string SaveItemsPositions(string jsonData)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                var httpCookie = HttpContext.Current.Request.Cookies["privileges"];
                List<MetaList> collection = serializer.Deserialize<List<MetaList>>(jsonData);
                foreach (var item in collection)
                {
                    //Example 1
                    CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                    c.NonQuery("update [dbo].[metas] set orderSeq=" + item.Index + " where [metaID]=" + item.Id + " ");
                }
                return serializer.Serialize("true");
            }
            catch (Exception ex)
            {

                return serializer.Serialize("false");
            }
        }
        [WebMethod]
        public static string SaveWorkFlowPositions(string jsonData)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                var httpCookie = HttpContext.Current.Request.Cookies["privileges"];
                List<MetaList> collection = serializer.Deserialize<List<MetaList>>(jsonData);
                int indexValue = 1;
                foreach (var item in collection)
                {
                    //Example 1
                    CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                    c.NonQuery("update [dbo].[wfPathDetails] set seqNo=" + indexValue + " where id=" + item.Id + "");
                    indexValue++;
                }
                return serializer.Serialize("true");
            }
            catch (Exception ex)
            {

                return serializer.Serialize("false");
            }
        }
        [WebMethod]
        public static string UpdateMeta(string jsonData)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                MetaTB metaEntity = serializer.Deserialize<MetaTB>(jsonData);
                //var httpCookie = HttpContext.Current.Request.Cookies["privileges"];
                //  collection = serializer.Deserialize<List<MetaList>>(jsonData);
                //foreach (var item in collection)
                //{
                //    //Example 1
                int isVis = 0;
                int isReq = 0;
                if (metaEntity.visible)
                {
                    isVis = 1;
                }
                if (metaEntity.required)
                {
                    isReq = 1;
                }
                string query = "update [dbo].[metas] set   metaDesc=N'" + metaEntity.metaDesc + "', metaDescAr=N'" + metaEntity.metaDescAr + "', metaDataType= '" + metaEntity.metaDataType + "', ctrlID= " + metaEntity.ctrlID + ", defaultTexts=N'" + metaEntity.defaultTexts + "', defaultValues=N'" + metaEntity.defaultValues + "', visible= " + isVis + ", required = " + isReq + "  where [metaID]=" + metaEntity.metaID + "";
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                c.NonQuery(query);
                //}
                return serializer.Serialize("true");
            }
            catch (Exception ex)
            {

                return serializer.Serialize("false");
            }
        }
        [WebMethod]
        public static string GetMetaByID(int id)
        {
            MetaTB entity = new MetaTB();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                string query = "SELECT  * FROM dbo.metas where metaID=" + id + "";
                DataTable dt = c.GetDataAsDataTable(query);
                foreach (var item in dt.AsEnumerable())
                {
                    MetaTB obj = new MetaTB()
                    {
                        metaID = item.Field<int>("metaID"),
                        ctrlID = item.Field<int>("ctrlID"),
                        defaultTexts = item.Field<string>("defaultTexts"),
                        defaultValues = item.Field<string>("defaultValues"),
                        metaDataType = item.Field<string>("metaDataType"),
                        metaDesc = item.Field<string>("metaDesc"),
                        metaDescAr = item.Field<string>("metaDescAr"),
                        required = item.Field<bool>("required"),
                        visible = item.Field<bool>("visible")
                    };
                    entity = obj;
                    //obj.metaID = item.Field<int>("metaID");
                    //obj.metaDesc = item.Field<string>("Documnet");
                    //obj.metaDescAr = item.Field<string>("Width");
                    //obj.Height = item.Field<string>("Height");
                    //obj.Top = item.Field<string>("Top");
                    //obj.Left = item.Field<string>("Left");
                    //obj.Signture = item.Field<string>("Signture");
                    //obj.UserId = item.Field<int>("UserId");
                    //list.Add(obj);
                }
                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(entity);
            }
            catch (Exception ex)
            {

                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(entity);
            }
        }
        [WebMethod]
        public static string SetComplet(int id)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                c.NonQuery("update [dbo].[ToDoList] set [IsComplete]=1 where [Id]=" + id);
                return serializer.Serialize("true");
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }
        [WebMethod]
        public static string updateEventDateTime(int id, string start, string end)
        {
            start = ConvertToEasternArabicNumerals(start);
            end = ConvertToEasternArabicNumerals(end);
            var serializer = new JavaScriptSerializer();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                string query = "update [dbo].[event] set [event_start]=CAST('" + start + "' AS DATE) , [event_end]=CAST('" + end + "' AS DATE)  where [event_id]=" + id;
                c.NonQuery(query);
                return serializer.Serialize("true");
            }
            catch (Exception ex)
            {

                return serializer.Serialize("false");
            }
        }
        public static string ConvertToEasternArabicNumerals(string input)
        {
            string EnglishNumbers = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                {
                    EnglishNumbers += char.GetNumericValue(input, i);
                }
                else
                {
                    EnglishNumbers += input[i].ToString();
                }
            }
            return EnglishNumbers;

        }
        [WebMethod]
        public static string AddSignture(string width, string height, string top, string left, int user, string document)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                //Example 5
                string signture = c.GetDataAsScalar("select top 1 Signature from users where userID=" + user).ToString();
                if (signture != null && signture != "")
                {
                    //int id = c.NonQuery("insert into SignatureTB values('" + signture + "','" + document + "'," + user + ",'" + width + "','" + height + "','" + top + "','" + left + "')");
                    Hashtable parameters = new Hashtable();
                    parameters.Add("@signture", signture);
                    parameters.Add("@user", user);
                    parameters.Add("@width", width);
                    parameters.Add("@height", height);
                    parameters.Add("@top", top);
                    parameters.Add("@left", left);
                    parameters.Add("@document", document);
                    c.NonQueryFromSP("AddSigture", parameters);

                    int maxid = int.Parse(c.GetDataAsScalar("select top 1 max(Id) from SignatureTB where UserId=" + user).ToString());
                    return serializer.Serialize(maxid);
                }
                else
                {
                    return serializer.Serialize("false");
                }
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }

        [WebMethod]
        public static string  AddLable(string width, string height, string top, string left, int user, string document,string lable)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                string txtDocID = document.Split('-')[0];
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                //Example 5
                //string lable = "";//c.GetDataAsScalar("select top 1 Barcode from documents where docID=" + document.Split('-')[0]).ToString();
                int sort = int.Parse(c.GetDataAsScalar("select ISNULL(max(DocumentLablesTB.Sort),0) from DocumentLablesTB where Documnet='" + document + "'").ToString()) + 1;
                //try
                //{
                //    string serial = c.GetDataAsScalar("select top 1 serial from documents where docID=" + int.Parse(txtDocID) + "").ToString();
                //    string typeId = c.GetDataAsScalar("select top 1 typeId from documents where docID=" + int.Parse(txtDocID) + "").ToString();
                //    string txtDocName = c.GetDataAsScalar("select top 1 docName from documents where docID=" + int.Parse(txtDocID) + "").ToString();
                //    string docNUM = txtDocID + "-" + sort;
                //    //using IronBarCode;
                //    GeneratedBarcode MyBarCode = IronBarCode.BarcodeWriter.CreateBarcode("00000000" + txtDocID, BarcodeWriterEncoding.Code128, 200, 50);
                //    string txtBarCode = "العنوان : " + txtDocName + "";
                //    txtBarCode += "\r\n";
                //    txtBarCode += "التاريخ : " + DateTime.Now.ToString("dd-MM-yyyy") + "   " + "رقم المستند :" + docNUM;
                //    if (typeId != "" && typeId != null)
                //    {
                //        if (typeId == "1")
                //        {
                //            txtBarCode += "\r\n";
                //            txtBarCode += "رقم الصادر : " + serial;
                //        }
                //        else
                //        {
                //            txtBarCode += "\r\n";
                //            txtBarCode += "رقم الوارد : " + serial;
                //        }
                //    }
                //    string filename = "/images/barcode" + DateTime.Now.ToString("ddMMyyyyhhmmssfff") + ".png";
                //    MyBarCode.AddAnnotationTextAboveBarcode(txtBarCode);
                //    MyBarCode.SaveAsPng(HttpContext.Current.Server.MapPath("~" + filename));
                //    lable = filename;
                //    //c.NonQuery("update documents set Barcode='" + filename + "' where docID=" + int.Parse(txtDocID));
                //}
                //catch (Exception ex)
                //{

                //    //throw;
                //}
                if (lable != null && lable != "")
                {
                    //int id = c.NonQuery("insert into SignatureTB values('" + signture + "','" + document + "'," + user + ",'" + width + "','" + height + "','" + top + "','" + left + "')");
                    Hashtable parameters = new Hashtable();
                    parameters.Add("@lable", lable);
                    parameters.Add("@user", user);
                    parameters.Add("@width", width);
                    parameters.Add("@height", height);
                    parameters.Add("@top", top);
                    parameters.Add("@left", left);
                    parameters.Add("@document", document);
                    parameters.Add("@sort", sort);
                    c.NonQueryFromSP("AddDocumentLable", parameters);

                    int maxid = int.Parse(c.GetDataAsScalar("select top 1 max(Id) from DocumentLablesTB where UserId=" + user).ToString());
                    return serializer.Serialize(maxid+"|"+ lable+"|"+sort);
                }
                else
                {
                    return serializer.Serialize("false");
                }
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }

        [WebMethod]
        public static string CopyBarcode(int id, string tans,string document)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                //Example 5
                string inValues = "";
                var transArr = tans.Split(';');
                foreach (var item in transArr)
                {
                    if (item != "")
                    {
                        Hashtable parameters = new Hashtable();
                        parameters.Add("@id", id);
                        parameters.Add("@tansform", item+";");
                        c.NonQueryFromSP("CopyBarcode", parameters);
                        string q = "select top 1 max(Id) from DocumentLablesTB where Documnet='" + document + "'";
                        int maxid = int.Parse(c.GetDataAsScalar(q).ToString());
                        if (inValues == "")
                        {
                            inValues = maxid.ToString();
                        }
                        else
                        {
                            inValues += "," + maxid;
                        }
                    }
                }
                //int id = c.NonQuery("insert into SignatureTB values('" + signture + "','" + document + "'," + user + ",'" + width + "','" + height + "','" + top + "','" + left + "')");
                List<SignatureTB> list = new List<SignatureTB>();
                string query = "SELECT  Id, Lable, Documnet, UserId, Width, Height, [Top], [Left],Transform,Sort FROM dbo.DocumentLablesTB where id in("+inValues+")";
                DataTable dt = c.GetDataAsDataTable(query);
                foreach (var item in dt.AsEnumerable())
                {
                    SignatureTB obj = new SignatureTB();
                    obj.Id = item.Field<int>("Id");
                    obj.Documnet = item.Field<string>("Documnet");
                    obj.Transform = item.Field<string>("Transform");
                    obj.Width = item.Field<string>("Width");
                    obj.Height = item.Field<string>("Height");
                    obj.Top = item.Field<string>("Top");
                    obj.Left = item.Field<string>("Left");
                    obj.Lable = item.Field<string>("Lable");
                    obj.UserId = item.Field<int>("UserId");
                    obj.Sort = item.Field<int>("Sort");
                    list.Add(obj);
                }
                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(list);
                //return serializer.Serialize(maxid);
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }
        [WebMethod]
        public static string CopySignture(int id, string tans, string document)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                //Example 5
                string inValues = "";
                var transArr = tans.Split(';');
                foreach (var item in transArr)
                {
                    if (item != "")
                    {
                        Hashtable parameters = new Hashtable();
                        parameters.Add("@id", id);
                        parameters.Add("@tansform", item + ";");
                        c.NonQueryFromSP("CopySignture", parameters);
                        string q = "select top 1 max(Id) from SignatureTB where Documnet='" + document + "'";
                        int maxid = int.Parse(c.GetDataAsScalar(q).ToString());
                        if (inValues == "")
                        {
                            inValues = maxid.ToString();
                        }
                        else
                        {
                            inValues += "," + maxid;
                        }
                    }
                }
                //int id = c.NonQuery("insert into SignatureTB values('" + signture + "','" + document + "'," + user + ",'" + width + "','" + height + "','" + top + "','" + left + "')");
                List<SignatureTB> list = new List<SignatureTB>();
                string query = "SELECT  Id, Signture, Documnet, UserId, Width, Height, [Top], [Left],Transform FROM dbo.SignatureTB where id in(" + inValues + ")";
                DataTable dt = c.GetDataAsDataTable(query);
                foreach (var item in dt.AsEnumerable())
                {
                    SignatureTB obj = new SignatureTB();
                    obj.Id = item.Field<int>("Id");
                    obj.Documnet = item.Field<string>("Documnet");
                    obj.Transform = item.Field<string>("Transform");
                    obj.Width = item.Field<string>("Width");
                    obj.Height = item.Field<string>("Height");
                    obj.Top = item.Field<string>("Top");
                    obj.Left = item.Field<string>("Left");
                    obj.Signture = item.Field<string>("Signture");
                    obj.UserId = item.Field<int>("UserId");
                    list.Add(obj);
                }
                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(list);
                //return serializer.Serialize(maxid);
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }
        [WebMethod]
        public static string UpdateLablePosition(string top, string left, string transform, int id)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                string q = "update DocumentLablesTB set  [top]=" + top + ",[Left]=" + left + ",[Transform]='" + transform + "' where id=" + id + "";
                c.NonQuery(q);
                return serializer.Serialize("true");
                //Example 5
                //string lable = c.GetDataAsScalar("select top 1 Barcode from documents where docID=" + document.Split('-')[0]).ToString();
                //if (lable != null && lable != "")
                //{
                //    //int id = c.NonQuery("insert into SignatureTB values('" + signture + "','" + document + "'," + user + ",'" + width + "','" + height + "','" + top + "','" + left + "')");
                //    Hashtable parameters = new Hashtable();
                //    parameters.Add("@lable", lable);
                //    parameters.Add("@user", user);
                //    parameters.Add("@width", width);
                //    parameters.Add("@height", height);
                //    parameters.Add("@top", top);
                //    parameters.Add("@left", left);
                //    parameters.Add("@document", document);
                //    c.NonQueryFromSP("AddDocumentLable", parameters);

                //    int maxid = int.Parse(c.GetDataAsScalar("select top 1 max(Id) from DocumentLablesTB where UserId=" + user).ToString());
                //    return serializer.Serialize(maxid);
                //}
                //else
                //{
                //    return serializer.Serialize("false");
                //}
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }
        [WebMethod]
        public static string UpdateSignPosition(string top, string left, string transform, int id)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                string q = "update SignatureTB set  [top]=" + top + ",[Left]=" + left + ",[Transform]='" + transform + "' where id=" + id + "";
                c.NonQuery(q);
                return serializer.Serialize("true");
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }
        [WebMethod]
        public static string GetAllSigntures(string document)
        {
            List<SignatureTB> list = new List<SignatureTB>();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                string query = "SELECT  Id, Signture, Documnet, UserId, Width, Height, [Top], [Left],Transform FROM dbo.SignatureTB where Documnet='" + document.ToString() + "'";
                DataTable dt = c.GetDataAsDataTable(query);
                foreach (var item in dt.AsEnumerable())
                {
                    SignatureTB obj = new SignatureTB();
                    obj.Id = item.Field<int>("Id");
                    obj.Documnet = item.Field<string>("Documnet");
                    obj.Transform = item.Field<string>("Transform");
                    obj.Width = item.Field<string>("Width");
                    obj.Height = item.Field<string>("Height");
                    obj.Top = item.Field<string>("Top");
                    obj.Left = item.Field<string>("Left");
                    obj.Signture = item.Field<string>("Signture");
                    obj.UserId = item.Field<int>("UserId");
                    list.Add(obj);
                }
                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(list);
            }
            catch (Exception ex)
            {

                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(list);
            }
        }


        [WebMethod]
        public static string GetAllBarcods(string document)
        {
            List<SignatureTB> list = new List<SignatureTB>();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                string query = "SELECT  Id, Lable, Documnet, UserId, Width, Height, [Top], [Left],Transform,Sort FROM dbo.DocumentLablesTB where Documnet='" + document.ToString() + "'";
                DataTable dt = c.GetDataAsDataTable(query);
                foreach (var item in dt.AsEnumerable())
                {
                    SignatureTB obj = new SignatureTB();
                    obj.Id = item.Field<int>("Id");
                    obj.Documnet = item.Field<string>("Documnet");
                    obj.Transform = item.Field<string>("Transform");
                    obj.Width = item.Field<string>("Width");
                    obj.Height = item.Field<string>("Height");
                    obj.Top = item.Field<string>("Top");
                    obj.Left = item.Field<string>("Left");
                    obj.Lable = item.Field<string>("Lable");
                    obj.UserId = item.Field<int>("UserId");
                    obj.Sort = item.Field<int>("Sort");
                    list.Add(obj);
                }
                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(list);
            }
            catch (Exception ex)
            {

                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(list);
            }
        }
        [WebMethod]
        public static string GetAllSearch(string str)
        {
            List<SignatureTB> list = new List<SignatureTB>();
            try
            {
                GeneralSearchLists generalSearhLists = new GeneralSearchLists();
                // List<EventIns> EventInsList = new List<EventIns>();
                generalSearhLists.EvenetsList = new List<EventIns>();
                //search events
                try
                {
                    var dt1 = LoadEvents(str);
                    foreach (DataRow item in dt1.Rows)
                    {
                        EventIns obj = new EventIns();
                        obj.Id = int.Parse(item["event_id"].ToString());// item.Field<int>("docID");
                        obj.Name = item["title"].ToString();//("docName");
                        obj.Type = 1;
                        generalSearhLists.EvenetsList.Add(obj);
                    }
                    //foreach (var item in dt1.AsEnumerable())
                    //{
                    //    EventIns obj = new EventIns();
                    //    obj.Id = item.Field<int>("event_id");
                    //    obj.Name = item.Field<string>("title");
                    //    obj.Type = 1;
                    //    generalSearhLists.EvenetsList.Add(obj);
                    //}
                }
                catch (Exception)
                {

                    // throw;
                }
                generalSearhLists.TasksList = new List<EventIns>();
                //search tasks
                try
                {
                    var dt2 = LoadTasks(str);
                    foreach (DataRow item in dt2.Rows)
                    {
                        EventIns obj = new EventIns();
                        obj.Id = int.Parse(item["Id"].ToString());// item.Field<int>("docID");
                        obj.Name = item["TaskName"].ToString();//("docName");
                        obj.Type = 2;
                        generalSearhLists.TasksList.Add(obj);
                    }
                    //foreach (var item in dt2.AsEnumerable())
                    //{
                    //    EventIns obj = new EventIns();
                    //    obj.Id = item.Field<int>("Id");
                    //    obj.Name = item.Field<string>("TaskName");
                    //    obj.Type = 2;
                    //    generalSearhLists.TasksList.Add(obj);
                    //}
                }
                catch (Exception)
                {

                    // throw;
                }
                generalSearhLists.DocumentsList = new List<EventIns>();
                //search events
                try
                {
                    var dt3 = LoadDocuments(str);
                    foreach (DataRow item in dt3.Rows)
                    {
                        EventIns obj = new EventIns();
                        obj.Id = int.Parse(item["docID"].ToString());// item.Field<int>("docID");
                        obj.Name = item["docName"].ToString();//("docName");
                        obj.Type = 3;
                        generalSearhLists.DocumentsList.Add(obj);
                    }
                }
                catch (Exception ex)
                {

                    //throw;
                }
                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(generalSearhLists);
            }
            catch (Exception ex)
            {

                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(new GeneralSearchLists() { });
            }
        }
        protected static DataTable LoadEvents(string str)
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            Hashtable parameters = new Hashtable();
            parameters.Add("@UserID", HttpContext.Current.Session["userId"]);
            parameters.Add("@SearchText", str);
            return c.GetDataAsDataTableFromSP("EventsSearch", parameters);
        }
        protected static DataTable LoadTasks(string str)
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            Hashtable parameters = new Hashtable();
            parameters.Add("@UserID", HttpContext.Current.Session["userId"]);
            parameters.Add("@lang", HttpContext.Current.Session["lang"]);
            parameters.Add("@SearchText", str);
            return c.GetDataAsDataTableFromSP("TasksSearch", parameters);
        }
        protected static DataTable LoadDocuments(string str)
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            Hashtable parameters = new Hashtable();
            parameters.Add("@UserID", HttpContext.Current.Session["userId"]);
            parameters.Add("@lang", HttpContext.Current.Session["lang"]);
            parameters.Add("@SearchText", str);
            return c.GetDataAsDataTableFromSP("DocumentsSearch", parameters);
        }
        [WebMethod]
        public static string LoadEventById(int id)
        {
            ImproperCalendarEvent tasksObj = new ImproperCalendarEvent();
            try
            {
                List<ImproperCalendarEvent> tasksList = new List<ImproperCalendarEvent>();
                foreach (CalendarEvent cevent in EventDAO.getEventsById(id))
                {

                    tasksList.Add(new ImproperCalendarEvent
                    {
                        id = cevent.id,
                        title = cevent.title,
                        start = String.Format("{0:s}", cevent.start),
                        end = String.Format("{0:s}", cevent.end),
                        description = cevent.description,
                        allDay = cevent.allDay,
                        backgroundColor = cevent.backgroundColor,
                        borderColor = cevent.backgroundColor,
                        color = cevent.backgroundColor,
                        CreatedBy = cevent.CreatedBy,

                    });
                    //idList.Add(cevent.id);
                }
                tasksObj = tasksList.FirstOrDefault();
                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(tasksList);
            }
            catch (Exception ex)
            {
                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(new ImproperCalendarEvent() { });
            }
        }
        [WebMethod]
        public static string fillLineChar()
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            List<SignatureTB> list = new List<SignatureTB>();
            try
            {
                //string query = "SELECT  Id, Signture, Documnet, UserId, Width, Height, [Top], [Left] FROM dbo.SignatureTB where Documnet='" + document.ToString() + "'";
                //DataTable dt = c.GetDataAsDataTable(query);
                for (int i = 0; i < lastDayOfMonth.Day; i++)
                {
                    var searchDate = firstDayOfMonth.AddDays(i);
                    int countFiles = int.Parse(c.GetDataAsScalar("select ISNULL(Count(documents.docID),0) as id from documents where Convert(date,documents.addedDate) =CONVERT(date,'" + searchDate.ToString("yyyy-MM-dd") + "')").ToString());
                    SignatureTB obj = new SignatureTB();
                    obj.Id = countFiles;
                    list.Add(obj);
                }
                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(list);
            }
            catch (Exception ex)
            {

                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(list);
            }
        }
        [WebMethod]
        public static string fillBarChar()
        {
            List<EventIns> list = new List<EventIns>();
            try
            {
                DataTable uploads = GetFoldersUploads();
                string folderName = "fldrName";
                if (HttpContext.Current.Session["lang"].ToString() == "1")
                { folderName = "fldrNameAr"; }
                for (Int32 j = 0; j < uploads.Rows.Count; j++)
                {

                    list.Add(new EventIns() { Id = int.Parse(uploads.Rows[j]["docsCount"].ToString()), Name = uploads.Rows[j][folderName].ToString(), Color = GetRandomeColor() });
                    //script.AppendLine("	{ label: \"" + uploads.Rows[j][folderName].ToString() + "\", y: " + uploads.Rows[j]["docsCount"].ToString() + " },");
                }
                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(list);
            }
            catch (Exception ex)
            {

                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(list);
            }
        }
        private static DataTable GetFoldersUploads()
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            string fldrs = "";
            DMS.DAL.operations op = new DMS.DAL.operations();
            tables.dbo.userFolders userDocs = new tables.dbo.userFolders();
            userDocs = op.dboGetAllUserFolders("userID=" + HttpContext.Current.Session["userID"].ToString());
            for (Int32 i = 0; i < userDocs.rowsCount; i++)
            {
                fldrs += userDocs.fieldFldrID.ToString() + ",";
                userDocs.moveNext();
            }
            fldrs = fldrs.Remove(fldrs.Length - 1);
            string folderName = "dbo.folders.fldrName";
            if (HttpContext.Current.Session["lang"].ToString() == "1")
            { folderName = "dbo.folders.fldrNameAr"; }
            DataTable res = new DataTable();
            string sql = "SELECT " + folderName + ", count(dbo.documents.docID) as docsCount"
                + " FROM dbo.folders INNER JOIN dbo.documents ON dbo.folders.fldrID = dbo.documents.fldrID"
                //+ " WHERE(dbo.folders.fldrID IN(" + fldrs + "))"
                + " group by " + folderName;
            res = c.GetDataAsDataTable(sql);
            return res;
        }

        private static string GetRandomeColor()
        {
            var random = new Random();
            var color = String.Format("#{0:X6}", random.Next(0x1000000));
            return color;
        }
        [WebMethod]
        public static string GetTodayTasks()
        {
            List<EventIns> eventIns = new List<EventIns>();
            try
            {
                int id = int.Parse(HttpContext.Current.Session["userID"].ToString());
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                //"and CONVERT(date,TaskDate)=" +DateTime.Now.ToString("yyyy-MM-dd")
                string datet = DateTime.Now.ToString("yyyy-MM-dd");
                string query = "select [Id],[TaskName],[TaskDate] from [dbo].[ToDoList] where CAST(TaskDate as date)='" + datet + "' and  [AssignTo]=" + id;
                DataTable dt = c.GetDataAsDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (var item in dt.AsEnumerable())
                    {
                        EventIns eventIns1 = new EventIns();
                        //UsersList obj = new UsersList();
                        //obj.Name = item.Field<string>("userName");
                        //list.Add(obj);
                        eventIns1.Id = item.Field<int>("Id");
                        eventIns1.Name = item.Field<string>("TaskName");
                        eventIns1.Start = item.Field<DateTime>("TaskDate").ToString("yyyy-MM-dd'T'HH:mm");
                        eventIns.Add(eventIns1);
                    }
                    JavaScriptSerializer jscript = new JavaScriptSerializer();
                    return jscript.Serialize(eventIns);
                }
                else
                {
                    JavaScriptSerializer jscript = new JavaScriptSerializer();
                    return jscript.Serialize(eventIns);
                }

            }
            catch (Exception ex)
            {
                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(eventIns);
            }
        }
        [WebMethod]
        public static string GetDocSignture(string ID)
        {

            List<UsersList> list = new List<UsersList>();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                string docId = c.decrypt(ID);
                string query = "SELECT DISTINCT dbo.users.userName FROM dbo.users INNER JOIN dbo.SignatureTB ON dbo.users.userID = dbo.SignatureTB.UserId where dbo.SignatureTB.Documnet='" + docId + "'";
                DataTable dt = c.GetDataAsDataTable(query);
                foreach (var item in dt.AsEnumerable())
                {
                    UsersList obj = new UsersList();
                    obj.Name = item.Field<string>("userName");
                    list.Add(obj);
                }
                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(list);
            }
            catch (Exception ex)
            {

                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(list);
            }
        }
        [WebMethod]
        public static string GetNewNotifications()
        {
            List<UsersList> list = new List<UsersList>();
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            string sql = "";
            DataTable dt = new DataTable();
            if (HttpContext.Current.Session["lang"].ToString() == "0")
            {
                sql = "select docID,'Reminder for document (' + docName + ') based on feild (' + metadesc + ')' as ReminderText ,beforedays  from dbo.userReminders where (isRemoved = 0 or isRemoved is null)  and  userid=" + HttpContext.Current.Session["userId"].ToString();
            }
            else
            {
                sql = "select docID,N'تذكير على المستند (' + docName + N') بناءً على الحقل (' + metadescAr + N')' as ReminderText,beforedays from dbo.userReminders where (isRemoved = 0 or isRemoved is null)  and userid=" + HttpContext.Current.Session["userId"].ToString();
            }
            dt = c.GetDataAsDataTable(sql);
            foreach (var item in dt.AsEnumerable())
            {
                UsersList obj = new UsersList();
                obj.Name = item.Field<string>("ReminderText");
                obj.Id = int.Parse(item.Field<string>("docID"));
                obj.beforedays = int.Parse(item.Field<string>("beforedays"));
                list.Add(obj);
            }
            JavaScriptSerializer jscript = new JavaScriptSerializer();
            return jscript.Serialize(list);

        }
        [WebMethod]
        public static string GetRecentUploads()
        {
            Int32 duration = 30;
            List<UsersList> list = new List<UsersList>();
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            DataTable res = new DataTable();
            res.Columns.Add("addedDate", typeof(DateTime));
            res.Columns.Add("docsCount", typeof(Int32));
            for (Int32 i = 0; i < duration; i++)
            {
                DateTime currDate = DateTime.Today.AddDays(i * -1);
                string SQL = "select count(docID) as docsCount from documents"
                    + " where CONVERT(date, addedDate) = '" + currDate.ToString("M/d/yyyy") + "'"
                    + " group by CONVERT(date, addedDate)";
                string strCount = Convert.ToString(c.GetDataAsScalar(SQL));
                if (string.IsNullOrEmpty(strCount)) strCount = "0";
                Int32 count = Convert.ToInt32(strCount);
                res.Rows.Add(currDate, count);
            }
            foreach (var item in res.AsEnumerable())
            {
                UsersList obj = new UsersList();
                // obj.Name = item.Field<string>("ReminderText");
                obj.Id = int.Parse(item.Field<string>("docsCount"));
                list.Add(obj);
            }
            JavaScriptSerializer jscript = new JavaScriptSerializer();
            return jscript.Serialize(list);

        }
        [WebMethod]
        public static string GetNewInbox()
        {

            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            string durationTypeQuery = ", ISNULL((SELECT durationType FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as durationType";
            string durationQuery = ",ISNULL((SELECT duration FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=documents.docID  and dbo.documentWFPath.actionType=0 )),-1) as duration";
            string sql = "SELECT     TOP (100) PERCENT dbo.documentWFPath.ID,dbo.docTypes.docTypDesc,dbo.docTypes.docTypDescAr, dbo.documents.docName, dbo.documentWFPath.receiveDate,dbo.documentWFPath.EndDate,dbo.documentWFPath.docID," +
                "CASE WHEN (dbo.documentWFPath.EndDate IS NOT NULL and dbo.documentWFPath.EndDate > GETDATE()) THEN 'black' ELSE CASE WHEN dbo.documentWFPath.EndDate IS NOT NULL THEN 'red' ELSE 'black' END END as 'Color'" +
                 durationTypeQuery +
                 durationQuery +
                ",documents.submitDate" +
                 ",documents.addedDate" +
                 ",documents.statusId" +
                " FROM dbo.documents INNER JOIN dbo.documentWFPath ON dbo.documents.docID = dbo.documentWFPath.docID INNER JOIN " +
                " dbo.docTypes ON dbo.documents.docTypID = dbo.docTypes.docTypID " +
                " WHERE     (dbo.documentWFPath.userID = " + HttpContext.Current.Session["userID"].ToString() + ") AND (dbo.documentWFPath.actionType = 0) and (isRemoved = 0 or isRemoved is null)" +
                " ORDER BY dbo.documentWFPath.receiveDate DESC";
            DataTable DT = new DataTable();
            DT = c.GetDataAsDataTable(sql);
            //fill inbox data
            List<InboxVM> inboxVM = new List<InboxVM>();
            foreach (DataRow row in DT.Rows)
            {
                InboxVM obj = new InboxVM();
                obj.ID = int.Parse(row["ID"].ToString());
                obj.Color = row["Color"].ToString();
                obj.docTypDesc = row["docTypDesc"].ToString();
                obj.docTypDescAr = row["docTypDescAr"].ToString();
                obj.receiveDate = DateTime.Parse(row["receiveDate"].ToString());
                obj.receiveDateStr = DateTime.Parse(row["receiveDate"].ToString()).ToShortDateString();
                obj.addedDate = row["addedDate"].ToString() != "" ? DateTime.Parse(row["addedDate"].ToString()) : DateTime.Now;
                obj.Color = row["Color"] != null ? row["Color"].ToString() : "";
                obj.docID = int.Parse(row["docID"].ToString());
                obj.docName = row["docName"] != null ? row["docName"].ToString() : "";
                obj.submitDate = row["submitDate"].ToString() != "" ? DateTime.Parse(row["submitDate"].ToString()) : DateTime.Parse(row["addedDate"].ToString());
                obj.durationType = int.Parse(row["durationType"].ToString());
                obj.duration = int.Parse(row["duration"].ToString());
                obj.isDelay = 0;
                if (obj.durationType != -1 && obj.duration != -1 && obj.duration > 0)
                {
                    int durationMuliplie = obj.durationType == 1 ? 1 : 24;
                    var totalHours = (obj.submitDate.Value.AddHours(obj.duration * durationMuliplie) - DateTime.Now).TotalHours;
                    if (totalHours > 0)
                    {
                        string h = HttpContext.Current.Session["lang"].ToString() == "0" ? "Hour" : "ساعة";
                        string remainTime = HttpContext.Current.Session["lang"].ToString() == "0" ? "the remaining time" : "الوقت المتبقي";
                        obj.LeftTime = remainTime + ": " + Math.Round(totalHours, 1).ToString() + h;
                        if (obj.durationType == 2)
                        {
                            h = HttpContext.Current.Session["lang"].ToString() == "0" ? "Day" : "يوم";
                            obj.LeftTime = Math.Round((totalHours / 24), 1).ToString() + h;
                        }
                        if (obj.durationType == 1 && totalHours < 1 && totalHours > 0)
                        {
                            h = HttpContext.Current.Session["lang"].ToString() == "0" ? "Minute" : "دقيقة";
                            obj.LeftTime = remainTime + ": " + Math.Round((totalHours * 60), 1).ToString() + h;
                        }
                    }
                    else
                    {
                        if (row["statusId"].ToString() != "2")
                        {
                            string h = HttpContext.Current.Session["lang"].ToString() == "0" ? "Hour" : "ساعة";
                            string remainTime = HttpContext.Current.Session["lang"].ToString() == "0" ? "the remaining time" : "الوقت المتبقي";
                            obj.LeftTime = remainTime + ": " + "0 " + h;
                            obj.statusName = HttpContext.Current.Session["lang"].ToString() == "0" ? "Late" : "متأخر";
                            if (obj.durationType == 2)
                            {
                                h = HttpContext.Current.Session["lang"].ToString() == "0" ? "Day" : "يوم";
                                obj.LeftTime = remainTime + ": " + "0 " + h;
                            }
                            //CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                            //c.NonQuery("update dbo.documents set statusId=3 where dbo.documents.docID=" + obj.docID);
                        }
                    }
                }
                else
                {
                    string remainTime = HttpContext.Current.Session["lang"].ToString() == "0" ? "the remaining time" : "الوقت المتبقي";
                    obj.LeftTime = remainTime + ": " + "∞";
                }
                inboxVM.Add(obj);
                //TextBox1.Text = row["ImagePath"].ToString();
            }
            string durationTypeQueryDelay = ", ISNULL((SELECT durationType FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPathDelayed.wfPathID from dbo.documentWFPath where dbo.documentWFPathDelayed.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPathDelayed.wfSeqNo from dbo.documentWFPath where dbo.documentWFPathDelayed.docID=documents.docID  and dbo.documentWFPathDelayed.actionType=0 )),-1) as durationType";
            string durationQueryDelay = ",ISNULL((SELECT duration FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPathDelayed.wfPathID from dbo.documentWFPath where dbo.documentWFPathDelayed.docID=documents.docID ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPathDelayed.wfSeqNo from dbo.documentWFPath where dbo.documentWFPathDelayed.docID=documents.docID  and dbo.documentWFPathDelayed.actionType=0 )),-1) as duration";
            //get delay documents
            string sqlDelay = "SELECT     TOP (100) PERCENT dbo.documentWFPathDelayed.ID,dbo.docTypes.docTypDesc,dbo.docTypes.docTypDescAr, dbo.documents.docName, dbo.documentWFPathDelayed.receiveDate,dbo.documentWFPathDelayed.EndDate,dbo.documentWFPathDelayed.docID,CASE WHEN (dbo.documentWFPathDelayed.EndDate IS NOT NULL and dbo.documentWFPathDelayed.EndDate > GETDATE()) THEN 'black' ELSE CASE WHEN dbo.documentWFPathDelayed.EndDate IS NOT NULL THEN 'red' ELSE 'black' END END as 'Color'" +
    durationTypeQueryDelay +
    durationQueryDelay +
   ",documents.submitDate" +
    ",documents.addedDate" +
    ",documents.statusId" +
   " FROM dbo.documents INNER JOIN dbo.documentWFPathDelayed ON dbo.documents.docID = dbo.documentWFPathDelayed.docID INNER JOIN  dbo.docTypes ON dbo.documents.docTypID = dbo.docTypes.docTypID" +
   " WHERE     (dbo.documentWFPathDelayed.userID = " + HttpContext.Current.Session["userID"].ToString() + ") AND (dbo.documentWFPathDelayed.actionType = 0) " +
   " ORDER BY dbo.documentWFPathDelayed.receiveDate DESC";
            DataTable DTDelay = new DataTable();
            DTDelay = c.GetDataAsDataTable(sqlDelay);
            foreach (DataRow row in DTDelay.Rows)
            {
                InboxVM obj = new InboxVM();
                obj.ID = int.Parse(row["ID"].ToString());
                obj.Color = row["Color"].ToString();
                obj.docTypDesc = "Late document";
                obj.docTypDescAr = "مستند متأخر";
                obj.receiveDate = DateTime.Parse(row["receiveDate"].ToString());
                obj.addedDate = row["addedDate"].ToString() != "" ? DateTime.Parse(row["addedDate"].ToString()) : DateTime.Now;
                obj.Color = row["Color"] != null ? row["Color"].ToString() : "";
                obj.docID = int.Parse(row["docID"].ToString());
                obj.docName = row["docName"] != null ? row["docName"].ToString() : "";
                obj.submitDate = row["submitDate"].ToString() != "" ? DateTime.Parse(row["submitDate"].ToString()) : DateTime.Parse(row["addedDate"].ToString());
                obj.durationType = int.Parse(row["durationType"].ToString());
                obj.duration = int.Parse(row["duration"].ToString());
                obj.isDelay = 1;
                if (obj.durationType != -1 && obj.duration != -1 && obj.duration > 0)
                {
                    int durationMuliplie = obj.durationType == 1 ? 1 : 24;
                    var totalHours = (obj.submitDate.Value.AddHours(obj.duration * durationMuliplie) - DateTime.Now).TotalHours;
                    if (totalHours > 0)
                    {
                        string h = HttpContext.Current.Session["lang"].ToString() == "0" ? "Hour" : "ساعة";
                        string remainTime = HttpContext.Current.Session["lang"].ToString() == "0" ? "the remaining time" : "الوقت المتبقي";
                        obj.LeftTime = remainTime + ": " + Math.Round(totalHours, 1).ToString() + h;
                        if (obj.durationType == 2)
                        {
                            h = HttpContext.Current.Session["lang"].ToString() == "0" ? "Day" : "يوم";
                            obj.LeftTime = remainTime + ": " + Math.Round((totalHours / 24), 1).ToString() + h;
                        }
                        if (obj.durationType == 1 && totalHours < 1 && totalHours > 0)
                        {
                            h = HttpContext.Current.Session["lang"].ToString() == "0" ? "Minute" : "دقيقة";
                            obj.LeftTime = remainTime + ": " + Math.Round((totalHours * 60), 1).ToString() + h;
                        }
                    }
                    else
                    {
                        if (row["statusId"].ToString() != "2")
                        {
                            string h = HttpContext.Current.Session["lang"].ToString() == "0" ? "Hour" : "ساعة";
                            string remainTime = HttpContext.Current.Session["lang"].ToString() == "0" ? "the remaining time" : "الوقت المتبقي";
                            obj.LeftTime = remainTime + ": " + "0 " + h;
                            obj.statusName = HttpContext.Current.Session["lang"].ToString() == "0" ? "Late" : "متأخر";
                            if (obj.durationType == 2)
                            {
                                h = HttpContext.Current.Session["lang"].ToString() == "0" ? "Day" : "يوم";
                                //obj.LeftTime = Math.Round((totalHours / 24), 1).ToString() + h;
                                obj.LeftTime = remainTime + ": " + "0 " + h;
                            }
                            //CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                            //c.NonQuery("update dbo.documents set statusId=3 where dbo.documents.docID=" + obj.docID);
                        }
                    }
                }
                else
                {
                    string remainTime = HttpContext.Current.Session["lang"].ToString() == "0" ? "the remaining time" : "الوقت المتبقي";
                    obj.LeftTime = remainTime + ": " + "∞";
                }
                inboxVM.Add(obj);
                //TextBox1.Text = row["ImagePath"].ToString();
            }
            inboxVM = inboxVM.OrderByDescending(x => x.receiveDate).ToList();
            JavaScriptSerializer jscript = new JavaScriptSerializer();
            return jscript.Serialize(inboxVM);

        }
        [WebMethod]
        public static string CheckNewTasks()
        {
            EventIns eventIns = new EventIns();
            try
            {
                int id = int.Parse(HttpContext.Current.Session["userID"].ToString());
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                string query = "select [Id],[TaskName] from [dbo].[ToDoList] where [AssignTo]=" + id;
                DataTable dt = c.GetDataAsDataTable(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (var item in dt.AsEnumerable())
                    {
                        //UsersList obj = new UsersList();
                        //obj.Name = item.Field<string>("userName");
                        //list.Add(obj);
                        eventIns.Id = item.Field<int>("Id");
                        eventIns.Name = item.Field<string>("TaskName");
                    }
                    JavaScriptSerializer jscript = new JavaScriptSerializer();
                    return jscript.Serialize(eventIns);
                }
                else
                {
                    eventIns.Id = 0;
                    JavaScriptSerializer jscript = new JavaScriptSerializer();
                    return jscript.Serialize(eventIns);
                }

            }
            catch (Exception ex)
            {
                eventIns.Id = 0;
                JavaScriptSerializer jscript = new JavaScriptSerializer();
                return jscript.Serialize(eventIns);
            }
        }
        [WebMethod]
        public static string DeleteSigntures(int id)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                //Example 5
                if (id != 0)
                {
                    int x = c.NonQuery("Delete  SignatureTB where Id=" + id);
                    return serializer.Serialize("true");
                }
                else
                {
                    return serializer.Serialize("false");
                }
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }
        [WebMethod]
        public static string DeleteBarcode(int id)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                //Example 5
                if (id != 0)
                {
                    int x = c.NonQuery("Delete  DocumentLablesTB where Id=" + id);
                    return serializer.Serialize("true");
                }
                else
                {
                    return serializer.Serialize("false");
                }
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }
        [WebMethod]
        public static string DeleteFileVersion(int id, int version)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                //Example 5
                if (id != 0)
                {
                    string ext = c.GetDataAsScalar("Select ext From documentVersions where docID=" + id + "and version=" + version).ToString();
                    int x = c.NonQuery("Delete  documentVersions where docID=" + id + "and version=" + version);
                    string Deletefile = Helper.GetUploadDiskPath() + id.ToString() + "-" + version.ToString() + "." + ext;
                    if (File.Exists(Deletefile))
                    {
                        File.Delete(Deletefile);
                    }
                    string Deletefilepdf = Helper.GetUploadDiskPath() + id.ToString() + "-" + version.ToString() + ".pdf";
                    if (File.Exists(Deletefilepdf))
                    {
                        File.Delete(Deletefilepdf);
                    }
                    string DeleteTempfile = Helper.GetTempDiskPath() + id.ToString() + "-" + version.ToString() + ".pdf";
                    if (File.Exists(DeleteTempfile))
                    {
                        File.Delete(DeleteTempfile);
                    }
                    return serializer.Serialize("true");
                }
                else
                {
                    return serializer.Serialize("false");
                }
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }

        [WebMethod]
        public static string RemoveNotifyItem(int id)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                //Example 5
                if (id != 0)
                {
                    if (id != -1)
                    {
                        int x = c.NonQuery("Update usersRemiders set isRemoved=1 where reminderID=" + id);
                    }
                    else
                    {
                        int x = c.NonQuery("Update usersRemiders set isRemoved=1");
                    }
                    return serializer.Serialize("true");
                }
                else
                {
                    return serializer.Serialize("false");
                }
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }
        [WebMethod]
        public static string RemoveInboxItem(int id)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                //Example 5
                if (id != 0)
                {
                    if (id != -1)
                    {
                        int x = c.NonQuery("Update documentWFPath set isRemoved=1 where ID=" + id);
                    }
                    else
                    {
                        int x = c.NonQuery("Update documentWFPath set isRemoved=1");
                    }
                    return serializer.Serialize("true");
                }
                else
                {
                    return serializer.Serialize("false");
                }
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }
        [WebMethod]
        public static string UpdateSize(int width, int height, int id)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                //Example 5
                if (id != 0)
                {
                    int x = c.NonQuery("Update  SignatureTB Set Width=" + width + ",Height=" + height + " where Id=" + id);
                    return serializer.Serialize("true");
                }
                else
                {
                    return serializer.Serialize("false");
                }
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }

        [WebMethod]
        public static string UpdateSizeBarcode(int width, int height, int id)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                //Example 5
                if (id != 0)
                {
                    int x = c.NonQuery("Update  DocumentLablesTB Set Width=" + width + ",Height=" + height + " where Id=" + id);
                    return serializer.Serialize("true");
                }
                else
                {
                    return serializer.Serialize("false");
                }
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }
        //// get Start Time and end time with delimiter
        //[WebMethod]
        //public static string GetStartTime(int ID)
        //{
        //    ScopeMasterEntities db = new ScopeMasterEntities();
        //    //List<ObjetX> list = new List<ObjetX>();
        //    var data = db.Clinics.Where(p => p.ClinicID == 1).ToList();
        //    List<ObjetXTime> list = new List<ObjetXTime>();
        //    //var data = db.Procedures.Where(p => p.ClinicID == ID).ToList();
        //    int v = data.Count();
        //   // int i = 0;

        //        ObjetXTime objst = new ObjetXTime();
        //        objst.StartTime = data.First().StartTime.ToString();
        //        objst.EndTime = data.First().EndTime.ToString();
        //        objst.Delimiter = data.First().Delimiter.ToString();
        //        list.Insert(0, objst);


        //    JavaScriptSerializer jscript = new JavaScriptSerializer();
        //    return jscript.Serialize(list);

        //}
        //// get Start Time and end time with delimiter
        //[WebMethod]
        //public static string GetColorTime(string DateT)
        //{
        //    ScopeMasterEntities db = new ScopeMasterEntities();
        //    DateTime dt=new DateTime();
        //    dt=DateTime.Parse(DateT);
        //    //List<ObjetX> list = new List<ObjetX>();
        //    var data = from p in db.Reservations
        //               where p.ReserveJoinDate == dt && p.Active ==1
        //               select new {p.ReserveJoinTime };
        //    List<ObjetRservTime> list = new List<ObjetRservTime>();
        //    //var data = db.Procedures.Where(p => p.ClinicID == ID).ToList();
        //    int i = 0;
        //    foreach (var item in data)
        //    {
        //        ObjetRservTime objst = new ObjetRservTime();
        //        objst.RevTime = item.ReserveJoinTime.ToString(@"hh\:mm");
        //        list.Insert(i, objst);
        //        i++;
        //    }
        //    int v = data.Count();
        //    // int i = 0;
        //    JavaScriptSerializer jscript = new JavaScriptSerializer();
        //    return jscript.Serialize(list);
        //}
        //// get Resev By id to update
        //[WebMethod]
        //public static string GetReservedByID(int id)
        //{
        //    ScopeMasterEntities db = new ScopeMasterEntities();
        //    //List<ObjetX> list = new List<ObjetX>();
        //    var data = db.Reservations.Find(id);
        //    var data2 = db.Patients.Find(int.Parse(data.PatientID.ToString()));
        //    List<Reservations> list = new List<Reservations>();
        //    //var data = db.Procedures.Where(p => p.ClinicID == ID).ToList();
        //    int i = 0;
        //   Reservations obj = new Reservations();
        //   obj.ClinicID =data.ClinicID;
        //   obj.ReserveJoinDate = data.ReserveJoinDate.ToString("yyyy-MM-dd");
        //   obj.ReserveJoinTime = data.ReserveJoinTime.ToString(@"hh\:mm");
        //   obj.ReserveEndTime = data.ReserveEndTime.ToString(@"hh\:mm");
        //   obj.RoomID = int.Parse(data.RoomID.ToString());
        //   obj.ProcedureID = int.Parse(data.ProcedureID.ToString());
        //   obj.PatientID = int.Parse(data.PatientID.ToString());
        //   obj.PTNAME = data2.FirstName + " " + data2.MiddleName + " " + data2.LastName;
        //   list.Insert(i, obj);


        // //   int v = data.Count();
        //    // int i = 0;
        //    JavaScriptSerializer jscript = new JavaScriptSerializer();
        //    return jscript.Serialize(list);
        //}
        //// get Start Time and end time with delimiter
        //[WebMethod]
        //public static string GetAllPT()
        //{
        //    ScopeMasterEntities db = new ScopeMasterEntities();
        //    var data = db.Patients.ToList();
        //    List<objectPT> list = new List<objectPT>();
        //    //var data = db.Procedures.Where(p => p.ClinicID == ID).ToList();
        //    int i = 0;
        //    foreach (var item in data)
        //    {
        //        objectPT obj = new objectPT();
        //        obj.id = item.PatientID;
        //        obj.NAME = item.FirstName + ' ' + item.MiddleName + ' ' + item.LastName;
        //        obj.Gender = item.Gender.ToString();
        //       // obj.NAME=item.d
        //       // objst.RevTime = item.ReserveJoinTime.ToString(@"hh\:mm");
        //        list.Insert(i, obj);
        //        i++;
        //    }
        //    int v = data.Count();
        //    // int i = 0;
        //    JavaScriptSerializer jscript = new JavaScriptSerializer();
        //    return jscript.Serialize(list);
        //}
        [WebMethod]
        public static string FeedbackMessage(string img, string note, string url)
        {
            var serializer = new JavaScriptSerializer();
            DMS.DAL.Feedbacks feedback = new DMS.DAL.Feedbacks();
            try
            {
                int userID = int.Parse(HttpContext.Current.Session["userID"].ToString());
                int feedbackID = feedback.Save(userID.ToString(), note, url);
                string filePath = HttpContext.Current.Server.MapPath("/") + "Images/Feedbacks/" + feedbackID.ToString() + ".png";
                if (!String.IsNullOrEmpty(img))
                {
                    System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(img.Replace("data:image/png;base64,", "")));
                }
                CommonFunction.clsCommon c = new CommonFunction.clsCommon();
                DMS.DAL.operations op = new DMS.DAL.operations();
                tables.dbo.settings settings = op.dboGetSettingsByPrimaryKey(1);
                op = new DMS.DAL.operations();
                tables.dbo.users user = new tables.dbo.users();
                user = op.dboGetUsersByPrimaryKey(c.convertToInt32(userID));
                System.Net.Mail.SmtpClient M = new System.Net.Mail.SmtpClient(settings.fieldOutgoingMailServer);
                M.Credentials = new System.Net.NetworkCredential(settings.fieldSystemEmail, c.decrypt(settings.fieldSystemEmailPassword));
                System.Net.Mail.MailMessage MailMsg = new System.Net.Mail.MailMessage();
                MailMsg.From = new System.Net.Mail.MailAddress(user.fieldEmail, user.fieldFullName);
                MailMsg.To.Add(new System.Net.Mail.MailAddress("support@myhudhud.com"));
                MailMsg.IsBodyHtml = false;
                MailMsg.Body = note;
                MailMsg.Attachments.Add(new System.Net.Mail.Attachment(filePath));
                try
                {
                    M.Send(MailMsg);
                }
                catch (Exception ex)
                { }
                return serializer.Serialize("true");
            }
            catch (Exception ex)
            {
                return serializer.Serialize("false");
            }
        }
        #region web Methods Used Objects
        // object return date
        public class ObjetX
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
        // object for time
        public class ObjetXTime
        {
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string Delimiter { get; set; }
        }
        public class ObjetRservTime
        {
            public string RevTime { get; set; }
        }
        // object for patient to select
        public class objectPT
        {
            public long id { get; set; }
            public string NAME { get; set; }
            public string Gender { get; set; }
        }
        public class UsersList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool isRead { get; set; }
            public bool isEdit { get; set; }
            public int beforedays { get; set; }
        }
        public class MetaList
        {
            public int Id { get; set; }
            public string Index { get; set; }
            //public int Type { get; set; }
            //public bool isRead { get; set; }
            //public bool isEdit { get; set; }
        }
        public class SignatureTB
        {
            public int Id { get; set; }
            public string Signture { get; set; }
            public string Lable { get; set; }
            public string Transform { get; set; }

            public string Documnet { get; set; }
            public int UserId { get; set; }

            public string Width { get; set; }
            public string Height { get; set; }
            public int Sort { get; set; }
            public string Top { get; set; }
            public string Left { get; set; }
        }
        public class MetaTB
        {
            public int metaID { get; set; }
            public string metaDesc { get; set; }
            public string metaDataType { get; set; }
            public bool required { get; set; }
            public int ctrlID { get; set; }
            public string defaultTexts { get; set; }
            public string defaultValues { get; set; }
            public bool visible { get; set; }
            public string metaDescAr { get; set; }
        }
        public class Reservations
        {
            public int ClinicID { get; set; }
            public int OrganizationID { get; set; }
            public long ReserveID { get; set; }
            public string ReserveJoinDate { get; set; }
            public string ReserveJoinTime { get; set; }
            public string ReserveEndTime { get; set; }
            public long PatientID { get; set; }
            public int RoomID { get; set; }
            public int DoctorID { get; set; }
            public int ProcedureID { get; set; }
            public string ReserveTime { get; set; }
            public double Active { get; set; }
            public string PTNAME { get; set; }
        }

        #endregion
    }
    public class EventIns
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Start { get; set; }
        public int Type { get; set; }
    }
    public class GeneralSearchLists
    {
        public List<EventIns> EvenetsList { get; set; }
        public List<EventIns> DocumentsList { get; set; }
        public List<EventIns> TasksList { get; set; }
    }
}