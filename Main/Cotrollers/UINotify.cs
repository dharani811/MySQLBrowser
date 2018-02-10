using Main.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Main.Cotrollers
{
   public  class UINotify : INotifyPropertyChanged
    {
        private bool isVisible;
        private UnitOfWork workUnit;
        
        public UINotify()
        {
            IsVisible = false;
        }

        public void WorkUnit_StatusChanged(object sender, EventArgs e)
        {
            if(workUnit!=null)
            MainController.This.CurrentStatus = workUnit.StatusOfWork.ToString();
        }

        public bool IsVisible { get => isVisible;
            set { isVisible = value; NotifyPropertyChanged("IsVisible"); } }

        public UnitOfWork WorkUnit { get => workUnit; set => workUnit = value; }
        public Dispatcher CurrentDisptacher => Application.Current.Dispatcher;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string  propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
