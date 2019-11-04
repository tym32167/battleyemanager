using System.Net;

namespace BattlEyeManager.Steam
{
    public interface ISteamService
    {
        ServerRulesResult GetServerRulesSync(IPEndPoint endpoint);
        ServerPlayers GetServerChallengeSync(IPEndPoint endpoint);
        ServerInfoResult GetServerInfoSync(IPEndPoint endpoint);
    }
}