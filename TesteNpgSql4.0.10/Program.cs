using System;
using System.Threading.Tasks;
using Npgsql;
using TesteNpgSql4._0._10.Model;

namespace TesteNpgSql4._0._10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConnectionUsingClass();
        }
        public static Task ConnectionUsingClass()
        {
            QuestDBConnectionClass.Connection_Query dataSource = new QuestDBConnectionClass.Connection_Query();
            string localdatestring = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffff");
            var Query = "INSERT INTO TestPartition VALUES (to_timestamp('" + localdatestring + "', 'yyyy-MM-ddTHH:mm:ss.SSSUUUN')" + "," + "'" + "ssadgfrhfhgjtyktyu" + "'" + "," + GenerateRandom().ToString() + ")";
            dataSource.OpenConection();
            dataSource.ExecuteQueries(Query);
            dataSource.CloseConnection();
            return null;
        }
        public static Task InsertData_QuestDBWireProtocol()
        {
            Console.WriteLine("Teste QuestDB Wire Protocol c/ Insert");
            string username = "admin";
            string password = "quest";
            string database = "qdb";
            int port = 8812;
            var connectionString = $@"host=localhost;port={port};username={username};password={password};database={database};ServerCompatibilityMode=NoTypeLoading;";
            NpgsqlConnection dataSource = new NpgsqlConnection(connectionString);

            DateTime inicio = DateTime.Now;
            Console.WriteLine("Hora inicio: " + inicio.ToString());

            string localdatestring = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffff");
            using (var cmd = dataSource.CreateCommand())
            {
                //dataSource.Database.
                //dataSource.CreateCommand().CommandText = "INSERT INTO TestPartition VALUES (to_timestamp('" + localdatestring + "', 'yyyy-MM-ddTHH:mm:ss.SSSUUUN')" + "," + "'" + "ssadgfrhfhgjtyktyu" + "'" + "," + GenerateRandom().ToString() + ")";
                cmd.CommandText = "INSERT INTO TestPartition VALUES (to_timestamp('" + localdatestring + "', 'yyyy-MM-ddTHH:mm:ss.SSSUUUN')" + "," + "'" + "ssadgfrhfhgjtyktyu" + "'" + "," + GenerateRandom().ToString() + ")";
                dataSource.Open();
                //dataSource.CreateBatch();
                cmd.Parameters.AddWithValue(1);

                cmd.ExecuteNonQuery();
            }

            DateTime final = DateTime.Now;
            Console.WriteLine("Hora fim: " + final.ToString());

            dataSource.Dispose();
            return null;
        }
        private static float GenerateRandom()
        {
            Random r = new Random();
            int rInt = r.Next(0, 50);

            var rand = new Random();
            double min = 1;
            double max = 100;
            double range = max - min;

            double sample2 = rand.NextDouble();
            double scaled2 = (sample2 * range) + min;
            //return (float)scaled2 + rInt
            return rInt;

        }
    }
}
