using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSCD.DAL;
using RSCD.Model.Custom;

namespace Registration.Model.DB
{
    public class Users : RSCDDataEntryModel
    {
        public Name Name { get; set; }
        [MongoDB]
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool IsCommonUser {get; set;}
        public string PhoneNumber { get; set; }
        public string GovernmentIdType { get; set; }
        public string GovernmentIdNumber { get; set; }
        public bool IsVolunteering { get; set; }
        public string VolunteeringField { get; set; }
        public string Department { get; set; }
        public string BadgeId { get; set; }
        public string Role { get; set; }
    }
}
