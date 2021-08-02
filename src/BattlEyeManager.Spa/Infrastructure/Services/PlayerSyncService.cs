using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.Spa.Api.Sync;
using BattlEyeManager.Spa.Core.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Infrastructure.Services
{
    public class PlayerSyncService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public PlayerSyncService(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public async Task<int> GetPlayersCount()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var repo = scope.ServiceProvider.GetService<IPlayerRepository>())
                {
                    return await repo.PlayersTotalCount();
                }
            }
        }

        public async Task<PlayerSyncDto[]> GetPlayers(int skip, int take)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var repo = scope.ServiceProvider.GetService<IPlayerRepository>())
                {
                    var dbItems = await repo.GetPlayers(skip, take);

                    var items = dbItems
                        .Select(x => _mapper.Map<PlayerSyncDto>(x))
                        .ToArray();

                    return items;
                }
            }
        }

        public async Task Import(PlayerSyncDto[] requestPlayers)
        {
            var impoerData = requestPlayers
                .Where(x => x.GUID?.Length == 32)
                .GroupBy(x => x.GUID)
                .Select(x => x.First())
                .ToDictionary(x => x.GUID);

            var ids = impoerData.Keys.ToArray();

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var repo = scope.ServiceProvider.GetService<IPlayerRepository>())
                {

                    var request = requestPlayers.Select(x => new BattlEyeManager.Core.DataContracts.Models.Player()
                    {
                        Name = x.Name,
                        Comment = x.Comment,
                        GUID = x.GUID,
                        IP = x.IP,
                        LastSeen = new DateTime(x.LastSeen.Ticks, DateTimeKind.Utc),
                        SteamId = x.SteamId
                    }).ToArray();

                    await repo.ImportPlayers(request);
                }
            }
        }
    }
}