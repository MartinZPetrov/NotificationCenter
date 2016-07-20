using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotificationCenter
{
    public sealed class Messages
    {
        private static Messages instance = null;
      
        private static object padlock = new object();
        private bool _isCheckCertificateExpiration;
        private string _notificationMessage;
        private bool _isModifiedStatus;
        private Tuple<string, string, string> _statusController;

        private Messages()
        {
            
        }
        public static Messages Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Messages();
                    }
                    return instance;
                }
            }
        }

        public string NotificationMessages
        {
            get
            {
                return _notificationMessage;
            }
            set
            {
                _notificationMessage = value;
            }
        }

        public bool IsModifiedStatus
        {
            get
            {
                return _isModifiedStatus;
            }
            set
            {
                _isModifiedStatus = value;
            }
        }


        public bool IsCheckCertificateExpiration
        {
            get
            {
                return _isCheckCertificateExpiration;
            }
            set
            {
                _isCheckCertificateExpiration = value;
            }
        }
       
    }
}