namespace SenseApi.Models
{
    public class DeviceDetails
    {
        public DeviceAlert Alerts { get; set; }
        public string Notes { get; set; }
        public Usage Usage { get; set; }
        public Timeline Timeline { get; set; }
        public Device Device { get; set; }
        public string Info { get; set; }
    }
}
