using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration.BusinessLogic;
using Registration.Model.API;
using RSCD.Models.API;

namespace Registration.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly Registration_BL _businessLogic;
        public RegistrationController(Registration_BL businessLogic)
        {
            _businessLogic = businessLogic;
        }

        [Route("test")]
        [HttpGet]
        public ActionResult Test()
        {
            return Ok(new ActionResponse(StatusCodes.Status200OK));
        }

        [Route("registerCu")]
        [HttpPost]
        public async Task<IActionResult> RegisterCommonUser(RegisterCommonUserRequest request)
        {
            ActionResponse response;

            try
            {
                bool result = await _businessLogic.RegisterCommonUser(request);
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