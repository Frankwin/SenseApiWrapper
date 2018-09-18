using System.Collections.Generic;
using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class DeviceDetection
    {
        [JsonProperty("in_progress")]
        public List<DeviceDetectionStatus> InProgress { get; set; }
        [JsonProperty("found")]
        public List<DeviceDetectionStatus> Found { get; set; }
        [JsonProperty("num_detected")]
        public int NumDetected { get; set; }
    }
}
