using FilmsUCWpf.Presenter;
using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
using System;
using System.Collections;
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

namespace FilmsUCWpf.View
{
	/// <summary>
	/// Логика взаимодействия для FilmCategoryControl.xaml
	/// </summary>
	public partial class FilmCategoryControl : UserControl, ICategoryView
	{
		private FilmCategoryPresenter presenter;
		public FilmCategoryControl()
		{
			InitializeComponent();
			DefaultHeght = this.Height;
		}

        public IList CategoryCollection => cat_panel.Children;
		double IView.Height { get => grid.Height; set => grid.Height = value; }
		public double MinimizedHeight => 15;
		public double DefaultHeght { get; private set; }
		public void SelfRemove()
		{
			Panel panel = (Panel)this.Parent;
			if (panel != null)
				panel.Children.Remove(this);
		}

		public bool SetPresenter(IBasePresenter presenter)
		{
			if (this.presenter == null)
			{
				this.presenter = (FilmCategoryPresenter)presenter;
                this.presenter.Model.Films.CollectionChanged += Films_CollectionChanged;
				DataContext = this.presenter;
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool isOpen = true;
        private void Films_CollectionChanged(object sender, EventArgs e)
        {
			isOpen = true;
        }

		private void hide_show_Cilck(object sender, RoutedEventArgs e)
		{
            if (isOpen) grid.Height = MinimizedHeight;
            else presenter.RefreshCategoryFilms();

			isOpen = !isOpen;
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
			throw new NotImplementedException();
		}

		private void btn_update_Click(object sender, RoutedEventArgs e)
		{
			presenter.OpenUpdateMenu();
		}

        private void btn_plus_Click(object sender, RoutedEventArgs e)
        {
			presenter.CreateFilmInCategory();
        }
    }
}
