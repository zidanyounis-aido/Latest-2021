using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Araneas_ERP.screen
{
    public partial class propertiesPage : System.Web.UI.Page
    {
        ////Araneas.DAL.operations op = new Araneas.DAL.operations();
        ////CommonFunction.clsCommon c = new CommonFunction.clsCommon();

        ////protected void Page_Load(object sender, EventArgs e)
        ////{
        ////    string parentID = c.convertToString(Request.QueryString["CODEN"]);
        ////    if (parentID != "")
        ////    {

        ////        tables.dbo.B_ENTITIES BEnt = new tables.dbo.B_ENTITIES();
        ////        op = new Araneas.DAL.operations();
        ////        BEnt = op.dboGetB_ENTITIESByPrimaryKey(c.convertToInt32(parentID));

        ////        tables.dbo.B_SYSTEM BSys = new tables.dbo.B_SYSTEM();
        ////        op = new Araneas.DAL.operations();
        ////        BSys = op.dboGetAllB_SYSTEM("OCODE=" + BEnt.fieldCODET.ToString()
        ////            + " and [Type] = 3");
        ////        rptMainIcons.DataSource = BSys.table;
        ////        rptMainIcons.DataBind();
        ////    }
        //}
    }
}