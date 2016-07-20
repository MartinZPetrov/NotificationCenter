using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotificationCenter.Model
{
    public class Certificate
    {
        public int CertificateID { get; set; }
        public int ClientID { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

    }
}