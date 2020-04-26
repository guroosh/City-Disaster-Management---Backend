using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Disaster.BusinessLogic;
using Disaster.Model.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSCD.Model.Custom.ExternalModel;
using RSCD.Model.Custom.MinimalDetails;
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


        [Route("getDisasterList")]
        [HttpGet]
        public async Task<IActionResult> GetDisasterList()
        {
            GetDisasterListResponse response;

            try
            {
                List<DisasterReport_minimal> result = (List<DisasterReport_minimal>)(await _businessLogic.GetAllDocumentsAsync());
                response = new GetDisasterListResponse
                {
                    ActionResponse = new ActionResponse(StatusCodes.Status200OK),
                    DisasterReportList = result
                };
            }
            catch (Exception ex)
            {
                response = new GetDisasterListResponse
                {
                    ActionResponse = new ActionResponse(StatusCodes.Status500InternalServerError)
                };
                response.ActionResponse.StatusDescription += ex.Message.ToString();
            }

            return StatusCode(response.ActionResponse.StatusCode, response);
        }

        [Route("getDisaster")]
        [HttpPost]
        public async Task<IActionResult> GetDisaster(GeneralFetchRequest request)
        {
            GetDisasterResponse response;
            try
            {
                Disaster_EM result = (Disaster_EM)(await _businessLogic.GetDocumentAsync(request));
                response = new GetDisasterResponse
                {
                    ActionResponse = new ActionResponse(StatusCodes.Status200OK),
                    DisasterReport = result
                };
            }
            catch (Exception ex)
            {
                response = new GetDisasterResponse
                {
                    ActionResponse = new ActionResponse(StatusCodes.Status500InternalServerError)
                };
                response.ActionResponse.StatusDescription += ex.Message.ToString();
            }

            return StatusCode(response.ActionResponse.StatusCode, response);
        }

        [Route("closeDisaster")]
        [HttpPost]
        public async Task<IActionResult> CloseDisaster(GeneralFetchRequest request)
        {
            ActionResponse response;

            try
            {
                bool result = await _businessLogic.CloseDisaster(request);
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