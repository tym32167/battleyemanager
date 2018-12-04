using AutoMapper;
using BattleNET;
using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Spa.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Player = BattlEyeManager.BE.Models.Player;

namespace BattlEyeManager.Spa.Services
{
    public class OnlinePlayerService
    {
        private readonly ServerStateService _serverStateService;
        private readonly IBeServerAggregator _serverAggregator;
        private readonly IServiceScopeFactory _scopeFactory;

        public OnlinePlayerService(ServerStateService serverStateService, IBeServerAggregator serverAggregator, IServiceScopeFactory scopeFactory)
        {
            _serverStateService = serverStateService;
            _serverAggregator = serverAggregator;
            _scopeFactory = scopeFactory;
        }

        public async Task<OnlinePlayerModel[]> GetOnlinePlayers(int serverId)
        {
            var players = _serverStateService.GetPlayers(serverId);
            var ret =
                players.Select(Mapper.Map<Player, OnlinePlayerModel>)
                    .ToArray();
            return ret;
        }

        public async Task KickAsync(int serverId, int playerNum, string playerGuid, string reason, string currentUser)
        {
            reason = $"[{currentUser}][{DateTime.UtcNow:dd.MM.yy HH:mm:ss}] {reason}";

            _serverAggregator.Send(serverId, BattlEyeCommand.Kick,
                $"{playerNum} {reason}");

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var player = await ctx.Players.FirstOrDefaultAsync(x => x.GUID == playerGuid);

                    player?.Notes.Add(new PlayerNote()
                    {
                        Author = currentUser,
                        Date = DateTime.UtcNow,
                        PlayerId = player.Id,
                        Text = $"Kicked with reason: {reason}"
                    });

                    await ctx.SaveChangesAsync();
                }
            }
        }
    }
}

