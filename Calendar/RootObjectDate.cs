using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dms.Calendar
{
    public class Month
    {
        public int number { get; set; }
        public string en { get; set; }
        public string ar { get; set; }
    }

    public class Designation
    {
        public string abbreviated { get; set; }
        public string expanded { get; set; }
    }

    public class Hijri
    {
        public string date { get; set; }
        public string format { get; set; }
        public string day { get; set; }
        public Month month { get; set; }
        public string year { get; set; }
        public Designation designation { get; set; }
    }

    public class Month2
    {
        public int number { get; set; }
        public string en { get; set; }
    }

    public class Designation2
    {
        public string abbreviated { get; set; }
        public string expanded { get; set; }
    }

    public class Gregorian
    {
        public string date { get; set; }
        public string format { get; set; }
        public string day { get; set; }
        public Month2 month { get; set; }
        public string year { get; set; }
        public Designation2 designation { get; set; }
    }

    public class Data
    {
        public Hijri hijri { get; set; }
        public Gregorian gregorian { get; set; }
    }

    public class RootObjectDate
    {
        public int code { get; set; }
        public string status { get; set; }
        public Data data { get; set; }
    }
}