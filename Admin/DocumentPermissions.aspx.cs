using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace dms.Screen
{
    public partial class DocumentPermissions : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DMS.DAL.operations op = new DMS.DAL.operations();
        
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();


        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                Button1.Text = "انشاء لوحة";
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDrp();
            }

            if (c.convertToString(Request.QueryString["img"]).Trim() != "")
                Image1.ImageUrl = "../Images/Templates/" + Request.QueryString["img"] + ".jpg";
            else
                Image1.ImageUrl = "../Images/Templates/1.jpg";

            Int16 counter = Convert.ToInt16(Convert.ToInt16(hdnCounter.Value));
            string[] panels = hdnValues.Value.Split(';');

            con = new SqlConnection();
            com = new SqlCommand();
            con.ConnectionString = c.decrypt(System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            com.Connection = con;

            for (Int16 i = 0; i < panels.Length-1; i++)
            {
                Panel pnl = new Panel();
                string[] keys = panels[i].Split(':');
                string[] values = keys[1].Split(',');

                pnl.Style.Add("left",values[0]);
                pnl.Style.Add("top", values[1]);
                pnl.Style.Add("width", values[2]);
                pnl.Style.Add("height", values[3]);
                pnl.BackColor = System.Drawing.Color.FromName("#DFC");
                pnl.CssClass = "drsElement drsMoveHandle";
                pnl.ID = keys[0];
                pnl.Attributes.Add("onmousedown", "activatePanel(this)");
                pnlMain.Controls.Add(pnl);
            }

            converttoArabic();
        }

        void fillDrp()
        {
            op = new DMS.DAL.operations();
            tables.dbo.groups grpTB = new tables.dbo.groups();
            grpTB = op.dboGetAllGroups();
            c.FillDropDownList(drpGrpID, grpTB.table);

            //op = new DMS.DAL.operations();
            //tables.dbo.users usersTB = new tables.dbo.users();
            //usersTB = op.dboGetAllUsers();
            //c.FillDropDownList(drpUserID, usersTB.table);

            op = new DMS.DAL.operations();
            tables.dbo.docTypes docTypesTB = new tables.dbo.docTypes();
            docTypesTB = op.dboGetAllDocTypes();
            c.FillDropDownList(drpDocTypes, docTypesTB.table);


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Image1.ImageUrl = "../Images/Templates/" + drpDocTypes.SelectedValue + ".jpg";
 //            class="drsElement drsMoveHandle" id="per2" onmousedown="activatePanel(this)"
 //style="left: 150px; top: 280px; width: 50px; height: 100px;
 //background: #DFC;
            Int16 counter = Convert.ToInt16(Convert.ToInt16(hdnCounter.Value) + 1);

            Panel pnl = new Panel();
            pnl.Style.Add("left", "278px");
            pnl.Style.Add("top", "377px");
            pnl.Style.Add("width","50px");
            pnl.Style.Add("height","100px");
            pnl.BackColor = System.Drawing.Color.FromName("#DFC");
            pnl.CssClass = "drsElement drsMoveHandle";
            pnl.ID = "per" + counter.ToString();
            pnl.Attributes.Add("onmousedown", "activatePanel(this)");
            hdnCounter.Value = counter.ToString();

            //Session["blocks"] 

            pnlMain.Controls.Add(pnl);

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Int16 counter = Convert.ToInt16(Convert.ToInt16(hdnCounter.Value));
            string[] panels = hdnValues.Value.Split(';');

            for (Int16 i = 0; i < panels.Length - 1; i++)
            {
                Panel pnl = new Panel();
                string[] keys = panels[i].Split(':');
                string[] values = keys[1].Split(',');
                com.CommandText = "select isnull(count(blockNum),0) from groupBlocks where blockNum=" + (i + 1).ToString();
                con.Open();
                Int16 res = Convert.ToInt16(com.ExecuteScalar());
                con.Close();
                if (res == 0)
                {
                    com.CommandText = "Insert into groupBlocks (grpID,docTypID,blockNum,blockLeft,blockTop,blockWidth,blockHeight) "
                        + " values(1,1," + (i + 1).ToString() + ",'" + values[0] + "','" + values[1] + "','" + values[2] + "','" + values[3] + "')";
                }
                else
                {
                    com.CommandText = "update groupBlocks set blockLeft='" + values[0] + "',blockTop='" + values[1] + "',blockWidth='" + values[2] + "',blockHeight='" + values[3] + "'" +
                        " where grpID=1 and docTypID=1 and blockNum=" + (i + 1).ToString();
                }
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter();
            com.CommandText = "select * from groupBlocks where grpID=1 and docTypID=1";
            DA.SelectCommand = com;
            DA.Fill(dt);
            hdnCounter.Value = dt.Rows.Count.ToString();
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                Panel pnl = new Panel();
                Int32 l; Int32 t;
                l = c.convertToInt32(dt.Rows[i]["blockLeft"].ToString().Substring(0,dt.Rows[i]["blockLeft"].ToString().Length-2));
                t = c.convertToInt32(dt.Rows[i]["blockTop"].ToString().Substring(0,dt.Rows[i]["blockLeft"].ToString().Length-2));
                l+=278;
                t+=277;

                pnl.Style.Add("left", l.ToString()+"px");
                pnl.Style.Add("top", t.ToString() + "px");
                pnl.Style.Add("width", dt.Rows[i]["blockWidth"].ToString());
                pnl.Style.Add("height", dt.Rows[i]["blockHeight"].ToString());
                pnl.BackColor = System.Drawing.Color.FromName("#DFC");
                pnl.CssClass = "drsElement drsMoveHandle";
                pnl.ID = "per" + dt.Rows[i]["blockNum"].ToString();
                pnl.Attributes.Add("onmousedown", "activatePanel(this)");
                pnlMain.Controls.Add(pnl);
            }

        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
           if(FileUpload1.HasFile)
           {
                //FileUpload1.SaveAs(Server.MapPath("../Images/Templates/") + drpDocTypes.SelectedValue + ".jpg");
                FileUpload1.SaveAs($"Templates/{drpDocTypes.SelectedValue}.jpg".GetImageDiskPath());
                //Image1.ImageUrl = "../Images/Templates/" + drpDocTypes.SelectedValue + ".jpg";
                Image1.ImageUrl = $"Templates/{drpDocTypes.SelectedValue}.jpg".GetImageFolderUrl();

            }
        }

        protected void drpGrpID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Image1.ImageUrl = "../Images/Templates/" + drpDocTypes.SelectedValue + ".jpg";
            Image1.ImageUrl = $"Templates/{drpDocTypes.SelectedValue}.jpg".GetImageFolderUrl();
            DataTable dt = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter();
            com.CommandText = "select * from groupBlocks where grpID=" + drpGrpID.SelectedValue + " and docTypID=" + drpDocTypes.SelectedValue;
            DA.SelectCommand = com;
            DA.Fill(dt);
            hdnCounter.Value = dt.Rows.Count.ToString();
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                Panel pnl = new Panel();
                Int32 l; Int32 t;
                l = c.convertToInt32(dt.Rows[i]["blockLeft"].ToString().Substring(0, dt.Rows[i]["blockLeft"].ToString().Length - 2));
                t = c.convertToInt32(dt.Rows[i]["blockTop"].ToString().Substring(0, dt.Rows[i]["blockLeft"].ToString().Length - 2));
                l += 278;
                t += 277;

                pnl.Style.Add("left", l.ToString() + "px");
                pnl.Style.Add("top", t.ToString() + "px");
                pnl.Style.Add("width", dt.Rows[i]["blockWidth"].ToString());
                pnl.Style.Add("height", dt.Rows[i]["blockHeight"].ToString());
                pnl.BackColor = System.Drawing.Color.FromName("#DFC");
                pnl.CssClass = "drsElement drsMoveHandle";
                pnl.ID = "per" + dt.Rows[i]["blockNum"].ToString();
                pnl.Attributes.Add("onmousedown", "activatePanel(this)");
                pnlMain.Controls.Add(pnl);
            }

        }

        protected void drpDocTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Image1.ImageUrl = "../Images/Templates/" + drpDocTypes.SelectedValue + ".jpg";
            Image1.ImageUrl = $"Templates/{drpDocTypes.SelectedValue}.jpg".GetImageFolderUrl();
            drpGrpID.SelectedIndex = 0;
        }
    }
}