using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Interpreters
{
   public  class LambdaNode:TreeViewItem
    {
        string name;
        public LambdaNode():base()
        {
            name = "";
        }

        public LambdaNode(string name):this()
        {
            this.name = name;
            this.Header = name;
        }

        public string NodeName {

            get { return name; }
            set { name = value;
                Header = value;
            }
        }
    }
}
