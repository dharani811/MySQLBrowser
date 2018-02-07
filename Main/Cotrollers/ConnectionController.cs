using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Main.Cotrollers
{
    public class ConnectionController:UINotify
    {
        private string serverName;
        private string userName;
        private string password;
        private string port;
        private ICommand applyCommand;
        private ICommand cancelCommand;


        public ConnectionController()
        {
            IsVisible = true;
            applyCommand = new RelayCommand((p) => OnApply());
            cancelCommand = new RelayCommand((p) => OnCancel());
        }

        private void OnCancel()
        {
            IsVisible = false;
        }

        private void OnApply()
        {
            IsVisible = false;
            MainController.This.MainController_ConnectionChanged(this, null);
        }

        public string ServerName { get => serverName; set => serverName = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string Port { get => port; set => port = value; }
        public ICommand ApplyCommand { get => applyCommand; set => applyCommand = value; }
        public ICommand CancelCommand { get => cancelCommand; set => cancelCommand = value; }
        public string ConnectionString { get { return ConnectionStringConcat(); } }

        private string ConnectionStringConcat()
        {
            return "Server=" + serverName + ";UserName=" + userName + ";Password=" + password + ";Port=" + port;
        }
    }
}
