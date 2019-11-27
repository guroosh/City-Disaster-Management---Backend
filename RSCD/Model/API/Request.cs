using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSCD.Models.API
{
    public class FetchModuleRequest
    {
        public bool AllModulesNeeded { get; set; }
        public string ModuleCode { get; set; }
    }

    public class GeneralFetchRequest
    {
       public string Code { get; set; }
    }

    public class GeneralDeleteRequest
    {
        public string Code { get; set; }
        public string DeleteReason { get; set; }
        public string UserEmployeeCode { get; set; }
    }

    public class GeneralListRequest
    {
        public string NeededList { get; set; }
    }

}
