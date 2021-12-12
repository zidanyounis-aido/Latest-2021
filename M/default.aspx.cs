using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace dms.M
{
    public partial class _default : System.Web.UI.Page
    {
        public Int32 userID;

        protected override void OnInit(EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("../M/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }
        }

        SecurityNB.TextEncryption sec = new SecurityNB.TextEncryption();

        DataTable treeDT = new DataTable();
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (c.convertToString(Session["userId"]) == "")
            {
                Response.Redirect("../M/login.aspx?URL=" + Request.Url.AbsoluteUri);
            }
            else
            {
                userID = c.convertToInt32(Session["userId"]);
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
            //treeDT.DefaultView.Sort = companyName;
            string oldCompany = "";
            TreeNode companyNode = new TreeNode();
            DMS.DAL.operations op = new DMS.DAL.operations();
            tables.dbo.users userTB = op.dboGetUsersByPrimaryKey(c.convertToInt32(Session["userID"]));
            rptFolders.DataSource = treeDT;
            rptFolders.DataBind();



        }

     
    }
}