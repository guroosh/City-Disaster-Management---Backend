using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster.Model.API
{
    public class ReportDisasterRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ReportedTime { get; set; }
        public string ReportedBy { get; set; }
    }

    public class VerifyDisasterRequest
    {
        public string ReferenceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ReportedTime { get; set; }
        public string ReportedBy { get; set; }
    }

    public class VerifiedDisasterRequest
    {
        public string ReferenceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string VerifiedTime { get; set; }
        public string VerifiedBy { get; set; }
        public bool IsInfoTrue { get; set; }
        public double Radius { get; set; }
        public string Scale { get; set; }

    }
}
