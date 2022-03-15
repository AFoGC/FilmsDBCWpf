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
    /// Логика взаимодействия для FilmCategoryControl.xaml
    /// </summary>
    public partial class FilmCategoryControl : UserControl, ICategoryView<Category>
    {
        public FilmCategoryPresenter Presenter { get; private set; }
        public FilmCategoryControl(Category category, IMenu<Film> menu)
        {
            InitializeComponent();
            Presenter = new FilmCategoryPresenter(category, this, menu);
            DataContext = category;
            
        }

        public Category Info => Presenter.Model;

        public UIElementCollection CategoryCollection => cat_panel.Children;

        public void SelfRemove()
        {
            Panel panel = (Panel)this.Parent;
            panel.Children.Remove(this);
        }

        public void SetVisualDefault()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(53, 53, 53));
            this.id.Background = myBrush;
        }

        public void SetVisualFinded()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 0, 220));
            this.id.Background = myBrush;
        }

        public void SetVisualSelected()
        {
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromRgb(0, 220, 0));
            this.id.Background = myBrush;
        }
    }
}
