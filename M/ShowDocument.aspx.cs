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
    public partial class ShowDocument : System.Web.UI.Page
    {
        dms.Masters.DocumentsMaster m = new Masters.DocumentsMaster();
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        DMS.DAL.operations op = new DMS.DAL.operations();
        Int32 docID; string ext;

        public string getWFAction(Int32 actionID)
        {
            if (Session["lang"].ToString() == "1")
            {
                return c.ActionsAr[actionID];
            }
            else
            {
                return c.ActionsEn[actionID];
            }
        }

        public void fillWorkflow()
        {
            string sql = "SELECT   dbo.documentWFPath.ID,  dbo.getUserPosition(dbo.users.userID) as fullName, dbo.documentWFPath.receiveDate, dbo.documentWFPath.actionType, dbo.documentWFPath.actionDateTime,  " +
                        " dbo.documentWFPath.userNotes FROM         dbo.users INNER JOIN       dbo.documentWFPath ON dbo.users.userID = dbo.documentWFPath.userID " +
                        " WHERE  dbo.documentWFPath.actionType in (1,2,3,4,6,7) and  (dbo.documentWFPath.docID = " + docID.ToString() + ") and dbo.users.positionID in (1,2,3,4,11)";
            DataTable DT = new DataTable();
            DT = c.GetDataAsDataTable(sql);
            rptWorkflow.DataSource = DT;
            rptWorkflow.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //m = (dms.Masters.)Page.Master;

            

            if (Request.QueryString["docID"] == "")
                Response.Redirect("../Screen/", true);
            string res = Request.QueryString["docID"];
            res = c.decrypt(res);
            docID = c.convertToInt32(res);
            
            fillWorkflow();
            fillFullWorkflow();
            
            Int16 ver =1;
            if (Request.QueryString["ver"] != null)
                ver = c.convertToInt16(Request.QueryString["ver"]);

            if (ver < 1)
                ver = 1;

            op=new DMS.DAL.operations();
            tables.dbo.documents doc =new tables.dbo.documents();
            doc = op.dboGetDocumentsByPrimaryKey(docID);

            op=new DMS.DAL.operations();
            tables.dbo.documentVersions docVer =new tables.dbo.documentVersions();
            docVer = op.dboGetDocumentVersionsByPrimaryKey(docID,ver);

            ext = docVer.fieldExt;

            bool hasPer=false;

            op = new DMS.DAL.operations();
            if (op.dboGetGroupFoldersByPrimaryKey(doc.fieldFldrID, c.convertToInt32(Session["grpCode"])).hasRows)
                hasPer = true;
            else
            { 
                op = new DMS.DAL.operations();
                if (op.dboGetUserFoldersByPrimaryKey(doc.fieldFldrID, m.userID).hasRows)
                    hasPer = true;

                op = new DMS.DAL.operations();
                if (op.dboGetUserDocumentsByPrimaryKey(docID,c.convertToInt32(Session["userID"])).hasRows)
                    hasPer = true;

                op = new DMS.DAL.operations();
                tables.dbo.documentWFPath dwf = op.dboGetAllDocumentWFPath("docID=" + docID.ToString() + " and userID=" + Session["userID"].ToString());
                if (dwf.hasRows)
                    hasPer = true;
            }

            //if(hasPer)
            //{
                string filePath = Helper.GetUploadDiskPath() + doc.fieldFldrID.ToString() + "/" + doc.fieldDocID.ToString() + "-" + docVer.fieldVersion.ToString() + "." + docVer.fieldExt;
                if (!System.IO.File.Exists(filePath))
                {
                    filePath = Helper.GetUploadDiskPath() + doc.fieldDocID.ToString() + "-" + docVer.fieldVersion.ToString() + "." + docVer.fieldExt;
                }
                switch(c.getFileType(docVer.fieldExt))
                {
                    case CommonFunction.clsCommon.fileType.Image:
                        //ShowDoc();
                        Image1.ImageUrl = "../Validation.ashx?file=" + filePath;
                        break;
                    default:
                        Response.Redirect("../Validation.ashx?file=" + filePath);
                        break;
                }
            //}
            //else
            //    Response.Redirect("../Screen/", true);
        }

        public void ShowDoc(){
            string imgURL = "";
            string fName ;
            tables.dbo.documents doc =new tables.dbo.documents();
            op=new DMS.DAL.operations();
            doc = op.dboGetDocumentsByPrimaryKey(docID);
            fName = "doc_" + docID.ToString() + "." + ext ;
            if (ext.ToLower() == "jpg")
            {
                imgURL = Helper.GetUploadDiskPath(fName); //+ ".jpg";

                System.Drawing.Bitmap btmp = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(imgURL);

                System.Drawing.Graphics grp = System.Drawing.Graphics.FromImage(btmp);
                //grp.DrawImage((System.Drawing.Image)btmp, 0, 0);

                DataTable dt = new DataTable();
        
                tables.dbo.groupBlocks grpBlock = new tables.dbo.groupBlocks();
                op = new DMS.DAL.operations();
                grpBlock = op.dboGetAllGroupBlocks("groupCode=" + Session["grpCode"].ToString() + " and docTypeCode=" + doc.fieldDocTypID.ToString());
                dt = grpBlock.table;

                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    Int32 x;
                    Int32 y;
                    Int32 w;
                    Int32 h;

                    string _x = dt.Rows[i]["blockLeft"].ToString();
                    string _y = dt.Rows[i]["blockTop"].ToString();
                    string _w = dt.Rows[i]["blockwidth"].ToString();
                    string _h = dt.Rows[i]["blockHeight"].ToString();
                    if (_x.ToLower().EndsWith("px"))
                        _x = _x.Remove(_x.Length - 2);
                    if (_y.ToLower().EndsWith("px"))
                        _y = _y.Remove(_y.Length - 2);
                    if (_w.ToLower().EndsWith("px"))
                        _w = _w.Remove(_w.Length - 2);
                    if (_h.ToLower().EndsWith("px"))
                        _h = _h.Remove(_h.Length - 2);
                    x = Convert.ToInt32(_x);
                    y = Convert.ToInt32(_y) - 85;
                    w = Convert.ToInt32(_w);
                    h = Convert.ToInt32(_h);

                    System.Drawing.Pen p = new System.Drawing.Pen(System.Drawing.Brushes.WhiteSmoke);
                    p.Color = System.Drawing.Color.Gray;

                    //System.Drawing.Drawing2D.PenType.SolidColor;
                    //grp.DrawRectangle(p, x, y, w, h);
                    grp.FillRectangle(System.Drawing.Brushes.WhiteSmoke, x, y, w, h);

                }

                grp.Save();

                btmp.Save(Server.MapPath("../Images/") + "temp.jpg");

                Image1.ImageUrl = "../Images/temp.jpg";

                //grp.DrawImage(
            }

        }


        public void fillFullWorkflow()
        {
            string sql = "SELECT     dbo.users.fullName, dbo.documentWFPath.receiveDate, dbo.documentWFPath.actionType, dbo.documentWFPath.actionDateTime,  " +
                      " dbo.documentWFPath.userNotes FROM         dbo.users INNER JOIN       dbo.documentWFPath ON dbo.users.userID = dbo.documentWFPath.userID " +
" WHERE     (dbo.documentWFPath.docID = " + docID.ToString() + ")";
            DataTable DT = new DataTable();
            DT = c.GetDataAsDataTable(sql);
            rptFullWorkflow.DataSource = DT;
            rptFullWorkflow.DataBind();

        }

        Int32 wfCount = 0;
        public string getCounter()
        {
            wfCount += 1;
            return wfCount.ToString();
        }

        //public string getWFAction(Int32 actionID)
        //{
        //    if (Session["lang"].ToString() == "1")
        //    {
        //        return c.ActionsAr[actionID];
        //    }
        //    else
        //    {
        //        return c.ActionsEn[actionID];
        //    }
        //}

    }
}