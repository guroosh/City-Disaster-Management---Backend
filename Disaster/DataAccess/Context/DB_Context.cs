using Disaster.Model.DB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RSCD.DAL;
using RSCD.Model.Configration;

namespace Disaster.DataAccess.Context
{
    public class DB_Context : MongoContext
    {
        public IMongoCollection<ReportedDisaster> ReportedDisasterCollection
        {
            get
            {
                return _database.GetCollection<ReportedDisaster>("ReportedDisasterCollection");
            }
        }

        public IMongoCollection<UsersDetail> UsersDetailCollection
        {
            get
            {
                return _database.GetCollection<UsersDetail>("UsersDetailCollection");
            }
        }

        public DB_Context(IOptions<DB_Settings> options) : base(options.Value.DE_ConnectionString, options.Value.DE_DataBase)
        {
        }
    }
}
