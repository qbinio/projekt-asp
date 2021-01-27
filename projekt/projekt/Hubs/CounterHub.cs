using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Hubs
{
    public class CounterHub : Hub
    {
        public static int userCount = 0;
        public override Task OnConnectedAsync()
        {
            userCount++;
            Clients.All.SendAsync("updateCount", userCount);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            userCount--;
            Clients.All.SendAsync("updateCount", userCount);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
