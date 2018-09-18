using System;
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
    }
}
