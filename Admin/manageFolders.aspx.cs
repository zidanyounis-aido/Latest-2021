using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using dms.Utilities;

namespace dms.Admin
{
    public partial class manageFolders : System.Web.UI.Page
    {
        UserData _userData = new UserData();
        DMS.DAL.operations op = new DMS.DAL.operations();
        DataTable treeDT = new DataTable();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        tables.dbo.users usersTB = new tables.dbo.users();
        tables.dbo.folders fldTB = new tables.dbo.folders();
        string minusPath = "/Assets/UIKIT/minus.png";
        string plusPath = "/Assets/UIKIT/plus.png";
        Int32 fldrID; string fldrName; Int32 fldrParentID; bool active;
        Int32 iconID; Int32 defaultDocTypID; Int32 folderOrder; bool isDiwan;
        Int32 folderOwner;
        string fldrNameAr; bool allowWF; Int32 ClientId;
        bool allowIncoming;bool allowOutgoing;


        public void fillVariables()
        {
            fldrID = c.convertToInt32(txtFldrID.Text);
            fldrName = c.convertToString(txtFldrName.Text);
            fldrNameAr = txtFldrNameAr.Text;
            fldrParentID = c.convertToInt32(drpFldrParentID.SelectedValue);
            active = c.convertToBool(chkActive.Checked);
            iconID = c.convertToInt32(drpIconID.SelectedValue);
            folderOwner = c.convertToInt32(dropFolderOwnr.SelectedValue);
            defaultDocTypID = c.convertToInt32(drpDefaultDocTypID.SelectedValue);

            folderOrder = c.convertToInt32(drpFolderOrder.SelectedValue);
            isDiwan = chkIsDiwan.Checked;

            allowWF = chkAllowWF.Checked;

            ClientId = c.convertToInt32(Session["clientId"]);
        }

        public void fillData(DataTable DT)
        {
            c.fillData(DT, 0, fldTB.columnsArray, Page);

            if (System.IO.File.Exists(Server.MapPath("../Images/dbIcons/") + drpIconID.SelectedValue + "-32.png"))
            {
                imgIcon.Src = "../Images/dbIcons/" + drpIconID.SelectedValue + "-32.png";
                imgIcon.Style["visibility"] = "visible";
            }
            else
                imgIcon.Style["visibility"] = "hidden";

            fillFolderCompanies();
            fillFolderBranches();
            fillGroups();
            fillUsers();



        }

        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                lblFormMode.Text = " اضافة مجلد جديد";
                Label1.Text = "رقم المجلد";
                Label2.Text = "اسم المجلد (بالانجليزية)";
                lblFldrNameAr.Text = "اسم المجلد (بالعربي)";
                Label3.Text = "المجلد الام";
                Label4.Text = "النموذج الافتراضي";
                Label5.Text = "ترتيب المجلد";
                chkIsDiwan.Text = "هل هو مجلد ديوان";
                chkActive.Text = "فعال";
                chkAllowWF.Text = "السماح بتحويل المستندات";
                rdoSaveMethod.Items.FindByValue("0").Text = "مجلد جديد";
                rdoSaveMethod.Items.FindByValue("1").Text = "مجلد حالي";
                grdFolderCompanies.Columns[0].HeaderText = "الرقم";
                grdFolderCompanies.Columns[1].HeaderText = "اسم الشركة";

                lblCompany.Text = "الشركة";

