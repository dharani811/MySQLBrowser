using ApiConnector;
using Interpreters;
using Main.Utils;
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
            WorkUnit = new UnitOfWork(()=> DataTable = DbConnector.ExecuteQuery("select * from " + tableNode.NodeName),false);
            WorkUnit.StatusChanged += WorkUnit_StatusChanged;
            WorkUnit.DoWork();

        }

        public void ResetView()
        {
            DataTable = null;
        }
       
    }
}
