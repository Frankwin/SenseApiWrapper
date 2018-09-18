using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class MonitorAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public decimal Cost { get; set; }
        [JsonProperty("user_set_cost")]
        public bool UserSetCost { get; set; }
        [JsonProperty("cycle_start")]
        public int CycleStart { get; set; }
    }
}
