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
using System.Windows.Media.Animation;
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
                    Grid.SetZIndex(log_grid, 1);
                    Grid.SetZIndex(user_grid, 2);
                }
                else
                {
                    Grid.SetZIndex(user_grid, 1);
                    Grid.SetZIndex(log_grid, 2);
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
                    email_label.Content = cutMail(value.Email);
                    LoggedIn = true;
                }
                else
                {
                    LoggedIn = false;
                }
                user = value;
            }
        }

        private string cutMail(string email)
        {
            int i = email.IndexOf('@');
            string str = email.Substring(i);
            return "" + email[0] + email[1] + "****" + email[i-2] + email[i-1] + str;
        }

        public UserMenuElement()
        {
            InitializeComponent();
          
            SolidColorBrush myAnimatedBrush = new SolidColorBrush();
            log_grid.Background = myAnimatedBrush;
            user_grid.Background = myAnimatedBrush;
            myAnimatedBrush.Color = Color.FromRgb(31, 31, 31);
            this.RegisterName("MyAnimatedBrush", myAnimatedBrush);


            //
            // Animation log_grid on MouseEnter
            //
            ColorAnimation mouseEnterColorAnimation = new ColorAnimation();
            mouseEnterColorAnimation.To = Colors.DarkGray;
            mouseEnterColorAnimation.Duration = TimeSpan.FromSeconds(0.5);
            Storyboard.SetTargetName(mouseEnterColorAnimation, "MyAnimatedBrush");
            Storyboard.SetTargetProperty(
                mouseEnterColorAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard mouseEnterStoryboard = new Storyboard();
            mouseEnterStoryboard.Children.Add(mouseEnterColorAnimation);
            grid.MouseEnter += delegate (object sender, MouseEventArgs e)
            {
                mouseEnterStoryboard.Begin(this);
            };
            

            //
            // Animation log_grid on MouseLeave
            //
            ColorAnimation mouseLeaveColorAnimation = new ColorAnimation();
            mouseLeaveColorAnimation.To = Color.FromRgb(31, 31, 31);
            mouseLeaveColorAnimation.Duration = TimeSpan.FromSeconds(0.5);
            Storyboard.SetTargetName(mouseLeaveColorAnimation, "MyAnimatedBrush");
            Storyboard.SetTargetProperty(
                mouseLeaveColorAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard mouseLeaveStoryboard = new Storyboard();
            mouseLeaveStoryboard.Children.Add(mouseLeaveColorAnimation);
            grid.MouseLeave += delegate (object sender, MouseEventArgs e)
            {
                mouseLeaveStoryboard.Begin(this);
            };
        }

        private void LogIn(object sender, MouseButtonEventArgs e)
        {
            Registration registration = new Registration();
            registration.ShowDialog();
            User = registration.UserBO;
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
