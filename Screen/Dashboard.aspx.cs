using dms.DTOS;
using dms.VM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Screen
{
    public partial class Dashboard : System.Web.UI.Page
    {
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = "";
            DataTable DT = new DataTable();
            #region Inbox
            //string sql = "SELECT     TOP (100) PERCENT dbo.documentWFPath.ID,dbo.docTypes.docTypDesc, dbo.documents.docName, dbo.documentWFPath.receiveDate,dbo.documentWFPath.docID " +
            //   " FROM dbo.documents INNER JOIN dbo.documentWFPath ON dbo.documents.docID = dbo.documentWFPath.docID INNER JOIN " +
            //   " dbo.docTypes ON dbo.documents.docTypID = dbo.docTypes.docTypID " +
            //   " WHERE     (dbo.documentWFPath.userID = " + Session["userID"].ToString() + ") AND (dbo.documentWFPath.actionType = 0) " +
            //   " ORDER BY dbo.documentWFPath.receiveDate DESC";
            //DataTable DT = new DataTable();
            //DT = c.GetDataAsDataTable(sql);

            //rptInbox.DataSource = DT;
            //rptInbox.DataBind();
            fillInbox();
            //fillTodo();
            //if (DT.Rows.Count == 0)
            //{
            //    pnlEmptyInbox.Visible = true;
            //    pnlInbox.Visible = false;
            //}
            #endregion

            #region recently uploaded
            sql = "SELECT TOP (3)  documents.*, folders.fldrName,folders.fldrNameAr FROM     documents INNER JOIN folders ON documents.fldrID = folders.fldrID where docExt != '' order by addedDate desc";
            DT = new DataTable();
            DT = c.GetDataAsDataTable(sql);
            List<LastFile> lst = new List<LastFile>();
            foreach (var item in DT.AsEnumerable())
            {
                if (item.Field<string>("docExt").ToString().ToLower() == "pdf")
                {
                    lst.Add(new LastFile() { docId = c.encrypt(item.Field<long>("docID").ToString()), docName = item.Field<string>("docName"), docLink = "/PdfLauncher.aspx?docID=" + c.encrypt(item.Field<long>("docID").ToString()) + "&ver=" + item.Field<short>("lastVersion") + "&userID=" + Session["userID"].ToString(), docDate = item.Field<DateTime>("addedDate").ToString(), fldrName = item.Field<string>("fldrName"), fldrNameAr = item.Field<string>("fldrNameAr") });
                }
                else
                {
                    lst.Add(new LastFile() { docId = c.encrypt(item.Field<long>("docID").ToString()), docName = item.Field<string>("docName"), docLink = "javascript:parent.showDialog(195, '" + item.Field<string>("docName") + "', '../Screen/showDocument.aspx?docID=" + c.encrypt(item.Field<long>("docID").ToString()) + "&amp;ver=" + item.Field<short>("lastVersion") + "&amp;', 1300, 700)", docDate = item.Field<DateTime>("addedDate").ToString(), fldrName = item.Field<string>("fldrName"), fldrNameAr = item.Field<string>("fldrNameAr") });
                }
            }
            rptUploads.DataSource = lst;
            rptUploads.DataBind();
            if (DT.Rows.Count == 0)
            {
                pnlEmptyUploads.Visible = true;
                pnlUploads.Visible = false;
            }
            #endregion
            #region fill In Out Come
            sql = "SELECT TOP (3) * from documents where docExt != '' and typeId in(1,2) order by addedDate desc";
            DT = new DataTable();
            DT = c.GetDataAsDataTable(sql);
            List<LastFile> lst2 = new List<LastFile>();
            foreach (var item in DT.AsEnumerable())
            {
                if (item.Field<string>("docExt").ToString().ToLower() == "pdf")
                {
                    lst2.Add(new LastFile() { docId = c.encrypt(item.Field<long>("docID").ToString()), docName = item.Field<string>("docName"), docLink = "/PdfLauncher.aspx?docID=" + c.encrypt(item.Field<long>("docID").ToString()) + "&ver=" + item.Field<short>("lastVersion") + "&userID=" + Session["userID"].ToString(), docDate = item.Field<DateTime>("addedDate").ToString() });
                }
                else
                {
                    lst2.Add(new LastFile() { docId = c.encrypt(item.Field<long>("docID").ToString()), docName = item.Field<string>("docName"), docLink = "javascript:parent.showDialog(195, '" + item.Field<string>("docName") + "', '../Screen/showDocument.aspx?docID=" + c.encrypt(item.Field<long>("docID").ToString()) + "&amp;ver=" + item.Field<short>("lastVersion") + "&amp;', 1300, 700)", docDate = item.Field<DateTime>("addedDate").ToString() });
                }
            }
            rptInOutCome.DataSource = lst2;
            rptInOutCome.DataBind();
            if (DT.Rows.Count == 0)
            {
                pnlEmptyInOutCome.Visible = true;
                pnlInOutCome.Visible = false;
            }
            spnInOutCount.InnerHtml = DT.Rows.Count.ToString();
            #endregion

            #region fillChart1


            StringBuilder script = new StringBuilder();
            #region First chart repo - deprecated chart
            //DataTable recent = GetRecentUploads(30);
            ////StringBuilder script = new StringBuilder();
            //string str = "<script type='text/javascript'>";
            //str += "$(function () {";
            //str += "var chart = new CanvasJS.Chart('chartContainer', { animationEnabled: true,height:200, exportEnabled: true,axisY:{gridThickness: 0.2,tickLength: 5,margin:20 }, theme: 'light1', data: [{ type: 'column', indexLabelFontColor: '#5A5757', indexLabelPlacement: 'outside', dataPoints: [ ";
            //for (Int32 j = 0; j < 10; j++)
            //{
            //    str += "	{ x: \"" + Convert.ToDateTime(recent.Rows[j]["addedDate"]).ToString("d-MMM") + "\", y: " + recent.Rows[j]["docsCount"].ToString() + " },";
            //}
            //str += "] }] }); chart.render();";
            //str += "});";
            //str += "</script>";
            //script.AppendLine(str);
            //ltrScript.Text = script.ToString();
            #endregion
            #endregion
            #region fillChart2  - deprecated chart
            // DataTable uploads = GetFoldersUploads();
            lblparchartcount.InnerHtml = c.GetDataAsScalar("SELECT count(documents.docID) FROM     documents INNER JOIN folders ON documents.fldrID = folders.fldrID INNER JOIN userFolders ON documents.fldrID = userFolders.fldrID where userFolders.userID="+int.Parse(Session["userID"].ToString()) +" ").ToString(); ;
            ltrScript2.Text = script.ToString();
            #endregion
            lblWelcomeDashBoad.InnerHtml = c.getUserName(c.convertToInt32(Session["userId"]));
            FillLatestTasks();
            // check inbox perm
            string permisstionId = c.GetDataAsScalar("select top 1 programID from dbo.userPrograms where userID=" + Session["userID"].ToString() + " and programID=6").ToString();
            if (permisstionId == "")
            {
                divpnlInbox.Visible = false;
            }
            //check folders perm
            string folderpermisstionId = c.GetDataAsScalar("select top 1 programID from dbo.userPrograms where userID=" + Session["userID"].ToString() + " and programID=1").ToString();
            if (folderpermisstionId == "")
            {
                divpnlFolder.Visible = false;
            }
        }
        public void fillInbox()
        {
            string sql = "SELECT     TOP (100) PERCENT dbo.documentWFPath.ID,dbo.docTypes.docTypDesc,dbo.docTypes.docTypDescAr, dbo.documents.docName, dbo.documentWFPath.receiveDate,dbo.documentWFPath.EndDate,dbo.documentWFPath.docID," +
                "CASE WHEN (dbo.documentWFPath.EndDate IS NOT NULL and dbo.documentWFPath.EndDate > GETDATE()) THEN 'black' ELSE CASE WHEN dbo.documentWFPath.EndDate IS NOT NULL THEN 'red' ELSE 'black' END END as 'Color'" +
                ",documents.submitDate" +
                 ",documents.addedDate" +
                 ",documents.statusId" +
                " FROM dbo.documents INNER JOIN dbo.documentWFPath ON dbo.documents.docID = dbo.documentWFPath.docID INNER JOIN " +
                " dbo.docTypes ON dbo.documents.docTypID = dbo.docTypes.docTypID " +
                " WHERE     (dbo.documentWFPath.userID = " + Session["userID"].ToString() + ") AND (dbo.documentWFPath.actionType = 0) " +
                " ORDER BY dbo.documentWFPath.receiveDate DESC";
            DataTable DT = new DataTable();
            DT = c.GetDataAsDataTable(sql);
            //fill inbox data
            string Culture = (Session["lang"].ToString() == "0") ? "en-US" : "ar-AE";
            List<InboxVM> inboxVM = new List<InboxVM>();
            foreach (DataRow row in DT.Rows)
            {
                if (row["statusId"].ToString() != "2")
                {
                    var dt = c.GetDataAsDataTable("SELECT top 2 documentVersions.docID, documentVersions.version, documentVersions.addedDate, documentVersions.addedUserID, documentVersions.ext ,documentVersions.DocumentFileName, documents.docName FROM     documentVersions INNER JOIN documents ON documentVersions.docID = documents.docID where documentVersions.docID= " + row["docID"].ToString().ToString());
                    string count = c.GetDataAsScalar("select ISNULL(count(documentVersions.docID),0) as countros FROM     documentVersions where docID=" + row["docID"].ToString()).ToString();
                    InboxVM obj = new InboxVM();
                    obj.Culture = Culture;
                    obj.ID = int.Parse(row["ID"].ToString());
                    obj.docTypDesc = row["docTypDesc"].ToString();
                    obj.docTypDescAr = row["docTypDescAr"].ToString();
                    obj.receiveDate = DateTime.Parse(row["receiveDate"].ToString());
                    obj.docID = int.Parse(row["docID"].ToString());
                    obj.docName = row["docName"] != null ? row["docName"].ToString() : "";
                    obj.isDelay = 0;
                    obj.versionCount = int.Parse(count);
                    obj.FilesList = dt;
                    inboxVM.Add(obj);
                }
                //TextBox1.Text = row["ImagePath"].ToString();
            }

            //get delay documents
            string sqlDelay = "SELECT     TOP (100) PERCENT dbo.documentWFPathDelayed.ID,dbo.docTypes.docTypDesc,dbo.docTypes.docTypDescAr, dbo.documents.docName, dbo.documentWFPathDelayed.receiveDate,dbo.documentWFPathDelayed.EndDate,dbo.documentWFPathDelayed.docID,CASE WHEN (dbo.documentWFPathDelayed.EndDate IS NOT NULL and dbo.documentWFPathDelayed.EndDate > GETDATE()) THEN 'black' ELSE CASE WHEN dbo.documentWFPathDelayed.EndDate IS NOT NULL THEN 'red' ELSE 'black' END END as 'Color'" +
   ",documents.submitDate" +
    ",documents.addedDate" +
    ",documents.statusId" +
   " FROM dbo.documents INNER JOIN dbo.documentWFPathDelayed ON dbo.documents.docID = dbo.documentWFPathDelayed.docID INNER JOIN  dbo.docTypes ON dbo.documents.docTypID = dbo.docTypes.docTypID" +
   " WHERE     (dbo.documentWFPathDelayed.userID = " + Session["userID"].ToString() + ") AND (dbo.documentWFPathDelayed.actionType = 0) " +
   " ORDER BY dbo.documentWFPathDelayed.receiveDate DESC";
            DataTable DTDelay = new DataTable();
            DTDelay = c.GetDataAsDataTable(sqlDelay);
            foreach (DataRow row in DTDelay.Rows)
            {
                if (row["statusId"].ToString() != "2")
                {
                 
                        var dt = c.GetDataAsDataTable("SELECT top 2 documentVersions.docID, documentVersions.version, documentVersions.addedDate, documentVersions.addedUserID, documentVersions.ext ,documentVersions.DocumentFileName, documents.docName FROM     documentVersions INNER JOIN documents ON documentVersions.docID = documents.docID where documentVersions.docID= " + row["docID"].ToString().ToString());
                        string count = c.GetDataAsScalar("select ISNULL(count(documentVersions.docID),0) as countros FROM     documentVersions where docID=" + row["docID"].ToString()).ToString();
                        InboxVM obj = new InboxVM();
                        obj.Culture = Culture;
                        obj.ID = int.Parse(row["ID"].ToString());
                        obj.docTypDesc = "Late document -";
                        obj.docTypDescAr = "مسستند متاخر -";
                        obj.receiveDate = DateTime.Parse(row["receiveDate"].ToString());
                        obj.docID = int.Parse(row["docID"].ToString());
                        obj.docName = row["docName"] != null ? row["docName"].ToString() : "";
                        obj.isDelay = 1;
                        obj.versionCount = int.Parse(count);
                        obj.FilesList = dt;
                        inboxVM.Add(obj);
                }
                //TextBox1.Text = row["ImagePath"].ToString();
            }
            spnInboxCount.InnerHtml = inboxVM.Count().ToString();
            inboxVM = inboxVM.OrderByDescending(x => x.receiveDate).Skip(0).Take(3).ToList();
            //bind inbox data
            rptInbox.DataSource = inboxVM;
            rptInbox.DataBind();
            if (inboxVM.Count == 0)
            {
                pnlEmptyInbox.Visible = true;
                pnlInbox.Visible = false;
            }
        }

        public void fillTodo()
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string query = "SELECT todoList.[Id], todoList.[Description], createdbyUser.fullName as 'CreatedBy', assignToUser.fullName as 'AssignTo', todoList.[TaskDate] as 'TaskDate', todoList.[TaskName] as 'TaskName', taskType.ArText as 'TaskType', todoList.IsComplete,case when (CAST(todoList.TaskDate as Date) >= CAST('" + currentDate + "' as Date)) Then 'قيد الانتظار' ELSE 'متأخرة' END AS StsAr,case when (CAST(todoList.TaskDate as Date) >= CAST('" + currentDate + "' as Date)) Then 'Pending' ELSE 'Late' END AS StsEn FROM [dbo].[ToDoList] as todoList INNER JOIN [dbo].[TaskTypes] taskType on todoList.TaskType = taskType.Id LEFT JOIN [dbo].[users] as createdbyUser on todoList.CreatedBy = createdbyUser.[userID] LEFT JOIN [dbo].[users] as assignToUser on todoList.AssignTo = assignToUser.[userID]";
            query += "Where  (ToDoList.CreatedBy = " + Session["userID"].ToString() + " or ToDoList.AssignTo = " + Session["userID"].ToString() + ") and todoList.IsComplete <> 1";
            var gridList = new List<GridViewItem>();
            var gvToDoListResult = c.GetDataAsDataTable(query);
            for (int i = 0; i < gvToDoListResult.Rows.Count; i++)
            {
                gridList.Add(new GridViewItem
                {
                    Id = Convert.ToInt32(gvToDoListResult.Rows[i]["Id"].ToString()),
                    IsComplete = Convert.ToBoolean(gvToDoListResult.Rows[i]["IsComplete"].ToString()),
                    AssignTo = gvToDoListResult.Rows[i]["AssignTo"].ToString(),
                    CreatedBy = gvToDoListResult.Rows[i]["CreatedBy"].ToString(),
                    TaskDate = Convert.ToDateTime(gvToDoListResult.Rows[i]["TaskDate"].ToString()).ToString(),
                    TaskName = gvToDoListResult.Rows[i]["TaskName"].ToString(),
                    TaskTime = Convert.ToDateTime(gvToDoListResult.Rows[i]["TaskDate"].ToString()).ToShortTimeString(),
                    TaskType = gvToDoListResult.Rows[i]["TaskType"].ToString(),
                    Description = gvToDoListResult.Rows[i]["Description"].ToString(),
                    StsAr = gvToDoListResult.Rows[i]["StsAr"].ToString(),
                    StsEn = gvToDoListResult.Rows[i]["StsEn"].ToString()

                });
            }
            int gridCount = gridList.Count;
            rptTodoList.DataSource = gridList.OrderByDescending(x => x.Id).Skip(0).Take(3).ToList();
            rptTodoList.DataBind();
            if (gvToDoListResult.Rows.Count == 0)
            {
                pnlEmptyTasks.Visible = true;
                PnlTodo.Visible = false;
            }
            tasksListcount.InnerHtml = gridCount.ToString();
        }
        private DataTable GetRecentUploads(Int32 duration)
        {
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

            return res;
        }

        private DataTable GetFoldersUploads()
        {
            string fldrs = "";
            DMS.DAL.operations op = new DMS.DAL.operations();
            tables.dbo.userFolders userDocs = new tables.dbo.userFolders();
            userDocs = op.dboGetAllUserFolders("userID=" + Session["userID"].ToString());
            for (Int32 i = 0; i < userDocs.rowsCount; i++)
            {
                fldrs += userDocs.fieldFldrID.ToString() + ",";
                userDocs.moveNext();
            }
            fldrs = fldrs.Remove(fldrs.Length - 1);
            string folderName = "dbo.folders.fldrName";
            if (Session["lang"].ToString() == "1")
            { folderName = "dbo.folders.fldrNameAr"; }
            DataTable res = new DataTable();
            string sql = "SELECT " + folderName + ", count(dbo.documents.docID) as docsCount"
                + " FROM dbo.folders INNER JOIN dbo.documents ON dbo.folders.fldrID = dbo.documents.fldrID"
                //+ " WHERE(dbo.folders.fldrID IN(" + fldrs + "))"
                + " group by " + folderName;
            res = c.GetDataAsDataTable(sql);
            return res;
        }

        public void FillLatestTasks()
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            DataTable dt = c.GetDataAsDataTable("SELECT top 3 [Id] ,[TaskName] ,[TaskDate] ,[AssignTo] ,[CreatedBy] ,[TaskType] ,[CreatedOn] ,[IsComplete] ,[IsDeleted] ,[Description],case when (CAST(todoList.TaskDate as Date) >= CAST('" + currentDate + "' as Date)) Then N'قيد الانتظار' ELSE N'متأخرة' END AS StsAr,case when (CAST(todoList.TaskDate as Date) >= CAST('" + currentDate + "' as Date)) Then 'Pending' ELSE 'Late' END AS StsEn  FROM [dbo].[ToDoList] WHERE [AssignTo] =" + Convert.ToInt32(Session["userId"]) + " and [IsDeleted]=0 and [IsComplete]=0 and  todoList.IsComplete <> 1 order by [Id] desc");
            if (dt.Rows.Count > 0)
            {
                rptTodoList.Visible = true;
                rptTodoList.DataSource = dt;
                rptTodoList.DataBind();
                //lbltable.Text = "";
            }
            else
            {
                //lbltable.Text = (Session["lang"].ToString() == "0") ? "No tasks available" : "لا توجد مهمات";
                //lbltable.ForeColor = Color.Red;
                //tbllatestasks.Visible = false;
            }
            if (dt.Rows.Count == 0)
            {
                pnlEmptyTasks.Visible = true;
                PnlTodo.Visible = false;
            }
            tasksListcount.InnerHtml = dt.Rows.Count.ToString();
        }
    }
    public class LastFile
    {
        public string docId { get; set; }
        public string docName { get; set; }
        public string docDate { get; set; }
        public string docLink { get; set; }
        public string fldrName { get; set; }
        public string fldrNameAr { get; set; }
    }
}