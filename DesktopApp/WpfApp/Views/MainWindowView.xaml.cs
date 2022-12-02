using System.Windows;
using System.Windows.Input;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
            //DataContext = new MainViewModel();
        }

        private void header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
