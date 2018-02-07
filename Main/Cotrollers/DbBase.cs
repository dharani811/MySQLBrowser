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

        public DbBase():base()
        {

        }
        public DbBase(DbConnector dbConnector) : base()
        {
            this.dbConnector = dbConnector;
        }

        public DbConnector DbConnector { get => dbConnector; set => dbConnector = value; }
    }
}
