using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DistributedTimer.Hubs
{
    public class TimerHub : Hub
    {
        // public async Task SendMessage(string user, string message)
        // {
        //     await Clients.All.SendAsync("UpdateTime", user, message);
        // }
    }
}