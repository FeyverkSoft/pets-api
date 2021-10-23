using System;
using System.Data;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

using Asp.Core.FluentExtensions;

using Core;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using MySql.Data.MySqlClient;

using Rabbita.Core.DafaultHandlers;
using Rabbita.Core.FluentExtensions;
using Rabbita.Entity.MariaDbTarget;
using Rabbita.InProc.FluentExtensions;

using Pets.Api.Authorization;
using Pets.Api.Extensions;
using Pets.Api.Middlewares;

using Query.Core.FluentExtensions;

using MongoDB.Driver.GridFS;
using MongoDB.Driver;

using Pets.Domain.Authentication;
using Pets.Domain.Documents;
using Pets.Infrastructure.Authentication;
using Pets.Infrastructure.FileStoreService;
using Pets.Infrastructure.Markdown;

using Rabbita.Entity.FluentExtensions;

namespace Pets.Api
{
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
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


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
                    ValidateIssuerSigningKey = true,
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

            services.AddDbContextPool<AuthenticationDbContext>(options => { options.UseMySql(Configuration.GetConnectionString("Pets")); });
            services.Configure<JwtAuthOptions>(Configuration.GetSection("Auth:Jwt"));
            services.AddScoped<IAccessTokenFactory, JwtAccessTokenFactory>();
            services.AddScoped<IRefreshTokenStore, RefreshTokenStore>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<AuthenticationService>();

            #endregion

            #region pet

            services.AddScoped<Domain.Pet.IPetCreateService, Domain.Pet.PetService>();
            services.AddScoped<Domain.Pet.IPetUpdateService, Domain.Pet.PetService>();
            services.AddScoped<Domain.Pet.IPetRepository, Infrastructure.Pet.PetRepository>();
            services.AddDbContextPool<Infrastructure.Pet.PetDbContext>(options => { options.UseMySql(Configuration.GetConnectionString("Pets")); });

            #endregion

            services.AddScoped<IGridFSBucket>(_ =>
            {
                var connectionString = Configuration.GetConnectionString("MongoDb");
                var database = new MongoClient(connectionString).GetDatabase(MongoUrl.Create(connectionString).DatabaseName);
                return new GridFSBucket(database);
            });
            services.AddScoped<IDocumentRepository, MongoDocumentRepository>();

            #region DatabaseMigration

            services.AddDbContextPool<Pets.DB.Migrations.MigrateDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("PetsMigration"));
            });

            #endregion

            services.AddScoped<IDbConnection, MySqlConnection>(_ => new MySqlConnection(Configuration.GetConnectionString("Pets")));
            services.RegQueryProcessor(registry =>
            {
                registry.Register<Queries.Infrastructure.Pets.PetsQueryHandler>();
                registry.Register<Queries.Infrastructure.Pages.PagesQueryHandler>();
                registry.Register<Queries.Infrastructure.Organisation.DocumentsQueryHandler>();
                registry.Register<Queries.Infrastructure.Documents.DocumentsQueryHandler>();
                registry.Register<Queries.Infrastructure.News.PetsQueryHandler>();
                registry.Register<Queries.Infrastructure.Search.SearchQueryHandler>();
            });

            services.AddExceptionProcessor(registry => { registry.Register<ExceptionHandler>(); });

            services
                .AddRabbitaSerializer() 
                .AddRabbitaPersistent(
                    options => { },
                    options => { options.UseMySql(Configuration.GetConnectionString("Pets")); }
                );

            services.AddRabbitaDbPersistentMigrator(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("PetsMigration");
                options.DbCommandTimeout = 30;
            });
            services.AddEventBus();
            services.AddEventProcessor(registry => { });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
            app.UseRewriter(new RewriteOptions().AddRedirect(@"^$", "swagger", (Int32) HttpStatusCode.Redirect));
        }
    }
}