using System;
using Microsoft.Data.Sqlite;

namespace sqliteCase
{
    class Program
    {
        static void Main(string[] args)
        {
            var uid = 1;
            using (var connection = new SqliteConnection("Data Source=test.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                        SELECT username
                        FROM userinfo
                        WHERE uid = $uid
                    ";
                command.Parameters.AddWithValue("$uid", uid);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);

                        Console.WriteLine($"Hello, {name}!");
                    }
                }
            }
            Console.WriteLine("Hello World!");
        }
    }
}