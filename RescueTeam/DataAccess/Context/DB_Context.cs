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
        public IMongoCollection<AdminDetails> RescueTeamCollection
        {
            get
            {

                return _database.GetCollection<AdminDetails>("rescueTeamCollection");
            }
        }

        public DB_Context(IOptions<DB_Settings> options) : base(options.Value.DE_ConnectionString, options.Value.DE_DataBase)
        {
        }
    }
}
