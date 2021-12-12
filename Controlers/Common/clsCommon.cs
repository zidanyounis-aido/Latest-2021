using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
//using AjaxControlToolkit;
using System.Web.UI;
using System.Threading;
using System.Globalization;
using DMS.DAL;
//using DllNum;
using System.Text.RegularExpressions;
using System.Collections;
using AjaxControlToolkit;
using dms.Controlers.Common;

namespace CommonFunction
{
    public partial class clsCommon
    {
        const string passPhrase = "Pas5pr@se";
        const string saltValue = "s@1tValue";
        const string hashAlgorithm = "SHA1";
        const int passwordIterations = 2;
        const string initVector = "@1B2c3D4e5F6g7H8";
        const int keySize = 256;

        public string[] ActionsEn = { "Pending", "Approved", "Declined", "Approved with Condition", "Consultation", "Forward", "Seen", "Agree According to regulations" };
        public string[] ActionsAr = { "معلق", "موافق", "رفض", "موافق  ضمن شروط", "مداولة", "تحويل", "اطلعت", "موافق وفق الضوابط" };


        SecurityNB.TextEncryption sec = new SecurityNB.TextEncryption();

        public string encrypt(string value)
        {
            return sec.Encrypt(value, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        }

        public string decrypt(string value)
        {
            return sec.Decrypt(value, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        }




        DMS.DAL.operations op = new DMS.DAL.operations();
        //DMS.SiteMaster m = new DMS.SiteMaster();
        public enum Typesech
        {
            byColomenNum = 0,
            byColomenName = 1,

        }
        public enum IsFilter
        {
            True = 0,
            False = 1,

        }
        public enum FilterOpration
        {
            True = 0,
            False = 1,

        }
        public enum SelectOpration
        {
            True = 0,
            False = 1,
        }


        public void FillDropDownList(DropDownList drp, DataTable dtLockup, Typesech SearchBy = Typesech.byColomenNum, IsFilter Ok = IsFilter.False, string FilterFild = "", string FilterValue = "", String ValueFild = "", String TextFild = "", SelectOpration select = SelectOpration.True)
        {
            DataTable dtF;
            if (dtLockup != null)
            {
                dtLockup.TableName = "Table";
                if (Ok == IsFilter.True)
                {
                    DataView dv = new DataView();
                    dv.Table = dtLockup;
                    dv.RowFilter = FilterFild + "='" + FilterValue + "'";
                    dtF = dv.ToTable();
                }
                else
                {
                    dtF = dtLockup;
                }

                if (SearchBy == Typesech.byColomenNum)
                {
                    try
                    {
                        drp.DataSource = dtF;
                        drp.DataValueField = dtF.Columns[0].ColumnName.ToString();
                        if (drp.ID == "drpFolders")
                        {
                            if (HttpContext.Current.Session["lang"].ToString() == "1")
                            {
                                drp.DataTextField = dtF.Columns[8].ColumnName.ToString();
                            }
                            else
                            {
                                drp.DataTextField = dtF.Columns[1].ColumnName.ToString();
                            }
                        }
                        else
                        {
                            drp.DataTextField = dtF.Columns[1].ColumnName.ToString();
                        }
                        drp.DataBind();
                    }
                    catch { }

                }
                else
                {
                    drp.DataSource = dtF;
                    drp.DataValueField = ValueFild;
                    drp.DataTextField = TextFild;
                    drp.DataBind();

                }
            }
            if (select == SelectOpration.True)
            {
                if (System.Web.HttpContext.Current.Session["lang"].ToString() == "0")
                    drp.Items.Insert(0, new ListItem("--Select--", Int16.MinValue.ToString()));
                else
                    drp.Items.Insert(0, new ListItem("--اختر--", Int16.MinValue.ToString()));

            }
        }


        public short boolToInt16(bool p)
        {
            if (p)
                return 1;
            else
                return 0;
        }
        /// <summary>
        /// مشان تعمل فلتر على درب داون لست من خلال درب داون لست  ثانيه
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="drp"></param>
        public void filterDropDownList(object sender, DropDownList drp)
        {
            DataView dv = new DataView();
            DataTable dt = (DataTable)HttpContext.Current.Cache[((DropDownList)drp).ToolTip];
            dt.TableName = "Table";
            dv.Table = dt;
            if (((DropDownList)sender).SelectedValue.ToString() != "")
            {
                dv.RowFilter = dt.Columns[2].ColumnName + " ='" + ((DropDownList)sender).SelectedValue.ToString() + "'";
                FillDropDownList(drp, dv.ToTable());
            }
        }
        /// <summary>
        /// حاله خاصه اذا احتجت تعبي تيكست فيلد نفس الفاليو فيلد في الدرب داون لست
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="drp"></param>
        /// <param name="ValueFild"></param>
        /// <param name="TextFild"></param>
        public void filterDropDownList(object sender, DropDownList drp, string ValueFild, string TextFild)
        {
            DataView dv = new DataView();
            DataTable dt = (DataTable)HttpContext.Current.Cache[((DropDownList)drp).ToolTip];
            dt.TableName = "Table";
            dv.Table = dt;
            dv.RowFilter = dt.Columns[2].ColumnName + " ='" + ((DropDownList)sender).SelectedValue.ToString() + "'";
            FillDropDownList(drp, dv.ToTable(), Typesech.byColomenName, IsFilter.False, "", "", ValueFild, TextFild, SelectOpration.True);
        }

        public void filterDropDownList(string val, DropDownList drp)
        {
            DataView dv = new DataView();
            DataTable dt = (DataTable)HttpContext.Current.Cache[((DropDownList)drp).ToolTip];
            dt.TableName = "Table";
            dv.Table = dt;
            dv.RowFilter = dt.Columns[2].ColumnName + " = '" + val + "'";
            FillDropDownList(drp, dv.ToTable());
        }
        /// <summary>
        /// بتفلتر Drp على اكثر منقيمه
        /// </summary>
        /// <param name="drp"></param>
        /// <param name="val"></param>
        public void filterDropDownList(DropDownList drp, params string[] val)
        {
            DataView dv = new DataView();
            DataTable dt = (DataTable)HttpContext.Current.Cache[((DropDownList)drp).ToolTip];
            dt.TableName = "Table";
            dv.Table = dt;
            string where = "";
            for (int i = 0; i < val.Length; i++)
            {
                if (i != 0)
                    where += " or ";

                where += dt.Columns[2].ColumnName + " = '" + val[i].ToString() + "'";
            }
            dv.RowFilter = where;
            FillDropDownList(drp, dv.ToTable());
        }
        /// <summary>
        /// بدون بيانات فقط اختر
        /// </summary>
        /// <param name="drp"></param>
        public void filterDropDownList(DropDownList drp)
        {

            drp.Items.Insert(0, new ListItem("اختر", Int16.MinValue.ToString()));
        }
        /// <summary>
        ///  ممكن تبعث اكثر من قيمه وتفلتر التيبل عليها
        /// </summary>
        /// <param name="NameCashTable"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public string filterData(string NameCashTable, params string[] val)
        {
            DataView dv = new DataView();
            DataTable dt = (DataTable)HttpContext.Current.Cache[NameCashTable];
            dt.TableName = "Table";
            dv.Table = dt;
            string where = "";
            for (int i = 0; i < val.Length; i++)
            {
                if (i != 0)
                    where += " or ";

                where += dt.Columns[2].ColumnName + " = '" + val[i].ToString() + "'";
            }
            dv.RowFilter = where;
            return dv.ToTable().Rows[0][0].ToString();
        }

        public DataTable getCashTable(string tableName)
        {
            DataTable dt = new DataTable();
            if (HttpContext.Current.Cache[tableName] != null)
            {
                dt = (DataTable)HttpContext.Current.Cache[tableName.Replace(".", "")];
            }
            return dt;
        }

        /// <summary>
        /// تستفيد منه اذا بدك قيمه معينه من كاش تيبل بدل ما تروح على قاعدة بيانات
        /// </summary>
        /// <param name="NameCashTable">Cash Table Name</param>
        /// <param name="searchBy">Name Or Id</param>
        /// <param name="val">one ore More Vale</param>
        /// <returns></returns>
        public string filterData(string NameCashTable, Typesech searchBy, params string[] val)
        {
            DataView dv = new DataView();
            DataTable dt = (DataTable)HttpContext.Current.Cache[NameCashTable];
            dt.TableName = "Table";
            dv.Table = dt;
            string where = "";
            for (int i = 0; i < val.Length; i++)
            {
                if (i != 0)
                    where += " or ";

                where += dt.Columns[2].ColumnName + " = '" + val[i].ToString() + "'";
            }
            dv.RowFilter = where;
            if (searchBy == Typesech.byColomenName)
                return dv.ToTable().Rows[0][1].ToString();
            else
                return dv.ToTable().Rows[0][0].ToString();

        }
        /// <summary>
        /// تستفيد منه اذا بدك قيمه معينه من كاش تيبل بدل ما تروح على قاعدة بيانات ويفضل استخدام هذا الفنكشن
        /// </summary>
        /// <param name="NameCashTable"></param>
        /// <param name="searchBy"></param>
        /// <param name="ReturnSearchBy"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public string filterData(string NameCashTable, Typesech searchBy, Typesech ReturnSearchBy, params string[] val)
        {
            DataView dv = new DataView();
            DataTable dt = (DataTable)HttpContext.Current.Cache[NameCashTable];
            dt.TableName = "Table";
            dv.Table = dt;
            string where = "";
            for (int i = 0; i < val.Length; i++)
            {
                if (i != 0)
                    where += " or ";

                where += dt.Columns[(int)searchBy].ColumnName + " = '" + val[i].ToString() + "'";
            }
            dv.RowFilter = where;
            return dv.ToTable().Rows[0][(int)ReturnSearchBy].ToString();

        }

        public DateTime convertToDateTime(object value)
        {
            if (string.IsNullOrEmpty(value.ToString().Trim()))
                return DateTime.MaxValue.AddDays(-1);
            else
            {
                string tempDate = value.ToString();
                string[] tempDateAR = tempDate.Split('/');
                string dateFormat;
                if (tempDateAR.Length > 1)
                {
                    dateFormat = tempDateAR[1] + "/" + tempDateAR[0] + "/" + tempDateAR[2];
                    if (Convert.ToInt16(tempDateAR[1]) > 12 && Convert.ToInt16(tempDateAR[0]) <= 12)
                        dateFormat = value.ToString().Trim();

                    if (dateFormat.Trim().Length <= 10)
                        dateFormat += " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
                }
                else
                    dateFormat = tempDate;
                DateTime finalDate = Convert.ToDateTime(dateFormat);
                return finalDate;
            }
        }
        public string convertToString(object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
                return "";
            else
            {
                if (value.ToString() == Int16.MinValue.ToString() || value.ToString() == DateTime.MaxValue.AddDays(-1).ToString())
                    return "";
                else
                    return Convert.ToString(value);
            }
        }
        public Decimal convertToDecimal(object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
                return Convert.ToDecimal(Int16.MinValue);
            else
                return Convert.ToDecimal(value);
        }
        public Int16 convertToInt16(object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
                return Int16.MinValue;
            else
                return Convert.ToInt16(value);
        }
        public Int32 convertToInt32(object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
                return Convert.ToInt32(Int16.MinValue);
            else
                return Convert.ToInt32(value);
        }
        public Int64 convertToInt64(object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
                return Convert.ToInt64(Int16.MinValue);
            else
                return Convert.ToInt64(value);
        }
        public Boolean convertToBoolean(object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
                return false;
            else
                return Convert.ToBoolean(value);
        }
        public Boolean convertToBool(object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
                return false;
            else
                return Convert.ToBoolean(value);
        }
        public Double convertToDouble(object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
                return Convert.ToDouble(Int16.MinValue);
            else
                return Convert.ToDouble(value);
        }
        public string toUpperFirstLetter(string str)
        {
            if (str != "")
                str = (((str).ToString())[0].ToString()).ToUpper() + (str).ToString().Substring(1, (str).ToString().Length - 1);
            return str;
        }
        public enum Operators
        {
            Equal = 1,
            MoreThan = 2,
            Lessthan = 3,
            MoreThanOrEqual = 4,
            LessthanOrEqual = 5,
            NotEqual = 6,
            In = 7,
            Between = 8
        }
        public void fillData(DataTable dt, Int32 rowIndex, string[] ColumnName, Control Root)
        {

            if (dt != null)
            {
                if (ColumnName.Length > 0 && dt.Rows.Count > 0)
                {

                    #region loop
                    for (int i = 0; i <= ColumnName.Length - 1; i++)
                    {
                        TextBox txt = new TextBox();
                        CheckBox chk = new CheckBox();
                        DropDownList drp = new DropDownList();
                        ComboBox cmb = new ComboBox();
                        RadioButtonList rdo = new RadioButtonList();
                        HiddenField hdn = new HiddenField();
                        string str = ColumnName[i].Split(' ')[1].ToString();
                        str = (((str).ToString())[0].ToString()).ToUpper() + (str).ToString().Substring(1, (str).ToString().Length - 1);
                        if (FindControlRecursive(Root, "txt" + str) != null)
                        {
                            txt = (TextBox)FindControlRecursive(Root, "txt" + str);

                            ////txt.Text = dt.Rows[rowIndex][str].ToString();
                            //// Using ToolTip Like '{0:dd/MM/yyyy} => 03/16/2011  or {0:0,0.000} => 14,520.000'
                            //// for more information visit this link.
                            //// Date   http://www.csharp-examples.net/string-format-datetime/
                            //// Double http://www.csharp-examples.net/string-format-double/
                            try
                            {
                                txt.Text = convertToString(dt.Rows[rowIndex][str].ToString());
                                if (dt.Rows[rowIndex][str] != DBNull.Value && dt.Rows[rowIndex][str].ToString() != "")
                                {
                                    if (ColumnName[i].Split(' ')[0].ToString().ToLower() == "datetime")
                                        txt.Text = Convert.ToDateTime(dt.Rows[rowIndex][str]).ToString("dd/MM/yyyy");
                                }

                                if ((txt.ToolTip.Length > 0) && (txt.ToolTip.Substring(0, 1) == "{"))
                                {
                                    string strText = txt.Text;
                                    strText = String.Format(new CultureInfo("en-US"), txt.ToolTip, strText);
                                    txt.Text = strText;

                                }
                            }
                            catch (Exception)
                            {

                                //throw;
                            }

                        }
                        else if (FindControlRecursive(Root, "chk" + str) != null)
                        {
                            chk = (CheckBox)FindControlRecursive(Root, "chk" + str);

                            if (dt.Columns[i].DataType == Type.GetType("System.Boolean"))
                            { chk.Checked = Convert.ToBoolean(dt.Rows[rowIndex][str]); }
                            else
                            {
                                if (dt.Rows[rowIndex][str].ToString() == "0")
                                    chk.Checked = false;
                                else
                                    chk.Checked = true;


                            }
                        }
                        else if (FindControlRecursive(Root, "drp" + str) != null)
                        {
                            drp = (DropDownList)FindControlRecursive(Root, "drp" + str);
                            if (drp.Items.FindByValue(dt.Rows[rowIndex][str].ToString()) != null)
                            {
                                drp.SelectedValue = dt.Rows[rowIndex][str].ToString();
                            }
                        }
                        else if (FindControlRecursive(Root, "cmb" + str) != null)
                        {
                            cmb = (ComboBox)FindControlRecursive(Root, "cmb" + str);
                            if (cmb.Items.FindByValue(dt.Rows[rowIndex][str].ToString()) != null)
                            {
                                cmb.SelectedValue = dt.Rows[rowIndex][str].ToString();
                            }
                        }
                        else if (FindControlRecursive(Root, "rdo" + str) != null)
                        {
                            rdo = (RadioButtonList)FindControlRecursive(Root, "rdo" + str);
                            if (rdo.Items.FindByValue(dt.Rows[rowIndex][str].ToString()) != null)
                            {
                                rdo.SelectedValue = dt.Rows[rowIndex][str].ToString();
                            }
                        }
                        if (FindControlRecursive(Root, "hdn" + str) != null)
                        {
                            hdn = (HiddenField)FindControlRecursive(Root, "hdn" + str);
                            hdn.Value = dt.Rows[rowIndex][str].ToString();
                        }
                    }
                    #endregion
                }
            }
        }
        public Control FindControlRecursive(Control Root, string Id)
        {

            if (Root.ID == Id)

                return Root;

            foreach (Control Ctl in Root.Controls)
            {

                Control FoundCtl = FindControlRecursive(Ctl, Id);

                if (FoundCtl != null)

                    return FoundCtl;

            }

            return null;

        }
        public enum selectType
        {
            byText,
            byValue
        }
        public string getControlText(Control Root, string control_ID, selectType selectBy = selectType.byText)
        {
            TextBox txt = new TextBox();
            CheckBox chk = new CheckBox();
            DropDownList drp = new DropDownList();
            ComboBox cmb = new ComboBox();
            RadioButtonList rdo = new RadioButtonList();
            HiddenField hdn = new HiddenField();

            string value = "";

            if (control_ID.StartsWith("txt"))
            {
                txt = (TextBox)FindControlRecursive(Root, control_ID);
                value = txt.Text;
            }
            else if (control_ID.StartsWith("chk"))
            {
                chk = (CheckBox)FindControlRecursive(Root, control_ID);
                //need to if statment
                value = chk.Text;
            }
            else if (control_ID.StartsWith("drp"))
            {
                drp = (DropDownList)FindControlRecursive(Root, control_ID);
                if (selectBy == selectType.byText)
                    value = drp.SelectedItem.Text;
                else
                    value = drp.SelectedValue;
            }
            else if (control_ID.StartsWith("cmb"))
            {
                cmb = (ComboBox)FindControlRecursive(Root, control_ID);
                if (selectBy == selectType.byText)
                    value = cmb.SelectedItem.Text;
                else
                    value = cmb.SelectedValue;
            }
            else if (control_ID.StartsWith("rdo"))
            {
                rdo = (RadioButtonList)FindControlRecursive(Root, control_ID);
                if (selectBy == selectType.byText)
                    value = rdo.SelectedItem.Text;
                else
                    value = rdo.SelectedValue;
            }
            if (control_ID.StartsWith("hdn"))
            {
                hdn = (HiddenField)FindControlRecursive(Root, control_ID);
                value = hdn.Value;
            }

            return value;
        }
        public void filldrp(Control Root, string[] drpName)
        {
            //string[] drpName = { "drpBrchCode", "drpApplCode", "drpAptyCode", "drpFtypCode", "drpFobjCode", "drpFrsnCode", "drpCtypCode", "drpPprimCode", "drpPrdcCode", "drpPeriodCode", "drpCollabAcntBrch", "drpCompDiscountAcntBrch", "drpFbrkerDiscountAcntBrch", "drpSuppCommAcntBrch", "drpFbrkCode", "drpFinAcntType", "drpOdueAcntType", "drpBankCode", "drpBbrchCode", "drpApstCode", "drpRgdgCode", "drpContractTypes", "drpCommTaxes" };
            for (int i = 0; i < drpName.Length; i++)
            {
                DropDownList drp = new DropDownList();
                drp = (DropDownList)FindControlRecursive(Root, drpName[i]);
                if (drp != null)
                {
                    DataTable dtf = new DataTable();
                    dtf = (DataTable)HttpContext.Current.Cache[drp.ToolTip.ToString()];
                    FillDropDownList(drp, dtf);
                    drp.ToolTip = "";
                }
            }
        }
        /// <summary>
        /// تسلسل على حقل معين
        /// </summary>
        /// <param name="fieldName">اسم الحقل المراد جلب اكبر قيمه +1</param>
        /// <param name="tableName">على اي جدول</param>
        /// <param name="cond">   where الشرط" ملاحظه يجب وضع</param>
        /// <returns></returns>
        public int GetMax(string fieldName, string tableName, string cond = " ")
        {
            string sql = "select isnull(max(" + fieldName + "),0) + 1 from " + tableName;
            if (!cond.Trim().StartsWith("where") && cond.Trim() != "")
                cond = "where " + cond;
            sql += " " + cond;
            Int32 res = convertToInt32(GetDataAsScalar(sql));
            if (res < 1)
                res = 1;
            return res;

        }
        /// <summary>
        /// Execute SQL Command and get Scalar result
        /// </summary>
        /// <param name="cond">sql select </param>
        /// <returns> و يمكنك تحويلها لاي قيمهobjectالقيمه من نوع </returns>
        public object GetDataAsScalar(string cond)
        {
            DMS.DAL.DataProccess dr = new DMS.DAL.DataProccess();
            dr.parameters.Add("@cond", cond);
            object Data = dr.executeScalar("dbo.executeCommand");
            if (Data != null)
                return Data;
            else
                return "";

        }

        /// <summary>
        /// Execute Stored Procedure and get Scalar result
        /// </summary>
        /// <param name="cond">sql select </param>
        /// <returns> و يمكنك تحويلها لاي قيمهobjectالقيمه من نوع </returns>
        public object GetDataAsScalarFromSP(string StoredProcedureName, Hashtable Parameters)
        {
            DMS.DAL.DataProccess dr = new DMS.DAL.DataProccess();
            dr.parameters = Parameters;
            object Data = dr.executeScalar(StoredProcedureName);
            if (Data != null)
                return Data;
            else
                return "";

        }

        /// <summary>
        /// Execute SQL Command and get DataTable result
        /// </summary>
        /// <param name="cond">Sql select</param>
        /// <returns>retarn Datat AS DataTable</returns>
        public DataTable GetDataAsDataTable(string cond)
        {
            DMS.DAL.DataProccess dr = new DMS.DAL.DataProccess();
            dr.parameters.Add("@cond", cond);
            return dr.excuteQuery("dbo.executeCommand");

        }

        /// <summary>
        /// Execute Stored Procedure and get DataTable result
        /// </summary>
        /// <param name="cond">Sql select</param>
        /// <returns>retarn Datat AS DataTable</returns>
        public DataTable GetDataAsDataTableFromSP(string StoredProcedureName, Hashtable Parameters)
        {
            DMS.DAL.DataProccess dr = new DMS.DAL.DataProccess();
            dr.parameters = Parameters;
            return dr.excuteQuery(StoredProcedureName);

        }

        /// <summary>
        /// Execute SQL Command without results (Returns number of effected rows)
        /// </summary>
        /// <param name="cond">SQL NonQuery</param>
        public Int32 NonQuery(string cond)
        {
            DMS.DAL.DataProccess dr = new DMS.DAL.DataProccess();
            dr.parameters.Add("@cond", cond);
            return convertToInt32(dr.excuteNonQuery("dbo.executeCommand"));

        }

        /// <summary>
        /// Execute Stored Procedure without results (Returns number of effected rows)
        /// </summary>
        /// <param name="cond">SQL NonQuery</param>
        public Int32 NonQueryFromSP(string StoredProcedureName, Hashtable Parameters)
        {
            DMS.DAL.DataProccess dr = new DMS.DAL.DataProccess();
            dr.parameters = Parameters;
            return convertToInt32(dr.excuteNonQuery(StoredProcedureName));

        }
        #region executeQuery
        public void FillSqlCond(Control Root, string[] ColumnName)
        {
            HttpContext.Current.Session["sql"] = "(1=1) ";
            for (int i = 0; i <= ColumnName.Length - 1; i++)
            {
                TextBox txt = new TextBox();
                CheckBox chk = new CheckBox();
                DropDownList drp = new DropDownList();
                ComboBox cmb = new ComboBox();
                RadioButtonList rdo = new RadioButtonList();
                HiddenField hdn = new HiddenField();
                string str = ColumnName[i].Split(' ')[1].ToString();
                str = str.Substring(0, 1).ToUpper() + str.Substring(1);
                if (FindControlRecursive(Root, "txt" + str) != null)
                {
                    txt = (TextBox)FindControlRecursive(Root, "txt" + str);
                    if (txt.ReadOnly == false)
                        CreateStringSql(txt.Text, str);
                }
                else if (FindControlRecursive(Root, "chk" + str) != null)
                {
                    chk = (CheckBox)FindControlRecursive(Root, "chk" + str);
                    CreateCheackSql(chk.Checked, str);
                }
                else if (FindControlRecursive(Root, "drp" + str) != null)
                {
                    drp = (DropDownList)FindControlRecursive(Root, "drp" + str);
                    if (drp.SelectedValue != "-32768")
                        CreateStringSql(drp.SelectedValue, str);
                }
                else if (FindControlRecursive(Root, "cmb" + str) != null)
                {
                    cmb = (ComboBox)FindControlRecursive(Root, "cmb" + str);
                    if (cmb.SelectedValue != "-32768")
                        CreateStringSql(drp.SelectedValue, str);
                }
                else if (FindControlRecursive(Root, "rdo" + str) != null)
                {
                    rdo = (RadioButtonList)FindControlRecursive(Root, "rdo" + str);
                    CreateStringSql(rdo.SelectedValue, str);
                }
                else if (FindControlRecursive(Root, "hdn" + str) != null)
                {
                    hdn = (HiddenField)FindControlRecursive(Root, "hdn" + str);
                    CreateStringSql(hdn.Value, str);

                }
            }
        }

        public void CreateStringSql(string str, string ColomenName)
        {
            if (str == "" || str == Int16.MinValue.ToString() || str == null)
            {
            }
            else
            {
                if (str.Contains("%"))
                {
                    //str = str.Replace("%", "");
                    HttpContext.Current.Session["sql"] += " and " + ColomenName + " Like N'" + str + "' ";
                }
                else if (str.Contains("<"))
                {
                    str = str.Replace("<", "");
                    HttpContext.Current.Session["sql"] += " and " + ColomenName + " < N'" + str + "'";
                }
                else if (str.Contains("<="))
                {
                    str = str.Replace("<", "");
                    str = str.Replace("=", "");

                    HttpContext.Current.Session["sql"] += " and " + ColomenName + " <= N'" + str + "'";
                }
                else if (str.Contains(">"))
                {
                    str = str.Replace(">", "");

                    HttpContext.Current.Session["sql"] += " and " + ColomenName + " > N'" + str + "'";
                }
                else if (str.Contains(">="))
                {
                    str = str.Replace(">", "");
                    str = str.Replace("=", "");

                    HttpContext.Current.Session["sql"] += " and " + ColomenName + " >= N'" + str + "'";
                }
                else
                {
                    HttpContext.Current.Session["sql"] += " and " + ColomenName + " = N'" + str + "'";
                }
            }
        }
        public void CreateCheackSql(Boolean str, string ColomenName)
        {
            if (str == true)
            {
                HttpContext.Current.Session["sql"] += " and " + ColomenName + " = 1 ";
            }

        }
        #endregion

        //#region التفقيط
        ///// <summary>
        ///// يقوم بتحويل الارقام الى  نصوص ويتحمل حتى 9 مليون
        ///// </summary>
        ///// <param name="num"></param>
        ///// <returns></returns>
        //public string Num_Text(string num)
        //{
        //    try
        //    {
        //        DllNum.clsNumToText opnum = new DllNum.clsNumToText();
        //        return opnum.NumToText(num);
        //    }
        //    catch
        //    {
        //        return num;
        //    }
        //}
        //#endregion

        public string getDateArabicDayName(DateTime date)
        {
            string daystr = "";
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    daystr = "الجمعة";
                    break;
                case DayOfWeek.Monday:
                    daystr = "الإثنين";
                    break;
                case DayOfWeek.Saturday:
                    daystr = "السبت";
                    break;
                case DayOfWeek.Sunday:
                    daystr = "الأحد";
                    break;
                case DayOfWeek.Thursday:
                    daystr = "الخميس";
                    break;
                case DayOfWeek.Tuesday:
                    daystr = "الثلاثاء";
                    break;
                case DayOfWeek.Wednesday:
                    daystr = "الأربعاء";
                    break;
            }
            return daystr;
        }

        public string getDateArabicDayName(string strDate)
        {
            DateTime date = convertToDateTime(strDate);
            string daystr = "";
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    daystr = "الجمعة";
                    break;
                case DayOfWeek.Monday:
                    daystr = "الإثنين";
                    break;
                case DayOfWeek.Saturday:
                    daystr = "السبت";
                    break;
                case DayOfWeek.Sunday:
                    daystr = "الأحد";
                    break;
                case DayOfWeek.Thursday:
                    daystr = "الخميس";
                    break;
                case DayOfWeek.Tuesday:
                    daystr = "الثلاثاء";
                    break;
                case DayOfWeek.Wednesday:
                    daystr = "الأربعاء";
                    break;
            }
            return daystr;
        }

