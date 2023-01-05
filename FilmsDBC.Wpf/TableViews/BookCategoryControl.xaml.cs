using System.Windows;
using System.Windows.Controls;

namespace WpfApp.TableViews
{
    /// <summary>
    /// Логика взаимодействия для BookCategoryControl.xaml
    /// </summary>
    public partial class BookCategoryControl : UserControl
    {
        public BookCategoryControl()
        {
            InitializeComponent();
        }

        private void OpenCM(object sender, RoutedEventArgs e)
        {
            cm.IsOpen = true;
        }
    }
}
