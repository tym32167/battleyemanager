using BattlEyeManager.Core;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;

namespace BattlEyeManager.DataLayer.Repositories
{
    public abstract class BaseRepository : DisposeObject, IRepository
    {
        private readonly AppDbContext context;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            this.context?.Dispose();
        }
    }
}
