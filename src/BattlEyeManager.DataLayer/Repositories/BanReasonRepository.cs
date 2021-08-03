using BattlEyeManager.Core.DataContracts.Models.Values;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class BanReasonRepository : GenericRepository<BanReason, int, Models.BanReason, int>, IBanReasonRepository
    {
        protected BanReasonRepository(AppDbContext context) : base(context)
        {
        }

        protected override BanReason ToItem(Models.BanReason model)
        {
            return new BanReason()
            {
                Id = model.Id,
                Text = model.Text
            };
        }

        protected override int ToItemKey(int modelKey)
        {
            return modelKey;
        }

        protected override Models.BanReason ToModel(BanReason item)
        {
            return new Models.BanReason()
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