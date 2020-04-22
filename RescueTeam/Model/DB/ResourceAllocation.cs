using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam.Model.DB
{
    
        public class ResourceAllocation : RSCD.DAL.RSCDDataModel
        {
            public string DisasterId { get; set; }
            public List<string> AllocatedOfficeId { get; set; }
            public string ListOfDepartment { get; set; }

        }
    }

