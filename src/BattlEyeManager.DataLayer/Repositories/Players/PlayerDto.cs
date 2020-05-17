using System;

namespace BattlEyeManager.DataLayer.Repositories.Players
{
    public class PlayerDto
    {
        public PlayerDto()
        {
            LastSeen = DateTime.UtcNow;
        }

        public static PlayerDto Copy(PlayerDto inner)
        {
            var copy = new PlayerDto()
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