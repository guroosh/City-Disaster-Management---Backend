using Microsoft.Extensions.DependencyInjection;
using RSCD.Model.Configration;
using RescueTeam.DataAccess.Context;
using RescueTeam.Mqtt;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Threading.Tasks;
using RSCD.Model.Message;
using Moq;
using RescueTeam.BusinessLogic;
using RescueTeam.DataAccess.Repository;
using RescueTeam.DataAccess.Manager;
using RescueTeam.Model.API;
using RSCD.Models.API;

namespace RSCD_tests.RescueTeam
{
    public class BusinessLogicTests
    {
        public ServiceProvider ServiceProvider { get; set; }
        private void SetupServices()
        {

            ServiceCollection services = new ServiceCollection();
            services.Configure<Mqtt_Settings>(options =>
            {
                options.ClientId = "RSCD_RegistrationModule";
                options.Host = "broker.hivemq.com";
                options.SuscribeTopic = "RSCD/Registration/#";
            });
            services.Configure<DB_Settings>(options =>
            {
                options.DE_ConnectionString = "mongodb://amrish_kulasekaran:kavi%40123@localhost:27017/?authSource=admin";
                options.DE_DataBase = "rscd_db";
            });
            services.AddScoped<DB_Context>();
            services.AddHostedService<MqttSubscriber>();
            services.AddScoped<RSCD.Mqtt.MqttPublisher>();
            services.AddScoped<RescueTeam_BL>();
            services.AddScoped<IOfficerDetailCollection, RescueTeam_CM>();
            services.AddScoped<IVerifiedDisasterReportCollection, VerifiedDisasterReport_CM>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            ServiceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task CreateAsyncTest()
        {
            SetupServices();
            var MockRequest = new Mock<UserDetailMessage>();
            MockRequest.Object.ReferenceCode = "USR4555684";
            MockRequest.Object.EmailId = "admin1@tcd.ie";
            MockRequest.Object.Password = "1234";
            MockRequest.Object.IsCommonUser = false;
            MockRequest.Object.Department = "Medical";

            var businessLogic = ServiceProvider.GetRequiredService<RescueTeam_BL>();

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
            MockRequest.Object.ReferenceCode = "USR4555684";
            MockRequest.Object.EmailId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";

            var businessLogic = ServiceProvider.GetRequiredService<RescueTeam_BL>();

            //Act

            var result = await businessLogic.UpdateDocumentAsync(MockRequest.Object);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void AllocateResourceTest()
        {
            SetupServices();
            var MockRequest = new Mock<VerifiedDisasterMessage>();
            MockRequest.Object.ReferenceCode = "RD523261";

            var businessLogic = ServiceProvider.GetRequiredService<RescueTeam_BL>();

           //Act
           businessLogic.ResourceAllocationAsync(MockRequest.Object);
        }

        [Fact]
        public async Task AllocateAdditionalResourceTest()
        {
            SetupServices();
            var MockRequest = new Mock<AdditionalResourcesRequest>();
            MockRequest.Object.ReferenceCode = "RD523261";
            MockRequest.Object.AdditionalUnits = 2;
            MockRequest.Object.Department = "Medical";

            var businessLogic = ServiceProvider.GetRequiredService<RescueTeam_BL>();

            //Act
            var result = await businessLogic.AllocateAdditionalResourceAsync(MockRequest.Object);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAllocatedOfficersTest()
        {
            SetupServices();
            var MockRequest = new Mock<GeneralFetchRequest>();
            MockRequest.Object.Code = "RD523261";

            var businessLogic = ServiceProvider.GetRequiredService<RescueTeam_BL>();

            //Act
            var result = await businessLogic.GetAllocatedOfficersAsync(MockRequest.Object);

            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task DeallocateOfficerTest()
        {
            SetupServices();
            var MockRequest = new Mock<ResourceDeallocationRequest>();
            MockRequest.Object.DisasterReferenceCode = "RD523261";
            MockRequest.Object.OfficerReferenceCode = "USR4555684";

            var businessLogic = ServiceProvider.GetRequiredService<RescueTeam_BL>();

            //Act
            var result = await businessLogic.ResourceDeallocationAsync(MockRequest.Object);

            //Assert
            Assert.True(result);
        }
    }
}
