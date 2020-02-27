using Gateway.BusinessLogic;
using Gateway.DataAccess.Repository;
using Gateway.DataAccess.Manager;
using Gateway.Model.API;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RSCD.Model.Configration;
using RSCD.Models.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Gateway.DataAccess;
using RSCD.Model.Message;

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

            var businessLogic = ServiceProvider.GetRequiredService<Login_BL>();

            //Act
            var result = await businessLogic.CheckCredentialsAsync(MockRequest.Object);

            //Assert

            Assert.Equal(result.ReferenceCode, response.ReferenceCode);
        }
        [Fact]
        public async Task CreateAsyncTest()
        {
            SetupServices();
            var MockRequest = new Mock<NewUser>();
            MockRequest.Object.ReferenceCode = "Android";
            MockRequest.Object.EmailId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";
            MockRequest.Object.IsCommonUser = true;

            bool response = true;
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
            var MockRequest = new Mock<NewUser>();
            MockRequest.Object.ReferenceCode = "Android";
            MockRequest.Object.EmailId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";
            MockRequest.Object.IsCommonUser = true;
            bool response = false;
            var businessLogic = ServiceProvider.GetRequiredService<Login_BL>();

            //Act

            var result = await businessLogic.UpdateDocumentAsync(MockRequest.Object);

            //Assert
            Assert.True(result);

        }
        [Fact]
        public async Task DeleteDocumentAsyncTest()
        {
            SetupServices();
            var MockRequest = new Mock<NewUser>();
            MockRequest.Object.ReferenceCode = "Android";
            MockRequest.Object.EmailId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";
            MockRequest.Object.IsCommonUser = true;
            bool response = false;
            var businessLogic = ServiceProvider.GetRequiredService<Login_BL>();

            //Act

            var result = await businessLogic.DeleteDocumentAsync(MockRequest.Object);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetDocumentAsyncTest()
        {
            SetupServices();
            var MockRequest = new Mock<NewUser>();
            MockRequest.Object.ReferenceCode = "Android";
            MockRequest.Object.EmailId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";
            MockRequest.Object.IsCommonUser = true;
           // bool response = false;
            var businessLogic = ServiceProvider.GetRequiredService<Login_BL>();
            //Act

            var result = await businessLogic.GetDocumentAsync(MockRequest.Object);

            //Assert
            Assert.True((bool)result);
        }

        [Fact]
        public async Task GetAllDocumentsAsyncTest()
        {
            SetupServices();
            var MockRequest = new Mock<NewUser>();
            MockRequest.Object.ReferenceCode = "Android";
            MockRequest.Object.EmailId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";
            MockRequest.Object.IsCommonUser = true;
            //bool response = false;
            var businessLogic = ServiceProvider.GetRequiredService<Login_BL>();

            //Act
            var result = await businessLogic.GetAllDocumentsAsync(MockRequest.Object);

            //Assert
            //Assert.True(result);
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
