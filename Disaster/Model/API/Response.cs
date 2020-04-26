using RSCD.Model.Custom.ExternalModel;
using RSCD.Model.Custom.MinimalDetails;
using RSCD.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster.Model.API
{
    public class GetDisasterListResponse
    {
        public ActionResponse ActionResponse { get; set; }
        public List<DisasterReport_minimal> DisasterReportList { get; set; }
    }

    public class GetDisasterResponse
    {
        public ActionResponse ActionResponse { get; set; }
        public Disaster_EM DisasterReport { get; set; }
    }
}
