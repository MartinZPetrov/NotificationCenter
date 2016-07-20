using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NotificationCenter.Hubs
{
    public class MessagesHub : Hub
    {
        private static string conString = ConfigurationManager.ConnectionStrings["constr"].ToString();
        public void Hello()
        {
            Clients.All.hello();
        }

        [HubMethodName("sendMessages")]
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessagesHub>();
            if (!string.IsNullOrEmpty(Messages.Instance.NotificationMessages))
            {
                context.Clients.All.updateMessages(Messages.Instance.NotificationMessages);
                Messages.Instance.NotificationMessages = string.Empty;
            }
        }
    }
}