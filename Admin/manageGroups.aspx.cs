using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace dms.Admin
{
    public partial class manageGroups : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        tables.dbo.groups groupsTB = new tables.dbo.groups();

        Int32 grpID; string grpDesc; Int32 companyID; Int32 branchID; Int32 clientID;
        bool allowOutgoing;bool allowIncoming;
        public void fillVariables()
        {
            grpID = c.convertToInt32(txtGrpID.Text);
            grpDesc = c.convertToString(txtGrpDesc.Text);
            companyID = c.convertToInt32(drpCompanyID.SelectedValue);
            branchID = c.convertToInt32(drpBranchID.SelectedValue);
            clientID = c.convertToInt32(Session["clientId"]);
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
        public void fillData(DataTable DT)
        {
            c.fillData(DT, 0, groupsTB.columnsArray, Page);

            fillGroupFolders();
            fillGroupPrograms();

            if (drpCompanyID.SelectedIndex < 1 && drpBranchID.SelectedIndex < 1)
            {
                tables.dbo.folders fldrTB = new tables.dbo.folders();
                fldrTB = op.dboGetAllFolders();
                c.FillDropDownList(drpFolders, fldrTB.table);
            }
            string s;
            if (drpCompanyID.SelectedIndex > 0)
            {
                s = "SELECT     dbo.companies.companyName,dbo.companies.companyNameAr, dbo.folders.fldrID, dbo.folders.fldrName,dbo.folders.fldrNameAr, dbo.folders.fldrParentID,dbo.folders.iconID" +
                        " FROM         dbo.companies INNER JOIN" +
                        " dbo.companyFolders ON dbo.companies.companyID = dbo.companyFolders.companyID INNER JOIN" +
                        " dbo.folders ON dbo.companyFolders.fldrID = dbo.folders.fldrID" +
                        " WHERE     (dbo.companies.companyID = " + drpCompanyID.SelectedValue + ")";
            }
            else
            {
                s = "SELECT     dbo.companies.companyName,dbo.companies.companyNameAr, dbo.folders.fldrID, dbo.folders.fldrName,dbo.folders.fldrNameAr, dbo.folders.fldrParentID,dbo.folders.iconID" +
                                   " FROM         dbo.companies INNER JOIN" +
                                   " dbo.companyFolders ON dbo.companies.companyID = dbo.companyFolders.companyID INNER JOIN" +
                                   " dbo.folders ON dbo.companyFolders.fldrID = dbo.folders.fldrID";

            }
            folderTree1.selectStatment = s;
        }

        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {

                //grdGroups.Columns[0].HeaderText = "الرقم";
                //grdGroups.Columns[1].HeaderText = "اسم المجموعة";
                lblFormMode.Text = "اضافة مجموعة جديدة";
                Label1.Text = "رقم المجموعة";
                Label2.Text = "وصف المجموعة";
                Label3.Text = "القسم";
                rdoSaveMethod.Items.FindByValue("0").Text = "مجموعة جديدة";
                rdoSaveMethod.Items.FindByValue("1").Text = "مجموعة حالية";
                Label5.Text = "الشعبة";
                chkAllowInsert.Text = "اضافة";
                chkAllowUpdate.Text = "تحديث";
                chkAllowDelete.Text = "حذف";
                chkAllowOutgoing.Text = "إنشاء ملف صادر";
                chkAllowIncoming.Text = "إضافة ملف وارد";
                
                chkInheritSubFolders.Text = "وراثة فرع";
                grdFolders.Columns[0].HeaderText = "رقم المجلد";
                grdFolders.Columns[1].HeaderText = "اسم المجلد";
                grdFolders.Columns[2].HeaderText = "اضافة";
                grdFolders.Columns[3].HeaderText = "تحديث";
                grdFolders.Columns[4].HeaderText = "حذف";
                grdFolders.Columns[5].HeaderText = "إنشاء ملف صادر";
                grdFolders.Columns[6].HeaderText = "إضافة ملف وارد";
                grdFolders.Columns[7].HeaderText = "وراثة مجلدات فرعي";
                grdPrograms.Columns[0].HeaderText = "الرقم";
                grdPrograms.Columns[1].HeaderText = "اسم البرنامج";

            }


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            if (!IsPostBack)
            {
                fillGroups();

                fillDrp();
            }

        }

        private void fillDrp()
        {
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
                c.FillDropDownList(drpBranchID, branchsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "BranchID", "BranchName");
            else
                c.FillDropDownList(drpBranchID, branchsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "BranchID", "BranchNameAr");



            op = new DMS.DAL.operations();
            tables.dbo.programs programsTB = new tables.dbo.programs();
            programsTB = op.dboGetAllPrograms();
            if (Session["lang"].ToString() == "0")
                c.FillDropDownList(drpProgramID, programsTB.table);
            else
                c.FillDropDownList(drpProgramID, programsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "programID", "programNameAr");


        }



        private void fillGroups()
        {
            op = new DMS.DAL.operations();
            groupsTB = new tables.dbo.groups();
            groupsTB = op.dboGetAllGroups();
            dlgroups.DataSource = groupsTB.table;
            dlgroups.DataBind();
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

                    varRes = op.dboAddGroups(grpDesc, companyID, branchID, clientID);
                    txtGrpID.Text = varRes.ToString();
                }
                else
                {
                    if (txtGrpID.Text != "")
                        varRes = op.dboUpdateGroupsByPrimaryKey(grpID, grpDesc, companyID, branchID, clientID);
                    else
                        varRes = -1;
                }

                if (varRes > -1)
                {
                    lblRes.Text = "Save successful";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "تم الحفظ بنجاح";
                    fillGroups();
                    fillGroupFolders();
                    fillGroupPrograms();

                    hdnFldrID.Value = "";
                    drpProgramID.SelectedIndex = 0;
                    divDetails.Visible = false;
                    divList.Visible = true;
                }
                else
                {
                    lblRes.Text = "Data Not Saved";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "لم يتم الحفظ";
                }
            }
        }

        protected void grdGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            //groupsTB = new tables.dbo.groups();
            //op = new DMS.DAL.operations();
            //groupsTB = op.dboGetGroupsByPrimaryKey(c.convertToInt32(grdGroups.SelectedRow.Cells[0].Text));

            //fillData(groupsTB.table);
            //rdoSaveMethod.SelectedValue = "1";

            //lblFormMode.Text = "Edit Group";
            //if (Session["lang"].ToString() == "1")
            //    lblFormMode.Text = "تعديل المجموعة"; 
            //tblDetailsForm.Style["display"] = "table";

            //lblRes.Text = "";
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            //op = new DMS.DAL.operations();
            //groupsTB = new tables.dbo.groups();
            //groupsTB = op.dboGetAllGroups(" grpDesc like '%" + txtUserSearch.Text + "%'");
            //grdGroups.DataSource = groupsTB.table;
            //grdGroups.DataBind();
        }

        public void fillGroupFolders()
        {
            if (txtGrpID.Text != "")
            {
                string sql = "SELECT     dbo.folders.fldrName,dbo.folders.fldrNameAr, dbo.groupFolders.fldrID,"
                    + "  dbo.groupFolders.allowInsert, dbo.groupFolders.allowUpdate, dbo.groupFolders.allowDelete, dbo.groupFolders.allowOutgoing, dbo.groupFolders.allowIncoming, dbo.groupFolders.inheritSubFolders "
                    + " FROM         dbo.groupFolders INNER JOIN"
                    + " dbo.folders ON dbo.groupFolders.fldrID = dbo.folders.fldrID"
                    + " WHERE (dbo.groupFolders.grpID = " + txtGrpID.Text + ")";
                DataTable DT = c.GetDataAsDataTable(sql);
                grdFolders.DataSource = DT;
                BoundField bf = (BoundField)grdFolders.Columns[1];

                if (Session["lang"].ToString() == "0")
                    bf.DataField = "fldrName";
                else
                    bf.DataField = "fldrNameAr";
                grdFolders.DataBind();
                try
                {
                    grdFolders.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grdFolders.FooterRow.TableSection = TableRowSection.TableFooter;
                }
                catch (Exception)
                {
                }
            }
        }

        protected void btnAddFolder_Click(object sender, EventArgs e)
        {
            if (hdnFldrID.Value != "")
            {
                op = new DMS.DAL.operations();

                Int32 fldrID; bool allowInsert; bool allowUpdate; bool allowDelete; bool allowCreateFldr; bool allowRenameFldr; bool allowRelocationFldr; bool inheritSubFolders;

                grpID = c.convertToInt32(txtGrpID.Text);
                fldrID = c.convertToInt32(hdnFldrID.Value);
                allowInsert = c.convertToBool(chkAllowInsert.Checked);
                allowUpdate = c.convertToBool(chkAllowUpdate.Checked);
                allowDelete = c.convertToBool(chkAllowDelete.Checked);
                
                allowOutgoing = c.convertToBool(chkAllowOutgoing.Checked);
                allowIncoming = c.convertToBool(chkAllowIncoming.Checked);

                inheritSubFolders = c.convertToBool(chkInheritSubFolders.Checked);

                op.dboAddGroupFolders(grpID, fldrID, allowInsert, allowUpdate, allowDelete, allowOutgoing, allowIncoming, inheritSubFolders);

                tables.dbo.users usersTB = new tables.dbo.users();
                op = new DMS.DAL.operations();
                usersTB = op.dboGetAllUsers("grpID=" + grpID.ToString());

                for (Int32 i = 0; i < usersTB.rowsCount; i++)
                {
                    usersTB.currentIndex = i;
                    op = new DMS.DAL.operations();
                    op.dboAddUserFolders(usersTB.fieldUserID, fldrID, true, allowInsert, allowUpdate, allowDelete, allowOutgoing, allowIncoming, inheritSubFolders);
                }

                fillGroupFolders();
                hdnFldrID.Value = "";
            }
        }

        protected void grdFolders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 fldrID;
            grpID = c.convertToInt32(txtGrpID.Text);
            fldrID = c.convertToInt32(grdFolders.Rows[e.RowIndex].Cells[0].Text);
            tables.dbo.users usersTB = new tables.dbo.users();
            op = new DMS.DAL.operations();
            usersTB = op.dboGetAllUsers("grpID=" + grpID.ToString());
            string userIDs = "";
            for (Int32 i = 0; i < usersTB.rowsCount; i++)
            {
                userIDs += usersTB.fieldUserID.ToString() + ",";
            }
            if (userIDs.Trim() != "")
            {
                userIDs = userIDs.Remove(userIDs.Length - 1);

                c.NonQuery("update dbo.userFolders set allow=0 where userID in (" +
                    userIDs + ") and fldrID=" + fldrID.ToString());
            }
            op = new DMS.DAL.operations();
            op.dboDeleteGroupFoldersByPrimaryKey(fldrID, grpID);
            fillGroupFolders();
        }

        protected void lnkAddProgram_Click(object sender, EventArgs e)
        {
            if (drpProgramID.SelectedIndex > 0)
            {
                grpID = c.convertToInt32(txtGrpID.Text);

                op = new DMS.DAL.operations();
                op.dboAddGroupPrograms(grpID, c.convertToInt32(drpProgramID.SelectedValue));

                tables.dbo.users usersTB = new tables.dbo.users();
                op = new DMS.DAL.operations();
                usersTB = op.dboGetAllUsers("grpID=" + grpID.ToString());

                for (Int32 i = 0; i < usersTB.rowsCount; i++)
                {
                    usersTB.currentIndex = i;
                    op = new DMS.DAL.operations();
                    op.dboAddUserPrograms(usersTB.fieldUserID, c.convertToInt32(drpProgramID.SelectedValue));
                }

                fillGroupPrograms();
            }
        }

        private void fillGroupPrograms()
        {

            string sql = "SELECT     dbo.groupPrograms.programID, dbo.programs.programName, dbo.programs.programNameAr " +
                        " FROM         dbo.groupPrograms INNER JOIN dbo.programs ON dbo.groupPrograms.programID = dbo.programs.programID" +
                        " WHERE     (dbo.groupPrograms.groupID = " + txtGrpID.Text + ")";

            DataTable DT = new DataTable();
            DT = c.GetDataAsDataTable(sql);
            grdPrograms.DataSource = DT;
            BoundField bf = (BoundField)grdPrograms.Columns[1];

            if (Session["lang"].ToString() == "0")
                bf.DataField = "programName";
            else
                bf.DataField = "programNameAr";
            grdPrograms.DataBind();
            try
            {
                grdPrograms.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdPrograms.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            catch (Exception)
            {
            }
        }

        protected void grdPrograms_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 programID;
            grpID = c.convertToInt32(txtGrpID.Text);
            programID = c.convertToInt32(grdPrograms.Rows[e.RowIndex].Cells[0].Text);
            tables.dbo.users usersTB = new tables.dbo.users();
            op = new DMS.DAL.operations();
            usersTB = op.dboGetAllUsers("grpID=" + grpID.ToString());
            string userIDs = "";
            for (Int32 i = 0; i < usersTB.rowsCount; i++)
            {
                userIDs += usersTB.fieldUserID.ToString() + ",";
            }


            if (userIDs.Trim() != "")
            {
                userIDs = userIDs.Remove(userIDs.Length - 1);
                op = new DMS.DAL.operations();
                op.dboDeleteUserPrograms("userID in (" + userIDs + ") and ProgramID=" + programID.ToString());
            }
            op = new DMS.DAL.operations();
            op.dboDeleteGroupProgramsByPrimaryKey(grpID, programID);
            fillGroupPrograms();
        }

        protected void grdFolders_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdFolders.EditIndex = -1;
            fillGroupFolders();
        }

        protected void grdFolders_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdFolders.EditIndex = e.NewEditIndex;
            fillGroupFolders();
        }

        protected void grdFolders_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Int32 grpID; Int32 fldrID; bool allowInsert; bool allowUpdate; bool allowDelete; bool allowCreateFldr; bool allowRenameFldr; bool allowRelocationFldr; bool inheritSubFolders;

            grpID = c.convertToInt32(txtGrpID.Text);
            fldrID = c.convertToInt32(grdFolders.Rows[e.RowIndex].Cells[0].Text);

            CheckBox chk = new CheckBox();
            chk = (CheckBox)grdFolders.Rows[e.RowIndex].Cells[2].Controls[0];
            allowInsert = c.convertToBool(chk.Checked);
            chk = new CheckBox();
            chk = (CheckBox)grdFolders.Rows[e.RowIndex].Cells[3].Controls[0];
            allowUpdate = c.convertToBool(chk.Checked);
            chk = new CheckBox();
            chk = (CheckBox)grdFolders.Rows[e.RowIndex].Cells[4].Controls[0];
            allowDelete = c.convertToBool(chk.Checked);
            chk = new CheckBox();
            chk = (CheckBox)grdFolders.Rows[e.RowIndex].Cells[5].Controls[0];
            allowOutgoing = c.convertToBool(chk.Checked);
            chk = new CheckBox();
            chk = (CheckBox)grdFolders.Rows[e.RowIndex].Cells[6].Controls[0];
            allowIncoming = c.convertToBool(chk.Checked);
            chk = new CheckBox();
            chk = (CheckBox)grdFolders.Rows[e.RowIndex].Cells[7].Controls[0];
            inheritSubFolders = c.convertToBool(chk.Checked);


            op = new DMS.DAL.operations();
            op.dboUpdateGroupFoldersByPrimaryKey(grpID, fldrID, allowInsert, allowUpdate, allowDelete, allowOutgoing, allowIncoming, inheritSubFolders);

            grdFolders.EditIndex = -1;
            fillGroupFolders();
        }

        protected void dl_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)dlgroups.Items[e.NewSelectedIndex];
            LinkButton btn = (LinkButton)item.FindControl("lnkSelect");
            int ID = c.convertToInt32(btn.CommandArgument);
            if (ID > 0)
            {
                groupsTB = new tables.dbo.groups();
                op = new DMS.DAL.operations();
                groupsTB = op.dboGetGroupsByPrimaryKey(ID);

                fillData(groupsTB.table);
                rdoSaveMethod.SelectedValue = "1";

                lblFormMode.Text = "Edit Group";
                if (Session["lang"].ToString() == "1")
                    lblFormMode.Text = "تعديل المجموعة";
                //tblDetailsForm.Style["display"] = "table";
                lblRes.Text = "";
            }


            divDetails.Visible = true;
            divList.Visible = false;
            btnUndo.Visible = true;
        }
        protected void btnUndo_ServerClick(object sender, EventArgs e)
        {
            divDetails.Visible = false;
            divList.Visible = true;
        }
        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            divDetails.Visible = false;
            divList.Visible = true;
        }
        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            grdFolders.DataSource = dt;
            grdFolders.DataBind();
            DataTable dt2= new DataTable();
            grdPrograms.DataSource = dt2;
            grdPrograms.DataBind();


            string s = "SELECT     dbo.companies.companyName,dbo.companies.companyNameAr, dbo.folders.fldrID, dbo.folders.fldrName,dbo.folders.fldrNameAr, dbo.folders.fldrParentID,dbo.folders.iconID" +
                                     " FROM         dbo.companies INNER JOIN" +
                                     " dbo.companyFolders ON dbo.companies.companyID = dbo.companyFolders.companyID INNER JOIN" +
                                     " dbo.folders ON dbo.companyFolders.fldrID = dbo.folders.fldrID";
            folderTree1.selectStatment = s;
            rdoSaveMethod.SelectedValue = "0";
            if (Session["lang"].ToString() == "0")
                lblFormMode.Text = "Add Group";
            else
                lblFormMode.Text = "اضافة مجموعة";
            divDetails.Visible = true;
            divList.Visible = false;
            btnUndo.Visible = true;
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:clear_form_elements('#ContentPlaceHolder1_ContentPlaceHolder1_divDetails'); ", true);
        }
    }
}