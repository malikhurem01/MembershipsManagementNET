using MembershipsManagement.Services.Authentication;
using MembershipsManagement.Services.DzematServiceImpl;
using MembershipsManagement.Services.FinanceServices;
using MembershipsManagement.Services.MedzlisServiceImpl;
using MembershipsManagement.Services.MemberServiceImpl;
using MembershipsManagement.Services.SpreadSheets;

namespace MembershipsManagement.Extensions
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection service)
        {
            service.AddScoped<IAuthenticationService, AuthenticationService>();
            service.AddScoped<IMemberService, MemberService>();
            service.AddScoped<ISpreadsheetService, SpreadsheetService>();
            service.AddScoped<IMedzlisService, MedzlisService>();
            service.AddScoped<IPaymentService, PaymentService>();
            service.AddScoped<IDzematService, DzematService>();
        }
    }
}
