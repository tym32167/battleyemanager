using BattlEyeManager.DataLayer.Repositories.Players;
using System.Collections.Generic;

namespace BattlEyeManager.DataLayer.Models
{
    public class Player : PlayerDto
    {
        public ICollection<PlayerSession> PlayerSessions { get; set; }
        public ICollection<ServerBan> ServerBans { get; set; }
        public ICollection<PlayerNote> Notes { get; set; }
        public ICollection<PlayerPoints> PlayerPoints { get; set; }
        public ICollection<PlayerPointsHistory> PlayerPointsHistory { get; set; }
    }
}