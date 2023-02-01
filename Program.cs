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
            DataContextEF entityFramework = new DataContextEF(config);

            DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

            // Console.WriteLine(rightNow.ToString());

            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 1499.00m,
                VideoCard = "RTX 2060"
            };

            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();

            // "@" allows multiple lines in a single string
            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('" + myComputer.Motherboard
                    + "','" + myComputer.HasWifi
                    + "','" + myComputer.HasLTE
                    + "','" + myComputer.ReleaseDate
                    + "','" + myComputer.Price
                    + "','" + myComputer.VideoCard
                 + "')";

            // Console.WriteLine(sql);

            // int result = dapper.ExecuteSqlWithRowCount(sql);
            bool result = dapper.ExecuteSql(sql);

            // Console.WriteLine(result);

            string sqlSelect = @"
            SELECT 
                Computer.ComputerId,
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
              FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            Console.WriteLine("ComputerId, Motherboard, HasWifi, HasLTE, ReleaseDate, Price, VideoCard");
            foreach (Computer singleComputer in computers)
            {
                Console.WriteLine("'" + singleComputer.ComputerId
                      + "','" + singleComputer.Motherboard
                      + "','" + singleComputer.HasWifi
                      + "','" + singleComputer.HasLTE
                      + "','" + singleComputer.ReleaseDate
                      + "','" + singleComputer.Price
                      + "','" + singleComputer.VideoCard
                   + "'");
            }

            IEnumerable<Computer>? computersEf = entityFramework.Computer?.ToList<Computer>();

            if (computersEf != null)
            {
                Console.WriteLine("ComputerId, Motherboard, HasWifi, HasLTE, ReleaseDate, Price, VideoCard");
                foreach (Computer singleComputer in computersEf)
                {
                    Console.WriteLine("'" + singleComputer.ComputerId
                          + "','" + singleComputer.HasWifi
                          + "','" + singleComputer.HasWifi
                          + "','" + singleComputer.HasLTE
                          + "','" + singleComputer.ReleaseDate
                          + "','" + singleComputer.Price
                          + "','" + singleComputer.VideoCard
                       + "'");
                }
            }
            // // wifi card broke . . .
            // myComputer.HasWifi = false;
            // System.Console.WriteLine(myComputer.ReleaseDate);
            // System.Console.WriteLine(myComputer.HasWifi);
            // System.Console.WriteLine(myComputer.VideoCard);
            // System.Console.WriteLine(myComputer.Motherboard);
        }
    }
}














