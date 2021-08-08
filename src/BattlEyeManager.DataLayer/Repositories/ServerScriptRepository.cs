using BattlEyeManager.Core.DataContracts.Models;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class ServerScriptRepository : GenericRepository<ServerScript, int, Models.ServerScript, int>, IServerScriptRepository
    {
        private readonly AppDbContext _context;

        public ServerScriptRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ServerScript[]> GetByServerAsync(int serverId)
        {
            return await _context.ServerScripts.Where(s => s.ServerId == serverId).Select(x => ServerScriptRepository.Mapper.ToItem(x)).ToArrayAsync();
        }

        protected override ServerScript ToItem(Models.ServerScript model)
        {
            return ServerScriptRepository.Mapper.ToItem(model);
        }

        protected override int ToItemKey(int modelKey)
        {
            return modelKey;
        }

        protected override Models.ServerScript ToModel(ServerScript item)
        {
            return ServerScriptRepository.Mapper.ToModel(item);
        }

        protected override int ToModelKey(int itemKey)
        {
            return itemKey;
        }

        private static class Mapper
        {
            public static ServerScript ToItem(Models.ServerScript model)
            {
                return new ServerScript()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Path = model.Path,
                    ServerId = model.ServerId
                };
            }

            public static Models.ServerScript ToModel(ServerScript item)
            {
                return new Models.ServerScript()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Path = item.Path,
                    ServerId = item.ServerId
                };
            }
        }
    }
}