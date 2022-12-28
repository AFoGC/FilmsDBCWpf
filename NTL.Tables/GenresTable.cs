using System;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TL_Objects;

namespace TL_Tables
{
    public class GenresTable : Table<Genre>
    {
        public GenresTable() : base() { }
        public GenresTable(int id) : base(id) { }
        public GenresTable(int id, string name) : base(id, name) { }

        public override void ConnectionsSubload(TableCollection tablesCollection)
        {

        }

        public Genre GetByName(string name)
        {
            foreach (Genre genre in this)
            {
                if (name == genre.Name)
                {
                    return genre;
                }
            }
            return DefaultCell;
        }

        public string[] GetAllGenresNames()
        {
            List<string> export = new List<string>();
            foreach (Genre genre in this)
            {
                export.Add(genre.Name);
            }
            return export.ToArray();
        }

        public static GenresTable GetDefaultGenresTable()
        {
            GenresTable export = new GenresTable();
            export.name = "Genres";

            Genre film = new Genre();
            film.IsSerialGenre = false;
            film.Name = "film";

            Genre series = new Genre();
            series.IsSerialGenre = true;
            series.Name = "series";

            export.AddElement(film);
            export.AddElement(series);

            return export;
        }

        public Genre GetFirstSeriealGenre()
        {
            foreach (Genre genre in this)
            {
                if (genre.IsSerialGenre)
                {
                    return genre;
                }
            }

            return null;
        }
    }
}
