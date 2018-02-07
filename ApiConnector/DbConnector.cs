using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ApiConnector
{
    public  class DbConnector
    {
        private string connectionString;
        private MySqlConnection connection; 

         public DbConnector()
        {
            connectionString = "";

        }
        public DbConnector(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new MySqlConnection(connectionString);
        }

        public IEnumerable<string> GetDatabaseList()
        {
            connection.Open();
            DataTable dt=new DataTable();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter("show databases", connection);
            dataAdapter.Fill(dt);
            dataAdapter.Dispose();
            connection.Close();
            foreach (DataRow item in dt.Rows)
            {
                yield return item.ItemArray[0].ToString();
            }
            
        }

        public DataTable ExecuteQuery(string query)
        {
            connection.Open();
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            adapter.Fill(dataTable);
            adapter.Dispose();
            connection.Close();

            return dataTable;
        }

        public IEnumerable<string> GetTables(string database)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand("use "+ database, connection);
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter("show tables", connection);
            dataAdapter.Fill(dt);
            dataAdapter.Dispose();
            connection.Close();
            foreach (DataRow item in dt.Rows)
            {
                yield return item.ItemArray[0].ToString();
            }

        }

         
    }
}
