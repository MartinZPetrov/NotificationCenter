using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotificationCenter
{
    public static  class StatusHelper
    {
        public static string StatusType {get;set;}

        public static string OldStatus {get;set;}

        public static string NewStatus {get;set;}

        static StatusHelper ()
        {
            StatusType = string.Empty;
            OldStatus = string.Empty;
            StatusType = string.Empty;
        }

        public static void Clear()
        {
            StatusType = string.Empty;
            OldStatus = string.Empty;
            NewStatus = string.Empty;
        }
        public static void AddStatus(string type, string oldStatus, string newStatus)
        {
            StatusType = type;
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }

        public static bool ContainsStatues()
        {
           if(string.IsNullOrEmpty(StatusType) && string.IsNullOrEmpty(OldStatus) && string.IsNullOrEmpty(NewStatus))
           {
               return false;
           }
           else
           {
               return true;
           }
        }

    }
}