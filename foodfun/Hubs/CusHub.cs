using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace foodfun.Hubs
{
    public class CusHub : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<CusHub>();
            context.Clients.All.displayGoPASTA();
        }
    }
}