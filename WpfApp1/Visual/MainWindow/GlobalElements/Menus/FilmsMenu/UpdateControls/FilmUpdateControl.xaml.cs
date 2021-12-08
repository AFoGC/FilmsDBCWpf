using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.FilmsControls;
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

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.UpdateControls
{
    /// <summary>
    /// Логика взаимодействия для FilmUpdateControl.xaml
    /// </summary>
    public partial class FilmUpdateControl : UserControl, IUpdateControl
    {
        public FilmUpdateControl(FilmControl filmControl)
        {
            InitializeComponent();
        }

        public void UpdateElement()
        {
            throw new NotImplementedException();
        }
    }
}
