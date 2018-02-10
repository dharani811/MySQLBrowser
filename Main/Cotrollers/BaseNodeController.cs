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
   public class BaseNodeController: DbBase
    {
        private ObservableCollection<LambdaNode> lambdaNodes;

        public BaseNodeController():base()
        {
            lambdaNodes = null;
        }
        public BaseNodeController(DbConnector dbConnector):base(dbConnector)
        {
            lambdaNodes = new ObservableCollection<LambdaNode>();
        }

        public ObservableCollection<LambdaNode> Nodes { get => lambdaNodes; private set => lambdaNodes = value; }

        public void PopulateNodes(List<LambdaNode> nodes, bool append)
        {
            if (!append)
                CurrentDisptacher.Invoke(()=> lambdaNodes.Clear());
            CurrentDisptacher.Invoke(() =>
            {
                foreach (var item in nodes)
                {
                    CurrentDisptacher.Invoke(() => lambdaNodes.Add(item));
                    AssignEvents(item);
                }
            });
        }
        public void PopulateNodes(IEnumerable<string> nodes, bool append)
        {
            if (!append)
                CurrentDisptacher.Invoke(() => lambdaNodes.Clear());
            CurrentDisptacher.Invoke(() =>
            {

                foreach (var item in nodes)
                {
                    var node = new LambdaNode(item);
                    lambdaNodes.Add(node);
                    AssignEvents(node);

                }
            }
            );
        }

        private void AssignEvents(LambdaNode node)
        {
            node.MouseDoubleClick += Node_MouseDoubleClick;
        }

        private void Node_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MouseDoubleClick(sender as LambdaNode);
        }


        protected virtual void MouseDoubleClick(LambdaNode node)
        {

        }
    }
}
