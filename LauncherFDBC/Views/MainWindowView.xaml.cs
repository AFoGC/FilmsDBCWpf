using LauncherFDBC.Models;
using LauncherFDBC.Presenters;
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

namespace LauncherFDBC.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window, IMainWindowView
    {
        MainWindowPresenter presenter;
        public MainWindowView()
        {
            InitializeComponent();
            presenter = new MainWindowPresenter(new MainWindowModel(), this);
            RefreshCanBeUpdated();
            RefreshIsProgExist();
            RefreshLauncherCanBeUpdated();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            presenter.GetUpdateFromDB();
            RefreshCanBeUpdated();
            RefreshIsProgExist();
        }

        private void updateLauncherButton_Click(object sender, RoutedEventArgs e)
        {
            presenter.GetLauncherUpdateFromDB();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            presenter.RunProgram();
        }

        private void RefreshCanBeUpdated()
        {
            if (presenter.CanBeUpdated())
            {
                updateButton.Background = Brushes.Red;
                updateButton.IsEnabled = true;
            }
            else
            {
                updateButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFDDDDDD");
                updateButton.IsEnabled = false;
            }
        }

        private void RefreshLauncherCanBeUpdated()
        {
            if (presenter.LauncherCanBeUpdated())
            {
                updateLauncherButton.IsEnabled = true;
            }
            else
            {
                updateLauncherButton.IsEnabled = false;
            }
        }

        private void RefreshIsProgExist()
        {
            if (presenter.IsProgramExist())
            {
                startButton.IsEnabled = true;
            }
            else
            {
                startButton.IsEnabled = false;
            }
        }
    }
}
