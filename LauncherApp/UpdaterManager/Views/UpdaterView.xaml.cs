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
    /// Логика взаимодействия для UpdaterView.xaml
    /// </summary>
    public partial class UpdaterView : Page, IUpdaterView
    {
        private UpdaterPresenter presenter;
        public string UpdaterPath => updaterPath.Text;

        public UpdaterView()
        {
            presenter = new UpdaterPresenter(this);
            InitializeComponent();
        }

        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "WpfApp";
            dialog.DefaultExt = ".exe";
            dialog.Filter = "executable file (.exe)|*.exe";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                updaterPath.Text = dialog.FileName;
                VersionText.Text = FileVersionInfo.GetVersionInfo(UpdaterPath).FileVersion;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            presenter.SendNewUpdate();
        }
    }
}
