using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace dms.Admin
{
    public partial class positionsManage : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        tables.dbo.positions PositionsTB = new tables.dbo.positions();

        Int32 positionID; string positionTitle; string positionTitleAr; Int32 clientID;

        public void fillVariables()
        {
            positionID = c.convertToInt32(txtPositionID.Text);
            positionTitle = c.convertToString(txtPositionTitle.Text);
            positionTitleAr = txtPositionTitleAr.Text;
            clientID = c.convertToInt32(Session["clientId"]);
        }
        //public static string SafeSmartSubstring(this string myString)
        //{
        //    string str = String.Join(String.Empty, myString.Split(new[] { ' ' }).Select(word => word.First()))
        //    return str;
        //}
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
            c.fillData(DT, 0, PositionsTB.columnsArray, Page);
        }

        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                //grdPositions.Columns[0].HeaderText = "الرقم";
                //grdPositions.Columns[1].HeaderText = "اسم المسمى";
                lblPositionID.Text = "رقم المسمى";
                lblPositionTitle.Text = "وصف المسمى (بالانجليزية)";
                lblPositionTitleAr.Text = "وصف المسمى (بالعربي)";
                rdoSaveMethod.Items.FindByValue("0").Text = "مسمى جديد";
                rdoSaveMethod.Items.FindByValue("1").Text = "مسمى حالي";
                lblFormMode.Text = "اضافة مسمى وظيفي";
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            if (txtPositionID.Text != "")
            {
                if (Session["lang"].ToString() == "0")
                    lblFormMode.Text = "Edit Position";
                else
                    lblFormMode.Text = "تعديل مسمى وظيفي";
            }

            if (!IsPostBack)
            {
                fillPositions();
            }
        }

        private void fillPositions()
        {
            op = new DMS.DAL.operations();
            PositionsTB = new tables.dbo.positions();
            PositionsTB = op.dboGetAllPositions();
            DataRow dr = PositionsTB.table.NewRow();
            //grdPositions.DataSource = PositionsTB.table;
            dlPositions.DataSource = PositionsTB.table;
            //BoundField bf = (BoundField)grdPositions.Columns[1];
            //if (Session["lang"].ToString() == "0")
            //    bf.DataField = "positionTitle";
            //else
            //    bf.DataField = "positionTitleAr";
            dlPositions.DataBind();
            //grdPositions.DataBind();
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

                    varRes = op.dboAddPositions(positionTitle, positionTitleAr, clientID);
                    txtPositionID.Text = varRes.ToString();
                }
                else
                {
                    if (txtPositionID.Text != "")
                        varRes = op.dboUpdatePositionsByPrimaryKey(positionID, positionTitle, positionTitleAr, clientID);
                    else
                        varRes = -1;
                }

                if (varRes > -1)
                {
                    lblRes.Text = "Save successful";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "تم الحفظ بنجاح";
                    fillPositions();
                }
                else
                {
                    lblRes.Text = "Data Not Saved";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "لم يتم الحفظ";
                }
            }
            divDetails.Visible = false;
            divList.Visible = true;
        }

        protected void grdPositions_SelectedIndexChanged(object sender, EventArgs e)
        {
            PositionsTB = new tables.dbo.positions();
            op = new DMS.DAL.operations();
            PositionsTB = op.dboGetPositionsByPrimaryKey(c.convertToInt32(grdPositions.SelectedRow.Cells[0].Text));

            fillData(PositionsTB.table);
            rdoSaveMethod.SelectedValue = "1";

            if (Session["lang"].ToString() == "0")
                lblFormMode.Text = "Edit Position";
            else
                lblFormMode.Text = "تعديل مسمى وظيفي";

            lblRes.Text = "";
        }
        protected void dlPositions_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

            }
        }

        protected void lnkSelectPath_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            int ID = c.convertToInt32(btn.CommandArgument);
            if (ID > 0)
            {
                PositionsTB = new tables.dbo.positions();
                op = new DMS.DAL.operations();
                PositionsTB = op.dboGetPositionsByPrimaryKey(ID);
                fillData(PositionsTB.table);
                rdoSaveMethod.SelectedValue = "1";
                //lblFormMode.Text = HudhudResources.Resources.Admin_WorkFlow_Modifyingthecourseofaction;
                if (Session["lang"].ToString() == "0")
                    lblFormMode.Text = "Edit Position";
                else
                    lblFormMode.Text = "تعديل مسمى وظيفي";
                lblRes.Text = "";
            }
            else
            {

                rdoSaveMethod.SelectedValue = "0";
                if (Session["lang"].ToString() == "0")
                    lblFormMode.Text = "Add Position";
                else
                    lblFormMode.Text = "اضافة مسمى وظيفي";
                lblRes.Text = "";
                //lblFormMode.Text = HudhudResources.Resources.Admin_WorkFlow_Addacourseofaction;
                //rdoSaveMethod.SelectedValue = "0";
                //txtPathDesc.Text = "";
                //txtPathDescAr.Text = "";
                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "AddNew", "changeToAddNew('');", true);
                //divAddNewWFPath.Visible = false;
                //ListViewWorkFlow.Visible = false;
                //tblDetailsForm.Style["display"] = "none";
            }
            divDetails.Focus();
        }

        protected void dlPositions_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)dlPositions.Items[e.NewSelectedIndex];
            LinkButton btn = (LinkButton)item.FindControl("lnkSelect");
            int ID = c.convertToInt32(btn.CommandArgument);
            if (ID > 0)
            {
                PositionsTB = new tables.dbo.positions();
                op = new DMS.DAL.operations();
                PositionsTB = op.dboGetPositionsByPrimaryKey(ID);
                fillData(PositionsTB.table);
                rdoSaveMethod.SelectedValue = "1";
                //lblFormMode.Text = HudhudResources.Resources.Admin_WorkFlow_Modifyingthecourseofaction;
                if (Session["lang"].ToString() == "0")
                    lblFormMode.Text = "Edit Position";
                else
                    lblFormMode.Text = "تعديل مسمى وظيفي";
                lblRes.Text = "";
            }
            else
            {

                rdoSaveMethod.SelectedValue = "0";
                if (Session["lang"].ToString() == "0")
                    lblFormMode.Text = "Add Position";
                else
                    lblFormMode.Text = "اضافة مسمى وظيفي";
                lblRes.Text = "";
                //lblFormMode.Text = HudhudResources.Resources.Admin_WorkFlow_Addacourseofaction;
                //rdoSaveMethod.SelectedValue = "0";
                //txtPathDesc.Text = "";
                //txtPathDescAr.Text = "";
                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "AddNew", "changeToAddNew('');", true);
                //divAddNewWFPath.Visible = false;
                //ListViewWorkFlow.Visible = false;
                //tblDetailsForm.Style["display"] = "none";
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
            if (Session["lang"].ToString() == "0")
                lblFormMode.Text = "Add Position";
            else
                lblFormMode.Text = "اضافة مسمى وظيفي";
            divDetails.Visible = true;
            divList.Visible = false;
            btnUndo.Visible = true;
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:clear_form_elements('#ContentPlaceHolder1_ContentPlaceHolder1_divDetails'); ", true);
        }
    }
}