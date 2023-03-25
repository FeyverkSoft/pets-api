namespace Pets.DB.Migrations;

using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/// <summary>
///     Обособленный Сервис миграции базы данных
///     Создаёт таблицы, базу.
///     Если необходимо накатывает последовательно миграции
///     В будущем может быть вынесен в отдельный солюшен.
/// </summary>
public static class MigrationsRunner
{
    /// <summary>
    ///     Колличество попыток миграции.
    ///     По истечению которых мы считаем что миграция не прошла
    ///     Необходимо из-за того что старт контейнера с базой не обзначает того что база та уже запустилась
    ///     база в контейнере может долго подниматься
    /// </summary>
    private const Int32 RetryCount = 10;

    public static IHost RunDatabaseMigrations(this IHost host)
    {
        var attempt = 0;
        using var serviceScope = host.Services.CreateScope();
        var loggerFactory = serviceScope.ServiceProvider.GetService<ILoggerFactory>();

        var logger = loggerFactory.CreateLogger("DatabaseMigrations");
        logger.LogInformation("Starting migrations...");

        var context = serviceScope.ServiceProvider.GetService<MigrateDbContext>();
        do
        {
            attempt++;
            try
            {
                context.Database.Migrate();
                logger.LogInformation("Migrations ended. Awaiting cancel...");
                return host;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Migrations were fail");
                if (attempt >= RetryCount)
                    throw;

                logger.LogError(ex, $"Migrations sleep for {attempt * 1000}");
                Task.Delay(attempt * 1000);
            }
        } while (attempt < RetryCount);

        return host;
    }
}