// Add the necessary references at the top of the script
#r "nuget: SQLitePCLRaw.bundle_e_sqlite3, 2.1.8"
#r "nuget: Microsoft.Data.Sqlite, 7.0.0"

using System;
using Microsoft.Data.Sqlite;

// Initialize SQLitePCL
SQLitePCL.Batteries_V2.Init();

try
{
    var connectionString = "Data Source=building_sardinia.db";
    using (var connection = new SqliteConnection(connectionString))
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT name FROM sqlite_master WHERE type='table';";
        using (var reader = command.ExecuteReader())
        {
            Console.WriteLine("Tables in the database:");
            while (reader.Read())
            {
                Console.WriteLine($"- {reader.GetString(0)}");
            }
        }
    }
    Console.WriteLine("Database connection successful.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
