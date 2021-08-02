using System;

namespace BattlEyeManager.Core.DataContracts.Models
{
    public class Player
    {
        public Player()
        {
            LastSeen = DateTime.UtcNow;
        }

        public static Player Copy(Player inner)
        {
            var copy = new Player()
            {
                Id = inner.Id,
                GUID = inner.GUID,
                Name = inner.Name,
                Comment = inner.Comment,
                IP = inner.IP,
                LastSeen = inner.LastSeen,
                SteamId = inner.SteamId
            };

            return copy;
        }

        public int Id { get; set; }
        public string SteamId { get; set; }
        public string GUID { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public DateTime LastSeen { get; set; }
    }
}
