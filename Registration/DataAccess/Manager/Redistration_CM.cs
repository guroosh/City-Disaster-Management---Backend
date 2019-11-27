using Registration.DataAccess.Repository;
using Registration.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.DataAccess.Manager
{
    public class Redistration_CM : IUsersCollection
    {
        public Task<bool> AddAsync(Users document)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id, string userCode, string reason = "")
        {
            throw new NotImplementedException();
        }

        public Task<List<Users>> GetAllAsync(string getDocs = "active")
        {
            throw new NotImplementedException();
        }

        public Task<Users> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Users document)
        {
            throw new NotImplementedException();
        }
    }
}
