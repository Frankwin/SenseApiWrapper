using System.Collections.Generic;
using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class ConsumptionData
    {
        public decimal Total { get; set; }
        public List<decimal> Totals { get; set; }
        public List<Device> Devices { get; set; }
        [JsonProperty("total_cost")]
        public decimal TotalCost { get; set; }
        [JsonProperty("total_costs")]
        public List<decimal> TotalCosts { get; set; }
    }
}
