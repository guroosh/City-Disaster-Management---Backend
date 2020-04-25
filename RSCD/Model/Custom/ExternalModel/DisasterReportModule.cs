using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSCD.Model.Custom.MinimalDetails;

namespace RSCD.Model.Custom.ExternalModel
{
    public class Disaster_EM
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public CommonUserDetails_minimal ReportedBy { get; set; } // who reported it
        public string ReportedTime { get; set; }
        public double Radius { get; set; } // circumference of the disaster
        public AdminUserDetails_minimal VerifiedBy { get; set; } // 
        public string VerifiedTime { get; set; }
        public bool IsInfoTrue { get; set; } //
        public string ScaleOfDisaster { get; set; } // high, medium, low
        public bool MedicalAssistanceRequired { get; set; }
        public bool TrafficPoliceAssistanceRequired { get; set; }
        public bool FireBrigadeAssistanceRequired { get; set; }
        // to be checked and removed if nessary
        public string OtherResponseTeamRequired { get; set; }
        public bool IsClosed { get; set; }
        public string CloseddBy { get; set; }
        public string ClosedTime { get; set; }

        //routes

    }
}
