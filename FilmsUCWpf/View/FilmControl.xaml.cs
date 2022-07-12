using FilmsUCWpf.ModelBinder;
using FilmsUCWpf.Presenter;
using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View.Interfaces;
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

namespace FilmsUCWpf.View
{
    /// <summary>
    /// Логика взаимодействия для FilmControl.xaml
    /// </summary>
    public partial class FilmControl : UserControl, IView
	{
		private FilmPresenter presenter;
		public IBasePresenter Presenter => presenter;
		public bool SetPresenter(IBasePresenter presenter)
		{
			if (this.presenter == null) 
			{
				this.presenter = (FilmPresenter)presenter;
				DataContext = new FilmBinder(this.presenter.Model);
				return true;
			}
            else
            {
				return false;
            }
		}

		public FilmControl()
		{
			InitializeComponent();
		}

		private void id_GotFocus(object sender, RoutedEventArgs e)
		{
			presenter.SetSelectedElement();
		}

		private void id_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			presenter.SetSelectedElement();
		}

		private bool commentIsOpen = false;

        double IView.Height { get => this.Height; set { this.Height = value; } }

        private void btn_comment_Click(object sender, RoutedEventArgs e)
		{
			if (commentIsOpen) { this.grid.Height -= 15; }
			else { this.grid.Height += 15; }

			commentIsOpen = !commentIsOpen;
		}

		private void btn_copyUrl_Click(object sender, RoutedEventArgs e)
		{
			presenter.CopyUrl();
		}

		private void btn_update_Click(object sender, RoutedEventArgs e)
		{
			presenter.OpenUpdateMenu();
		}

		public void SelfRemove()
		{
			Panel panel = (Panel)this.Parent;
			if (panel != null) 
				panel.Children.Remove(this);
		}

		public void SetVisualDefault()
		{
			SolidColorBrush myBrush = BrushColors.DefaultColor;
			this.id.Background = myBrush;
			this.name.Background = myBrush;
			this.genre.Background = myBrush;
			this.realiseYear.Background = myBrush;
			this.watchDate.Background = myBrush;
			this.mark.Background = myBrush;
			this.countOfviews.Background = myBrush;
		}

		public void SetVisualFinded()
		{
			SolidColorBrush myBrush = BrushColors.FindColor;
			this.id.Background = myBrush;
			this.name.Background = myBrush;
			this.genre.Background = myBrush;
			this.realiseYear.Background = myBrush;
			this.watchDate.Background = myBrush;
			this.mark.Background = myBrush;
			this.countOfviews.Background = myBrush;
		}

		public void SetVisualSelected()
		{
			SolidColorBrush myBrush = BrushColors.SelectColor;
			this.id.Background = myBrush;
			this.name.Background = myBrush;
			this.genre.Background = myBrush;
			this.realiseYear.Background = myBrush;
			this.watchDate.Background = myBrush;
			this.mark.Background = myBrush;
			this.countOfviews.Background = myBrush;
		}

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
			presenter.DeleteThis();
        }

        private void addToPriority_Click(object sender, RoutedEventArgs e)
        {
			presenter.AddToPriority();
        }
    }
}
