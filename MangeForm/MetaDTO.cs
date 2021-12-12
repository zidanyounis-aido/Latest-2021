using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using tables.dbo;

namespace dms.MangeForm
{
    public class MetaDTO
    {
        public int metaID { get; set; }
        public int docTypID { get; set; }
        public string metaDesc { get; set; }
        public string metaDescAr { get; set; }
        public string metaDataType { get; set; }
        public bool required { get; set; }
        public int orderSeq { get; set; }
        public int ctrlID { get; set; }
        public string defaultTexts { get; set; }
        public string defaultValues { get; set; }
        public string defaultArTexts { get; set; }
        public bool visible { get; set; }
        public int columnSeq { get; set; }
        public int metaIdFK { get; set; }
        public decimal width { get; set; }
        public string permissionType { get; set; }
        public List<string> lstDefaultTexts { get; set; } = new List<string>();
        public List<string> lstDefaultArTexts { get; set; } = new List<string>();
        public List<string> lstDefaultValues { get; set; } = new List<string>();
        public List<MetaDTO> tableCtrls { get; set; } = new List<MetaDTO>();

        public MetaDTO(metas obj)
        {
            metaID = obj.fieldMetaID;
            docTypID = obj.fieldDocTypID;
            metaDesc = obj.fieldMetaDesc;
            metaDescAr = obj.fieldMetaDescAr;
            metaDataType = obj.fieldMetaDataType;
            required = obj.fieldRequired;
            orderSeq = obj.fieldOrderSeq;
            ctrlID = obj.fieldCtrlID;
            defaultTexts = obj.fieldDefaultTexts;
            defaultValues = obj.fieldDefaultValues;
            defaultArTexts = obj.fieldDefaultArTexts;
            visible = obj.fieldVisible;
            columnSeq = obj.fieldColumnSeq;
            metaIdFK = obj.fieldMetaIdFK;
            width = obj.fieldWidth;
            permissionType = obj.fieldPermissionType;

            LoadDefaultText(obj);
        }



        private CommonFunction.clsCommon c => new CommonFunction.clsCommon();
        private void LoadDefaultText(metas obj)
        {
            if (ctrlID == (int)ControlType.CheckBoxList || ctrlID == (int)ControlType.DropDownList || ctrlID == (int)ControlType.RadioButtonList)
            {
                if (!defaultTexts.ToLower().StartsWith("db:"))
                {
                        LoadDefaultEnText(obj);
                    
                        LoadDefaultArText(obj);
                }
                else
                {
                    string[] query = obj.fieldDefaultTexts.Split(':');
                    string[] queryVal = obj.fieldDefaultValues.Split(':');
                    string tableName = query[1];
                    string textFeild = query[2];
                    string valueFeild = queryVal[2];
                    string cond = "";
                    if (query.Length > 3)
                        cond = " where " + query[3];
                    string drpSQL = $"select {valueFeild} , { textFeild } from { tableName } { cond } order by { textFeild}";
                    var drpDT = c.GetDataAsDataTable(drpSQL);

                    foreach (DataRow r in drpDT.Rows)
                    {
                        lstDefaultValues.Add(r[0].ToString());
                        lstDefaultTexts.Add(r[1].ToString());
                        lstDefaultArTexts.Add(r[0].ToString());
                    }
                }
                if (lstDefaultTexts.Count > 0 && lstDefaultValues.Count == 0)
                {
                    lstDefaultValues.AddRange(lstDefaultTexts);
                }
                if (lstDefaultArTexts.Count > 0 && lstDefaultValues.Count == 0)
                {
                    lstDefaultValues.AddRange(lstDefaultArTexts);
                }
            }
        }
        private void LoadDefaultEnText(metas obj)
        {
            char[] chars = { ',', ';', '،' };
            if (obj.fieldDefaultTexts.IndexOfAny(chars) > -1)
            {
                string[] texts;
                texts = obj.fieldDefaultTexts.Split(chars);
                string[] values;
                values = obj.fieldDefaultValues.Split(chars);
                for (var  j = 0; j < texts.Length; j++)
                {
                    lstDefaultTexts.Add(texts[j].Trim());
                    lstDefaultValues.Add(j < values.Length ? values[j].Trim() : texts[j].Trim());
                }
            }
            else
            {
                lstDefaultTexts.Add(obj.fieldDefaultTexts.Trim());
                lstDefaultValues.Add(obj.fieldDefaultValues.Trim());
            }
        }
        private void LoadDefaultArText(metas obj)
        {
            var boolIsDefaultValueLoaded = lstDefaultValues.Count > 0;
            char[] chars = { ',', ';', '،' };
            if (obj.fieldDefaultArTexts.IndexOfAny(chars) > -1)
            {
                string[] texts;
                texts = obj.fieldDefaultArTexts.Split(chars);
                string[] values;
                values = obj.fieldDefaultValues.Split(chars);
                for (var j = 0; j < texts.Length; j++)
                {
                    lstDefaultArTexts.Add(texts[j].Trim());
                    if (!boolIsDefaultValueLoaded)
                    {
                        lstDefaultValues.Add(j < values.Length ? values[j].Trim() : texts[j].Trim());
                    }
                }
            }
            else
            {
                lstDefaultArTexts.Add(obj.fieldDefaultArTexts.Trim());
                lstDefaultValues.Add(obj.fieldDefaultValues.Trim());
            }
        }
    }
}