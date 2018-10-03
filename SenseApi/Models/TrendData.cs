using System;
using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class TrendData
    {
        public int Steps { get; set; }
        [JsonProperty("start")]
        public DateTime StartDateTime { get; set; }
        [JsonProperty("end")]
        public DateTime EndDateTime { get; set; }
        public ConsumptionData Consumption { get; set; }
        public ProductionData Production { get; set; }
        public string Scale { get; set; }
    }
}
