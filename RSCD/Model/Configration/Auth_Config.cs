using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSCD.Model.Configration 
{
    public class Auth_Config
    {
        public string Issuer { get; set; } 
        public string IssuedTo { get; set; }
        public string L1Key { get; set; }
        public string L1Token { get; set; }
        public string L2Token { get; set; }
        public string L3Token { get; set; }
        public string SecurityKey { get; set; }
        public string PayLoadKey { get; set; }
    }
}
