using RSCD.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Registration.Model.DB;
using Registration.Model.API;

namespace Registration.DataAccess.Repository
{
    public interface IUsersCollection : IDataRepository<Users>
    {
        string _collectionCodePrefix { get; }
        Task<Users> RegisterUserAsync(Users document);
    }
}
