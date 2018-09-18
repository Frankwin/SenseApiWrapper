using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class AuthorizationResponse
    {
        public bool Authorized { get; set; }
        [JsonProperty("account_id")]
        public int AccountId { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("monitors")]
        public List<Monitor> Monitors { get; set; }
        [JsonProperty("bridge_server")]
        public string BridgeServer { get; set; }
        [JsonProperty("partner_id")]
        public int? PartnerId { get; set; }
        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }
    }
}
