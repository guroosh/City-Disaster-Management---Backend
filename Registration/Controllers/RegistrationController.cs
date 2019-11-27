using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration.Model.API;
using RSCD.Models.API;

namespace Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        [Route("test")]
        [HttpGet]
        public ActionResult Test()
        {
            return Ok(new ActionResponse(StatusCodes.Status200OK));
        }

        [Route("addUser")]
        [HttpPost]
        public ActionResult AddUser(AddUserRequest request)
        {
            return Ok();
        }

        [Route("getUserDetails")]
        [HttpPost]
        public ActionResult GetUserDetails(UserDetailRquest rquest)
        {
            return Ok();
        }


    }
}