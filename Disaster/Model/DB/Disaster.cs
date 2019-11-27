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
        public string ReportedBy { get; set; }
        public double Radius { get; set; }
        public string VerifiedBy { get; set; }
        public string ReportedTime { get; set; }
        public string VerifiedTime { get; set; }
        public bool IsInfoTrue { get; set; }
    }
}
