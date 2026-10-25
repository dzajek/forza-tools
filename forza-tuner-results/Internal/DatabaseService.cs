using Microsoft.Data.Sqlite;


namespace ForzaTunerResults.Internal
{
    public sealed class DatabaseService(string dbConnectionString)
    {
        private readonly string _connectionString = dbConnectionString;

        public SqliteConnection OpenConnection()
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
