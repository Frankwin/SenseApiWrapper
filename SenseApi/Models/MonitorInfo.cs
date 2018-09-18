using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class MonitorInfo
    {
        public string Serial { get; set; }
        [JsonProperty("ndt_enabled")]
        public bool NdtEnabled { get; set; }
        public bool Online { get; set; }
        public string Version { get; set; }
        public string Ssid { get; set; }
        public string Signal { get; set; }
        public string Mac { get; set; }
    }
}
