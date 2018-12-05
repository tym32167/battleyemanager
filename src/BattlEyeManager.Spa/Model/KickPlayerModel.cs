namespace BattlEyeManager.Spa.Model
{
    public class KickPlayerModel
    {
        public int ServerId { get; set; }
        public string Reason { get; set; }
        public OnlinePlayerModel Player { get; set; }
    }

    public class BanPlayerModel
    {
        public int ServerId { get; set; }
        public string Reason { get; set; }
        public int Minutes { get; set; }
        public OnlinePlayerModel Player { get; set; }
    }
}