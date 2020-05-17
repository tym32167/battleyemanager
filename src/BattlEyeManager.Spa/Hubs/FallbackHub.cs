using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Hubs
{
    public class FallbackHub : Hub
    {
        public Task Send(int serverId, string message)
        {
            return Clients.All.SendAsync("event", serverId, message);
        }
    }
}