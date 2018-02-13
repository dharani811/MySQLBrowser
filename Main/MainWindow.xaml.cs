using Main.Cotrollers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.Themes;
using Xceed.Wpf.AvalonDock.Layout;

namespace Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = MainController.This;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in ThemeManager.GetThemes())
            {
                var menuItem = new MenuItem() { Header = item.ToString() };
                menuItem.PreviewMouseLeftButtonUp += MenuItem_PreviewMouseDown;
                themes.Items.Add(menuItem);
            }
        }

        private void MenuItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ThemeManager.SetTheme(this, (sender as MenuItem).Header.ToString());
            e.Handled = true;
            themes.IsSubmenuOpen = false;
        }

        private void dataBrowser_Click(object sender, RoutedEventArgs e)
        {
            var dataBrowser = new Views.DataBrowser();
            LayoutDocument layoutDocument = new LayoutDocument() {Title="DataBrowser_"+docPane.Children.Count+1 };
            layoutDocument.Content = dataBrowser;
            var iDoc = MainController.This.CreateNewDataBrowser();
            dataBrowser.DataContext = iDoc;
            MainController.This.Document = iDoc;
            docPane.Children.Add(layoutDocument);
        }

        private void queryBrowser_Click(object sender, RoutedEventArgs e)
        {
            var queryBrowser = new Views.QueryBrowser();
            LayoutDocument layoutDocument = new LayoutDocument() { Title = "QueryBrowser" + docPane.Children.Count + 1 };
            layoutDocument.Content = queryBrowser;
            docPane.Children.Add(layoutDocument);

        }
    }

    public class BoolToVis : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
