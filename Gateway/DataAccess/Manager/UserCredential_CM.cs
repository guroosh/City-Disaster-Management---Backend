using Gateway.DataAccess.Repository;
using Gateway.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.DataAccess.Manager
{
    public class UserCredential_CM : IUserCredentialCollection
    {
        public Task<bool> AddAsync(UserCredentials document)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckLoginCredential()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckUserExistence()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id, string userCode, string reason = "")
        {
            throw new NotImplementedException();
        }

        public Task<List<UserCredentials>> GetAllAsync(string getDocs = "active")
        {
            throw new NotImplementedException();
        }

        public Task<UserCredentials> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(UserCredentials document)
        {
            throw new NotImplementedException();
        }


    }
}
