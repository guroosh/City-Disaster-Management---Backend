using RSCD.Model.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Model.API
{
    public class AddUserRequest
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public Name UserName { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string GovtIdNumber { get; set; }
        public string Token { get; set; }
        public string GovtIdType { get; set; }
        public bool IsVolunteer { get; set; }
        public string VolunteerType { get; set; }
    }

    public class UserDetailRquest
    {

        public string UserId { get; set; }

    }




}
