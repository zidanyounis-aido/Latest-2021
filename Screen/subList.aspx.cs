using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Araneas_ERP.screen
{
    public partial class subList : System.Web.UI.Page
    {
        //Araneas.DAL.operations op = new Araneas.DAL.operations();
        //CommonFunction.clsCommon c = new CommonFunction.clsCommon();

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    string parentID = c.convertToString(Request.QueryString["CODEN"]);
        //    if (parentID != "")
        //    {
        //        tables.dbo.B_SYSTEM BSys = new tables.dbo.B_SYSTEM();
        //        op = new Araneas.DAL.operations();
        //        BSys = op.dboGetAllB_SYSTEM("OCODE=" + parentID + " and [Type] = 2");

        //        for (Int32 i = 0; i < BSys.rowsCount; i++)
        //        {
        //            BSys.currentIndex = i;
        //            TreeNode nd = new TreeNode();
        //            nd.Text = BSys.fieldNAMEL;
        //            nd.Value = BSys.fieldCODEN.ToString();
        //            if(hasChilds(BSys.fieldCODEN))
        //                nd.PopulateOnDemand = true;
        //            trvCategories.Nodes.Add(nd);
        //        }
        //        //trvCategories.DataSource = BSys.table;
        //        //trvCategories.DataBind();
        //    }
        //}

        //public bool hasChilds(Int32 CODEN)
        //{
        //    tables.dbo.B_SYSTEM bsys = new tables.dbo.B_SYSTEM();
        //    op = new Araneas.DAL.operations();
        //    bsys = op.dboGetAllB_SYSTEM("OCODE=" + CODEN.ToString());
        //    return bsys.hasRows;
        //}

        //protected void trvCategories_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        //{
        //    TreeNode pNode = (TreeNode)sender;

        //    tables.dbo.B_SYSTEM BSys = new tables.dbo.B_SYSTEM();
        //    op = new Araneas.DAL.operations();
        //    BSys = op.dboGetAllB_SYSTEM("OCODE=" + pNode.Value + " and [Type] = 2");

        //    for (Int32 i = 0; i < BSys.rowsCount; i++)
        //    {
        //        TreeNode nd = new TreeNode();
        //        nd.Text = BSys.fieldNAMEL;
        //        nd.Value = BSys.fieldCODEN.ToString();
        //        if (hasChilds(BSys.fieldCODEN))
        //            nd.PopulateOnDemand = true;
        //        pNode.ChildNodes.Add(nd);
        //    }
        //}

        //protected void trvCategories_SelectedNodeChanged(object sender, EventArgs e)
        //{
        //    TreeNode pNode = (TreeNode)sender;


        //    string s = "SELECT [CODEN],[FNAME] + ' ' + [LNAME] as fullName,[ICON],[STATUS]"
        //            + " FROM [Araneas_ERP].[dbo].[B_ENTITIES]"
        //            + " Where [CODET]=" + pNode.Value;
        //    DataTable dt = new DataTable();
        //    dt = c.GetDataAsDataTable(s);
        //    grdEntities.DataSource = dt;
        //    grdEntities.DataBind();

        //}

        //protected void grdEntities_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
    }
}