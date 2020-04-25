using RSCD.Model.Custom;
using System.Collections.Generic;

namespace RescueTeam.Model.DB
{
   
    public class VerifiedDisasterReport : RSCD.DAL.RSCDDataEntryModel
    {
        public string DisasterId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string VerifiedTime { get; set; }
        public string VerifiedBy { get; set; }
        public bool IsInfoTrue { get; set; }
        public double Radius { get; set; }
        public string Landmark { get; set; }
        public string ScaleOfDisaster { get; set; } 
        public bool MedicalAssistanceRequired { get; set; }
        public bool TrafficPoliceAssistanceRequired { get; set; }
        public bool FireBrigadeAssistanceRequired { get; set; }
        public string OtherResponseTeamRequired { get; set; }
        public List<MapRoute> FireRoute { get; set; }
        public List<string> AssignedOfficers { get; set; }
    }


}
