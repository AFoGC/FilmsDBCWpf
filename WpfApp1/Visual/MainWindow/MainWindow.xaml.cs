using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BL_Films;
namespace WpfApp.Visual.MainWindow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public GlobalElements.Menus.FilmsMenu.FilmsMenuControl FilmsMenu
        {
            get { return films_menu; }
        }

        private void Add_To_Base_Click(object sender, RoutedEventArgs e)
        {
            //BL_Films.FilmsBL.Add_User(Convert.ToInt32(id.Text), Username.Text.ToString(), Password.Text.ToString());
        }

        
    }
}
