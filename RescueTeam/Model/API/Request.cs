using RSCD.Model.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam.Model.API
{

    public class ResourceAllocationRequest
    {
        public string DisasterId { get; set; }
        public string ReferenceCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string VerifiedTime { get; set; }
        public string VerifiedBy { get; set; }
        public bool IsInfoTrue { get; set; }
        public double Radius { get; set; }
        public string Landmark { get; set; }
        public string ScaleOfDisaster { get; set; } // high, medium, low
        public bool MedicalAssistanceRequired { get; set; }
        public bool TrafficPoliceAssistanceRequired { get; set; }
        public bool FireBrigadeAssistanceRequired { get; set; }
        public string OtherResponseTeamRequired { get; set; }
    }
    public class AdminUserRequest
    {
        public Name Name { get; set; }
        public string EmailId { get; set; }
        public string Department { get; set; }
        public string BadgeId { get; set; }
        public string Role { get; set; }

    }
    public class UpdateAdminUserRequest
    {
        public Name Name { get; set; }
        public string EmailId { get; set; }
        public string Department { get; set; }
        public string BadgeId { get; set; }
        public string Role { get; set; }
        public string UserCode { get; set; }
        public string CurrentUserCode { get; set; }
    }
    public class AdditionalResourcesRequest
    {
        public string ReferenceCode { get; set; }
        public string Department { get; set; }
        public int AdditionalUnits { get; set; }
    }

    public class ResourceDeallocationRequest
    {
        public string DisasterReferenceCode { get; set; }
        public string OfficerReferenceCode { get; set; }
    }
}