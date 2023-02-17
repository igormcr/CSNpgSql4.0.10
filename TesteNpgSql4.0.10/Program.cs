using System;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Npgsql;
using TesteNpgSql4._0._10.Model;

namespace TesteNpgSql4._0._10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando:");
            InsertQuery();
            SelectQuery();
        }
        public static Task InsertQuery()
        {
            QuestDBConnectionClass.Connection_Query dataSource = new QuestDBConnectionClass.Connection_Query();
            string localdatestring = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffff");
            var Query = "INSERT INTO TestPartition VALUES (to_timestamp('" + localdatestring + "', 'yyyy-MM-ddTHH:mm:ss.SSSUUUN')" + "," + "'" + "ssadgfrhfhgjtyktyu" + "'" + "," + GenerateRandom().ToString() + ")";
            dataSource.OpenConection();
            dataSource.ExecuteQueries(Query);
            dataSource.CloseConnection();
            return null;
        }
        public static string SelectQuery()
        {
            QuestDBConnectionClass.Connection_Query dataSource = new QuestDBConnectionClass.Connection_Query();
            var selectQuery = "SELECT * FROM 'TestPartition' ORDER BY ts DESC LIMIT 15";
            dataSource.OpenConection();
            Console.WriteLine("Selecionando Tabela de Teste");

            using (var cmd = dataSource.DataReader(selectQuery))
            {
                using (var reader = cmd)
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetTimeStamp(0));
                    }
            }
            dataSource.CloseConnection();

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
