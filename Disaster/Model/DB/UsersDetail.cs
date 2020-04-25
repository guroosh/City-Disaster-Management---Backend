using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSCD.DAL;
using RSCD.Model.Custom;

namespace Disaster.Model.DB
{
    public class UsersDetail : RSCDDataEntryModel
    {
        public Name Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool IsCommonUser { get; set; }
        public string Department { get; set; }
        public string BadgeId { get; set; }
    }
}
