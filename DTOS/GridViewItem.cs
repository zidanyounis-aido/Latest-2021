using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dms.DTOS
{
    public class GridViewItem
    {
        public int Id { set; get; }

        public bool IsComplete { set; get; }
        public string TaskName { set; get; }
        public string TaskDate { set; get; }
        public string TaskTime { set; get; }
        public string AssignTo { set; get; }
        public string CreatedBy { set; get; }
        public string TaskType { set; get; }
        public string Description { set; get; }
        public string CreatedUserID { set; get; }
        public string StsAr { set; get; }
        public string StsEn { set; get; }
        public int NumberOfComments { get; set; }
    }
}