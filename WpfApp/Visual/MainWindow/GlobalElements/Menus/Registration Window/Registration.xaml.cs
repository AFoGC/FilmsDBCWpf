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
using BL_Films;
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
            Username.BorderBrush = Brushes.White;
            Password.BorderBrush = Brushes.White;
            SolidColorBrush myAnimatedBrush = new SolidColorBrush();
            SolidColorBrush myAnimatedBrush2 = new SolidColorBrush();
            myAnimatedBrush.Color = Colors.Orange;
            myAnimatedBrush2.Color = Colors.Orange;
            Username.BorderBrush = myAnimatedBrush;
            Password.BorderBrush = myAnimatedBrush2;
            this.RegisterName("MyAnimatedBrush", myAnimatedBrush);
            this.RegisterName("MyAnimatedBrush2", myAnimatedBrush2);

            //
            // Animation username on MouseEnter
            //
            ColorAnimation mouseEnterColorAnimation = new ColorAnimation();
            mouseEnterColorAnimation.To = Colors.LightBlue;
            mouseEnterColorAnimation.Duration = TimeSpan.FromSeconds(1);
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
            mouseLeaveColorAnimation.To = Colors.White;
            mouseLeaveColorAnimation.Duration = TimeSpan.FromSeconds(1);
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
            mouseEnterColorAnimation.To = Colors.LightBlue;
            mouseEnterColorAnimation.Duration = TimeSpan.FromSeconds(1);
            Storyboard.SetTargetName(mouseEnterColorAnimation, "MyAnimatedBrush");
            Storyboard.SetTargetProperty(
                mouseEnterColorAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard mouseEnterStoryboard2 = new Storyboard();
            mouseEnterStoryboard.Children.Add(mouseEnterColorAnimation);
            Password.MouseEnter += delegate (object sender, MouseEventArgs e)
            {
                mouseEnterStoryboard2.Begin(this);
            };
            //
            // Animation Password on MouseLeave
            //
            ColorAnimation mouseLeaveColorAnimation2 = new ColorAnimation();
            mouseLeaveColorAnimation.To = Colors.White;
            mouseLeaveColorAnimation.Duration = TimeSpan.FromSeconds(1);
            Storyboard.SetTargetName(mouseLeaveColorAnimation, "MyAnimatedBrush");
            Storyboard.SetTargetProperty(
                mouseLeaveColorAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard mouseLeaveStoryboard2 = new Storyboard();
            mouseLeaveStoryboard.Children.Add(mouseLeaveColorAnimation);
            Password.MouseLeave += delegate (object sender, MouseEventArgs e)
            {
                mouseLeaveStoryboard2.Begin(this);
            };
        }

        private void Sign_up_Click(object sender, RoutedEventArgs e)
        {
            FilmsBL.Add_User(Username.Text, Password.Text);
        }
    }
}
