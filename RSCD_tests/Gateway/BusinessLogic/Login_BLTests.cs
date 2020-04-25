using Gateway.BusinessLogic;
using Gateway.DataAccess.Repository;
using Gateway.DataAccess.Manager;
using Gateway.Model.API;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RSCD.Model.Configration;
using RSCD.Models.API;
using RSCD.Model.Message;
using System;
using System.Threading.Tasks;
using Xunit;
using Gateway.DataAccess;
using Newtonsoft.Json;

namespace RSCD_tests.Gateway.BusinessLogic
{
    public class Login_BLTests
    {
        public ServiceProvider ServiceProvider { get;  set; }
        [Fact]
        public async Task CheckUserCredentialTest()
        {
            SetupServices();
            //Arrange
            var MockRequest = new Mock<LoginRequest>();
            MockRequest.Object.Channel = "Android";
            MockRequest.Object.LoginId = "testmail@gmail.com";
            MockRequest.Object.Password = "123";

            ActionResponse res = new ActionResponse(StatusCodes.Status500InternalServerError);
            LoginResponse response = new LoginResponse()
            {
                ActionResponse = res,
                ReferenceCode = "USR843731",
                AccessToken = "",
                IsCommonUser = true
            };

            var businessLogic = ServiceProvider.GetRequiredService<Login_BL>();

            //Act
            var result = await businessLogic.CheckCredentialsAsync(MockRequest.Object);

            //Assert
            Console.WriteLine(JsonConvert.SerializeObject(result));

            Assert.Equal( response.ReferenceCode, result.ReferenceCode);
        }
        [Fact]
        public async Task CreateAsyncTest()
        {
            SetupServices();
            var MockRequest = new Mock<UserDetailMessage>();
            MockRequest.Object.ReferenceCode = "USR872447";
            MockRequest.Object.EmailId = "datatest1@tcd.ie";
            MockRequest.Object.Password = "1234";
            MockRequest.Object.IsCommonUser = true;

            var businessLogic = ServiceProvider.GetRequiredService<Login_BL>();

            //Act

            var result = await businessLogic.CreateAsync(MockRequest.Object);

            //Assert
            Assert.True(result);
        }
        [Fact]
        public async Task UpdateDocumentAsyncTest()
        {
            SetupServices();
            var MockRequest = new Mock<UserDetailMessage>();
            MockRequest.Object.ReferenceCode = "USR872447";
            MockRequest.Object.EmailId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";
            MockRequest.Object.IsCommonUser = true;

            var businessLogic = ServiceProvider.GetRequiredService<Login_BL>();

            //Act

            var result = await businessLogic.UpdateDocumentAsync(MockRequest.Object);

            //Assert
            Assert.True(result);

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
