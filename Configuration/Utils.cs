using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration
{
    public class Utils : IServiceProvider
    {
        private IServiceProvider Services { get; }

        public Utils()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            
            var services = new ServiceCollection();
            
            services.AddPostgresDb(configuration);
            
            services.AddServices();
            Services = services.BuildServiceProvider();
        }

        public object GetService(Type serviceType)
        {
            return Services.GetService(serviceType);
        }
    }
}