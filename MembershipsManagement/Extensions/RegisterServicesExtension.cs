using MembershipsManagement.Services.Authentication;
using MembershipsManagement.Services.Member;
using MembershipsManagement.Services.SpreadSheets;

namespace MembershipsManagement.Extensions
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection service)
        {
            service.AddScoped<IAuthenticationService, AuthenticationService>();
            service.AddScoped<IMemberService, MemberService>();
            service.AddScoped<IBazaService, BazaService>();
        }
    }
}
