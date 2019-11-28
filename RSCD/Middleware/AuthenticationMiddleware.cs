using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Options;
using RSCD.Model.Configration;
using RSCD.Models.API;
using RSCD.Helper;
using Newtonsoft.Json;

namespace RSCD.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Token _token;

        public AuthenticationMiddleware (RequestDelegate next, IOptions<Token> token)
        {
            _next = next;
            _token = token.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if(CheckRscdMSHeader(context))
            {
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

        private bool CheckRscdMSHeader(HttpContext context)
        {
            StringValues data = new StringValues();
            bool headerFound = context.Request.Headers.TryGetValue(_token.Key, out data);
            return (headerFound) ? VerifyRscdMSHeader(data) : false;
        }

        private bool VerifyRscdMSHeader(string token)
        {
            try
            {
                Base64 base64 = new Base64();
                var plainText = base64.Decode(token);
                return plainText == _token.Value;
            }
            catch
            {
                return false;
            }
        }
    }
}
