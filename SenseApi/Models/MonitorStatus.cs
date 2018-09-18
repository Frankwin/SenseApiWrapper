using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class MonitorStatus
    {
        public Signals Signals { get; set; }
        [JsonProperty("device_detection")]
        public DeviceDetection DeviceDetection { get; set; }
        [JsonProperty("monitor_info")]
        public MonitorInfo MonitorInfo { get; set; }
    }
}
