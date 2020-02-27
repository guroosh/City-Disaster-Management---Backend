using System;
using System.Collections.Generic;
using System.Text;
using Gateway.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RSCD.Model.Configration;
using RSCD.Models.API;
using Xunit;
using Gateway.Model.API;
using Microsoft.AspNetCore.Http;
using Gateway.DataAccess;
using Gateway.DataAccess.Repository;
using Gateway.DataAccess.Manager;
using Gateway.BusinessLogic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RSCD_tests.Gateway
{
    public class LoginControllerTests
    {
        /// <summary>
        /// All the functions iu the controller
        /// input param -> resquest
        /// output -> IActionResult have convert it ObjectResult
        /// </summary>


        public ServiceProvider ServiceProvider { get; set; }
        [Fact]
        public async Task LoginTestAsync()
        {
            SetupServices();
            var MockRequest = new Mock<LoginRequest>();
            MockRequest.Object.Channel = "Android";
            MockRequest.Object.LoginId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";

            ActionResponse res = new ActionResponse(StatusCodes.Status500InternalServerError);

            var loginController = new LoginController(ServiceProvider.GetRequiredService<Login_BL>());

            //Act
            var result = await (loginController.Login(MockRequest.Object)) as ObjectResult;

            //Assert
            Assert.Equal(res.StatusCode, result.StatusCode);
            


        }
        private void SetupServices()
        {
            ServiceCollection services = new ServiceCollection();
            services.Configure<DB_Settings>(options =>
            {
                options.DE_ConnectionString = "";
                options.DE_DataBase = "";
            });
            services.Configure<Auth_Config>(options =>
            {
                options.IssuedTo = "";
                options.Issuer = "";
                options.L1Key = "";
                options.L1Token = "";
                options.L2Token = "";
                options.L3Token = "";
                options.PayLoadKey = "";
                options.SecurityKey = "";
            });
            services.AddScoped<DB_Context>();
            services.AddScoped<IUserCredentialCollection, UserCredential_CM>();
            services.AddScoped<Login_BL>();
            ServiceProvider = services.BuildServiceProvider();
        }

    }
}