        #region" Date Table Opration "
        public enum ComputeType { Sum = 1 }
        public object DataTableCompute(DataTable dt, String ColumeName, string where = " 1=1 ", ComputeType type = ComputeType.Sum)
        {
            string expression = "";
            string filter = where;
            switch (type)
            {
                case ComputeType.Sum:
                    expression = "sum(" + ColumeName + ")";
                    break;
            }
            return dt.Compute(expression, filter);

        }

        #endregion
        public DateTime LongToDate(Int64 PDate)
        {
            DateTime dResult;
            string lYear;
            string lMonth;
            string lDay;

            string strDate = PDate.ToString();
            lDay = strDate.Substring(strDate.Length - 2);
            lMonth = strDate.Substring(strDate.Length - 4, 2);
            lYear = strDate.Substring(0, strDate.Length - 4);
            dResult = convertToDateTime(lDay + "/" + lMonth + "/" + lYear);
            return dResult;
            //LongToDate = DateSerial(lYear, lMonth, lDay)
        }


        public Int64 DateToLong(DateTime PDate)
        {
            string llngDateNumber;

            llngDateNumber = PDate.Year.ToString().PadLeft(4, '0') + PDate.Month.ToString().PadLeft(2, '0') + PDate.Day.ToString().PadLeft(2, '0');

            return convertToInt64(llngDateNumber);
        }

