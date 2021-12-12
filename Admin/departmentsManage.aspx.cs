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
    public partial class departmentsManage : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        tables.dbo.departments DepartmentsTB = new tables.dbo.departments();
        string minusPath = "/Assets/UIKIT/minus.png";
        string plusPath = "/Assets/UIKIT/plus.png";
        private UserData _userData = new UserData();
        Int32 departmentID; string departmentName; Int32 headUserID; Int32 parentDepartmentID; Int32 parentID;
        string departmentNameAr;Int32 clientID;

        DataTable treeDT = new DataTable();
        public void fillVariables()
        {
            departmentID = c.convertToInt32(txtDepartmentID.Text);
            departmentName = c.convertToString(txtDepartmentName.Text);
            headUserID = c.convertToInt32(drpHeadUserID.SelectedValue);
            parentDepartmentID = c.convertToInt32(drpParentDepartmentID.SelectedValue);
            try
            {
                parentID = c.convertToInt32(dropParentID.SelectedValue);
            }
            catch (Exception)
            {

                parentID = 0;
            }
            departmentNameAr = txtDepartmentNameAr.Text;

            clientID= c.convertToInt32(Session["clientId"]);

        }

        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                lblDepartmentID.Text = "رقم الإدارة ";
                lblDepartmentName.Text = "اسم الإدارة (بالانجليزية)";
                lblDepartmentNameAr.Text = "اسم الإدارة (بالعربية)";
                lblHeadUserID.Text = "مدير الإدارة";
                lblParentDepartmentID.Text = " الفرع";
                lblDropParentID.Text = " القسم الام";
                rdoSaveMethod.Items.FindByValue("0").Text = "إدارة جديد";
                rdoSaveMethod.Items.FindByValue("1").Text = "الإدارة الحالية";
                //grdDepartments.Columns[0].HeaderText = "الرقم ";
                // grdDepartments.Columns[1].HeaderText = "اسم الإدارة";
                lblFormMode.Text = "اضافة الإدارة";

            }


        }
        public void fillData(DataTable DT)
        {
            c.fillData(DT, 0, DepartmentsTB.columnsArray, Page);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            if (!IsPostBack)
            {
                fillDepartments();
                fillDrp();
                fillParentID(0);
                fillTree();
            }
        }

        private void fillDrp()
        {
            op = new DMS.DAL.operations();
            tables.dbo.departments departmentsTB = new tables.dbo.departments();
            departmentsTB = op.dboGetAllDepartments();

            if (Session["lang"].ToString() == "0")
                c.FillDropDownList(drpParentDepartmentID, departmentsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "departmentID", "departmentName");
            else
                c.FillDropDownList(drpParentDepartmentID, departmentsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "departmentID", "departmentNameAr");

            if (Session["lang"].ToString() == "0")
                c.FillDropDownList(dropParentID, departmentsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "departmentID", "departmentName");
            else
                c.FillDropDownList(dropParentID, departmentsTB.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "departmentID", "departmentNameAr");
            op = new DMS.DAL.operations();
            tables.dbo.users usersTB = new tables.dbo.users();
            usersTB = op.dboGetAllUsers();
            c.FillDropDownList(drpHeadUserID, usersTB.table);
        }

        private void fillParentID(int id, int depId = 0)
        {

            // fill parent
            string query = "";
            if (id == 0)
            {
                query = "select * from departments";
            }
            else
            {
                query = "select * from departments where departmentID!= " + id;
            }
            DataTable dt = c.GetDataAsDataTable(query);
            dropParentID.DataSource = dt;
            dropParentID.DataValueField = "departmentID";
            if (Session["lang"].ToString() == "0")
                dropParentID.DataTextField = "departmentName";
            else
                dropParentID.DataTextField = "departmentNameAr";
            dropParentID.DataBind();
            string strLang = Session["lang"].ToString() == "0" ? "Main Department" : "رئيسي";
            dropParentID.Items.Insert(0, strLang);
            if (depId > 0)
            {
                dropParentID.SelectedValue = depId.ToString();
            }
        }

        private void fillDepartments()
        {
            op = new DMS.DAL.operations();
            DepartmentsTB = new tables.dbo.departments();
            DepartmentsTB = op.dboGetAllDepartments();
            // grdDepartments.DataSource = DepartmentsTB.table;
            //BoundField bf = (BoundField)grdDepartments.Columns[1];
            //if (Session["lang"].ToString() == "0")
            //    bf.DataField = "DepartmentName";
            //else
            //    bf.DataField = "DepartmentNameAr";
            //grdDepartments.DataBind();
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

                    varRes = op.dboAddDepartments(departmentName, headUserID, parentDepartmentID, departmentNameAr, parentID,clientID);
                    txtDepartmentID.Text = varRes.ToString();
                }
                else
                {
                    if (txtDepartmentID.Text != "")
                        varRes = op.dboUpdateDepartmentsByPrimaryKey(departmentID, departmentName, headUserID, parentDepartmentID, departmentNameAr, parentID,clientID);
                    else
                        varRes = -1;
                }

                if (varRes > -1)
                {
                    lblRes.Text = "Save successful";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "تم الحفظ بنجاح";

                    fillDepartments();
                    fillDrp();
                }
                else
                {
                    lblRes.Text = "Data Not Saved";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "لم يتم الحفظ";
                }
                trvDep.Nodes.Clear();
                fillTree();
            }
        }

        protected void grdDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DepartmentsTB = new tables.dbo.departments();
            //op = new DMS.DAL.operations();
            //DepartmentsTB = op.dboGetDepartmentsByPrimaryKey(c.convertToInt32(grdDepartments.SelectedRow.Cells[0].Text));
            //fillParentID(DepartmentsTB.fieldDepartmentID, DepartmentsTB.fieldParentparentID);
            //fillData(DepartmentsTB.table);

            //rdoSaveMethod.SelectedValue = "1";

            //if (Session["lang"].ToString() == "0")
            //    lblFormMode.Text = "Edit Department";
            //else
            //    lblFormMode.Text = "تعديل إدارة";
        }

        private void fillTree()
        {
            trvDep.CollapseImageUrl = minusPath;
            trvDep.ExpandImageUrl = plusPath;
            //trvDep.DataSource = null;
            string depNameParm = "departmentName";
            if (Session["lang"].ToString() == "1")
                depNameParm = "departmentNameAr";
            DataTable dt = this.GetData($"SELECT departmentID as Id, { depNameParm } as Name FROM departments WHERE  clientId = {_userData.ClientId} and parentID =0 or parentID is null");
            this.PopulateTreeView(dt, 0, null);
            //string companyName;
            //string folderName;
            //if (Session["lang"].ToString() == "0")
            //{
            //    companyName = "departmentName";
            //    folderName = "departmentName";
            //}
            //else
            //{
            //    companyName = "departmentNameAr";
            //    folderName = "departmentNameAr";
            //}

            ////bool isDiwan = string.IsNullOrEmpty(Request.QueryString["isDiwan"]);
            //treeDT = c.GetDataAsDataTable("select * from departments  ");//c.getUserFolders(userID, false);
            ////treeDT.DefaultView.Sort = companyName;
            //string oldCompany = "";
            //TreeNode companyNode = new TreeNode();
            ////DMS.DAL.operations op = new DMS.DAL.operations();
            ////tables.dbo.users userTB = op.dboGetUsersByPrimaryKey(c.convertToInt32(Session["userID"]));
            //DataRow[] DR;
            //DR = treeDT.Select(" parentID = 0 or parentID is null");
            //Int16 fldrID = 0;
            //if (Request.QueryString["fldrID"] != null)
            //    fldrID = c.convertToInt16(c.decrypt(Request.QueryString["fldrID"]));

            //foreach (DataRow R in DR)
            //{
            //    if (oldCompany != R[companyName].ToString())
            //    {
            //        companyNode = new TreeNode();
            //        oldCompany = R["departmentName"].ToString();
            //        companyNode.Text = oldCompany;
            //        //companyNode.PopulateOnDemand = true;
            //        trvDep.Nodes.Add(companyNode);
            //    }
            //    TreeNode node = new TreeNode();
            //    string icon = "<i class='fas fa-folder'></i> ";


            //    if (R["departmentID"].ToString() == fldrID.ToString())
            //        icon = "<i class='fas fa-folder-open'></i> ";


            //    node.Text = icon + R[folderName].ToString();
            //    node.Value = R["departmentID"].ToString();
            //    node.NavigateUrl = "javascript:selectFolder(" + R["departmentID"].ToString() + ",'" + R[folderName].ToString() + "')";

            //    //if (System.IO.File.Exists(Server.MapPath("../Images/dbIcons/") + R["iconID"].ToString() + "-16.png"))
            //    //    node.ImageUrl = "../Images/dbIcons/" + R["iconID"].ToString() + "-32.png";

            //    if (nodeHasChilds(c.convertToInt32(R["departmentID"])))
            //        node.PopulateOnDemand = true;
            //    //else
            //    //    node.NavigateUrl = "../Screen/documentsList.aspx?fldrID=" + c.encrypt(R["fldrID"].ToString());
            //    node.NavigateUrl = "../Screen/documentsList.aspx?fldrID=" + c.encrypt(R["departmentID"].ToString());
            //    companyNode.ChildNodes.Add(node);
            //}
            //trvDep.ExpandAll();
        }

        private bool nodeHasChilds(Int32 fldrID)
        {
            if (treeDT.Select("parentID=" + fldrID.ToString()).Length > 0)
                return true;
            else
                return false;
        }

        protected void trvDep_SelectedNodeChanged(object sender, EventArgs e)
        {
            EditDepartent(int.Parse(trvDep.SelectedNode.Value));
        }
        protected void trvDep_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            op = new DMS.DAL.operations();
            treeDT = op.dboGetAllDepartments().table;

            TreeNode treNode = new TreeNode();
            treNode = (TreeNode)e.Node;

            foreach (DataRow R in treeDT.Select("parentID=" + treNode.Value))
            {
                string icon = "<svg xmlns='http://www.w3.org/2000/svg' style='margin-left: 5px;' width='15.378' height='13.729' viewBox='0 0 9.378 7.729'> <g id='Group_3025' data-name='Group 3025' transform='translate(-1630 -394)'> <path id='Path_6934' data-name='Path 6934' d='M254.34,75.824h4.06a.141.141,0,0,0,.141-.141v-.543A.141.141,0,0,0,258.4,75h-4.447a.141.141,0,0,0-.115.223l.388.543A.141.141,0,0,0,254.34,75.824Z' transform='translate(1380.838 319.396)' fill='#484848'></path> <path id='Path_6935' data-name='Path 6935' d='M3.056,45H.763A.763.763,0,0,0,0,45.763v6.2a.763.763,0,0,0,.763.763H8.615a.763.763,0,0,0,.763-.763v-4.28a.763.763,0,0,0-.763-.763h-3.4a.763.763,0,0,1-.621-.32L3.677,45.32A.763.763,0,0,0,3.056,45Z' transform='translate(1630 349)' fill='#484848'></path> </g> </svg>";
                TreeNode node = new TreeNode();
                if (Session["lang"].ToString() == "0")
                    node.Text = icon + R["departmentName"].ToString();
                else
                    node.Text = icon + R["departmentNameAr"].ToString();

                node.Value = R["departmentID"].ToString();
                //if (System.IO.File.Exists(Server.MapPath("../Images/dbIcons/") + R["iconID"].ToString() + "-16.png"))
                //    node.ImageUrl = "../Images/dbIcons/" + R["iconID"].ToString() + "-16.png";

                if (nodeHasChilds(c.convertToInt32(R["departmentID"])))
                    node.PopulateOnDemand = true;
                //else
                //    node.NavigateUrl = "../Screen/documentsList.aspx?fldrID=" + c.encrypt(R["fldrID"].ToString());

                treNode.ChildNodes.Add(node);
            }
        }
        private void PopulateTreeView(DataTable dtParent, int parentId, TreeNode treeNode)
        {
                string icon = "<svg xmlns='http://www.w3.org/2000/svg' style='margin-left: 5px;' width='15.378' height='13.729' viewBox='0 0 9.378 7.729'> <g id='Group_3025' data-name='Group 3025' transform='translate(-1630 -394)'> <path id='Path_6934' data-name='Path 6934' d='M254.34,75.824h4.06a.141.141,0,0,0,.141-.141v-.543A.141.141,0,0,0,258.4,75h-4.447a.141.141,0,0,0-.115.223l.388.543A.141.141,0,0,0,254.34,75.824Z' transform='translate(1380.838 319.396)' fill='#484848'></path> <path id='Path_6935' data-name='Path 6935' d='M3.056,45H.763A.763.763,0,0,0,0,45.763v6.2a.763.763,0,0,0,.763.763H8.615a.763.763,0,0,0,.763-.763v-4.28a.763.763,0,0,0-.763-.763h-3.4a.763.763,0,0,1-.621-.32L3.677,45.32A.763.763,0,0,0,3.056,45Z' transform='translate(1630 349)' fill='#484848'></path> </g> </svg>";
            foreach (DataRow row in dtParent.Rows)
            {
                string depNameParm = "departmentName";
                if (Session["lang"].ToString() == "1")
                    depNameParm = "departmentNameAr";
                TreeNode child = new TreeNode
                {
                    Text = icon + row["Name"].ToString(),
                    Value = row["Id"].ToString(),
                };
                if (parentId == 0)
                {
                    trvDep.Nodes.Add(child);
                    DataTable dtChild = this.GetData($"SELECT departmentID as Id, { depNameParm } as Name FROM departments WHERE  clientId = {_userData.ClientId} and parentID = { child.Value}");
                    PopulateTreeView(dtChild, int.Parse(child.Value), child);
                }
                else
                {
                    treeNode.ChildNodes.Add(child);
                    string hasChield = c.GetDataAsScalar($"select top 1 departmentID from departments where  clientId = {_userData.ClientId} and parentID=" + child.Value).ToString();
                    if (hasChield != "")
                    {
                        DataTable dtChild = this.GetData($"SELECT departmentID as Id, { depNameParm } as Name FROM departments WHERE  clientId = {_userData.ClientId} and parentID = " + child.Value);
                        PopulateTreeView(dtChild, int.Parse(child.Value), child);
                    }
                }

            }
        }

        private DataTable GetData(string query)
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            DataTable dt = c.GetDataAsDataTable(query);
            return dt;
        }

        private void EditDepartent(int id)
        {
            DepartmentsTB = new tables.dbo.departments();
            op = new DMS.DAL.operations();
            DepartmentsTB = op.dboGetDepartmentsByPrimaryKey(id);
            fillParentID(DepartmentsTB.fieldDepartmentID, DepartmentsTB.fieldParentID);
            fillData(DepartmentsTB.table);

            rdoSaveMethod.SelectedValue = "1";

            if (Session["lang"].ToString() == "0")
                lblFormMode.Text = "Edit Department";
            else
                lblFormMode.Text = "تعديل إدارة";
            divDetails.Visible = true;
            divList.Visible = false;
            btnUndo.Visible = true;
            btnDelete.Visible = false;
        }
        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            lblFormMode.Text = "Add New Department";
            if (Session["lang"].ToString() == "1")
                lblFormMode.Text = "اضافة إدارة جديد";
            divDetails.Visible = true;
            divList.Visible = false;
            btnUndo.Visible = true;
            btnDelete.Visible = false;
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:clear_form_elements('#ContentPlaceHolder1_ContentPlaceHolder1_divDetails'); ", true);
        }
        protected void btnUndo_ServerClick(object sender, EventArgs e)
        {
            divDetails.Visible = false;
            divList.Visible = true;
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:clear_form_elements('#ContentPlaceHolder1_ContentPlaceHolder1_divDetails'); ", true);
        }
    }
}