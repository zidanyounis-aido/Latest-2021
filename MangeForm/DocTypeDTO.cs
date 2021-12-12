using tables.dbo;

namespace dms.MangeForm
{
    public class DocTypeDTO
    {
        public int DocTypeId { get; set; }
        public string DocTypeDesc { get; set; }
        public string DocTypeDescAr { get; set; }
        public int DefaultWFId { get; set; }
        public bool DocTypeIsActive { get; set; }
        public DocTypeDTO(docTypes obj)
        {
            DocTypeId = obj.fieldDocTypID;
            DocTypeDesc = obj.fieldDocTypDesc;
            DocTypeDescAr = obj.fieldDocTypDescAr;
            DefaultWFId = obj.fieldDefaultWFID;
            DocTypeIsActive = obj.fieldActive;
        }
    }
}