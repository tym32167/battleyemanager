using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.DataLayer.Models
{
    public class Server
    {
        public int Id { get; set; }

        [Required]
        public string Host { get; set; }

        [Required]
        public int Port { get; set; }

        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public int SteamPort { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; }
        public ICollection<PlayerSession> PlayerSessions { get; set; }
        public ICollection<ServerBan> ServerBans { get; set; }
        public ICollection<Admin> Admins { get; set; }
    }

    public class Player
    {
        public int Id { get; set; }

        public string SteamId { get; set; }

        public string GUID { get; set; }
        public string Comment { get; set; }

        public string Name { get; set; }
        public string IP { get; set; }
        public DateTime LastSeen { get; set; }

        public ICollection<PlayerSession> PlayerSessions { get; set; }
        public ICollection<ServerBan> ServerBans { get; set; }
        public ICollection<PlayerNote> Notes { get; set; }
    }

    public class PlayerSession
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int ServerId { get; set; }
        public Server Server { get; set; }

        public string Name { get; set; }
        public string IP { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndtDate { get; set; }
    }


    public class ServerBan
    {
        public int Id { get; set; }

        public int? PlayerId { get; set; }
        public Player Player { get; set; }

        public int ServerId { get; set; }
        public Server Server { get; set; }

        public int Num { get; set; }
        public string GuidIp { get; set; }
        public int Minutes { get; set; }
        public int MinutesLeft { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
    }

    public class PlayerNote
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
    }

    public class Admin
    {
        public int Id { get; set; }

        public string IP { get; set; }
        public int Num { get; set; }
        public int Port { get; set; }

        public int ServerId { get; set; }
        public Server Server { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndtDate { get; set; }
    }

    public class KickReason
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class ImportantWord
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class BanTime
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TimeInMinutes { get; set; }
    }

    public class BanReason
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class BadNickname
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

}