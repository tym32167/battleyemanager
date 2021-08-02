namespace BattlEyeManager.Core.DataContracts.Models
{
    public class WelcomeFeatureBlackList
    {
        public int Id { get; set; }
        public int ServerId { get; set; }
        public string PlayerId { get; set; }
        public string PlayerGuid { get; set; }
    }
}