using Gateway.BusinessLogic;
using Gateway.Model.API;
using Microsoft.AspNetCore.Http;
using Moq;
using RSCD.Models.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RSCD_tests.Gateway.BusinessLogic
{
    public class Login_BLTests
    {
        [Fact]
        public async Task CheckUserCredentialTest()
        {
            //Arrange
            var MockRequest = new Mock<LoginRequest>();
            MockRequest.Object.Channel = "Android";
            MockRequest.Object.LoginId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";


            ActionResponse res = new ActionResponse(StatusCodes.Status500InternalServerError);
            LoginResponse response = new LoginResponse()
            {
                ActionResponse = res,
                ReferenceCode = "USR1234",
                AccessToken = "",
                IsCommonUser = true
            };

            var businessLogic = new Login_BL();

            //Act
            var result = await businessLogic.CheckCredentialsAsync(MockRequest.Object);

            //Assert

            Assert.Equal(result.ReferenceCode, response.ReferenceCode);
        }
    }
}
