using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSCD.Model.Configration
{
    public class DB_Settings
    {
        public string DE_ConnectionString { get; set; }
        public string DE_DataBase { get; set; }
    }
}
