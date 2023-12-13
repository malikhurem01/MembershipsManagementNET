using Microsoft.Extensions.Configuration;

namespace MembershipsManagement.Extensions
{
    public static class RegisterCORSExtension
    {
        public static string origin = "FrontEndReactServerApp";
        public static void RegisterCORSPolicy(this IServiceCollection service, ConfigurationManager config)
        {
            service.AddCors(options =>
            {
                options.AddPolicy(name: origin,
                policy =>
                {
                    policy
                    .WithOrigins(config.GetSection("Cors:Origin").Value)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }
    }
}
