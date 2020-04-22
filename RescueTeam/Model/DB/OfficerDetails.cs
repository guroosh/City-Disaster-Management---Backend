using RSCD.Model.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam.Model.DB
{
    
        public class OfficerDetails : RSCD.DAL.RSCDDataEntryModel
        {
            public Name Name { get; set; }
            public string EmailId { get; set; }
            public string PhoneNumber { get; set; }
            public string Department { get; set; }
            public string BadgeId { get; set; }
            public string Role { get; set; }
            public bool IsOfficerAssigned { get; set; }
        }

        
    }

