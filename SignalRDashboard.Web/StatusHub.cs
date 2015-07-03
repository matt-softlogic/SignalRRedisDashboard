using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRDashboard.Web
{
    public class StatusHub: Hub
    {
        public void SendStatus(string connectionId, string systemName, string value, string status)
        {
            Clients.Client(connectionId).ProcessStatusMessage(systemName, value, status);
        }
    }
}