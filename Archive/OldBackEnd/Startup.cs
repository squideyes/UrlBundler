using BackEnd.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(BackEnd.Startup))]
namespace BackEnd
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IBlackListChecker>(
                new EnvironmentBlackListChecker());

            builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}