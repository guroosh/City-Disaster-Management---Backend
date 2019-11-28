using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gateway.Models;
using Newtonsoft.Json;

namespace Gateway.Handlers.Managers
{
    public class RoutingManager 
    {
        private Routes _availableRoutes { get; set; }


        /// <summary>
        /// connstructor loads the routes from the Routes.json as objects of Routes class
        /// </summary>
        public RoutingManager()
        {
            string jsonString = File.ReadAllText("Routes.json");
            _availableRoutes = JsonConvert.DeserializeObject<Routes>(jsonString);
        }


        /// <summary>
        /// used to check whether the endpoint is available in the microservice and send its uri
        /// </summary>
        /// <param name="path"> request path from the user </param>
        /// <returns>
        /// url string -> when the endpoint is available
        /// empty string -> endpoint not found
        /// </returns>
        public Route EndpointUrlBuilder(string path)
        {
            Route endpointRoute = new Route();
            List<string> pathArray = path.Split("/").ToList();
            pathArray.RemoveAt(0);
            string endpoint = pathArray[1].ToLower();
            string basePath = pathArray[0].ToLower();
            string endpointAction = pathArray.Last().ToLower();

            switch (basePath)
            {
                case "views":
                    endpointRoute = _availableRoutes.ViewRoutes.FirstOrDefault(route => route.Endpoint == endpoint);
                    break;
                case "services":
                    endpointRoute = _availableRoutes.ServiceRoutes.FirstOrDefault(route => route.Endpoint == endpoint);
                    break;
                default:
                    endpointRoute.Url = "endpoint not found";
                    return endpointRoute;
            }
            
            string url = endpointRoute.Url;

            for(int i = 2; i < pathArray.Count; i++)
            {
                url += string.Format("/{0}", pathArray[i]);
            }

            return new Route(url,endpointRoute.Key);
        }
    }
}
