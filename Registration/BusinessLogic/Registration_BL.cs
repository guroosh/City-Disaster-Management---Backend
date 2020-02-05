using Registration.Model.API;
using RSCD.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSCD.Helper;
using Registration.Model.DB;
using Registration.DataAccess.Repository;

namespace Registration.BusinessLogic
{
    public class Registration_BL : IBusinessLogic
    {
        private readonly IUsersCollection _usersCollection;
        public Registration_BL(IUsersCollection usersCollection)
        {
            _usersCollection = usersCollection;
        }

        public Task<bool> CreateAsync(object request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterCommonUser(RegisterCommonUserRequest request)
        {
            var copier = new ClassValueCopier();
            Users newUser = copier.ConvertAndCopy<Users, RegisterCommonUserRequest>(request);
            newUser.Role = "CommonUser";
            newUser.IsCommonUser = true;
            return await _usersCollection.AddAsync(newUser);
        }

        public async Task<bool> RegisterAdminUser(RegisterAdminUserRequest request)
        {
            var copier = new ClassValueCopier();
            Users newUser = copier.ConvertAndCopy<Users, RegisterAdminUserRequest>(request);
            newUser.IsCommonUser = false;
            return await _usersCollection.AddAsync(newUser);
        }

        public Task<bool> DeleteDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAllDocumentsAsync(object request = null)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }
    }
}
