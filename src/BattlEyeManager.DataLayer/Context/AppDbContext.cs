﻿using BattlEyeManager.DataLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BattlEyeManager.DataLayer.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySql(@"server=localhost; database=battleyemanager; port=3306; user=root ");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Player>()
                .HasIndex(u => u.GUID)
                .IsUnique();


            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Server> Servers { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerSession> PlayerSessions { get; set; }
        public DbSet<ServerBan> ServerBans { get; set; }
        public DbSet<PlayerNote> PlayerNotes { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<KickReason> KickReasons { get; set; }
        public DbSet<ImportantWord> ImportantWords { get; set; }
        public DbSet<BanTime> BanTimes { get; set; }
        public DbSet<BanReason> BanReasons { get; set; }
        public DbSet<BadNickname> BadNicknames { get; set; }
    }
}
