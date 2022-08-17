using FilmsUCWpf.ModelBinder;
using FilmsUCWpf.Presenter;
using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using TablesLibrary.Interpreter;
using TL_Objects;

namespace FilmsUCWpf.View
{
    /// <summary>
    /// Логика взаимодействия для FilmSerieUpdateControl.xaml
    /// </summary>
    public partial class FilmSerieUpdateControl : UserControl
	{
		private FilmSerieUpdatePresenter presenter;
		public FilmSerieUpdateControl(Film film, IMenuPresenter<Film> menu, TableCollection collection)
		{
			InitializeComponent();
			presenter = new FilmSerieUpdatePresenter(film, menu);
			DataContext = new FilmSerieBinder(film);
		}

		private bool commentIsOpen = false;
		private void btn_comment_Click(object sender, RoutedEventArgs e)
		{
			if (commentIsOpen)
				lowerRow.Height = new GridLength(0);
			else
				lowerRow.Height = GridLength.Auto;

			commentIsOpen = !commentIsOpen;
		}

		private void btn_sources_Click(object sender, RoutedEventArgs e)
		{
			presenter.OpenSources();
		}

		private void watched_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
            if (watched.IsChecked == false)
            {
				if (watchDate.IsEmpty)
				{
					watchDate.Date = DateTime.Today;
				}

				if (startWatchDate.IsEmpty)
				{
					startWatchDate.Date = DateTime.Today;
				}

				int cows = 0;
				int ts = 0;
				bool emptyTotal = true;

				if (countOfWatchedSeries.Text != "")
				{
					cows = Convert.ToInt32(countOfWatchedSeries.Text);
				}

				if (totalSeries.Text != "")
				{
					ts = Convert.ToInt32(totalSeries.Text);
					emptyTotal = false;
				}

				if (cows < ts && emptyTotal == false)
				{
					countOfWatchedSeries.Text = totalSeries.Text;
				}

				if (countOfViews.Text == "")
				{
					countOfViews.Text = "1";
				}
				watched.IsChecked = !watched.IsChecked;
			}
		}

		private void watched_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (watched.IsChecked == false)
			{
				if (countOfViews.Text == "")
				{
					countOfViews.Text = "1";
				}
			}
		}

		private void TextInputIsNumber(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}
    }
}
