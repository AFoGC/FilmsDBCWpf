using System.Windows;
using System.Windows.Controls;

namespace WpfApp.TableViews
{
    /// <summary>
    /// Логика взаимодействия для BookInCategorySimpleControl.xaml
    /// </summary>
    public partial class BookInCategorySimpleControl : UserControl
    {
        public BookInCategorySimpleControl()
        {
            InitializeComponent();
        }

        private void OpenCM(object sender, RoutedEventArgs e)
        {
            cm.IsOpen = true;
        }
    }
}
