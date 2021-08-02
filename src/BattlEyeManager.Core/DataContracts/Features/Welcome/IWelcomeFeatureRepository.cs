using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IWelcomeFeatureRepository : IRepository
    {
        Task<string[]> GetFeatureBlackList(int id);

        Task<WelcomeServerSettings[]> GetWelcomeServerSettings();
    }

    public class WelcomeServerSettings
    {
        public int ServerId { get; set; }
        public bool WelcomeFeatureEnabled { get; set; }
        public string WelcomeFeatureTemplate { get; set; }
        public string WelcomeFeatureEmptyTemplate { get; set; }
        public string WelcomeGreater50MessageTemplate { get; set; }
    }

}
