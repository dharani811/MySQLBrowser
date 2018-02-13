using ApiConnector;
using Contracts;
using Interpreters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Main.Cotrollers
{
   public  class MainController:UINotify
    {

        private string currentStatus;
        private IDocument document;
        public static MainController This = new MainController();
        public MainController()
        {
            currentStatus = "Idle";
        }

       
    
        public String CurrentStatus
        {
            get => currentStatus;
            set
            {
                currentStatus = value;
                NotifyPropertyChanged("CurrentStatus");
            }
        }

        public IDocument Document { get => document; set { document = value; NotifyPropertyChanged("Connect"); } }
        public IDocument CreateNewDataBrowser()
        {
            IDocument doc = new DataBrowserController();
            return doc;
        }
    }
}
