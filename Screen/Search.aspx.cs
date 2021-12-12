using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Screen
{
    public partial class SearchResults : System.Web.UI.Page
    {
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["CODEN"]))
                {
                    ViewState["SearchText"] = Request.QueryString["CODEN"];
                    LoadSearchResult();
                }
            }
        }
        protected void LoadSearchResult()
        {
            gvEvents.DataSource = LoadEvents();
            gvEvents.DataBind();
            gvTasks.DataSource = LoadTasks();
            gvTasks.DataBind();
            gvOutgoingAndIncomingDocuments.DataSource = LoadOutgoingAndIncomingDocuments();
            gvOutgoingAndIncomingDocuments.DataBind();
            gvDocuments.DataSource = LoadDocuments();
            gvDocuments.DataBind();
            LoadSearchResultCount();
        }
        protected void LoadSearchResultCount()
        {
            spanEventsCount.InnerHtml = GetDataSourceCount(gvEvents) > 10 ? "+10" : GetDataSourceCount(gvEvents).ToString();
            spanTasksCount.InnerHtml = GetDataSourceCount(gvTasks) > 10 ? "+10" : GetDataSourceCount(gvTasks).ToString();
            spanOutgoingAndIncomingDocumentsCount.InnerHtml = GetDataSourceCount(gvOutgoingAndIncomingDocuments) > 10 ? "+10" : GetDataSourceCount(gvOutgoingAndIncomingDocuments).ToString();
            spanDocumentsCount.InnerHtml = GetDataSourceCount(gvDocuments) > 10 ? "+10" : GetDataSourceCount(gvDocuments).ToString();
        }
        protected int GetDataSourceCount(GridView Grid)
        {
            if (Grid.DataSource == null)
                return 0;
            else if (Grid.DataSource.GetType() == typeof(DataTable))
                return (Grid.DataSource as DataTable).Rows.Count;
            else if (Grid.DataSource.GetType().IsGenericType)
                return (Grid.DataSource as IList).Count;
            return Grid.Rows.Count;
        }
        protected DataTable LoadEvents()
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("@UserID", Session["userId"]);
            parameters.Add("@SearchText", ViewState["SearchText"]);
            return c.GetDataAsDataTableFromSP("EventsSearch", parameters);
        }
        protected void gvEvents_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gvEvents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEvents.PageIndex = e.NewPageIndex;
            LoadEvents();
        }

        protected void btnShowDeleteDialog_ServerClick(object sender, EventArgs e)
        {
            if (((System.Web.UI.Control)sender).ClientID.Contains("gvTasks"))
            {
                int Id = int.Parse(gvTasks.DataKeys[((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)sender).Parent.Parent).RowIndex].Value.ToString());
                hTaskID.Value = Id.ToString();
                hEventID.Value = "";
                hDocumentID.Value = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showTaskRemoveModal();", true);
            }
            else if (((System.Web.UI.Control)sender).ClientID.Contains("gvEvents"))
            {
                int Id = int.Parse(gvEvents.DataKeys[((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)sender).Parent.Parent).RowIndex].Value.ToString());
                hEventID.Value = Id.ToString();
                hTaskID.Value = "";
                hDocumentID.Value = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showEventRemoveModal();", true);
            }
            else if (((System.Web.UI.Control)sender).ClientID.Contains("gvDocuments"))
            {
                int Id = int.Parse(gvDocuments.DataKeys[((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)sender).Parent.Parent).RowIndex].Value.ToString());
                hDocumentID.Value = Id.ToString();
                hTaskID.Value = "";
                hEventID.Value = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showDocumentRemoveModal();", true);
            }
            else if (((System.Web.UI.Control)sender).ClientID.Contains("gvOutgoingAndIncomingDocuments"))
            {
                int Id = int.Parse(gvOutgoingAndIncomingDocuments.DataKeys[((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)sender).Parent.Parent).RowIndex].Value.ToString());
                hDocumentID.Value = Id.ToString();
                hTaskID.Value = "";
                hEventID.Value = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showDocumentRemoveModal();", true);
            }
        }
        protected DataTable LoadTasks()
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("@UserID", Session["userId"]);
            parameters.Add("@lang", Session["lang"]);
            parameters.Add("@SearchText", ViewState["SearchText"]);
            return c.GetDataAsDataTableFromSP("TasksSearch", parameters);

        }
        public SortDirection directionEvents
        {
            get
            {
                if (ViewState["directionStateEvents"] == null)
                {
                    ViewState["directionStateEvents"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionStateEvents"];
            }
            set
            {
                ViewState["directionStateEvents"] = value;
            }
        }

        protected void gvTasks_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
        public SortDirection directionTasks
        {
            get
            {
                if (ViewState["directionStateTasks"] == null)
                {
                    ViewState["directionStateTasks"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionStateTasks"];
            }
            set
            {
                ViewState["directionStateTasks"] = value;
            }
        }
        protected void gvTasks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Sort"))
            {
                string sortingDirection = string.Empty;
                if (directionTasks == SortDirection.Ascending)
                {
                    directionTasks = SortDirection.Descending;
                    sortingDirection = "Desc";
                }
                else
                {
                    directionTasks = SortDirection.Ascending;
                    sortingDirection = "Asc";
                }
                DataView sortedView = new DataView(LoadTasks());
                sortedView.Sort = e.CommandArgument + " " + sortingDirection;
                Session["SortedView"] = sortedView;
                gvTasks.DataSource = sortedView;
                gvTasks.DataBind();
                gvTasks.Focus();
            }
        }

        protected void gvTasks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTasks.PageIndex = e.NewPageIndex;
            LoadTasks();
        }

        protected void gvTasks_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            var id = chk.Attributes["CommandArgument"];
            var updateQuery = string.Format("UPDATE [dbo].[ToDoList] SET IsComplete = {0},CompleteDate='{1}' WHERE Id = {2}", chk.Checked ? "1" : "0", chk.Checked ? DateTime.Now.ToString("MM/dd/yyyy HH:mm") : "NULL", id).Replace("'NULL'", "NULL");
            c.NonQuery(updateQuery);
            LoadSearchResult();
        }
        protected DataTable LoadOutgoingAndIncomingDocuments()
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("@UserID", Session["userId"]);
            parameters.Add("@lang", Session["lang"]);
            parameters.Add("@SearchText", ViewState["SearchText"]);
            return c.GetDataAsDataTableFromSP("OutgoingAndIncomingDocumentsSearch", parameters);

        }
        public SortDirection directionOutgoingAndIncomingDocuments
        {
            get
            {
                if (ViewState["directionStateOutgoingAndIncomingDocuments"] == null)
                {
                    ViewState["directionStateOutgoingAndIncomingDocuments"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionStateOutgoingAndIncomingDocuments"];
            }
            set
            {
                ViewState["directionStateOutgoingAndIncomingDocuments"] = value;
            }
        }
        protected void gvOutgoingAndIncomingDocuments_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gvOutgoingAndIncomingDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Sort"))
            {
                string sortingDirection = string.Empty;
                if (directionOutgoingAndIncomingDocuments == SortDirection.Ascending)
                {
                    directionOutgoingAndIncomingDocuments = SortDirection.Descending;
                    sortingDirection = "Desc";
                }
                else
                {
                    directionOutgoingAndIncomingDocuments = SortDirection.Ascending;
                    sortingDirection = "Asc";
                }
                DataView sortedView = new DataView(LoadOutgoingAndIncomingDocuments());
                sortedView.Sort = e.CommandArgument + " " + sortingDirection;
                Session["SortedView"] = sortedView;
                gvOutgoingAndIncomingDocuments.DataSource = sortedView;
                gvOutgoingAndIncomingDocuments.DataBind();
                gvOutgoingAndIncomingDocuments.Focus();
            }
        }

        protected void gvOutgoingAndIncomingDocuments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOutgoingAndIncomingDocuments.PageIndex = e.NewPageIndex;
            LoadOutgoingAndIncomingDocuments();
        }
        protected DataTable LoadDocuments()
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("@UserID", Session["userId"]);
            parameters.Add("@lang", Session["lang"]);
            parameters.Add("@SearchText", ViewState["SearchText"]);
            return c.GetDataAsDataTableFromSP("DocumentsSearch", parameters);
        }
        public SortDirection directionDocuments
        {
            get
            {
                if (ViewState["directionStateDocuments"] == null)
                {
                    ViewState["directionStateDocuments"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionStateDocuments"];
            }
            set
            {
                ViewState["directionStateDocuments"] = value;
            }
        }
        protected void gvDocuments_Sorting(object sender, GridViewSortEventArgs e)
        {
            //string sortingDirection = string.Empty;
            //if (directionDocuments == SortDirection.Ascending)
            //{
            //    directionDocuments = SortDirection.Descending;
            //    sortingDirection = "Desc";
            //}
            //else
            //{
            //    directionDocuments = SortDirection.Ascending;
            //    sortingDirection = "Asc";
            //}
            //DataView sortedView = new DataView(LoadDocuments());
            //sortedView.Sort = e.SortExpression + " " + sortingDirection;
            //Session["SortedView"] = sortedView;
            //gvDocuments.DataSource = sortedView;
            //gvDocuments.DataBind();
            //gvDocuments.Focus();
        }

        protected void gvDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Sort"))
            {
                string sortingDirection = string.Empty;
                if (directionDocuments == SortDirection.Ascending)
                {
                    directionDocuments = SortDirection.Descending;
                    sortingDirection = "Desc";
                }
                else
                {
                    directionDocuments = SortDirection.Ascending;
                    sortingDirection = "Asc";
                }
                DataView sortedView = new DataView(LoadDocuments());
                sortedView.Sort = e.CommandArgument + " " + sortingDirection;
                Session["SortedView"] = sortedView;
                gvDocuments.DataSource = sortedView;
                gvDocuments.DataBind();
                gvDocuments.Focus();
            }
        }

        protected void gvDocuments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDocuments.PageIndex = e.NewPageIndex;
            LoadDocuments();
        }

        protected void btnDeleteEvent_ServerClick(object sender, EventArgs e)
        {
            var deleteQuery = string.Format(@"  Delete From event WHERE event_id = {0}", hEventID.Value);
            c.NonQuery(deleteQuery);
            LoadSearchResult();
        }

        protected void btnDeleteDocument_ServerClick(object sender, EventArgs e)
        {
            var deleteQuery = string.Format(@"  Delete From documents WHERE docID = {0}", hDocumentID.Value);
            c.NonQuery(deleteQuery);
            LoadSearchResult();
        }

        protected void btnDeleteTask_ServerClick(object sender, EventArgs e)
        {
            var deleteQuery = string.Format(@"  UPDATE [dbo].[ToDoList] SET IsDeleted = 1 WHERE Id = {0}", hTaskID.Value);
            c.NonQuery(deleteQuery);
            LoadSearchResult();
        }

        protected void btnShowAllDocuments_ServerClick(object sender, EventArgs e)
        {
            DataTable dt = LoadDocuments();
            gvDocuments.AllowPaging = false;
            gvDocuments.DataSource = dt;
            gvDocuments.DataBind();
            gvDocuments.Focus();
        }

        protected void btnShowAllTasks_ServerClick(object sender, EventArgs e)
        {
            DataTable dt = LoadTasks();
            gvTasks.AllowPaging = false;
            gvTasks.DataSource = dt;
            gvTasks.DataBind();
            gvTasks.Focus();
        }

        protected void btnShowAllOutgoingAndIncomingDocuments_ServerClick(object sender, EventArgs e)
        {
            DataTable dt = LoadOutgoingAndIncomingDocuments();
            gvOutgoingAndIncomingDocuments.AllowPaging = false;
            gvOutgoingAndIncomingDocuments.DataSource = dt;
            gvOutgoingAndIncomingDocuments.DataBind();
            gvOutgoingAndIncomingDocuments.Focus();
        }

        protected void btnShowAllEvents_ServerClick(object sender, EventArgs e)
        {
            DataTable dt = LoadEvents();
            gvEvents.AllowPaging = false;
            gvEvents.DataSource = dt;
            gvEvents.DataBind();
            gvEvents.Focus();
        }

        protected void gvEvents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Sort"))
            {
                string sortingDirection = string.Empty;
                if (directionEvents == SortDirection.Ascending)
                {
                    directionEvents = SortDirection.Descending;
                    sortingDirection = "Desc";
                }
                else
                {
                    directionEvents = SortDirection.Ascending;
                    sortingDirection = "Asc";
                }
                DataView sortedView = new DataView(LoadEvents());
                sortedView.Sort = e.CommandArgument + " " + sortingDirection;
                Session["SortedView"] = sortedView;
                gvEvents.DataSource = sortedView;
                gvEvents.DataBind();
                gvEvents.Focus();
            }
        }
        public string ToDate(object input)
        {
            try
            {
                return Convert.ToDateTime(input).ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {
            }
            return "";
        }
        public string ToTime(object input)
        {
            try
            {
                return Convert.ToDateTime(input).ToString("hh:mm tt");
            }
            catch (Exception ex)
            {
            }
            return "";
        }
    }
}