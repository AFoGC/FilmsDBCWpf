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
using UpdaterManager.Models;
using UpdaterManager.Presenters;

namespace UpdaterManager.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window, IMainWindowView
    {
        private readonly MainWindowPresenter presenter;

        public string FilePath { get => FilePathTextBox.Text; set => FilePathTextBox.Text = value; }
        public string UpdateInfo { get => UpdateInfoTextBox.Text; set => UpdateInfoTextBox.Text = value; }

        public MainWindowView()
        {
            InitializeComponent();
            presenter = new MainWindowPresenter(this, new MainWindowModel());
        }

        private void FilePathTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "WpfApp";
            dialog.DefaultExt = ".exe";
            dialog.Filter = "executable file (.exe)|*.exe";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                FilePathTextBox.Text = dialog.FileName;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            presenter.SendNewUpdate();
        }
    }
}
