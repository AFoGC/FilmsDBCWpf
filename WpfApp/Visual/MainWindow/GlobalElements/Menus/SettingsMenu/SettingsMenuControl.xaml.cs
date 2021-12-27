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
using WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu
{
    /// <summary>
    /// Логика взаимодействия для SettingsMenuControl.xaml
    /// </summary>
    public partial class SettingsMenuControl : UserControl
    {
        private SettingsControl.SettingsList SettingsList;
        public SettingsMenuControl()
        {
            InitializeComponent();

            SettingsList = new SettingsControl.SettingsList();

            RefreshControl();
            SettingsListPanel.Children.Add(SettingsList);
        }

        public void RefreshControl()
        {
            SettingsList.RefreshControl();
        }

        private void clickButton_embraceSettings_Click(object sender, EventArgs e)
        {
            foreach (ISettingsControl setting in SettingsListPanel.Children)
            {
                setting.GetSettings();
            }


            MainInfo.TableCollection.LoadTables();
        }
    }
}
