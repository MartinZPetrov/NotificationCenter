using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotificationCenter.Model
{
    public class Client
    {
        public int ClientID { get; set; }

        public string Name { get; set; }

        public int GroupType { get; set; }

        public string Password { get; set; }
    }
}