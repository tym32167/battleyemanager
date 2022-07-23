﻿// <auto-generated />
using System;
using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BattlEyeManager.DataLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220717191028_PlayerPoints")]
    partial class PlayerPoints
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.15");

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("IP")
                        .HasColumnType("longtext");

                    b.Property<int>("Num")
                        .HasColumnType("int");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.BadNickname", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("BadNicknames");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.BanReason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("BanReasons");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.BanTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("TimeInMinutes")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("BanTimes");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Date");

                    b.HasIndex("ServerId");

                    b.HasIndex("Text");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ImportantWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ImportantWords");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.KickReason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("KickReasons");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext");

                    b.Property<string>("GUID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("IP")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("LastSeen")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SteamId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("GUID")
                        .IsUnique();

                    b.HasIndex("IP");

                    b.HasIndex("Name");

                    b.HasIndex("SteamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.PlayerNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Author")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerNotes");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.PlayerPoints", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("ServerId");

                    b.ToTable("PlayerPoints");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.PlayerPointsHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("longtext");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("ServerId");

                    b.ToTable("PlayerPointsHistory");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.PlayerSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("IP")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("EndDate");

                    b.HasIndex("PlayerId");

                    b.HasIndex("ServerId");

                    b.HasIndex("StartDate");

                    b.ToTable("PlayerSessions");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.Server", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<int>("SteamPort")
                        .HasColumnType("int");

                    b.Property<bool>("ThresholdFeatureEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ThresholdFeatureMessageTemplate")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ThresholdMinHoursCap")
                        .HasColumnType("int");

                    b.Property<string>("WelcomeFeatureEmptyTemplate")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("WelcomeFeatureEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("WelcomeFeatureTemplate")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("WelcomeGreater50MessageTemplate")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ServerBan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CloseDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("GuidIp")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Minutes")
                        .HasColumnType("int");

                    b.Property<int>("MinutesLeft")
                        .HasColumnType("int");

                    b.Property<int>("Num")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("longtext");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("ServerId");

                    b.ToTable("ServerBans");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ServerModerators", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.HasIndex("UserId");

                    b.ToTable("ServerModerators");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ServerScript", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("ServerScripts");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ServerUserCount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PlayersCount")
                        .HasColumnType("int");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("ServerUserCounts");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.WelcomeFeatureBlackList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Guid")
                        .HasColumnType("longtext");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("WelcomeFeatureBlackList");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.Admin", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany("Admins")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ChatMessage", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany("ChatMessages")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.PlayerNote", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Player", "Player")
                        .WithMany("Notes")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.PlayerPoints", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Player", "Player")
                        .WithMany("PlayerPoints")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany("PlayerPoints")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Server");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.PlayerPointsHistory", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Player", "Player")
                        .WithMany("PlayerPointsHistory")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany("PlayerPointsHistory")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Server");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.PlayerSession", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Player", "Player")
                        .WithMany("PlayerSessions")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany("PlayerSessions")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Server");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ServerBan", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Player", "Player")
                        .WithMany("ServerBans")
                        .HasForeignKey("PlayerId");

                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany("ServerBans")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Server");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ServerModerators", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany("Servers")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BattlEyeManager.DataLayer.Models.ApplicationUser", "User")
                        .WithMany("Servers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ServerScript", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany("ServerScripts")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ServerUserCount", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany()
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BattlEyeManager.DataLayer.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ApplicationUser", b =>
                {
                    b.Navigation("Servers");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.Player", b =>
                {
                    b.Navigation("Notes");

                    b.Navigation("PlayerPoints");

                    b.Navigation("PlayerPointsHistory");

                    b.Navigation("PlayerSessions");

                    b.Navigation("ServerBans");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.Server", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("ChatMessages");

                    b.Navigation("PlayerPoints");

                    b.Navigation("PlayerPointsHistory");

                    b.Navigation("PlayerSessions");

                    b.Navigation("ServerBans");

                    b.Navigation("Servers");

                    b.Navigation("ServerScripts");
                });
#pragma warning restore 612, 618
        }
    }
}