﻿using RSCD.Model.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Model.API
{
    public class UserDetailRquest
    {
        public string UserId { get; set; }

    }
    
    public class RegisterCommonUserRequest
    {
        public Name Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string GovernmentIdType { get; set; }
        public string GovernmentIdNumber { get; set; }
        public bool IsVolunteering { get; set; }
        public string VolunteeringField { get; set; }
    }

    public class RegisterAdminUserRequest
    {
        public Name Name { get; set; } 
        public string EmailId { get; set; }
        public string Department { get; set; }
        public string BadgeId { get; set; }
        public string Role { get; set; }
       
    }

    public class UpdateCommonUserRequest
    {
        public Name Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string GovernmentIdType { get; set; }
        public string GovernmentIdNumber { get; set; }
        public string UserCode{get; set; }
        public string CurrentUserCode {get; set;}
    }

    public class UpdateAdminUserRequest
    {
        public Name Name { get; set; }
        public string EmailId { get; set; }
        public string Department { get; set; }
        public string BadgeId { get; set; }
        public string Role { get; set; }
        public string UserCode{ get; set; }
        public string CurrentUserCode {get; set;}
    }

    public class UpdateVolunteeringPreferenceRequest
    {
        public bool IsVolunteering { get; set; }
        public string VolunteeringField { get; set; }
        public string CurrentUserCode { get; set; }
        public string UserCode { get; set; }

    }
}
