using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SenseApi.Models
{
    public class Tag
    {
        public bool Alertable { get; set; }
        public string AlwaysOn { get; set; }
        public List<string> ControlCapabilities { get; set; }
        public DateTime DateCreated { get; set; }
        public string DefaultLocation { get; set; }
        public string DefaultMake { get; set; }
        public string DefaultUserDeviceType { get; set; }
        public bool DeployToMonitor { get; set; }
        public bool DeviceListAllowed { get; set; }
        public bool Mature { get; set; }
        public string MergedDevices { get; set; }
        public string ModelCreatedVersion { get; set; }
        [JsonProperty("name_useredit")]
        public bool NameUserEdit { get; set; }

        public string OriginalName { get; set; }
        public List<PeerName> PeerNames { get; set; }
        public bool Pending { get; set; }
        public bool Revoked { get; set; }
        public bool TimelineAllowed { get; set; }
        public bool TimelineDefault { get; set; }
        public string Type { get; set; }
        [JsonProperty("user_editable")]
        public bool UserEditable { get; set; }

        public bool UserDeletable { get; set; }
        public string UserDeviceType { get; set; }
        public string UserDeviceTypeDisplayString { get; set; }
        [JsonProperty("UserEditable")]
        public bool UserEditable2 { get; set; }
        public bool UserEditableMeta { get; set; }
        public bool UserMergeable { get; set; }
        public bool Virtual { get; set; }
    }
}
