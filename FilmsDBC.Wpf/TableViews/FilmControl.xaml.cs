using System.Windows;
using System.Windows.Controls;

namespace WpfApp.TableViews
{
    /// <summary>
    /// Логика взаимодействия для FilmControl.xaml
    /// </summary>
    public partial class FilmControl : UserControl
    {

        public FilmControl()
        {
            InitializeComponent();
        }

        private void OpenCM(object sender, RoutedEventArgs e)
        {
            cm.IsOpen = true;
        }
    }
}
