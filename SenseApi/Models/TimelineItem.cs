using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class TimelineItem
    {
        public DateTime Time { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Icon { get; set; }
        public string Body { get; set; }
        public string Destination { get; set; }
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }
        [JsonProperty("device_name")]
        public string DeviceName { get; set; }
        public bool ShowAction { get; set; }
        public bool AllowSticky { get; set; }
        public Guid Guid { get; set; }
        [JsonProperty("user_device_type")]
        public string UserDeviceType { get; set; }
        public List<TimelineItem> Children { get; set; }
    }
}
