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
    /// Логика взаимодействия для ProgramView.xaml
    /// </summary>
    public partial class ProgramView : Page, IProgramView
    {
        ProgramPresenter presenter;
        public ProgramView()
        {
            presenter = new ProgramPresenter(this);
            InitializeComponent();
        }

        public string UpdateInfo => UpdateInfoTextBox.Text;
        public string ProgramPath => FilePathTextBox.Text;
        public string VersionInfo { set => VersionText.Text = value; }
        public string ZipPath => ZipPathTextBox.Text;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (FilePathTextBox.Text != String.Empty)
            {
                presenter.SendNewUpdate();
            }
            else
            {
                MessageBox.Show("Присутствуют пустые поля");
            }
        }

        private void FilePathTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "WpfApp";
            dialog.DefaultExt = ".exe";
            dialog.Filter = "executable file (.exe)|*.exe";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                FilePathTextBox.Text = dialog.FileName;
                VersionText.Text = FileVersionInfo.GetVersionInfo(ProgramPath).FileVersion;
            }
        }
    }
}
