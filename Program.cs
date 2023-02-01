using System;
using System.Linq;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
// Add folder name "Models" to ModelsTutorial namespace with import statement.
using ModelsTutorial.Models;
// Import data folder
using ModelsTutorial.Data;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ModelsTutorial
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();

            DataContextDapper dapper = new DataContextDapper(config);
            // DataContextEF entityFramework = new DataContextEF(config);

            // DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

            // "@" allows multiple lines in a single string. ie. "\n" at beginning isn't necessary,
            // output would be the same if INSERT... was entered on next line.
            // string sql = @"INSERT INTO TutorialAppSchema.Computer (
            //     Motherboard,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            // ) VALUES ('" + myComputer.Motherboard
            //         + "','" + myComputer.HasWifi
            //         + "','" + myComputer.HasLTE
            //         + "','" + myComputer.ReleaseDate
            //         + "','" + myComputer.Price
            //         + "','" + myComputer.VideoCard
            //      + "')";

            // void File.WriteAllText(string path, string? contents) (+ 1 overload)
            // Creates a new file, writes the specified string to the file, and then closes the file\. If the target file already exists, it is overwritten\.
            // File.WriteAllText("log.txt", $"\n" + sql + "\n");

            // class System.IO.StreamWriter
            // Implements a `TextWriter` for writing characters to a stream in a particular encoding\.
            // using StreamWriter openFile = new("log.txt", append: true);

            // openFile.WriteLine($"\n" + sql + "\n");

            // openFile.Close();

            string fileText = File.ReadAllText("log.txt");

            string computersJson = File.ReadAllText("Computers.json");

            // Console.WriteLine(computersJson);
            // .NET built in json serializer
            // class System.Text.Json.JsonSerializer
            // Provides functionality to serialize objects or value types to JSON and to deserialize JSON into objects or value types\.
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // class Newtonsoft.Json.JsonConvert
            // Provides methods for converting between \.NET types and JSON types\.
            IEnumerable<Computer>? computersNewtonSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);

            if (computersNewtonSoft != null)
            {
                foreach (Computer computer in computersNewtonSoft)
                {
                    // Console.WriteLine(computer.Motherboard);
                    string sql = @"INSERT INTO TutorialAppSchema.Computer (
                        Motherboard,
                        HasWifi,
                        HasLTE,
                        ReleaseDate,
                        Price,
                        VideoCard
                    ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
                            + "','" + computer.HasWifi
                            + "','" + computer.HasLTE
                            + "','" + computer.ReleaseDate
                            + "','" + computer.Price
                            + "','" + EscapeSingleQuote(computer.VideoCard)
                         + "')";

                    dapper.ExecuteSql(sql);
                }
            }

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);

            File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);

            string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);

            File.WriteAllText("computersCopySystem.txt", computersCopySystem);
        }

        // SQL doesn't accept single quotes in strings
        static string EscapeSingleQuote(string input)
        {
            string output = input.Replace("'", "''");
            return output;
        }
    }
}














