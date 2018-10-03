using System.Collections.Generic;
using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class TimelineData
    {
        [JsonProperty("more_items")]
        public bool MoreItem { get; set; }
        [JsonProperty("sticky_items")]
        public List<TimelineItem> StickyItems { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        public List<TimelineItem> Items { get; set; }
    }
}
