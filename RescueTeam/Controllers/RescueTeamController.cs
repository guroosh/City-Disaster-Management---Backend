using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RescueTeam.BusinessLogic;
using RescueTeam.Model.API;
using RSCD.Model.Custom.MinimalDetails;
using RSCD.Models.API;

namespace RescueTeam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RescueTeamController : ControllerBase
    {
        private readonly RescueTeam_BL _businessLogic;

        public RescueTeamController(RescueTeam_BL businessLogic)
        {
            _businessLogic = businessLogic;
        }

        [Route("test")]
        [HttpGet]
        public ActionResult Test()
        {
            return Ok(new ActionResponse(StatusCodes.Status200OK));
        }

        [Route("additionalResourceAllocation")]
        [HttpPost]
        public async Task<IActionResult> AdditionalResourceAllocation(AdditionalResourcesRequest request)
        {
            ActionResponse response;

            try
            {
                bool result = await _businessLogic.AllocateAdditionalResourceAsync(request);
                response = (result) ? new ActionResponse(StatusCodes.Status200OK) : new ActionResponse(StatusCodes.Status422UnprocessableEntity);

            }
            catch (Exception ex)
            {
                response = new ActionResponse(StatusCodes.Status500InternalServerError);
                response.StatusDescription += ex.Message.ToString();
            }

            return StatusCode(response.StatusCode, response);
        }

        [Route("resourceDeallocation")]
        [HttpPost]
        public async Task<IActionResult> ResourceDeallocation(ResourceDeallocationRequest request)
        {
            ActionResponse response;

            try
            {
                bool result = await _businessLogic.ResourceDeallocationAsync(request);
                response = (result) ? new ActionResponse(StatusCodes.Status200OK) : new ActionResponse(StatusCodes.Status422UnprocessableEntity);

            }
            catch (Exception ex)
            {
                response = new ActionResponse(StatusCodes.Status500InternalServerError);
                response.StatusDescription += ex.Message.ToString();
            }

            return StatusCode(response.StatusCode, response);
        }

        [Route("getAllocatedOfficers")]
        [HttpPost]
        public async Task<IActionResult> GetAllocatedOfficers(GeneralFetchRequest request)
        {
            GetAllocatedOfficerResponse response;

            try
            {
                List<AdminUserDetails_minimal> result = await _businessLogic.GetAllocatedOfficersAsync(request);
                response = new GetAllocatedOfficerResponse
                {
                    ActionResponse = new ActionResponse(StatusCodes.Status200OK),
                    OfficersList = result
                };
            }
            catch (Exception ex)
            {
                response = new GetAllocatedOfficerResponse
                {
                    ActionResponse = new ActionResponse(StatusCodes.Status500InternalServerError)

                };
                response.ActionResponse.StatusDescription += ex.Message.ToString();
            }

            return StatusCode(response.ActionResponse.StatusCode, response);
        }
    }
}
