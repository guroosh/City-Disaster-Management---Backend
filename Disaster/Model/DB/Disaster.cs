using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSCD.DAL;

namespace Disaster.Model.DB
{
    public class ReportedDisaster : RSCDDataEntryModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ReportedBy { get; set; } // who reported it
        public string ReportedTime { get; set; }
        public double Radius { get; set; } // circumference of the disaster
        public string VerifiedBy { get; set; } // 
        public string VerifiedTime { get; set; }
        public bool IsInfoTrue { get; set; } //
        public string ScaleOfDisaster { get; set; } // high, medium, low
        public bool MedicalAssistanceRequired { get; set; }
        public bool TrafficPoliceAssistanceRequired { get; set; }
        public bool FireBrigadeAssistanceRequired { get; set; }
        public string OtherResponseTeamRequired { get; set; }

    }
}
