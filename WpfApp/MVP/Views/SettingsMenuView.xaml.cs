using System;
using System.Collections;
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
using WpfApp.MVP.Models;
using WpfApp.MVP.Presenters;
using WpfApp.MVP.ViewsInterface;

namespace WpfApp.MVP.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingsMenuView.xaml
    /// </summary>
    public partial class SettingsMenuView : UserControl, ISettingsMenuView
    {
        public IList SettingsList => SettingsListPanel.Children;

        private readonly SettingsMenuPresenter presenter;
        public SettingsMenuView(MainWindowModel model)
        {
            presenter = new SettingsMenuPresenter(new SettingsMenuModel(), this, model);
            InitializeComponent();
            LogInUser.SetUserModel(model);
            presenter.InitializeSettingsPanel();

        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("Довідка до програми.pdf");
        }

        private void SendToDB_Click(object sender, RoutedEventArgs e)
        {
            presenter.SendToDB();
        }

        private void GetFromDB_Click(object sender, RoutedEventArgs e)
        {
            presenter.GetFromDB();
        }
    }
}
