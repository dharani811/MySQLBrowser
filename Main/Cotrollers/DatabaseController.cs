using ApiConnector;
using Interpreters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Cotrollers
{
  public   class DatabaseController:BaseNodeController
    {
        TableController tableController;

        public DatabaseController():base()
        {
            tableController = new TableController();
        }
        public DatabaseController(DbConnector dbConnector):base(dbConnector)
        {
            tableController = new TableController(dbConnector);
        }

        public TableController TableController { get => tableController; set => tableController = value; }

        protected override void MouseDoubleClick(LambdaNode node)
        {
            base.MouseDoubleClick(node);

            tableController.PopulateNodes(DbConnector.GetTables(node.NodeName),false);
            tableController.GridController.ResetView();
        }
    }
}
