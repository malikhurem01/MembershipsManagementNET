using MembershipsManagement.Extensions;
var builder = WebApplication.CreateBuilder(args);

const string CorsPolicyName = "MyReactFrontEndServer";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();
builder.Services.RegisterDBContext(builder.Configuration);
builder.Services.RegisterIdentityAuthentication(builder.Configuration);
builder.Services.RegisterAutoMapper();
builder.Services.RegisterCORSPolicy(CorsPolicyName, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(RegisterCORSExtension.origin);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
