using Disaster.DataAccess.Repository;
using Disaster.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disaster.DataAccess.Manager
{
    public class UsersDetail_CM : IUsersCollection
    {
        public Task<bool> AddAsync(UsersDetail document)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id, string userCode, string reason = "")
        {
            throw new NotImplementedException();
        }

        public Task<List<UsersDetail>> GetAllAsync(string getDocs = "active")
        {
            throw new NotImplementedException();
        }

        public Task<UsersDetail> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(UsersDetail document)
        {
            throw new NotImplementedException();
        }
    }
}
