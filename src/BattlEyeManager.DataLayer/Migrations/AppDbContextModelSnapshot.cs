﻿// <auto-generated />
using System;
using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BattlEyeManager.DataLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("IP");

                    b.Property<int>("Num");

                    b.Property<int>("Port");

                    b.Property<int>("ServerId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.BadNickname", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("BadNicknames");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.BanReason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("BanReasons");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.BanTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TimeInMinutes");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("BanTimes");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("ServerId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("Date");

                    b.HasIndex("ServerId");

                    b.HasIndex("Text");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ImportantWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("ImportantWords");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.KickReason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("KickReasons");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<string>("GUID");

                    b.Property<string>("IP");

                    b.Property<DateTime>("LastSeen");

                    b.Property<string>("Name");

                    b.Property<string>("SteamId");

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
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<DateTime>("Date");

                    b.Property<int>("PlayerId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerNotes");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.PlayerSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("IP");

                    b.Property<string>("Name");

                    b.Property<int>("PlayerId");

                    b.Property<int>("ServerId");

                    b.Property<DateTime>("StartDate");

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
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Host")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password");

                    b.Property<int>("Port");

                    b.Property<int>("SteamPort");

                    b.Property<string>("WelcomeFeatureEmptyTemplate")
                        .HasMaxLength(255);

                    b.Property<bool>("WelcomeFeatureEnabled");

                    b.Property<string>("WelcomeFeatureTemplate")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ServerBan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CloseDate");

                    b.Property<DateTime>("Date");

                    b.Property<string>("GuidIp");

                    b.Property<bool>("IsActive");

                    b.Property<int>("Minutes");

                    b.Property<int>("MinutesLeft");

                    b.Property<int>("Num");

                    b.Property<int?>("PlayerId");

                    b.Property<string>("Reason");

                    b.Property<int>("ServerId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("ServerId");

                    b.ToTable("ServerBans");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ServerModerators", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ServerId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.HasIndex("UserId");

                    b.ToTable("ServerModerators");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ServerUserCount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PlayersCount");

                    b.Property<int>("ServerId");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("ServerUserCounts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.Admin", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany("Admins")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ChatMessage", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany("ChatMessages")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.PlayerNote", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Player", "Player")
                        .WithMany("Notes")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.PlayerSession", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Player", "Player")
                        .WithMany("PlayerSessions")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany("PlayerSessions")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ServerBan", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Player", "Player")
                        .WithMany("ServerBans")
                        .HasForeignKey("PlayerId");

                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany("ServerBans")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ServerModerators", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany("Servers")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BattlEyeManager.DataLayer.Models.ApplicationUser", "User")
                        .WithMany("Servers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BattlEyeManager.DataLayer.Models.ServerUserCount", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.Server", "Server")
                        .WithMany()
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BattlEyeManager.DataLayer.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BattlEyeManager.DataLayer.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
