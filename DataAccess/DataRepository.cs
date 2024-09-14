
using DataCollector.Model;
using DataCollector.Service;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace DataCollector.DataAccess
{
    public class DataRepository: IDisposable
    {
        private readonly SqliteConnection _connection;
        private const int MAX_DB_CONNECT_RETRIES = 5;
        private const int RETRY_DB_CONNECT_DELAY = 2000;

        public DataRepository()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection"));

            OpenConnection();
            CreateTableIfNotExist();
        }

        private void OpenConnection()
        {
            int retries = 0;
            while (_connection.State != System.Data.ConnectionState.Open && retries < MAX_DB_CONNECT_RETRIES)
            {
                try
                {
                    _connection.Open();
                    ConsoleLogService.LogSuccess("Database connection opened");
                    ConsoleLogService.LogInfo("Database connection state: " + _connection.State.ToString());
                    return;
                }
                catch (Exception ex)
                {
                    retries++;
                    ConsoleLogService.LogError($"Error opening connection. Retry {retries}/{MAX_DB_CONNECT_RETRIES} \n Exception: {ex.Message}");
                    if (retries >= MAX_DB_CONNECT_RETRIES)
                    {
                        ConsoleLogService.LogError("Failed to open database connection after multiple retries.");
                    }
                    Thread.Sleep(RETRY_DB_CONNECT_DELAY);
                }
            }
        }

        private void CreateTableIfNotExist()
        {
            using (var command = _connection.CreateCommand())
            {
                // Create table if it does not exist
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Data (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Temperature REAL,
                        Temperature2 REAL,
                        Humidity REAL,
                        Pressure REAL,
                        Timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                        );
                    ";
                command.ExecuteNonQuery();
            }

            // Check if the table exists
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"
                    SELECT COUNT(*) FROM sqlite_master
                    WHERE type='table' AND name='Data';
                    ";

                var count = (long)command.ExecuteScalar();
                if (count == 0)
                {
                    ConsoleLogService.LogError("Failed to create table Data");
                }
            }
        }


        public void InsertData(DataVO data)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Data (Temperature, Temperature2, Humidity, Pressure, Timestamp)
                VALUES (@Temperature, @Temperature2, @Humidity, @Pressure, @Timestamp);
            ";
            command.Parameters.AddWithValue("@Temperature", data.Temperature.DegreesCelsius);
            command.Parameters.AddWithValue("@Temperature2", data.Temperature2.DegreesCelsius);
            command.Parameters.AddWithValue("@Humidity", data.Humidity.Percent);
            command.Parameters.AddWithValue("@Pressure", data.Pressure.Bars);
            command.Parameters.AddWithValue("@Timestamp", data.Timestamp.ToUniversalTime());

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                ConsoleLogService.LogInfo("Environment data collected");
            } else
            {
                ConsoleLogService.LogError("Failed to save environment data");
            }
        }

        public void PrintData()
        {
            using var command = _connection.CreateCommand();
            command.CommandText = @"
                SELECT * FROM Data;
            ";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Temperature: {reader.GetDouble(1)}°C");
                Console.WriteLine($"Temperature2: {reader.GetDouble(2)}°C");
                Console.WriteLine($"Humidity: {reader.GetDouble(3)}%");
                Console.WriteLine($"Pressure: {reader.GetDouble(4)} bars");
                Console.WriteLine($"Timestamp: {reader.GetDateTime(5)}");
            }
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }

    }
}
