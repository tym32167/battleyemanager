﻿using AutoMapper;
using BattleNET;
using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.Spa.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Services
{
    public class OnlineServerService
    {
        private readonly ServerStateService _serverStateService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IBeServerAggregator _beServerAggregator;

        public OnlineServerService(ServerStateService serverStateService, IServiceScopeFactory scopeFactory, IBeServerAggregator beServerAggregator)
        {
            _serverStateService = serverStateService;
            _scopeFactory = scopeFactory;
            _beServerAggregator = beServerAggregator;
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
                        .Select(x => Update(Mapper.Map<OnlineServerModel>(x)))
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

                    var ret = Mapper.Map<OnlineServerModel>(item);
                    ret = Update(ret);

                    return ret;
                }
            }
        }

        private OnlineServerModel Update(OnlineServerModel input)
        {
            input.PlayersCount = _serverStateService.GetPlayersCount(input.Id);
            input.AdminsCount = _serverStateService.GetAdminsCount(input.Id);
            input.BansCount = _serverStateService.GetBansCount(input.Id);
            input.IsConnected = _serverStateService.IsConnected(input.Id);
            return input;
        }


        private static Dictionary<string, BattlEyeCommand> _commands = new Dictionary<string, BattlEyeCommand>();

        public async Task Execute(OnlineServerCommandModel command)
        {
            if (!_commands.ContainsKey(command.Command))
                throw new NotSupportedException();
            var c = _commands[command.Command];
            _beServerAggregator.Send(command.ServerId, c);
        }
    }
}