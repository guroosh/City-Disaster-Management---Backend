using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSCD.Model.Custom.MinimalDetails
{
   public class DisasterReport_minimal
    {
        public string ReferenceCode { get; set; }
        public string DisasterType { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; } // circumference of the disaster
        public Name ReportedBy { get; set; } // who reported it
        public string ReporterId { get; set; }
        public string ReportedTime { get; set; }
        public bool IsVerfied { get; set; }
        public bool IsClosed { get; set; }
    }
}
