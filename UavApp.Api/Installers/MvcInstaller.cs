using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UavApp.Application;
using UavApp.Infrastructure;

namespace UavApp.Api.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            services.AddInfrastructure();
            services.AddControllers();
        }
    }
}
