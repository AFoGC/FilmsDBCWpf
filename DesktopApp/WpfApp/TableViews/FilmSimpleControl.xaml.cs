using System.Windows;
using System.Windows.Controls;

namespace WpfApp.TableViews
{
	/// <summary>
	/// Логика взаимодействия для FilmSimpleControl.xaml
	/// </summary>
	public partial class FilmSimpleControl : UserControl
	{
		public FilmSimpleControl()
		{
			InitializeComponent();
		}

		private void OpenCM(object sender, RoutedEventArgs e)
		{
			cm.IsOpen = true;
		}
	}
}
