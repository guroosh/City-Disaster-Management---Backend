using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSCD.Model.Custom.ExternalModel.Registration
{
    public class CommonUser_EM
    {
        public Name Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string GovermentIdType { get; set; }
        public string GovermentIdNumber { get; set; }
        public bool IsVolunteering { get; set; }
        public string VolunteeringField { get; set; }
    }
    public class AdminUser_EM
    {
        public Name Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public string BadgeId { get; set; }
        public string Role { get; set; }
    }
}
