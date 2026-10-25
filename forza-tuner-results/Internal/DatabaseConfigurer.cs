using DbUp;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using Windows.Storage;

namespace ForzaTunerResults.Internal
{
    internal class DatabaseConfigurer
    {

        private static readonly ILogger<DatabaseConfigurer> logger = App.CreateLogger<DatabaseConfigurer>();
        private static readonly ILogger databaseLogger = App.CreateLogger("Database");

        public static string InitializeDatabase()
        {
            var appDataDirectory = Path.Combine(ApplicationData.Current.LocalFolder.Path, "ForzaTunerResults");
            Directory.CreateDirectory(appDataDirectory);
            logger.LogInformation("Application directory: {appDataDirectory}", Path.GetFullPath(appDataDirectory));

            var connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = Path.Combine(appDataDirectory, "database.db"),
                ForeignKeys = true,
                Mode = SqliteOpenMode.ReadWriteCreate
            }.ToString();

            logger.LogInformation("Database connection string: {connectionString}", connectionString);

            var upgrader = DeployChanges.To
                .SqliteDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogTo(databaseLogger)
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                throw new InvalidOperationException("Database initialization failed.", result.Error);
            }

            return connectionString;
        }

    }
}
