namespace Pets.Api;

using System.Data;
using System.Text;
using System.Text.Json.Serialization;

using Authorization;

using Core;
using Core.Mediatr;

using DB.Migrations;

using Domain.Authentication;
using Domain.Documents;
using Domain.News;
using Domain.Organisation;
using Domain.Pet;

using Extensions;

using FluentValidation.AspNetCore;

using Infrastructure.Authentication;
using Infrastructure.FileStoreService;
using Infrastructure.Markdown;
using Infrastructure.News;
using Infrastructure.Organisation;
using Infrastructure.Pet;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Middlewares;

using MongoDB.Driver;
using MongoDB.Driver.GridFS;

using MySqlConnector;

using Queries.Infrastructure;

using Rabbita.Core.DefaultHandlers;
using Rabbita.Core.FluentExtensions;
using Rabbita.Entity.FluentExtensions;
using Rabbita.Entity.MariaDbTarget;
using Rabbita.InProc.FluentExtensions;
using Rabbita.InProc.FluentExtensions.Event;

public static class Startup
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddLogging();

        services.AddControllers();
        services.AddScoped<AuthorizationApiFilter>();
        services.AddTransient<ErrorHandlingMiddleware>();

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<Program>();
        
        services
            .AddControllers(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add<AuthorizationApiFilter>();
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
                ValidIssuer = configuration["Auth:Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["Auth:Jwt:Audience"],
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Jwt:SecretKey"])),
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
            options.UseMySql(configuration.GetConnectionString("Pets"),
                new MariaDbServerVersion(new Version(10, 5, 8)));
        });
        services.Configure<JwtAuthOptions>(configuration.GetSection("Auth:Jwt"));
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
            options.UseMySql(configuration.GetConnectionString("Pets"),
                new MariaDbServerVersion(new Version(10, 5, 8)));
        });

        #endregion

        #region org
        
        services.AddScoped<IOrganisationGetter, OrganisationRepository>();
        services.AddDbContextPool<OrganisationDbContext>(options =>
        {
            options.UseMySql(configuration.GetConnectionString("Pets"),
                new MariaDbServerVersion(new Version(10, 5, 8)));
        });

        #endregion
        
        #region news

        services.AddScoped<INewsCreateService, NewsService>();
        services.AddScoped<IPetGetter, NewsRepository>();
        services.AddScoped<INewsRepository, NewsRepository>();
        services.AddDbContextPool<NewsDbContext>(options =>
        {
            options.UseMySql(configuration.GetConnectionString("Pets"),
                new MariaDbServerVersion(new Version(10, 5, 8)));
        });

        #endregion

        services.AddScoped<IGridFSBucket>(_ =>
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var database = new MongoClient(connectionString).GetDatabase(MongoUrl.Create(connectionString).DatabaseName);
            return new GridFSBucket(database);
        });
        services.AddScoped<IDocumentRepository, MongoDocumentRepository>();

        #region DatabaseMigration

        services.AddDbContextPool<MigrateDbContext>(options =>
        {
            options.UseMySql(configuration.GetConnectionString("Pets"),
                new MariaDbServerVersion(new Version(10, 5, 8)));
        });

        #endregion

        services.AddScoped<IDbConnection, MySqlConnection>(_ => new MySqlConnection(configuration.GetConnectionString("Pets")));
        services.AddMediatorCommandDedublicateBehaviour();
        services.AddQueries();

        services
            .AddRabbitaDefaultSerializer()
            .AddRabbitaPersistent(
                 options => { },
                 options =>
                 {
                     options.UseMySql(configuration.GetConnectionString("Pets"),
                         new MariaDbServerVersion(new Version(10, 5, 8)));
                 }
             ); 

         services.AddRabbitaDbPersistentMigrator(options =>
         {
             options.ConnectionString = configuration.GetConnectionString("PetsMigration");
             options.DbCommandTimeout = 30;
         });  
        services.AddEventBus();
        services.AddEventProcessor(registry => { });
        return services;
    }
}