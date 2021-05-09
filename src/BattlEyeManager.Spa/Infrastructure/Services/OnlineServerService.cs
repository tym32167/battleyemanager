using BattleNET;
using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.Spa.Core.Mapping;
using BattlEyeManager.Spa.Infrastructure.State;
using BattlEyeManager.Spa.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Infrastructure.Services
{
    public class OnlineServerService
    {
        private readonly ServerStateService _serverStateService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IBeServerAggregator _beServerAggregator;
        private readonly OnlinePlayerStateService _onlinePlayerStateService;
        private readonly IMapper _mapper;

        public OnlineServerService(ServerStateService serverStateService,
            IServiceScopeFactory scopeFactory,
            IBeServerAggregator beServerAggregator,
            OnlinePlayerStateService onlinePlayerStateService,
            IMapper mapper)
        {
            _serverStateService = serverStateService;
            _scopeFactory = scopeFactory;
            _beServerAggregator = beServerAggregator;
            _onlinePlayerStateService = onlinePlayerStateService;
            _mapper = mapper;
        }


        public async Task<OnlineServerModel[]> GetOnlineServers()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var dbItems = await ctx.Servers
                        .Where(x => x.Active)
                        .OrderBy(x => x.Name)
                        .ToArrayAsync();

                    var items = dbItems
                        .Select(x => Update(_mapper.Map<OnlineServerModel>(x)))
                        .ToArray();

                    return items;
                }
            }
        }

        public async Task<OnlineServerModel> GetOnlineServer(int serverId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var item = await ctx.Servers.FindAsync(serverId);
                    if (item == null) return null;

                    var ret = _mapper.Map<OnlineServerModel>(item);
                    ret = Update(ret);

                    return ret;
                }
            }
        }

        private OnlineServerModel Update(OnlineServerModel input)
        {
            input.PlayersCount = _onlinePlayerStateService.GetPlayersCount(input.Id);
            input.AdminsCount = _serverStateService.GetAdminsCount(input.Id);
            input.BansCount = _serverStateService.GetBansCount(input.Id);
            input.IsConnected = _serverStateService.IsConnected(input.Id);
            return input;
        }


        private static Dictionary<string, BattlEyeCommand> _commands = new Dictionary<string, BattlEyeCommand>()
        {
            { "lock", BattlEyeCommand.Lock},
            { "unlock", BattlEyeCommand.Unlock},
            { "shutdown", BattlEyeCommand.Shutdown},
            { "restart", BattlEyeCommand.Restart},
            { "restartserver", BattlEyeCommand.RestartServer},
            { "init", BattlEyeCommand.Init},
            { "reassign", BattlEyeCommand.Reassign},
            { "loadbans", BattlEyeCommand.LoadBans},
            { "loadscripts", BattlEyeCommand.LoadScripts},
            { "loadevents", BattlEyeCommand.LoadEvents},

        };

        public Task Execute(OnlineServerCommandModel command)
        {
            if (!_commands.ContainsKey(command.Command))
                throw new NotSupportedException();
            var c = _commands[command.Command];
            _beServerAggregator.Send(command.ServerId, c);
            return Task.FromResult(true);
        }


        public async Task<int> GetPlayerSessionCount(int serverId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    return await ctx.PlayerSessions
                        .Where(x => x.ServerId == serverId)
                        .CountAsync();
                }
            }
        }

        public async Task<PlayerSessionModel[]> GetPlayerSessions(int serverId, int skip, int take, DateTime startSearh, DateTime endSearch)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var dbItems = await ctx.PlayerSessions
                        .Where(x => x.ServerId == serverId
                                && x.StartDate != x.EndDate
                                && x.StartDate > startSearh
                                && x.StartDate < endSearch)
                        .OrderByDescending(x => x.StartDate)
                        .Skip(skip)
                        .Take(take)
                        .ToArrayAsync();

                    var items = dbItems
                        .Select(x => _mapper.Map<PlayerSessionModel>(x))
                        .ToArray();

                    return items;
                }
            }
        }
    }
}