using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class HistoryRecord
    {
        [JsonProperty("totals")]
        public List<List<int>> Totals { get; set; }
        [JsonProperty("start")]
        public DateTime StartDateTime { get; set; }
        [JsonProperty("endOfData")]
        public DateTime EndDateTime { get; set; }
    }
}
