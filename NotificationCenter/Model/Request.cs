using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotificationCenter.Model
{
    public class Request
    {
        public int RequestID { get; set; }

        public int ClientID { get; set; }
        public string Type { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }

    }
}