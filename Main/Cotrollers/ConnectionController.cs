using ApiConnector;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace Main.Cotrollers
{
    public class ConnectionController:DbBase
    {
        private string serverName;
        private string userName;
        private string password;
        private string port;
        private ICommand applyCommand;
        private ICommand cancelCommand;
        private ICommand testCommand;
        private bool isValidConnection;
        private string conResultOne;
        private string conResultTwo;
        private Timer visibilityTimer;
        private IConController conControlBase;

        public ConnectionController()
        {
            IsVisible = true;
            ConnectionUpdate = false;
            applyCommand = new RelayCommand((p) => OnApply());
            cancelCommand = new RelayCommand((p) => OnCancel());
            testCommand = new RelayCommand((p)=>OnTestCon());
            conResultOne = "";
            conResultTwo = "";
            ConnectionUpdate = false;
            visibilityTimer = new Timer(3000);
            visibilityTimer.Elapsed += VisibilityTimer_Elapsed;
            visibilityTimer.Enabled = false;
        }

        public ConnectionController(IConController conChangeController):this()
        {
            this.conControlBase = conChangeController;
        }

        private void VisibilityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            visibilityTimer.Enabled = false;
            ConnectionUpdate = false;

        }

        private void OnTestCon()
        {
             DbConnector = new DbConnector(ConnectionString);
            if(DbConnector.CheckConnection())
            {
                ConResultOne = "Connection";
                ConResultTwo = "Succeeded";
            }
            else
            {
                ConResultOne = "Connection";
                ConResultTwo = "Failed";

            }
            ConnectionUpdate = true;
            visibilityTimer.Enabled = true;
        }

        private void OnCancel()
        {
            IsVisible = false;
        }

        private void OnApply()
        {
            DbConnector = new DbConnector(ConnectionString);
            if (!DbConnector.CheckConnection())
            {
                ConResultOne = "Connection";
                ConResultTwo = "Failed";
                ConnectionUpdate = true;
                visibilityTimer.Enabled = true;
            }
            else
            {
                IsVisible = false;
               conControlBase.ConnectionChanged();
            }
        }

        public string ServerName { get => serverName; set => serverName = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string Port { get => port; set => port = value; }
        public ICommand ApplyCommand { get => applyCommand; set => applyCommand = value; }
        public ICommand CancelCommand { get => cancelCommand; set => cancelCommand = value; }
        public string ConnectionString { get { return ConnectionStringConcat(); } }

        public ICommand TestCommand { get => testCommand; set => testCommand = value; }
        public bool ConnectionUpdate { get => isValidConnection; set { isValidConnection = value; NotifyPropertyChanged("ConnectionUpdate"); } }
        public string ConResultOne { get => conResultOne; set { conResultOne = value; NotifyPropertyChanged("ConResultOne"); } }
        public string ConResultTwo { get => conResultTwo; set { conResultTwo = value; NotifyPropertyChanged("ConResultTwo"); } }

        private string ConnectionStringConcat()
        {
            return "Server=" + serverName + ";UserName=" + userName + ";Password=" + password + ";Port=" + port;
        }
    }
}
