using RSCD.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Registration.Model.DB;

namespace Registration.DataAccess.Repository
{
    public interface IUsersCollection : IDataRepository<Users>
    {
    }
}
