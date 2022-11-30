using System;

namespace MyAPI.Models
{
    public class Devices
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string CategoryId { get; set; }
        public string ZoneId { get; set; }
        public string Status { get; set; }
        public Boolean IsActive { get; set; }
    }
}
