using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace dms.Admin
{
    
    public partial class eFormsManage : System.Web.UI.Page
    {
        DMS.DAL.operations op = new DMS.DAL.operations();
        CommonFunction.clsCommon c = new CommonFunction.clsCommon();
        tables.dbo.eForms typesTB = new tables.dbo.eForms();

        Int32 formID; string formName; bool active;
        Int32 defaultPathID; Int32 catID; Int32 catPrgID;
        string formNameAr;

        public void fillVariables()
        {
            formID = c.convertToInt32(txtFormID.Text);
            formName = c.convertToString(txtFormName.Text);
            formNameAr = txtFormNameAr.Text;
            defaultPathID = c.convertToInt32(drpDefaultPathID.SelectedValue);
            active = c.convertToBool(chkActive.Checked);

            if (!Int32.TryParse(cmbCatID.SelectedValue, out catID))
            {
                op = new DMS.DAL.operations();
                //catPrgID = op.dboAddPrograms("eForm Category : " + cmbCatID.SelectedValue, 18, "categoryForms",850,500,txtFormNameAr.Text);
                
                System.IO.File.Copy(Server.MapPath("../Images/Icons/") + "formCat.png", Server.MapPath("../Images/Icons/") + "Icon" + catPrgID.ToString() + ".png");

                op = new DMS.DAL.operations();
                catID = op.dboAddEformsCategories(cmbCatID.SelectedValue, catPrgID);

                cmbCatID.Items.Clear();
                fillCategories();
                cmbCatID.SelectedValue = catID.ToString();
            }
            else
            {
                tables.dbo.eformsCategories formCat = new tables.dbo.eformsCategories();
                op=new DMS.DAL.operations();
                formCat = op.dboGetEformsCategoriesByPrimaryKey(catID);
                catPrgID = formCat.fieldCatPrgID;
            }
        }

        public void fillData(DataTable DT)
        {
            c.fillData(DT, 0, typesTB.columnsArray, Page);

        }

        public void converttoArabic()
        {
            if (Session["lang"].ToString() == "1")
            {
                grdEForms.Columns[0].HeaderText = "الرقم";
                grdEForms.Columns[1].HeaderText = "الاسم";
                Label1.Text = "الرقم";
                Label8.Text = "وصف النموذج - لغة انجليزية";
                Label3.Text = "وصف النموذج - لغة عربي";
                Label2.Text = "مسار العمل الافتراضي";
                rdoSaveMethod.Items.FindByValue("0").Text = "نموذج جديد";
                rdoSaveMethod.Items.FindByValue("1").Text = "النموذج الحالي";
                grdEForms.Columns[0].HeaderText = "الرقم";
                grdEForms.Columns[1].HeaderText = "الاسم";

                if (txtFormID.Text != "")
                    lblFormMode.Text = "تعديل نموذج";
                else
                    lblFormMode.Text = "اضافة نموذج جديد";

                drpMetaDataType.Items[0].Text = "نص";
                drpMetaDataType.Items[1].Text = "عدد صحيح";
                drpMetaDataType.Items[2].Text = "عدد عشري";
                drpMetaDataType.Items[3].Text = "تاريخ";
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            converttoArabic();
            if (!IsPostBack)
            {
                filldocTypes();

                fillCategories();

                tables.dbo.controlsTypes cntls = new tables.dbo.controlsTypes();
                op = new DMS.DAL.operations();
                cntls = op.dboGetAllControlsTypes();
                //cntls = op.dboGetAllControlsTypes();
                if(Session["lang"].ToString() == "0")
                    c.FillDropDownList(drpCtrlID, cntls.table);
                else
                    c.FillDropDownList(drpCtrlID, cntls.table,CommonFunction.clsCommon.Typesech.byColomenName,CommonFunction.clsCommon.IsFilter.False,"","","crtlID","ctrlDescAr");

                tables.dbo.workFlowPaths paths = new tables.dbo.workFlowPaths();
                op = new DMS.DAL.operations();
                paths = op.dboGetAllWorkFlowPaths();
                //cntls = op.dboGetAllControlsTypes();
                if (Session["lang"].ToString() == "0")
                    c.FillDropDownList(drpDefaultPathID, paths.table);
                else
                    c.FillDropDownList(drpDefaultPathID, paths.table, CommonFunction.clsCommon.Typesech.byColomenName, CommonFunction.clsCommon.IsFilter.False, "", "", "pathID", "pathDescAr");

                
            }
            
        }

        private void fillCategories()
        {
            tables.dbo.eformsCategories cat = new tables.dbo.eformsCategories();
            op = new DMS.DAL.operations();
            cat = op.dboGetAllEformsCategories();
            cmbCatID.DataSource = cat.table;
            cmbCatID.DataTextField = "catTitle";
            cmbCatID.DataValueField = "catID";
            cmbCatID.DataBind();

            c.FillDropDownList(drpCats, cat.table);
        }

        public void filldocTypes()
        {
            if (drpCats.SelectedValue.Trim() != "")
            {
                op = new DMS.DAL.operations();
                typesTB = new tables.dbo.eForms();
                typesTB = op.dboGetAllEForms("catID=" + drpCats.SelectedValue);
                grdEForms.DataSource = typesTB.table;
                BoundField bf = (BoundField)grdEForms.Columns[1];
                if (Session["lang"].ToString() == "0")
                    bf.DataField = "formName";
                else
                    bf.DataField = "formNameAr";
                grdEForms.DataBind();
            }
        }

        protected void grdTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            typesTB = new tables.dbo.eForms();
            op = new DMS.DAL.operations();
            typesTB = op.dboGetEFormsByPrimaryKey(c.convertToInt32(grdEForms.SelectedRow.Cells[0].Text));

            fillData(typesTB.table);
            rdoSaveMethod.SelectedValue = "1";

            fillMetas();
            tblDetailsForm.Style["display"] = "block";

            if (Session["lang"].ToString() == "0")
                lblFormMode.Text = "Edit eForm";
            else
                lblFormMode.Text = "تعديل نموذج";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                fillVariables();
                Int32 varRes;

                if (rdoSaveMethod.SelectedValue == "0")
                {
                    op = new DMS.DAL.operations();
                    varRes = op.dboAddEForms(formName, defaultPathID, active, catID, catPrgID, formNameAr);
                    txtFormID.Text = varRes.ToString();
                }
                else
                {
                    if (txtFormID.Text != "")
                        varRes = op.dboUpdateEFormsByPrimaryKey(formID, formName, defaultPathID, active, catID, catPrgID, formNameAr);
                    else
                        varRes = -1;
                }

                if (varRes > -1)
                {
                    lblRes.Text = "Save successful";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "تم الحفظ بنجاح";
                    filldocTypes();
                    tblDetailsForm.Style["display"] = "block";
                }
                else
                {
                    lblRes.Text = "Data Not Saved";
                    if (Session["lang"].ToString() == "1")
                        lblRes.Text = "لم يتم الحفظ ";
                }
            }
        }

        string metaDesc; string metaDataType; bool required; Int32 orderSeq; Int32 ctrlID; string defaultTexts; string defaultValues; bool visible;

        protected void lnkAddMeta_Click(object sender, EventArgs e)
        {
            if (txtFormID.Text != "")
            {
                fillVariables();
                metaDesc = c.convertToString(txtMetaDesc.Text);
                metaDataType = c.convertToString(drpMetaDataType.SelectedValue);
                required = c.convertToBool(chkrRequired.Checked);
                orderSeq = c.convertToInt32(txtOrderSeq.Text);
                ctrlID = c.convertToInt32(drpCtrlID.SelectedValue);
                defaultTexts = c.convertToString(txtDefaultTexts.Text);
                defaultValues = c.convertToString(txtDefaultValues.Text);
                visible = c.convertToBool(chkVisible.Checked);

                Int32 fieldSeq = c.GetMax("fieldSeq", "dbo.eFormFields", "formID=" + formID.ToString());
                op = new DMS.DAL.operations();
                op.dboAddEFormFields(formID, fieldSeq, metaDesc, metaDataType, required, orderSeq, ctrlID, defaultTexts, defaultValues, visible);

                fillMetas();
            }
        }

        private void fillMetas()
        {
            tables.dbo.eFormFields metasTB = new tables.dbo.eFormFields();
            op = new DMS.DAL.operations();
            metasTB = op.dboGetAllEFormFields("formID=" + txtFormID.Text);

            grdMetas.DataSource = metasTB.table;
            grdMetas.DataBind();

            lst1.Items.Clear();
            for (Int32 i = 0; i < metasTB.rowsCount; i++)
            {
                metasTB.currentIndex = i;
                ListItem li = new ListItem(metasTB.fieldMetaDesc, "meta_" + metasTB.fieldOrderSeq.ToString());
                li.Attributes.Add("onclick", "addTextToExpEditor('meta_" + metasTB.fieldOrderSeq.ToString() + "')");

                lst1.Items.Add(li);
            }
        }

        protected void grdMetas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            op = new DMS.DAL.operations();
            op.dboDeleteMetasByPrimaryKey(c.convertToInt32(grdMetas.Rows[e.RowIndex].Cells[0].Text));
            fillMetas();
        }

        protected void grdMetas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdMetas.EditIndex = e.NewEditIndex;
            fillMetas();
        }

        protected void grdMetas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdMetas.EditIndex = -1;
            fillMetas();
        }

        protected void grdMetas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            fillVariables();
            Int32 metaID = c.convertToInt32(grdMetas.Rows[e.RowIndex].Cells[0].Text);

            TextBox txt = new TextBox();
            txt = (TextBox)grdMetas.Rows[e.RowIndex].Cells[1].Controls[0];
            metaDesc = txt.Text;

            DropDownList drp = new DropDownList();
            drp = (DropDownList)grdMetas.Rows[e.RowIndex].Cells[2].FindControl("drpMetaDataType");
            metaDataType = drp.SelectedValue;

            CheckBox chk = new CheckBox();
            chk = (CheckBox)grdMetas.Rows[e.RowIndex].Cells[3].Controls[0];
            required = chk.Checked;

            txt = new TextBox();
            txt = (TextBox)grdMetas.Rows[e.RowIndex].Cells[4].Controls[0];
            orderSeq = c.convertToInt32(txt.Text);

            drp = new DropDownList();
            drp = (DropDownList)grdMetas.Rows[e.RowIndex].Cells[5].FindControl("drpCtrlID");
            ctrlID = c.convertToInt32(drp.SelectedValue);

            txt = new TextBox();
            txt = (TextBox)grdMetas.Rows[e.RowIndex].Cells[6].Controls[0];
            defaultTexts = txt.Text;

            txt = new TextBox();
            txt = (TextBox)grdMetas.Rows[e.RowIndex].Cells[7].Controls[0];
            defaultValues = txt.Text;

            chk = new CheckBox();
            chk = (CheckBox)grdMetas.Rows[e.RowIndex].Cells[8].Controls[0];
            visible = c.convertToBool(chk.Checked);

            op = new DMS.DAL.operations();
            op.dboUpdateEFormFieldsByPrimaryKey(metaID, formID, metaDesc, metaDataType, required, orderSeq, ctrlID, defaultTexts, defaultValues, visible);

            grdMetas.EditIndex = -1;
            fillMetas();
        }

        public string getControlType(string cntrlID)
        {
            try
            {
                return drpCtrlID.Items.FindByValue(cntrlID).Text;
            }
            catch { return ""; }
        }

        protected void grdMetas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                if (e.Row.Cells.Count > 4)
                {
                    if (e.Row.Cells[5].FindControl("drpCtrlID") != null)
                    {
                        DropDownList drp = new DropDownList();
                        drp = (DropDownList)e.Row.Cells[5].FindControl("drpCtrlID");
                        tables.dbo.controlsTypes cntls = new tables.dbo.controlsTypes();
                        op = new DMS.DAL.operations();
                        cntls = op.dboGetAllControlsTypes();
                        c.FillDropDownList(drp, cntls.table);

                        HiddenField hdn = new HiddenField();
                        hdn = (HiddenField)e.Row.Cells[5].FindControl("hdnCtrlID");
                        drp.SelectedValue = hdn.Value;
                    }
                }
            }
        }

        protected void drpCats_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdEForms.SelectedIndex = -1;
            filldocTypes();
            
        }
    }
}