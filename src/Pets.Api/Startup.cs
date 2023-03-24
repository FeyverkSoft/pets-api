namespace Pets.Api;

using System.Data;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

using Asp.Core.FluentExtensions;

using Authorization;

using Core;

using DB.Migrations;

using Domain.Authentication;
using Domain.Documents;
using Domain.Pet;

using Extensions;

using FluentValidation.AspNetCore;

using Infrastructure.Authentication;
using Infrastructure.FileStoreService;
using Infrastructure.Markdown;
using Infrastructure.Pet;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using Middlewares;

using MongoDB.Driver;
using MongoDB.Driver.GridFS;

using MySqlConnector;

using Queries.Infrastructure;

using Rabbita.Core.DafaultHandlers;
using Rabbita.Core.FluentExtensions;
using Rabbita.InProc.FluentExtensions;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddLogging();

        services.AddControllers();
        services.AddScoped<AuthorizationApiFilter>();
        services.AddTransient<ErrorHandlingMiddleware>();

        services
            .AddControllers(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add<AuthorizationApiFilter>();
            })
            .AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<Startup>();
                cfg.LocalizationEnabled = false;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });


        #region Authentication

        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer("admin", options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Configuration["Auth:Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = Configuration["Auth:Jwt:Audience"],
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:SecretKey"])),
                ValidateIssuerSigningKey = true
            };
        });
        services.AddAuthorization(o =>
        {
            var adminPolicy = new AuthorizationPolicyBuilder("admin").RequireAuthenticatedUser();
            o.AddPolicy("admin", adminPolicy.Build());
        });

        #endregion

        services.AddSwagger();
        services.AddSingleton<IDateTimeGetter, DefaultDateTimeGetter>();

        services.AddSingleton<IMarkdown, Markdown>();

        #region auth

        services.AddDbContextPool<AuthenticationDbContext>(options =>
        {
            options.UseMySql(Configuration.GetConnectionString("Pets"),
                new MariaDbServerVersion(new Version(10, 5, 8)));
        });
        services.Configure<JwtAuthOptions>(Configuration.GetSection("Auth:Jwt"));
        services.AddScoped<IAccessTokenFactory, JwtAccessTokenFactory>();
        services.AddScoped<IRefreshTokenStore, RefreshTokenStore>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<AuthenticationService>();

        #endregion

        #region pet

        services.AddScoped<IPetCreateService, PetService>();
        services.AddScoped<IPetUpdateService, PetService>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddDbContextPool<PetDbContext>(options =>
        {
            options.UseMySql(Configuration.GetConnectionString("Pets"),
                new MariaDbServerVersion(new Version(10, 5, 8)));
        });

        #endregion

        services.AddScoped<IGridFSBucket>(_ =>
        {
            var connectionString = Configuration.GetConnectionString("MongoDb");
            var database = new MongoClient(connectionString).GetDatabase(MongoUrl.Create(connectionString).DatabaseName);
            return new GridFSBucket(database);
        });
        services.AddScoped<IDocumentRepository, MongoDocumentRepository>();

        #region DatabaseMigration

        services.AddDbContextPool<MigrateDbContext>(options =>
        {
            options.UseMySql(Configuration.GetConnectionString("Pets"),
                new MariaDbServerVersion(new Version(10, 5, 8)));
        });

        #endregion

        services.AddScoped<IDbConnection, MySqlConnection>(_ => new MySqlConnection(Configuration.GetConnectionString("Pets")));
        services.AddQueries();

        services.AddExceptionProcessor(registry => { registry.Register<ExceptionHandler>(); });

        services
            .AddRabbitaSerializer()
            /* .AddRabbitaPersistent(
                 options => { },
                 options =>
                 {
                     options.UseMySql(Configuration.GetConnectionString("Pets"),
                         new MariaDbServerVersion(new Version(10, 5, 8)));
                 }
             )*/;

        /* services.AddRabbitaDbPersistentMigrator(options =>
         {
             options.ConnectionString = Configuration.GetConnectionString("PetsMigration");
             options.DbCommandTimeout = 30;
         });  */
        services.AddEventBus();
        services.AddEventProcessor(registry => { });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });

        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseHttpsRedirection();
        app.UseAspNetCorePathBase();

        app.UseRouting();

        app.UseAuthorization();


        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "Pets Api"); });
        app.UseRewriter(new RewriteOptions().AddRedirect(@"^$", "swagger", (Int32)HttpStatusCode.Redirect));
    }
}