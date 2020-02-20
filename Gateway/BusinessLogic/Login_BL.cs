using Gateway.DataAccess;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using RSCD.Model.Configration;
using Gateway.Model.DB;
using Gateway.Model.API;
using RSCD.Helper;
using Gateway.DataAccess.Repository;
using RSCD.Model.Message;
using RSCD.BLL;

namespace Gateway.BusinessLogic
{
    public class Login_BL : IBusinessLogic
    {
        private readonly IUserCredentialCollection _userCredentialCollection;
        public Login_BL(IUserCredentialCollection credentialCollection) 
        {
            _userCredentialCollection = credentialCollection;
        }
        public Task<LoginResponse> CheckCredentialsAsync (LoginRequest request)
        {
            throw new NotImplementedException();
        }
       
        public async Task<bool> CreateAsync(object request)
        {
            NewUser request_ = (NewUser)request;
            var copier = new ClassValueCopier();
            UserCredentials userCredentials = copier.ConvertAndCopy<UserCredentials, NewUser>(request_);
            bool result = await _userCredentialCollection.AddAsync(userCredentials);
            return result;
        }

        public async Task<bool> UpdateDocumentAsync(object request)
        {
            NewUser request_ = (NewUser)request;
            var copier = new ClassValueCopier();
            UserCredentials userCredentials = copier.ConvertAndCopy<UserCredentials, NewUser>(request_);
            userCredentials.LastUpdatedBy = request_.ReferenceCode;
            userCredentials.LastUpdatedAt = DateTime.Now.ToString();
            bool result = await _userCredentialCollection.UpdateAsync(userCredentials);
            return result;
        }

        public Task<bool> DeleteDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetDocumentAsync(object request)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAllDocumentsAsync(object request = null)
        {
            throw new NotImplementedException();
        }
    }   
}
