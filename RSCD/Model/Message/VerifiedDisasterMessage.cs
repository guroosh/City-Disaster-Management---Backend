using RSCD.Model.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSCD.Model.Message
{
    public class VerifiedDisasterMessage
    {
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
        public List<MapRoute> FireRoute { get; set; }
    }
}
