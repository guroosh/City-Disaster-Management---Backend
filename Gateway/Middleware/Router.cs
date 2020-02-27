using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Gateway.Handlers.Managers;
using Newtonsoft.Json;
using System.Text;
using Gateway.Models;
using System;
using RSCD.Models.API;

namespace Gateway.Middleware
{
    public class Router
    {
        private readonly RequestDelegate _next;
        private RoutingManager _routingManager { get; set; }

        public Router (RequestDelegate next, RoutingManager  routingManager)
        {
            _next = next;
            _routingManager = routingManager;
        }

        public async Task Invoke (HttpContext context, RequestRedirectManager requestRedirectManager)
        {
            if(context.Request.Path.Value.Contains("login"))
            {
                await _next(context);
            } 
            else
            {
                Route endpoint = _routingManager.EndpointUrlBuilder(context.Request.Path.Value);

                if (endpoint.Url == "endpoint not found")
                {
                    var response = new ActionResponse(StatusCodes.Status404NotFound);
                    context.Response.StatusCode = response.StatusCode;
                    context.Response.ContentType = "application/json";
                    string serializedResponse = JsonConvert.SerializeObject(response);
                    await context.Response.WriteAsync(serializedResponse, Encoding.UTF8);
                    return;
                }
                else
                {
                    try
                    {
                        var response = await requestRedirectManager.SendRequest(context.Request, endpoint);
                        context.Response.StatusCode = (int)response.StatusCode;
                        await context.Response.WriteAsync(await response.Content.ReadAsStringAsync());
                        return;
                    }
                    catch(Exception ex)
                    {
                        var response = new ActionResponse(StatusCodes.Status500InternalServerError);
                        response.StatusDescription += ex.Message;
                        context.Response.StatusCode = response.StatusCode;
                        context.Response.ContentType = "application/json";
                        string serializedResponse = JsonConvert.SerializeObject(response);
                        await context.Response.WriteAsync(serializedResponse, Encoding.UTF8);
                        return;
                    }
                }
            }
        }
    }
}
