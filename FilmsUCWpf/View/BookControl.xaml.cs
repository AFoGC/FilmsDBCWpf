using FilmsUCWpf.Presenter;
using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
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
	/// Логика взаимодействия для BookControl.xaml
	/// </summary>
	public partial class BookControl : UserControl, IBaseView
	{
		BookPresenter presenter;
		public BookControl()
		{
			InitializeComponent();
		}

		private void id_GotFocus(object sender, RoutedEventArgs e)
		{
			presenter.SetSelectedElement();
		}

		private void btn_copyUrl_Click(object sender, RoutedEventArgs e)
		{
			presenter.CopyUrl();
		}

		private void btn_update_Click(object sender, RoutedEventArgs e)
		{
			presenter.OpenUpdateMenu();
		}

		public bool SetPresenter(IBasePresenter presenter)
		{
			if (this.presenter == null)
			{
				this.presenter = (BookPresenter)presenter;
				DataContext = this.presenter;
				return true;
			}
			else
			{
				return false;
			}
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
