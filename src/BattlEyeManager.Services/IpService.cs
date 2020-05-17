using BattlEyeManager.Core;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;
using System;
using System.Collections.Concurrent;
using System.Net;

namespace BattlEyeManager.Services
{
    public class IpService : IIpService
    {
        private readonly ILog _log;

        public IpService(string pathToDatabase, ILog log)
        {
            _log = log;
            _log.Info($"IpService initialized with {pathToDatabase}");
            reader = new DatabaseReader(pathToDatabase);
        }

        private DatabaseReader reader;

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

        private readonly ConcurrentDictionary<string, string> _cache = new ConcurrentDictionary<string, string>();

        public string GetCountry(string ip)
        {
            if (string.IsNullOrEmpty(ip)) return string.Empty;

            if (_cache.TryGetValue(ip, out string country))
            {
                return country;
            }

            var c = GetCountryImpl(ip);
            _cache.AddOrUpdate(ip, k => c, (k, p) => c);
            return c;
        }

        private string GetCountryImpl(string ip)
        {
            if (string.IsNullOrEmpty(ip)) return string.Empty;

            try
            {
                if (reader.TryCountry(ip, out CountryResponse country))
                {
                    var result = country.Country.Name;
                    return result;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return string.Empty;
            }
        }
    }
}