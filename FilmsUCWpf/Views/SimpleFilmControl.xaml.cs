using FilmsUCWpf.Presenters;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TL_Objects;

namespace FilmsUCWpf.Views
{
    /// <summary>
    /// Логика взаимодействия для SimpleFilmControl.xaml
    /// </summary>
    public partial class SimpleFilmControl : UserControl, IView<Film>
    {
        public SimpleFilmPresenter Presenter { get; private set; }
        public Film Info => Presenter.Model;
        public SimpleFilmControl(Film film, IMenu<Film> menu)
        {
            InitializeComponent();
            Presenter = new SimpleFilmPresenter(film, this, menu);
            DataContext = film;
        }

        private void id_GotFocus(object sender, RoutedEventArgs e)
        {
            Presenter.SetSelectedElement();
        }

        private void btn_moreInfo_Click(object sender, RoutedEventArgs e)
        {
            Presenter.MoreInfoMenu();
        }

        public void SetVisualDefault()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(53, 53, 53));
            this.id.Background = myBrush;
        }

        public void SetVisualSelected()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 220, 0));
            this.id.Background = myBrush;
        }

        public void SetVisualFinded()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 0, 220));
            this.id.Background = myBrush;
        }

        public void SelfRemove()
        {
            Panel panel = (Panel)this.Parent;
            panel.Children.Remove(this);
        }
    }
}
