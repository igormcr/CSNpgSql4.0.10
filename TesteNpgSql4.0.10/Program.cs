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
            Console.WriteLine("Realizando limpeza da tabela");
            PartitionByQuery();
            int repeat = 0;
            Console.WriteLine("Inserindo 5 entradas na tabela");
            while (repeat < 1000) {
                InsertQuery();
                repeat++;
            }
            Console.WriteLine("Selecionando as últimas 15 entradas na tabela");
            SelectQuery();
        }
        public static string PartitionByQuery()
        {
            QuestDBConnectionClass.Connection_Query dataSource = new QuestDBConnectionClass.Connection_Query();
            string localdatestring = DateTime.Now.AddSeconds(-1).ToString("yyyy-MM-ddTHH:mm:ss");
            var AlterQuery = "ALTER TABLE 'TestPartition' DROP PARTITION WHERE ts < to_timestamp('" + localdatestring + "', 'yyyy-MM-ddTHH:mm:ss');";
            dataSource.OpenConection();
            Console.WriteLine("Realisando Limpeza Com Partition By:");
            Console.WriteLine(AlterQuery);
            dataSource.ExecuteQueries(AlterQuery);

            dataSource.CloseConnection();

            return null;
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
            using (var cmd = dataSource.DataReader(selectQuery))
            {
                try
                {
                    using (var reader = cmd)
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetTimeStamp(0).ToString(), reader.GetString(1), reader.GetDouble(2));
                        }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());   
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
