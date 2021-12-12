using dms.DTOS;
using System;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using dms.Controlers.Common;

namespace dms.Screen
{
    public partial class ToDoList : System.Web.UI.Page
    {
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        public string Direction
        {
            set
            {
                ViewState["Direction"] = value;
            }
            get
            {
                if (ViewState["Direction"] == null) return "";
                return ViewState["Direction"].ToString();
            }
        }
        public string SortExpression
        {
            set
            {
                ViewState["SortExpression"] = value;
            }
            get
            {
                if (ViewState["SortExpression"] == null) return "";
                return ViewState["SortExpression"].ToString();
            }
        }
        public int PageNumber
        {
            get
            {
                if (gvTaskLists.PageIndex >= 0)
                {
                    return gvTaskLists.PageIndex;
                }
                return 0;
            }
        }
        public int GridPageSize
        {
            get
            {
                return gvTaskLists.PageSize;
            }
        }
        public string SortAndOrderCondition
        {
            get
            {
                if (string.IsNullOrEmpty(Direction) || string.IsNullOrEmpty(SortExpression))
                {
                    return "ORDER BY todoList.[Id] DESC";
                }

                var template = "ORDER BY {0} {1}";
                var result = string.Format(template, SortExpression, Direction);

                return result;
            }
        }
        public string ConditionBuilder
        {
            get
            {
                try
                {
                    StringBuilder strBuild = new StringBuilder();
                    if (!string.IsNullOrEmpty(strBuild.ToString()))
                    {
                        strBuild.Append(" AND (ToDoList.CreatedBy = " + Session["userID"].ToString() + " or ToDoList.AssignTo = " + Session["userID"].ToString() + ")");
                    }
                    else
                    {
                        strBuild.Append(" (ToDoList.CreatedBy = " + Session["userID"].ToString() + " or ToDoList.AssignTo = " + Session["userID"].ToString() + ")");
                    }
                    if (!string.IsNullOrEmpty(txtDate.Value))
                    {
                        var taskDate = DateTime.Parse(
                    txtDate.Value + " " + "12" + ":" + "00" + ":00 " + "AM");

                        strBuild.Append(" AND CONVERT(DATE,todoList.[TaskDate]) = CONVERT(DATE,'" + taskDate.ToString("yyyy-MM-dd") + "')");
                    }

                    if (ddlAssignTo.SelectedValue != "-1")
                    {
                        if (!string.IsNullOrEmpty(strBuild.ToString()))
                        {
                            strBuild.Append(" AND assignToUser.userID = " + ddlAssignTo.SelectedValue);
                        }
                        else
                        {
                            strBuild.Append(" assignToUser.userID = " + ddlAssignTo.SelectedValue);
                        }
                    }
                    if (!string.IsNullOrEmpty(txtTask.Value))
                    {
                        strBuild.Append(" AND (TaskName like N'%" + txtTask.Value + "%' OR Description like N'%" + txtTask.Value + "%')");
                    }
                    if (rblStatus.SelectedValue != "-1" && !string.IsNullOrEmpty(rblStatus.SelectedValue))
                    {
                        if (!string.IsNullOrEmpty(strBuild.ToString()))
                        {
                            strBuild.Append(" AND todoList.[IsComplete] = " + rblStatus.SelectedValue);
                        }
                        else
                        {
                            strBuild.Append(" todoList.[IsComplete] = " + rblStatus.SelectedValue);
                        }
                    }

                    if (Request.QueryString["docID"] != null)
                    {
                        string dc = Request.QueryString["docID"];
                        var docID = c.convertToInt64(dc);
                        if (!string.IsNullOrEmpty(strBuild.ToString()))
                        {
                            strBuild.Append(" AND todoList.[DocumentId] = " + docID);
                        }
                        else
                        {
                            strBuild.Append(" todoList.[DocumentId] = " + docID);
                        }
                        strBuild.Append(string.Format(" AND (todoList.[AssignTo] = {0} OR todoList.[CreatedBy] = {0})", c.convertToInt32(Session["userID"])));
                    }
                    if (drpFilter.SelectedIndex > 0)
                    {
                        if (!string.IsNullOrEmpty(strBuild.ToString()))
                        {
                            strBuild.Append(" AND todoList.[IsComplete] = " + drpFilter.SelectedValue);
                        }
                        else
                        {
                            strBuild.Append(" todoList.[IsComplete] = " + drpFilter.SelectedValue);
                        }
                    }

                    return " WHERE todoList.[IsDeleted] = 0 " + (!string.IsNullOrEmpty(strBuild.ToString()) ? string.Format("AND {0}", strBuild.ToString()) : "");
                }
                catch
                {
                    return " WHERE todoList.[IsDeleted] = 0 ";
                }

            }
        }
        public string ListQuery
        {
            get
            {

                return string.Format(@"
  SELECT 
  
  todoList.[Id], 
  todoList.[TaskName] as 'TaskName',
  todoList.[TaskDate] as 'TaskDate',
  assignToUser.fullName as 'AssignTo',
  createdbyUser.fullName as 'CreatedBy',
  taskType.ArText as 'TaskType',
  todoList.IsComplete,
  todoList.[Description],
  todoList.CreatedBy as CreatedUserID,
  todoList.TaskType as TaskTypeID,
 (Select Count(*) from [ToDoListComments] Where [ToDoListComments].IsDeleted = 0 and  todoList.Id = [ToDoListComments].ToDoListId)  as NumberOfComments
  FROM [dbo].[ToDoList] as todoList
  INNER JOIN [dbo].[TaskTypes] taskType on todoList.TaskType = taskType.Id
  LEFT JOIN [dbo].[users] as createdbyUser on todoList.CreatedBy = createdbyUser.[userID]
  LEFT JOIN [dbo].[users] as assignToUser on todoList.AssignTo = assignToUser.[userID]

{2}

  {3};
", GridPageSize, PageNumber, ConditionBuilder, SortAndOrderCondition);
            }
        }
        public string ListQueryCount
        {
            get
            {

                return string.Format(@"
  SELECT 
  
 COUNT(*)
     
  FROM [dbo].[ToDoList] as todoList
  INNER JOIN [dbo].[TaskTypes] taskType on todoList.TaskType = taskType.Id
  LEFT JOIN [dbo].[users] as createdbyUser on todoList.CreatedBy = createdbyUser.[userID]
  LEFT JOIN [dbo].[users] as assignToUser on todoList.AssignTo = assignToUser.[userID]
{0}
", ConditionBuilder);
            }
        }

        public string DeleteQuery
        {
            get
            {
                return @"  UPDATE [dbo].[ToDoList]
	SET IsDeleted = 1 
	WHERE Id = {0}";
            }
        }

        public string UpdateCompleteQuery
        {
            get
            {
                return @"  UPDATE [dbo].[ToDoList]
	SET IsComplete = {0}, CompleteDate=N'{1}'
	WHERE Id = {2}";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Localize();
            if (!Page.IsPostBack)
            {
                var defaultItem = new DropDownItem { Id = -1, Text = HudhudResources.Resources.Screen_ToDoList_All };
                rblStatus.Items.Clear();
                rblStatus.Items.Add(new System.Web.UI.WebControls.ListItem(HudhudResources.Resources.Screen_ToDoList_All, "-1"));
                rblStatus.Items.Add(new System.Web.UI.WebControls.ListItem(HudhudResources.Resources.Screen_ToDoList_Based, "0"));
                rblStatus.Items.Add(new System.Web.UI.WebControls.ListItem(HudhudResources.Resources.Screen_ToDoList_Ending, "1"));
                ddlPageSize.Items.Clear();
                ddlPageSize.Items.Add(new System.Web.UI.WebControls.ListItem(HudhudResources.Resources.Screen_ToDoList_Show + " 5", "5"));
                ddlPageSize.Items.Add(new System.Web.UI.WebControls.ListItem(HudhudResources.Resources.Screen_ToDoList_Show + " 10", "10"));
                ddlPageSize.Items.Add(new System.Web.UI.WebControls.ListItem(HudhudResources.Resources.Screen_ToDoList_Show + " 20", "20"));
                var assignToItems = new List<DropDownItem>();
                assignToItems.Add(defaultItem);
                var usersResult = c.GetDataAsDataTable("SELECT * FROM [dbo].[users]");
                for (var i = 0; i < usersResult.Rows.Count; i++)
                {
                    assignToItems.Add(new DropDownItem
                    {
                        Id = Convert.ToInt32(usersResult.Rows[i]["userID"].ToString()),
                        Text = usersResult.Rows[i]["fullName"].ToString()
                    });
                }
                ddlAssignTo.DataSource = assignToItems;
                ddlAssignTo.DataTextField = "Text";
                ddlAssignTo.DataValueField = "Id";
                ddlAssignTo.DataBind();
                BindpriorityLevel();
                BindToDoListGrid();
            }
        }
        public void Localize()
        {
            if (Session["lang"] != null && Session["lang"].ToString() == "0")
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            else
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("ar");
            btnSearch.InnerHtml = HudhudResources.Resources.Screen_ToDoList_Yeah;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the run time error "  
            //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
        }
        public void BindpriorityLevel()
        {
            //var taskTypeItems = new List<DropDownItem>();
            //var taskResult = c.GetDataAsDataTable("SELECT * FROM [dbo].[TaskTypes]");
            //taskTypeItems.Add(new DropDownItem
            //{
            //    Id = -1,
            //    Text = Session["lang"].ToString() == "1" ? "الكل": "All"
            //});
            //for (var i = 0; i < taskResult.Rows.Count; i++)
            //{
            //    taskTypeItems.Add(new DropDownItem
            //    {
            //        Id = Convert.ToInt32(taskResult.Rows[i]["Id"].ToString()),
            //        Text = Session["lang"].ToString() == "1" ? taskResult.Rows[i]["ArText"].ToString() : taskResult.Rows[i]["EnText"].ToString()
            //    });
            //}
            //ddlTaskType.DataSource = taskTypeItems;
            //ddlTaskType.DataTextField = "Text";
            //ddlTaskType.DataValueField = "Id";
            //ddlTaskType.DataBind();
        }

        protected void gvTaskLists_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTaskLists.PageIndex = e.NewPageIndex;
            BindToDoListGrid();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["docID"] != null)
            {
                string dc = Request.QueryString["docID"];
                var docID = c.convertToInt64(dc);
                Response.Redirect(string.Format("~/Screen/AddToDoList.aspx?docId={0}", docID));
            }
            else
            {
                Response.Redirect("~/Screen/AddToDoList.aspx");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Server.Transfer("EventsList.aspx");
            gvTaskLists.PageIndex = 0;
            gvTaskLists.PageIndex = 0;
            BindToDoListGrid();
        }

        private void BindToDoListGrid()
        {
            var gridList = new List<GridViewItem>();
            var gvToDoListResult = c.GetDataAsDataTable(ListQuery);
            for (int i = 0; i < gvToDoListResult.Rows.Count; i++)
            {
                gridList.Add(new GridViewItem
                {
                    Id = Convert.ToInt32(gvToDoListResult.Rows[i]["Id"].ToString()),
                    IsComplete = Convert.ToBoolean(gvToDoListResult.Rows[i]["IsComplete"].ToString()),
                    AssignTo = gvToDoListResult.Rows[i]["AssignTo"].ToString(),
                    CreatedBy = gvToDoListResult.Rows[i]["CreatedBy"].ToString(),
                    TaskDate = Convert.ToDateTime(gvToDoListResult.Rows[i]["TaskDate"].ToString()).ToShortDateString(),
                    TaskName = gvToDoListResult.Rows[i]["TaskName"].ToString(),
                    TaskTime = Convert.ToDateTime(gvToDoListResult.Rows[i]["TaskDate"].ToString()).ToShortTimeString(),
                    TaskType = gvToDoListResult.Rows[i]["TaskType"].ToString(),
                    NumberOfComments = Convert.ToInt32(gvToDoListResult.Rows[i]["NumberOfComments"].ToString()),
                    Description = gvToDoListResult.Rows[i]["Description"].ToString(),
                    CreatedUserID = gvToDoListResult.Rows[i]["CreatedUserID"].ToString()
                });
            }

            var gvTotalCount = c.GetDataAsDataTable(ListQueryCount);

            gvTaskLists.DataSource = gridList;

            gvTaskLists.VirtualItemCount = Convert.ToInt32(gvTotalCount.Rows[0][0].ToString());
            gvTaskLists.DataBind();

            if (gridList.Count == 0)
            {
                lblNoResult.Visible = true;
                if (Request.QueryString["docID"] != null)
                {
                    lblNoResult.Text = (Session["lang"].ToString() == "0") ? "There are no tasks added within this document" : "لا يوجد مهام مضافة ضمن هذا المستند";
                }
                else
                {
                    lblNoResult.Text = (Session["lang"].ToString() == "0") ? "There are no results" : "لا يوجد نتائج";
                }
            }
            else
            {
                lblNoResult.Visible = false;
            }
            //lblNoResult
        }
        protected void gvTaskLists_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "deletethisrow") return;

            int id = Convert.ToInt32(e.CommandArgument);
            var deleteQuery = string.Format(DeleteQuery, id);
            c.NonQuery(deleteQuery);
            BindToDoListGrid();
        }
        protected void gvTaskLists_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            var id = chk.Attributes["CommandArgument"];
            var updateQuery = string.Format(UpdateCompleteQuery, chk.Checked ? "1" : "0", chk.Checked ? DateTime.Now.ToString("MM/dd/yyyy HH:mm") : "NULL", id).Replace("N'NULL'", "NULL");
            c.NonQuery(updateQuery);
            BindToDoListGrid();
        }
        protected void gridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            //var sortexp = gvTaskLists.SortExpression;
            //var asc = gvTaskLists.SortDirection == SortDirection.Ascending;

