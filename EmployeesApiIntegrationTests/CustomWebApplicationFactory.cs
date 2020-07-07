using EmployeesApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeesApiIntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> :
        WebApplicationFactory<TStartup> where TStartup : class
    {
        public DateTime SystemTimeToUse { get; set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(ISystemTime));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddTransient<ISystemTime, TestingSystemTime>();
                services.BuildServiceProvider();
            });
        }
    }

    public class TestingSystemTime : ISystemTime
    {
        public DateTime TimeToReturn { get; set; }

        public DateTime GetCreatedAt()
        {
            return DateTime.Now;
        }

        public DateTime GetCurrent()
        {
            return TimeToReturn;
        }
    }
}
