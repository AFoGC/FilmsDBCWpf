using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;

namespace TL_Tables
{
	public class CategoriesTable : Table<Category>
	{
		private int markSystem;
		public bool NewMarkSystem { get; private set; }
		public int MarkSystem
		{
			get => markSystem;
			set
			{
				markSystem = value;
				foreach (Category category in this)
				{
					category.FormatedMark.MarkSystem = markSystem;
				}
			}
		}
		public CategoriesTable()
        {
			NewMarkSystem = false;
			MarkSystem = 6;
            this.CollectionChanged += CategoriesTable_CollectionChanged;
		}

        private void CategoriesTable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				Category category = (Category)e.NewItems[0];
				category.FormatedMark.MarkSystem = MarkSystem;
			}
		}

		protected override void saveBody(StreamWriter streamWriter)
		{
			streamWriter.Write(Cell.FormatParam("newMarkSystem", NewMarkSystem, false, 1));
			streamWriter.Write(Cell.FormatParam("markSystem", MarkSystem, 6, 1));
		}

		protected override void loadBody(Comand comand)
		{
			switch (comand.Paramert)
			{
				case "newMarkSystem":
					NewMarkSystem = Convert.ToBoolean(comand.Value);
					break;
				case "markSystem":
					MarkSystem = Convert.ToInt32(comand.Value);
					break;

				default:
					break;
			}
		}

		public override void ConnectionsSubload(TableCollection tablesCollection)
		{
			Table<Film> filmsTable = tablesCollection.GetTable<Film>();

			List<Film> categoryFilms = new List<Film>();
			foreach (Category category in this)
            {
				categoryFilms.Clear();
                foreach (Film film in filmsTable)
                {
                    if (film.FranshiseId == category.ID)
                    {
						categoryFilms.Add(film);
					}
                }
				sortFilms(category.Films, categoryFilms);
            }

			if (!this.NewMarkSystem)
			{
				changeToNewMarkSystem();
			}
		}

		private void changeToNewMarkSystem()
		{
			foreach (Category category in this)
			{
				category.Mark *= 50;
			}

			this.NewMarkSystem = true;
		}

		private void sortFilms(ObservableCollection<Film> categoryFilms, List<Film> source)
		{
			IEnumerable<Film> enumerable = source.OrderBy(o => o.FranshiseListIndex);
			
			foreach (Film film in enumerable)
			{
				categoryFilms.Add(film);
			}
		}

		public Category GetCategoryByFilm(Film film)
		{
			foreach (Category item in this)
			{
				if (item.Films.Contains(film))
					return item;
			}
			return null;
		}
	}
}