            if (Direction == "")
            {
                Direction = "asc";
            }
            else if (Direction == "asc" && SortExpression == e.SortExpression)
            {
                Direction = "desc";
            }
            else if (SortExpression == e.SortExpression)
            {
                Direction = "asc";
            }
            else
            {
                Direction = "asc";
            }

            SortExpression = e.SortExpression;

            BindToDoListGrid();
        }
        private string GetAllComments(int ToDoListId)
        {
            return string.Format(@"SELECT [Id],[CommentText],[CreatedOn],[CreatedBy],[IsDeleted]
                                  FROM dbo.[ToDoListComments]
                                  WHERE [ToDoListId] = {0}", ToDoListId);
        }
        private string InsertComment(int ToDoListId)
        {
            return string.Format(@"INSERT INTO [dbo].[ToDoListComments]([ToDoListId],[CommentText],[CreatedBy],[CreatedOn])
                 VALUES
                       (N'{0}',N'{1}',N'{2}',N'{3}')",
                   ToDoListId, txtComment.Value, Session["userId"].ToString(), DateTime.Now.ToString());
        }
        private string DeleteComment
        {
            get
            {
                return "UPDATE [dbo].[ToDoListComments] SET [IsDeleted] = 1 WHERE [Id]={0}";
            }
        }
        protected void btnAddComment_ServerClick(object sender, EventArgs e)
        {
            int ToDoListId = int.Parse(hToDoListId.Value);
            c.NonQuery(InsertComment(ToDoListId));
            txtComment.Value = "";
            BindCommentsGrid(ToDoListId);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showCommentModal();", true);
        }
        public string GetUserName(Int32 userID)
        {
            string sql = "select fullname from users where userid=" + userID.ToString();
            return c.GetDataAsScalar(sql).ToString();

        }
        public bool IsThisUserCreate(object id, object isDeleted)
        {
            var result = Session["userId"].ToString() == id.ToString();

            bool isParse;
            bool.TryParse(isDeleted.ToString(), out isParse);
            if (isParse == false) return result;

            return Session["userId"].ToString() == id.ToString() && !Convert.ToBoolean(isDeleted.ToString());
        }
        public bool IsDeleted(object isDeleted)
        {
            bool isParse;
            bool.TryParse(isDeleted.ToString(), out isParse);
            if (isParse == false) return false;
            return Convert.ToBoolean(isDeleted.ToString());
        }
        protected void gvTaskListComments_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Sort")
            {
                if (Direction == "")
                {
                    Direction = "asc";
                }
                else if (Direction == "asc" && SortExpression == e.CommandArgument.ToString())
                {
                    Direction = "desc";
                }
                else if (SortExpression == e.CommandArgument.ToString())
                {
                    Direction = "asc";
                }
                else
                {
                    Direction = "asc";
                }

                SortExpression = e.CommandArgument.ToString();

                BindToDoListGrid();
            }
            else if (e.CommandName == "deletethisrow")
            {
                hCommentToDoListId.Value = e.CommandArgument.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showCommentToDoListRemoveModal();", true);

            }
        }
        protected void btnShowComment_ServerClick(object sender, EventArgs e)
        {
            var row = (GridViewRow)(gvTaskLists.Rows[((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)sender).Parent.Parent).RowIndex]);

            if (row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hCreatedUserID = (HiddenField)(row.FindControl("hTaskName"));
                hToDoListName.Value = hCreatedUserID.Value;
                taskName.InnerHtml = hCreatedUserID.Value;
            }

            int ToDoListId = int.Parse(gvTaskLists.DataKeys[((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)sender).Parent.Parent).RowIndex].Value.ToString());
            hToDoListId.Value = ToDoListId.ToString();//TaskName

            BindCommentsGrid(ToDoListId);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showCommentModal();", true);
        }
        private void BindCommentsGrid(int ToDoListId)
        {
            var result = c.GetDataAsDataTable(GetAllComments(ToDoListId));
            gvTaskListComments.DataSource = result;
            gvTaskListComments.DataBind();
        }
        protected void btnShowDeleteDialog_ServerClick(object sender, EventArgs e)
        {
            int ToDoListId = int.Parse(gvTaskLists.DataKeys[((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)sender).Parent.Parent).RowIndex].Value.ToString());
            hToDoListId.Value = ToDoListId.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showToDoListRemoveModal();", true);
        }
        protected void btnDeleteTask_ServerClick(object sender, EventArgs e)
        {
            var deleteQuery = string.Format(@"  UPDATE [dbo].[ToDoList] SET IsDeleted = 1 WHERE Id = {0}", hToDoListId.Value);
            c.NonQuery(deleteQuery);
            BindToDoListGrid();
        }
        protected void btnDeleteComment_ServerClick(object sender, EventArgs e)
        {
            var deleteQuery = string.Format(@"  UPDATE [dbo].[ToDoListComments] SET IsDeleted = 1 WHERE Id = {0}", hCommentToDoListId.Value);
            c.NonQuery(deleteQuery);
            BindCommentsGrid(int.Parse(hToDoListId.Value));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showCommentModal();", true);
        }
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvTaskLists.PageSize = int.Parse(ddlPageSize.SelectedValue.ToString());
            BindToDoListGrid();
        }
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Vithal" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvTaskLists.GridLines = GridLines.Both;
            gvTaskLists.HeaderStyle.Font.Bold = true;
            gvTaskLists.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }

        protected void btnExport_ServerClick(object sender, EventArgs e)
        {
            DataTable gvToDoListResult = c.GetDataAsDataTable(ListQuery);
            //ExportToEXEL(gvToDoListResult);
        }

        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, BaseColor color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }
        //protected void btnDeleteComment_ServerClick(object sender, EventArgs e)
        //{
        //    int id = int.Parse(gvTaskListComments.DataKeys[((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)sender).Parent.Parent).RowIndex].Value.ToString());
        //    var deleteQuery = string.Format(DeleteComment, id);
        //    c.NonQuery(deleteQuery);
        //    BindCommentsGrid(int.Parse(hToDoListId.Value));
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showCommentModal();", true);
        //}

        protected void gvTaskLists_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hCreatedUserID = (HiddenField)(e.Row.FindControl("hCreatedUserID"));
                System.Web.UI.HtmlControls.HtmlAnchor btnEdit = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("btnEdit");
                System.Web.UI.HtmlControls.HtmlAnchor btnShowDeleteDialog = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("btnShowDeleteDialog");
                if (hCreatedUserID.Value != Session["userID"].ToString())
                {
                    btnEdit.Style.Add("display", "none");
                    btnShowDeleteDialog.Style.Add("display", "none");
                }
            }
        }

        protected void btnExportPDF_ServerClick(object sender, EventArgs e)
        {
            DataTable dt = c.GetDataAsDataTable(ListQuery);
            dt.Columns.Remove("Id");
            dt.Columns.Remove("CreatedUserID");
            dt.Columns.Remove("TaskTypeID");
            dt.Columns.Remove("Description");
            //dt.Columns[0].ColumnName = "تسلسل";
            ExportToPdf(dt);
            //Session["myDataTable"] = c.GetDataAsDataTable(ListQuery);
            //RegisterStartupScript("ExportPDF", "<script>window.open('../ExportDataTableToPdf.ashx');</script>");
        }

        protected void btnExportEXCEL_ServerClick(object sender, EventArgs e)
        {
            //Session["myDataTable"] = c.GetDataAsDataTable(ListQuery);
            //RegisterStartupScript("ExportPDF", "<script>window.open('../ExportDataTableToExcel.ashx');</script>");
            string attachment = "attachment; filename=tasks.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            // Insert below
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            //To Export all pages
            gvTaskLists.AllowPaging = false;
            this.BindToDoListGrid();
            gvTaskLists.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        //public void ExportToEXEL(DataTable myDataTable)
        //{
        //    DataTable dt = myDataTable;
        //    try
        //    {

        //        using (XLWorkbook wb = new XLWorkbook())
        //        {
        //            wb.Worksheets.Add(dt, "Tasks");

        //            Response.Clear();
        //            Response.Buffer = true;
        //            Response.Charset = "";
        //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //            Response.AddHeader("content-disposition", "attachment;filename=Tasks_" + DateTime.Now.Date.Day.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Hour.ToString() + DateTime.Now.Date.Minute.ToString() + DateTime.Now.Date.Second.ToString() + DateTime.Now.Date.Millisecond.ToString() + ".xlsx");
        //            using (MemoryStream MyMemoryStream = new MemoryStream())
        //            {
        //                wb.SaveAs(MyMemoryStream);
        //                MyMemoryStream.WriteTo(Response.OutputStream);
        //                Response.Flush();
        //                Response.End();
        //            }
        //        }
        //    }
        //    catch (DocumentException de)
        //    {
        //    }
        //    // System.Web.HttpContext.Current.Response.Write(de.Message)
        //    catch (IOException ioEx)
        //    {
        //    }
        //    // System.Web.HttpContext.Current.Response.Write(ioEx.Message)
        //    catch (Exception ex)
        //    {
        //    }
        //}
        public void ExportToPdf(DataTable myDataTable)
        {
            DataTable dt = myDataTable;
            Document pdfDoc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 10, 10, 10, 10);
            var ArialFontFile = Path.Combine(HttpContext.Current.Server.MapPath("../fonts"), "ARIALUNI.ttf");
            //Reference a Unicode font to be sure that the symbols are present. 
            BaseFont bfArialUniCode = BaseFont.CreateFont(ArialFontFile, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            //Create a font from the base font
            Font font12 = new Font(bfArialUniCode, 12);
            Font font10 = new Font(bfArialUniCode, 10);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                if (dt.Rows.Count > 0)
                {
                    PdfPTable PdfTable = new PdfPTable(1)
                    {
                        RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                    };
                    PdfTable.TotalWidth = 200f;
                    PdfTable.LockedWidth = true;
                    //Ensure that wrapping is on, otherwise Right to Left text will not display 
                    PdfTable.DefaultCell.NoWrap = false;
                    PdfPCell PdfPCell;
                    //PdfPCell PdfPCell = new PdfPCell(new Phrase(new Chunk("Employee Details", font18)));
                    //PdfPCell.Border = Rectangle.NO_BORDER;
                    //PdfTable.AddCell(PdfPCell);
                    //DrawLine(writer, 25f, pdfDoc.Top - 30f, pdfDoc.PageSize.Width - 25f, pdfDoc.Top - 30f, new BaseColor(System.Drawing.Color.Red));
                    pdfDoc.Add(PdfTable);

                    PdfTable = new PdfPTable(dt.Columns.Count)
                    {
                        RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                    };
                    PdfTable.DefaultCell.NoWrap = false;
                    PdfTable.SpacingBefore = 20f;
                    for (int columns = 0; columns <= dt.Columns.Count - 1; columns++)
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Columns[columns].ColumnName, font12)))
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                        };
                        //Ensure that wrapping is on, otherwise Right to Left text will not display 
                        PdfPCell.NoWrap = false;
                        PdfTable.AddCell(PdfPCell);
                    }

                    for (int rows = 0; rows <= dt.Rows.Count - 1; rows++)
                    {
                        for (int column = 0; column <= dt.Columns.Count - 1; column++)
                        {
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Rows[rows][column].ToString(), font10)))
                            {
                                RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                            };
                            //Ensure that wrapping is on, otherwise Right to Left text will not display 
                            PdfPCell.NoWrap = false;
                            PdfTable.AddCell(PdfPCell);
                        }
                    }
                    pdfDoc.Add(PdfTable);
                }
                pdfDoc.Close();
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=TasksReport_" + DateTime.Now.Date.Day.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Hour.ToString() + DateTime.Now.Date.Minute.ToString() + DateTime.Now.Date.Second.ToString() + DateTime.Now.Date.Millisecond.ToString() + ".pdf");
                System.Web.HttpContext.Current.Response.Write(pdfDoc);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (DocumentException de)
            {
            }
            // System.Web.HttpContext.Current.Response.Write(de.Message)
            catch (IOException ioEx)
            {
            }
            // System.Web.HttpContext.Current.Response.Write(ioEx.Message)
            catch (Exception ex)
            {
            }
        }

        protected void ddlExport_ServerChange(object sender, EventArgs e)
        {
            if (ddlExport.SelectedValue == "PDF")
            {
                ////ExportToPdf(c.GetDataAsDataTable(ListQuery));
                ////Response.Redirect(HttpContext.Current.Request.UrlReferrer.ToString());
                Session["myDataTable"] = c.GetDataAsDataTable(ListQuery);
                RegisterStartupScript("ExportPDF", "<script>window.open('../ExportDataTableToPdf.ashx');</script>");
                //Response.Redirect("../ExportDataTableToPdf.ashx");
            }
            else if (ddlExport.SelectedValue == "EXCEL")
            {
                //////ExportToEXEL(c.GetDataAsDataTable(ListQuery));
                //////Response.Redirect(HttpContext.Current.Request.UrlReferrer.ToString());
                //Session["myDataTable"] = c.GetDataAsDataTable(ListQuery);
                //RegisterStartupScript("ExportEXCEL", "<script>window.open('../ExportDataTableToExcel.ashx');</script>");
                ////Response.Redirect("../ExportDataTableToExcel.ashx");
                string attachment = "attachment; filename=Tasks.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                // Insert below
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvTaskLists.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }

        protected void btnShowCommentUsingCurrentToDoListId_ServerClick(object sender,EventArgs e)
        {

            BindCommentsGrid(hToDoListId.Value.ToInt());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showCommentModal();", true);
        }

        protected void drpFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindToDoListGrid();
        }
    }
}