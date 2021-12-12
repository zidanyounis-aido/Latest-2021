using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace dms.Controls
{
    public partial class folderTree : System.Web.UI.UserControl
    {
        string _sql;
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        DMS.DAL.operations op = new DMS.DAL.operations();

        public string selectStatment
        {
            get 
            {
                return _sql;
            }
            set
            {
                _sql = value;
                fillTree();
            }
        }

        DataTable treeDT = new DataTable();

        private bool nodeHasChilds(Int32 fldrID)
        {
            if (treeDT.Select("fldrParentID=" + fldrID.ToString()).Length > 0)
                return true;
            else
                return false;
        }

        Int32 fldrID;

        protected void Page_Load(object sender, EventArgs e)
        {}

        private void fillTree(){
            trvFolders.Nodes.Clear();


            if (!String.IsNullOrEmpty(_sql))
            {
                string companyName = "companyName";
                if (Session["lang"].ToString() == "1")
                    companyName = "companyNameAr";

                treeDT = c.GetDataAsDataTable(_sql);
                treeDT.DefaultView.Sort = companyName;
                string oldCompany = "";
                TreeNode companyNode = new TreeNode() ;

                foreach (DataRow R in treeDT.Select("fldrParentID=0"))
                {
                    if (oldCompany != R[companyName].ToString())
                    {
                        companyNode = new TreeNode();
                        oldCompany = R[companyName].ToString();
                        companyNode.Text = oldCompany;
                        companyNode.PopulateOnDemand = false;
                        trvFolders.Nodes.Add(companyNode);

                    }

                    string fldrNameFeild = "fldrName";
                    if (Session["lang"].ToString() == "1")
                        fldrNameFeild = "fldrNameAr";

                    TreeNode node = new TreeNode();
                    
                        node.Text = R[fldrNameFeild].ToString();

                    node.Value = R["fldrID"].ToString();
                    node.NavigateUrl = "javascript:selectFolder(" + R["fldrID"].ToString() + ",'" + R[fldrNameFeild].ToString() + "')";

                    if (System.IO.File.Exists(Server.MapPath("../Images/dbIcons/") + R["iconID"].ToString() + "-16.png"))
                        node.ImageUrl = "../Images/dbIcons/" + R["iconID"].ToString() + "-16.png";

                    if (nodeHasChilds(c.convertToInt32(R["fldrID"])))
                        node.PopulateOnDemand = true;
                    //else
                    //    node.NavigateUrl = "../Screen/documentsList.aspx?fldrID=" + c.encrypt(R["fldrID"].ToString());
                    
                    companyNode.ChildNodes.Add(node);
                }
                trvFolders.ExpandAll();
            }
        }

        protected void trvFolders_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            string fldrNameFeild = "fldrName";
            if (Session["lang"].ToString() == "1")
                fldrNameFeild = "fldrNameAr";

            op = new DMS.DAL.operations();
            treeDT = op.dboGetAllFolders().table;

            TreeNode treNode = new TreeNode();
            treNode = (TreeNode)e.Node;

            foreach (DataRow R in treeDT.Select("fldrParentID=" + treNode.Value))
            {
                TreeNode node = new TreeNode();
                node.Text = R[fldrNameFeild].ToString();
                node.Value = R["fldrID"].ToString();
                node.NavigateUrl = "javascript:selectFolder(" + R["fldrID"].ToString() + ",'" + R[fldrNameFeild].ToString() + "')";

                if (System.IO.File.Exists(Server.MapPath("../Images/dbIcons/") + R["iconID"].ToString() + "-16.png"))
                    node.ImageUrl = "../Images/dbIcons/" + R["iconID"].ToString() + "-16.png";

                if (nodeHasChilds(c.convertToInt32(R["fldrID"])))
                    node.PopulateOnDemand = true;
                //else
                //    node.NavigateUrl = "../Screen/documentsList.aspx?fldrID=" + c.encrypt(R["fldrID"].ToString());

                
                treNode.ChildNodes.Add(node);
            }
        }

        

    }
}