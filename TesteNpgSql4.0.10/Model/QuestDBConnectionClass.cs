using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace TesteNpgSql4._0._10.Model
{
    class QuestDBConnectionClass
    {
        public class Connection_Query
        {

            static string username = "admin";
            static string password = "quest";
            static string database = "qdb";
            static int port = 8812;
            string ConnectionString = $@"host=192.168.15.121;port={port};username={username};password={password};database={database};ServerCompatibilityMode=NoTypeLoading;";
            NpgsqlConnection con;

            public void OpenConection()
            {
                con = new NpgsqlConnection(ConnectionString);
                con.Open();
            }

            public void CloseConnection()
            {
                con.Close();
            }


            public void ExecuteQueries(string Query_)
            {
                NpgsqlCommand cmd = new NpgsqlCommand(Query_, con);
                cmd.Parameters.AddWithValue(1);
                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Query Executada");
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message,"Query não foi executada");  
                }    
                
            }


            public NpgsqlDataReader DataReader(string Query_)
            {
                NpgsqlCommand cmd = new NpgsqlCommand(Query_, con);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                try
                {
                    Console.WriteLine(dr.ToString());
                } catch (Exception ex) { Console.WriteLine(ex.Message); }  
                return dr;
            }

        }
    }
}
