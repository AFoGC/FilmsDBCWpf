using System.Windows;
using System.Windows.Controls;

namespace WpfApp.TableViews
{
    /// <summary>
    /// Логика взаимодействия для BookControl.xaml
    /// </summary>
    public partial class BookControl : UserControl
    {
        public BookControl()
        {
            InitializeComponent();
        }

        private void OpenCM(object sender, RoutedEventArgs e)
        {
            cm.IsOpen = true;
        }
    }
}