        public void convertDataTableToExcel(DataTable dt, string name)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();

            foreach (DataColumn column in dt.Columns)
            {
                context.Response.Write(column.ColumnName + ";");
            }

            context.Response.Write(Environment.NewLine);

            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    context.Response.Write(row[i].ToString().Replace(";", string.Empty) + ";");
                }
                context.Response.Write(Environment.NewLine);
            }

            context.Response.ContentType = "text/csv";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + name + ".csv");
            context.Response.End();
        }

        public struct decimalParts
        {
            public Int64 integer;
            public decimal fraction;
        }

        public decimalParts partDecimal(decimal value)
        {
            decimalParts dp;
            dp.integer = Convert.ToInt32(Math.Truncate(value));
            dp.fraction = value - dp.integer;
            return dp;
        }



        public DataTable getSQLTemplateResult(Control Root, string sql)
        {

            foreach (Match m in Regex.Matches(sql, @"@\w+([-+.']\w+)*"))
            {
                string cond = m.Value;
                sql = sql.Replace(m.Value, getControlText(Root, (m.Value.Substring(1))));
            }
            DataProccess dp = new DataProccess();
            dp.parameters.Clear();
            dp.parameters.Add("@cond", sql);
            DataTable dt = new DataTable();
            dt = dp.excuteQuery("dbo.executeCommand");

            return dt;
        }

        public DataTable getUserFolders(Int32 userID, bool isDiwan)
        {
            DataProccess dp = new DataProccess();
            dp.parameters.Clear();
            dp.parameters.Add("@userID", userID);
            dp.parameters.Add("@isDiwan", isDiwan);
            DataTable dt = new DataTable();
            dt = dp.excuteQuery("dbo.getUserFolders");

            return dt;
        }

        public DataTable getUserFoldersWithoutCompanies(Int32 userID)
        {
            DataProccess dp = new DataProccess();
            dp.parameters.Clear();
            dp.parameters.Add("@userID", userID);
            DataTable dt = new DataTable();
            dt = dp.excuteQuery("dbo.getUserFoldersWithoutCompanies");

            return dt;
        }

        public string getUserName(Int32 userID)
        {
            string res = "";
            op = new operations();
            try
            {
                res = op.dboGetUsersByPrimaryKey(userID).fieldFullName;
            }
            catch { }

            return res;
        }

        public enum fileType
        {
            Text, Image, Word_Document, Excel, Access, PDF, Video, Audio, Power_Point, Other, TIFF
        }

        public fileType getFileType(string ext)
        {
            string[] _image = { "jpg", "jpeg", "bmp", "gif", "png", "wmf" };
            string[] _tiff = { "tif", "tiff" };
            string[] _word = { "doc", "docx" };
            string[] _text = { "txt", "inf", "bat", "xml", "html", "htm", "xhtm", "xhtml", "sys" };
            string[] _excel = { "xls","xlsx" };
            string[] _access = { "mdb", "accdb" };
            string[] _pdf = { "pdf" };
            string[] _video = { "avi", "mpg", "mpeg", "mp4", "mov", "fla", "qt", "asf", "wmv", "asx", "wma", "wmx", "ra", "rm", "ram", "3gp", "ogm", "mkv" };
            string[] _audio = { "mp3", "wav", "aif", "iff", "m3u", "m4a", "mid", "mpa", "ra", "wma" };
            string[] _power_Point = { "ppt", "ppx", "pptx" };

            if (Array.IndexOf(_image, ext) > -1)
                return fileType.Image;
            else if (Array.IndexOf(_tiff, ext) > -1)
                return fileType.TIFF;
            else if (Array.IndexOf(_access, ext) > -1)
                return fileType.Access;
            else if (Array.IndexOf(_audio, ext) > -1)
                return fileType.Audio;
            else if (Array.IndexOf(_excel, ext) > -1)
                return fileType.Excel;
            else if (Array.IndexOf(_pdf, ext) > -1)
                return fileType.PDF;
            else if (Array.IndexOf(_power_Point, ext) > -1)
                return fileType.Power_Point;
            else if (Array.IndexOf(_text, ext) > -1)
                return fileType.Text;
            else if (Array.IndexOf(_video, ext) > -1)
                return fileType.Video;
            else if (Array.IndexOf(_word, ext) > -1)
                return fileType.Word_Document;
            else
                return fileType.Other;

        }

        public static string WordScrambler(Match match)
        {
            string replace = @"document.getElementById(""" + match.Value + @""").value";
            return replace;
        }

        public string fixMetaExp(string exp, Int32 index = 0)
        {
            Int32 i = index;
            string textValue = exp;
            textValue = textValue.Substring(textValue.IndexOf(":") + 1);
            MatchEvaluator evaluator = new MatchEvaluator(WordScrambler);
            textValue = Regex.Replace(textValue, @"meta_\d+", evaluator);
            textValue = textValue.Replace("this", "document.getElementById('meta_" + (i + 1).ToString() + "').value");
            textValue = textValue.Replace("this", "document.getElementById('meta_" + (i + 1).ToString() + "').value");
            textValue = textValue.Replace("FolderSeq", "document.getElementById('hdnFolderSeq').value");
            textValue = textValue.Replace("DocTypeSeq", "document.getElementById('hdnDocTypeSeq').value");
            textValue = textValue.Replace("FolderDocTypeSeq", "document.getElementById('hdnFolderDocTypeSeq').value");

            return textValue;
        }
    
        public void GetTblRowAndColumnNumber(string defaultValue, out int rowNumber, out int columnNumber)
        {
            rowNumber = 0;
            columnNumber = 0;
            if (defaultValue.IsValid())
            {
                rowNumber = defaultValue.Split(',')[0].Split('_')[1].ToInt();
                columnNumber = defaultValue.Split(',')[1].Split('_')[1].ToInt();
            }
        }
    }

}