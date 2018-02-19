using System;

namespace BattlEyeManager.BE.Services
{
    public interface IBeServerAggregator
    {
        bool AddServer(ServerInfo info);
        bool RemoveServer(Guid serverId);
    }
}