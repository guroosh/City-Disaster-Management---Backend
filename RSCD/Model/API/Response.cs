using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RSCD.Models.API
{
    public class ActionResponse
    {

        public ActionResponse (int statusCode)
        {
            StatusCode = statusCode;

            switch (statusCode)
            {
                case 200:
                    StatusDescription = "Success";
                    break;
                case 400:
                    StatusDescription = "Bad Request";
                    break;
                case 401:
                    StatusDescription = "Unauthorized";
                    break;
                case 404:
                    StatusDescription = "Not Found";
                    break;
                case 422:
                    StatusDescription = "Failed to process the request";
                    break;
                case 417:
                    StatusDescription = "Failed to process due to the exception : \n";
                    break;
                case 500:
                    StatusDescription = "Failed to process due to the exception : \n";
                    break;
                default:
                    break;
            }
        }


        public ActionResponse(int statusCode, string statusDescription)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }

        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }

    }

}
