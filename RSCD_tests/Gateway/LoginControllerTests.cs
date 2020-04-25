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
            MockRequest.Object.LoginId = "testmail@gmail.com";
            MockRequest.Object.Password = "123";

            ActionResponse res = new ActionResponse(StatusCodes.Status200OK);

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
                options.DE_ConnectionString = "mongodb://amrish_kulasekaran:kavi%40123@localhost:27017/?authSource=admin";
                options.DE_DataBase = "rscd_db";
            });
            services.Configure<Auth_Config>(options =>
            {
                options.IssuedTo = "Actors";
                options.Issuer = "RSCD Admin";
                options.L1Key = "RHluYXR0cmFsTDFUb2tlbktleTEyMzQ1";
                options.L1Token = "RSCD-Token";
                options.L2Token = "RSCD-JWT-Token";
                options.L3Token = "RSCD-Module-Token";
                options.PayLoadKey = "12d08eb0aa92b945965565b29d53ad1f15a55144ed0714ac56c34677ccbcb400";
                options.SecurityKey = "GFsZ3VkaUthcnVwcGlhaEdhbmRoaVJKQmFsYWpp";

            });
            services.AddScoped<DB_Context>();
            services.AddScoped<IUserCredentialCollection, UserCredential_CM>();
            services.AddScoped<Login_BL>();
            ServiceProvider = services.BuildServiceProvider();
        }

    }
}

