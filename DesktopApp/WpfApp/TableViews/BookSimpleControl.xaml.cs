using System.Windows;
using System.Windows.Controls;

namespace WpfApp.TableViews
{
	/// <summary>
	/// Логика взаимодействия для BookSimpleControl.xaml
	/// </summary>
	public partial class BookSimpleControl : UserControl
	{
		public BookSimpleControl()
		{
			InitializeComponent();
		}

		private void OpenCM(object sender, RoutedEventArgs e)
		{
			cm.IsOpen = true;
		}
	}
}
