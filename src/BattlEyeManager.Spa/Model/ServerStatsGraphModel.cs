using System;
using System.Collections.Generic;

namespace BattlEyeManager.Spa.Model
{
    public class ServerStatsGraphModel
    {
        public DateTime[] Dates { get; set; }
        public List<DataSet> DataSets { get; set; } = new List<DataSet>();
    }

    public class DataSet
    {
        public string Label { get; set; }
        public List<int> Data { get; set; } = new List<int>();
    }
}