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
using BO_Films;
using WpfApp.Visual.HelpWindows.LogOutWindow;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.Registration_Window;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsElements
{
    /// <summary>
    /// Логика взаимодействия для UserMenuElement.xaml
    /// </summary>
    public partial class UserMenuElement : UserControl
    {
        private bool loggedIn;
        public bool LoggedIn
        {
            get
            {
                return loggedIn;
            }
            set
            {
                if (value)
                {
                    Grid.SetZIndex(log_grid, 0);
                    Grid.SetZIndex(user_grid, 1);
                }
                else
                {
                    Grid.SetZIndex(user_grid, 0);
                    Grid.SetZIndex(log_grid, 1);
                }
                loggedIn = value;
            }
        }

        private UserBO user;
        public UserBO User
        {
            get
            {
                return User;
            }

            private set
            {
                if (value != null)
                {
                    username_label.Content = "User " + value.Username;
                    email_label.Content = value.Email;
                    LoggedIn = true;
                }
                else
                {
                    LoggedIn = false;
                }
                user = value;
            }
        }
        public UserMenuElement()
        {
            InitializeComponent();
        }

        private void LogIn(object sender, MouseButtonEventArgs e)
        {
            //UserBO item = new UserBO();
            //item.Email = "example@gmail.com";
            //item.Username = "Fuhrer";
            //User = item;
            Registration registration = new Registration();
            registration.ShowDialog();
        }

        private void LogOut(object sender, MouseButtonEventArgs e)
        {
            LogOutWindow logOutWindow = new LogOutWindow();
            logOutWindow.ShowDialog();
            if (logOutWindow.LogOut)
            {
                User = null;
            }
        }
    }
}
