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
using RSCD.BLL;
using System.Diagnostics;
using RSCD.Model.Message;

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
                var checklogin = await _userCredentialCollection.CheckUserExistence(request.LoginId);
                
                if(checklogin.Password == checklogin.Password)
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
                        IsCommonUser = checklogin.IsCommonUser
                    };

                    return access;
                }
                else
                {
                    throw new Exception("Failed to validate");
                }

            }
            catch(NullReferenceException)
            {
                // user does not exist
                throw new Exception("Null exception");
            }
        }

        public async Task<bool> CreateAsync(object request)
        {
            UserDetailMessage request_ = (UserDetailMessage)request;
            var copier = new ClassValueCopier();
            UserCredentials userCredentials = copier.ConvertAndCopy<UserCredentials, UserDetailMessage>(request_);
            bool result = await _userCredentialCollection.AddAsync(userCredentials);
            return result;
        }

        public async Task<bool> UpdateDocumentAsync(object request)
        {
            // recive the request
            UserDetailMessage request_ = (UserDetailMessage)request;
            var copier = new ClassValueCopier();
            UserCredentials oldUserCredentials = await _userCredentialCollection.GetAsync(request_.ReferenceCode);
            //update the credentials
            UserCredentials updatedCredentials = copier.ConvertAndCopy(request_, oldUserCredentials);
            updatedCredentials.LastUpdatedBy = request_.ReferenceCode;
            updatedCredentials.LastUpdatedAt = DateTime.Now.ToString();
            //push to DB
            bool result = await _userCredentialCollection.UpdateAsync(updatedCredentials);
            return result;
        }

        //functions below are not used --- added due to inheritance
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
