using FilmsUCWpf.Presenter;
using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
using InfoMenusWpf;
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
	public partial class FilmSerieUpdateControl : UserControl, IFilmSerieUpdateView, IUpdateControl
	{
		private FilmSerieUpdatePresenter presenter;
		public FilmSerieUpdateControl(Film film, IMenu<Film> menu, TableCollection collection)
		{
			InitializeComponent();
			presenter = new FilmSerieUpdatePresenter(film, this, menu, collection);
		}

		private bool commentIsOpen = false;
		private void btn_comment_Click(object sender, RoutedEventArgs e)
		{
			if (commentIsOpen) { this.grid.Height -= 20; }
			else { this.grid.Height += 20; }

			commentIsOpen = !commentIsOpen;
		}

		private void btn_sources_Click(object sender, RoutedEventArgs e)
		{
			presenter.OpenSources();
		}

		private void watched_Click(object sender, RoutedEventArgs e)
		{
			if (watchDate.IsEmpty)
			{
				watchDate.Date = DateTime.Today;
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
		}

		private void TextInputIsNumber(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		public void UpdateElement()
		{
			try
			{
				presenter.UpdateElement();
			}
			catch
			{
				MessageBox.Show("Invalid data type entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		public string ID { set => id.Text = value; }
		string IFilmUpdateView.Name { get => name.Text; set => name.Text = value; }
		public IList Genres => genre.Items;
		public Genre Genre { get => (Genre)genre.SelectedItem; set => genre.SelectedItem = value; }
		public string RealiseYear { get => realiseYear.Text; set => realiseYear.Text = value; }
		public bool Wathced { get => (bool)watched.IsChecked; set => watched.IsChecked = value; }
		public IList Marks => mark.Items;
		public string Mark { get => mark.Text; set => mark.Text = value; }
		public string CountOfViews { get => countOfViews.Text; set => countOfViews.Text = value; }
		public DateTime DateOfWatch { get => watchDate.Date; set => watchDate.Date = value; }
		public string Comment { get => comment.Text; set => comment.Text = value; }

		public DateTime StartWatchDate { get => startWatchDate.Date; set => startWatchDate.Date = value; }
		public string CountOfWatchedSeries { get => countOfWatchedSeries.Text; set => countOfWatchedSeries.Text = value; }
		public string TotalSeries { get => totalSeries.Text; set => totalSeries.Text = value; }
	}
}
