using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using RSCD.Models.API;
using RSCD.Model.Configration;
using RSCD.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Collections.Generic;

namespace Gateway.Middleware
{
    public class Authentication
    {
        private readonly RequestDelegate _next;
        private readonly Auth_Config _authConfig;

        public Authentication (RequestDelegate next, IOptions<Auth_Config> authConfig)
        {
            _next = next;
            _authConfig = authConfig.Value;
        } 

        public async Task Invoke(HttpContext context)
        {
            if (CheckRscdToken(context))
            {
                if (context.Request.Path.Value.Contains("login"))
                {
                    // skip authentication when user login and redirects to login controller
                    await _next(context);
                }
                else
                {
                    if (CheckJWT(context))
                    {
                        // add service specific token and forward to router
                        await _next(context);
                    }
                    else
                    {
                        var response = new ActionResponse(StatusCodes.Status401Unauthorized);
                        context.Response.StatusCode = response.StatusCode;
                        context.Response.ContentType = "application/json";
                        string serializedResponse = JsonConvert.SerializeObject(response);
                        await context.Response.WriteAsync(serializedResponse, Encoding.UTF8);
                        return;
                    }
                }
            }
            else
            {
                var response = new ActionResponse(StatusCodes.Status401Unauthorized);
                context.Response.StatusCode = response.StatusCode;
                context.Response.ContentType = "application/json";
                string serializedResponse = JsonConvert.SerializeObject(response);
                await context.Response.WriteAsync(serializedResponse, Encoding.UTF8);
                return;
            }
        }

        private bool CheckRscdToken(HttpContext context)
        {
            StringValues data = new StringValues();
            bool headerFound = context.Request.Headers.TryGetValue(_authConfig.L1Token, out data);
            return (headerFound) ? VerifyRscdToken(data) : false;
        }

        private bool VerifyRscdToken(string Token)
        {
            try
            {
                Base64 base64 = new Base64();
                var plainText = base64.Encode(Token);
                return plainText == _authConfig.L1Key;
            }
            catch
            {
                return false;
            }
        }

        private bool CheckJWT(HttpContext context)
        {
            bool headerFound = context.Request.Headers.TryGetValue(_authConfig.L2Token, out StringValues data);
            
            if(data.Equals(""))
            {
                return VerifyChannel(context);
            }
            
            return (headerFound) ? VerifyJWT(data) : false;
        }

        private bool VerifyJWT(string Token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(Token);
            var claims = token.Claims;
            var clm_ = claims.FirstOrDefault(clm => clm.Type == "PayloadKey");
            return (clm_.Value == _authConfig.PayLoadKey);
        }

        private bool VerifyChannel(HttpContext context)
        {
            //exception rule 
            // -> if jwt is empty
            // -> check if channel is android
            // -> if url is to register common user return true

            bool channelMatches = context.Request.Headers.TryGetValue("Channel", out StringValues channelName);
            if (channelName.Equals("Android"))
            {

                return context.Request.Path.Value == "/services/rs/registration/registerCu";
                //List<string> pathArray = context.Request.Path.Value.Split("/").ToList();
                //pathArray.RemoveAt(0);
                //string endpoint = pathArray[1].ToLower();
                //string basePath = pathArray[0].ToLower();
                //return endpoint.ToLower().Equals("registerCu") && basePath.ToLower().Equals("Registration");
            }
            return false;
        }
    }
}
