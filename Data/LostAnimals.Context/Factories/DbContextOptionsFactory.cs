using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Context;

public static class DbContextOptionsFactory
{
    private const string migrationProjectPrefix = "LostAnimals.Context.Migrations.";

    public static DbContextOptions<MainDbContext> Create(string connectionString, DbType dbType, bool detailedLogging  = false)
    {
        var builder = new DbContextOptionsBuilder<MainDbContext>();

        Configure(connectionString, dbType, detailedLogging).Invoke(builder);

        return builder.Options;
    }

    public static Action<DbContextOptionsBuilder> Configure(string connectionString, DbType dbType, bool detailedLogging = false)
    {
        return (builder) =>
        {
            switch (dbType)
            {
                case DbType.MSSQL:
                    builder.UseSqlServer(connectionString,
                        options => options
                        .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                            .MigrationsHistoryTable("_migrations")
                            .MigrationsAssembly($"{migrationProjectPrefix}{DbType.MSSQL}")
                            );
                    break;

                case DbType.PgSql:
                    builder.UseNpgsql(connectionString,
                        options => options
                        .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                            .MigrationsHistoryTable("_migrations")
                            .MigrationsAssembly($"{migrationProjectPrefix}{DbType.PgSql}")
                    );
                    break;
            }

            if (detailedLogging)
            {
                builder.EnableSensitiveDataLogging();
            }

            builder.UseLazyLoadingProxies(true);
        };
    }
}
