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
using Microsoft.Extensions.Configuration;

namespace Gateway.BusinessLogic
{
    public class Login_BL : IBusinessLogic
    {
        private readonly IUserCredentialCollection _userCredentialCollection;
        private readonly Auth_Config _authConfig;

        public Login_BL(IUserCredentialCollection credentialCollection, IOptions<Auth_Config> authConfig)
        {
            _userCredentialCollection = credentialCollection;
            _authConfig = authConfig.Value;
        }
        public async Task<LoginResponse> CheckCredentialsAsync (LoginRequest request)
        {
            try
            {
                var checklogin = await _userCredentialCollection.GetAsync(request.LoginId);
                if(checklogin.Password == "")
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.SecurityKey));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                    var jwtHeader = new JwtHeader(credentials);

                    var payloadClaims = new[]
                    {
                    new Claim("Issuer",_authConfig.Issuer),
                    new Claim("IssuedTo",_authConfig.IssuedTo),
                    new Claim("UserCode", checklogin.ReferenceCode),
                    new Claim("PayloadKey",_authConfig.PayLoadKey),
                    new Claim("IssuedAt",DateTime.Now.ToString()),
                    new Claim("Channel",request.Channel)
                };

                    var payload = new JwtPayload(payloadClaims);

                    var token = new JwtSecurityToken(jwtHeader, payload);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    string tokenString = tokenHandler.WriteToken(token);

                    var access = new LoginResponse
                    {
                        AccessToken = tokenString,
                        ReferenceCode = checklogin.ReferenceCode,
                       
                    };
                    return access;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch(NullReferenceException)
            {
                // user does not exist
                throw new Exception();
            }
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
            //convert to our need
            //return obj of userCredentiAL
            throw new NotImplementedException();
        }

        public Task<object> GetAllDocumentsAsync(object request = null)
        {
            throw new NotImplementedException();
        }
    }   
}
