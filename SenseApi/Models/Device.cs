using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class Device
    {
        public string Id { get; set; }
        public int MonitorId { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public string Location { get; set; }
        public string Model { get; set; }
        public string Icon { get; set; }
        public Tag Tags { get; set; }
        [JsonProperty("last_state")]
        public string LastState { get; set; }
        [JsonProperty("last_merge_state")]
        public MergeState LastMergeState { get; set; }
        [JsonProperty("last_state_time")]
        public DateTime LastStateTime { get; set; }
        [JsonProperty("given_location")]
        public string GivenLocation { get; set; }
        [JsonProperty("given_make")]
        public string GivenMake { get; set; }
        [JsonProperty("given_model")]
        public string GivenModel { get; set; }

        public List<decimal> History { get; set; }
        [JsonProperty("avgw")]
        public decimal AverageWattage { get; set; }
        [JsonProperty("total_cost")]
        public decimal TotalCost { get; set; }
        [JsonProperty("pct")]
        public decimal Percentage { get; set; }
        [JsonProperty("cost_history")]
        public List<decimal> CostHistory { get; set; }
        [JsonProperty("num_times_on")]
        public int? NumberOfTimesOn { get; set; }
        [JsonProperty("total_time_on")]
        public decimal? TotalTimeOn { get; set; }
    }
}
