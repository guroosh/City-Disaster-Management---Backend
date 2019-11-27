using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSCD.Model.Configration
{
    public class Mqtt_Settings
    {
        public string Host { get; set; }
        public string ClientId { get; set; }
        public string SuscribeTopic { get; set; }
    }
}
