using dms.DTOS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Screen
{
    public partial class EventsDocument : System.Web.UI.Page
    {
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        public int PageNumber
        {
            get
            {
                if (gvEvents.PageIndex >= 0)
                {
                    return gvEvents.PageIndex;
                }
                return 0;
            }
        }
        public int PageSize
        {
            get
            {
                return gvEvents.PageSize;
            }
        }
        public string InsertQuery
        {
            get
            {
                return
                   @"
INSERT INTO [dbo].[Event] ([title]
      ,[description]
      ,[event_start]
      ,[event_end]
      ,[all_day]
      ,[DocumentId]
      ,[CreatedBy])VALUES(N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}')
";
            }
        }

        public string UpdateQuery
        {
            get
            {
                return
                   @"UPDATE [dbo].[Event] SET  [title] = N'{0}',[description] =N'{1}',[event_start]= N'{2}',[event_end]=N'{3}' where [event_id]= {4}";
            }
        }
        public string DeleteQuery
        {
            get
            {
                return @" DELETE FROM [dbo].[Event] WHERE [event_id] = {0}";
            }
        }
        public string DocumentId
        {
            get
            {
                return Request.Params["docId"];
                //return document id
                //return "257";

            }
        }
        public string ListQuery
        {
            get
            {

                return string.Format(@"SELECT 
  evnt.event_id,
  createdbyUser.fullName as 'CreatedBy',
  evnt.title,
  evnt.event_start,
  evnt.event_end
   FROM 
  [dbo].[Event] as evnt 
  LEFT JOIN [dbo].[users] as createdbyUser on evnt.CreatedBy = createdbyUser.[userID]
  WHERE evnt.DocumentId = {2}
  order by evnt.event_id desc
  OFFSET {0} * ({1}) ROWS
  FETCH NEXT {0} ROWS ONLY;
", PageSize, PageNumber, DocumentId);
            }
        }
        public string ListQueryCount
        {
            get
            {

                return string.Format(@"
SELECT 
  COUNT(*)
   FROM 
  [dbo].[Event] as evnt 
  WHERE evnt.DocumentId = {0}
", DocumentId);
            }
        }

        public string GetItemToEdit
        {
            get
            {
                return @"
SELECT 
  evnt.event_id,
  createdbyUser.fullName as 'CreatedBy',
  evnt.title,
  evnt.event_start,
  evnt.event_end,
  evnt.description
   FROM 
  [dbo].[Event] as evnt 
  LEFT JOIN [dbo].[users] as createdbyUser on evnt.CreatedBy = createdbyUser.[userID]
  WHERE evnt.event_id = {0}
";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var defaultItem = new DropDownItem { Id = -1, Text = "اختر..." };
                var hoursDropDownItems = new List<DropDownItem>();
                var minutes = new List<DropDownItem>();
                var amPm = new List<DropDownItem>();
                var amPm2 = new List<DropDownItem>();

                for (int i = 0; i < 12; i++)
                {
                    hoursDropDownItems.Add(new DropDownItem { Id = i, Text = (i + 1).ToString() });
                }
                for (int i = 0; i < 59; i++)
                {
                    minutes.Add(new DropDownItem { Id = i, Text = (i + 1).ToString().Length > 1 ? (i + 1).ToString() : "0" + (i + 1).ToString() });
                }

                amPm.Add(new DropDownItem { Id = 1, Text = "AM" });
                amPm.Add(new DropDownItem { Id = 2, Text = "PM" });

                amPm2.Add(new DropDownItem { Id = 1, Text = "AM" });
                BindToDoListGrid();
            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        private void BindToDoListGrid()
        {
            var gridList = new List<EventsDocumentGridItems>();

            var gvToDoListResult = c.GetDataAsDataTable(ListQuery);
            for (int i = 0; i < gvToDoListResult.Rows.Count; i++)
            {

                var dateFrom = Convert.ToDateTime(gvToDoListResult.Rows[i]["event_start"].ToString());
                var dateTo = Convert.ToDateTime(gvToDoListResult.Rows[i]["event_end"].ToString());

                gridList.Add(new EventsDocumentGridItems
                {
                    event_id = Convert.ToInt32(gvToDoListResult.Rows[i]["event_id"].ToString()),
                    event_start = dateFrom.ToShortDateString() + " " + dateFrom.ToShortTimeString(),
                    event_end = dateTo.ToShortDateString() + " " + dateTo.ToShortTimeString(),
                    title = gvToDoListResult.Rows[i]["title"].ToString(),
                    CreatedBy = gvToDoListResult.Rows[i]["CreatedBy"].ToString()
                });
            }

            var gvTotalCount = c.GetDataAsDataTable(ListQueryCount);

            gvEvents.DataSource = gridList;
            gvEvents.VirtualItemCount = Convert.ToInt32(gvTotalCount.Rows[0][0].ToString());
            gvEvents.DataBind();
            try
            {
                if (gvEvents.HeaderRow != null)
                    gvEvents.HeaderRow.TableSection = TableRowSection.TableHeader;
                if (gvEvents.FooterRow != null)
                    gvEvents.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            catch (Exception)
            {
            }
            if (gridList.Count == 0)
            {
                lblNoResult.Visible = true;
                if (Request.QueryString["docId"] != null)
                {
                    lblNoResult.Text = (Session["lang"].ToString() == "0") ? "There are no events added within this document" : "لا يوجد مهام أحداث ضمن هذا المستند";
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
        }
        protected void gvEventsLists_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editCommand")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                var editResult = string.Format(GetItemToEdit, id);
 
                lblFormMode.Text = Session["lang"].ToString() == "0" ? "Edit" : "تعديل";
                divDetails.Visible = true;
                divList.Visible = false;


                hdnSelectedId.Value = id.ToString();
                var item = c.GetDataAsDataTable(editResult);

                txtEventTitleEvents.Text = item.Rows[0]["title"].ToString();
                //dd/MM/yyyy
                var startDate = Convert.ToDateTime(item.Rows[0]["event_start"].ToString());
                var endDate = Convert.ToDateTime(item.Rows[0]["event_end"].ToString());

                txtDateEvents.Text = startDate.ToString("dd/MM/yyyy");
                txtDateToEvents.Text = endDate.ToString("dd/MM/yyyy");
                txtEventsDescription.Text = item.Rows[0]["description"].ToString();
                txtTime.Value = startDate.ToString("HH:mm:ss");
                txtTimeToEvents.Value = endDate.ToString("HH:mm:ss");
               
                
                


            }
            else if (e.CommandName == "deletethisrow")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                var deleteQuery = string.Format(DeleteQuery, id);
                c.NonQuery(deleteQuery);
                BindToDoListGrid();
            }
        }
        protected void gvTaskLists_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEvents.PageIndex = e.NewPageIndex;
            BindToDoListGrid();
        }

        protected void btnAddNewEvent_Click(object sender, EventArgs e)
        {
            txtEventTitleEvents.Text = "";
            txtEventsDescription.Text = "";
            txtDateEvents.Text = "";
            txtDateToEvents.Text = "";
            lblFormMode.Text = Session["lang"].ToString() == "0" ? "Add New" : "إضافة حدث جديد";
            divDetails.Visible = true;
            divList.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //MultiView1.ActiveViewIndex = 0;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
            
                if (hdnSelectedId.Value != null && !string.IsNullOrEmpty(hdnSelectedId.Value))
                {
                    var id =Convert.ToInt32(hdnSelectedId.Value);
                    if (id > 0)
                    {
                        Update();
                        return;
                    }
                }
                Add();
            }
            catch (Exception)
            {

                
            }
   
               
                
        }
        private void Add()
        {
            try
            {
              
                var dateFrom = DateTime.ParseExact(
              txtDateEvents.Text + " " + txtTime.Value, "dd/MM/yyyy HH:mm",
              CultureInfo.InvariantCulture);
                var dateTo = DateTime.ParseExact(
               txtDateToEvents.Text + " " + txtTimeToEvents.Value, "dd/MM/yyyy HH:mm",
               CultureInfo.InvariantCulture);
                var query =
                    string.Format(InsertQuery,
                    txtEventTitleEvents.Text,
                    txtEventsDescription.Text,
                    dateFrom,
                    dateTo,
                    "0",
                   DocumentId,
                   Session["userID"]
                    );
                c.NonQuery(query);
                //MultiView1.ActiveViewIndex = 0;
                gvEvents.PageIndex = 0;
                BindToDoListGrid();
                divDetails.Visible = false;
                divList.Visible = true;
            }
            catch (Exception)
            {
                lblRes.Text = Session["lang"].ToString() == "0" ? "please fill all required fields" : "يرجى تعبئة جميع الحقول المطلوبة";
            }
        }
        private void Update()
        {
            var dateFromString = txtDateEvents.Text + " " + txtTime.Value;
            var dateToString = txtDateToEvents.Text + " " + txtTimeToEvents.Value;


            //@"UPDATE [dbo].[Event] SET  [title] = N'{0}',[description] =N'{1}',[event_start]= N'{2}',[event_end]=N'{3}' where [event_id]= {4}";
            var query =
                string.Format(UpdateQuery,
                txtEventTitleEvents.Text,
                txtEventsDescription.Text,
                dateFromString,
                dateToString,
               hdnSelectedId.Value
                );
            c.NonQuery(query);
            //MultiView1.ActiveViewIndex = 0;
            gvEvents.PageIndex = 0;
            BindToDoListGrid();
            divDetails.Visible = false;
            divList.Visible = true;
        }

        protected void btnExportEXCEL_ServerClick(object sender, EventArgs e)
        {

            string attachment = "attachment; filename=Contacts.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            // Insert below
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvEvents.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:onButtonClick(); ", true);
        }
        protected void btnUndo_ServerClick(object sender, EventArgs e)
        {
            divDetails.Visible = false;
            divList.Visible = true;
        }
    }
}