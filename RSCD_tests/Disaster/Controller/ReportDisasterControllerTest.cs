using Disaster.BusinessLogic;
using Disaster.DataAccess.Manager;
using Disaster.DataAccess.Repository;
using Gateway.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using RSCD.Model.Configration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RSCD_tests.Disaster.Controller
{
    class ReportDisasterControllerTest
    {

        private ServiceProvider ServiceProvider { get; set; }

        public ReportDisasterControllerTest()
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
            services.AddScoped<DisasterReport_BL>();
            services.AddScoped<IReportedDisasterCollection, ReportDisaster_CM>();
            ServiceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void ReportDisasterTest()
        {
            //Arrange

            //Act

            //Assert
        }

    }
}
