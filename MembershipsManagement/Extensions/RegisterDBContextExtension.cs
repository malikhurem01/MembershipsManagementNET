using MembershipsManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace MembershipsManagement.Extensions
{
    public static class RegisterDBContextExtension
    {
        public static void RegisterDBContext(this IServiceCollection service, ConfigurationManager config)
        {
            service.AddDbContext<DataContext>(options => options.UseSqlServer(config.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));
        }
    }
}
