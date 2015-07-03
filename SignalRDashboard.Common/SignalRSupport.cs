using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRDashboard.Common
{
    /// <summary>
    /// A singleton class used to handle communications with the Web sites SignalR hub.
    /// </summary>
    public sealed class SignalRSupport
    {
        // hold a refernce to the single instance of the SignalRSupport class.
        private static readonly SignalRSupport instance = new SignalRSupport();

        IHubProxy statusHubProxy;

        private SignalRSupport()
        {
            // create a connection to the hub
            // note that this is a disposable object, but I do not dispose of it here.
            // the connection will be used for the life of the application, so this is lazy but acceptable.
            var hubConnection = new HubConnection("http://localhost:15301/");

            // create a client proxy class to use to communicate with the hub.
            statusHubProxy = hubConnection.CreateHubProxy("StatusHub");

            // start the connection and wait for it to complete.
            // without .wait, the code would continue while the connection is made.
            hubConnection.Start().Wait();
      
        }

        public static SignalRSupport Instance
        {
            get
            {
                // return the single instance of the SignalRSupport class.  
                // if it doesn't already exist, then it will be created.
                return instance;
            }
        }

        public void SendStatusUpdate(string clientId, string systemName, string value, string status)
        {
            // invoke the SendStatus method that is defined on the hub.
            statusHubProxy.Invoke("SendStatus", clientId, systemName, value, status);
        }
    }
}
