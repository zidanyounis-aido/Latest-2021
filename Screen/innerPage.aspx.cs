using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Araneas_ERP.screen
{
    public partial class innerPage : System.Web.UI.Page
    {

        Int16 OWNER; Int32 CODET; string FNAME; string SNAME; string MNAME; string LNAME; string FNAMEL; string SNAMEL; string MNAMEL; string LNAMEL; string NAME_SHORT; string IMAGE; string ICON; Int16 STATUS;

        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        public void fillVariables()
        {
            OWNER = c.convertToInt16(txtOWNER.Text);
            CODET = c.convertToInt32(drpCODET.SelectedValue);
            FNAME = c.convertToString(txtFNAME.Text);
            SNAME = c.convertToString(txtSNAME.Text);
            MNAME = c.convertToString(txtMNAME.Text);
            LNAME = c.convertToString(txtLNAME.Text);
            FNAMEL = c.convertToString(txtFNAMEL.Text);
            SNAMEL = c.convertToString(txtSNAMEL.Text);
            MNAMEL = c.convertToString(txtMNAMEL.Text);
            LNAMEL = c.convertToString(txtLNAMEL.Text);
            NAME_SHORT = c.convertToString(txtNAME_SHORT.Text);
            IMAGE = c.convertToString(txtIMAGE.Text);
            ICON = c.convertToString(txtICON.Text);
            STATUS = c.convertToInt16(txtSTATUS.Text);

        }
        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
               grdEntities.Columns[0].HeaderText = "كود";
                 grdEntities.Columns[1].HeaderText = "الاسم الاول";
                 grdEntities.Columns[1].HeaderText = "الاسم الاخير";
                 lblCODEN.Text = "";
                 lblOWNER.Text = "المالك";
                 lblCODET.Text = "";
                 lblFNAME.Text = "الاسم الاول";
                 lblSNAME.Text = "الاسم الثاني";
                 lblMNAME.Text = "الاسم الثالث";
                 lblLNAME.Text = "الاسم الخير";
                 lblIMAGE.Text = "صورة";
                 lblICON.Text = "ايقونة";
                 lblSTATUS.Text = "حالة"; 
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Araneas.DAL.operations op = new Araneas.DAL.operations();

            //tables.dbo.B_ENTITIES b_Ent = new tables.dbo.B_ENTITIES();

            //fillVariables();
            ////op.dboAddB_ENTITIES(OWNER,CODET

            //b_Ent = op.dboGetAllB_ENTITIES("FNAME like '%" + FNAME + "%'", "CodeN Desc");

            ////b_Ent.f

            //if (b_Ent.hasRows)
            //{
            //    grdEntities.DataSource = b_Ent.table;
            //    grdEntities.DataBind();
            //}
            converttoArabic();
        }
        
    }
    
}