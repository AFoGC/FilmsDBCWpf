using BO_Films;
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
using WpfApp.Models;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для UserSettingsControl.xaml
    /// </summary>
    public partial class UserSettingsControl : UserControl
    {
        private IUserModel model;
        public void SetUserModel(IUserModel userModel)
        {
            if (model != null)
            {
                model = userModel;
                User = model.LoggedInUser;
                model.UserChanged += LogIn;
            }
        }

        private bool loggedIn;
        public bool LoggedIn
        {
            get
            {
                return loggedIn;
            }
            private set
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
        private UserBO User
        {
            get
            {
                return user;
            }

            set
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

                if (model.Settings.StartUser.LoggedIn = LoggedIn)
                {
                    model.Settings.StartUser.Email = user.Email;
                    model.Settings.StartUser.Password = user.Password;
                }
                else
                {
                    model.Settings.StartUser.Email = String.Empty;
                    model.Settings.StartUser.Password = String.Empty;
                }
                model.Settings.SaveSettings();
            }
        }

        private string cutMail(string email)
        {
            int i = email.IndexOf('@');
            string str = email.Substring(i);
            return "" + email[0] + email[1] + "****" + email[i - 2] + email[i - 1] + str;
        }

        public UserSettingsControl()
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

        public void LogIn(object sender, EventArgs e)
        {
            User = model.LoggedInUser;
        }

        private void LogIn(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindow registration = new RegistrationWindow(model);
            registration.ShowDialog();
        }

        private void LogOut(object sender, MouseButtonEventArgs e)
        {
            LogOutWindow logOutWindow = new LogOutWindow();
            logOutWindow.ShowDialog();
            if (logOutWindow.LogOut)
            {
                model.LoggedInUser = null;
            }
        }
    }
}
