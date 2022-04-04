﻿using FilmsUCWpf.Presenter;
using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
using InfoMenusWpf;
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

			if (countOfViews.Text == "")
			{
				countOfViews.Text = "1";
			}
		}

		public void UpdateElement()
		{
			presenter.UpdateElement();
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