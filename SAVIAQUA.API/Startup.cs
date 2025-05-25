using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Serialization;
using SAVIAQUA.API.Delegates;
using SAVIAQUA.API.Filters;
using SAVIAQUA.Core.App;
using SAVIAQUA.Core.Options;
using SAVIAQUA.Infraestructure.Extensions;

namespace SAVIAQUA.API;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        // services.AddLogging(loggingBuilder =>
        // {
        //     loggingBuilder.AddSerilog(CreateLogger());
        // });

        services.AddAntiforgery(ServiceCollectionActions.SetupAntiForgery);
        services.AddTransient<AuthFilter>();
        services.AddScoped<Session>();
        
        services.SetupAuthentication(Configuration);

        services.AddCors(options =>
        {
            options.AddPolicy(name: "Development", builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetIsOriginAllowed(_ => true);
            });
        });
        
        /** Options Configuration **/
        services.Configure<PasswordOptions>(Configuration.GetSection("PasswordOptions"));
        services.Configure<MailOptions>(configuration.GetSection("MailOptions"));
        
        services
            .AddDbProvider(Configuration)
            .AddRepositories()
            .AddServices();

        services
            .AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        
        // services.AddScoped<MetaDataMiddleware>();
        // services.AddScoped<MessagesMiddleware>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        SetCultureInfo();
        app.Use(AppBuilderFunctions.AddSecurityHeaders);
        app.Use(AppBuilderFunctions.RejectForbiddenMethods);
        app.UseHsts();

        app.UseCors("Development");

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        // app.UseMiddleware<MetaDataMiddleware>();
        // app.UseMiddleware<MessagesMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private static void SetCultureInfo()
    {
        var cultureInfo = new CultureInfo("es-EC")
        {
            NumberFormat =
            {
                NumberDecimalSeparator = "."
            }
        };
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }

    // private Logger CreateLogger()
    // {
    //     var logger = new LoggerConfiguration();
    //     logger = logger.WriteTo.Seq(
    //         Configuration.GetValue<string>("Seq:ServerUrl") ?? string.Empty,
    //         apiKey: Configuration.GetValue<string>("Seq:ApiKey")
    //     )
    //     .Enrich.FromLogContext()
    //     .Enrich.WithProperty("AppID", Configuration.GetValue<string>("AppID"));
    //
    //     switch (Configuration.GetValue<string>("Seq:MinimumLevel"))
    //     {
    //         case "Debug": logger = logger.MinimumLevel.Debug(); break;
    //         case "Information": logger = logger.MinimumLevel.Information(); break;
    //         case "Error": logger = logger.MinimumLevel.Error(); break;
    //         case "Warning": logger = logger.MinimumLevel.Warning(); break;
    //         case "Fatal": logger = logger.MinimumLevel.Fatal(); break;
    //         case "Verbose": logger = logger.MinimumLevel.Verbose(); break;
    //
    //         default: logger = logger.MinimumLevel.Error(); break;
    //     }
    //
    //     return logger.CreateLogger();
    // }
}