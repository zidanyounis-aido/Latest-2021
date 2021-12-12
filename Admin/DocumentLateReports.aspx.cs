using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dms.VM;
using DMS.DAL;
using Microsoft.Reporting.WebForms;
namespace dms.Admin
{
    public partial class DocumentLateReports : System.Web.UI.Page
    {
        operations op = new operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                var userid = Session["userID"].ToString();
                Hashtable parameters = new Hashtable();
                parameters.Add("@ID", 0);
                DataTable dt2 = c.GetDataAsDataTableFromSP("GetAllDocumentLate", parameters);
                List<GetAllDocumentLateVM> getAllDocumentLateVMs = new List<GetAllDocumentLateVM>();
                foreach (DataRow row in dt2.Rows)
                {
                    GetAllDocumentLateVM obj = new GetAllDocumentLateVM();
                    obj.docID = int.Parse(row["docID"].ToString());
                    obj.docName = row["docName"].ToString();
                    obj.DelayTime = row["DelayTime"].ToString() != "" ? int.Parse(row["DelayTime"].ToString()) : 0;
                    obj.userName = DMS.BLL.specialCases.GetReciptNameByIdType(int.Parse(row["recipientID"].ToString()), int.Parse(row["recipientType"].ToString()), Session["lang"].ToString());
                    obj.modifyDate =row["modifyDate"].ToString() != "" ? DateTime.Parse(row["modifyDate"].ToString()) : DateTime.Parse(row["modifyDate"].ToString()); ;
                    obj.docTypDescAr = row["docTypDescAr"].ToString();
                    obj.docTypDesc = row["docTypDesc"].ToString();
                    getAllDocumentLateVMs.Add(obj);

                }
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                string reportPath = Session["lang"].ToString() == "0" ? "~/Reports/DocumentLateReportsEn.rdlc" : "~/Reports/DocumentLateReports.rdlc";
                ReportViewer1.LocalReport.ReportPath = Server.MapPath(reportPath);
                
                ReportDataSource datasource = new ReportDataSource("DataSet1", getAllDocumentLateVMs);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                //c.FillDropDownList(drpUsers, users.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "userID", "fullName");
            }
        }

        //protected void btnShow_Click(object sender, EventArgs e)
        //{
        //    DateTime fromDate = DateTime.Today;DateTime toDate= DateTime.Today;

        //    if (txtFromDate.Text.Trim() == "")
        //    {
        //        txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
        //    }
        //    else
        //    {
        //        fromDate = c.convertToDateTime(txtFromDate.Text);
        //    }

        //    if (txtToDate.Text.Trim() == "")
        //    {
        //        txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
        //    }
        //    else
        //    {
        //        toDate = c.convertToDateTime(txtToDate.Text);
        //    }

        //    if (toDate < fromDate)
        //    {
        //        lblRes.Text = "يجب أن يكون تاريخ البداية أقل أو تساوي تاريخ النهاية";
        //    }
        //    else {
        //        lblRes.Text = "";
        //        string sqlDB = "select * from [dbo].[showAllDBEvents] where [userID]=" + drpUsers.SelectedValue + " and [eventDateTime] between '" + fromDate.ToString("MM/dd/yyyy") + " 00:00:00' and '" + toDate.ToString("MM/dd/yyyy") + " 23:59:59'";
        //        string sqlBrowse = "select * from [dbo].[showAllBrowsingEvents] where [userID]=" + drpUsers.SelectedValue + " and [eventDateTime] between '" + fromDate.ToString("MM/dd/yyyy") + " 00:00:00' and '" + toDate.ToString("MM/dd/yyyy") + " 23:59:59'";
        //        string sqlLogins = "select * from [dbo].[showAllLoginEvents] where [userID]=" + drpUsers.SelectedValue + " and [eventDateTime] between '" + fromDate.ToString("MM/dd/yyyy") + " 00:00:00' and '" + toDate.ToString("MM/dd/yyyy") + " 23:59:59'";

        //        grdShowAllDBEvents.DataSource = c.GetDataAsDataTable(sqlDB);
        //        grdShowAllDBEvents.DataBind();

        //        grdShowAllBrowsingEvents.DataSource = c.GetDataAsDataTable(sqlBrowse);
        //        grdShowAllBrowsingEvents.DataBind();

        //        grdShowAllLoginEvents.DataSource = c.GetDataAsDataTable(sqlLogins);
        //        grdShowAllLoginEvents.DataBind();
        //    }

                
        //}
    }
}