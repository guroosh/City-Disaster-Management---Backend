using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RSCD.Model.Configration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace RSCD.DAL
{
    public class MongoContext
    {
        protected readonly IMongoDatabase _database;

        public MongoContext(string ConnectionString, string DataBase)
        {
            try
            {
                var client = new MongoClient(ConnectionString);

                if (client != null)
                {
                    string dataBase = DataBase;
                    _database = client.GetDatabase(dataBase);
                }
            }
            catch
            {
            }   
        }
    }
}
