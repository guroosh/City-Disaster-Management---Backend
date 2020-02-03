using RSCD.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Model.API
{
    public class LoginResponse
    {
        public ActionResponse ActionResponse { get; set; }
        public string UserCode { get; set; }
        public string AccessToken { get; set; }
    }

}
