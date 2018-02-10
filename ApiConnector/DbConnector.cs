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

         public DbConnector()
        {
            connectionString = "";

        }
        public DbConnector(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private MySqlConnection CreateAndOpenCon()
        {
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public IEnumerable<string> GetDatabaseList()
        {
            var connection = CreateAndOpenCon();
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
            var connection = CreateAndOpenCon();
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            adapter.Fill(dataTable);
            adapter.Dispose();
            connection.Close();

            return dataTable;
        }

        public IEnumerable<string> GetTables(string database)
        {
            var connection = CreateAndOpenCon();
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
