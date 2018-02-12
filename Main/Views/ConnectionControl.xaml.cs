using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Main.Views
{
    /// <summary>
    /// Interaction logic for ConnectionControl.xaml
    /// </summary>
    public partial class ConnectionControl : UserControl
    {
        public ConnectionControl()
        {
            InitializeComponent();
        }

        private void conBox2_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(conBox2.Visibility==Visibility.Visible)
            {
                conBox1.Visibility = Visibility.Visible;
                serverIpTxt.Visibility = Visibility.Hidden;
                serverTextBlock.Visibility = Visibility.Hidden;
            }
            else
            {
                conBox1.Visibility = Visibility.Collapsed;
                serverIpTxt.Visibility = Visibility.Visible;
                serverTextBlock.Visibility = Visibility.Visible;

            }
        }
    }
}
