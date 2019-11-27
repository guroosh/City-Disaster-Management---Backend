using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Registration.Model.DB;
using RSCD.DAL;
using RSCD.Model.Configration;

namespace Registration.DataEntry.DataAccess.Context
{
    public class DB_Context : MongoContext
    {
        public IMongoCollection<Users> UsersCollection
        {
            get
            {
                return _database.GetCollection<Users>("userCollection");
            }
        }


        public DB_Context(IOptions<DB_Settings> options) : base(options.Value.DE_ConnectionString, options.Value.DE_DataBase)
        {
        }
    }
}
