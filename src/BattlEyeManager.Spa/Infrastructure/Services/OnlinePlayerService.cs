using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BattleNET;
using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Spa.Infrastructure.State;
using BattlEyeManager.Spa.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Player = BattlEyeManager.BE.Models.Player;

namespace BattlEyeManager.Spa.Infrastructure.Services
{
    public class OnlinePlayerService
    {
        private readonly IBeServerAggregator _serverAggregator;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly MessageHelper _messageHelper;
        private readonly OnlinePlayerStateService _onlinePlayerStateService;

        public OnlinePlayerService(IBeServerAggregator serverAggregator,
            IServiceScopeFactory scopeFactory,
            MessageHelper messageHelper,
            OnlinePlayerStateService onlinePlayerStateService)
        {
            _serverAggregator = serverAggregator;
            _scopeFactory = scopeFactory;
            _messageHelper = messageHelper;
            _onlinePlayerStateService = onlinePlayerStateService;
        }

        public Task<OnlinePlayerModel[]> GetOnlinePlayers(int serverId)
        {
            var players = _onlinePlayerStateService.GetPlayers(serverId);
            var ret =
                players.Select(Mapper.Map<Player, OnlinePlayerModel>)
                    .OrderBy(x => x.Num)
                    .ToArray();
            return Task.FromResult(ret);
        }

        public async Task KickAsync(int serverId, int playerNum, string playerGuid, string reason, string currentUser)
        {
            reason = _messageHelper.GetKickMessage(reason, currentUser);

            _serverAggregator.Send(serverId, BattlEyeCommand.Kick,
                $"{playerNum} {reason}");

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var player = await ctx.Players.Include(p => p.Notes).FirstOrDefaultAsync(x => x.GUID == playerGuid);

                    if (player != null)
                    {
                        player.Notes.Add(new PlayerNote()
                        {
                            Author = currentUser,
                            Date = DateTime.UtcNow,
                            PlayerId = player.Id,
                            Text = $"Kicked with reason: {reason}"
                        });
                    }

                    await ctx.SaveChangesAsync();
                }
            }
        }


        public async Task BanGuidOnlineAsync(int serverId, int playerNum, string playerGuid, string reason, long minutes, string currentUser)
        {
            reason = _messageHelper.GetBanMessage(reason, minutes, currentUser);

            _serverAggregator.Send(serverId, BattlEyeCommand.Ban,
                $"{playerNum} {minutes} {reason}");

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var player = await ctx.Players.Include(p => p.Notes).FirstOrDefaultAsync(x => x.GUID == playerGuid);

                    if (player != null)
                    {
                        player.Notes.Add(new PlayerNote()
                        {
                            Author = currentUser,
                            Date = DateTime.UtcNow,
                            PlayerId = player.Id,
                            Text = $"Baned with reason: {reason}"
                        });

                        player.Comment = $"{player.Comment} | {reason}";
                    }

                    await ctx.SaveChangesAsync();
                }
            }
        }
    }


    public class MessageHelper
    {
        private readonly ISettingsStore _settingsStore;

        public MessageHelper(ISettingsStore settingsStore)
        {
            _settingsStore = settingsStore;
        }

        public string GetKickMessage(string reason, string currentUser)
        {
            // [{AdminName}][{Date} {Time}] {Reason}
            var templater = new StringTemplater();
            PrepareTemplate(reason, currentUser, templater);
            return templater.Template(_settingsStore.KickMessageTemplate);
        }

        public string GetBanMessage(string reason, long minutes, string currentUser)
        {
            // [{AdminName}][{Date} {Time}] {Reason}
            var templater = new StringTemplater();
            PrepareTemplate(reason, currentUser, templater);

            templater.AddParameter("Minutes", minutes == 0 ? $"perm" : $"{minutes}");

            return templater.Template(_settingsStore.BanMessageTemplate);
        }

        private static void PrepareTemplate(string reason, string currentUser, StringTemplater templater)
        {
            templater.AddParameter("AdminName", currentUser);
            templater.AddParameter("Reason", reason);
            templater.AddParameter("Date", () => DateTime.UtcNow.ToString("dd.MM.yy"));
            templater.AddParameter("Time", () => DateTime.UtcNow.ToString("HH:mm:ss"));
        }
    }


    public class StringTemplater
    {
        private readonly IDictionary<string, Func<string>> _params;

        public StringTemplater()
        {
            _params = new Dictionary<string, Func<string>>();
        }

        public string Template(string someText)
        {
            var result = someText;

            foreach (var p in _params)
            {
                result = result.Replace($"{{{p.Key}}}", p.Value());
            }

            return result;
        }

        public void AddParameter(string param, string value)
        {
            _params.Add(param, () => value);
        }

        public void AddParameter(string param, Func<string> value)
        {
            _params.Add(param, value);
        }
    }


    public interface ISettingsStore
    {
        string BanMessageTemplate { get; }
        string KickMessageTemplate { get; }
    }

    public class SettingsStore : ISettingsStore
    {
        public string BanMessageTemplate => "[{AdminName}][{Date} {Time}] {Reason} {Minutes}";
        public string KickMessageTemplate => "[{AdminName}][{Date} {Time}] {Reason}";
    }
}