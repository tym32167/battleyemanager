using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Hubs
{
    public class FallbackHub : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.SendAsync("send", message);
        }
    }
}