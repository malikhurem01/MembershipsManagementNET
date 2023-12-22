using MembershipsManagement.AutoMapperMaps;

namespace MembershipsManagement.Extensions
{
    public static class RegisterAutoMapperExtension
    {
        public static void RegisterAutoMapper(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(UserMap));
            service.AddAutoMapper(typeof(MemberMap));
            service.AddAutoMapper(typeof(SpreadsheetMap));
            service.AddAutoMapper(typeof(MedzlisMap));
            service.AddAutoMapper(typeof(PaymentMap));
            service.AddAutoMapper(typeof(ExpenseItemMap));
        }
    }
}
