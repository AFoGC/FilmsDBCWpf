using FilmsUCWpf.ModelBinder;
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

namespace FilmsUCWpf.View
{
	/// <summary>
	/// Логика взаимодействия для BookCategoryControl.xaml
	/// </summary>
	public partial class BookCategoryControl : UserControl, ICategoryView
	{
		private BookCategoryPresenter presenter;
		public BookCategoryControl()
		{
			InitializeComponent();
		}

		double IView.Height { get => grid.Height; set => grid.Height = value; }
		public double DefaultHeght => 35;
		public double MinimizedHeight => 15;
		public IList CategoryCollection => cat_panel.Children;

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
				this.presenter = (BookCategoryPresenter)presenter;
				DataContext = new BookCategoryBinder(this.presenter.Model);
				return true;
			}
			else
			{
				return false;
			}
		}

        public void SetVisualDefault()
		{
			SolidColorBrush myBrush = BrushColors.DefaultColor;
			this.id.Background = myBrush;
			this.name.Background = myBrush;
			this.mark.Background = myBrush;
		}

		public void SetVisualFinded()
		{
			SolidColorBrush myBrush = BrushColors.FindColor;
			this.id.Background = myBrush;
			this.name.Background = myBrush;
			this.mark.Background = myBrush;
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
			presenter.CreateBookInCategory();
        }

		private bool isOpen = true;
		public void Maximize()
		{
			isOpen = true;
			Height = DefaultHeght + getControlsHeight();
		}

		public void Minimize()
		{
			isOpen = false;
			Height = MinimizedHeight;
		}

		private double getControlsHeight()
		{
			double height = 0;

			foreach (Control control in cat_panel.Children)
				height += control.Height;

			return height;
		}

		private void hide_show_Cilck(object sender, RoutedEventArgs e)
        {
			if (isOpen) 
				Minimize();
			else 
				Maximize();
		}

		private void delete_Click(object sender, RoutedEventArgs e) =>
			presenter.DeleteThisCategory();

		private void addSelected_Click(object sender, RoutedEventArgs e) =>
			presenter.AddSelected();

		private void removeSelected_Click(object sender, RoutedEventArgs e) =>
			presenter.RemoveSelected();
    }
}
