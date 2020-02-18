using System;
using Gateway.Model.DB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RSCD.DAL;
using RSCD.Model.Configration;

namespace Gateway.DataAccess
{
    public class DB_Context : MongoContext
    {


        public DB_Context(IOptions<DB_Settings> options) : base(options.Value.DE_ConnectionString, options.Value.DE_DataBase)
        {
            Console.WriteLine($"->ConsolePrint:in DBConext \nCS: {options.Value.DE_ConnectionString}\nDB: {options.Value.DE_DataBase}");
        }
    }
}
