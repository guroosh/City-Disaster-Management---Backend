using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSCD.Model.Message
{
    public class NewUser
    {
        public string ReferenceCode { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool IsCommonUser { get; set; }
    }
}
