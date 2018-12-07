using System.Net;
using BattlEyeManager.Core;

namespace BattlEyeManager.Services
{
    public class IpService : IIpService
    {
        public string GetIpAddress(string host)
        {
            if (IPAddress.TryParse(host, out var ip))
            {
                return ip.ToString();
            }
            try
            {
                var entry = Dns.GetHostEntry(host);
                return entry.AddressList[0].ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}