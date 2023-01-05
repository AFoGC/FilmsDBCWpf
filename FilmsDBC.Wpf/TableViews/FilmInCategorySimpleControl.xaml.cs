using System.Windows;
using System.Windows.Controls;

namespace WpfApp.TableViews
{
    /// <summary>
    /// Логика взаимодействия для FilmInCategorySimpleControl.xaml
    /// </summary>
    public partial class FilmInCategorySimpleControl : UserControl
    {
        public FilmInCategorySimpleControl()
        {
            InitializeComponent();
        }

        private void OpenCM(object sender, RoutedEventArgs e)
        {
            cm.IsOpen = true;
        }
    }
}
