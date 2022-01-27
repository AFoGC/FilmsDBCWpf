using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.FilmsControls;
using System;
using System.Collections.Generic;
using System.Text;
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
using WpfApp.StaticFilmClasses;
using WpfApp.Visual.StaticVisualClasses;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.UpdateControls
{
	/// <summary>
	/// Логика взаимодействия для FilmUpdateControl.xaml
	/// </summary>
	public partial class FilmUpdateControl : UserControl, IUpdateControl
	{
		private FilmControl filmControl = null;
		private Film film = null;
		public FilmUpdateControl(FilmControl filmControl)
		{
			InitializeComponent();
			this.filmControl = filmControl;
			this.film = filmControl.FilmInfo;

			foreach (var item in MainInfo.Tables.GenresTable)
			{
				genre.Items.Add(item);
			}
			foreach (var item in FilmMethods.GetAllMarks().ToArray())
			{
				mark.Items.Add(item);
			}

			refresh();
		}

		private void refresh()
		{
			Film defFilm = MainInfo.Tables.FilmsTable.DefaultCell;

			this.id.Text = film.ID.ToString();
			this.name.Text = film.Name;
			this.genre.SelectedItem = film.Genre;
			this.realiseYear.Text = Film.FormatToString(film.RealiseYear, defFilm.RealiseYear);
			this.watched.IsChecked = film.Watched;
			this.mark.SelectedItem = VisualHelper.markToText(Film.FormatToString(film.Mark, defFilm.Mark));
			this.countOfViews.Text = Film.FormatToString(film.CountOfViews, defFilm.CountOfViews);
			this.watchDate.Date = film.DateOfWatch;
			this.comment.Text = film.Comment;
		}

		public void UpdateElement()
		{
			film.Name = this.name.Text;
			if (!film.Genre.IsSerialGenre && ((Genre)this.genre.SelectedItem).IsSerialGenre)
			{
				MainInfo.Tables.SeriesTable.FindAndConnectSerie(film);
			}
			film.Genre = (Genre)this.genre.SelectedItem;
			film.RealiseYear = VisualHelper.TextToInt32(this.realiseYear.Text);
			film.Watched = (bool)this.watched.IsChecked;
			film.Mark = VisualHelper.TextToMark(this.mark.Text);
			film.CountOfViews = VisualHelper.TextToInt32(this.countOfViews.Text);
			film.DateOfWatch = this.watchDate.Date;
			film.Comment = this.comment.Text;

			filmControl.RefreshData();
		}

		private void btn_sources_Click(object sender, RoutedEventArgs e)
		{
			MainInfo.MainWindow.FilmsMenu.UpdateFormVisualizer.SourcesVisualizer.OpenSourceControl(this.film.Sources);
		}

		private bool commentIsOpen = false;
		private void btn_comment_Click(object sender, RoutedEventArgs e)
		{
			if (commentIsOpen) { this.grid.Height -= 20; }
			else { this.grid.Height += 20; }

			commentIsOpen = !commentIsOpen;
		}

		private void watched_Click(object sender, RoutedEventArgs e)
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

		private void genre_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			/*
			if (((Genre)genre.SelectedItem).IsSerialGenre)
			{
				MainInfo.MainWindow.FilmsMenu.UpdateVisualizer.OpenUpdateControl(filmControl);
			}
			*/
		}
	}
}
