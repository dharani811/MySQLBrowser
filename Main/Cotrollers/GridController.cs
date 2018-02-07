using ApiConnector;
using Interpreters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Cotrollers
{
   public  class GridController:DbBase
    {
        private DataTable dataTable;

        public DataTable DataTable { get => dataTable;

            private set  { dataTable = value; NotifyPropertyChanged("DataTable"); }
        }

        public GridController():base()
        {

        }

        public GridController(DbConnector dbConnector):base(dbConnector)
        {
        }

        public void PopulateView(LambdaNode tableNode)
        {
            DataTable = DbConnector.ExecuteQuery("select * from " + tableNode.NodeName);

        }

        public void ResetView()
        {
            DataTable = null;
        }
       
    }
}
