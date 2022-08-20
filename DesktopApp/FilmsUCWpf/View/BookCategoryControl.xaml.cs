using FilmsUCWpf.ModelBinder;
using FilmsUCWpf.Presenter;
using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View.Interfaces;
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

		public IBasePresenter Presenter => presenter;
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
			SolidColorBrush myBrush = BrushColors.SelectColor;
			this.id.Background = myBrush;
			this.name.Background = myBrush;
			this.mark.Background = myBrush;
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
		private void hide_show_Cilck(object sender, RoutedEventArgs e)
        {
			if (isOpen)
            {
				lowerRow.Height = new GridLength(0);
				isOpen = false;
			}
            else
            {
				lowerRow.Height = GridLength.Auto;
				isOpen = true;
			}
		}

		private void delete_Click(object sender, RoutedEventArgs e) =>
			presenter.DeleteThisCategory();

		private void addSelected_Click(object sender, RoutedEventArgs e) =>
			presenter.AddSelected();

		private void removeSelected_Click(object sender, RoutedEventArgs e) =>
			presenter.RemoveSelected();

		private void cm_Opened(object sender, RoutedEventArgs e) => SetVisualSelected();

		private void cm_Closed(object sender, RoutedEventArgs e) => SetVisualDefault();
    }
}
