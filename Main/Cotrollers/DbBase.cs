using ApiConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Cotrollers
{
    public class DbBase:UINotify
    {
        private DbConnector dbConnector;
        private string warning;
        private string message;


        public DbBase():base()
        {

        }
        public DbBase(DbConnector dbConnector) : base()
        {
            this.dbConnector = dbConnector;
            SubscribeEvents();
        }

        public void SubscribeEvents()
        {
            dbConnector.ErrorOccured += DbConnector_ErrorOccured;
            dbConnector.WarningOccured += DbConnector_WarningOccured;
        }

        private void DbConnector_WarningOccured(object sender, EventArgs e)
        {
            Warning = dbConnector.Warnings;
        }

        private void DbConnector_ErrorOccured(object sender, EventArgs e)
        {
            Message = dbConnector.Message;
        }

        public DbConnector DbConnector { get => dbConnector; set => dbConnector = value; }
        public string Warning { get => warning; set { warning = value; NotifyPropertyChanged("Warning"); } }
        public string Message { get => message; set { message = value; NotifyPropertyChanged("Message"); } }
    }
}
