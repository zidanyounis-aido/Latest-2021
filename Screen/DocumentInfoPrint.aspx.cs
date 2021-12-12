using dms.MangeForm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using tables.dbo;

namespace dms.Screen
{
    public partial class DocumentInfoPrint : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        public CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        Int32 docTypID; string docName; string docExt; DateTime addedDate;
        Int32 addedUserID; Int16 lastVersion; DateTime modifyDate; Int32 modifyUserID;
        Int32 fldrID; string ocrContent; Int64 docID;
        tables.dbo.documents docTB = new tables.dbo.documents();
        Int64 folderSeq; Int64 docTypeSeq; Int64 folderDocTypeSeq;
        decimal wfTimeFrame; Int16 wfStatus;

        public void fillData(DataTable DT)
        {
            //c.fillData(DT, 0, docTB.columnsArray, Page);
            txtDocID.Text = DT.Rows[0]["docID"].ToString();
            txtDocName.Text = DT.Rows[0]["DocName"].ToString();
            //showDocTypeMetas();

            
        }

        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                lblDocName.Text = "اسم الملف";
                
            }

        }

        public void Localize()
        {
            if (Session["lang"] != null && Session["lang"].ToString() == "0")
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
            else
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("ar");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Localize();
            linkLtr.Attributes["href"] = (Session["lang"].ToString() == "0") ? "/Assets/UIKIT/css/Style-LTR.css" : "";
            linkBootstrap.Attributes["href"] = (Session["lang"].ToString() == "0") ? "" : "/Assets/UIKIT/css/bootstrap-rtl.min.css";

            converttoArabic();

            op = new DMS.DAL.operations();
            tables.dbo.users user = new tables.dbo.users();
            user = op.dboGetUsersByPrimaryKey(c.convertToInt32(Session["userID"]));
            
            c = new CommonFunction.clsCommon();
            if (Request.QueryString["docID"] == "")
                Response.Redirect("../Scressn/", true);

            string dc = Request.QueryString["docID"];
            dc = c.decrypt(dc);
            docID = c.convertToInt64(dc);

            if (!IsPostBack)
            {
                
                op = new DMS.DAL.operations();
                docTB = op.dboGetDocumentsByPrimaryKey(docID);

                showDocTypeMetas(docTB.fieldDocTypID);

                showData();

            }
            

        }

        private void showData()
        {

            op = new DMS.DAL.operations();
            docTB = op.dboGetDocumentsByPrimaryKey(docID);
            fillData(docTB.table);
            op = new DMS.DAL.operations();
            tables.dbo.folders fldr = op.dboGetFoldersByPrimaryKey(docTB.fieldFldrID);
            

            if (fldr.fieldIsDiwan)
            {
                if (Session["lang"].ToString() == "1")
                    lblDocName.Text = "موضوع الكتاب";
                else
                    lblDocName.Text = "Letter subject";
            }

           
            tables.dbo.userFolders userFldr = new tables.dbo.userFolders();
            op = new DMS.DAL.operations();
            userFldr = op.dboGetUserFoldersByPrimaryKey(docTB.fieldFldrID, c.convertToInt32(Session["userID"]));
            if (userFldr.hasRows)
            {
                if (!userFldr.fieldAllow)
                    Response.Redirect("../Screen/DefaultAr.aspx");

                txtDocID.Focus();
            }
            else
            {
                Response.Redirect("../screen/notAllowed.html");
            }

        }

        

        public void showDocTypeMetas(int docTypeID = 0)
        {
            DMS.ReadFormGenerator f = new DMS.ReadFormGenerator();
            f.FormMetaManager(docTypeID, docID, int.Parse(Session["userID"].ToString()), !(Session["lang"].ToString() == "0"));
            StringBuilder autoScript = new StringBuilder();
            //int docID = c.convertToInt32(c.decrypt(Request.QueryString["docID"]));
            int folderID = int.Parse(c.GetDataAsScalar("select top 1 fldrID from documents where docID=" + docID).ToString());
            f.GetPanal(ref pnlDocMetas, ref autoScript, folderID, int.Parse(Session["userID"].ToString()), false);
        }

       
        //public void showDocTypeMetas(int docTypeID =0)
        //{
        //    //pnlDocDetails.Visible = true;
        //    op = new DMS.DAL.operations();
        //    tables.dbo.metas metasTB = new tables.dbo.metas();
        //    metasTB = op.dboGetAllMetas("DocTypID = " + docTypeID.ToString() + " and visible=1 order by orderSeq,metaID");
        //    if (metasTB.hasRows)
        //    {
        //        Panel TR = new Panel();
        //        TR.CssClass = "rowCSS";
        //        Int32 rows = 0;
        //        for (Int32 i = 0; i < metasTB.rowsCount; i++)
        //        {
        //            metasTB.currentIndex = i;

        //            if (rows != metasTB.fieldOrderSeq)
        //            {
        //                TR = new Panel();
        //                TR.CssClass = "rowCSS";
        //            }
        //            rows = metasTB.fieldOrderSeq;

        //            if (metasTB.fieldCtrlID == 5)
        //            {
        //                Panel TD = new Panel();
        //                if (Session["lang"].ToString() == "0")
        //                    TD.CssClass = "cellTDEn";
        //                else
        //                    TD.CssClass = "cellTDAr";

        //                //TD.ColumnSpan = 4;
        //                //lbl.Font.Bold = true;
        //                Label lbl = new Label();
        //                lbl.Font.Bold = true;

        //                if (Session["lang"].ToString() == "0")
        //                    lbl.Text = metasTB.fieldMetaDesc ;
        //                else
        //                    lbl.Text = metasTB.fieldMetaDescAr ;
        //                TD.Controls.Add(lbl);
        //                TR.Controls.Add(TD);
        //                TR.CssClass = "rowTitle";
        //                pnlDocMetas.Controls.Add(TR);

        //            }
        //            else
        //            {


        //                Panel TD = new Panel();
        //                if (Session["lang"].ToString() == "0")
        //                    TD.CssClass = "cellTDEn";
        //                else
        //                    TD.CssClass = "cellTDAr";

        //                Label lbl = new Label();
        //                if (Session["lang"].ToString() == "0")
        //                    lbl.Text = metasTB.fieldMetaDesc + " : ";
        //                else
        //                    lbl.Text = metasTB.fieldMetaDescAr + " : ";
        //                //TD.Width = new Unit(20, UnitType.Percentage);
        //                TD.Controls.Add(lbl);
        //                TR.Controls.Add(TD);

        //                TD = new Panel();
        //                if (Session["lang"].ToString() == "0")
        //                    TD.CssClass = "cellTDEn";
        //                else
        //                    TD.CssClass = "cellTDAr";

        //                if (Session["lang"].ToString() == "0")
        //                    lbl.Text = metasTB.fieldMetaDesc + " : ";
        //                else
        //                    lbl.Text = metasTB.fieldMetaDescAr + " : ";
        //                //TD.Width = new Unit(30, UnitType.Percentage);
        //                HiddenField hdn = new HiddenField();
        //                hdn.ID = "hdn_" + (i + 1).ToString();
        //                hdn.Value = metasTB.fieldMetaID.ToString();
        //                TD.Controls.Add(hdn);

        //                string value = "";
        //                op = new DMS.DAL.operations();
        //                tables.dbo.documentMataValues docMetas = new tables.dbo.documentMataValues();
        //                docMetas = op.dboGetDocumentMataValuesByPrimaryKey(docID, metasTB.fieldMetaID);
        //                if (docMetas.hasRows)
        //                    value = docMetas.fieldValue;

        //                //Label lblVal = new Label();
        //                //lblVal.Text = value;
        //                //TD.Controls.Add(lblVal);

        //                switch (metasTB.fieldCtrlID)
        //                {
        //                    case 1:
        //                        Label txt = new Label();
        //                        txt.TabIndex = Convert.ToInt16(6 + i);
        //                        txt.ID = "meta_" + (i + 1).ToString();
        //                        txt.Text = metasTB.fieldDefaultTexts;
        //                        txt.Text = value;

        //                        TD.Controls.Add(txt);
        //                        break;
        //                    case 2:
        //                        Label drp = new Label();
        //                        //drp.TabIndex = Convert.ToInt16(6 + i);
        //                        drp.ID = "meta_" + (i + 1).ToString();
        //                        Hashtable drpHS = new Hashtable();

        //                        if (!metasTB.fieldDefaultTexts.ToLower().StartsWith("db:"))
        //                        {
        //                            char[] chars = { ',', ';', '،' };
        //                            if (metasTB.fieldDefaultTexts.IndexOfAny(chars) > -1)
        //                            {
        //                                string[] texts;
        //                                texts = metasTB.fieldDefaultTexts.Split(chars);
        //                                string[] values;
        //                                values = metasTB.fieldDefaultValues.Split(chars);
        //                                for (Int32 j = 0; j < texts.Length; j++)
        //                                {
        //                                    string _value = "";
        //                                    if (j < values.Length)
        //                                        _value = values[j].Trim();
        //                                    else
        //                                        _value = texts[j].Trim();

        //                                    drpHS.Add(texts[j].Trim(), _value);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                drpHS.Add(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim());
        //                            }
        //                        }
        //                        else
        //                        {
        //                            string[] query = metasTB.fieldDefaultTexts.Split(':');
        //                            string[] queryVal = metasTB.fieldDefaultValues.Split(':');
        //                            string tableName = query[1];
        //                            string textFeild = query[2];
        //                            string valueFeild = queryVal[2];
        //                            string cond = "";
        //                            if (query.Length > 3)
        //                                cond = " where " + query[3];
        //                            string drpSQL = "select " + valueFeild + "," + textFeild + " from " + tableName + cond + " order by " + textFeild;
        //                            DataTable drpDT = c.GetDataAsDataTable(drpSQL);
        //                            for (Int32 t = 0; t < drpDT.Rows.Count; t++)
        //                            {
        //                                drpHS.Add(drpDT.Rows[t][0].ToString(), drpDT.Rows[t][1].ToString());
        //                            }
        //                            //c.FillDropDownList(drp, drpDT);
        //                        }
        //                        if (value != "")
        //                            drp.Text = drpHS[value].ToString();

        //                        TD.Controls.Add(drp);
        //                        break;
        //                    case 3:
        //                        Label chk = new Label();
        //                        //chk.TabIndex = Convert.ToInt16(6 + i);
        //                        Hashtable chkHS = new Hashtable();
        //                        chk.ID = "meta_" + (i + 1).ToString();
        //                        if (metasTB.fieldDefaultTexts.Contains(','))
        //                        {
        //                            string[] texts;
        //                            texts = metasTB.fieldDefaultTexts.Split(',');
        //                            string[] values;
        //                            values = metasTB.fieldDefaultValues.Split(',');
        //                            for (Int32 j = 0; j < texts.Length; j++)
        //                            {
        //                                string _value = "";
        //                                if (j < values.Length)
        //                                    _value = values[j].Trim();
        //                                else
        //                                    _value = texts[j].Trim();

        //                                chkHS.Add(texts[j].Trim(), _value);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            chkHS.Add(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim());
        //                        }

        //                        if (value != "")
        //                            chk.Text = chkHS[value].ToString();

        //                        TD.Controls.Add(chk);
        //                        break;
        //                    case 4:
        //                        Label rdo = new Label();
        //                        //rdo.TabIndex = Convert.ToInt16(6 + i);
        //                        Hashtable rdoHS = new Hashtable();
        //                        rdo.ID = "meta_" + (i + 1).ToString();
        //                        if (metasTB.fieldDefaultTexts.Contains(','))
        //                        {
        //                            string[] texts;
        //                            texts = metasTB.fieldDefaultTexts.Split(',');
        //                            string[] values;
        //                            values = metasTB.fieldDefaultValues.Split(',');
        //                            for (Int32 j = 0; j < texts.Length; j++)
        //                            {
        //                                string _value = "";
        //                                if (j < values.Length)
        //                                    _value = values[j].Trim();
        //                                else
        //                                    _value = texts[j].Trim();

        //                                rdoHS.Add(texts[j].Trim(), _value);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            rdoHS.Add(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim());
        //                        }

        //                        if (value != "")
        //                            rdo.Text = rdoHS[value].ToString();

        //                        TD.Controls.Add(rdo);
        //                        break;

        //                }
        //                TR.Controls.Add(TD);
        //            }
        //            pnlDocMetas.Controls.Add(TR);
        //        }
        //    }
        //}

    }
}