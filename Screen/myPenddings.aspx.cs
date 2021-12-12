using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms.Screen
{
    public partial class myPenddings : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();


        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                GridView1.Columns[0].HeaderText = "نوع الشكل";
                GridView1.Columns[1].HeaderText = "تاريخ الارسال";
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["CODEN"]))
            {
                showDocTypeMetas(c.convertToInt32(Request.QueryString["CODEN"]));
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

        protected void LinkButton3_Click(object sender, EventArgs e)
        {

        }
    }
}