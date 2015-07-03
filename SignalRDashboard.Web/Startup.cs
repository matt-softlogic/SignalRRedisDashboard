using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(SignalRDashboard.Web.Startup))]
namespace SignalRDashboard.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            //GlobalHost.DependencyResolver.UseRedis("server", port, "password", "AppName");
            GlobalHost.DependencyResolver.UseRedis("poc-dashboardhealth.redis.cache.windows.net", 
                6379, "hcJn/De4nrnWcksbbxVdlozwbZU26G6W8HzUkJhaaxU=", "SignalRRedisHealthDashboard");
            app.MapSignalR();
        }
    }
}