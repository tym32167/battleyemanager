using BattleNET;
using BattlEyeManager.BE.Services;
using BattlEyeManager.Core;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.Spa.Core.Mapping;
using BattlEyeManager.Spa.Infrastructure.State;
using BattlEyeManager.Spa.Infrastructure.Utils;
using BattlEyeManager.Spa.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Player = BattlEyeManager.BE.Models.Player;

namespace BattlEyeManager.Spa.Infrastructure.Services
{
    public class OnlinePlayerService
    {
        private readonly IBeServerAggregator _serverAggregator;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly MessageHelper _messageHelper;
        private readonly OnlinePlayerStateService _onlinePlayerStateService;
        private readonly IIpService _ipService;
        private readonly IMapper _mapper;

        public OnlinePlayerService(IBeServerAggregator serverAggregator,
            IServiceScopeFactory scopeFactory,
            MessageHelper messageHelper,
            OnlinePlayerStateService onlinePlayerStateService,
            IIpService ipService,
            IMapper mapper)
        {
            _serverAggregator = serverAggregator;
            _scopeFactory = scopeFactory;
            _messageHelper = messageHelper;
            _onlinePlayerStateService = onlinePlayerStateService;
            _ipService = ipService;
            _mapper = mapper;
        }

        public Task<OnlinePlayerModel[]> GetOnlinePlayers(int serverId)
        {
            var players = _onlinePlayerStateService.GetPlayers(serverId);
            var ret =
                players.Select(p => _mapper.Map<Player, OnlinePlayerModel>(p))
                    .OrderBy(x => x.Num)
                    .ToArray();

            foreach (var p in ret)
            {
                p.Country = _ipService.GetCountry(p.IP);
            }

            return Task.FromResult(ret);
        }

        public async Task KickAsync(int serverId, int playerNum, string playerGuid, string reason, string currentUser)
        {
            reason = _messageHelper.GetKickMessage(reason, currentUser);

            _serverAggregator.Send(serverId, BattlEyeCommand.Kick,
                $"{playerNum} {reason}");

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var repo = scope.ServiceProvider.GetService<IPlayerNoteRepository>())
                {
                    var note = $"Kicked with reason: {reason}";
                    await repo.AddNoteToPlayer(playerGuid, currentUser, note);
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
                using (var repo = scope.ServiceProvider.GetService<IPlayerNoteRepository>())
                {
                    var note = $"Banned with reason: {reason}";
                    await repo.AddNoteToPlayer(playerGuid, currentUser, note, note);
                }
            }
        }

        public async Task BanOfflineAsync(int serverId, int playerId, string reason, long minutes, string currentUser)
        {
            reason = _messageHelper.GetBanMessage(reason, minutes, currentUser);
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var notesRepo = scope.ServiceProvider.GetService<IPlayerNoteRepository>())
                {
                    using (var playerRepo = scope.ServiceProvider.GetService<IPlayerRepository>())
                    {
                        var player = await playerRepo.GetById(playerId);
                        _serverAggregator.Send(serverId, BattlEyeCommand.AddBan, $"{player.GUID} {minutes} {reason}");
                        var note = $"Banned with reason: {reason}";
                        await notesRepo.AddNoteToPlayer(player.GUID, currentUser, note, note);
                    }
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