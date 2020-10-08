using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Rabbita.Entity.Migration;
using Serilog;
using Pets.DB.Migrations;

namespace Pets.Api
{
    public static class Program
    {
        public static void Main(String[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .RunDatabaseMigrations()
                .MessagingDbInitialize()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(String[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .UseSerilog((whbc, conf) =>
                            conf
                                .ReadFrom
                                .Configuration(whbc.Configuration)
                                .Enrich.FromLogContext());
                });
    }
}
