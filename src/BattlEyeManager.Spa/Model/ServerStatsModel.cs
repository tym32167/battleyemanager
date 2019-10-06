using System.Collections.Generic;

namespace BattlEyeManager.Spa.Model
{
    public class ServerStatsModel
    {
        public string[] Labels { get; set; }
        public List<DataSet> DataSets { get; set; } = new List<DataSet>();
    }

    public class DataSet
    {
        public string Label { get; set; }
        public List<int> Data { get; set; } = new List<int>();
    }
}