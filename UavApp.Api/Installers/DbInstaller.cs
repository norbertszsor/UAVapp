using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UavApp.Infrastructure.Data;

namespace UavApp.Api.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UavAppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("UavAppConnectionString")));
        }
    }
}
