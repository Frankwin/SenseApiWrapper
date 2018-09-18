using System;
using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class Monitor
    {
        public int Id { get; set; }
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }
        [JsonProperty("solar_connected")]
        public bool SolarConnected { get; set; }
        [JsonProperty("solar_configured")]
        public bool SolarConfigured { get; set; }
        public bool Online { get; set; }
        [JsonProperty("attributes")]
        public MonitorAttribute Attribute { get; set; }
        [JsonProperty("signal_check_completed_time")]
        public DateTime SignalCheckCompletedTime { get; set; }
    }
}
