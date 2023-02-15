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
            string ConnectionString = $@"host=localhost;port={port};username={username};password={password};database={database};ServerCompatibilityMode=NoTypeLoading;";
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
                cmd.ExecuteNonQuery();
            }


            public NpgsqlDataReader DataReader(string Query_)
            {
                NpgsqlCommand cmd = new NpgsqlCommand(Query_, con);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                return dr;
            }

        }
    }
}
