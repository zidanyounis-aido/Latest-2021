using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.Resources
{
    public class CalendarEvent
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public bool allDay { get; set; }
        public int? DocumentId { set; get; }
        public string CreatedBy { set; get; }
        public string backgroundColor { get; set; }
        public string color { get; set; }
    }
}