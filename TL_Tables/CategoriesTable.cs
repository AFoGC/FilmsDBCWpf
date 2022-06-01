using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TL_Objects;

namespace TL_Tables
{
	public class CategoriesTable : Table<Category>
	{
		public CategoriesTable() : base() { }
		public CategoriesTable(int id) : base(id) { }
		public CategoriesTable(int id, string name) : base(id, name) { }

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
