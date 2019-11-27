using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disaster.BusinessLogic;
using Disaster.DataAccess.Manager;
using Disaster.Model.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSCD.Models.API;
using RSCD.MQTT;

namespace Disaster.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DisasterReportController : ControllerBase
    {
        private readonly DisasterReport_BL _businessLogic;
        public DisasterReportController(DisasterReport_BL businessLogic)
        {
            _businessLogic = businessLogic;
        }

        [Route("test")]
        [HttpGet]
        public ActionResult Test()
        {
            return Ok(new ActionResponse(StatusCodes.Status200OK));
        }

        [Route("reportDisaster")]
        [HttpPost]
        public async Task<ActionResult> ReportDisaster(ReportDisasterRequest request)
        {
            ActionResponse response;

            try
            {
                bool result = await _businessLogic.CreateAsync(request);
                response = (result) ? new ActionResponse(StatusCodes.Status200OK) : new ActionResponse(StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {
                response = new ActionResponse(StatusCodes.Status500InternalServerError);
                response.StatusDescription += ex.Message.ToString();
            }
            
            return StatusCode(response.StatusCode, response);
        }


    }
}