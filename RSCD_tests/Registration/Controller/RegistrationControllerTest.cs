using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSCD_tests.Registration.Controller
{
    class RegistrationControllerTest
    {
        private ServiceProvider ServiceProvider { get; set; }

        public RegistrationControllerTest()
        {
            SetupServices();
        }

        private void SetupServices()
        {
            ServiceCollection services = new ServiceCollection();
            ServiceProvider = services.BuildServiceProvider();
        }

        public void RegisterCUTest()
        {
            throw new NotImplementedException();
        }

        public void RegisterAUTest()
        {
            throw new NotImplementedException();
        }
    }
}
