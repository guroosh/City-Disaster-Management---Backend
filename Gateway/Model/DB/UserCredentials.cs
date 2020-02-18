using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Model.DB
{
    public class UserCredentials:RSCD.DAL.RSCDDataEntryModel
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool IsCommonUser { get; set; }

    }
}
