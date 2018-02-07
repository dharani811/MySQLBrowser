using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Cotrollers
{
   public  class UINotify : INotifyPropertyChanged
    {
        private bool isVisible;

        public UINotify()
        {
            IsVisible = false;
        }

        public bool IsVisible { get => isVisible;
            set { isVisible = value; NotifyPropertyChanged("IsVisible"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string  propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
