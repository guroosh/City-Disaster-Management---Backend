using Microsoft.Extensions.DependencyInjection;
using Moq;
using Registration.BusinessLogic;
using Registration.DataAccess.Manager;
using Registration.DataAccess.Repository;
using Registration.DataEntry.DataAccess.Context;
using Registration.Model.API;
using Registration.Model.DB;
using Registration.Mqtt;
using RSCD.Model.Configration;
using RSCD.Model.Custom;
using RSCD.Models.API;
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
            services.AddScoped<Registration_BL>();
            services.AddScoped<IUsersCollection, Users_CM>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            ServiceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task RegisterCommonUserTest()
        {
            setupServices();
            
            var MockRequest = new Mock<RegisterCommonUserRequest>();
            MockRequest.Object.EmailId = "test2@gmail.com";
            MockRequest.Object.Name = new Name()
            {
                FirstName = "Test",
                LastName = "Tester"
            };
            MockRequest.Object.Password = "test@123";
            MockRequest.Object.PhoneNumber = "9159154630";
            MockRequest.Object.VolunteeringField = "";
            MockRequest.Object.GovernmentIdType = "GNIB";
            MockRequest.Object.GovernmentIdNumber = "HF54548";


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
            MockRequest.Object.EmailId = "testAdmin@gmail.com";
            MockRequest.Object.Name = new Name()
            {
                LastName = "Test",
                FirstName = "Admin"
            };
            MockRequest.Object.BadgeId = "AGFRET4564";
            MockRequest.Object.Department = "Medical";
            MockRequest.Object.Role = "Officer";
            
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

            MockRequest.Object.EmailId = "test1@gmail.com";
            MockRequest.Object.Name = new Name()
            {
                FirstName = "Test",
                LastName = "Updated"
            };
            MockRequest.Object.Password = "test@123";
            MockRequest.Object.PhoneNumber = "9159154630";
            MockRequest.Object.GovernmentIdType = "GNIB";
            MockRequest.Object.GovernmentIdNumber = "HF54548";
            MockRequest.Object.UserCode = "USR523644";

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
            MockRequest.Object.EmailId = "testAdmin@gmail.com";
            MockRequest.Object.Name = new Name()
            {
                LastName = "Updated",
                FirstName = "Admin"
            };
            MockRequest.Object.BadgeId = "AGFRET4564";
            MockRequest.Object.Department = "Medical";
            MockRequest.Object.Role = "Officer";
            MockRequest.Object.UserCode = "USR32191";

            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();

            //Act

            var result = await businessLogic.UpdateAdminUser(MockRequest.Object);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetDocumentAsyncTest()
        {
            setupServices();
            var MockRequest = new Mock<GeneralFetchRequest>();
            MockRequest.Object.Code = "USR523644";


            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();
            //Act

            var result = await businessLogic.GetDocumentAsync(MockRequest.Object);

            //Assert
            Assert.True(result != null);
        }

        
        

        [Fact]

        public async Task UpdateVolunteeringPreferenceAsyncTest()
        {
            setupServices();
            var MockRequest = new Mock<UpdateVolunteeringPreferenceRequest>();
            MockRequest.Object.IsVolunteering = true;
            MockRequest.Object.UserCode = "USR523644";
            MockRequest.Object.VolunteeringField = "Traffic";

            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();

            //Act
            var result = await businessLogic.UpdateVolunteeringPreferenceAsync(MockRequest.Object);

            //Assert
            Assert.True(result);

        }
        [Fact]
        public async Task PublishUserCredentialAsync()
        {
            setupServices();
            var MockRequest = new Mock<Users>();
            MockRequest.Object.EmailId = "testAdmin@gmail.com";
            MockRequest.Object.Name = new Name()
            {
                LastName = "Updated",
                FirstName = "Admin"
            };
            MockRequest.Object.BadgeId = "AGFRET4564";
            MockRequest.Object.Department = "Medical";
            MockRequest.Object.Role = "Officer";
            MockRequest.Object.ReferenceCode = "USR32191";

            var businessLogic = ServiceProvider.GetRequiredService<Registration_BL>();

            //Act

            var result = await businessLogic.PublishUserCredentialAsync(MockRequest.Object);

            //Assert
            Assert.True(result);


        }

    }
    

}
