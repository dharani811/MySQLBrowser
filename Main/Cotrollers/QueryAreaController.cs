using ApiConnector;
using Contracts;
using Main.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Main.Cotrollers
{
    public class QueryAreaController:DbBase, IConController,IDocument
    {
        ConnectionController connectionController;
        private string queryString;
        private ICommand cancel;
        private ICommand clear;
        private ICommand execute;
        private ICommand connect;
        private DataTable currentData;
        private bool isClearEnabled;
        private bool isExecuteEnabled;
        private bool isCancelEnabled;
        private bool isQueryAreaVisible;
        private ObservableCollection<string> dataBaseList;
        private string currentDataBase;

        public QueryAreaController()
        {
            connect = new RelayCommand((p)=>OnConnectClick());
            clear = new RelayCommand((p) => OnClearClick());
            cancel = new RelayCommand((p) => OnCancelClick());
            execute = new RelayCommand((p) => OnExecuteClick());
            isCancelEnabled = false;
            isClearEnabled = true;
            isExecuteEnabled = true;
            isQueryAreaVisible = false;
            dataBaseList = new ObservableCollection<string>();
            OnConnectClick();

        }

        private void OnExecuteClick()
        {
            WorkUnit = new UnitOfWork(() => CurrentData = DbConnector.ExecuteQuery(queryString), true);
            WorkUnit.StatusChanged += WorkUnit_StatusChanged;
            WorkUnit.DoWork();
        }

        public override void WorkUnit_StatusChanged(object sender, EventArgs e)
        {
            base.WorkUnit_StatusChanged(sender, e);
            if(WorkUnit.StatusOfWork==Status.Completed || WorkUnit.StatusOfWork==Status.Stopped)
            {
                IsClearEnabled = true;
                IsExecuteEnabled = true;
                IsCancelEnabled = false;
            }
            else if(WorkUnit.StatusOfWork==Status.Running|| WorkUnit.StatusOfWork==Status.Started)
            {
                IsClearEnabled = false;
                IsExecuteEnabled = false;
                IsCancelEnabled = true;
            }

        }
        private void OnClearClick()
        {
            QueryString = "";
        }

        private void OnCancelClick()
        {
            if(WorkUnit != null)
            {
                if(WorkUnit.StatusOfWork==Status.Started|| WorkUnit.StatusOfWork==Status.Running)
                {
                    WorkUnit.CancelWork();
                }
            }
        }

        private void OnConnectClick()
        {
            if (ConnectionController == null)
            {
                ConnectionController = new ConnectionController(this);
            }
            ConnectionController.IsVisible = true;
        }

        public void ConnectionChanged()
        {
            DbConnector = new DbConnector(ConnectionController.ConnectionString);
            SubscribeEvents();
            WorkUnit = new UnitOfWork(() => DbConnector.GetDatabaseList().ToList().ForEach(db=>System.Windows.Application.Current.Dispatcher.Invoke(()=> dataBaseList.Add(db))), true);
            WorkUnit.StatusChanged += WorkUnit_StatusChanged;
            WorkUnit.DoWork();

            IsQueryAreaVisible = true;
        }

        public ICommand Cancel { get => cancel; set => cancel = value; }
        public ICommand Clear { get => clear; set => clear = value; }
        public ICommand Execute { get => execute; set => execute = value; }
        public ICommand Connect { get => connect; set => connect = value; }
        public string QueryString { get => queryString; set { queryString = value; NotifyPropertyChanged("QueryString"); } }
        public DataTable CurrentData { get => currentData; set { currentData = value; NotifyPropertyChanged("CurrentData"); } }

        public bool IsClearEnabled { get => isClearEnabled; set { isClearEnabled = value; NotifyPropertyChanged("IsClearEnabled"); } }
        public bool IsExecuteEnabled { get => isExecuteEnabled; set { isExecuteEnabled = value; NotifyPropertyChanged("IsExecuteEnabled"); } }
        public bool IsCancelEnabled { get => isCancelEnabled; set { isCancelEnabled = value; NotifyPropertyChanged("IsCancelEnabled"); } }

        public ConnectionController ConnectionController { get => connectionController; set => connectionController = value; }
        public bool IsQueryAreaVisible { get => isQueryAreaVisible; set { isQueryAreaVisible = value; NotifyPropertyChanged("IsQueryAreaVisible"); } }

        public ObservableCollection<string> DataBaseList { get => dataBaseList; set => dataBaseList = value; }
        public string CurrentDataBase { get => currentDataBase;

            set { currentDataBase = value;
                if (DbConnector != null)
                { DbConnector.CurrentDb = currentDataBase; }
                NotifyPropertyChanged("CurrentDataBase"); }
        }
    }
}
