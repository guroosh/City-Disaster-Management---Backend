using System;
using System.Threading.Tasks;
using Disaster.BusinessLogic;
using Disaster.Model.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSCD.Models.API;

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
        public async Task<IActionResult> ReportDisaster(ReportDisasterRequest request)
        {
            ActionResponse response;
            bool result = await _businessLogic.CreateAsync(request);
            response = new ActionResponse((result) ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError);
            return StatusCode(response.StatusCode, response);
        }

        [Route("verifiedDisaster")]
        [HttpPost]
        public async Task<IActionResult> VerifiedDisaster(VerifiedDisasterRequest request)
        {
            ActionResponse response;
            bool result = await _businessLogic.UpdateDocumentAsync(request);
            response = new ActionResponse((result) ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError);
            return StatusCode(response.StatusCode, response);
        }
    }
}