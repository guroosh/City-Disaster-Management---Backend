using Gateway.BusinessLogic;
using Gateway.Controllers;
using Gateway.DataAccess;
using Gateway.Model.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RSCD.Model.Configration;
using RSCD.Models.API;
using Xunit;

namespace RSCD_tests.Gateway
{
    public class LoginControllerTests
    {
        private ServiceProvider ServiceProvider { get; set; }

        public LoginControllerTests()
        {
            SetupServices();
        }

        private void SetupServices()
        {
            ServiceCollection services = new ServiceCollection();
            services.Configure<DB_Settings>(options =>
            {
                options.DE_ConnectionString = "";
                options.DE_DataBase = "";
            });
            services.AddScoped<DB_Context>();
            services.AddScoped<Login_BL>();
            ServiceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async System.Threading.Tasks.Task LoginTestAsync()
        {
            //Arrange
            var MockRequest = new Mock<LoginRequest>();
            MockRequest.Object.Channel = "Android";
            MockRequest.Object.LoginId = "";
            MockRequest.Object.Password = "";

            ActionResponse res = new ActionResponse(StatusCodes.Status200OK);
            LoginResponse response = new LoginResponse()
            {
                ActionResponse = res,
                UserCode = "",
                AccessToken = "",
            };

            var loginController = new LoginController(ServiceProvider.GetRequiredService<Login_BL>());

            //Act
            IActionResult result = await loginController.Login(MockRequest.Object);

            //Assert
            ObjectResult ok = result as ObjectResult;
            Assert.True(ok.StatusCode == StatusCodes.Status500InternalServerError);
        }
    }   
}
