using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class Usage
    {
        [JsonProperty("avg_monthly_runs")]
        public decimal AverageMonthlyRuns { get; set; }
        [JsonProperty("yearly_text")]
        public string YearlyText { get; set; }
        [JsonProperty("avg_monthly_pct")]
        public decimal AverageMonthlyPercentage { get; set; }
        [JsonProperty("avg_duration")]
        public decimal AverageDuration { get; set; }
        [JsonProperty("current_month_runs")]
        public int CurrentMonthRuns { get; set; }
        [JsonProperty("yearly_KWH")]
        public decimal YearlyKwh { get; set; }
        [JsonProperty("yearly_cost")]
        public decimal YearlyCost { get; set; }
        [JsonProperty("current_month_cost")]
        public decimal CurrentMonthCost { get; set; }
        [JsonProperty("avg_monthly_cost")]
        public decimal AvergageMontlyCost { get; set; }
        [JsonProperty("avg_watts")]
        public decimal AverageWatts { get; set; }
        [JsonProperty("avg_monthly_KWH")]
        public decimal AverageMonthlyKwh { get; set; }
        [JsonProperty("current_month_KWH")]
        public decimal CurrentMonthKwh { get; set; }
    }
}
