using System.Net;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Pets.Api;
using Pets.Api.AspCore.FluentExtensions;
using Pets.Api.Middlewares;
using Pets.DB.Migrations;

using Rabbita.Entity.Migration;

var builder = WebApplication.CreateBuilder();
ConfigWebHost(builder);
builder.Services.ConfigureServices(builder.Configuration);
var app = builder.Build();
Configure(app, builder.Environment);
app.RunDatabaseMigrations()
.MessagingDbInitialize();


app.Run();

static void ConfigWebHost(WebApplicationBuilder builder)
{
    builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true)
        .AddJsonFile("appsettings.Development.json", true)
        .AddJsonFile("hosting.json", true)
        .AddEnvironmentVariables();

    builder.WebHost.ConfigureKestrel((context, options) => { options.AddServerHeader = false; })
        .UseUrls(builder.Configuration["server:urls"] ?? "http://*:80");
}

static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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