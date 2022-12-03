using System.Windows;
using System.Windows.Controls;

namespace WpfApp.TableViews
{
	/// <summary>
	/// Логика взаимодействия для FilmCategoryControl.xaml
	/// </summary>
	public partial class FilmCategoryControl : UserControl
	{
		public FilmCategoryControl()
		{
			InitializeComponent();
		}

		private void OpenCM(object sender, RoutedEventArgs e)
		{
			cm.IsOpen = true;
		}
	}
}
