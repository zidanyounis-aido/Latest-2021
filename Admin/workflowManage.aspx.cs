using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace dms.Admin
{
    public partial class workflowManage : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        tables.dbo.workFlowPaths workFlowPathsTB = new tables.dbo.workFlowPaths();

        Int32 pathId; string pathDesc; Int32 fldrId; Int32 docTypId;
        string pathDescAr; Int32 clientID;

        public void fillVariables()
        {
            pathId = c.convertToInt32(txtPathId.Text);
            pathDesc = c.convertToString(txtPathDesc.Text);
            pathDescAr = c.convertToString(txtPathDescAr.Text);
            fldrId = c.convertToInt32(drpFldrId.SelectedValue);
            docTypId = c.convertToInt32(drpDocTypId.SelectedValue);
            clientID = c.convertToInt32(Session["clientId"]);
        }

        public void fillData(DataTable DT)
        {
            c.fillData(DT, 0, workFlowPathsTB.columnsArray, Page);
            fillDetGrid();
            pnl1.Visible = true;
        }

        public void Localize()
        {
            if (Session["lang"].ToString() == "0")
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            else
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("ar");
            lblPathId.Text = HudhudResources.Resources.Admin_WorkFlow_Tracknumber;
            rdoSaveMethod.Items.FindByValue("0").Text = HudhudResources.Resources.Admin_WorkFlow_Newworkflow;
            rdoSaveMethod.Items.FindByValue("1").Text = HudhudResources.Resources.Admin_WorkFlow_Progressofcurrentwork;
            lblPathDesc.Text = HudhudResources.Resources.Admin_WorkFlow_DescriptionpathEnglish;
            lblPathDescAr.Text = HudhudResources.Resources.Admin_WorkFlow_DescriptionpathinArabic;
            lblFldrId.Text = HudhudResources.Resources.Admin_WorkFlow_Folder;
            lblDocTypId.Text = HudhudResources.Resources.Admin_WorkFlow_filetype;
            lblSeqNo.Text = HudhudResources.Resources.Admin_WorkFlow_RankingNo;
            lblRecipientType.Text = HudhudResources.Resources.Admin_WorkFlow_Typetherecipient;
            lblRecipientID.Text = HudhudResources.Resources.Admin_WorkFlow_Therecipient;
            //lblCompanyID.Text = HudhudResources.Resources.Admin_WorkFlow_Section;
            //lblBranchID.Text = HudhudResources.Resources.Admin_WorkFlow_Division;
            lblEndOfPath.Text = HudhudResources.Resources.Admin_WorkFlow_Takeaction;
            lblApproveType.Text = HudhudResources.Resources.Admin_WorkFlow_TypeApproval;
            lblApproveType0.Text = HudhudResources.Resources.Admin_WorkFlow_Procedures;
            drpRecipientType.Items.FindByValue("1").Text = HudhudResources.Resources.Admin_WorkFlow_user;
            drpRecipientType.Items.FindByValue("2").Text = HudhudResources.Resources.Admin_WorkFlow_Collection;
            drpRecipientType.Items.FindByValue("3").Text = HudhudResources.Resources.Admin_WorkFlow_Jobtitle;
            drpRecipientType.Items.FindByValue("4").Text = HudhudResources.Resources.Admin_WorkFlow_Alone;
            drpApproveType.Items.FindByValue("3").Text = HudhudResources.Resources.Admin_WorkFlow_Oneapproval;
            chkNewDet.Text = HudhudResources.Resources.Admin_WorkFlow_NewPath;
            drpApproveType.Items.FindByValue("1").Text = HudhudResources.Resources.Admin_WorkFlow_Mustbeeveryonesapproval;
            drpApproveType.Items.FindByValue("2").Text = HudhudResources.Resources.Admin_WorkFlow_vote;
            lblDuration.Text = HudhudResources.Resources.Admin_WorkFlow_Durationofaction;
            dropDurationType.Items.FindByValue("1").Text = HudhudResources.Resources.Admin_WorkFlow_hour;
            dropDurationType.Items.FindByValue("2").Text = HudhudResources.Resources.Admin_WorkFlow_day;
            grdWFDet.Columns[0].HeaderText = HudhudResources.Resources.Admin_WorkFlow_thenumber;
            lblAddNew.InnerText = HudhudResources.Resources.Admin_WorkFlow_AddNew;
            lblSave1.InnerHtml = HudhudResources.Resources.Admin_WorkFlow_save;
            lblRetreat1.InnerHtml = HudhudResources.Resources.Admin_WorkFlow_Retreat;
            lblSurvey1.InnerHtml = HudhudResources.Resources.Admin_WorkFlow_Survey;
            lblSave2.InnerHtml = HudhudResources.Resources.Admin_WorkFlow_save;
            lblRetreat2.InnerHtml = HudhudResources.Resources.Admin_WorkFlow_Retreat;
            //lblSurvey2.InnerHtml = HudhudResources.Resources.Admin_WorkFlow_Survey;
            if (txtPathId.Text == "")
            {
                lblFormMode.Text = HudhudResources.Resources.Admin_WorkFlow_Addacourseofaction;
            }
            else
            {
                lblFormMode.Text = HudhudResources.Resources.Admin_WorkFlow_Modifyingthecourseofaction;
            }


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Localize();
            
            if (!IsPostBack)
            {
                divdrpBranchID.Visible = false;
                divdrpCompanyID.Visible = false;
                if (string.IsNullOrEmpty(Request.QueryString["PathID"]))
                {
                    divdlWorkFlows.Visible = true;
                    divDetails.Visible = false;
                }
                else
                {
                    divDetails.Visible = true;
                    divdlWorkFlows.Visible = false;
                    int ID = int.Parse(Request.QueryString["PathID"]);
                    if (ID > 0)
                    {
                        workFlowPathsTB = new tables.dbo.workFlowPaths();
                        op = new DMS.DAL.operations();
                        workFlowPathsTB = op.dboGetWorkFlowPathsByPrimaryKey(ID);

                        fillData(workFlowPathsTB.table);
                        rdoSaveMethod.SelectedValue = "1";
                        lblFormMode.Text = HudhudResources.Resources.Admin_WorkFlow_Modifyingthecourseofaction;
                        divAddNewWFPath.Visible = true;
                        ListViewWorkFlow.Visible = true;
                        tblDetailsForm.Style["display"] = "block";
                    }
                    else
                    {
                        lblFormMode.Text = HudhudResources.Resources.Admin_WorkFlow_Addacourseofaction;
                        rdoSaveMethod.SelectedValue = "0";
                        txtPathDesc.Text = "";
                        txtPathDescAr.Text = "";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "AddNew", "changeToAddNew('');", true);
                        divAddNewWFPath.Visible = false;
                        ListViewWorkFlow.Visible = false;
                        tblDetailsForm.Style["display"] = "none";
                    }
                }
                if (drpRecipientType.SelectedValue == "1")
                {
                    //drpCompanyID.Visible = false;
                    //drpBranchID.Visible = false;
                }
                else
                {
                    drpCompanyID.Visible = true;
                    drpBranchID.Visible = true;
                }
                fillworkFlowPaths();
                fillDrp();
            }
        }

        private void fillDrp()
        {
            fillDrpRecipientID();
            op = new DMS.DAL.operations();
            tables.dbo.docTypes docTypesTB = new tables.dbo.docTypes();
            docTypesTB = op.dboGetAllDocTypes();
            c.FillDropDownList(drpDocTypId, docTypesTB.table);

            op = new DMS.DAL.operations();
            tables.dbo.folders foldersTB = new tables.dbo.folders();
            foldersTB = op.dboGetAllFolders();
            c.FillDropDownList(drpFldrId, foldersTB.table);

            op = new DMS.DAL.operations();
            tables.dbo.companies companiesTB = new tables.dbo.companies();
            companiesTB = op.dboGetAllCompanies();
            if (Session["lang"].ToString() == "0")
                c.FillDropDownList(drpCompanyID, companiesTB.table);
            else
                c.FillDropDownList(drpCompanyID, companiesTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "companyID", "companyNameAr");

            op = new DMS.DAL.operations();
            tables.dbo.branchs branchsTB = new tables.dbo.branchs();
            branchsTB = op.dboGetAllBranchs();
            if (Session["lang"].ToString() == "0")
                c.FillDropDownList(drpBranchID, branchsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "branchID", "BranchName");
            else
                c.FillDropDownList(drpBranchID, branchsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "branchID", "BranchNameAr");


        }
        //protected void grdWFDet_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    //GridView1.EditIndex = e.NewEditIndex;
        //    //gvbind();
        //}
        //protected void grdWFDet_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    int userid = Convert.ToInt32(grdWFDet.DataKeys[e.RowIndex].Value.ToString());
        //    GridViewRow row = (GridViewRow)grdWFDet.Rows[e.RowIndex];
        //    Label lblID = (Label)row.FindControl("lblID");
        //    //TextBox txtname=(TextBox)gr.cell[].control[];  
        //    TextBox textName = (TextBox)row.Cells[0].Controls[0];
        //    TextBox textadd = (TextBox)row.Cells[1].Controls[0];
        //    TextBox textc = (TextBox)row.Cells[2].Controls[0];
        //    //TextBox textadd = (TextBox)row.FindControl("txtadd");  
        //    //TextBox textc = (TextBox)row.FindControl("txtc");  
        //    grdWFDet.EditIndex = -1;
        //    //conn.Open();
        //    ////SqlCommand cmd = new SqlCommand("SELECT * FROM detail", conn);  
        //    //SqlCommand cmd = new SqlCommand("update detail set name='" + textName.Text + "',address='" + textadd.Text + "',country='" + textc.Text + "'where id='" + userid + "'", conn);
        //    //cmd.ExecuteNonQuery();
        //    //conn.Close();
        //    //gvbind();
        //    //GridView1.DataBind();  
        //}
        private void fillworkFlowPaths()
        {
            op = new DMS.DAL.operations();
            workFlowPathsTB = new tables.dbo.workFlowPaths();
            workFlowPathsTB = op.dboGetAllWorkFlowPaths();
            DataRow dr = workFlowPathsTB.table.NewRow();
            dr["pathId"] = 0;
            dr["pathDesc"] = "Add New";
            dr["fldrId"] = 0;
            dr["docTypId"] = 0;
            dr["pathDescAr"] = "إضافة جديد";
            workFlowPathsTB.table.Rows.InsertAt(dr, 0);
            dlWorkFlows.DataSource = workFlowPathsTB.table;
            //BoundField BF = (BoundField)grdWorkFlows.Columns[1];
            //if (Session["lang"].ToString() == "0")
            //    BF.DataField = "pathDesc";
            //else
            //    BF.DataField = "pathDescAr";

            dlWorkFlows.DataBind();
            pnl1.Visible = true;

        }
        public string GetFirstLastChar(string strText)
        {
            return string.Format("{0} {1}", strText.Trim()[0], strText.Trim().Split(' ').Length>1? strText.Trim().Split(' ').Last()[0]: strText.Trim()[(strText.Trim().Length>1?1:0)]).ToUpper();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                fillVariables();
                Int32 varRes;

                if (rdoSaveMethod.SelectedValue == "0")
                {
                    op = new DMS.DAL.operations();

                    varRes = op.dboAddWorkFlowPaths(pathDesc, fldrId, docTypId, pathDescAr, clientID);
                    txtPathId.Text = varRes.ToString();

                    dlWorkFlows.SelectedIndex = -1;
                    fillDetGrid();
                }
                else
                {
                    if (txtPathId.Text != "")
                        varRes = op.dboUpdateWorkFlowPathsByPrimaryKey(pathId, pathDesc, fldrId, docTypId, pathDescAr, clientID);
                    else
                        varRes = -1;
                }

                if (varRes > -1)
                {
                    lblRes.Text = HudhudResources.Resources.Admin_WorkFlow_Savedsuccessfully;
                    tblDetailsForm.Style["display"] = "block";
                    divAddNewWFPath.Visible = true;
                    ListViewWorkFlow.Visible = true;
                    divAddNewWFPath.Focus();
                    fillworkFlowPaths();
                    fillDetGrid();
                    try
                    {
                        if (pathId == -32768)
                        {
                            drpRecipientType.SelectedIndex = 0;
                            if (drpRecipientType.SelectedValue == "1")
                            {
                                divdrpBranchID.Visible = false;
                                divdrpCompanyID.Visible = false;
                                drpApproveType.SelectedValue = "1";
                                drpApproveType.Visible = false;
                            }
                            else
                            {
                                divdrpBranchID.Visible = true;
                                divdrpCompanyID.Visible = true;
                                drpApproveType.SelectedValue = "1";
                                drpApproveType.Visible = true;
                            }

                            drpRecipientID.SelectedIndex = 0;
                            drpCompanyID.SelectedIndex = 0;
                            drpBranchID.SelectedIndex = 0;
                            drpApproveType.SelectedIndex = 0;
                            chkEndOfPath.Checked = false;
                            chkNewDet.Checked = true;
                            txtSeqNo.Text = "";
                        }
                    }
                    catch { }
                }
                else
                {
                    lblRes.Text = HudhudResources.Resources.Admin_WorkFlow_Notsaved;
                    LinkButton1.Focus();
                }
            }
        }

        protected void grdWorkFlows_SelectedIndexChanged(object sender, EventArgs e)
        {
            workFlowPathsTB = new tables.dbo.workFlowPaths();
            op = new DMS.DAL.operations();
            //workFlowPathsTB = op.dboGetWorkFlowPathsByPrimaryKey(c.convertToInt32(dlWorkFlows.SelectedItem.Cells[0].Text));

            fillData(workFlowPathsTB.table);
            rdoSaveMethod.SelectedValue = "1";
            lblFormMode.Text = HudhudResources.Resources.Admin_WorkFlow_Modifyingthecourseofaction;
            tblDetailsForm.Style["display"] = "block";
        }

        protected void drpRecipientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpRecipientType.SelectedValue == "1")
            {
                divdrpBranchID.Visible = false;
                divdrpCompanyID.Visible = false;
                drpApproveType.SelectedValue = "1";
                drpApproveType.Visible = false;
            }
            else
            {
                divdrpBranchID.Visible = true;
                divdrpCompanyID.Visible = true;
                drpApproveType.SelectedValue = "1";
                drpApproveType.Visible = true;
            }
            fillDrpRecipientID();
        }

        Int16 seqNo; Int32 recipientID; bool endOfPath; Int16 recipientType; Int32 companyID; Int32 branchID; Int16 approveType;

        public void fillDetVariables()
        {
            pathId = c.convertToInt32(txtPathId.Text);
            if (chkNewDet.Checked)
                seqNo = c.convertToInt16(c.GetMax("SeqNo", "WfPathDetails", "pathId=" + txtPathId.Text));
            else
                seqNo = c.convertToInt16(hdnSeqNo.Value);

            recipientID = c.convertToInt32(drpRecipientID.SelectedValue);
            endOfPath = c.convertToBool(chkEndOfPath.Checked);
            recipientType = c.convertToInt16(drpRecipientType.SelectedValue);
            if (drpRecipientType.SelectedValue == "1")
            {
                divdrpBranchID.Visible = false;
                divdrpCompanyID.Visible = false;
                drpApproveType.SelectedValue = "1";
                drpApproveType.Visible = false;
            }
            else
            {
                divdrpBranchID.Visible = true;
                divdrpCompanyID.Visible = true;
                drpApproveType.SelectedValue = "1";
                drpApproveType.Visible = true;
            }
            companyID = c.convertToInt32(drpCompanyID.SelectedValue);
            branchID = c.convertToInt32(drpBranchID.SelectedValue);
            approveType = c.convertToInt16(drpApproveType.SelectedValue);

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (drpRecipientID.SelectedIndex > 0)
            {
                op = new DMS.DAL.operations();
                fillDetVariables();

                if (chkNewDet.Checked)
                    op.dboAddWfPathDetails(pathId, seqNo, recipientID, endOfPath, recipientType, companyID, branchID, approveType, int.Parse(txtDuration.Text), int.Parse(dropDurationType.SelectedValue));
                else
                    op.dboUpdateWfPathDetailsByPrimaryKey(pathId, seqNo, recipientID, endOfPath, recipientType, companyID, branchID, approveType, int.Parse(txtDuration.Text), int.Parse(dropDurationType.SelectedValue), int.Parse(dropDurationType.SelectedValue));

                grdWFDet.SelectedIndex = -1;
                drpRecipientID.SelectedIndex = 0;
                fillDetGrid();
            }
            else
            {
                lblDet.Text = HudhudResources.Resources.Admin_WorkFlow_Pleasechoosersrecipient;
            }
            ListViewWorkFlow.Focus();
        }

        private void fillDetGrid()
        {
            op = new DMS.DAL.operations();
            tables.dbo.wfPathDetails wfDetTB = new tables.dbo.wfPathDetails();
            wfDetTB = op.dboGetAllWfPathDetails("pathID=" + txtPathId.Text + " order by seqNo");

            grdWFDet.DataSource = wfDetTB.table;
            grdWFDet.DataBind();
            DataTable dt = c.GetDataAsDataTable("select * from wfPathDetails where [pathID]=" + txtPathId.Text + "order by seqNo");
            ListViewWorkFlow.DataSource = dt;
            ListViewWorkFlow.DataBind();
        }

        protected void grdWFDet_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillVariables();
            seqNo = c.convertToInt16(grdWFDet.SelectedRow.Cells[0].Text);
            tables.dbo.wfPathDetails wfDetTB = new tables.dbo.wfPathDetails();
            DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
            wfDetTB = sp.dboGetWfPathDetailsByPrimaryKey(pathId, seqNo);

            txtSeqNo.Text = wfDetTB.fieldSeqNo.ToString();
            hdnSeqNo.Value = wfDetTB.fieldSeqNo.ToString();
            drpRecipientType.SelectedValue = wfDetTB.fieldRecipientType.ToString();
            if (drpRecipientType.SelectedValue == "1")
            {
                divdrpBranchID.Visible = false;
                divdrpCompanyID.Visible = false;
                drpApproveType.SelectedValue = "1";
                drpApproveType.Visible = false;
            }
            else
            {
                divdrpBranchID.Visible = true;
                divdrpCompanyID.Visible = true;
                drpApproveType.SelectedValue = "1";
                drpApproveType.Visible = true;
            }

            fillDrpRecipientID();
            drpRecipientID.SelectedValue = wfDetTB.fieldRecipientID.ToString();
            chkEndOfPath.Checked = wfDetTB.fieldEndOfPath;
            drpCompanyID.SelectedValue = wfDetTB.fieldCompanyID.ToString();
            drpBranchID.SelectedValue = wfDetTB.fieldBranchID.ToString();
            drpApproveType.SelectedValue = wfDetTB.fieldApproveType.ToString();
            chkNewDet.Checked = false;
        }

        private void fillDrpRecipientID()
        {
            op = new DMS.DAL.operations();
            DataTable dt = new DataTable();
            string valueF = "";
            string textF = "";
            string inboxPerm = "EXISTS (select top 1 programID from dbo.userPrograms where userID=dbo.users.userID and programID=6)";
            switch (drpRecipientType.SelectedValue)
            {
                case "1":
                    tables.dbo.users usersTB = new tables.dbo.users();
                    usersTB = op.dboGetAllUsers(inboxPerm);
                    dt = usersTB.table;
                    valueF = "userID";
                    textF = "FullName";
                    break;
                case "2":
                    tables.dbo.groups grpTB = new tables.dbo.groups();
                    grpTB = op.dboGetAllGroups();
                    dt = grpTB.table;
                    valueF = "grpID";
                    textF = "grpDesc";
                    break;
                case "3":
                    tables.dbo.positions positionsTB = new tables.dbo.positions();
                    positionsTB = op.dboGetAllPositions();
                    dt = positionsTB.table;
                    valueF = "positionID";
                    if (Session["lang"].ToString() == "0")
                        textF = "positionTitle";
                    else
                        textF = "positionTitleAr";
                    break;
                case "4":
                    tables.dbo.departments departmentsTB = new tables.dbo.departments();
                    departmentsTB = op.dboGetAllDepartments();
                    dt = departmentsTB.table;
                    valueF = "departmentID";
                    if (Session["lang"].ToString() == "0")
                        textF = "departmentName";
                    else
                        textF = "departmentNameAr";
                    break;
            }
            c.FillDropDownList(drpRecipientID, dt, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", valueF, textF);
        }

        protected void grdWFDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            fillVariables();
            seqNo = c.convertToInt16(grdWFDet.Rows[e.RowIndex].Cells[0].Text);
            DMS.BLL.specialCases sp = new DMS.BLL.specialCases();
            sp.dboDeleteWfPathDetailsByPrimaryKey(pathId, seqNo);
            fillDetGrid();
        }
        protected void RowDeleting(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            //fillVariables();
            int id = c.convertToInt16(btn.CommandArgument.ToString());
            hwfID.Value = id.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showRemoveModal();", true);
        }
        protected void btnDeleteWFPath_ServerClick(object sender, EventArgs e)
        {
            //Button btn = (Button)sender;
            fillVariables();
            int id = c.convertToInt16(hwfID.Value.ToString());
            //op = new DMS.DAL.operations();
            //op.dboDeleteWfPathDetailsByPrimaryKey(pathId, seqNo);
            var x = c.NonQuery("delete  dbo.wfPathDetails where id=" + id);
            fillDetGrid();
            tblDetailsForm.Focus();
        }
        protected string getdrpRecipientType(string value)
        {
            try
            {
                string str = drpRecipientType.Items.FindByValue(value).Text;
                return str;
            }
            catch
            {
                return "";
            }
        }
        protected string getdrpRecipientID(string value, string type)
        {
            try
            {
                string str = "";
                if (type == "1")
                {
                    str = c.GetDataAsScalar("select top 1 fullName from users where userID=" + value).ToString();
                }
                else if (type == "2")
                {
                    str = c.GetDataAsScalar("select top 1 grpDesc from groups where grpID=" + value).ToString();
                }
                else if (type == "3")
                {
                    if (Session["lang"].ToString() == "1")
                        str = c.GetDataAsScalar("select top 1 positionTitleAr from positions where positionID=" + value).ToString();
                    else
                        str = c.GetDataAsScalar("select top 1 positionTitle from positions where positionID=" + value).ToString();

                }
                else
                {
                    if (Session["lang"].ToString() == "1")
                        str = c.GetDataAsScalar("select top 1 departmentNameAr from departments where departmentID=" + value).ToString();
                    else
                        str = c.GetDataAsScalar("select top 1 departmentName from departments where departmentID=" + value).ToString();
                }
                return str;
            }
            catch
            {
                return "";
            }
        }
        protected string getdrpCompanyID(string value)
        {
            try
            {
                return drpCompanyID.Items.FindByValue(value).Text;
            }
            catch
            {
                return "";
            }

        }
        protected string getdrpBranchID(string value)
        {
            try
            {
                return drpBranchID.Items.FindByValue(value).Text;
            }
            catch
            {
                return "";
            }
        }
        protected string getdrpApproveType(string value)
        {
            try
            {
                return drpApproveType.Items.FindByValue(value).Text;
            }
            catch
            {
                return "";
            }


        }

        protected void dlWorkFlows_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int ID = c.convertToInt32(e.CommandArgument.ToString());
                Response.Redirect("workflowManage.aspx?CODEN=17&dlgid=2&indexId=undefined&PathID=" + ID);
            }
        }

        protected void btnAddNewWFPath_ServerClick(object sender, EventArgs e)
        {
            RegisterStartupScript("openWorkFlowModel", "<script>openWorkFlowModel(0);</script>");
        }
    }
}