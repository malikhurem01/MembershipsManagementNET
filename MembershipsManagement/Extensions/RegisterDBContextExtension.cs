using Hangfire;
using MembershipsManagement.Data;
using MembershipsManagement.HostedServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace MembershipsManagement.Extensions
{
    public static class RegisterDBContextExtension
    {
        public static void RegisterDBContext(this IServiceCollection service, ConfigurationManager config)
        {
            service.AddDbContext<DataContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            service.AddHangfire(hangfire => hangfire.UseSqlServerStorage(config.GetConnectionString("DefaultConnection")));
            service.AddHangfireServer(); // Add a Hangfire server
            service.AddHostedService<HangfireStartup>();
        }
    }
}
