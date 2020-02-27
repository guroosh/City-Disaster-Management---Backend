using Microsoft.Extensions.DependencyInjection;
using Moq;
using Registration.BusinessLogic;
using Registration.DataAccess.Manager;
using Registration.DataAccess.Repository;
using Registration.DataEntry.DataAccess.Context;
using Registration.Model.API;
using Registration.Mqtt;
using RSCD.Model.Configration;
using RSCD.Model.Custom;
using RSCD.Model.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RSCD_tests.Registration
{
    public class Registration_BLTests
    {
        public ServiceProvider ServiceProvider { get; set; }
        private void setupServices()
        {
            
        ServiceCollection services = new ServiceCollection();
            services.Configure<Mqtt_Settings>(options =>
            {
                options.ClientId = "RSCD_RegistrationModule";
                options.Host = "localhost";
                options.SuscribeTopic = "RSCD/Registration/#";
            });
            services.Configure<DB_Settings>(options =>
            {
                options.DE_ConnectionString = "";
                options.DE_DataBase = "";
            });
            services.AddScoped<DB_Context>();
            services.AddHostedService<MqttSubscriber>();
            services.AddScoped<RSCD.Mqtt.MqttPublisher>();
            services.AddScoped<Registration_BL>();
            services.AddScoped<IUsersCollection, Registration_CM>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            ServiceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task CreateAsyncTest()
        {
            setupServices();
            var MockRequest = new Mock<NewUser>();
            MockRequest.Object.ReferenceCode = "Android";
            MockRequest.Object.EmailId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";
            MockRequest.Object.IsCommonUser = true;

            bool response = true;
            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();

            //Act

            var result = await businessLogic.CreateAsync(MockRequest.Object);

            //Assert
            Assert.True(result);
        }
        [Fact]
        public async Task RegisterCommonUserTest()
        {
            setupServices();
            
            var MockRequest = new Mock<RegisterCommonUserRequest>();
            MockRequest.Object.EmailId = "";
            MockRequest.Object.Name = new Name();
            MockRequest.Object.Password = "";
            MockRequest.Object.PhoneNumber = "";
            MockRequest.Object.VolunteeringField = "";
            MockRequest.Object.GovernmentIdType = "";
            MockRequest.Object.GovernmentIdNumber = "";
            MockRequest.Object.GovernmentIdType = "";

            bool response = true;
            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();

            //Act

            var result = await businessLogic.RegisterCommonUser(MockRequest.Object);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RegisterAdminUserTest()
        {
            setupServices();

            var MockRequest = new Mock<RegisterAdminUserRequest>();
            MockRequest.Object.EmailId = "";
            MockRequest.Object.Name = new Name();
            MockRequest.Object.BadgeId = "";
            MockRequest.Object.Department = "";
            MockRequest.Object.Role = "";
            
            bool response = true;
            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();

            //Act

            var result = await businessLogic.RegisterAdminUser(MockRequest.Object);

            //Assert
            Assert.True(result);
        }
        [Fact]
        public async Task UpdateCommonUserTest()
        {
            setupServices();

            var MockRequest = new Mock<UpdateCommonUserRequest>();
            MockRequest.Object.EmailId = "";
            MockRequest.Object.Name = new Name();
            MockRequest.Object.Password = "";
            MockRequest.Object.PhoneNumber = "";
            MockRequest.Object.GovermentIdNumber = "";
            MockRequest.Object.GovermentIdType = "";
            MockRequest.Object.CurrentUserCode = "";
            MockRequest.Object.UserCode = "";
            
            bool response = true;
            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();

            //Act

            var result = await businessLogic.UpdateCommonUser(MockRequest.Object);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAdminUserTest()
        {
            setupServices();

            var MockRequest = new Mock<UpdateAdminUserRequest>();
            MockRequest.Object.EmailId = "";
            MockRequest.Object.Name = new Name();
            MockRequest.Object.Department = "";
            MockRequest.Object.BadgeId = "";
            MockRequest.Object.CurrentUserCode = "";
            MockRequest.Object.UserCode = "";
            MockRequest.Object.Role = "";

            bool response = true;
            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();

            //Act

            var result = await businessLogic.UpdateAdminUser(MockRequest.Object);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteDocumentAsyncTest()
        {
            setupServices();
            var MockRequest = new Mock<NewUser>();
            MockRequest.Object.ReferenceCode = "Android";
            MockRequest.Object.EmailId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";
            MockRequest.Object.IsCommonUser = true;
            bool response = false;
            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();

            //Act

            var result = await businessLogic.DeleteDocumentAsync(MockRequest.Object);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetDocumentAsyncTest()
        {
            setupServices();
            var MockRequest = new Mock<NewUser>();
            MockRequest.Object.ReferenceCode = "Android";
            MockRequest.Object.EmailId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";
            MockRequest.Object.IsCommonUser = true;
            // bool response = false;
            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();
            //Act

            var result = await businessLogic.GetDocumentAsync(MockRequest.Object);

            //Assert
            Assert.True((bool)result);
        }

        [Fact]
        public async Task GetAllDocumentsAsyncTest()
        {
            setupServices();
            var MockRequest = new Mock<NewUser>();
            MockRequest.Object.ReferenceCode = "Android";
            MockRequest.Object.EmailId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";
            MockRequest.Object.IsCommonUser = true;
            //bool response = false;
            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();

            //Act
            var result = await businessLogic.GetAllDocumentsAsync(MockRequest.Object);

            //Assert
            //Assert.True(result);
        }
        [Fact]
        public async Task UpdateDocumentAsyncTest()
        {
            setupServices();
            var MockRequest = new Mock<NewUser>();
            MockRequest.Object.ReferenceCode = "Android";
            MockRequest.Object.EmailId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";
            MockRequest.Object.IsCommonUser = true;
            bool response = false;
            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();

            //Act

            var result = await businessLogic.UpdateDocumentAsync(MockRequest.Object);

            //Assert
            Assert.True(result);

        }

        [Fact]

        public async Task UpdateVolunteeringPreferenceAsyncTest()
        {
            setupServices();
            var MockRequest = new Mock<UpdateVolunteeringPreferenceRequest>();
            MockRequest.Object.CurrentUserCode = "";
            MockRequest.Object.IsVolunteering = true;
            MockRequest.Object.UserCode = "";
            MockRequest.Object.VolunteeringField = "";
            bool response = false;
            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();

            //Act

            var result = await businessLogic.UpdateDocumentAsync(MockRequest.Object);

            //Assert
            Assert.True(result);

        }
        [Fact]
        public async Task PublishUserCredentialAsync()
        {
            setupServices();
            var MockRequest = new Mock<NewUser>();
            bool response = false;
            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();

            //Act

            var result = await businessLogic.UpdateDocumentAsync(MockRequest.Object);

            //Assert
            Assert.True(result);


        }

    }
    

}
