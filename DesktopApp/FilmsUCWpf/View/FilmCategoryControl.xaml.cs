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
    public partial class FilmCategoryControl : UserControl
	{
		public FilmCategoryControl()
		{
			InitializeComponent();
		}
		private void hide_show_Cilck(object sender, RoutedEventArgs e)
		{
			
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
			
		}

        private void btn_plus_Click(object sender, RoutedEventArgs e)
        {
			
        }

        private void cm_Opened(object sender, RoutedEventArgs e) => SetVisualSelected();
        private void cm_Closed(object sender, RoutedEventArgs e) => SetVisualDefault();

		private void delete_Click(object sender, RoutedEventArgs e) { }

		private void addSelected_Click(object sender, RoutedEventArgs e) { }

        private void removeSelected_Click(object sender, RoutedEventArgs e) { }

        private void OpenCM(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
