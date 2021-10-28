using System;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter;
using TL_Objects;

namespace TL_Tables
{
    public class SeriesTable : Table<Serie>
    {
        public SeriesTable() : base() { }
        public SeriesTable(int id) : base(id) { }
        public SeriesTable(int id, string name) : base(id, name) { }

        public override void ConnectionsSubload(TableCollection tablesCollection)
        {
            Table<Film> filmsTable = tablesCollection.GetTable<Film>();

            foreach (Serie serie in this)
            {
                serie.Film = filmsTable.GetElementByIndex(serie.FilmId);
            }
        }
    }
}
