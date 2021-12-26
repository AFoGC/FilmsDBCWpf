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
using System.Windows.Threading;
namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl
{
    /// <summary>
    /// Логика взаимодействия для SettingsList.xaml
    /// </summary>
    public partial class SettingsList : UserControl
    {
        public SettingsList()
        {
            InitializeComponent();
            Grid.Height = 20;
        }
        private void Expand_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = Grid.Height;
            if  (Grid.Height == 80)
            {
                doubleAnimation.To = 20;
            }
            else
            {
                doubleAnimation.To = 80;
            }
            doubleAnimation.Duration = TimeSpan.FromSeconds(0.3);
            Grid.BeginAnimation(Grid.HeightProperty, doubleAnimation);
            
            
            
        }

    }
}
