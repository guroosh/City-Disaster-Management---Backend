using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Model.API
{
    public class LoginRequest
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string Channel { get; set; }
    }
}
