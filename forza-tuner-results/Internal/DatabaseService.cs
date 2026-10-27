using DbUp;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using Windows.Storage;


namespace ForzaTunerResults.Internal
{
    public sealed class DatabaseService
    {
        private static readonly ILogger<DatabaseService> logger = App.CreateLogger<DatabaseService>();
        private static readonly ILogger databaseLogger = App.CreateLogger("ForzaTunerResults.Database");

        private string? _connectionString;
        private bool _initialized = false;

        public bool IsInitialized { get => _initialized; }

        public void InitializeDb()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            logger.LogInformation("Application directory: {appDataDirectory}", localFolder);

            _connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = Path.Combine(localFolder.Path, "database.db"),
                ForeignKeys = true,
                Mode = SqliteOpenMode.ReadWriteCreate
            }.ToString();

            logger.LogInformation("Database connection string: {connectionString}", _connectionString);

            var upgrader = DeployChanges.To
               .SqliteDatabase(_connectionString)
               .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
               .LogTo(databaseLogger)
               .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                throw new InvalidOperationException("Database initialization failed.", result.Error);
            }
            _initialized = true;
        }

        public SqliteConnection OpenConnection()
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("Database is not initialized. Call InitializeDb() first.");
            }
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
