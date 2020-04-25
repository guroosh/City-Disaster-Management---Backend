using RSCD.Model.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSCD.Model.Message
{
    public class UserDetailMessage
    {
        public string ReferenceCode { get; set; }
        public Name Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool IsCommonUser { get; set; }
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