                Label8.Text = "الشركة:";
                Label9.Text = "الفرع";
                grdBranches.Columns[0].HeaderText = "الرقم";
                grdBranches.Columns[1].HeaderText = "اسم الشركة";
                grdBranches.Columns[2].HeaderText = "اسم الفرع";
                grdGroups.Columns[0].HeaderText = "الرقم";
                grdGroups.Columns[1].HeaderText = "اسم المجموعة";
                grdGroups.Columns[2].HeaderText = "اضافة";
                grdGroups.Columns[3].HeaderText = "تحديث";
                grdGroups.Columns[4].HeaderText = "حذف";
                grdUsers.Columns[0].HeaderText = "الرقم";
                grdUsers.Columns[1].HeaderText = "الاسم الكامل";
                grdUsers.Columns[2].HeaderText = "عرض";
                grdUsers.Columns[3].HeaderText = "اضافة";
                grdUsers.Columns[4].HeaderText = "تحديث";
                grdUsers.Columns[5].HeaderText = "مسح";
            }


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            if (!IsPostBack)
            {
                fillTree();
                fillDrp();

            }
        }

        public void fillGroups()
        {
            if (txtFldrID.Text != "")
            {
                string sql = "SELECT     dbo.groupFolders.grpID, dbo.groups.grpDesc"
                    + ",dbo.groupFolders.allowInsert,dbo.groupFolders.allowUpdate,dbo.groupFolders.allowDelete "
                    + " FROM         dbo.groups INNER JOIN dbo.groupFolders ON dbo.groups.grpID = dbo.groupFolders.grpID"
                    + $" WHERE     (dbo.groupFolders.fldrID = { txtFldrID.Text }) ";
                // + $" WHERE     (dbo.groupFolders.fldrID = { txtFldrID.Text }) and dbo.groups.clientId = {_userData.ClientId}";
                DataTable DT = c.GetDataAsDataTable(sql);
                grdGroups.DataSource = DT;
                grdGroups.DataBind();
                try
                {
                    grdGroups.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grdGroups.FooterRow.TableSection = TableRowSection.TableFooter;
                }
                catch (Exception)
                {
                }
            }
        }

        public void fillUsers()
        {
            if (txtFldrID.Text != "")
            {
                string sql = "SELECT     dbo.userFolders.userID, dbo.users.fullName"
                    + ",dbo.userFolders.allow,dbo.userFolders.allowInsert,dbo.userFolders.allowUpdate,dbo.userFolders.allowDelete "
                    + " FROM         dbo.userFolders INNER JOIN dbo.users ON dbo.userFolders.userID = dbo.users.userID"
                    + $" WHERE     (dbo.userFolders.fldrID = { txtFldrID.Text })";
                //  + $" WHERE     (dbo.userFolders.fldrID = { txtFldrID.Text }) and  dbo.users.clientId = {_userData.ClientId }";

                DataTable DT = c.GetDataAsDataTable(sql);
                grdUsers.DataSource = DT;
                grdUsers.DataBind();
                try
                {
                    grdUsers.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grdUsers.FooterRow.TableSection = TableRowSection.TableFooter;
                }
                catch (Exception)
                {
                }
            }
        }

        private bool nodeHasChilds(Int32 fldrID)
        {
            if (treeDT.Select("fldrParentID=" + fldrID.ToString()).Length > 0)
                return true;
            else
                return false;
        }

        public void fillDrp()
        {
            tables.dbo.groups grpTB = new tables.dbo.groups();
            grpTB = op.dboGetAllGroups();
            c.FillDropDownList(drpGrpID, grpTB.table);
            // fill users table 
            tables.dbo.users users = new tables.dbo.users();
            users = op.dboGetAllUsers();
            c.FillDropDownList(dropFolderOwnr, users.table);


            tables.dbo.icons iconsTB = new tables.dbo.icons();
            iconsTB = op.dboGetAllIcons();
            c.FillDropDownList(drpIconID, iconsTB.table);

            op = new DMS.DAL.operations();
            tables.dbo.users usersTB = new tables.dbo.users();
            usersTB = op.dboGetAllUsers();
            c.FillDropDownList(drpUserID, usersTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "userID", "FullName");

            op = new DMS.DAL.operations();
            tables.dbo.docTypes docTypesTB = new tables.dbo.docTypes();
            docTypesTB = op.dboGetAllDocTypes();
            if (Session["lang"].ToString() == "0")
                c.FillDropDownList(drpDefaultDocTypID, docTypesTB.table);
            else
                c.FillDropDownList(drpDefaultDocTypID, docTypesTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "DocTypID", "DocTypDescAr");

            op = new DMS.DAL.operations();
            tables.dbo.companies companiesTB = new tables.dbo.companies();
            companiesTB = op.dboGetAllCompanies();
            if (Session["lang"].ToString() == "0")
            {
                c.FillDropDownList(drpCompanyID, companiesTB.table);
                c.FillDropDownList(drpCompanyID0, companiesTB.table);

            }
            else
            {
                c.FillDropDownList(drpCompanyID, companiesTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "companyID", "companyNameAr");
                c.FillDropDownList(drpCompanyID0, companiesTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "companyID", "companyNameAr");
            }



        }

        private void fillTree()
        {
            trvFolders.CollapseImageUrl = minusPath;
            trvFolders.ExpandImageUrl = plusPath;

            trvFolders.Nodes.Clear();

            op = new DMS.DAL.operations();
            treeDT = op.dboGetAllFolders().table;


            if (Session["lang"].ToString() == "0")
                c.FillDropDownList(drpFldrParentID, treeDT);
            else
                c.FillDropDownList(drpFldrParentID, treeDT, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "fldrID", "fldrNameAr");

            if (Session["lang"].ToString() == "1")
                drpFldrParentID.Items[0].Text = "-- مجلد رئيسي --";
            else
                drpFldrParentID.Items[0].Text = "-- Main Folder --";

            fillOrders();

            foreach (DataRow R in treeDT.Select("fldrParentID=0"))
            {
                string icon = "<svg xmlns='http://www.w3.org/2000/svg' style='margin-left: 5px;' width='15.378' height='13.729' viewBox='0 0 9.378 7.729'> <g id='Group_3025' data-name='Group 3025' transform='translate(-1630 -394)'> <path id='Path_6934' data-name='Path 6934' d='M254.34,75.824h4.06a.141.141,0,0,0,.141-.141v-.543A.141.141,0,0,0,258.4,75h-4.447a.141.141,0,0,0-.115.223l.388.543A.141.141,0,0,0,254.34,75.824Z' transform='translate(1380.838 319.396)' fill='#484848'></path> <path id='Path_6935' data-name='Path 6935' d='M3.056,45H.763A.763.763,0,0,0,0,45.763v6.2a.763.763,0,0,0,.763.763H8.615a.763.763,0,0,0,.763-.763v-4.28a.763.763,0,0,0-.763-.763h-3.4a.763.763,0,0,1-.621-.32L3.677,45.32A.763.763,0,0,0,3.056,45Z' transform='translate(1630 349)' fill='#484848'></path> </g> </svg>";
                TreeNode node = new TreeNode();
                if (Session["lang"].ToString() == "0")
                    node.Text = icon + R["fldrName"].ToString();
                else
                    node.Text = icon + R["fldrNameAr"].ToString();
                node.Value = R["fldrID"].ToString();

                if (System.IO.File.Exists(Server.MapPath("../Images/dbIcons/") + R["iconID"].ToString() + "-16.png"))
                    //node.ImageUrl = "../Images/dbIcons/" + R["iconID"].ToString() + "-16.png";

                    if (nodeHasChilds(c.convertToInt32(R["fldrID"])))
                        node.PopulateOnDemand = true;
                //else
                //    node.NavigateUrl = "../Screen/documentsList.aspx?fldrID=" + c.encrypt(R["fldrID"].ToString());

                trvFolders.Nodes.Add(node);
            }
            //getUserFolders
        }

        protected void trvFolders_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            op = new DMS.DAL.operations();
            treeDT = op.dboGetAllFolders().table;

            TreeNode treNode = new TreeNode();
            treNode = (TreeNode)e.Node;

            foreach (DataRow R in treeDT.Select("fldrParentID=" + treNode.Value))
            {
                string icon = "<svg xmlns='http://www.w3.org/2000/svg' style='margin-left: 5px;' width='15.378' height='13.729' viewBox='0 0 9.378 7.729'> <g id='Group_3025' data-name='Group 3025' transform='translate(-1630 -394)'> <path id='Path_6934' data-name='Path 6934' d='M254.34,75.824h4.06a.141.141,0,0,0,.141-.141v-.543A.141.141,0,0,0,258.4,75h-4.447a.141.141,0,0,0-.115.223l.388.543A.141.141,0,0,0,254.34,75.824Z' transform='translate(1380.838 319.396)' fill='#484848'></path> <path id='Path_6935' data-name='Path 6935' d='M3.056,45H.763A.763.763,0,0,0,0,45.763v6.2a.763.763,0,0,0,.763.763H8.615a.763.763,0,0,0,.763-.763v-4.28a.763.763,0,0,0-.763-.763h-3.4a.763.763,0,0,1-.621-.32L3.677,45.32A.763.763,0,0,0,3.056,45Z' transform='translate(1630 349)' fill='#484848'></path> </g> </svg>";
                TreeNode node = new TreeNode();
                if (Session["lang"].ToString() == "0")
                    node.Text = icon + R["fldrName"].ToString();
                else
                    node.Text = icon + R["fldrNameAr"].ToString();

                node.Value = R["fldrID"].ToString();
                //if (System.IO.File.Exists(Server.MapPath("../Images/dbIcons/") + R["iconID"].ToString() + "-16.png"))
                //    node.ImageUrl = "../Images/dbIcons/" + R["iconID"].ToString() + "-16.png";

                if (nodeHasChilds(c.convertToInt32(R["fldrID"])))
                    node.PopulateOnDemand = true;
                //else
                //    node.NavigateUrl = "../Screen/documentsList.aspx?fldrID=" + c.encrypt(R["fldrID"].ToString());

                treNode.ChildNodes.Add(node);
            }
        }

        protected void trvFolders_SelectedNodeChanged(object sender, EventArgs e)
        {
            fldrID = c.convertToInt32(trvFolders.SelectedNode.Value);
            tables.dbo.folders foldersTB = new tables.dbo.folders();
            op = new DMS.DAL.operations();
            foldersTB = op.dboGetFoldersByPrimaryKey(fldrID);
            fillData(foldersTB.table);

            fillOrders();
            try
            {
                drpFolderOrder.SelectedValue = foldersTB.fieldFolderOrder.ToString();
            }
            catch
            {
                drpFolderOrder.SelectedValue = "0";
            }

            fillGroups();
            fillUsers();

            rdoSaveMethod.SelectedValue = "1";
            lblFormMode.Text = "Edit Folder";
            if (Session["lang"].ToString() == "1")
                lblFormMode.Text = "تعديل المجلد";

            tblDetailsForm.Style["display"] = "block";
            if (foldersTB.fieldFolderOwner != 0)
                dropFolderOwnr.SelectedValue = foldersTB.fieldFolderOwner.ToString();
            else
                dropFolderOwnr.SelectedValue = "-32768";

            divList.Visible = false;
            divDetails.Visible = true;
            divForUsersGroups.Visible = true;
        }
        protected void btnUndo_ServerClick(object sender, EventArgs e)
        {
            divDetails.Visible = false;
            divList.Visible = true;
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            //fillVariables();
            //op = new DMS.DAL.operations();
            //Int32 compID = c.convertToInt32(txtCompanyID.Text);
            //op.dboDeleteCompaniesByPrimaryKey(compID);
            //fillCompanyFolders();
            //fillcompanies();
            divDetails.Visible = false;
            divList.Visible = true;
        }

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            lblFormMode.Text = "Add Folder";
            if (Session["lang"].ToString() == "1")
                lblFormMode.Text = "اضافة المجلد";
            divDetails.Visible = true;
            divList.Visible = false;
            btnUndo.Visible = true;
            btnDelete.Visible = false;
            divForUsersGroups.Visible = false;
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:clear_form_elements('#ContentPlaceHolder1_ContentPlaceHolder1_divDetails'); ", true);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                fillVariables();

                if (fldrParentID < 0)
                    fldrParentID = 0;

                Int32 varRes;

                if (rdoSaveMethod.SelectedValue == "0")
                {
                    op = new DMS.DAL.operations();

                    varRes = op.dboAddFolders(fldrName, fldrParentID, active, iconID, defaultDocTypID, folderOrder, isDiwan, fldrNameAr, allowWF, int.Parse(dropFolderOwnr.SelectedValue), ClientId);
                    txtFldrID.Text = varRes.ToString();
                }
                else
                {
                    if (txtFldrID.Text != "")
                        varRes = op.dboUpdateFoldersByPrimaryKey(fldrID, fldrName, fldrParentID, active, iconID, defaultDocTypID, folderOrder, isDiwan, fldrNameAr, allowWF, int.Parse(dropFolderOwnr.SelectedValue), ClientId);
                    else
                        varRes = -1;
                }

                if (varRes > -1)
                {
                    lblRes.Text = "Save successful";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "تم الحفظ بنجاح";
                    fillTree();
                    fillFolderBranches();
                    fillFolderCompanies();
                    fillGroups();
                    fillUsers();

                    drpGrpID.SelectedIndex = 0;
                    drpUserID.SelectedIndex = 0;
                    drpCompanyID.SelectedIndex = 0;
                    drpBranchID.SelectedIndex = 0;
                    drpCompanyID0.SelectedIndex = 0;

                }
                else
                {
                    lblRes.Text = "Data Not Saved";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "لم يتم الحفظ";
                }
            }

            fillTree();

            tblDetailsForm.Style["display"] = "block";
        }

        protected void btnAddGroup_Click(object sender, EventArgs e)
        {
            if (drpGrpID.SelectedIndex > 0)
            {
                fillVariables();
                op = new DMS.DAL.operations();
                op.dboAddGroupFolders(c.convertToInt32(drpGrpID.SelectedValue),
                    fldrID, true, true, true,false,false,true);
                fillGroups();
            }
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            if (drpUserID.SelectedIndex > 0)
            {
                fillVariables();
                op = new DMS.DAL.operations();
                op.dboAddUserFolders(c.convertToInt32(drpUserID.SelectedValue),
                    fldrID, true, true, true, true,false,false,true);
                fillUsers();
            }
        }

        protected void grdGroups_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            op = new DMS.DAL.operations();
            op.dboDeleteGroupFoldersByPrimaryKey(c.convertToInt32(txtFldrID.Text), c.convertToInt32(grdGroups.Rows[e.RowIndex].Cells[0].Text));
            fillGroups();

        }

        protected void grdUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            op = new DMS.DAL.operations();
            op.dboDeleteUserFoldersByPrimaryKey(c.convertToInt32(txtFldrID.Text), c.convertToInt32(grdUsers.Rows[e.RowIndex].Cells[0].Text));
            fillUsers();
        }

        protected void drpIconID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (System.IO.File.Exists(Server.MapPath("../Images/dbIcons/") + drpIconID.SelectedValue + "-32.png"))
            //{
            //    imgIcon.ImageUrl = "../Images/dbIcons/" + drpIconID.SelectedValue + "-32.png";
            //    imgIcon.Visible = true;
            //}
            //else
            //    imgIcon.Visible = false;
        }

        protected void grdGroups_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdGroups.EditIndex = e.NewEditIndex;
            fillGroups();
        }

        protected void grdGroups_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdGroups.EditIndex = -1;
            fillGroups();
        }

        protected void grdUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdUsers.EditIndex = -1;
            fillUsers();

        }

        protected void grdUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdUsers.EditIndex = e.NewEditIndex;
            fillUsers();
        }

        protected void drpCompanyID_SelectedIndexChanged(object sender, EventArgs e)
        {

            op = new DMS.DAL.operations();
            tables.dbo.branchs branchsTB = new tables.dbo.branchs();
            branchsTB = op.dboGetAllBranchs("companyID=" + drpCompanyID0.SelectedValue);

            if (Session["lang"].ToString() == "0")
                c.FillDropDownList(drpBranchID, branchsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "branchID", "branchName");
            else
                c.FillDropDownList(drpBranchID, branchsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "branchID", "branchNameAr");
        }

        protected void btnAddCompany_Click(object sender, EventArgs e)
        {
            if (drpCompanyID.SelectedIndex > 0)
            {
                op = new DMS.DAL.operations();
                op.dboAddCompanyFolders(c.convertToInt32(drpCompanyID.SelectedValue), c.convertToInt32(txtFldrID.Text));
                fillFolderCompanies();
                DataTable dt = c.GetDataAsDataTable("select [userID] from [dbo].[users] where companyID=" + drpCompanyID.SelectedValue + "and active=1");
                //var imagePaths = dt.AsEnumerble().Select(r => r.Field<string>("userID");
                foreach (DataRow row in dt.Rows)
                {
                    string str = row["userID"].ToString();
                    DataTable dx = c.GetDataAsDataTable("select * from [dbo].[userFolders] where [userID]=" + c.convertToInt32(str) + " and [fldrID]=" + c.convertToInt32(txtFldrID.Text) + "");
                    if (dx.Rows.Count == 0)
                    {
                        op.dboAddUserFolders(c.convertToInt32(str), c.convertToInt32(txtFldrID.Text), true, true, true, true,false,false,true);
                    }
                }
                fillUsers();
            }
        }

        private void fillFolderCompanies()
        {
            string sql = "SELECT     dbo.companyFolders.companyID, dbo.companies.companyName, dbo.companies.companyNameAr "
                    + " FROM         dbo.companyFolders INNER JOIN dbo.companies ON dbo.companyFolders.companyID = dbo.companies.companyID"
                    + " WHERE     (dbo.companyFolders.fldrID = " + txtFldrID.Text + ")";
            DataTable dt = new DataTable();
            dt = c.GetDataAsDataTable(sql);
            grdFolderCompanies.DataSource = dt;
            BoundField bf = (BoundField)grdFolderCompanies.Columns[1];
            if (Session["lang"].ToString() == "0")
                bf.DataField = "companyName";
            else
                bf.DataField = "companyNameAr";
            grdFolderCompanies.DataBind();
        }

        protected void grdFolderCompanies_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 companyID = c.convertToInt32(grdFolderCompanies.Rows[e.RowIndex].Cells[0].Text);
            op = new DMS.DAL.operations();
            op.dboDeleteCompanyFoldersByPrimaryKey(companyID, c.convertToInt32(txtFldrID.Text));
            fillFolderCompanies();
        }

        protected void lnkAddBranch_Click(object sender, EventArgs e)
        {
            if (drpBranchID.SelectedIndex > 0)
            {
                op = new DMS.DAL.operations();
                op.dboAddBranchFolders(c.convertToInt32(drpBranchID.SelectedValue), c.convertToInt32(txtFldrID.Text));
                fillFolderBranches();
            }
        }

        private void fillFolderBranches()
        {
            string sql = "SELECT     dbo.branchFolders.branchID, dbo.companies.companyName, dbo.companies.companyNameAr, dbo.branchs.branchName, dbo.branchs.branchNameAr FROM         dbo.branchFolders INNER JOIN"
                        + " dbo.branchs ON dbo.branchFolders.branchID = dbo.branchs.branchID INNER JOIN dbo.companies ON dbo.branchs.companyID = dbo.companies.companyID"
                        + " WHERE     (dbo.branchFolders.fldrID =" + txtFldrID.Text + ")";
            DataTable dt = new DataTable();
            dt = c.GetDataAsDataTable(sql);
            grdBranches.DataSource = dt;
            BoundField bf1 = (BoundField)grdBranches.Columns[1];
            BoundField bf2 = (BoundField)grdBranches.Columns[2];
            if (Session["lang"].ToString() == "0")
            {
                bf1.DataField = "companyName";
                bf2.DataField = "branchName";
            }
            else
            {
                bf1.DataField = "companyNameAr";
                bf2.DataField = "branchNameAr";
            }
            grdBranches.DataBind();
        }

        protected void grdBranches_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 branchID = c.convertToInt32(grdBranches.Rows[e.RowIndex].Cells[0].Text);
            op = new DMS.DAL.operations();
            op.dboDeleteBranchFoldersByPrimaryKey(branchID, c.convertToInt32(txtFldrID.Text));
            fillFolderBranches();
        }

        protected void grdUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Int32 userID; Int32 fldrID; bool allow; bool allowInsert; bool allowUpdate; bool allowDelete; bool allowCreateFldr; bool allowRenameFldr; bool allowRelocationFldr; bool inheritSubFolders;
            userID = c.convertToInt32(grdUsers.Rows[e.RowIndex].Cells[0].Text);
            fldrID = c.convertToInt32(txtFldrID.Text);
            op = new DMS.DAL.operations();
            tables.dbo.userFolders UF = op.dboGetUserFoldersByPrimaryKey(fldrID, userID);

            CheckBox chk = new CheckBox();
            chk = (CheckBox)grdUsers.Rows[e.RowIndex].Cells[2].Controls[0];
            allow = c.convertToBool(chk.Checked);
            chk = new CheckBox();
            chk = (CheckBox)grdUsers.Rows[e.RowIndex].Cells[3].Controls[0];
            allowInsert = c.convertToBool(chk.Checked);
            chk = new CheckBox();
            chk = (CheckBox)grdUsers.Rows[e.RowIndex].Cells[4].Controls[0];
            allowUpdate = c.convertToBool(chk.Checked);
            chk = new CheckBox();
            chk = (CheckBox)grdUsers.Rows[e.RowIndex].Cells[5].Controls[0];
            allowDelete = c.convertToBool(chk.Checked);

            allowIncoming = UF.fieldAllowIncoming;

            allowOutgoing = UF.fieldAllowOutgoing;

            inheritSubFolders = UF.fieldInheritSubFolders;

            op = new DMS.DAL.operations();
            op.dboUpdateUserFoldersByPrimaryKey(userID, fldrID, allow, allowInsert, allowUpdate, allowDelete, 
                allowOutgoing, allowIncoming, inheritSubFolders);

            grdUsers.EditIndex = -1;
            fillUsers();
        }

        protected void grdGroups_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Int32 grpID; Int32 fldrID; bool allow; bool allowInsert; bool allowUpdate; bool allowDelete; bool allowCreateFldr; bool allowRenameFldr; bool allowRelocationFldr; bool inheritSubFolders;
            grpID = c.convertToInt32(grdGroups.Rows[e.RowIndex].Cells[0].Text);
            fldrID = c.convertToInt32(txtFldrID.Text);
            op = new DMS.DAL.operations();
            tables.dbo.groupFolders UF = op.dboGetGroupFoldersByPrimaryKey(fldrID, grpID);

            CheckBox chk = new CheckBox();

            chk = (CheckBox)grdGroups.Rows[e.RowIndex].Cells[2].Controls[0];
            allowInsert = c.convertToBool(chk.Checked);
            chk = new CheckBox();
            chk = (CheckBox)grdGroups.Rows[e.RowIndex].Cells[3].Controls[0];
            allowUpdate = c.convertToBool(chk.Checked);
            chk = new CheckBox();
            chk = (CheckBox)grdGroups.Rows[e.RowIndex].Cells[4].Controls[0];
            allowDelete = c.convertToBool(chk.Checked);

            allowIncoming = UF.fieldAllowIncoming;

            allowOutgoing = UF.fieldAllowOutgoing;

            inheritSubFolders = UF.fieldInheritSubFolders;

            op = new DMS.DAL.operations();
            op.dboUpdateGroupFoldersByPrimaryKey(grpID, fldrID, allowInsert, allowUpdate, allowDelete, 
                allowOutgoing, allowIncoming, inheritSubFolders);

            grdGroups.EditIndex = -1;
            fillGroups();
        }

        protected void drpFldrParentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillOrders();
        }
        private void fillOrders()
        {
            Int32 parentID = c.convertToInt32(drpFldrParentID.SelectedValue);
            if (parentID < 0)
                parentID = 0;

            op = new DMS.DAL.operations();
            tables.dbo.folders fldr = op.dboGetAllFolders("fldrParentID = " + parentID.ToString());

            drpFolderOrder.Items.Clear();
            if (Session["lang"].ToString() == "1")
                drpFolderOrder.Items.Add(new ListItem("-- في البداية --", "0"));
            else
                drpFolderOrder.Items.Add(new ListItem("-- At first --", "0"));

            for (Int32 fo = 1; fo <= fldr.rowsCount; fo++)
            {
                fldr.currentIndex = fo - 1;
                if (Session["lang"].ToString() == "1")
                    drpFolderOrder.Items.Add(new ListItem("بعد - " + fldr.fieldFldrNameAr, fo.ToString()));
                else
                    drpFolderOrder.Items.Add(new ListItem("After - " + fldr.fieldFldrName, fo.ToString()));
            }


        }

    }
}