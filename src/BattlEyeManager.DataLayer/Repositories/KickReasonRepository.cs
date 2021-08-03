using BattlEyeManager.Core.DataContracts.Models.Values;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class KickReasonRepository : GenericRepository<KickReason, int, Models.KickReason, int>, IKickReasonRepository
    {
        protected KickReasonRepository(AppDbContext context) : base(context)
        {
        }

        protected override KickReason ToItem(Models.KickReason model)
        {
            return new KickReason()
            {
                Id = model.Id,
                Text = model.Text
            };
        }

        protected override int ToItemKey(int modelKey)
        {
            return modelKey;
        }

        protected override Models.KickReason ToModel(KickReason item)
        {
            return new Models.KickReason()
            {
                Id = item.Id,
                Text = item.Text
            };
        }

        protected override int ToModelKey(int itemKey)
        {
            return itemKey;
        }
    }
}