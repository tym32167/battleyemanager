using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class PlayerNoteRepository : BaseRepository, IPlayerNoteRepository
    {
        public PlayerNoteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
