using BattlEyeManager.BE.Core;
using BattlEyeManager.BE.Logging;
using System.Diagnostics;
using System.Net;

namespace BattlEyeManager.BE.Net
{
    public class IpService : DisposeObject, IIpService
    {
        private static readonly ILog Log = LogFactory.Create(new StackTrace().GetFrame(0).GetMethod().DeclaringType);

        public IpService()
        {
        }

        public string GetIpAddress(string host)
        {
            IPAddress ip;

            if (IPAddress.TryParse(host, out ip))
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