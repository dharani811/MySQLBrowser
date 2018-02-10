using ApiConnector;
using Interpreters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Main.Cotrollers
{
   public  class MainController:UINotify
    {

        DbConnector dbConnector;
        DatabaseController dbController;
        ConnectionController connectionController;
        private ICommand connect;
        private string currentStatus;

        public static MainController This = new MainController();
        public MainController()
        {
            connect = new RelayCommand((p) => OnConnectClick());
           
            dbController = new DatabaseController();

            OnConnectClick();
            currentStatus = "Idle";
        }

        public void MainController_ConnectionChanged(object sender, EventArgs e)
        {
            dbConnector = new DbConnector(connectionController.ConnectionString);
            dbController = new DatabaseController(dbConnector);
           NotifyPropertyChanged("DbController");
            dbController.IsVisible = true;
            dbController.TableController.IsVisible = true;
            dbController.TableController.GridController.IsVisible = true;
            DbController.PopulateNodes(dbConnector.GetDatabaseList(), false);

        }

        private void OnConnectClick()
        {
            if (connectionController == null)
            {
                connectionController = new ConnectionController();
            }
            connectionController.IsVisible = true;
            if (dbController!=null)
            {
                dbController.IsVisible = false;
                dbController.TableController.IsVisible = false;
                dbController.TableController.GridController.IsVisible = false;
            }
        }

        public DatabaseController DbController { get => dbController; set => dbController = value; }
        public ConnectionController ConnectionController { get => connectionController; set => connectionController = value; }
        public ICommand Connect { get => connect; set => connect = value; }
        public String CurrentStatus
        {
            get => currentStatus;
            set
            {
                currentStatus = value;
                NotifyPropertyChanged("CurrentStatus");
            }
        }
        public void LoadSamples()
        {
          DbController.PopulateNodes(dbConnector.GetDatabaseList(),false);
            
        }
    }
}
