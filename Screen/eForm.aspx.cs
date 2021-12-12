using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace dms.Screen
{
    public partial class eForm : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        tables.dbo.eForms typesTB = new tables.dbo.eForms();

        Int32 formID; string formName; bool active;

        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                grdEForms.Columns[0].HeaderText = "الرقم";
                grdEForms.Columns[1].HeaderText = "الاسم";
                drpRecipientType.Items.FindByValue("1").Text = "مستخدم";
                drpRecipientType.Items.FindByValue("2").Text = "مجموعة";
                drpRecipientType.Items.FindByValue("3").Text = "موقع";
                drpRecipientType.Items.FindByValue("4").Text = "قسم";

            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                op = new DMS.DAL.operations();
                typesTB = new tables.dbo.eForms();
                typesTB = op.dboGetAllEForms();
                grdEForms.DataSource = typesTB.table;
                grdEForms.DataBind();

                if (!String.IsNullOrEmpty(Request.QueryString["CODEN"]))
                {
                    pnlDetails.Visible = true;
                    showDocTypeMetas(c.convertToInt32(Request.QueryString["CODEN"]));
                }
            }

            converttoArabic();
        }

        public void showDocTypeMetas(Int32 FormID)
        {
            
            op = new DMS.DAL.operations();
            tables.dbo.eFormFields metasTB = new tables.dbo.eFormFields();
            metasTB = op.dboGetAllEFormFields("formID = " + FormID.ToString() + " and visible=1 order by orderSeq,fieldSeq");
            System.Text.StringBuilder autoScript = new System.Text.StringBuilder();
            autoScript.AppendLine(@"<script type=""text/javascript"">");
            autoScript.AppendLine(@"function araneasFillAutos(){");
            autoScript.AppendLine(@"try  {");
            if (metasTB.hasRows)
            {
                for (Int32 i = 0; i < metasTB.rowsCount; i++)
                {
                    metasTB.currentIndex = i;
                    TableRow TR = new TableRow();
                    TableCell TD = new TableCell();
                    TD.Text = metasTB.fieldMetaDesc;
                    TR.Cells.Add(TD);

                    TD = new TableCell();
                    HiddenField hdn = new HiddenField();
                    hdn.ID = "hdn_" + (i + 1).ToString();
                    hdn.Value = metasTB.fieldFieldSeq.ToString();
                    hdn.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    TD.Controls.Add(hdn);
                    switch (metasTB.fieldCtrlID)
                    {
                        case 1:
                            TextBox txt = new TextBox();
                            txt.Attributes.Add("autocomplete", "off");
                            txt.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                            txt.ID = "meta_" + (i + 1).ToString();
                            if (metasTB.fieldDefaultTexts.ToLower().StartsWith("#expr:"))
                            {
                                string textValue = c.fixMetaExp(metasTB.fieldDefaultTexts, i);
                                textValue = textValue.Substring(textValue.IndexOf(":") + 1);
                                if (!textValue.EndsWith(";"))
                                    textValue = textValue + ";";

                                autoScript.AppendLine(textValue);
                            }
                            else
                                txt.Text = metasTB.fieldDefaultTexts;
                            TD.Controls.Add(txt);
                            break;
                        case 2:
                            DropDownList drp = new DropDownList();
                            drp.ID = "meta_" + (i + 1).ToString();
                            drp.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                            if (metasTB.fieldDefaultTexts.Contains(','))
                            {
                                string[] texts;
                                texts = metasTB.fieldDefaultTexts.Split(',');
                                string[] values;
                                values = metasTB.fieldDefaultValues.Split(',');
                                for (Int32 j = 0; j < texts.Length; j++)
                                {
                                    string value = "";
                                    if (j < values.Length)
                                        value = values[j].Trim();
                                    else
                                        value = texts[j].Trim();

                                    drp.Items.Add(new ListItem(texts[j].Trim(), value));
                                }
                            }
                            else
                            {
                                drp.Items.Add(new ListItem(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim()));
                            }
                            TD.Controls.Add(drp);
                            break;
                        case 3:
                            CheckBoxList chk = new CheckBoxList();
                            chk.ID = "meta_" + (i + 1).ToString();
                            chk.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                            if (metasTB.fieldDefaultTexts.Contains(','))
                            {
                                string[] texts;
                                texts = metasTB.fieldDefaultTexts.Split(',');
                                string[] values;
                                values = metasTB.fieldDefaultValues.Split(',');
                                for (Int32 j = 0; j < texts.Length; j++)
                                {
                                    string value = "";
                                    if (j < values.Length)
                                        value = values[j].Trim();
                                    else
                                        value = texts[j].Trim();

                                    chk.Items.Add(new ListItem(texts[j].Trim(), value));
                                }
                            }
                            else
                            {
                                chk.Items.Add(new ListItem(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim()));
                            }
                            TD.Controls.Add(chk);
                            break;
                        case 4:
                            RadioButtonList rdo = new RadioButtonList();
                            rdo.ID = "meta_" + (i + 1).ToString();
                            rdo.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                            if (metasTB.fieldDefaultTexts.Contains(','))
                            {
                                string[] texts;
                                texts = metasTB.fieldDefaultTexts.Split(',');
                                string[] values;
                                values = metasTB.fieldDefaultValues.Split(',');
                                for (Int32 j = 0; j < texts.Length; j++)
                                {
                                    string value = "";
                                    if (j < values.Length)
                                        value = values[j].Trim();
                                    else
                                        value = texts[j].Trim();

                                    rdo.Items.Add(new ListItem(texts[j].Trim(), value));
                                }
                            }
                            else
                            {
                                rdo.Items.Add(new ListItem(metasTB.fieldDefaultTexts.Trim(), metasTB.fieldDefaultValues.Trim()));
                            }
                            TD.Controls.Add(rdo);
                            break;

                    }
                    TR.Cells.Add(TD);
                    tblDocMetas.Rows.Add(TR);
                }
            }
            autoScript.AppendLine(@"return true;}catch(err)  {  alert(err);return false;  }");
            autoScript.AppendLine(@"}");
            autoScript.AppendLine(@"</script>");

            ltrScripts.Text = autoScript.ToString();
        }
        //CODEN
        protected void grdTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlDetails.Visible = true;
            
            showDocTypeMetas(c.convertToInt32(grdEForms.SelectedRow.Cells[0].Text));
        }

        protected void drpRecipientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //showDocTypeMetas();
            fillDrpRecipientID();
        }

        private void fillDrpRecipientID()
        {
            op = new DMS.DAL.operations();
            DataTable dt = new DataTable();
            switch (drpRecipientType.SelectedValue)
            {
                case "1":
                    tables.dbo.users usersTB = new tables.dbo.users();
                    usersTB = op.dboGetAllUsers();
                    dt = usersTB.table;
                    break;
                case "2":
                    tables.dbo.groups grpTB = new tables.dbo.groups();
                    grpTB = op.dboGetAllGroups();
                    dt = grpTB.table;
                    break;
                case "3":
                    tables.dbo.positions positionsTB = new tables.dbo.positions();
                    positionsTB = op.dboGetAllPositions();
                    dt = positionsTB.table;
                    break;
                case "4":
                    tables.dbo.departments departmentsTB = new tables.dbo.departments();
                    departmentsTB = op.dboGetAllDepartments();
                    dt = departmentsTB.table;
                    break;
            }

            c.FillDropDownList(drpRecipientID, dt);
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {

        }
    }
}