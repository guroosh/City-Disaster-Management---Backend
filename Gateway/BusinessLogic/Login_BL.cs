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

namespace Gateway.BusinessLogic
{
    public class Login_BL
    {
        private readonly DB_Context _context;
        private readonly Auth_Config _authConfig;

        public Login_BL(DB_Context context, IOptions<Auth_Config> authConfig)
        {
            _context = context;
            _authConfig = authConfig.Value;
        }

        private async Task<Users> GetUserAsync(string empCode)
        {
            try
            {
                return await _context.UserCollection.Find(doc => doc.UserCode == empCode).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public async Task<LoginResponse> GetUserAccessAsync(LoginRequest request)
        {
            var user = await GetUserAsync(request.LoginId);
            var copier = new ClassValueCopier();

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.SecurityKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var jwtHeader = new JwtHeader(credentials);

                var payloadClaims = new[]
                {
                    new Claim("Issuer",_authConfig.Issuer),
                    new Claim("IssuedTo",_authConfig.IssuedTo),
                    new Claim("UserCode", user.UserCode),
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
                    UserCode = user.UserCode
                };
                return access;
            }
            catch(NullReferenceException)
            {
                throw new Exception("User not Found");
            }
        }
    }
}
