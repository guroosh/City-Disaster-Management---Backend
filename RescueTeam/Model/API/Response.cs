using RSCD.Model.Custom.MinimalDetails;
using RSCD.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam.Model.API
{
    public class GetAllocatedOfficerResponse
    {
        public ActionResponse ActionResponse { get; set; }
        public List<AdminUserDetails_minimal> OfficersList { get; set; }
    }
}
