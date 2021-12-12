using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dms.DTOS
{
    public class EventsDocumentGridItems
    {
        public int event_id { set; get; }
        public string title { set; get; }
        public string event_start { set; get; }
        public string event_end { set; get; }
        public string Time { set; get; }
        public string CreatedBy { set; get; }

    }
}