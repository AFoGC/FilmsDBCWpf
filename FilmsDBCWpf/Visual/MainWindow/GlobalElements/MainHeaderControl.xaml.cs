using System;
using System.Collections.Generic;
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

namespace FilmsDBCWpf.Visual.MainWindow.GlobalElements
{
    /// <summary>
    /// Логика взаимодействия для MainHeaderControl.xaml
    /// </summary>
    public partial class MainHeaderControl : UserControl
    {
        private Window window;
        public MainHeaderControl()
        {
            InitializeComponent();
            window = App.Current.MainWindow;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        
    }
}
