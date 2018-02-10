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
   public class TableController:BaseNodeController
    {

        private GridController gridController;

        public TableController():base()
        {
            gridController = new GridController();
        }
        public TableController(DbConnector dbConnector):base(dbConnector)
        {
            GridController = new GridController(dbConnector);
        }

        public GridController GridController { get => gridController; set => gridController = value; }

        protected override void MouseDoubleClick(LambdaNode node)
        {
            base.MouseDoubleClick(node);
            gridController.ResetView();
            WorkUnit = new Utils.UnitOfWork(() => gridController.PopulateView(node),true);
            WorkUnit.StatusChanged += WorkUnit_StatusChanged;
            WorkUnit.DoWork();
        }

        
    }
}
