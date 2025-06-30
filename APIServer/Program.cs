using APIServer.Middlewares;
using APIServer.Models.Interfaces;
using APIServer.Repositories;
using APIServer.Services;
using APIServer.Utilities;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
var serverConfig = configuration.GetSection(nameof(ServerConfig));
builder.Configuration
	.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
// Config ���� ���
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
	loggerConfiguration
		.ReadFrom.Configuration(hostingContext.Configuration);
});

builder.Services.AddControllers();
builder.Services.AddSingleton<IRedisRepository, RedisRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IAIService, AIService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IAccountService, AccountService>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowClient", policy =>
	{
		policy.WithOrigins(["http://localhost:3000"
		])
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});
builder.WebHost.ConfigureKestrel(options =>
{
	options.ListenAnyIP(serverConfig.Get<ServerConfig>()!.Port);
});


var app = builder.Build();
app.UseCors("AllowClient");
app.UseRouting();
app.UseMiddleware<CustomAuthenticationMiddleware>();
app.MapControllers();
app.Run();
