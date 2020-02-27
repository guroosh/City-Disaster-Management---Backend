using Microsoft.Extensions.DependencyInjection;
using Moq;
using Registration.BusinessLogic;
using Registration.DataAccess.Manager;
using Registration.DataAccess.Repository;
using Registration.DataEntry.DataAccess.Context;
using Registration.Mqtt;
using RSCD.Model.Configration;
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
        private readonly ServiceProvider ServiceProvider;
        private void SetupServices()
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
        }
        [Fact]
        public async Task CreateAsyncRegistrationTest()
        {
            SetupServices();
            var MockRequest = new Mock<NewUser>();
            MockRequest.Object.ReferenceCode = "Android";
            MockRequest.Object.EmailId = "palkarm@tcd.ie";
            MockRequest.Object.Password = "1234";
            MockRequest.Object.IsCommonUser = true;

            bool response = true;
            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();
            //Act
            var result = await businessLogic.GetAllDocumentsAsync(MockRequest.Object);



            //Assert
            Assert.True((bool)result);
        }

    }


}
