using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL_Films;
using BO_Films;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.Registration_Window
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            Email.Background = Brushes.White;
            Password.Background = Brushes.White;
            SolidColorBrush myAnimatedBrush = new SolidColorBrush();
            SolidColorBrush myAnimatedBrush2 = new SolidColorBrush();
            Email.Background = myAnimatedBrush;
            myAnimatedBrush.Color = Colors.Transparent;
            myAnimatedBrush2.Color = Colors.Transparent;
            Password.Background = myAnimatedBrush2;
            this.RegisterName("MyAnimatedBrush", myAnimatedBrush);
            this.RegisterName("MyAnimatedBrush2", myAnimatedBrush2);

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
            Email.MouseEnter += delegate (object sender, MouseEventArgs e)
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
            Email.MouseLeave += delegate (object sender, MouseEventArgs e)
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
            SignUpMenu.Sign_up.Click += SignUpHide;
            SignUpMenu.btn_return.Click += SignUpHide;
        }
        public void SignUpHide(object sender, EventArgs e)
        {
            SignUpMenu.Visibility = Visibility.Hidden;
            RegistrationMenu.Visibility = Visibility.Visible;
        }

        private void Sign_up_Click(object sender, RoutedEventArgs e)
        {
            SignUpMenu.Visibility = Visibility.Visible;
            RegistrationMenu.Visibility = Visibility.Hidden;
            
        }

        public UserBO UserBO { get; private set; }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.LoggedInUser = UserBL.LogIn(Email.Text, Password.Text);
            this.Close();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle, 0x112, 0xf012, 0);
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Username_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Email.Text == "Email")
            {
                Email.Text = "";
            }
            else
            {

            }
        }

        private void Username_LostFocus(object sender, RoutedEventArgs e)
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
    }
}
