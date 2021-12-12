using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dms.MangeForm
{
    public class MetaCustomPermissionDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool AllowRead { get; set; } = false;
        public bool AllowEdit { get; set; } = false;
        public string PerType { get; set; }
    }
}