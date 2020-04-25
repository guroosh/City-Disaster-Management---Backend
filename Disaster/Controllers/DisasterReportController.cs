using System;
using System.Threading.Tasks;
using Disaster.BusinessLogic;
using Disaster.Model.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSCD.Models.API;

namespace Disaster.Controllers
{
    [Route("[controller]")] //CONTROLLER
    [ApiController]
    public class DisasterReportController : ControllerBase
    {
        private readonly DisasterReport_BL _businessLogic;
        public DisasterReportController(DisasterReport_BL businessLogic)
        {
            _businessLogic = businessLogic;
        }

        [Route("test")]
        [HttpGet] //ACTION
        public ActionResult Test()
        {
            return Ok(new ActionResponse(StatusCodes.Status200OK));
        }

        [Route("reportDisaster")]
        [HttpPost]
        public async Task<IActionResult> ReportDisaster(ReportDisasterRequest request)
        {
            ActionResponse response;
            
            try
            {
                bool result = await _businessLogic.CreateAsync(request);
                response = new ActionResponse((result) ? StatusCodes.Status200OK : StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {
                response = new ActionResponse(StatusCodes.Status500InternalServerError);
                response.StatusDescription += ex.Message.ToString();
            }
            return StatusCode(response.StatusCode, response);
        }

        [Route("verifiedDisaster")]
        [HttpPost]
        public async Task<IActionResult> VerifiedDisaster(VerifiedDisasterRequest request)
        {
            ActionResponse response;

            try
            {
                bool result = await _businessLogic.UpdateDocumentAsync(request);
                response = new ActionResponse((result) ? StatusCodes.Status200OK : StatusCodes.Status422UnprocessableEntity);
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