using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotificationCenter.Model
{
    public class Notification
    {
        public int NotificationID { get; set; }

        public string Name { get; set; }

        public string CriteriaGroup { get; set; }
        public string TypeGroup { get; set; }

        public string Channel { get; set; }
    }
}