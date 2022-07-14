using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using UpdaterManager.Presenters;
using UpdaterManager.Views.Interfaces;

namespace UpdaterManager.Views
{
    /// <summary>
    /// Логика взаимодействия для LauncherView.xaml
    /// </summary>
    public partial class LauncherView : Page, ILauncherView
    {
        private LauncherPresenter presenter;
        public string LauncherPath => LauncherPathTextBox.Text;
        public LauncherView()
        {
            presenter = new LauncherPresenter(this);
            InitializeComponent();
        }

        private void LauncherPathTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "WpfApp";
            dialog.DefaultExt = ".exe";
            dialog.Filter = "executable file (.exe)|*.exe";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                LauncherPathTextBox.Text = dialog.FileName;
                VersionText.Text = FileVersionInfo.GetVersionInfo(LauncherPath).FileVersion;
            }
        }

        private void Button_Click_SendLauncher(object sender, RoutedEventArgs e)
        {
            presenter.SendNewUpdate();
        }
    }
}
