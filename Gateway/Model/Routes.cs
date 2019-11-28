using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Models
{

    public class Routes
    {
        public  List<Route> ServiceRoutes { get; set; }
        public  List<Route> ViewRoutes { get; set; }
        public  Route AuthenticationRoute { get; set; }
    }

    public class Route
    {
        public Route(string url, string key)
        {
            Url = url;
            Key = key;
        }

        public Route()
        {

        }

        public string Endpoint { get; set; }
        public string Url { get; set; }
        public string Key { get; set; }
    }
}
