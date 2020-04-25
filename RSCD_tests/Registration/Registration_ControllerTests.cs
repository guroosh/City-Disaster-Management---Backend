using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Registration.BusinessLogic;
using Registration.Controllers;
using Registration.DataAccess.Manager;
using Registration.DataAccess.Repository;
using Registration.DataEntry.DataAccess.Context;
using Registration.Model.API;
using Registration.Mqtt;
using RSCD;
using RSCD.Model.Configration;
using RSCD.Models.API;
using RSCD.Mqtt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RSCD_tests.Registration
{
   public class Registration_ControllerTests
    {
        public ServiceProvider ServiceProvider { get; set; }
        [Fact]
        public async Task RegisterCommonUserTest()
        {
            SetupServices();
            var MockRequest = new Mock<RegisterCommonUserRequest>();
            MockRequest.Object.Name = new RSCD.Model.Custom.Name();
            MockRequest.Object.Password = "1234";
            MockRequest.Object.PhoneNumber = "abc123";
            MockRequest.Object.GovernmentIdNumber ="";
            MockRequest.Object.GovernmentIdType ="";
            MockRequest.Object.IsVolunteering =true;
            MockRequest.Object.VolunteeringField = "";

            ActionResponse res = new ActionResponse(StatusCodes.Status200OK);

            var registrationController = new RegistrationController(ServiceProvider.GetRequiredService<Registration_BL>());
            //Act
            var result = await(registrationController.RegisterCommonUser(MockRequest.Object)) as ObjectResult;

            //Assert
            Assert.Equal(res.StatusCode, result.StatusCode);

        }
        [Fact]
        public async Task RegisterAdminUserTest()
        {
            SetupServices();
            var MockRequest = new Mock<RegisterAdminUserRequest>();
            MockRequest.Object.Name = new RSCD.Model.Custom.Name();
            MockRequest.Object.Role = "";
            MockRequest.Object.EmailId = "";
            MockRequest.Object.BadgeId = "";
            MockRequest.Object.Department = "";
            ActionResponse res = new ActionResponse(StatusCodes.Status500InternalServerError);

            var registrationController = new RegistrationController(ServiceProvider.GetRequiredService<Registration_BL>());
            //Act
            var result = await (registrationController.RegisterAdminUser(MockRequest.Object)) as ObjectResult;

            //Assert
            Assert.Equal(res.StatusCode, result.StatusCode);
        }
        [Fact]
        public async Task UpdateCommonUserTest()
        {
            SetupServices();
            var MockRequest = new Mock<UpdateCommonUserRequest>();
            MockRequest.Object.Name = new RSCD.Model.Custom.Name();
            MockRequest.Object.Password = "";
            MockRequest.Object.PhoneNumber = "";
            MockRequest.Object.UserCode = "";
            MockRequest.Object.GovernmentIdNumber = "";
            MockRequest.Object.GovernmentIdType = "";
            MockRequest.Object.UserCode = "";
            MockRequest.Object.CurrentUserCode = "";
            ActionResponse res = new ActionResponse(StatusCodes.Status500InternalServerError);

            var registrationController = new RegistrationController(ServiceProvider.GetRequiredService<Registration_BL>());
            //Act
            var result = await (registrationController.UpdateCommonUser(MockRequest.Object)) as ObjectResult;

            //Assert
            Assert.Equal(res.StatusCode, result.StatusCode);

        }
        [Fact]
        public async Task UpdateAdminUserTest()
        {
            SetupServices();
            var MockRequest = new Mock<UpdateAdminUserRequest>();
            MockRequest.Object.Name =new RSCD.Model.Custom.Name();
            MockRequest.Object.EmailId = "";
            MockRequest.Object.Department = "";
            MockRequest.Object.CurrentUserCode = "";
            MockRequest.Object.BadgeId = "";
            MockRequest.Object.Role = "";
            MockRequest.Object.UserCode = "";
            ActionResponse res = new ActionResponse(StatusCodes.Status500InternalServerError);

            var registrationController = new RegistrationController(ServiceProvider.GetRequiredService<Registration_BL>());
            //Act
            var result = await (registrationController.UpdateAdminUser(MockRequest.Object)) as ObjectResult;

            //Assert
            Assert.Equal(res.StatusCode, result.StatusCode);
        }
        [Fact]
        public async Task UpdateVolunteeringPreferenceTest()
        {
            SetupServices();
            var MockRequest = new Mock<UpdateVolunteeringPreferenceRequest>();
            MockRequest.Object.CurrentUserCode = "";
            MockRequest.Object.IsVolunteering =true;
            MockRequest.Object.UserCode = "";
            MockRequest.Object.VolunteeringField = "";

            ActionResponse res = new ActionResponse(StatusCodes.Status500InternalServerError);

            var registrationController = new RegistrationController(ServiceProvider.GetRequiredService<Registration_BL>());
            //Act
            var result = await (registrationController.UpdateVolunteeringPreference(MockRequest.Object)) as ObjectResult;

            //Assert
            Assert.Equal(res.StatusCode, result.StatusCode);

                                         }
        [Fact]
        public async Task FetchCommonUserDetailsTest()
        {
            SetupServices();
            var MockRequest = new Mock<GeneralFetchRequest>();
            MockRequest.Object.Code = "";
            ActionResponse res = new ActionResponse(StatusCodes.Status500InternalServerError);

            var registrationController = new RegistrationController(ServiceProvider.GetRequiredService<Registration_BL>());
            //Act
            var result = await (registrationController.FetchCommonUserDetails(MockRequest.Object)) as ObjectResult;

            //Assert
            Assert.Equal(res.StatusCode, result.StatusCode);
        }
        [Fact]
        public async Task FetchAdminUserDetailsTest()
        {
            SetupServices();
            var MockRequest = new Mock<GeneralFetchRequest>();
            MockRequest.Object.Code = "";
            ActionResponse res = new ActionResponse(StatusCodes.Status500InternalServerError);

            var registrationController = new RegistrationController(ServiceProvider.GetRequiredService<Registration_BL>());
            //Act
            var result = await (registrationController.FetchAdminUserDetails(MockRequest.Object)) as ObjectResult;

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
           
            services.AddScoped<DB_Context>();
            services.AddHostedService<MqttSubscriber>();
            services.AddScoped<MqttPublisher>();
            services.AddScoped<Registration_BL>();
            services.AddScoped<IUsersCollection, Users_CM>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }
    }
}
