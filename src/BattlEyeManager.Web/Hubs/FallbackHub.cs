using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BattlEyeManager.Web.Hubs
{
    public class FallbackHub : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.SendAsync("send", message);
        }
    }
}