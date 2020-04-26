using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSCD.Model.Custom.MinimalDetails
{
    public class CommonUserDetails_minimal
    {
        public string ReferenceCode { get; set; }
        public Name Name { get; set; }
        public string EmailId { get; set; }
        public bool IsVolunteering { get; set; }
        public string VolunteeringField { get; set; }
    }

    public class AdminUserDetails_minimal
    {
        public string ReferenceCode { get; set; }
        public Name Name { get; set; }
        public string BadgeId { get; set; }
        public string EmailId { get; set; }
        public string Department { get; set; }
    }
}
