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
       
        public Task<bool> CreateAsync(object request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDocumentAsync(object request)
        {
            throw new NotImplementedException();
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
