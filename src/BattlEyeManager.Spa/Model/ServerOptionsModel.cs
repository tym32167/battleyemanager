namespace BattlEyeManager.Spa.Model
{
    public class ServerOptionsModel
    {
        public int Id { get; set; }       
        public bool ThresholdFeatureEnabled { get; set; }
        public int ThresholdMinHoursCap { get; set; }
        public string ThresholdFeatureMessageTemplate { get; set; }
    }
}
