using RSCD.Model.Custom;
using RSCD.Model.Custom.ExternalModel.Registration;
using RSCD.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Model.API
{
   public class FetchCommonUserResponse
    {
        public ActionResponse ActionResponse { get; set; }
        public CommonUser_EM UserDetail { get; set; }
    }
    public class FetchAdminUserResponse
    {
        public ActionResponse ActionResponse { get; set; }
        public AdminUser_EM UserDetail { get; set; }
    }
}
