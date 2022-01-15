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
          
            SolidColorBrush myAnimatedBrush = new SolidColorBrush();
            //SolidColorBrush myAnimatedBrush2 = new SolidColorBrush();
            log_grid.Background = myAnimatedBrush;
            user_grid.Background = myAnimatedBrush;
            myAnimatedBrush.Color = Color.FromRgb(31, 31, 31);
            //myAnimatedBrush2.Color = Color.FromRgb(31, 31, 31);
            this.RegisterName("MyAnimatedBrush", myAnimatedBrush);
            //this.RegisterName("MyAnimatedBrush2", myAnimatedBrush2);

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
            log_grid.MouseEnter += delegate (object sender, MouseEventArgs e)
            {
                mouseEnterStoryboard.Begin(this);
            };
            // Если не получится - удалить
            user_grid.MouseEnter += delegate (object sender, MouseEventArgs e)
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
            log_grid.MouseLeave += delegate (object sender, MouseEventArgs e)
            {
                mouseLeaveStoryboard.Begin(this);
            };
            // Если не получится - удалить
            user_grid.MouseLeave += delegate (object sender, MouseEventArgs e)
            {
                mouseEnterStoryboard.Begin(this);
            };

            //
            // Animation user_grid on MouseEnter
            //

            /*ColorAnimation mouseEnterColorAnimation2 = new ColorAnimation();
            mouseEnterColorAnimation2.To = Colors.DarkGray;
            mouseEnterColorAnimation2.Duration = TimeSpan.FromSeconds(0.5);
            Storyboard.SetTargetName(mouseEnterColorAnimation2, "MyAnimatedBrush2");
            Storyboard.SetTargetProperty(
                mouseEnterColorAnimation2, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard mouseEnterStoryboard2 = new Storyboard();
            mouseEnterStoryboard.Children.Add(mouseEnterColorAnimation2);
            user_grid.MouseEnter += delegate (object sender, MouseEventArgs e)
            {
                mouseEnterStoryboard2.Begin(this);
            };


            //
            // Animation user_grid on MouseLeave
            //
            ColorAnimation mouseLeaveColorAnimation2 = new ColorAnimation();
            mouseLeaveColorAnimation2.To = Color.FromRgb(31, 31, 31);
            mouseLeaveColorAnimation2.Duration = TimeSpan.FromSeconds(0.5);
            Storyboard.SetTargetName(mouseLeaveColorAnimation2, "MyAnimatedBrush2");
            Storyboard.SetTargetProperty(
                mouseLeaveColorAnimation2, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard mouseLeaveStoryboard2 = new Storyboard();
            mouseLeaveStoryboard2.Children.Add(mouseLeaveColorAnimation2);
            user_grid.MouseLeave += delegate (object sender, MouseEventArgs e)
            {
                mouseLeaveStoryboard2.Begin(this);
            };*/
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
