using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RescueTeam.BusinessLogic;
using RescueTeam.Model.API;
using RSCD.Models.API;

namespace RescueTeam.Controllers
{
    [Route("api/[controller]")]
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

        [Route("resourceallocation")]
        [HttpPost]
        public async Task<IActionResult> AdditionalResourceAllocation(AdditionalResourcesRequest request)
        {
            AdditionalResourceAllocationResponse response = new AdditionalResourceAllocationResponse()
            {

                ActionResponse = new ActionResponse(StatusCodes.Status200OK)


    };
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
