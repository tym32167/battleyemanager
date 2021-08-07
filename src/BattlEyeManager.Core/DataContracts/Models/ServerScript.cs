using System;
using System.Collections.Generic;

namespace BattlEyeManager.Core.DataContracts.Models
{
    public class ServerScript
    {
        public int Id { get; set; }
        public int ServerId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public class ServerStatsResult
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public List<ServerStatInfo> Servers { get; set; } = new List<ServerStatInfo>();

        public List<ServerStatItemResult> Data { get; set; } = new List<ServerStatItemResult>();
    }

    public class ServerStatItemResult
    {
        public DateTime Date { get; set; }
        public int ServerId { get; set; }
        public string ServerName { get; set; }
        public int PlayerCount { get; set; }
    }

    public class ServerStatInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}