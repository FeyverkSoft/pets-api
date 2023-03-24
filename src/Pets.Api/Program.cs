namespace Pets.Api;

using DB.Migrations;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Serilog;

public static class Program
{
    public static void Main(String[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .RunDatabaseMigrations()
            //.MessagingDbInitialize()
            .Run();
    }

    public static IHostBuilder CreateHostBuilder(String[] args)
    {
        return Host.CreateDefaultBuilder(args)
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