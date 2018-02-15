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
        private string currentDb;
        private string warnings;
        private string message;

        public string CurrentDb { get => currentDb; set => currentDb = value; }
        public string Warnings { get => warnings; set {
                warnings = value;
                if(WarningOccured!=null)
                {
                    WarningOccured(this, null);
                }
            } }
        public string Message { get => message; set { message = value;
                if(ErrorOccured!=null)
                {
                    ErrorOccured(this, null);
                }
            } }
        public event EventHandler WarningOccured;
        public event EventHandler ErrorOccured;

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
            connection.InfoMessage += Connection_InfoMessage;
            DataTable dt =new DataTable();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter("show databases", connection);
            dataAdapter.Fill(dt);
            dataAdapter.Dispose();
            connection.Close();
            foreach (DataRow item in dt.Rows)
            {
                yield return item.ItemArray[0].ToString();
            }
            
        }

        private void Connection_InfoMessage(object sender, MySqlInfoMessageEventArgs args)
        {
            // throw new NotImplementedException();
            warnings = "";
            args.errors.ToList().ForEach(error=>warnings+="\nErrorCode == "+error.Code+" ; ErrorLevel =="+error.Level+"\n Error: \n"+error.Message);
            Warnings = warnings;
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();

            try
            {
                var connection = CreateAndOpenCon();
                connection.InfoMessage += Connection_InfoMessage;
                SetDatabase(connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                adapter.Fill(dataTable);
                adapter.Dispose();
                connection.Close();

            }
            catch (Exception err)
            {
                Message = err.Message;
            }         

            return dataTable;
        }

        public IEnumerable<string> GetTables(string database)
        {
            var connection = CreateAndOpenCon();
            SetDatabase(connection);
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

        private  void SetDatabase(MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand("use " + CurrentDb, connection);
            command.ExecuteNonQuery();
        }

        public bool CheckConnection()
        {
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

            }
            catch (Exception err)
            {
                Message = err.Message;

            }
            return connection.State==ConnectionState.Open?true:false ;
        }

    }
}
