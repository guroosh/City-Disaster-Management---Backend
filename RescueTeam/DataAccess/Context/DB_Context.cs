using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RescueTeam.Model.DB;
using RSCD.DAL;
using RSCD.Model.Configration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueTeam.DataAccess.Context
{
    public class DB_Context : MongoContext
    {
        public IMongoCollection<OfficerDetails> OfficerDetailsCollection
        {
            get
            {

                return _database.GetCollection<OfficerDetails>("OfficerDetailsCollection");
            }
        }

        public IMongoCollection<VerifiedDisasterReport> VerifiedDisasterReportCollection
        {
            get
            {

                return _database.GetCollection<VerifiedDisasterReport>(" VerifiedDisasterReportCollection");
            }
        }

        public DB_Context(IOptions<DB_Settings> options) : base(options.Value.DE_ConnectionString, options.Value.DE_DataBase)
        {
        }
    }
}
