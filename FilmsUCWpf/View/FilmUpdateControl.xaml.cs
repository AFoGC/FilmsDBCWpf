using FilmsUCWpf.Presenter;
using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View.Interfaces;
using InfoMenusWpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
using TL_Tables;

namespace FilmsUCWpf.View
{
    /// <summary>
    /// Логика взаимодействия для FilmUpdateControl.xaml
    /// </summary>
    public partial class FilmUpdateControl : UserControl, IFilmUpdateView, IUpdateControl
	{
		private FilmUpdatePresenter presenter;
		public FilmUpdateControl(Film film, IMenu<Film> menu, TableCollection table)
		{
			InitializeComponent();
			presenter = new FilmUpdatePresenter(film, this, menu, table);
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

		private void TextInputIsNumber(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void watched_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
            if (watched.IsChecked == false)
            {
				if (watchDate.IsEmpty)
				{
					watchDate.Date = DateTime.Today;
				}

				if (countOfViews.Text == "")
				{
					countOfViews.Text = "1";
				}
			}
			watched.IsChecked = !watched.IsChecked;
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
		public IList Genres { get => genre.Items; }
		public Genre Genre { get => (Genre)genre.SelectedItem; set => genre.SelectedItem = value; }
		public string RealiseYear { get => realiseYear.Text; set => realiseYear.Text = value; }
		public bool Wathced { get => (bool)watched.IsChecked; set => watched.IsChecked = value; }
		public IList Marks => mark.Items;
		public string Mark { get => mark.Text; set => mark.Text = value; }
		public string CountOfViews { get => countOfViews.Text; set => countOfViews.Text = value; }
		public DateTime DateOfWatch { get => watchDate.Date; set => watchDate.Date = value; }
		public string Comment { get => comment.Text; set => comment.Text = value; }

		private void genre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
