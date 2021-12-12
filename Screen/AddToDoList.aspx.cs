using dms.DTOS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Screen
{
    public partial class AddToDoList : System.Web.UI.Page
    {
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();

        #region Queries
        private string GetToDoQuery
        {
            get
            {
                return @"
SELECT  [Id]
      ,[TaskName]
      ,[TaskDate]
      ,[AssignTo]
      ,[CreatedBy]
      ,[TaskType]
      ,[CreatedOn]
      ,[IsComplete]
      ,[IsDeleted]
      ,[Description]
      ,[RepeatType]
      ,[RepeatWeekDays]
  FROM [dbo].[ToDoList] 
WHERE  [Id] ={0}
";
            }
        }
        private string AddToDoQuery
        {
            get
            {
                return @"INSERT INTO [dbo].[ToDoList]
           ([TaskName]
           ,[TaskDate]
           ,[AssignTo]
           ,[CreatedBy]
           ,[TaskType]
           ,[CreatedOn]
           ,[IsComplete]
           ,[IsDeleted]
           ,[Description]
           ,[DocumentId]
           ,[CompleteDate]
           ,[RepeatType]
           ,[RepeatWeekDays]
)
     VALUES
           (N'{0}'
           ,N'{1}'
           ,N'{2}'
           ,N'{3}'
           ,N'{4}'
           ,N'{5}'
           ,N'{6}'
           ,N'{7}'
           ,N'{8}'
           ,{9}
           ,{10}
           ,N'{11}'
           ,N'{12}')";
            }
        }
        private string UpdateToDoQuery
        {
            get
            {
                return @"

UPDATE [dbo].[ToDoList] SET 
           [TaskName]=N'{0}'
           ,[TaskDate]=N'{1}'
           ,[AssignTo]=N'{2}'
           ,[TaskType]=N'{3}'
           ,[Description]=N'{4}'
           ,[IsComplete]=N'{5}'
           ,[CompleteDate]=N'{6}'
           ,[RepeatType]=N'{7}'
           ,[RepeatWeekDays]=N'{8}'
     WHERE [Id]={9}
         ";
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
        private string GetAllComments
        {
            get
            {
                return string.Format(@"SELECT [Id]
      ,[CommentText]
      ,[CreatedOn],[CreatedBy],[IsDeleted]
  FROM dbo.[ToDoListComments]
  WHERE [ToDoListId] = {0}", Request.Params["id"].ToString());
            }
        }
        private string InsertComment
        {
            get
            {
                return string.Format(@"
INSERT INTO [dbo].[ToDoListComments]
           ([ToDoListId]
           ,[CommentText]
           ,[CreatedBy]
           ,[CreatedOn])
     VALUES
           (N'{0}'
           ,N'{1}'
           ,N'{2}'
           ,N'{3}')", Request.Params["id"].ToString(), txtComment.Value, Session["userId"].ToString(), DateTime.Now.ToString());
            }
        }
        private string DeleteComment
        {
            get
            {
                return "UPDATE [dbo].[ToDoListComments] SET [IsDeleted] = 1 WHERE [Id]={0}";
            }
        }
        #endregion

        public string GetUserName(Int32 userID)
        {
            string sql = "select fullname from users where userid=" + userID.ToString();
            return c.GetDataAsScalar(sql).ToString();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            Localize();
            if (!Page.IsPostBack)
            {
                Int32 userID = c.convertToInt32(Session["userId"]);

                if (DMS.Security.checkAllowedPage(userID, Request.Url.AbsolutePath))
                {
                    DMS.DAL.operations op = new DMS.DAL.operations();
                    Int32 eventID = op.dboAddSysEvents(userID, 2, DateTime.Now, Request.Url.AbsoluteUri);
                    op = new DMS.DAL.operations();
                    op.dboAddBrowseingEvents(eventID, DMS.Security.getProgramID(Request.Url.AbsolutePath));
                }
                else
                {
                    Response.Redirect("../screen/notAllowed.html");
                }

                var defaultItem = new DropDownItem { Id = -1, Text = HudhudResources.Resources.Screen_ToDoList_Choose + "..." };

                var assignToItems = new List<DropDownItem>();
                var taskTypeItems = new List<DropDownItem>();


                assignToItems.Add(defaultItem);
                taskTypeItems.Add(defaultItem);

                var usersResult = c.GetDataAsDataTable("SELECT * FROM [dbo].[users]");
                for (var i = 0; i < usersResult.Rows.Count; i++)
                {
                    assignToItems.Add(new DropDownItem
                    {
                        Id = Convert.ToInt32(usersResult.Rows[i]["userID"].ToString()),
                        Text = usersResult.Rows[i]["fullName"].ToString()
                    });
                }

                var taskResult = c.GetDataAsDataTable("SELECT * FROM [dbo].[TaskTypes]");
                for (var i = 0; i < taskResult.Rows.Count; i++)
                {
                    taskTypeItems.Add(new DropDownItem
                    {
                        Id = Convert.ToInt32(taskResult.Rows[i]["Id"].ToString()),
                        Text = (Session["lang"].ToString() == "0") ? taskResult.Rows[i]["EnText"].ToString() : taskResult.Rows[i]["arText"].ToString(),
                    });
                }

                ddlAssignTo.DataSource = assignToItems;
                ddlAssignTo.DataTextField = "Text";
                ddlAssignTo.DataValueField = "Id";

                ddlTaskType.DataSource = taskTypeItems;
                ddlTaskType.DataTextField = "Text";
                ddlTaskType.DataValueField = "Id";



                ddlAssignTo.DataBind();
                ddlTaskType.DataBind();

                ddlRepeatType.Items.Add(new ListItem(defaultItem.Text, ""));
                ddlRepeatType.Items.Add(new ListItem(HudhudResources.Resources.Screen_ToDoList_Daily, "Daily"));
                ddlRepeatType.Items.Add(new ListItem(HudhudResources.Resources.Screen_ToDoList_Weekly, "Weekly"));
                ddlRepeatType.Items.Add(new ListItem(HudhudResources.Resources.Screen_ToDoList_weekdays, "WeekDays"));
                ddlRepeatType.Items.Add(new ListItem(HudhudResources.Resources.Screen_ToDoList_monthly, "Monthly"));
                ddlRepeatType.Items.Add(new ListItem(HudhudResources.Resources.Screen_ToDoList_annually, "Yearly"));
                ddlRepeatType.Attributes.Add("onchange", "RepeatTypeChanged(this.value)");
                List<string> repeatWeekDays = new List<string>();
                bool isExist = false;
                if (Request.Params["id"] != null) // edit
                {
                     isExist = ActiveEditMode(Request.Params["id"].ToString());
                }
                if (isExist)
                {
                    SetRepeatWeekDays(ref repeatWeekDays, Request.Params["id"].ToString());
                    txtToDoAction.InnerHtml = HudhudResources.Resources.Screen_ToDoList_Edittask;
                    BindToDoListGrid();
                    divCommentSection.Visible = true;
                }
                else // add
                {
                    divCommentSection.Visible = false;
                    divIsFinished.Visible = false;
                    txtToDoAction.InnerHtml = HudhudResources.Resources.Screen_ToDoList_Addanewtask;
                    btnDelete.Visible = false;
                }
                rptWeekDays.DataSource = GetWeekDays(repeatWeekDays, Session["lang"].ToString());
                rptWeekDays.DataBind();
            }
        }
        private void Localize()
        {
            if (Session["lang"].ToString() == "0")
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            else
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("ar");
            lnkToDoList.InnerHtml = HudhudResources.Resources.Screen_ToDoList_Todolist;
        }
        public void SetRepeatWeekDays(ref List<string> repeatWeekDays, string id)
        {
            var RepeatWeekDays = c.GetDataAsScalar("Select RepeatWeekDays From ToDoList Where id=" + id);
            if (RepeatWeekDays != null && !string.IsNullOrEmpty(RepeatWeekDays.ToString()))
            {
                foreach (var item in RepeatWeekDays.ToString().Split(','))
                {
                    if (item != "")
                        repeatWeekDays.Add(item);
                }
            }
        }
        public DataTable GetWeekDays(List<string> repeatWeekDays, string lang)
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();
            table.Columns.Add("WeekDay", typeof(string));
            table.Columns.Add("WeekDayName", typeof(string));
            table.Columns.Add("WeekDayFirstChar", typeof(string));
            table.Columns.Add("class", typeof(string));
            string[] weekdays = { "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            string[] arabicweekdays = { "السبت", "الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة" };
            int i = 0;
            foreach (var day in (lang == "0" ? weekdays : arabicweekdays))
            {
                table.Rows.Add(weekdays[i], day, day.Substring((lang == "0" ? 0 : 2), 1), (repeatWeekDays.Contains(weekdays[i]) ? "active" : ""));
                i++;
            }
            return table;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PreviousPage"]))
                Server.Transfer("ToDoList.aspx");
            else
                Server.Transfer(Request.QueryString["PreviousPage"]);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var taskName = txtTaskName.Text;
            var taskDate = DateTime.Parse(txtDate.Value + " " + txtTime.Value);
            if (taskDate >= DateTime.Now)
            {
                var description = txtDescription.Text;
                var assignTo = ddlAssignTo.SelectedValue;
                var taskType = ddlTaskType.SelectedValue;
                var createdBy = Convert.ToInt32(Session["userId"]);
                string query;
                if (Request.Params["id"] == null)
                {
                    query = string.Format(AddToDoQuery

               , taskName, taskDate.ToString(), assignTo, createdBy, taskType, DateTime.Now.ToString(), false, false, description,
               (Request.QueryString["docID"] != null ? Request.QueryString["docID"] : "NULL"), "NULL", ddlRepeatType.SelectedValue, hRepeatWeekDays.Value).Replace("N'NULL'", "NULL");
                }
                else
                {
                    query = string.Format(UpdateToDoQuery
                    , taskName, taskDate.ToString(), assignTo, taskType, description, chCompleted.Checked ? "1" : "0", chCompleted.Checked ? DateTime.Now.ToString("MM/dd/yyyy HH:mm") : "NULL", ddlRepeatType.SelectedValue, hRepeatWeekDays.Value, Request.Params["id"].ToString()).Replace("N'NULL'", "NULL");

                }

                c.NonQuery(query);
                if (string.IsNullOrEmpty(Request.QueryString["PreviousPage"]))
                    Server.Transfer("ToDoList.aspx");
                else
                    Server.Transfer(Request.QueryString["PreviousPage"]);

            }
            else
            {
                lblResult.Text = "تاريخ المهمه لابد ان يكون اكبر من تاريخ اليوم";
                lblResult.ForeColor = Color.Red;
            }

        }
        private bool ActiveEditMode(string id)
        {
            var query = string.Format(GetToDoQuery, id);
            var result = c.GetDataAsDataTable(query);
            if (result.Rows.Count != 0)
            {
                var todoDate = Convert.ToDateTime(result.Rows[0]["TaskDate"].ToString());
                txtTaskName.Text = result.Rows[0]["TaskName"].ToString();
                txtDate.Value = todoDate.ToString("yyyy-MM-dd");
                txtTime.Value = todoDate.ToString("HH:mm");
                txtDescription.Text = result.Rows[0]["Description"].ToString();
                ddlAssignTo.SelectedValue = result.Rows[0]["AssignTo"].ToString();
                ddlTaskType.SelectedValue = result.Rows[0]["TaskType"].ToString();
                ddlRepeatType.SelectedValue = result.Rows[0]["RepeatType"].ToString();
                if (ddlRepeatType.SelectedValue == "WeekDays")
                    RegisterStartupScript("ShowWeekDays", "<script>document.getElementById(\"divRepeatDays\").style.display = \"block\";</script>");
                hRepeatWeekDays.Value = result.Rows[0]["RepeatWeekDays"].ToString();
                chCompleted.Checked = Convert.ToBoolean(result.Rows[0]["IsComplete"].ToString());
                if (int.Parse(Session["userID"].ToString()) == 1 || (int.Parse(Session["userID"].ToString()) == int.Parse(result.Rows[0]["CreatedBy"].ToString())))
                {
                    btnSave.Visible = true;
                    btnDelete.Visible = true;
                }
                else
                {
                    btnSave.Visible = false;
                    btnDelete.Visible = false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        private int GetHourIndex(int hour)
        {
            if (hour > 12)
            {
                return hour - 12;
            }
            if (hour == 0) return 12;

            return hour;
        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            c.NonQuery(InsertComment);
            txtComment.Value = "";
            BindToDoListGrid();
            spanEnd.Focus();
        }


        protected void gvTaskListComments_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "deletethisrow") return;

            //int id = Convert.ToInt32(e.CommandArgument);
            //var deleteQuery = string.Format(DeleteComment, id);
            //c.NonQuery(deleteQuery);
            //BindToDoListGrid();
            //gvTaskListComments.Focus();
            hCommentToDoListId.Value = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showCommentToDoListRemoveModal();", true);

        }
        protected void btnDeleteComment_ServerClick(object sender, EventArgs e)
        {
            var deleteQuery = string.Format(DeleteComment,  hCommentToDoListId.Value);
            c.NonQuery(deleteQuery);
            BindToDoListGrid();
            gvTaskListComments.Focus();
        }
        private void BindToDoListGrid()
        {
            var result = c.GetDataAsDataTable(GetAllComments);
            gvTaskListComments.DataSource = result;
            gvTaskListComments.DataBind();
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

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.Params["id"].ToString());
            var deleteQuery = string.Format(DeleteQuery, id);
            c.NonQuery(deleteQuery);
            if (string.IsNullOrEmpty(Request.QueryString["PreviousPage"]))
                Server.Transfer("ToDoList.aspx");
            else
                Server.Transfer(Request.QueryString["PreviousPage"]);

        }
    }
}