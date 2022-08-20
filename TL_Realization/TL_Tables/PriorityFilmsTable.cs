using System;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TL_Objects;

namespace TL_Tables
{
    public class PriorityFilmsTable : Table<PriorityFilm>
    {
        public PriorityFilmsTable() : base() { }
        public PriorityFilmsTable(int id) : base(id) { }
        public PriorityFilmsTable(int id, string name) : base(id, name) { }

        public void RemoveWatchedFilms()
        {
            PriorityFilm priFilm;
            for (int i = 0; i < this.Count; i++)
            {
                priFilm = this[i];
                if (priFilm.Film.Watched)
                {
                    this.Remove(priFilm);
                    --i;
                }
            }
        }

        public bool ContainFilm(Film film)
        {
            foreach (PriorityFilm priority in this)
            {
                if (priority.Film == film)
                {
                    return true;
                }
            }
            return false;
        }

        public PriorityFilm GetElementByFilm(Film film)
        {
            foreach (PriorityFilm priority in this)
            {
                if (priority.Film == film)
                {
                    return priority;
                }
            }
            return null;
        }

        public override void ConnectionsSubload(TableCollection tablesCollection)
        {
            Table<Film> filmsTable = tablesCollection.GetTable<Film>();

            foreach (Film film in filmsTable)
            {
                foreach (PriorityFilm priorityFilm in this)
                {
                    if (priorityFilm.FilmId == film.ID)
                    {
                        priorityFilm.Film = film;
                    }
                }
            }
        }
    }
}
