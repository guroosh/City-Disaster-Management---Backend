using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSCD.Models.API;
using Gateway.BusinessLogic;
using Gateway.Model.API;

namespace Gateway.Controllers  
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Login_BL _businessLogic;

        public LoginController(Login_BL businessLogic)
        {
            _businessLogic = businessLogic;
        }

        [Route("test")]
        [HttpGet]
        public ActionResult Test()
        {
            return Ok(new ActionResponse(StatusCodes.Status200OK));
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            LoginResponse response;

            try
            {
                response = await _businessLogic.CheckCredentialsAsync(request);
                response.ActionResponse = new ActionResponse(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = new LoginResponse
                {
                    ActionResponse = new ActionResponse(StatusCodes.Status500InternalServerError)
                };
            
                response.ActionResponse.StatusDescription += ex.Message.ToString();
            }

            return StatusCode(response.ActionResponse.StatusCode, response);
        }
    }
}