using System;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfApp.Controls
{
    /// <summary>
    /// Логика взаимодействия для SingUpControl.xaml
    /// </summary>
    public partial class SingUpControl : UserControl
    {
        public SingUpControl()
        {
            InitializeComponent();
            Username.Background = Brushes.White;
            Password.Background = Brushes.White;
            Email.Background = Brushes.White;
            SolidColorBrush myAnimatedBrush = new SolidColorBrush();
            SolidColorBrush myAnimatedBrush2 = new SolidColorBrush();
            SolidColorBrush myAnimatedBrush3 = new SolidColorBrush();
            Username.Background = myAnimatedBrush;
            myAnimatedBrush.Color = Colors.Transparent;
            myAnimatedBrush2.Color = Colors.Transparent;
            Password.Background = myAnimatedBrush2;
            Email.Background = myAnimatedBrush3;
            this.RegisterName("MyAnimatedBrush", myAnimatedBrush);
            this.RegisterName("MyAnimatedBrush2", myAnimatedBrush2);
            this.RegisterName("MyAnimatedBrush3", myAnimatedBrush3);


            //
            // Animation username on MouseEnter
            //
            ColorAnimation mouseEnterColorAnimation = new ColorAnimation();
            mouseEnterColorAnimation.To = Colors.Gray;
            mouseEnterColorAnimation.Duration = TimeSpan.FromSeconds(0.5);
            Storyboard.SetTargetName(mouseEnterColorAnimation, "MyAnimatedBrush");
            Storyboard.SetTargetProperty(
                mouseEnterColorAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard mouseEnterStoryboard = new Storyboard();
            mouseEnterStoryboard.Children.Add(mouseEnterColorAnimation);
            Username.MouseEnter += delegate (object sender, MouseEventArgs e)
            {
                mouseEnterStoryboard.Begin(this);
            };


            //
            // Animation on MouseLeave
            //
            ColorAnimation mouseLeaveColorAnimation = new ColorAnimation();
            mouseLeaveColorAnimation.To = Colors.Transparent;
            mouseLeaveColorAnimation.Duration = TimeSpan.FromSeconds(0.5);
            Storyboard.SetTargetName(mouseLeaveColorAnimation, "MyAnimatedBrush");
            Storyboard.SetTargetProperty(
                mouseLeaveColorAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard mouseLeaveStoryboard = new Storyboard();
            mouseLeaveStoryboard.Children.Add(mouseLeaveColorAnimation);
            Username.MouseLeave += delegate (object sender, MouseEventArgs e)
            {
                mouseLeaveStoryboard.Begin(this);
            };

            //
            // Animation password on MouseEnter
            //
            ColorAnimation mouseEnterColorAnimation2 = new ColorAnimation();
            mouseEnterColorAnimation2.To = Colors.Gray;
            mouseEnterColorAnimation2.Duration = TimeSpan.FromSeconds(0.5);
            Storyboard.SetTargetName(mouseEnterColorAnimation2, "MyAnimatedBrush2");
            Storyboard.SetTargetProperty(
                mouseEnterColorAnimation2, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard mouseEnterStoryboard2 = new Storyboard();
            mouseEnterStoryboard2.Children.Add(mouseEnterColorAnimation2);
            Password.MouseEnter += delegate (object sender, MouseEventArgs e)
            {
                mouseEnterStoryboard2.Begin(this);
            };
            //
            // Animation Password on MouseLeave
            //
            ColorAnimation mouseLeaveColorAnimation2 = new ColorAnimation();
            mouseLeaveColorAnimation2.To = Colors.Transparent;
            mouseLeaveColorAnimation2.Duration = TimeSpan.FromSeconds(0.5);
            Storyboard.SetTargetName(mouseLeaveColorAnimation2, "MyAnimatedBrush2");
            Storyboard.SetTargetProperty(
                mouseLeaveColorAnimation2, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard mouseLeaveStoryboard2 = new Storyboard();
            mouseLeaveStoryboard2.Children.Add(mouseLeaveColorAnimation2);
            Password.MouseLeave += delegate (object sender, MouseEventArgs e)
            {
                mouseLeaveStoryboard2.Begin(this);
            };

            //
            // Animation Email on MouseEnter
            //
            ColorAnimation mouseEnterColorAnimation3 = new ColorAnimation();
            mouseEnterColorAnimation3.To = Colors.Gray;
            mouseEnterColorAnimation3.Duration = TimeSpan.FromSeconds(0.5);
            Storyboard.SetTargetName(mouseEnterColorAnimation3, "MyAnimatedBrush3");
            Storyboard.SetTargetProperty(
                mouseEnterColorAnimation3, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard mouseEnterStoryboard3 = new Storyboard();
            mouseEnterStoryboard3.Children.Add(mouseEnterColorAnimation3);
            Email.MouseEnter += delegate (object sender, MouseEventArgs e)
            {
                mouseEnterStoryboard3.Begin(this);
            };
            // Animation Email on MouseLeave
            //
            ColorAnimation mouseLeaveColorAnimation3 = new ColorAnimation();
            mouseLeaveColorAnimation3.To = Colors.Transparent;
            mouseLeaveColorAnimation3.Duration = TimeSpan.FromSeconds(0.5);
            Storyboard.SetTargetName(mouseLeaveColorAnimation3, "MyAnimatedBrush3");
            Storyboard.SetTargetProperty(
                mouseLeaveColorAnimation3, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard mouseLeaveStoryboard3 = new Storyboard();
            mouseLeaveStoryboard3.Children.Add(mouseLeaveColorAnimation3);
            Email.MouseLeave += delegate (object sender, MouseEventArgs e)
            {
                mouseLeaveStoryboard3.Begin(this);
            };
        }

        private void Username_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Username.Text == "Username")
            {
                Username.Text = "";
            }
            else
            {

            }
        }

        private void Username_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Username.Text.Length == 0)
            {
                Username.Text = "Username";
            }
            else
            {

            }
        }

        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Email.Text == "Email")
            {
                Email.Text = "";
            }
            else
            {

            }
        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Email.Text.Length == 0)
            {
                Email.Text = "Email";
            }
            else
            {

            }
        }
        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Password.Text == "Password")
            {
                Password.Text = "";
            }
            else
            {

            }
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Password.Text.Length == 0)
            {
                Password.Text = "Password";
            }
            else
            {

            }
        }

        private void Sign_up_Click(object sender, RoutedEventArgs e)
        {
            if (checkEmail())
            {

            }
        }

        private bool checkEmail()
        {
            try
            {
                MailAddress m = new MailAddress(Email.Text);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void btn_return_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
