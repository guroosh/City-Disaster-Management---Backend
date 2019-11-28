using Gateway.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Handlers.Managers
{
    public class RequestRedirectManager
    {
        private readonly string tokenKey = "RSCD-Module-Token";
        private readonly HttpClient _microService = new HttpClient();

        public async Task<HttpResponseMessage> SendRequest(HttpRequest request, Route endpoint)
        {
            Console.WriteLine("->Reached Request redirect manager");

            Stream requestStream = request.Body;
            StreamReader streamReader = new StreamReader(requestStream, Encoding.UTF8);
            string requestContent = streamReader.ReadToEnd();

            HttpRequestMessage redirectRequest = new HttpRequestMessage()
            {
                Method = new HttpMethod(request.Method),
                Version = new Version("2.0"),
                RequestUri = new Uri(endpoint.Url),
                Content = new StringContent(requestContent,Encoding.UTF8, request.ContentType)    
            };

            redirectRequest.Headers.Add(tokenKey, endpoint.Key);
            return await _microService.SendAsync(redirectRequest);
        }
    }
}
