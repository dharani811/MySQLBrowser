using ApiConnector;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Main.Cotrollers
{
   public class DataBrowserController:DbBase,IConController, IDocument
    {

        DatabaseController dbController;
        ConnectionController connectionController;
        private ICommand connect;


        public DataBrowserController()
        {
            connect = new RelayCommand((p) => OnConnectClick());

            dbController = new DatabaseController();

            OnConnectClick();

        }
  
        private void OnConnectClick()
        {
            if (connectionController == null)
            {
                connectionController = new ConnectionController(this);
            }
            connectionController.IsVisible = true;
            if (dbController != null)
            {
                dbController.IsVisible = false;
                dbController.TableController.IsVisible = false;
                dbController.TableController.GridController.IsVisible = false;
            }
        }

        void IConController.ConnectionChanged()
        {
            DbConnector = new DbConnector(connectionController.ConnectionString);
            dbController = new DatabaseController(DbConnector);
            NotifyPropertyChanged("DbController");
            dbController.IsVisible = true;
            dbController.TableController.IsVisible = true;
            dbController.TableController.GridController.IsVisible = true;
            DbController.PopulateNodes(DbConnector.GetDatabaseList(), false);

        }

        public DatabaseController DbController { get => dbController; set => dbController = value; }
        public ConnectionController ConnectionController { get => connectionController; set => connectionController = value; }
        public ICommand Connect { get => connect; set => connect = value; }

    }
}
