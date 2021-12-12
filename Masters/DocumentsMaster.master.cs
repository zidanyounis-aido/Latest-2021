using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace dms.Masters
{
    public partial class DocumentsMaster : System.Web.UI.MasterPage
    {
        public Int32 userID;
        string minusPath = "/Assets/UIKIT/minus.png";
        string plusPath = "/Assets/UIKIT/plus.png";
        protected override void OnInit(EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("../Screen/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }
        }

        SecurityNB.TextEncryption sec = new SecurityNB.TextEncryption();

        DataTable treeDT = new DataTable();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (c.convertToString(Session["userId"]) == "")
            {
                Response.Redirect("../Screen/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }
            else
            {
                userID = c.convertToInt32(Session["userId"]);
            }

            string url = Request.Url.AbsolutePath.ToLower();
            if (url.Contains("newdocument.aspx") || url.Contains("documentinfo.aspx"))
                tdFolders.Style.Add("display", "none");

            if (url.Contains("documentinfo.aspx"))
            {
                pnlFolders.Visible = false;
                pnlContent.Attributes["class"] = "col-xs-12";
            }

                if (!IsPostBack)
            {
                fillTree();
            }
        }

        private bool nodeHasChilds(Int32 fldrID)
        {
            if (treeDT.Select("fldrParentID=" + fldrID.ToString()).Length > 0)
                return true;
            else
                return false;
        }

        private void fillTree()
        {
            trvFolders.CollapseImageUrl = minusPath ;
            trvFolders.ExpandImageUrl = plusPath;
            string companyName;
            string folderName;
            if (Session["lang"].ToString() == "0")
            { 
                companyName = "companyName";
                folderName = "fldrName";
            }
            else
            { 
                companyName = "companyNameAr";
                folderName = "fldrNameAr";
            }
           treeDT = c.getUserFolders(userID,false);
            string oldCompany = "";
            TreeNode companyNode = new TreeNode();
            DMS.DAL.operations op=new DMS.DAL.operations();
            tables.dbo.users userTB = op.dboGetUsersByPrimaryKey(c.convertToInt32(Session["userID"]));
            DataRow[] DR;
            if (userTB.fieldCompanyID > 0)
                DR = treeDT.Select("fldrParentID=0 and companyID=" + userTB.fieldCompanyID.ToString(), "");
            else
                DR = treeDT.Select("fldrParentID = 0");
            Int16 fldrID = 0;
            if (Request.QueryString["fldrID"] != null)
                fldrID = c.convertToInt16(c.decrypt(Request.QueryString["fldrID"]));
            foreach (DataRow R in DR)
            {
                if (oldCompany != R[companyName].ToString())
                {
                    companyNode = new TreeNode();
                    oldCompany = R[companyName].ToString();
                    string iconParent = "<svg xmlns='http://www.w3.org/2000/svg' style='margin-left: 5px;' width='15.378' height='13.729' viewBox='0 0 9.378 7.729'> <g id='Group_3025' data-name='Group 3025' transform='translate(-1630 -394)'> <path id='Path_6934' data-name='Path 6934' d='M254.34,75.824h4.06a.141.141,0,0,0,.141-.141v-.543A.141.141,0,0,0,258.4,75h-4.447a.141.141,0,0,0-.115.223l.388.543A.141.141,0,0,0,254.34,75.824Z' transform='translate(1380.838 319.396)' fill='#484848'></path> <path id='Path_6935' data-name='Path 6935' d='M3.056,45H.763A.763.763,0,0,0,0,45.763v6.2a.763.763,0,0,0,.763.763H8.615a.763.763,0,0,0,.763-.763v-4.28a.763.763,0,0,0-.763-.763h-3.4a.763.763,0,0,1-.621-.32L3.677,45.32A.763.763,0,0,0,3.056,45Z' transform='translate(1630 349)' fill='#484848'></path> </g> </svg>";
                    companyNode.Text = iconParent + oldCompany;
                    //companyNode.PopulateOnDemand = true;
                    trvFolders.Nodes.Add(companyNode);
                }
                TreeNode node = new TreeNode();
                string icon = "<svg xmlns='http://www.w3.org/2000/svg' style='margin-left: 5px;' width='15.378' height='13.729' viewBox='0 0 9.378 7.729'> <g id='Group_3025' data-name='Group 3025' transform='translate(-1630 -394)'> <path id='Path_6934' data-name='Path 6934' d='M254.34,75.824h4.06a.141.141,0,0,0,.141-.141v-.543A.141.141,0,0,0,258.4,75h-4.447a.141.141,0,0,0-.115.223l.388.543A.141.141,0,0,0,254.34,75.824Z' transform='translate(1380.838 319.396)' fill='#484848'></path> <path id='Path_6935' data-name='Path 6935' d='M3.056,45H.763A.763.763,0,0,0,0,45.763v6.2a.763.763,0,0,0,.763.763H8.615a.763.763,0,0,0,.763-.763v-4.28a.763.763,0,0,0-.763-.763h-3.4a.763.763,0,0,1-.621-.32L3.677,45.32A.763.763,0,0,0,3.056,45Z' transform='translate(1630 349)' fill='#484848'></path> </g> </svg>";
                if (R["fldrID"].ToString() == fldrID.ToString())
                    icon = "<svg xmlns='http://www.w3.org/2000/svg' style='margin-left: 5px;' width='15.378' height='13.729' viewBox='0 0 9.378 7.729'> <g id='Group_3025' data-name='Group 3025' transform='translate(-1630 -394)'> <path id='Path_6934' data-name='Path 6934' d='M254.34,75.824h4.06a.141.141,0,0,0,.141-.141v-.543A.141.141,0,0,0,258.4,75h-4.447a.141.141,0,0,0-.115.223l.388.543A.141.141,0,0,0,254.34,75.824Z' transform='translate(1380.838 319.396)' fill='#484848'></path> <path id='Path_6935' data-name='Path 6935' d='M3.056,45H.763A.763.763,0,0,0,0,45.763v6.2a.763.763,0,0,0,.763.763H8.615a.763.763,0,0,0,.763-.763v-4.28a.763.763,0,0,0-.763-.763h-3.4a.763.763,0,0,1-.621-.32L3.677,45.32A.763.763,0,0,0,3.056,45Z' transform='translate(1630 349)' fill='#484848'></path> </g> </svg>";
                node.Text =icon + R[folderName].ToString();
                node.Value = R["fldrID"].ToString();
                node.NavigateUrl = "javascript:selectFolder(" + R["fldrID"].ToString() + ",'" + R[folderName].ToString() + "')";
                //if (System.IO.File.Exists(Server.MapPath("../Images/dbIcons/") + R["iconID"].ToString() + "-16.png"))
                //    node.ImageUrl = "../Images/dbIcons/" + R["iconID"].ToString() + "-32.png";
                if (nodeHasChilds(c.convertToInt32(R["fldrID"])))
                    node.PopulateOnDemand = true;
                //else
                //    node.NavigateUrl = "../Screen/documentsList.aspx?fldrID=" + c.encrypt(R["fldrID"].ToString());
                node.NavigateUrl = "../Screen/documentsList.aspx?fldrID=" + c.encrypt(R["fldrID"].ToString());
                companyNode.ChildNodes.Add(node);
            }
            trvFolders.ExpandAll();
        }

        protected void trvFolders_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            string folderName;
            if (Session["lang"].ToString() == "0")
            {
                folderName = "fldrName";
            }
            else
            {
                folderName = "fldrNameAr";
            }
            TreeNode treNode = new TreeNode();
            treNode = (TreeNode)e.Node;

            treeDT = c.getUserFoldersWithoutCompanies(userID);

            foreach (DataRow R in treeDT.Select("fldrParentID=" + e.Node.Value ))
            {

                TreeNode node = new TreeNode();
                node.Text = R[folderName].ToString();
                node.Value = R["fldrID"].ToString();
                if (System.IO.File.Exists(Server.MapPath("../Images/dbIcons/") + R["iconID"].ToString() + "-16.png"))
                    node.ImageUrl = "../Images/dbIcons/" + R["iconID"].ToString() + "-16.png";

                if(nodeHasChilds(c.convertToInt32(R["fldrID"])))
                    node.PopulateOnDemand=true;
                //else
                    
                node.NavigateUrl = "../Screen/documentsList.aspx?fldrID=" + c.encrypt(R["fldrID"].ToString());

                treNode.ChildNodes.Add(node);
                
            }
        }

        protected void trvFolders_SelectedNodeChanged(object sender, EventArgs e)
        {
            Response.Redirect("defaultAr.aspx");
        }

     
    }
}