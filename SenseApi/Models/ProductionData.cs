using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class ProductionData
    {
        public decimal Total { get; set; }
        [JsonProperty("total_cost")]
        public decimal TotalCost { get; set; }
    }
}
