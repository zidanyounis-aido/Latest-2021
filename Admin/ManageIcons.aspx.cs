using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace dms.Admin
{
    public partial class ManageIcons : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        tables.dbo.icons iconsTB = new tables.dbo.icons();

        Int32 iconID; string iconDesc;

        public void fillVariables()
        {
            iconID = c.convertToInt32(txtIconID.Text);
            iconDesc = c.convertToString(txtIconDescription.Text);

        }

        public void fillData(DataTable DT)
        {
            c.fillData(DT, 0, iconsTB.columnsArray, Page);
            if (System.IO.File.Exists(Server.MapPath("../Images/dbIcons/") + txtIconID.Text + "-32.png"))
            {
                Image1.ImageUrl = "../Images/dbIcons/" + txtIconID.Text + "-32.png";
                Image1.Visible = true;
            }
            else
                Image1.Visible = false;

        }


        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                grdIcons.Columns[0].HeaderText = "الرقم";
                grdIcons.Columns[1].HeaderText = "الصورة";
                grdIcons.Columns[2].HeaderText = "اسم الايقونة";
                Label1.Text = "رقم الايقونة";
                Label2.Text = "وصف الايقونة";
                rdoSaveMethod.Items.FindByValue("0").Text = "ايقونة جديدة";
                rdoSaveMethod.Items.FindByValue("1").Text = "ايقونة حالية";







            }


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillGroups();
            }
        }

        private void fillGroups()
        {
            op = new DMS.DAL.operations();
            iconsTB = new tables.dbo.icons();
            iconsTB = op.dboGetAllIcons();
            grdIcons.DataSource = iconsTB.table;
            grdIcons.DataBind();
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

                    varRes = op.dboAddIcons(iconDesc);
                    txtIconID.Text = varRes.ToString();

                    if (FileUpload1.HasFile)
                    { 
                        //FileUpload1.SaveAs(Server.MapPath("../Images/dbIcons/") + varRes.ToString() + "-16.png");
                        FileUpload1.SaveAs($"dbIcons/{varRes.ToString()}-16.jpg".GetImageDiskPath());
                    }

                    if (FileUpload2.HasFile)
                    {
                        //FileUpload2.SaveAs(Server.MapPath("../Images/dbIcons/") + varRes.ToString() + "-32.png");
                        FileUpload2.SaveAs($"dbIcons/{varRes.ToString()}-32.jpg".GetImageDiskPath());
                    }
                }
                else
                {
                    if (txtIconID.Text != "")
                    {
                        varRes = op.dboUpdateIconsByPrimaryKey(iconID, iconDesc);
                        if (FileUpload1.HasFile)
                        { 
                            //FileUpload1.SaveAs(Server.MapPath("../Images/dbIcons/") + iconID.ToString() + "-16.png");
                            FileUpload1.SaveAs($"dbIcons/{iconID.ToString()}-16.jpg".GetImageDiskPath());
                        }

                        if (FileUpload2.HasFile)
                        { 
                            //FileUpload2.SaveAs(Server.MapPath("../Images/dbIcons/") + iconID.ToString() + "-32.png");
                            FileUpload2.SaveAs($"dbIcons/{iconID.ToString()}-32.jpg".GetImageDiskPath());
                        }
                    }
                    else
                        varRes = -1;
                }

                if (varRes > -1)
                {
                    lblRes.Text = "Save successful";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "تم الحفظ بنجاح";
                    fillGroups();
                }
                else
                {
                    lblRes.Text = "Data Not Saved";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "لم يتم الحفظ";
                }
            
            }
        }

        protected void grdIcons_SelectedIndexChanged(object sender, EventArgs e)
        {
            iconsTB = new tables.dbo.icons();
            op = new DMS.DAL.operations();
            iconsTB = op.dboGetIconsByPrimaryKey(c.convertToInt32(grdIcons.SelectedRow.Cells[0].Text));

            fillData(iconsTB.table);
            rdoSaveMethod.SelectedValue = "1";
        }

    }
}